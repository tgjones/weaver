float3 CameraPosition : CAMERA_POSITION;
float4 AmbientColor : AMBIENT_COLOR = float4(0.1, 0.1, 0.1, 1);

PixelShaderOutput PS(VertexShaderOutput input)
{
	PixelShaderOutput output;

	float3 viewDirection = normalize(input.worldPosition - CameraPosition);

	SurfaceInput surfaceInput = (SurfaceInput) 0;
	surfaceInput.worldReflect = reflect(viewDirection, input.worldNormal);
	// TODO: Add other surface inputs here

	SURFACE_OUTPUT_NAME surfaceOutput;
	surfaceOutput.Normal = input.worldNormal;
	surface(surfaceInput, surfaceOutput);

	// Because light is additive, the final intensity of the light
	// reflected by a given surface is simply the sum of the ambient,
	// diffuse and specular components.
	float4 finalColor = AmbientColor * float4(surfaceOutput.Diffuse, 1);
	Light light = GetLight(input.worldPosition);
	finalColor += LIGHTING_MODEL_FUNC(surfaceOutput, light, viewDirection);

	output.color = finalColor;

	return output;
}