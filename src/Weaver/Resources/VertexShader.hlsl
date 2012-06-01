matrix wvp : WORLDVIEWPROJECTION;
matrix world : WORLD;

VertexShaderOutput VS(VertexShaderInput input)
{
	VertexShaderOutput output;

	output.position = mul(float4(input.position, 1), wvp);
	output.worldPosition = mul(float4(input.position, 1), world).xyz;
	output.worldNormal = mul(float4(input.normal, 0), world);
	output.uv = input.uv;

	return output;
}