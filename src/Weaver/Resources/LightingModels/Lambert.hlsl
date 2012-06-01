struct LambertSurfaceOutput
{
	float3 Diffuse;
	float3 Normal;
	float Alpha;
};

float4 LightingLambert(LambertSurfaceOutput s, Light l, float3 viewDirection)
{
	float diffuse = max(0, dot(s.Normal, l.DirectionToLight));
	
	float4 c;
	c.rgb = s.Diffuse * l.Color.rgb * diffuse;
	c.a = s.Alpha;
	return c;
}