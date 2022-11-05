using System.CodeDom.Compiler;

namespace AssetRipper.TextureDecoder.ColorGenerator;

internal class Program
{
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
		{ typeof(Half), "(Half)1" },
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

	private static readonly List<(string, Type, bool, bool, bool, bool)> Colors = new()
	{
		( "A8", typeof(byte), false, false, false, true ),

		( "R8", typeof(byte), true, false, false, false ),
		( "RG16", typeof(byte), true, true, false, false ),
		( "RGB24", typeof(byte), true, true, true, false ),
		( "RGBA32", typeof(byte), true, true, true, true ),

		( "R8Signed", typeof(sbyte), true, false, false, false ),
		( "RG16Signed", typeof(sbyte), true, true, false, false ),
		( "RGB24Signed", typeof(sbyte), true, true, true, false ),
		( "RGBA32Signed", typeof(sbyte), true, true, true, true ),

		( "R16", typeof(ushort), true, false, false, false ),
		( "RG32", typeof(ushort), true, true, false, false ),
		( "RGB48", typeof(ushort), true, true, true, false ),
		( "RGBA64", typeof(ushort), true, true, true, true ),

		( "R16Signed", typeof(short), true, false, false, false ),
		( "RG32Signed", typeof(short), true, true, false, false ),
		( "RGB48Signed", typeof(short), true, true, true, false ),
		( "RGBA64Signed", typeof(short), true, true, true, true ),

		( "RHalf", typeof(Half), true, false, false, false ),
		( "RGHalf", typeof(Half), true, true, false, false ),
		( "RGBHalf", typeof(Half), true, true, true, false ),
		( "RGBAHalf", typeof(Half), true, true, true, true ),

		( "RSingle", typeof(float), true, false, false, false ),
		( "RGSingle", typeof(float), true, true, false, false ),
		( "RGBSingle", typeof(float), true, true, true, false ),
		( "RGBASingle", typeof(float), true, true, true, true ),
	};

	static void Main()
	{
		foreach (var details in Colors)
		{
			WriteStruct(details);
		}
		Console.WriteLine("Done!");
	}

	private static void WriteStruct((string, Type, bool, bool, bool, bool) details)
	{
		string name = $"Color{details.Item1}";
		Console.WriteLine(name);
		using FileStream stream = File.Create($"{OutputFolder}{name}.g.cs");
		using StreamWriter streamWriter = new StreamWriter(stream);
		using IndentedTextWriter writer = new IndentedTextWriter(streamWriter, "\t");
		(_, Type type, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha) = details;
		WriteStruct(writer, name, type, hasRed, hasGreen, hasBlue, hasAlpha);
	}

	private static void WriteStruct(IndentedTextWriter writer, string name, Type type, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha)
	{
		string typeName = TypeNames[type];
		writer.WriteLine("//Auto-generated code. Do not modify.");
		writer.WriteLine($"namespace {OutputNamespace}");
		writer.WriteLine('{');
		writer.Indent++;

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
		WriteSetChannels(writer, typeName);

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
		writer.WriteLine("DefaultColorMethods.GetChannels(this, out r, out g, out b, out a);");
		writer.Indent--;
		writer.WriteLine('}');
	}

	private static void WriteSetChannels(IndentedTextWriter writer, string typeName)
	{
		writer.WriteLine($"public void SetChannels({typeName} r, {typeName} g, {typeName} b, {typeName} a)");
		writer.WriteLine('{');
		writer.Indent++;
		writer.WriteLine("DefaultColorMethods.SetChannels(ref this, r, g, b, a);");
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
}
