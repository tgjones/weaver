using Weaver.Content.Pipeline.Parser;

namespace Weaver.Content.Pipeline.CodeModel
{
	public class IntToken : LiteralToken
	{
		public int Value { get; private set; }

		public IntToken(int value, string sourcePath, BufferPosition position)
			: base(LiteralTokenType.Int, sourcePath, position)
		{
			Value = value;
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}