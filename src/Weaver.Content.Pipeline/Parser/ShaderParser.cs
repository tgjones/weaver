using System;
using System.Linq;
using Weaver.Content.Pipeline.CodeModel;

namespace Weaver.Content.Pipeline.Parser
{
	public class ShaderParser : Parser
	{
		public ShaderParser(string path, Token[] tokens)
			: base(path, tokens)
		{
		}

		public ShaderNode Parse()
		{
			var result = new ShaderNode();

			Eat(TokenType.Shader);
			result.Name = ParseString().Value;

			Eat(TokenType.OpenCurly);

			result.Properties = ParseProperties();
		    result.Technique = ParseTechnique();

			Eat(TokenType.CloseCurly);
			Eat(TokenType.Eof);

			return result;
		}

        private TechniqueNode ParseTechnique()
        {
            var result = new TechniqueNode();

            Eat(TokenType.Technique);
            Eat(TokenType.OpenCurly);

            if (PeekType() == TokenType.State)
                result.State = ParseState();

            result.Surface = ParseSurface();

            Eat(TokenType.CloseCurly);

            return result;
        }

        private StateNode ParseState()
        {
            var result = new StateNode();

            Eat(TokenType.State);
            Eat(TokenType.OpenCurly);

            while (PeekType() != TokenType.CloseCurly)
                ParseStateProperty(result);

            Eat(TokenType.CloseCurly);

            return result;
        }

        private void ParseStateProperty(StateNode stateNode)
        {
            var stateNameIdentifier = (IdentifierToken) Eat(TokenType.Identifier);
            var stateName = stateNameIdentifier.Identifier;
            var propertyInfo = typeof(StateNode).GetProperty(stateName);
            if (propertyInfo == null)
                ReportError("Unrecognized identifier: {0}", stateNameIdentifier, stateName);

            Eat(TokenType.Equal);

            var propertyType = propertyInfo.PropertyType;
            var underlyingType = Nullable.GetUnderlyingType(propertyType);

            object value = null;
            switch (PeekType())
            {
                case TokenType.Identifier:
                    if (!underlyingType.IsEnum)
                        ReportError("Invalid value for {0}.", stateName);
                    var stateValueIdentifier = (IdentifierToken) Eat(TokenType.Identifier);
                    var stateValue = stateValueIdentifier.Identifier;
                    if (!underlyingType.GetEnumNames().Contains(stateValue))
                        ReportError("Unexpected value: {0}", stateValueIdentifier, stateValue);
                    value = Enum.Parse(underlyingType, stateValue);
                    break;
                case TokenType.Literal:
                    if (underlyingType != typeof(float))
                        ReportError("Invalid value for {0}.", stateName);
                    value = ((FloatToken) Eat(TokenType.Literal)).Value;
                    break;
                case TokenType.True:
                    if (underlyingType != typeof(bool))
                        ReportError("Invalid value for {0}.", stateName);
                    Eat(TokenType.True);
                    value = true;
                    break;
                case TokenType.False:
                    if (underlyingType != typeof(bool))
                        ReportError("Invalid value for {0}.", stateName);
                    Eat(TokenType.False);
                    value = false;
                    break;
                default:
                    ReportError("Unexpected value for {0}.", stateName);
                    break;
            }
            propertyInfo.SetValue(stateNode, value, null);

            Eat(TokenType.Semicolon);
        }

		private SurfaceNode ParseSurface()
		{
			var result = new SurfaceNode();

			Eat(TokenType.Surface);
			Eat(TokenType.OpenCurly);

			Eat(TokenType.LightingModel);
			Eat(TokenType.Equal);
			result.LightingModel = ParseString().Value;
			Eat(TokenType.Semicolon);

			result.Code = ((ShaderCodeToken) Eat(TokenType.ShaderCode)).ShaderCode;

			Eat(TokenType.CloseCurly);

			return result;
		}
	}
}