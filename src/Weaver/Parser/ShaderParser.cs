using System.Collections.Generic;
using Weaver.CodeModel;

namespace Weaver.Parser
{
	public class ShaderParser : Parser
	{
		public ShaderParser(string path, Token[] tokens)
			: base(path, tokens)
		{
		}

		public ShaderNode Parse()
		{
			ShaderNode result = new ShaderNode();

			Eat(TokenType.Shader);
			result.Name = ParseString().Value;

			Eat(TokenType.OpenCurly);

			result.Properties = ParseProperties();

			result.Surface = ParseSurface();

			Eat(TokenType.CloseCurly);
			Eat(TokenType.Eof);

			return result;
		}

		private SurfaceNode ParseSurface()
		{
			SurfaceNode result = new SurfaceNode();

			Eat(TokenType.Surface);
			Eat(TokenType.OpenCurly);

			Eat(TokenType.LightingModel);
			result.LightingModel = ParseString().Value;

			result.Inputs = ParseSurfaceInputs();

			result.Code = ((ShaderCodeToken) Eat(TokenType.ShaderCode)).ShaderCode;

			Eat(TokenType.CloseCurly);

			return result;
		}

		private List<SurfaceInputNode> ParseSurfaceInputs()
		{
			List<SurfaceInputNode> results = new List<SurfaceInputNode>();

			Eat(TokenType.Inputs);
			Eat(TokenType.OpenCurly);

			while (PeekType() != TokenType.CloseCurly)
				results.Add(ParseSurfaceInput());

			Eat(TokenType.CloseCurly);

			return results;
		}

		private SurfaceInputNode ParseSurfaceInput()
		{
			SurfaceInputNode result = new SurfaceInputNode();

			result.Type = EatDataType();
			result.Name = ((IdentifierToken) Eat(TokenType.Identifier)).Identifier;

			return result;
		}
	}
}