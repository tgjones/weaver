shader "Terrain"
{
	properties
	{
		Texture2D SplatMap = "white"

		Texture2D Texture1 = "white"
		float2 Texture1TileSize = (1, 1)

		Texture2D Texture2 = "white"
		float2 Texture2TileSize = (1, 1)

		Texture2D Texture3 = "white"
		float2 Texture3TileSize = (1, 1)

		Texture2D Texture4 = "white"
		float2 Texture4TileSize = (1, 1)
	}

	surface
	{
		lightingmodel "Lambert"

		inputs { float2 uv }

		__hlsl__
		void surface(SurfaceInput input, inout LambertSurfaceOutput output)
		{
			float4 splatRatios = tex2D(SplatMap, input.uv);
			float3 colour = tex2D(Texture1, input.uv * Texture1TileSize).rgb * splatRatios.r;
			colour += tex2D(Texture2, input.uv * Texture2TileSize).rgb * splatRatios.g;
			colour += tex2D(Texture3, input.uv * Texture3TileSize).rgb * splatRatios.b;
			colour += tex2D(Texture4, input.uv * Texture4TileSize).rgb * splatRatios.a;
			output.Diffuse = colour;
			output.Alpha = 1;
		}
		__hlsl__
	}
}