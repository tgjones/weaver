using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace Weaver.Xna.Content.Pipeline
{
	public class CompiledShaderContent : ContentItem
	{
		public CompiledEffectContent Effect { get; set; }
	}
}