struct VertexShaderOutput
{
	float4 position : POSITION;
};

struct VertexShaderForwardBaseOutput
{
	VertexShaderOutput common;
	float3 worldPosition : TEXCOORD0;
	float3 worldNormal : TEXCOORD1;
	float2 uv : TEXCOORD2;
};

struct VertexShaderShadowCasterOutput
{
	VertexShaderOutput common;
	float2 depth : TEXCOORD0;
};