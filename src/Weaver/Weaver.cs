using System.IO;
using System.Text;
using Weaver.SurfaceShaders.CodeModel;

namespace Weaver
{
	public abstract class Weaver
	{
		public virtual string Weave(ShaderNode shaderNode, LightType lightType)
		{
			return CombineHlslFragments(
				GetDefines(shaderNode),
				GetHlsl("Macros"),
				GetHlsl("Lights.LightCommon"),
				GetHlsl("Lights." + lightType + "Light"),
				GetHlsl("LightingModels." + shaderNode.Surface.LightingModel),
				GetHlsl("VertexShaderInput"),
				GetHlsl("VertexShaderOutput"),
				GetHlsl("VertexShader"),
				GetHlsl("PixelShaderOutput"),
				GetHlsl("SurfaceInput"),
				GetSurfaceProperties(shaderNode),
				shaderNode.Surface.Code,
				GetHlsl("PixelShader"));
		}

		protected static string CombineHlslFragments(params string[] hlslFragments)
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

		private static string GetDefines(ShaderNode shaderNode)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(string.Format("#define SURFACE_OUTPUT_NAME {0}SurfaceOutput", shaderNode.Surface.LightingModel));
			sb.AppendLine(string.Format("#define LIGHTING_MODEL_FUNC Lighting{0}", shaderNode.Surface.LightingModel));
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

		private static string GetSurfaceProperties(ShaderNode shaderNode)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("// Surface properties");
			foreach (var propertyNode in shaderNode.Properties)
				sb.AppendLine(propertyNode.HlslDeclaration);
			sb.AppendLine();
			sb.AppendLine();
			return sb.ToString();
		}
	}
}