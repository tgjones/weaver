using Weaver.Parser;

namespace Weaver.CodeModel
{
	public class ShaderCodeToken : Token
	{
		public string ShaderCode { get; private set; }

		public ShaderCodeToken(string shaderCode, string sourcePath, BufferPosition position)
			: base(TokenType.ShaderCode, sourcePath, position)
		{
			ShaderCode = shaderCode;
		}

		public override string ToString()
		{
			return ShaderCode;
		}
	}
}