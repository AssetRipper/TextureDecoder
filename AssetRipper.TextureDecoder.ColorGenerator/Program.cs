using AssetRipper.Text.SourceGeneration;
using AssetRipper.TextureDecoder.SourceGeneration.Common;
using System.CodeDom.Compiler;
using System.Text;

namespace AssetRipper.TextureDecoder.ColorGenerator;

internal static partial class Program
{
	private const string AttributeNamespace = "AssetRipper.TextureDecoder.Attributes";
	private const string OutputNamespace = "AssetRipper.TextureDecoder.Rgb.Formats";
	private const string OutputFolder = "../../../../AssetRipper.TextureDecoder/Rgb/Formats/";

	/// <summary>
	/// Name, Type, Red, Blue, Green, Alpha, Fully Utilized
	/// </summary>
	private static readonly List<(string, Type, bool, bool, bool, bool, bool)> CustomColors = new()
	{
		( "ColorARGB16", typeof(byte), true, true, true, true, false ),
		( "ColorARGB32", typeof(byte), true, true, true, true, true ),
		( "ColorBGRA32", typeof(byte), true, true, true, true, true ),
		( "ColorRGB16", typeof(byte), true, true, true, false, false ),
		( "ColorRGB9e5", typeof(double), true, true, true, false, false ),
		( "ColorRGBA16", typeof(byte), true, true, true, true, false ),
	};

	/// <summary>
	/// Name, Red, Blue, Green, Alpha
	/// </summary>
	private static readonly List<(string, bool, bool, bool, bool)> GenericColors = new()
	{
		( "ColorR", true, false, false, false ),
		( "ColorRG", true, true, false, false ),
		( "ColorRGB", true, true, true, false ),
		( "ColorRGBA", true, true, true, true ),
		( "ColorA", false, false, false, true ),
	};

	static void Main()
	{
		foreach (var customColor in CustomColors)
		{
			WriteCustomColor(customColor);
		}
		foreach (var genericColor in GenericColors)
		{
			WriteGenericColor(genericColor);
		}
		NumericConversionGenerator.Run();
		Console.WriteLine("Done!");
	}

	private static void WriteCustomColor((string, Type, bool, bool, bool, bool, bool) details)
	{
		(string name, Type type, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha, bool fullyUtilized) = details;
		Console.WriteLine(name);
		using IndentedTextWriter writer = IndentedTextWriterFactory.Create(OutputFolder, name);
		WriteCustomColor(writer, name, type, hasRed, hasGreen, hasBlue, hasAlpha, fullyUtilized);
	}

	private static void WriteGenericColor((string, bool, bool, bool, bool) details)
	{
		(string name, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha) = details;
		Console.WriteLine(name);
		using IndentedTextWriter writer = IndentedTextWriterFactory.Create(OutputFolder, name);
		WriteGenericColor(writer, name, hasRed, hasGreen, hasBlue, hasAlpha);
	}

	private static void WriteCustomColor(IndentedTextWriter writer, string name, Type type, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha, bool fullyUtilized)
	{
		string typeName = CSharpPrimitives.Dictionary[type].LangName;
		writer.WriteGeneratedCodeWarning();
		writer.WriteLine();
		using (new Namespace(writer, OutputNamespace))
		{
			writer.WriteLine($"public partial struct {name} : IColor<{typeName}>");
			using (new CurlyBrackets(writer))
			{
				WriteNonGenericStaticProperties(writer, hasRed, hasGreen, hasBlue, hasAlpha, fullyUtilized, typeName);
				writer.WriteLineNoTabs();
				WriteToString(writer, hasRed, hasGreen, hasBlue, hasAlpha);
			}
		}
	}

