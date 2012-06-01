using System.IO;
using NUnit.Framework;
using Weaver.SurfaceShaders.Parser;

namespace Weaver.Tests
{
	[TestFixture]
	public class WeaverTests
	{
		[TestCase(LightType.Directional, 2481)]
		[TestCase(LightType.Point, 2427)]
		public void CanWeaveEffect(LightType lightType, int fxLength)
		{
			// Arrange.
			const string path = "Resources/Shaders/Flat-Diffuse.shader";
			var tokens = new Lexer(path, File.ReadAllText(path)).GetTokens();
			var parser = new ShaderParser(path, tokens);
			var shaderNode = parser.Parse();

			// Act.
			string fx = new Direct3D9Weaver().Weave(shaderNode, lightType);

			// Assert.
			Assert.That(fx, Is.Not.Null);
			Assert.That(fx.Length, Is.EqualTo(fxLength));
		}
	}
}