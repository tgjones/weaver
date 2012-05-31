namespace Weaver.CodeModel
{
	public class FloatShaderPropertyNode : ShaderPropertyNode
	{
		public float Value { get; set; }

		public override string HlslValue
		{
			get { return Value.ToString(); }
		}
	}
}