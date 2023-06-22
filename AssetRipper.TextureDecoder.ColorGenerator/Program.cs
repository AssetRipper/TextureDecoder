using System.CodeDom.Compiler;

namespace AssetRipper.TextureDecoder.ColorGenerator;

internal static class Program
{
	private const string AttributeNamespace = "AssetRipper.TextureDecoder.Attributes";
	private const string OutputNamespace = "AssetRipper.TextureDecoder.Rgb.Formats";
	private const string OutputFolder = "../../../../AssetRipper.TextureDecoder/Rgb/Formats/";

	private static readonly Dictionary<Type, string> MinimumValues = new()
	{
		{ typeof(byte), "byte.MinValue" },
		{ typeof(sbyte), "sbyte.MinValue" },
		{ typeof(short), "short.MinValue" },
		{ typeof(ushort), "ushort.MinValue" },
		{ typeof(Half), "default" },
		{ typeof(float), "0f" },
		{ typeof(double), "0d" },
	};

	private static readonly Dictionary<Type, string> MaximumValues = new()
	{
		{ typeof(byte), "byte.MaxValue" },
		{ typeof(sbyte), "sbyte.MaxValue" },
		{ typeof(short), "short.MaxValue" },
		{ typeof(ushort), "ushort.MaxValue" },
		{ typeof(Half), "HalfConstants.One" },
		{ typeof(float), "1f" },
		{ typeof(double), "1d" },
	};

	private static readonly Dictionary<Type, string> TypeNames = new()
	{
		{ typeof(byte), "byte" },
		{ typeof(sbyte), "sbyte" },
		{ typeof(short), "short" },
		{ typeof(ushort), "ushort" },
		{ typeof(Half), "Half" },
		{ typeof(float), "float" },
		{ typeof(double), "double" },
	};

	/// <summary>
	/// Name, Type, Red, Blue, Green, Alpha
	/// </summary>
	private static readonly List<(string, Type, bool, bool, bool, bool)> Colors = new()
	{
		( "ColorA8", typeof(byte), false, false, false, true ),

		( "ColorR8", typeof(byte), true, false, false, false ),
		( "ColorRG16", typeof(byte), true, true, false, false ),
		( "ColorRGB24", typeof(byte), true, true, true, false ),
		( "ColorRGBA32", typeof(byte), true, true, true, true ),

		( "ColorR8Signed", typeof(sbyte), true, false, false, false ),
		( "ColorRG16Signed", typeof(sbyte), true, true, false, false ),
		( "ColorRGB24Signed", typeof(sbyte), true, true, true, false ),
		( "ColorRGBA32Signed", typeof(sbyte), true, true, true, true ),

		( "ColorR16", typeof(ushort), true, false, false, false ),
		( "ColorRG32", typeof(ushort), true, true, false, false ),
		( "ColorRGB48", typeof(ushort), true, true, true, false ),
		( "ColorRGBA64", typeof(ushort), true, true, true, true ),

		( "ColorR16Signed", typeof(short), true, false, false, false ),
		( "ColorRG32Signed", typeof(short), true, true, false, false ),
		( "ColorRGB48Signed", typeof(short), true, true, true, false ),
		( "ColorRGBA64Signed", typeof(short), true, true, true, true ),

		( "ColorR16Half", typeof(Half), true, false, false, false ),
		( "ColorRG32Half", typeof(Half), true, true, false, false ),
		( "ColorRGB48Half", typeof(Half), true, true, true, false ),
		( "ColorRGBA64Half", typeof(Half), true, true, true, true ),

		( "ColorR32Single", typeof(float), true, false, false, false ),
		( "ColorRG64Single", typeof(float), true, true, false, false ),
		( "ColorRGB96Single", typeof(float), true, true, true, false ),
		( "ColorRGBA128Single", typeof(float), true, true, true, true ),

		( "ColorR64Double", typeof(double), true, false, false, false ),
		( "ColorRG128Double", typeof(double), true, true, false, false ),
		( "ColorRGB192Double", typeof(double), true, true, true, false ),
		( "ColorRGBA256Double", typeof(double), true, true, true, true ),
	};

	/// <summary>
	/// Name, Type, Red, Blue, Green, Alpha, Fully Utilized
	/// </summary>
	private static readonly List<(string, Type, bool, bool, bool, bool, bool)> OtherColors = new()
	{
		( "ColorARGB16", typeof(byte), true, true, true, true, false ),
		( "ColorARGB32", typeof(byte), true, true, true, true, true ),
		( "ColorBGRA32", typeof(byte), true, true, true, true, true ),
		( "ColorRGB16", typeof(byte), true, true, true, false, false ),
		( "ColorRGB9e5", typeof(double), true, true, true, false, false ),
		( "ColorRGBA16", typeof(byte), true, true, true, true, false ),
		( "ColorRGB32Half", typeof(Half), true, true, true, false, false ),
	};

	static void Main()
	{
		foreach (var color in Colors)
		{
			WriteColor(color);
		}
		foreach (var otherColor in OtherColors)
		{
			WriteOtherColor(otherColor);
		}
		Console.WriteLine("Done!");
	}

