using System;
using System.IO;
using NUnit.Framework;
using Weaver.Content.Pipeline.Parser;

namespace Weaver.Content.Pipeline.Tests.Parser
{
	[TestFixture]
	public class LexerTests
	{
		[TestCase("Resources/Shaders/Flat-Diffuse.shader", 90)]
		[TestCase("Resources/Shaders/Normal-Diffuse.shader", 43)]
		public void CanLexShader(string path, int tokenCount)
		{
			// Arrange.
			var lexer = new Lexer(path, File.ReadAllText(path));
            lexer.Error += (sender, args) =>
            {
                throw new Exception(args.Message);
            };

			// Act.
			var tokens = lexer.GetTokens();

			// Assert.
			Assert.That(tokens, Is.Not.Null);
			Assert.That(tokens, Has.Length.EqualTo(tokenCount));
		}
	}
}