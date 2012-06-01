using System.Text;
using Weaver.SurfaceShaders.CodeModel;

namespace Weaver
{
	public class Direct3D9Weaver : Weaver
	{
		public override string Weave(ShaderNode shaderNode, LightType lightType)
		{
			return base.Weave(shaderNode, lightType) + CombineHlslFragments(
				GetTechniques());
		}

		private static string GetTechniques()
		{
			var sb = new StringBuilder();
			sb.AppendLine("technique");
			sb.AppendLine("{");
			sb.AppendLine("\tpass");
			sb.AppendLine("\t{");
			sb.AppendLine("\t\tVertexShader = compile vs_3_0 VS();");
			sb.AppendLine("\t\tPixelShader = compile ps_3_0 PS();");
			sb.AppendLine("\t}");
			sb.AppendLine("}");
			return sb.ToString();
		}
	}
}