	private static void WriteToString(IndentedTextWriter writer, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha)
	{
		writer.WriteLine("public override string ToString()");
		using (new CurlyBrackets(writer))
		{
			StringBuilder sb = new();
			sb.Append("return $\"{{ ");
			bool first = true;
			if (hasRed)
			{
				sb.Append("R: {R}");
				first = false;
			}
			if (hasGreen)
			{
				if (!first)
				{
					sb.Append(", ");
				}
				sb.Append("G: {G}");
				first = false;
			}
			if (hasBlue)
			{
				if (!first)
				{
					sb.Append(", ");
				}
				sb.Append("B: {B}");
				first = false;
			}
			if (hasAlpha)
			{
				if (!first)
				{
					sb.Append(", ");
				}
				sb.Append("A: {A}");
			}
			sb.Append(" }}\";");
			writer.WriteLine(sb.ToString());
		}
	}

	private static void WriteNonGenericStaticProperties(IndentedTextWriter writer, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha, bool fullyUtilized, string typeName)
	{
		writer.WriteLine($"static bool IColor.HasRedChannel => {hasRed.ToLowerString()};");
		writer.WriteLine($"static bool IColor.HasGreenChannel => {hasGreen.ToLowerString()};");
		writer.WriteLine($"static bool IColor.HasBlueChannel => {hasBlue.ToLowerString()};");
		writer.WriteLine($"static bool IColor.HasAlphaChannel => {hasAlpha.ToLowerString()};");
		writer.WriteLine($"static bool IColor.ChannelsAreFullyUtilized => {fullyUtilized.ToLowerString()};");
		writer.WriteLine($"static Type IColor.ChannelType => typeof({typeName});");
	}

	private static void WriteBlackWhiteStaticProperties(IndentedTextWriter writer, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha, string colorTypeName, string channelTypeName)
	{
		string minValue = $"NumericConversion.GetMinimumValueSafe<{channelTypeName}>()";
		string maxValue = $"NumericConversion.GetMaximumValueSafe<{channelTypeName}>()";

		//Black
		{
			writer.Write($"public static {colorTypeName}<{channelTypeName}> Black => new(");
			int rgbCount = CountRGB(hasRed, hasGreen, hasBlue);
			for (int i = 0; i < rgbCount; i++)
			{
				if (i != 0)
				{
					writer.Write(", ");
				}
				writer.Write(minValue);
			}
			if (hasAlpha)
			{
				if (rgbCount != 0)
				{
					writer.Write(", ");
				}
				writer.Write(maxValue);
			}
			writer.WriteLine(");");
		}

		//White
		if (hasRed || hasGreen || hasBlue)
		{
			writer.Write($"public static {colorTypeName}<{channelTypeName}> White => new(");
			int rgbaCount = CountRGBA(hasRed, hasGreen, hasBlue, hasAlpha);
			for (int i = 0; i < rgbaCount; i++)
			{
				if (i != 0)
				{
					writer.Write(", ");
				}
				writer.Write(maxValue);
			}
			writer.WriteLine(");");
		}
		else
		{
			writer.WriteLine($"static {colorTypeName}<{channelTypeName}> IColor<{colorTypeName}<{channelTypeName}>, {channelTypeName}>.White => throw new NotSupportedException();");
		}

		static int CountRGB(bool hasRed, bool hasGreen, bool hasBlue)
		{
			return (hasRed ? 1 : 0) + (hasGreen ? 1 : 0) + (hasBlue ? 1 : 0);
		}

		static int CountRGBA(bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha)
		{
			return CountRGB(hasRed, hasGreen, hasBlue) + (hasAlpha ? 1 : 0);
		}
	}

