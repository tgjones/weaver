using System.IO;
using NUnit.Framework;
using Weaver.Parser;

namespace Weaver.Tests.Parser
{
	[TestFixture]
	public class ShaderParserTests
	{
		[Test]
		public void CanParseShader()
		{
			// Arrange.
			const string path = "Resources/Shaders/Flat-Diffuse.shader";
			var tokens = new Lexer(path, File.ReadAllText(path)).GetTokens();
			var parser = new ShaderParser(path, tokens);

			// Act.
			var shaderNode = parser.Parse();

			// Assert.
			Assert.That(shaderNode, Is.Not.Null);
			Assert.That(shaderNode.Name, Is.EqualTo("Flat"));
			Assert.That(shaderNode.Properties, Has.Count.EqualTo(2));
			Assert.That(shaderNode.Surface, Is.Not.Null);
			Assert.That(shaderNode.Surface.LightingModel, Is.EqualTo("FlatShading"));
		}
	}
}