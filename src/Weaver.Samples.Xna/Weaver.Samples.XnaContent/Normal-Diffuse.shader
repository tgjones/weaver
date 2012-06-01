shader "Diffuse"
{
	properties
	{
		float3 DiffuseColor = (0.5, 0.5, 0.5);
		float Alpha = 1;
		Texture2D DiffuseTexture = "white";
	}

	surface
	{
		LightingModel = "Lambert";

		__hlsl__
		void surface(SurfaceInput input, inout LambertSurfaceOutput output) 
		{
			output.Diffuse = SAMPLE_TEXTURE(DiffuseTexture).rgb * DiffuseColor;
			output.Alpha = Alpha;
		}
		__hlsl__
	}
}