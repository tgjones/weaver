float3 PointLightPosition : POINT_LIGHT_POSITION;
float4 PointLightColor : POINT_LIGHT_COLOR;

Light GetLight(float3 worldPosition)
{
	Light light;
	light.DirectionToLight = PointLightPosition - worldPosition;
	light.Color = PointLightColor;
	return light;
}