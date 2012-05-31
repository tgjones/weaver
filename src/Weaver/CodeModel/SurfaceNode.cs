using System.Collections.Generic;

namespace Weaver.CodeModel
{
	public class SurfaceNode : ParseNode
	{
		public string LightingModel { get; set; }
		public List<SurfaceInputNode> Inputs { get; set; }
		public string Code { get; set; }
	}
}