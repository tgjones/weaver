using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using NUnit.Framework;
using Weaver.Content.Pipeline.Parser;

namespace Weaver.Content.Pipeline.Tests.Parser
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
		    parser.Error += (sender, args) =>
		    {
		        throw new Exception(args.Message + Environment.NewLine + args.Position);
		    };

			// Act.
			var shaderNode = parser.Parse();

			// Assert.
			Assert.That(shaderNode, Is.Not.Null);
			Assert.That(shaderNode.Name, Is.EqualTo("Flat"));
			Assert.That(shaderNode.Properties, Has.Count.EqualTo(2));
			Assert.That(shaderNode.Technique, Is.Not.Null);
            Assert.That(shaderNode.Technique.State, Is.Not.Null);
            Assert.That(shaderNode.Technique.State.CullMode, Is.EqualTo(CullMode.CullClockwiseFace));
            Assert.That(shaderNode.Technique.State.FillMode, Is.Null);
            Assert.That(shaderNode.Technique.State.DepthBias, Is.EqualTo(0.001f));
            Assert.That(shaderNode.Technique.State.SlopeScaleDepthBias, Is.EqualTo(0.001f));
            Assert.That(shaderNode.Technique.State.DepthBufferFunction, Is.EqualTo(CompareFunction.LessEqual));
            Assert.That(shaderNode.Technique.State.DepthBufferWriteEnable, Is.True);
            Assert.That(shaderNode.Technique.State.DepthBufferEnable, Is.False);
            Assert.That(shaderNode.Technique.State.AlphaBlendFunction, Is.EqualTo(BlendFunction.Add));
            Assert.That(shaderNode.Technique.State.AlphaDestinationBlend, Is.EqualTo(Blend.One));
            Assert.That(shaderNode.Technique.State.AlphaSourceBlend, Is.EqualTo(Blend.Zero));
            Assert.That(shaderNode.Technique.State.ColorBlendFunction, Is.EqualTo(BlendFunction.Min));
            Assert.That(shaderNode.Technique.State.ColorDestinationBlend, Is.EqualTo(Blend.SourceAlpha));
            Assert.That(shaderNode.Technique.State.ColorSourceBlend, Is.EqualTo(Blend.SourceColor));
            Assert.That(shaderNode.Technique.Surface, Is.Not.Null);
			Assert.That(shaderNode.Technique.Surface.LightingModel, Is.EqualTo("FlatShading"));
		}
	}
}