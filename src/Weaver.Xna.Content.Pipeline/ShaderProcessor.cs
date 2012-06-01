using System.IO;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace Weaver.Xna.Content.Pipeline
{
	[ContentProcessor(DisplayName = "Shader - Weaver")]
	public class ShaderProcessor : ContentProcessor<ShaderContent, CompiledShaderContent>
	{
		public override CompiledShaderContent Process(ShaderContent input, ContentProcessorContext context)
		{
			// Build new .fx file.
			string effectCode = BuildFxFile(input);

			// Save to temporary .stitchedeffect file.
			string tempFxFile = GetTempFileName(input, ".fx");
			File.WriteAllText(tempFxFile, effectCode, Encoding.GetEncoding(1252));

			// Run standard effect processor.
			CompiledEffectContent compiledEffect = context.BuildAndLoadAsset<EffectContent, CompiledEffectContent>(
				new ExternalReference<EffectContent>(tempFxFile),
				"EffectProcessor");
			return new CompiledShaderContent { Effect = compiledEffect };
		}

		// Use a semi-unique filename so that multiple shaders can be worked on and the resulting
		// fx files opened simultaneously. This does mean you'll end up with several of the resulting
		// fx files in your temp folder, but they're quite small files.
		protected string GetTempFileName(ShaderContent input, string extension)
		{
			return Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetFileName(input.Identity.SourceFilename), extension));
		}

		private string BuildFxFile(ShaderContent input)
		{
			return "TODO";
		}
	}
}