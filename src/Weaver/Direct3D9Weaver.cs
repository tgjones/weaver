using Weaver.SurfaceShaders.CodeModel;

namespace Weaver
{
	public class Direct3D9Weaver : Weaver
	{
		public override string Weave(ShaderNode shaderNode, LightType lightType)
		{
			// TODO: Add #define's for what technique syntax to use
			return base.Weave(shaderNode, lightType);
		}
	}
}