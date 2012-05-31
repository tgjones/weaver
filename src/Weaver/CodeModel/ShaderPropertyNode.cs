namespace Weaver.CodeModel
{
	public abstract class ShaderPropertyNode : ParseNode
	{
		public Token Type { get; set; }
		public string Name { get; set; }

		public virtual string HlslValue { get { return null; } }
	}
}