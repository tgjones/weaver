using System;
using Microsoft.Xna.Framework.Graphics;

namespace Weaver.Xna
{
	public class WeavedEffect : Effect
	{
		public WeavedEffect(GraphicsDevice graphicsDevice, byte[] effectCode)
			: base(graphicsDevice, effectCode)
		{
		}

		public void SelectTechnique(WeavedEffectTechniqueType techniqueType)
		{
			switch (techniqueType)
			{
				case WeavedEffectTechniqueType.Forward:
					CurrentTechnique = Techniques["Forward"];
					break;
				default:
					throw new ArgumentOutOfRangeException("techniqueType");
			}
		}
	}
}
