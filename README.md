Weaver
======

Weaver is an HLSL surface shader parser, implemented as a content pipeline extension for XNA / MonoGame. 

To make Weaver useful, you will need to integrate Weaver into your game engine. Weaver, by itself, only
handles parsing a specific surface shader language into a node hierarchy.

This is an example of the surface shader language that Weaver parses:

```csharp
shader "Specular"
{
	properties
	{
		float3 DiffuseColor = (1, 1, 1)
		float Alpha = 1
		float3 SpecularColor = (0.5, 0.5, 0.5)
		float SpecularPower = 16
		Texture2D DiffuseTexture = "white"
	}

	surface
	{
		lightingmodel "BlinnPhong"

		inputs { float2 uv }

		__hlsl__
		void surface(SurfaceInput input, inout BlinnPhongSurfaceOutput output)
		{
			output.Diffuse = tex2D(DiffuseTexture, input.uv) * DiffuseColor;
			output.Alpha = Alpha;
			output.Specular = SpecularColor;
			output.SpecularPower = SpecularPower;
		}
		__hlsl__
	}
}
```

Usage
-----

* Install the [Weaver](http://nuget.org/packages/Weaver/) NuGet package in your XNA / MonoGame content project.

* Use the `Shader - Weaver` content importer for your surface shader content files.

* Define a new `ShaderProcessor` class that inherits from Weaver's `ShaderProcessor`:
      ```csharp
      [ContentProcessor(DisplayName = "Shader - My Game")]
      public class ShaderProcessor : Weaver.Xna.Content.Pipeline.ShaderProcessor
      {
          protected override string BuildFxFile(ShaderContent input)
          {
              // Implement this in whatever way is appropriate for your project.
              return new Shaders.Weaver().Weave(input.ShaderNode);
          }
      }
      ```

* Use your new content processor for your surface shader content files.

License
-------

Weaver is released under the [MIT License](http://www.opensource.org/licenses/MIT).
