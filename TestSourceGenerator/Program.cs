using AssetRipper.TextureDecoder.Rgb.Formats;
using System.CodeDom.Compiler;

namespace TestSourceGenerator
{
	internal static class Program
	{
		private const string OutputNamespace = "AssetRipper.TextureDecoder.Tests.Formats";
		private const string OutputFolder = "../../../../AssetRipper.TextureDecoder.Tests/Formats/";

		private static readonly char[] channels = new char[] { 'R', 'G', 'B', 'A' };

		private static readonly List<GenerationData> dataList = new()
		{
			new GenerationData(typeof(ColorA8), typeof(byte), 1),
			new GenerationData(typeof(ColorARGB16), typeof(byte), 2),
			new GenerationData(typeof(ColorARGB32), typeof(byte), 4),
			new GenerationData(typeof(ColorR16), typeof(ushort), 2),
			new GenerationData(typeof(ColorR8), typeof(byte), 1),
			new GenerationData(typeof(ColorRG16), typeof(byte), 2),
			new GenerationData(typeof(ColorRG32), typeof(ushort), 4),
			new GenerationData(typeof(ColorRGB16), typeof(byte), 2),
			new GenerationData(typeof(ColorRGB24), typeof(byte), 3),
			new GenerationData(typeof(ColorRGB48), typeof(ushort), 6),
			new GenerationData(typeof(ColorRGB9e5), typeof(float), 4),
			new GenerationData(typeof(ColorRGBA16), typeof(byte), 2),
			new GenerationData(typeof(ColorRGBA32), typeof(byte), 4),
			new GenerationData(typeof(ColorRGBA64), typeof(ushort), 8),
			new GenerationData(typeof(ColorRGBAHalf), typeof(Half), 8),
			new GenerationData(typeof(ColorRGBASingle), typeof(float), 16),
			new GenerationData(typeof(ColorRGHalf), typeof(Half), 4),
			new GenerationData(typeof(ColorRGSingle), typeof(float), 8),
			new GenerationData(typeof(ColorRHalf), typeof(Half), 2),
			new GenerationData(typeof(ColorRSingle), typeof(float), 4),
		};

		private static readonly Dictionary<Type, ColorRandomValues> randomValueDictionary = new()
		{
			{ typeof(byte), new ColorRandomValues("0b11010101", "0b01110010", "0b10001111", "0b11000111", "0b01001110") },
			{ typeof(ushort), new ColorRandomValues("44000", "21354", "60080", "347", "33871") },
			{ typeof(Half), new ColorRandomValues("(Half)0.447f", "(Half)0.224f", "(Half)0.95f", "(Half)0.897f", "(Half)0.333f") },
			{ typeof(float), new ColorRandomValues("0.447f", "0.224f", "0.95f", "0.897f", "0.333f") }
		};

		static void Main(string[] args)
		{
			Directory.CreateDirectory(OutputFolder);

			foreach (GenerationData data in dataList)
			{
				using MemoryStream memoryStream = new();
				using StreamWriter streamWriter = new StreamWriter(memoryStream);
				using IndentedTextWriter textWriter = new IndentedTextWriter(streamWriter, "\t");
				textWriter.NewLine = "\r\n";

				textWriter.WriteSourceGenerationDisclaimer();
				textWriter.WriteNamespaceUsings(data);
				textWriter.WriteFileScopedNamespace();
				textWriter.WriteClassDefinitionOpen(data);
				textWriter.Indent += 1;

				textWriter.WriteCorrectSizeTest(data);
				textWriter.WriteLine();
				textWriter.WritePropertySymmetryTests(data);
				textWriter.WriteIndependenceTests(data);
				textWriter.WriteGetMethodMatchesProperties();
				textWriter.WriteLine();
				textWriter.WriteMethodSymmetryTest();
				textWriter.WriteLine();
				textWriter.WriteMakeRandomColor(data);

				textWriter.Indent -= 1;
				textWriter.WriteClassDefinitionClose();

				textWriter.Flush();
				streamWriter.Flush();

				string outputFilePath = $"{OutputFolder}{data.ColorType.Name}Tests.g.cs";
				File.WriteAllBytes(outputFilePath, memoryStream.ToArray());
				Console.WriteLine(outputFilePath);
			}

			Console.WriteLine("Done!");
		}

		private static void WriteNamespaceUsings(this IndentedTextWriter textWriter, GenerationData data)
		{
			textWriter.WriteLine($"using {data.ColorType.Namespace};");
			textWriter.WriteLine($"using {typeof(System.Runtime.CompilerServices.Unsafe).Namespace};");
			textWriter.WriteLine();//An empty line after the usings
		}

		private static void WriteFileScopedNamespace(this IndentedTextWriter textWriter)
		{
			textWriter.WriteLine($"namespace {OutputNamespace};");
			textWriter.WriteLine();//An empty line after the namespace declaration
		}

		private static void WriteSourceGenerationDisclaimer(this IndentedTextWriter textWriter)
		{
			textWriter.WriteLine($"//This code is source generated. Do not edit manually.");
			textWriter.WriteLine();
		}

		private static void WriteClassDefinitionOpen(this IndentedTextWriter textWriter, GenerationData data)
		{
			textWriter.WriteLine($"public partial class {data.ColorType.Name}Tests");
			textWriter.WriteLine("{");
		}

		private static void WriteClassDefinitionClose(this IndentedTextWriter textWriter)
		{
			textWriter.WriteLine("}");
		}

