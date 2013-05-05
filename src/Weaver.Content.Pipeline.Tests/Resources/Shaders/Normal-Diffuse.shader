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
		SetLightingModel(Lambert);

		inputs { float2 uv }

		__hlsl__
		void surface(SurfaceInput input, inout LambertSurfaceOutput output) 
		{
			output.Diffuse = tex2D(DiffuseTexture, input.uvDiffuseTexture).rgb * DiffuseColor;
			output.Alpha = Alpha;
		}
		__hlsl__
	}
}