	private static void WriteColor((string, Type, bool, bool, bool, bool) details)
	{
		(string name, Type type, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha) = details;
		Console.WriteLine(name);
		using FileStream stream = File.Create($"{OutputFolder}{name}.g.cs");
		using StreamWriter streamWriter = new StreamWriter(stream);
		streamWriter.NewLine = "\n";
		using IndentedTextWriter writer = new IndentedTextWriter(streamWriter, "\t");
		writer.NewLine = "\n";
		WriteColor(writer, name, type, hasRed, hasGreen, hasBlue, hasAlpha);
	}

	private static void WriteOtherColor((string, Type, bool, bool, bool, bool, bool) details)
	{
		(string name, Type type, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha, bool fullyUtilized) = details;
		Console.WriteLine(name);
		using FileStream stream = File.Create($"{OutputFolder}{name}.g.cs");
		using StreamWriter streamWriter = new StreamWriter(stream);
		streamWriter.NewLine = "\n";
		using IndentedTextWriter writer = new IndentedTextWriter(streamWriter, "\t");
		writer.NewLine = "\n";
		WriteOtherColor(writer, name, type, hasRed, hasGreen, hasBlue, hasAlpha, fullyUtilized);
	}

	private static void WriteOtherColor(IndentedTextWriter writer, string name, Type type, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha, bool fullyUtilized)
	{
		string typeName = TypeNames[type];
		writer.WriteLine("//This code is source generated. Do not edit manually.");
		writer.WriteLine();
		writer.WriteLine($"using {AttributeNamespace};");
		writer.WriteLine();
		writer.WriteLine($"namespace {OutputNamespace}");
		writer.WriteLine('{');
		writer.Indent++;

		writer.WriteLine($"[RgbaAttribute(RedChannel = {hasRed.ToLowerString()}, GreenChannel = {hasGreen.ToLowerString()}, BlueChannel = {hasBlue.ToLowerString()}, AlphaChannel = {hasAlpha.ToLowerString()}, FullyUtilizedChannels = {fullyUtilized.ToLowerString()})]");
		writer.WriteLine($"public partial struct {name} : IColor<{typeName}>");
		writer.WriteLine('{');
		writer.WriteLine('}');

		writer.Indent--;
		writer.WriteLine('}');
	}

	private static void WriteColor(IndentedTextWriter writer, string name, Type type, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha)
	{
		string typeName = TypeNames[type];
		writer.WriteLine("//This code is source generated. Do not edit manually.");
		writer.WriteLine();
		writer.WriteLine($"using {AttributeNamespace};");
		writer.WriteLine();
		writer.WriteLine($"namespace {OutputNamespace}");
		writer.WriteLine('{');
		writer.Indent++;

		writer.WriteLine($"[RgbaAttribute(RedChannel = {hasRed.ToLowerString()}, GreenChannel = {hasGreen.ToLowerString()}, BlueChannel = {hasBlue.ToLowerString()}, AlphaChannel = {hasAlpha.ToLowerString()}, FullyUtilizedChannels = true)]");
		writer.WriteLine($"public partial struct {name} : IColor<{typeName}>");
		writer.WriteLine('{');
		writer.Indent++;

		WriteProperty(writer, hasRed, typeName, 'R', MinimumValues[type]);
		writer.WriteLine();
		WriteProperty(writer, hasGreen, typeName, 'G', MinimumValues[type]);
		writer.WriteLine();
		WriteProperty(writer, hasBlue, typeName, 'B', MinimumValues[type]);
		writer.WriteLine();
		WriteProperty(writer, hasAlpha, typeName, 'A', MaximumValues[type]);
		writer.WriteLine();

		WriteGetChannels(writer, typeName);
		writer.WriteLine();
		WriteSetChannels(writer, type, hasRed, hasGreen, hasBlue, hasAlpha);

		writer.Indent--;
		writer.WriteLine('}');

		writer.Indent--;
		writer.WriteLine('}');
	}

	private static void WriteGetChannels(IndentedTextWriter writer, string typeName)
	{
		writer.WriteLine($"public void GetChannels(out {typeName} r, out {typeName} g, out {typeName} b, out {typeName} a)");
		writer.WriteLine('{');
		writer.Indent++;
		writer.WriteLine("r = R;");
		writer.WriteLine("g = G;");
		writer.WriteLine("b = B;");
		writer.WriteLine("a = A;");
		writer.Indent--;
		writer.WriteLine('}');
	}

	private static void WriteSetChannels(IndentedTextWriter writer, Type type, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha)
	{
		string typeName = TypeNames[type];
		writer.WriteLine($"public void SetChannels({typeName} r, {typeName} g, {typeName} b, {typeName} a)");
		writer.WriteLine('{');
		writer.Indent++;
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
		writer.Indent--;
		writer.WriteLine('}');
	}

	private static void WriteProperty(IndentedTextWriter writer, bool hasColor, string typeName, char channel, string defaultValue)
	{
		if (hasColor)
		{
			writer.WriteLine($"public {typeName} {channel} {{ get; set; }}");
		}
		else
		{
			writer.WriteLine($"public {typeName} {channel} ");
			writer.WriteLine('{');
			writer.Indent++;
			writer.WriteLine($"get => {defaultValue};");
			writer.WriteLine("set { }");
			writer.Indent--;
			writer.WriteLine('}');
		}
	}

	private static string ToLowerString(this bool value)
	{
		return value ? "true" : "false";
	}
}