		private static void WriteCorrectSizeTest(this IndentedTextWriter textWriter, GenerationData data)
		{
			textWriter.WriteLine("[Test]");
			textWriter.WriteLine("public void CorrectSizeTest()");
			textWriter.WriteLine("{");
			textWriter.Indent += 1;
			textWriter.WriteLine($"Assert.That(Unsafe.SizeOf<{data.ColorType.Name}>(), Is.EqualTo({data.ColorSize}));");
			textWriter.Indent -= 1;
			textWriter.WriteLine("}");
		}

		private static void WriteMakeRandomColor(this IndentedTextWriter textWriter, GenerationData data)
		{
			textWriter.WriteLine($"public static {data.ColorType.Name} MakeRandomColor()");
			textWriter.WriteLine("{");
			textWriter.Indent += 1;

			textWriter.WriteLine("return new()");
			textWriter.WriteLine("{");
			textWriter.Indent += 1;

			ColorRandomValues randomValues = randomValueDictionary[data.ChannelType];
			textWriter.WriteLine($"R = {randomValues.DefaultR},");
			textWriter.WriteLine($"G = {randomValues.DefaultG},");
			textWriter.WriteLine($"B = {randomValues.DefaultB},");
			textWriter.WriteLine($"A = {randomValues.DefaultA},");

			textWriter.Indent -= 1;
			textWriter.WriteLine("};");

			textWriter.Indent -= 1;
			textWriter.WriteLine("}");
		}

		private static void WritePropertySymmetryTests(this IndentedTextWriter textWriter, GenerationData data)
		{
			foreach(char channel in channels)
			{
				textWriter.WritePropertySymmetryTest(data, channel);
				textWriter.WriteLine();
			}
		}

		private static void WritePropertySymmetryTest(this IndentedTextWriter textWriter, GenerationData data, char channel)
		{
			textWriter.WriteLine("[Test]");
			textWriter.WriteLine($"public void PropertyIsSymmetric_{channel}()");
			textWriter.WriteLine("{");
			textWriter.Indent += 1;
			textWriter.WriteLine("var color = MakeRandomColor();");
			char variableName = char.ToLowerInvariant(channel);
			textWriter.WriteLine($"var {variableName} = color.{channel};");
			textWriter.WriteLine($"color.{channel} = {variableName};");
			textWriter.WriteLine($"Assert.That(color.{channel}, Is.EqualTo({variableName}));");
			textWriter.Indent -= 1;
			textWriter.WriteLine("}");
		}

		private static void WriteIndependenceTests(this IndentedTextWriter textWriter, GenerationData data)
		{
			foreach (char channel in channels)
			{
				textWriter.WriteIndependenceTest(data, channel);
				textWriter.WriteLine();
			}
		}

		private static void WriteIndependenceTest(this IndentedTextWriter textWriter, GenerationData data, char channel)
		{
			textWriter.WriteLine("[Test]");
			textWriter.WriteLine($"public void ChannelsAreIndependent_{channel}()");
			textWriter.WriteLine("{");
			textWriter.Indent += 1;

			textWriter.WriteLine("var color = MakeRandomColor();");
			foreach(char otherChannel in channels.Where(c => c != channel))
			{
				textWriter.WriteLine($"var {char.ToLowerInvariant(otherChannel)} = color.{otherChannel};");
			}
			textWriter.WriteLine($"color.{channel} = {randomValueDictionary[data.ChannelType].SetValue};");

			textWriter.WriteLine("Assert.Multiple(() =>");
			textWriter.WriteLine("{");
			textWriter.Indent += 1;

			foreach (char otherChannel in channels.Where(c => c != channel))
			{
				textWriter.WriteLine($"Assert.That(color.{otherChannel}, Is.EqualTo({char.ToLowerInvariant(otherChannel)}));");
			}

			textWriter.Indent -= 1;
			textWriter.WriteLine("});");

			textWriter.Indent -= 1;
			textWriter.WriteLine("}");
		}

		private static void WriteGetMethodMatchesProperties(this IndentedTextWriter textWriter)
		{
			textWriter.WriteLine("[Test]");
			textWriter.WriteLine($"public void GetMethodMatchesProperties()");
			textWriter.WriteLine("{");
			textWriter.Indent += 1;

			textWriter.WriteLine("var color = MakeRandomColor();");
			textWriter.WriteLine("color.GetChannels(out var r, out var g, out var b, out var a);");
			textWriter.WriteLine("Assert.Multiple(() =>");
			textWriter.WriteLine("{");
			textWriter.Indent += 1;

			foreach (char channel in channels)
			{
				textWriter.WriteLine($"Assert.That(color.{channel}, Is.EqualTo({char.ToLowerInvariant(channel)}));");
			}

			textWriter.Indent -= 1;
			textWriter.WriteLine("});");

			textWriter.Indent -= 1;
			textWriter.WriteLine("}");
		}

		private static void WriteMethodSymmetryTest(this IndentedTextWriter textWriter)
		{
			textWriter.WriteLine("[Test]");
			textWriter.WriteLine($"public void MethodsAreSymmetric()");
			textWriter.WriteLine("{");
			textWriter.Indent += 1;

			textWriter.WriteLine("var color = MakeRandomColor();");
			textWriter.WriteLine("color.GetChannels(out var r, out var g, out var b, out var a);");
			textWriter.WriteLine("color.SetChannels(r, g, b, a);");
			textWriter.WriteLine("Assert.Multiple(() =>");
			textWriter.WriteLine("{");
			textWriter.Indent += 1;

			foreach (char channel in channels)
			{
				textWriter.WriteLine($"Assert.That(color.{channel}, Is.EqualTo({char.ToLowerInvariant(channel)}));");
			}

			textWriter.Indent -= 1;
			textWriter.WriteLine("});");

			textWriter.Indent -= 1;
			textWriter.WriteLine("}");
		}
	}
}