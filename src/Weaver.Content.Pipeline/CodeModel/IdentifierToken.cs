using Weaver.Content.Pipeline.Parser;

namespace Weaver.Content.Pipeline.CodeModel
{
	public class IdentifierToken : Token
	{
		public string Identifier { get; private set; }

		public IdentifierToken(string identifier, string sourcePath, BufferPosition position)
			: base(TokenType.Identifier, sourcePath, position)
		{
			Identifier = identifier;
		}

		public override string ToString()
		{
			return Identifier;
		}
	}
}