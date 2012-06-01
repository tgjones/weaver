shader "Flat"
{
	properties
	{
		float3 DiffuseColor = (0.5, 0.5, 0.5);
		float Alpha = 1;
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