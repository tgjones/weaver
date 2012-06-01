using Weaver.SurfaceShaders.CodeModel;

namespace Weaver.SurfaceShaders.Parser
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
			Eat(TokenType.Equal);
			result.LightingModel = ParseString().Value;
			Eat(TokenType.Semicolon);

			result.Code = ((ShaderCodeToken) Eat(TokenType.ShaderCode)).ShaderCode;

			Eat(TokenType.CloseCurly);

			return result;
		}
	}
}