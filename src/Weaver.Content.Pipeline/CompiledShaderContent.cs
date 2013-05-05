using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace Weaver.Content.Pipeline
{
	public class CompiledShaderContent : ContentItem
	{
		public CompiledEffectContent Effect { get; set; }
	}
}