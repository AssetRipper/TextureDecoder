using AssetRipper.Text.SourceGeneration;
using AssetRipper.TextureDecoder.Rgb.Formats;
using AssetRipper.TextureDecoder.SourceGeneration.Common;
using AssetRipper.TextureDecoder.TestGenerator;
using System.CodeDom.Compiler;

internal static class TestClassGenerator
{
	private const string OutputNamespace = "AssetRipper.TextureDecoder.Tests.Formats";
	private const string OutputFolder = "../../../../AssetRipper.TextureDecoder.Tests/Formats/";
	private const string GenericOutputNamespace = OutputNamespace + ".Generic";
	private const string GenericOutputFolder = OutputFolder + "Generic/";

	private static readonly char[] channels = new char[] { 'R', 'G', 'B', 'A' };

	internal static void GenerateTestClasses()
	{
		Directory.CreateDirectory(OutputFolder);
		MakeGenericColorTests();
		MakeLosslessColorTests();
		foreach (GenerationData data in Program.dataList)
		{
			using StringWriter streamWriter = new();
			using IndentedTextWriter textWriter = new IndentedTextWriter(streamWriter, "\t");
			streamWriter.NewLine = "\n";
			textWriter.NewLine = "\n";
			textWriter.WriteGeneratedCodeWarning();
			textWriter.WriteLineNoTabs();
			textWriter.WriteNamespaceUsings(data.ColorType.Namespace);
			textWriter.WriteFileScopedNamespace();
			textWriter.WriteClassDefinitionOpen(data);
			textWriter.Indent++;
			textWriter.WriteCorrectSizeTest(data);
			textWriter.WriteLineNoTabs();
			if (data.ColorType != typeof(ColorRGB9e5))//Rounding errors cause these to fail
			{
				textWriter.WritePropertySymmetryTests(data);
				textWriter.WriteIndependenceTests(data);
			}
			textWriter.WriteGetMethodMatchesProperties();
			textWriter.WriteLineNoTabs();
			textWriter.WriteMethodSymmetryTest();
			textWriter.WriteLineNoTabs();
			textWriter.WriteMakeRandomColor(data);
			textWriter.WriteLineNoTabs();
			textWriter.WriteMakeRandomValue(data);

			foreach (GenerationData otherData in Program.dataList)
			{
				if (otherData.Contains(data))
				{
					textWriter.WriteLineNoTabs();
					textWriter.WriteLosslessConversionTest(data, otherData);
				}
			}

			foreach (CSharpPrimitives.Data channelData in CSharpPrimitives.List)
			{
				Type channelType = channelData.Type;
				string channelTypeName = channelData.LangName;

				if (CSharpPrimitives.IsFloatingPoint(channelType) != CSharpPrimitives.IsFloatingPoint(data.ChannelType))
				{
					continue;
				}

				if (channelType == typeof(decimal) || data.ChannelType == typeof(decimal))
				{
					continue;
				}

				if (channelData.Size < CSharpPrimitives.Dictionary[data.ChannelType].Size)
				{
					continue;
				}

				textWriter.WriteLineNoTabs();
				textWriter.WriteLosslessConversionTest(data.ColorType.Name, data.ChannelTypeName, $"{nameof(ColorRGBA<byte>)}<{channelTypeName}>", channelTypeName);

				if (!data.AlphaChannel)
				{
					textWriter.WriteLineNoTabs();
					textWriter.WriteLosslessConversionTest(data.ColorType.Name, data.ChannelTypeName, $"{nameof(ColorRGB<byte>)}<{channelTypeName}>", channelTypeName);
					if (!data.BlueChannel)
					{
						textWriter.WriteLineNoTabs();
						textWriter.WriteLosslessConversionTest(data.ColorType.Name, data.ChannelTypeName, $"{nameof(ColorRG<byte>)}<{channelTypeName}>", channelTypeName);
						if (!data.GreenChannel)
						{
							textWriter.WriteLineNoTabs();
							textWriter.WriteLosslessConversionTest(data.ColorType.Name, data.ChannelTypeName, $"{nameof(ColorR<byte>)}<{channelTypeName}>", channelTypeName);
						}
					}
				}
				if (!data.RedChannel && !data.GreenChannel && !data.BlueChannel)
				{
					textWriter.WriteLineNoTabs();
					textWriter.WriteLosslessConversionTest(data.ColorType.Name, data.ChannelTypeName, $"{nameof(ColorA<byte>)}<{channelTypeName}>", channelTypeName);
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

	private static void MakeGenericColorTests()
	{
		using IndentedTextWriter writer = IndentedTextWriterFactory.Create(GenericOutputFolder, "GenericColorTests");
		writer.WriteGeneratedCodeWarning();
		writer.WriteLine();
		writer.WriteUsing(typeof(ColorR<>).Namespace!);
		writer.WriteLine();
		writer.WriteFileScopedNamespace(GenericOutputNamespace);

		foreach (string channeName in CSharpPrimitives.List.Select(d => d.LangName))
		{
			foreach (string colorName in GetGenericColorNames())
			{
				writer.WriteLine($"[TestFixture(TypeArgs = new Type[] {{ typeof({colorName}<{channeName}>), typeof({channeName}) }})]");
			}
		}
		writer.WriteLine("partial class GenericColorTests<TColor, TChannel>");
		using (new CurlyBrackets(writer))
		{
		}
	}

	internal static IEnumerable<string> GetGenericColorNames()
	{
		yield return nameof(ColorR<byte>);
		yield return nameof(ColorRG<byte>);
		yield return nameof(ColorRGB<byte>);
		yield return nameof(ColorRGBA<byte>);
		yield return nameof(ColorA<byte>);
	}

	private static void MakeLosslessColorTests()
	{
		using IndentedTextWriter writer = IndentedTextWriterFactory.Create(GenericOutputFolder, "LosslessColorTests");
		writer.WriteGeneratedCodeWarning();
		writer.WriteLine();
		writer.WriteUsing(typeof(ColorR<>).Namespace!);
		writer.WriteLine();
		writer.WriteFileScopedNamespace(GenericOutputNamespace);

		foreach (CSharpPrimitives.Data t1 in CSharpPrimitives.List)
		{
			foreach (CSharpPrimitives.Data t2 in CSharpPrimitives.List)
			{
				if (t1.IsFloatingPoint != t2.IsFloatingPoint)
				{
					// one is floating point, the other is not
					continue;
				}

				if (t1.Type == typeof(decimal) || t2.Type == typeof(decimal))
				{
					// decimal has rounding errors when converting to/from double and float
					continue;
				}

				if (t1.Size > t2.Size)
				{
					continue;
				}

				writer.WriteLine($"[TestFixture(TypeArgs = new Type[] {{ typeof({t1.LangName}), typeof({t2.LangName}) }})]");
			}
		}
		writer.WriteLine("partial class LosslessColorTests<T1, T2>");
		using (new CurlyBrackets(writer))
		{
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
		textWriter.WriteLine($"color.{channel} = MakeRandomValue();");

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
		textWriter.WriteLosslessConversionTest(currentData.ColorType.Name, currentData.ChannelTypeName, containingData.ColorType.Name, containingData.ChannelTypeName);
	}

	private static void WriteLosslessConversionTest(this IndentedTextWriter writer, string t1Color, string t1Channel, string t2Color, string t2Channel)
	{
		writer.WriteLine("[Test]");
		writer.WriteLine($"public void ConversionIsLosslessTo{t2Color.Replace("<","_").Replace(">", "")}()");
		using (new CurlyBrackets(writer))
		{
			writer.WriteLine($"LosslessConversion.Assert<{t1Color}, {t1Channel}, {t2Color}, {t2Channel}>();");
		}
	}

	private static void WriteMakeRandomColor(this IndentedTextWriter textWriter, GenerationData data)
	{
		textWriter.WriteLine($"public static {data.ColorType.Name} MakeRandomColor() => ColorRandom<{data.ColorType.Name}, {data.ChannelTypeName}>.MakeRandomColor();");
	}

	private static void WriteMakeRandomValue(this IndentedTextWriter textWriter, GenerationData data)
	{
		textWriter.WriteLine($"public static {data.ChannelTypeName} MakeRandomValue() => ColorRandom<{data.ColorType.Name}, {data.ChannelTypeName}>.MakeRandomValue();");
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

	private static void WriteNamespaceUsings(this IndentedTextWriter textWriter, string? colorNamespace)
	{
		textWriter.WriteLine($"using {colorNamespace};");
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