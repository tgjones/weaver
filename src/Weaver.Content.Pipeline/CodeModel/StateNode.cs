using Microsoft.Xna.Framework.Graphics;

namespace Weaver.Content.Pipeline.CodeModel
{
    public class StateNode : ParseNode
    {
        public CullMode? CullMode { get; set; }
        public FillMode? FillMode { get; set; }
        public float? DepthBias { get; set; }
        public float? SlopeScaleDepthBias { get; set; }

        public CompareFunction? DepthBufferFunction { get; set; }
        public bool? DepthBufferWriteEnable { get; set; }
        public bool? DepthBufferEnable { get; set; }

        public BlendFunction? AlphaBlendFunction { get; set; }
        public Blend? AlphaDestinationBlend { get; set; }
        public Blend? AlphaSourceBlend { get; set; }
        public BlendFunction? ColorBlendFunction { get; set; }
        public Blend? ColorDestinationBlend { get; set; }
        public Blend? ColorSourceBlend { get; set; }
    }
}