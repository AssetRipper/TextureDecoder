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
				WriteColorBaseStaticProperties(writer, hasRed, hasGreen, hasBlue, hasAlpha, fullyUtilized, typeName);
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

	private static void WriteColorBaseStaticProperties(IndentedTextWriter writer, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha, bool fullyUtilized, string typeName)
	{
		writer.WriteLine($"static bool IColorBase.HasRedChannel => {hasRed.ToLowerString()};");
		writer.WriteLine($"static bool IColorBase.HasGreenChannel => {hasGreen.ToLowerString()};");
		writer.WriteLine($"static bool IColorBase.HasBlueChannel => {hasBlue.ToLowerString()};");
		writer.WriteLine($"static bool IColorBase.HasAlphaChannel => {hasAlpha.ToLowerString()};");
		writer.WriteLine($"static bool IColorBase.ChannelsAreFullyUtilized => {fullyUtilized.ToLowerString()};");
		writer.WriteLine($"static Type IColorBase.ChannelType => typeof({typeName});");
	}

	private static void WriteGenericColor(IndentedTextWriter writer, string name, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha)
	{
		const string typeName = "T";
		const string minValue = $"NumericConversion.GetMinimumValueSafe<T>()";
		const string maxValue = $"NumericConversion.GetMaximumValueSafe<T>()";

		writer.WriteGeneratedCodeWarning();
		writer.WriteLineNoTabs();

		writer.WriteFileScopedNamespace(OutputNamespace);
		writer.WriteLineNoTabs();

		string constraints = hasRed && hasGreen && hasBlue && hasAlpha
			? "unmanaged"
			: "unmanaged, INumberBase<T>, IMinMaxValue<T>";
		writer.WriteLine($"public partial struct {name}<T> : IColor<{typeName}> where T : {constraints}");
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

			WriteGetChannels(writer, typeName);
			writer.WriteLineNoTabs();
			WriteSetChannels(writer, typeName, hasRed, hasGreen, hasBlue, hasAlpha);
			writer.WriteLineNoTabs();
			WriteColorBaseStaticProperties(writer, hasRed, hasGreen, hasBlue, hasAlpha, true, typeName);
			writer.WriteLineNoTabs();
			WriteToString(writer, hasRed, hasGreen, hasBlue, hasAlpha);
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
