shader "Specular"
{
	properties
	{
		float3 DiffuseColor = (1, 1, 1)
		float Alpha = 1
		float3 SpecularColor = (0.5, 0.5, 0.5)
		float SpecularPower = 16
		Texture2D DiffuseTexture = "white"
	}

	surface
	{
		lightingmodel "BlinnPhong"

		inputs { float2 uv }

		__hlsl__
		void surface(SurfaceInput input, inout BlinnPhongSurfaceOutput output)
		{
			output.Diffuse = tex2D(DiffuseTexture, input.uv) * DiffuseColor;
			output.Alpha = Alpha;
			output.Specular = SpecularColor;
			output.SpecularPower = SpecularPower;
		}
		__hlsl__
	}
}