	private static void WriteGenericColor(IndentedTextWriter writer, string name, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha)
	{
		const string typeName = "T";
		const string minValue = "NumericConversion.GetMinimumValueSafe<T>()";
		const string maxValue = "NumericConversion.GetMaximumValueSafe<T>()";

		writer.WriteGeneratedCodeWarning();
		writer.WriteLineNoTabs();

		writer.WriteFileScopedNamespace(OutputNamespace);
		writer.WriteLineNoTabs();

		writer.WriteLine($"public partial struct {name}<T> : IColor<{name}<T>, T> where T : unmanaged, INumberBase<T>, IMinMaxValue<T>");
		using (new CurlyBrackets(writer))
		{
			WriteProperty(writer, hasRed, typeName, 'R', minValue);
			writer.WriteLineNoTabs();
			WriteProperty(writer, hasGreen, typeName, 'G', minValue);
			writer.WriteLineNoTabs();
			WriteProperty(writer, hasBlue, typeName, 'B', minValue);
			writer.WriteLineNoTabs();
			WriteProperty(writer, hasAlpha, typeName, 'A', maxValue);
			writer.WriteLineNoTabs();

			WriteConstructor(writer, name, hasRed, hasGreen, hasBlue, hasAlpha, typeName);
			writer.WriteLineNoTabs();
			WriteGetChannels(writer, typeName);
			writer.WriteLineNoTabs();
			WriteSetChannels(writer, typeName, hasRed, hasGreen, hasBlue, hasAlpha);
			writer.WriteLineNoTabs();
			WriteNonGenericStaticProperties(writer, hasRed, hasGreen, hasBlue, hasAlpha, true, typeName);
			writer.WriteLineNoTabs();
			WriteBlackWhiteStaticProperties(writer, hasRed, hasGreen, hasBlue, hasAlpha, name, typeName);
			writer.WriteLineNoTabs();
			WriteToString(writer, hasRed, hasGreen, hasBlue, hasAlpha);
		}
	}

	private static void WriteConstructor(IndentedTextWriter writer, string declaringTypeName, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha, string channelTypeName)
	{
		writer.Write($"public {declaringTypeName}(");
		if (hasRed)
		{
			writer.Write($"{channelTypeName} r");
			if (hasGreen || hasBlue || hasAlpha)
			{
				writer.Write(", ");
			}
		}
		if (hasGreen)
		{
			writer.Write($"{channelTypeName} g");
			if (hasBlue || hasAlpha)
			{
				writer.Write(", ");
			}
		}
		if (hasBlue)
		{
			writer.Write($"{channelTypeName} b");
			if (hasAlpha)
			{
				writer.Write(", ");
			}
		}
		if (hasAlpha)
		{
			writer.Write($"{channelTypeName} a");
		}
		writer.WriteLine(")");
		using (new CurlyBrackets(writer))
		{
			if (hasRed)
			{
				writer.WriteLine("R = r;");
			}
			if (hasGreen)
			{
				writer.WriteLine("G = g;");
			}
			if (hasBlue)
			{
				writer.WriteLine("B = b;");
			}
			if (hasAlpha)
			{
				writer.WriteLine("A = a;");
			}
		}
	}

	private static void WriteGetChannels(IndentedTextWriter writer, string typeName)
	{
		writer.WriteLine($"public readonly void GetChannels(out {typeName} r, out {typeName} g, out {typeName} b, out {typeName} a)");
		using (new CurlyBrackets(writer))
		{
			writer.WriteLine("r = R;");
			writer.WriteLine("g = G;");
			writer.WriteLine("b = B;");
			writer.WriteLine("a = A;");
		}
	}

	private static void WriteSetChannels(IndentedTextWriter writer, string typeName, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha)
	{
		writer.WriteLine($"public void SetChannels({typeName} r, {typeName} g, {typeName} b, {typeName} a)");
		using (new CurlyBrackets(writer))
		{
			if (hasRed)
			{
				writer.WriteLine("R = r;");
			}
			if (hasGreen)
			{
				writer.WriteLine("G = g;");
			}
			if (hasBlue)
			{
				writer.WriteLine("B = b;");
			}
			if (hasAlpha)
			{
				writer.WriteLine("A = a;");
			}
		}
	}

	private static void WriteProperty(IndentedTextWriter writer, bool hasColor, string typeName, char channel, string defaultValue)
	{
		if (hasColor)
		{
			writer.WriteLine($"public {typeName} {channel} {{ get; set; }}");
		}
		else
		{
			writer.WriteLine($"public readonly {typeName} {channel} ");
			using (new CurlyBrackets(writer))
			{
				writer.WriteLine($"get => {defaultValue};");
				writer.WriteLine("set { }");
			}
		}
	}

	private static string ToLowerString(this bool value)
	{
		return value ? "true" : "false";
	}
}
