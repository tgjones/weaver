namespace Weaver.CodeModel
{
	public class Vector3ShaderPropertyNode : ShaderPropertyNode
	{
		public Vector3 Value { get; set; }

		public override string HlslValue
		{
			get { return string.Format("float3({0}, {1}, {2})", Value.X, Value.Y, Value.Z); }
		}
	}
}