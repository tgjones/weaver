struct FlatShadingSurfaceOutput
{
	float3 Diffuse;
	float3 Normal;
	float Alpha;
};

float4 LightingFlatShading(FlatShadingSurfaceOutput s, Light l, float3 viewDirection)
{
	float4 c;
	c.rgb = s.Diffuse;
	c.a = s.Alpha;
	return c;
}