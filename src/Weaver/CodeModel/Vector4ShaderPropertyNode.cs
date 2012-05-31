namespace Weaver.CodeModel
{
	public class Vector4ShaderPropertyNode : ShaderPropertyNode
	{
		public Vector4 Value { get; set; }

		public override string HlslValue
		{
			get { return string.Format("float4({0}, {1}, {2}, {3})", Value.X, Value.Y, Value.Z, Value.W); }
		}
	}
}