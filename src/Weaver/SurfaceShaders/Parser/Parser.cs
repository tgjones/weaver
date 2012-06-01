using System;
using Weaver.Properties;
using Weaver.SurfaceShaders.CodeModel;

namespace Weaver.SurfaceShaders.Parser
{
	public abstract class Parser
	{
		public event ErrorEventHandler Error;

		private readonly string _path;
		private readonly Token[] _tokens;
		private BufferPosition _lastErrorPosition;

		protected int TokenIndex { get; set; }

		protected Parser(string path, Token[] tokens)
		{
			_path = path;
			_tokens = tokens;
		}

		protected ShaderPropertyNodeCollection ParseProperties()
		{
			Eat(TokenType.Properties);
			Eat(TokenType.OpenCurly);

			ShaderPropertyNodeCollection properties = new ShaderPropertyNodeCollection();
			while (PeekType() != TokenType.CloseCurly)
				properties.Add(ParseShaderProperty());

			Eat(TokenType.CloseCurly);

			return properties;
		}

		private ShaderPropertyNode ParseShaderProperty()
		{
			Token dataType = EatDataType();
			IdentifierToken propertyName = (IdentifierToken)Eat(TokenType.Identifier);

			Eat(TokenType.Equal);

			ShaderPropertyNode shaderPropertyNode = ParseShaderPropertyDefaultValue(dataType, propertyName);

			Eat(TokenType.Semicolon);

			return shaderPropertyNode;
		}

		private ShaderPropertyNode ParseShaderPropertyDefaultValue(Token dataType, IdentifierToken propertyName)
		{
			switch (dataType.Type)
			{
				case TokenType.Float:
				{
					return new FloatShaderPropertyNode
					{
						Type = dataType,
						Name = propertyName.Identifier,
						Value = ParseNumber()
					};
				}
				case TokenType.Float2:
				{
					Eat(TokenType.OpenParen);
					float[] numbers = new float[2];
					for (int i = 0; i < 2; ++i)
					{
						numbers[i] = ParseNumber();
						if (i < 1)
							Eat(TokenType.Comma);
					}
					Eat(TokenType.CloseParen);
					return new Vector2ShaderPropertyNode
					{
						Type = dataType,
						Name = propertyName.Identifier,
						Value = new Vector2(numbers[0], numbers[1])
					};
				}
				case TokenType.Float3:
				{
					Eat(TokenType.OpenParen);
					float[] numbers = new float[3];
					for (int i = 0; i < 3; ++i)
					{
						numbers[i] = ParseNumber();
						if (i < 2)
							Eat(TokenType.Comma);
					}
					Eat(TokenType.CloseParen);
					return new Vector3ShaderPropertyNode
					{
						Type = dataType,
						Name = propertyName.Identifier,
						Value = new Vector3(numbers[0], numbers[1], numbers[2])
					};
				}
				case TokenType.Texture2D:
				case TokenType.TextureCube:
					string value = ParseString().Value;
					return new TextureShaderPropertyNode
					{
						Type = dataType,
						Name = propertyName.Identifier,
						Value = value
					};
				default:
					ReportUnexpectedError(dataType.Type);
					throw new NotSupportedException();
			}
		}

		protected float ParseNumber()
		{
			LiteralToken numberToken = (LiteralToken)Eat(TokenType.Literal);
			switch (numberToken.LiteralType)
			{
				case LiteralTokenType.Float :
					return ((FloatToken) numberToken).Value;
				case LiteralTokenType.Int :
					return ((IntToken)numberToken).Value;
				default :
					ReportError(Resources.ParserTokenUnexpected, numberToken.ToString());
					throw new NotSupportedException();
			}
		}

		protected StringToken ParseString()
		{
			//System.Diagnostics.Debugger.Launch();
			LiteralToken stringToken = (LiteralToken)Eat(TokenType.Literal);
			if (stringToken.LiteralType != LiteralTokenType.String)
			{
				ReportError(Resources.ParserTokenUnexpected, stringToken.ToString());
				throw new NotSupportedException();
			}
			return (StringToken)stringToken;
		}

		protected Token EatDataType()
		{
            if (Token.IsDataType(PeekType()))
				return NextToken();
			ReportError(Resources.ParserDataTypeExpected, PeekToken());
			return ErrorToken();
		}

		protected Token Eat(TokenType type)
		{
			if (PeekType() == type)
				return NextToken();
			ReportTokenExpectedError(type);
			return ErrorToken();
		}

		private Token NextToken()
		{
			return _tokens[TokenIndex++];
		}

		protected TokenType PeekType(int index = 0)
		{
			return PeekToken(index).Type;
		}

		private Token PeekToken(int index = 0)
		{
			return _tokens[TokenIndex + index];
		}

		private Token ErrorToken()
		{
			return new Token(TokenType.Error, _path, PeekToken().Position);
		}

		private void ReportTokenExpectedError(TokenType type)
		{
			ReportError(Resources.ParserTokenExpected, Token.GetString(type));
		}

		private void ReportUnexpectedError(TokenType type)
		{
			ReportError(Resources.ParserTokenUnexpected, Token.GetString(type));
		}

		protected void ReportError(string message, params object[] args)
		{
			ReportError(message, PeekToken(), args);
		}

		protected void ReportError(string message, Token token, params object[] args)
		{
			BufferPosition position = token.Position;
			if (Error != null && _lastErrorPosition != position)
				Error(this, new ErrorEventArgs(string.Format(message, args), position));
			_lastErrorPosition = position;
		}
	}
}