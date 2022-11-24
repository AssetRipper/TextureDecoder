using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using System.CodeDom.Compiler;

namespace AssetRipper.TextureDecoder.TestGenerator
{
	internal static class Program
	{
		internal static readonly List<GenerationData> dataList = new()
		{
			new GenerationData(typeof(ColorA8), 1),
			new GenerationData(typeof(ColorARGB16), 2),
			new GenerationData(typeof(ColorARGB32), 4),
			new GenerationData(typeof(ColorBGRA32), 4),
			new GenerationData(typeof(ColorR16), 2),
			new GenerationData(typeof(ColorR16Half), 2),
			new GenerationData(typeof(ColorR16Signed), 2),
			new GenerationData(typeof(ColorR32Single), 4),
			new GenerationData(typeof(ColorR8), 1),
			new GenerationData(typeof(ColorR8Signed), 1),
			new GenerationData(typeof(ColorRG16), 2),
			new GenerationData(typeof(ColorRG32Half), 4),
			new GenerationData(typeof(ColorRG16Signed), 2),
			new GenerationData(typeof(ColorRG32), 4),
			new GenerationData(typeof(ColorRG32Signed), 4),
			new GenerationData(typeof(ColorRG64Single), 8),
			new GenerationData(typeof(ColorRGB16), 2),
			new GenerationData(typeof(ColorRGB24), 3),
			new GenerationData(typeof(ColorRGB24Signed), 3),
			new GenerationData(typeof(ColorRGB32Half), 4),
			new GenerationData(typeof(ColorRGB48), 6),
			new GenerationData(typeof(ColorRGB48Half), 6),
			new GenerationData(typeof(ColorRGB48Signed), 6),
			new GenerationData(typeof(ColorRGB96Single), 12),
			new GenerationData(typeof(ColorRGB9e5), 4),
			new GenerationData(typeof(ColorRGBA128Single), 16),
			new GenerationData(typeof(ColorRGBA16), 2),
			new GenerationData(typeof(ColorRGBA32), 4),
			new GenerationData(typeof(ColorRGBA32Signed), 4),
			new GenerationData(typeof(ColorRGBA64), 8),
			new GenerationData(typeof(ColorRGBA64Half), 8),
			new GenerationData(typeof(ColorRGBA64Signed), 8),
		};

		static void Main()
		{
			TestClassGenerator.GenerateTestClasses();
			GenerateRgbConsoleMethod();
			Console.WriteLine("Done!");
		}

		private static void GenerateRgbConsoleMethod()
		{
			StringWriter streamWriter = new();
			IndentedTextWriter textWriter = new IndentedTextWriter(streamWriter, "\t");
			streamWriter.NewLine = "\n";
			textWriter.NewLine = "\n";

			textWriter.WriteSourceGenerationDisclaimer();
			textWriter.WriteLine($"using {typeof(RgbConverter).Namespace};");
			textWriter.WriteLine($"using {typeof(ColorBGRA32).Namespace};");
			textWriter.WriteLine();

			textWriter.WriteLine("namespace AssetRipper.TextureDecoder.ConsoleApp;");
			textWriter.WriteLine("static partial class Program");
			textWriter.WriteLine("{");
			textWriter.Indent++;

			textWriter.WriteLine("private static void DecodeRgb(ReadOnlySpan<byte> input, int width, int height, string modeString, Span<byte> output)");
			textWriter.WriteLine("{");
			textWriter.Indent++;

			textWriter.WriteLine("Console.WriteLine(\"Arg at index 5 : mode\");");
			for (int i = 0; i < dataList.Count; i++)
			{
				textWriter.WriteLine($"Console.WriteLine(\"  {i} - {dataList[i].ColorType.Name.Substring(5)}\");");
			}
			textWriter.WriteLine("int mode = int.Parse(modeString);");
			textWriter.WriteLine("switch(mode)");
			textWriter.WriteLine("{");
			textWriter.Indent++;

			for (int i = 0; i < dataList.Count; i++)
			{
				textWriter.WriteLine($"case {i}:");
				textWriter.Indent++;
				GenerationData data = dataList[i];
				textWriter.WriteLine($"RgbConverter.Convert<{data.ColorType.Name}, {data.ChannelTypeName}, ColorBGRA32, byte>(input, width, height, output);");
				textWriter.WriteLine("break;");
				textWriter.Indent--;
			}
			{
				textWriter.WriteLine("default:");
				textWriter.Indent++;
				textWriter.WriteLine("throw new NotSupportedException(mode.ToString());");
				textWriter.Indent--;
			}

			textWriter.Indent--;
			textWriter.WriteLine("}");

			textWriter.Indent--;
			textWriter.WriteLine("}");

			textWriter.Indent--;
			textWriter.WriteLine("}");

			textWriter.Flush();
			streamWriter.Flush();
			File.WriteAllText("../../../../AssetRipper.TextureDecoder.ConsoleApp/Program.g.cs", streamWriter.ToString());
			Console.WriteLine("Program");
		}
	}
}