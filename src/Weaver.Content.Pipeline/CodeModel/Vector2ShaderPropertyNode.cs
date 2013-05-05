using Microsoft.Xna.Framework;

namespace Weaver.Content.Pipeline.CodeModel
{
	public class Vector2ShaderPropertyNode : ShaderPropertyNode
	{
		public Vector2 Value { get; set; }

		public override string HlslValue
		{
			get { return string.Format("float2({0}, {1})", Value.X, Value.Y); }
		}
	}
}