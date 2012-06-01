using System;

namespace Weaver.SurfaceShaders.CodeModel
{
	public class TextureShaderPropertyNode : ShaderPropertyNode
	{
		public string Value { get; set; }

		public override string HlslValue
		{
			get { return "NULL"; }
		}

		public override string HlslDeclaration
		{
			get
			{
				return base.HlslDeclaration + Environment.NewLine
					+ string.Format("sampler {0}Sampler = sampler_state {{ Texture = {0}; }};", Name);
			}
		}
	}
}