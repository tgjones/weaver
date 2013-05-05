using System;
using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using Weaver.Content.Pipeline.CodeModel;
using Weaver.Content.Pipeline.Parser;
using ErrorEventArgs = Weaver.Content.Pipeline.Parser.ErrorEventArgs;

namespace Weaver.Content.Pipeline
{
	[ContentImporter(".shader", DisplayName = "Shader - Weaver", DefaultProcessor = "ShaderProcessor")]
	public class ShaderImporter : ContentImporter<ShaderContent>
	{
		private string ImporterName
		{
			get { return "Shader Importer"; }
		}
		public override ShaderContent Import(string filename, ContentImporterContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (string.IsNullOrEmpty(filename))
				throw new ArgumentException("Filename cannot be null or empty.", "filename");
			FileInfo info = new FileInfo(filename);
			if (!info.Exists)
				throw new FileNotFoundException("File not found", filename);

			ContentIdentity identity = new ContentIdentity(info.FullName, ImporterName);

			string text = File.ReadAllText(filename);

			Lexer lexer = new Lexer(filename, text);
			lexer.Error += (sender, e) => ThrowParserException(e, info);
			Token[] tokens = lexer.GetTokens();

			ShaderParser parser = new ShaderParser(filename, tokens);
			parser.Error += (sender, e) => ThrowParserException(e, info);

			ShaderNode shaderNode = parser.Parse();
			ShaderContent content = new ShaderContent
			{
				ShaderNode = shaderNode
			};
			content.Identity = identity;
			return content;
		}

		private void ThrowParserException(ErrorEventArgs e, FileInfo info)
		{
			string identifier = string.Format("{0},{1}", e.Position.Line + 1, e.Position.Column + 1);
			ContentIdentity contentIdentity = new ContentIdentity(info.FullName, ImporterName, identifier);
			throw new InvalidContentException(e.Message, contentIdentity);
		}
	}
}