using System.IO;
using System.Text;
using Weaver.SurfaceShaders.CodeModel;

namespace Weaver
{
	public static class Weaver
	{
		public static string Weave(ShaderNode shaderNode, LightType lightType)
		{
			return CombineHlslFragments(
				GetHlsl("LightingModels." + shaderNode.Surface.LightingModel),
				GetHlsl("Lights.LightCommon"),
				GetHlsl("Lights." + lightType + "Light"),
				shaderNode.Surface.Code,
				GetHlsl("VertexShaderInput"),
				GetHlsl("VertexShaderOutput"),
				GetHlsl("VertexShader"),
				GetHlsl("PixelShaderOutput"),
				GetHlsl("PixelShader"));
		}

		private static string CombineHlslFragments(params string[] hlslFragments)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string hlslFragment in hlslFragments)
			{
				sb.AppendLine(hlslFragment);
				sb.AppendLine();
				sb.AppendLine();
			}
			return sb.ToString();
		}

		private static string GetHlsl(string resourceName)
		{
			using (var streamReader = new StreamReader(GetResourceStream(resourceName)))
			{
				return streamReader.ReadToEnd();
			}
		}

		private static Stream GetResourceStream(string resourceName)
		{
			return typeof(Weaver).Assembly.GetManifestResourceStream(typeof(Weaver), "Resources." + resourceName + ".hlsl");
		}
	}
}