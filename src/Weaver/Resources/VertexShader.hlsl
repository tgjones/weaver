matrix wvp : WORLDVIEWPROJECTION;
matrix world : WORLD;

VertexShaderOutput VertexShaderCommon(VertexShaderInput input)
{
	VertexShaderOutput output;
	output.position = mul(float4(input.position, 1), wvp);
	return output;
}

VertexShaderForwardBaseOutput VertexShaderForwardBase(VertexShaderInput input)
{
	VertexShaderForwardBaseOutput output;
	output.common = VertexShaderCommon(input);
	output.worldPosition = mul(float4(input.position, 1), world).xyz;
	output.worldNormal = mul(float4(input.normal, 0), world);
	output.uv = input.uv;

	return output;
}

VertexShaderShadowCasterOutput VertexShaderShadowCaster(VertexShaderInput input)
{
	VertexShaderShadowCasterOutput output;
	output.common = VertexShaderCommon(input);
	output.depth = output.common.position.zw;
	return output;
}