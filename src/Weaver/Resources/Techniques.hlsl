technique ShadowCaster
{
	pass
	{
		VertexShader = compile vs_3_0 VertexShaderShadowCaster();
		PixelShader = compile ps_3_0 PixelShaderShadowCaster();
	}
}

technique ForwardBase
{
	pass
	{
		VertexShader = compile vs_3_0 VertexShaderForwardBase();
		PixelShader = compile ps_3_0 PixelShaderForwardBase();
	}
}