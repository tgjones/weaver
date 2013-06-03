shader "Flat"
{
	properties
	{
		float3 DiffuseColor = (0.5, 0.5, 0.5);
		float Alpha = 1;
	}

	technique
	{
		state
		{
			CullMode = CullClockwiseFace;
			//FillMode = Wireframe;
			DepthBias = 0.001;
			SlopeScaleDepthBias = 0.001;

			DepthBufferFunction = LessEqual;
			DepthBufferWriteEnable = true;
			DepthBufferEnable = false;

			AlphaBlendFunction = Add;
			AlphaDestinationBlend = One;
			AlphaSourceBlend = Zero;
			ColorBlendFunction = Min;
			ColorDestinationBlend = SourceAlpha;
			ColorSourceBlend = SourceColor;
		}

		surface
		{
			LightingModel = "FlatShading";

			__hlsl__
			void surface(SurfaceInput input, inout FlatShadingSurfaceOutput output)
			{
				output.Diffuse = DiffuseColor;
				output.Alpha = Alpha;
			}
			__hlsl__
		}
	}
}