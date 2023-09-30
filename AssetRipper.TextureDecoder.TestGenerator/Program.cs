using AssetRipper.Text.SourceGeneration;
using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using AssetRipper.TextureDecoder.SourceGeneration.Common;
using System.CodeDom.Compiler;

namespace AssetRipper.TextureDecoder.TestGenerator
{
	internal static class Program
	{
		internal static readonly List<GenerationData> dataList = new()
		{
			GenerationData.Create<ColorARGB16>(2),
			GenerationData.Create<ColorARGB32>(4),
			GenerationData.Create<ColorBGRA32>(4),
			GenerationData.Create<ColorRGB16>(2),
			GenerationData.Create<ColorRGB9e5>(4),
			GenerationData.Create<ColorRGBA16>(2),
		};

		static void Main()
		{
			TestClassGenerator.GenerateTestClasses();
			GenerateRgbConsoleMethod();
			TextureDataClassGenerator.Generate();
			Console.WriteLine("Done!");
		}

		private static void GenerateRgbConsoleMethod()
		{
			Console.WriteLine("Program");
			using IndentedTextWriter writer = IndentedTextWriterFactory.Create("../../../../AssetRipper.TextureDecoder.ConsoleApp", "Program");

			writer.WriteGeneratedCodeWarning();
			writer.WriteLine();
			writer.WriteLine($"using {typeof(RgbConverter).Namespace};");
			writer.WriteLine($"using {typeof(ColorBGRA32).Namespace};");
			writer.WriteLine();

			writer.WriteFileScopedNamespace("AssetRipper.TextureDecoder.ConsoleApp");
			writer.WriteLine("static partial class Program");
			using (new CurlyBrackets(writer))
			{
				writer.WriteLine("private static void DecodeRgb(ReadOnlySpan<byte> input, int width, int height, string modeString, Span<byte> output)");
				using (new CurlyBrackets(writer))
				{
					List<(string, string, string)> list = GetColorNames().ToList();
					writer.WriteLine("Console.WriteLine(\"Arg at index 5 : mode\");");
					for (int i = 0; i < list.Count; i++)
					{
						writer.WriteLine($"Console.WriteLine(\"  {i} - {list[i].Item1.Substring(5)}\");");
					}
					writer.WriteLine("int mode = int.Parse(modeString);");
					writer.WriteLine("switch(mode)");
					using (new CurlyBrackets(writer))
					{
						for (int i = 0 ; i < list.Count; i++)
						{
							(string prettyName, string colorName, string channelName) = list[i];
							writer.WriteLine($"case {i}:");
							using (new Indented(writer))
							{
								writer.WriteLine($"RgbConverter.Convert<{colorName}, {channelName}, ColorBGRA32, byte>(input, width, height, output);");
								writer.WriteLine("break;");
							}
						}
						{
							writer.WriteLine("default:");
							using (new Indented(writer))
							{
								writer.WriteLine("throw new NotSupportedException(mode.ToString());");
							}
						}
					}
				}
			}

			static IEnumerable<(string, string, string)> GetColorNames()
			{
				foreach (CSharpPrimitives.Data primitiveData in CSharpPrimitives.List)
				{
					int primitiveBitSize = primitiveData.Size * 8;
					foreach (string genericColorName in TestClassGenerator.GetGenericColorNames())
					{
						string prettyName = $"{genericColorName}{primitiveBitSize}{primitiveData.TypeName}";
						yield return (prettyName, $"{genericColorName}<{primitiveData.LangName}>", primitiveData.LangName);
					}
				}
				foreach (GenerationData data in dataList)
				{
					yield return (data.ColorType.Name, data.ColorType.Name, data.ChannelTypeName);
				}
			}
		}
	}
}
