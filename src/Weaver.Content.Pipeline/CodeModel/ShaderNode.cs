namespace Weaver.Content.Pipeline.CodeModel
{
	public class ShaderNode : ParseNode
	{
		public string Name { get; set; }
		public ShaderPropertyNodeCollection Properties { get; set; }
		public TechniqueNode Technique { get; set; }
	}
}