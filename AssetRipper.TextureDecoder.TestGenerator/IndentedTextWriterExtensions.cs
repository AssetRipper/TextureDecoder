using System.CodeDom.Compiler;

internal static class IndentedTextWriterExtensions
{
	internal static void WriteSourceGenerationDisclaimer(this IndentedTextWriter textWriter)
	{
		textWriter.WriteLine($"//This code is source generated. Do not edit manually.");
		textWriter.WriteLine();
	}
}