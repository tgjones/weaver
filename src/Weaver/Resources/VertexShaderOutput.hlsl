struct VertexShaderOutput
{
	float4 position : POSITION;
	float3 worldPosition : TEXCOORD0;
	float3 worldNormal : TEXCOORD1;
	float2 uv : TEXCOORD2;
};