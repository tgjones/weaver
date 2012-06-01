namespace Weaver.SurfaceShaders.CodeModel
{
	public abstract class ShaderPropertyNode : ParseNode
	{
		public Token Type { get; set; }
		public string Name { get; set; }

		public abstract string HlslValue { get; }

		public virtual string HlslDeclaration
		{
			get { return string.Format("{0} {1} = {2};", Type, Name, HlslValue); }
		}
	}
}