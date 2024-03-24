using AssetRipper.Text.SourceGeneration;
using AssetRipper.TextureDecoder.SourceGeneration.Common;
using System.CodeDom.Compiler;
using System.Text;

namespace AssetRipper.TextureDecoder.ColorGenerator;

internal static partial class Program
{
	private const string RgbNamespace = "AssetRipper.TextureDecoder.Rgb";
	private const string RgbFolder = "../../../../AssetRipper.TextureDecoder/Rgb/";
	private const string FormatsNamespace = "AssetRipper.TextureDecoder.Rgb.Formats";
	private const string FormatsFolder = "../../../../AssetRipper.TextureDecoder/Rgb/Formats/";

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
		for (int i = 1; i <= 4; i++)
		{
			WriteSuperGenericColor(i);
		}
		NumericConversionGenerator.Run();
		Console.WriteLine("Done!");
	}

	private static void WriteCustomColor((string, Type, bool, bool, bool, bool, bool) details)
	{
		(string name, Type type, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha, bool fullyUtilized) = details;
		Console.WriteLine(name);
		using IndentedTextWriter writer = IndentedTextWriterFactory.Create(FormatsFolder, name);
		WriteCustomColor(writer, name, type, hasRed, hasGreen, hasBlue, hasAlpha, fullyUtilized);
	}

	private static void WriteGenericColor((string, bool, bool, bool, bool) details)
	{
		(string name, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha) = details;
		Console.WriteLine(name);
		using IndentedTextWriter writer = IndentedTextWriterFactory.Create(FormatsFolder, name);
		WriteGenericColor(writer, name, hasRed, hasGreen, hasBlue, hasAlpha);
	}

	private static void WriteSuperGenericColor(int channelCount)
	{
		string name = $"Color`{channelCount}";
		Console.WriteLine(name);
		using IndentedTextWriter writer = IndentedTextWriterFactory.Create(RgbFolder, name);
		WriteSuperGenericColor(writer, channelCount);
	}

	private static void WriteCustomColor(IndentedTextWriter writer, string name, Type type, bool hasRed, bool hasGreen, bool hasBlue, bool hasAlpha, bool fullyUtilized)
	{
		string typeName = CSharpPrimitives.Dictionary[type].LangName;
		writer.WriteGeneratedCodeWarning();
		writer.WriteLine();
		using (new Namespace(writer, FormatsNamespace))
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
		WriteNonGenericStaticProperties(writer, hasRed.ToLowerString(), hasGreen.ToLowerString(), hasBlue.ToLowerString(), hasAlpha.ToLowerString(), fullyUtilized.ToLowerString(), typeName);
	}

	private static void WriteNonGenericStaticProperties(IndentedTextWriter writer, string hasRed, string hasGreen, string hasBlue, string hasAlpha, string fullyUtilized, string typeName)
	{
		writer.WriteLine($"static bool IColor.HasRedChannel => {hasRed};");
		writer.WriteLine($"static bool IColor.HasGreenChannel => {hasGreen};");
		writer.WriteLine($"static bool IColor.HasBlueChannel => {hasBlue};");
		writer.WriteLine($"static bool IColor.HasAlphaChannel => {hasAlpha};");
		writer.WriteLine($"static bool IColor.ChannelsAreFullyUtilized => {fullyUtilized};");
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

		writer.WriteFileScopedNamespace(FormatsNamespace);
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

	private static void WriteSuperGenericColor(IndentedTextWriter writer, int channelCount)
	{
		const string typeName = "TChannelValue";
		const string minValue = $"NumericConversion.GetMinimumValueSafe<{typeName}>()";
		const string maxValue = $"NumericConversion.GetMaximumValueSafe<{typeName}>()";

		string[] channelTypeParameters = channelCount switch
		{
			1 => ["TChannel"],
			2 => ["TChannel1", "TChannel2"],
			3 => ["TChannel1", "TChannel2", "TChannel3"],
			4 => ["TChannel1", "TChannel2", "TChannel3", "TChannel4"],
			_ => throw new(),
		};

		string[] fieldNames = channelCount switch
		{
			1 => ["value"],
			2 => ["value1", "value2"],
			3 => ["value1", "value2", "value3"],
			4 => ["value1", "value2", "value3", "value4"],
			_ => throw new(),
		};



		writer.WriteGeneratedCodeWarning();
		writer.WriteLineNoTabs();

		writer.WriteUsing("AssetRipper.TextureDecoder.Rgb.Channels");
		writer.WriteLineNoTabs();

		writer.WriteFileScopedNamespace("AssetRipper.TextureDecoder.Rgb");
		writer.WriteLineNoTabs();

		string structName;
		{
			StringBuilder sb = new();
			sb.Append("Color<");
			sb.Append(typeName);
			for (int i = 0; i < channelTypeParameters.Length; i++)
			{
				sb.Append(", ");
				sb.Append(channelTypeParameters[i]);
			}
			sb.Append('>');
			structName = sb.ToString();
		}

		writer.WriteLine($"public partial struct {structName} : IColor<{structName}, {typeName}>");
		using (new Indented(writer))
		{
			writer.WriteLine($"where {typeName} : unmanaged, INumberBase<{typeName}>, IMinMaxValue<{typeName}>");
			foreach (string channel in channelTypeParameters)
			{
				writer.WriteLine($"where {channel} : IChannel");
			}
		}

		using (new CurlyBrackets(writer))
		{
			foreach (string fieldName in fieldNames)
			{
				writer.WriteLine($"private {typeName} {fieldName};");
			}
			writer.WriteLineNoTabs();

			WriteProperty(writer, 'R', "Red", channelTypeParameters, fieldNames, minValue, typeName);
			writer.WriteLineNoTabs();
			WriteProperty(writer, 'G', "Green", channelTypeParameters, fieldNames, minValue, typeName);
			writer.WriteLineNoTabs();
			WriteProperty(writer, 'B', "Blue", channelTypeParameters, fieldNames, minValue, typeName);
			writer.WriteLineNoTabs();
			WriteProperty(writer, 'A', "Alpha", channelTypeParameters, fieldNames, maxValue, typeName);
			writer.WriteLineNoTabs();

			writer.Write("public Color(");
			for (int i = 0; i < fieldNames.Length; i++)
			{
				if (i != 0)
				{
					writer.Write(", ");
				}

				writer.Write(typeName);
				writer.Write(' ');
				writer.Write(fieldNames[i]);
			}
			writer.WriteLine(')');
			using (new CurlyBrackets(writer))
			{
				foreach (string fieldName in fieldNames)
				{
					writer.WriteLine($"this.{fieldName} = {fieldName};");
				}
			}
			writer.WriteLineNoTabs();
			WriteGetChannels(writer, typeName);
			writer.WriteLineNoTabs();
			WriteSetChannels(writer, typeName, true, true, true, true);
			writer.WriteLineNoTabs();
			WriteNonGenericStaticProperties(
				writer,
				string.Join(" || ", channelTypeParameters.Select(c => $"{c}.IsRed")),
				string.Join(" || ", channelTypeParameters.Select(c => $"{c}.IsGreen")),
				string.Join(" || ", channelTypeParameters.Select(c => $"{c}.IsBlue")),
				string.Join(" || ", channelTypeParameters.Select(c => $"{c}.IsAlpha")),
				string.Join(" && ", channelTypeParameters.Select(c => $"{c}.FullyUtilized")),
				typeName);
			writer.WriteLineNoTabs();

			writer.WriteLine($"public static {structName} Black");
			using (new CurlyBrackets(writer))
			{
				writer.WriteLine($"get => new({string.Join(", ", channelTypeParameters.Select(c => $"{c}.GetBlack<{typeName}>()"))});");
			}
			writer.WriteLineNoTabs();

			writer.WriteLine($"public static {structName} White");
			using (new CurlyBrackets(writer))
			{
				writer.WriteLine($"get => new({string.Join(", ", channelTypeParameters.Select(c => $"{c}.GetWhite<{typeName}>()"))});");
			}
			writer.WriteLineNoTabs();

			WriteToString(writer, true, true, true, true);
		}

		static void WriteProperty(IndentedTextWriter writer, char property, string channel, string[] channels, string[] fields, string defaultValue, string typeName)
		{
			writer.WriteLine($"public {typeName} {property}");
			using (new CurlyBrackets(writer))
			{
				writer.WriteLine("readonly get");
				using (new CurlyBrackets(writer))
				{
					If.Write(
						writer,
						Enumerable.Range(0, channels.Length).Select(i => new ValueTuple<string, Action<IndentedTextWriter>?>($"{channels[i]}.Is{channel}", (writer) =>
						{
							writer.WriteLine($"return {channels[i]}.Get{channel}({fields[i]});");
						})),
						(writer) =>
						{
							writer.WriteLine($"return {defaultValue};");
						});
				}
				writer.WriteLine("set");
				using (new CurlyBrackets(writer))
				{
					for (int i = 0; i < channels.Length; i++)
					{
						writer.WriteLine($"Channel.SetIf{channel}<{channels[i]}, {typeName}>(ref {fields[i]}, value);");
					}
				}
			}
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
