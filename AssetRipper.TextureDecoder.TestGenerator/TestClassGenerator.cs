using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.TestGenerator;
using System.CodeDom.Compiler;

internal static class TestClassGenerator
{
	private const string OutputNamespace = "AssetRipper.TextureDecoder.Tests.Formats";
	private const string OutputFolder = "../../../../AssetRipper.TextureDecoder.Tests/Formats/";

	private static readonly char[] channels = new char[] { 'R', 'G', 'B', 'A' };

	private static readonly Dictionary<Type, ColorRandomValues> randomValueDictionary = new()
		{
			{ typeof(sbyte), new ColorRandomValues("-0b01010101", "0b01110010", "-0b00001111", "-0b01000111", "0b01001110") },
			{ typeof(byte), new ColorRandomValues("0b11010101", "0b01110010", "0b10001111", "0b11000111", "0b01001110") },
			{ typeof(short), new ColorRandomValues("-24000", "21354", "-80", "347", "31871") },
			{ typeof(ushort), new ColorRandomValues("44000", "21354", "60080", "347", "33871") },
			{ typeof(Half), new ColorRandomValues("(Half)0.447f", "(Half)0.224f", "(Half)0.95f", "(Half)0.897f", "(Half)0.333f") },
			{ typeof(float), new ColorRandomValues("0.447f", "0.224f", "0.95f", "0.897f", "0.333f") },
			{ typeof(double), new ColorRandomValues("0.447", "0.224", "0.95", "0.897", "0.333") },
		};

	internal static void GenerateTestClasses()
	{
		Directory.CreateDirectory(OutputFolder);

		foreach (GenerationData data in Program.dataList)
		{
			using StringWriter streamWriter = new();
			using IndentedTextWriter textWriter = new IndentedTextWriter(streamWriter, "\t");
			streamWriter.NewLine = "\n";
			textWriter.NewLine = "\n";
			textWriter.WriteSourceGenerationDisclaimer();
			textWriter.WriteNamespaceUsings(data);
			textWriter.WriteFileScopedNamespace();
			textWriter.WriteClassDefinitionOpen(data);
			textWriter.Indent++;
			textWriter.WriteCorrectSizeTest(data);
			textWriter.WriteLine();
			textWriter.WritePropertySymmetryTests(data);
			textWriter.WriteIndependenceTests(data);
			textWriter.WriteGetMethodMatchesProperties();
			textWriter.WriteLine();
			textWriter.WriteMethodSymmetryTest();
			textWriter.WriteLine();
			textWriter.WriteMakeRandomColor(data);

			foreach (GenerationData otherData in Program.dataList)
			{
				if (otherData.Contains(data))
				{
					textWriter.WriteLine();
					textWriter.WriteLosslessConversionTest(data, otherData);
				}
			}

			textWriter.Indent--;
			textWriter.WriteClassDefinitionClose();

			textWriter.Flush();
			streamWriter.Flush();

			string outputFilePath = $"{OutputFolder}{data.ColorType.Name}Tests.g.cs";
			File.WriteAllText(outputFilePath, streamWriter.ToString());
			Console.WriteLine(data.ColorType.Name);
		}
	}

	private static void WriteClassDefinitionClose(this IndentedTextWriter textWriter)
	{
		textWriter.WriteLine("}");
	}

	private static void WriteClassDefinitionOpen(this IndentedTextWriter textWriter, GenerationData data)
	{
		textWriter.WriteLine($"public partial class {data.ColorType.Name}Tests");
		textWriter.WriteLine("{");
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

	private static void WriteFileScopedNamespace(this IndentedTextWriter textWriter)
	{
		textWriter.WriteLine($"namespace {OutputNamespace};");
		textWriter.WriteLine();//An empty line after the namespace declaration
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

	private static void WriteIndependenceTest(this IndentedTextWriter textWriter, GenerationData data, char channel)
	{
		textWriter.WriteLine("[Test]");
		textWriter.WriteLine($"public void ChannelsAreIndependent_{channel}()");
		textWriter.WriteLine("{");
		textWriter.Indent += 1;

		textWriter.WriteLine("var color = MakeRandomColor();");
		foreach (char otherChannel in channels.Where(c => c != channel))
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

	private static void WriteIndependenceTests(this IndentedTextWriter textWriter, GenerationData data)
	{
		foreach (char channel in channels)
		{
			textWriter.WriteIndependenceTest(data, channel);
			textWriter.WriteLine();
		}
	}

	private static void WriteLosslessConversionTest(this IndentedTextWriter textWriter, GenerationData currentData, GenerationData containingData)
	{
		string currentName = currentData.ColorType.Name;
		string containingName = containingData.ColorType.Name;

		textWriter.WriteLine("[Test]");
		textWriter.WriteLine($"public void ConversionTo{containingName}IsLossless()");
		textWriter.WriteLine("{");
		textWriter.Indent += 1;

		textWriter.WriteLine($"{currentName} original = MakeRandomColor();");
		textWriter.WriteLine($"{containingName} converted = original.{nameof(ColorExtensions.Convert)}<{currentName}, {currentData.ChannelTypeName}, {containingName}, {containingData.ChannelTypeName}>();");
		textWriter.WriteLine($"{currentName} convertedBack = converted.{nameof(ColorExtensions.Convert)}<{containingName}, {containingData.ChannelTypeName}, {currentName}, {currentData.ChannelTypeName}>();");
		textWriter.WriteLine("Assert.That(convertedBack, Is.EqualTo(original));");

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

	private static void WriteNamespaceUsings(this IndentedTextWriter textWriter, GenerationData data)
	{
		textWriter.WriteLine($"using {typeof(ColorExtensions).Namespace};");
		textWriter.WriteLine($"using {data.ColorType.Namespace};");
		textWriter.WriteLine($"using {typeof(System.Runtime.CompilerServices.Unsafe).Namespace};");
		textWriter.WriteLine();//An empty line after the usings
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

	private static void WritePropertySymmetryTests(this IndentedTextWriter textWriter, GenerationData data)
	{
		foreach (char channel in channels)
		{
			textWriter.WritePropertySymmetryTest(data, channel);
			textWriter.WriteLine();
		}
	}
}