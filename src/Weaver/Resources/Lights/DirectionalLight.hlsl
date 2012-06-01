float3 DirectionalLightDirection : DIRECTIONAL_LIGHT_DIRECTION = float3(1, -1, 0);
float3 DirectionalLightColor : DIRECTIONAL_LIGHT_COLOR;

Light GetLight(float3 worldPosition)
{
	Light light;
	light.DirectionToLight = -DirectionalLightDirection;
	light.Color = float4(DirectionalLightColor, 1);
	return light;
}