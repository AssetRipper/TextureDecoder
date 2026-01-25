using AssetRipper.Text.SourceGeneration;
using AssetRipper.TextureDecoder.SourceGeneration.Common;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace AssetRipper.TextureDecoder.ColorGenerator;
internal static class NumericConversionGenerator
{
	private const string OutputNamespace = "AssetRipper.TextureDecoder.Rgb";
	private const string OutputDirectory = "../../../../AssetRipper.TextureDecoder/Rgb/";
	private const string ClassName = "NumericConversion";
	private const string ChangeSign = "ChangeSign";

	public static void Run()
	{
		Console.WriteLine(ClassName);
		using IndentedTextWriter writer = IndentedTextWriterFactory.Create(OutputDirectory, ClassName);
		Write(writer);
	}

	private static void Write(IndentedTextWriter writer)
	{
		writer.WriteGeneratedCodeWarning();
		writer.WriteLine();
		writer.WriteFileScopedNamespace(OutputNamespace);
		writer.WriteLine();
		writer.WriteLine($"static partial class {ClassName}");
		using (new CurlyBrackets(writer))
		{
			WritePrimaryConvertMethod(writer);
			foreach (CSharpPrimitives.Data from in CSharpPrimitives.List)
			{
				WriteConvertMethod(writer, from);
			}
			WriteGetValueMethod(writer, "GetMinimumValue");
			WriteGetValueMethod(writer, "GetMaximumValue");
			WriteChangeSignMethods(writer);
		}
	}

	private static void WriteChangeSignMethods(IndentedTextWriter writer)
	{
		foreach ((CSharpPrimitives.Data unsignedType, CSharpPrimitives.Data signedType) in GetIntegerTypes())
		{
			if (!unsignedType.HasPointerSize(out _, out _))
			{
				WriteMethod(writer, unsignedType, signedType, unsignedType);
				WriteMethod(writer, signedType, unsignedType, unsignedType);
			}
		}

		static void WriteMethod(IndentedTextWriter writer, CSharpPrimitives.Data parameterType, CSharpPrimitives.Data returnType, CSharpPrimitives.Data unsignedType)
		{
			writer.WriteLine("[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]");
			writer.WriteLine($"private static {returnType.LangName} {ChangeSign}({parameterType.LangName} value)");
			using (new CurlyBrackets(writer))
			{
				string methodName = parameterType == unsignedType ? "ToSignedNumber" : "ToUnsignedNumber";
				writer.WriteLine($"return {methodName}<{parameterType.LangName}, {returnType.LangName}>(value);");
			}
			writer.WriteLineNoTabs();
		}

		static IEnumerable<(CSharpPrimitives.Data, CSharpPrimitives.Data)> GetIntegerTypes()
		{
			foreach (CSharpPrimitives.Data data in CSharpPrimitives.List)
			{
				if (data.IsSignedInteger(out CSharpPrimitives.Data? unsignedData))
				{
					yield return (unsignedData, data);
				}
			}
		}
	}

	private static void WritePrimaryConvertMethod(IndentedTextWriter writer)
	{
		writer.WriteLine("[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]");
		writer.WriteLine($"public static TTo Convert<TFrom, TTo>(TFrom value) where TFrom : unmanaged where TTo : unmanaged");
		using (new CurlyBrackets(writer))
		{
			writer.WriteLine("if (typeof(TFrom) == typeof(TTo))");
			using (new CurlyBrackets(writer))
			{
				writer.WriteLine("return Unsafe.As<TFrom, TTo>(ref value);");
			}
			foreach (CSharpPrimitives.Data from in CSharpPrimitives.List)
			{
				writer.WriteLine($"else if (typeof(TFrom) == typeof({from.LangName}))");
				using (new CurlyBrackets(writer))
				{
					writer.WriteLine($"return {ConvertMethodName(from.Type)}<TTo>(Unsafe.As<TFrom, {from.LangName}>(ref value));");
				}
			}
			writer.WriteLine("else");
			using (new CurlyBrackets(writer))
			{
				writer.WriteLine("return ThrowOrReturnDefault<TTo>();");
			}
		}
		writer.WriteLineNoTabs();
	}

	private static void WriteConvertMethod(IndentedTextWriter writer, CSharpPrimitives.Data from)
	{
		string methodName = ConvertMethodName(from.Type);
		writer.WriteLine("[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]");
		writer.WriteLine($"private static TTo {methodName}<TTo>({from.LangName} value) where TTo : unmanaged");
		using (new CurlyBrackets(writer))
		{
			if (from.HasPointerSize(out CSharpPrimitives.Data? bit32From, out CSharpPrimitives.Data? bit64From))
			{
				using (new If(writer, "IntPtr.Size == sizeof(int)"))
				{
					writer.WriteLine($"return {ConvertMethodName(bit32From.Type)}<TTo>(({bit32From.LangName})value);");
				}
				using (new Else(writer))
				{
					writer.WriteLine($"return {ConvertMethodName(bit64From.Type)}<TTo>(({bit64From.LangName})value);");
				}
			}
			else if (from.IsSignedInteger(out CSharpPrimitives.Data? unsignedFrom))
			{
				writer.WriteLine($"return {ConvertMethodName(unsignedFrom.Type)}<TTo>({ChangeSign}(value));");
			}
			else
			{
				foreach (CSharpPrimitives.Data to in CSharpPrimitives.List)
				{
					string ifOrElseIf = to == CSharpPrimitives.FirstData ? "if" : "else if";
					writer.WriteLine($"{ifOrElseIf} (typeof(TTo) == typeof({to.LangName}))");
					using (new CurlyBrackets(writer))
					{
						if (from == to)
						{
							writer.WriteLine($"return Unsafe.As<{to.LangName}, TTo>(ref value);");
						}
						else if (to.HasPointerSize(out CSharpPrimitives.Data? bit32To, out CSharpPrimitives.Data? bit64To))
						{
							using (new If(writer, "IntPtr.Size == sizeof(int)"))
							{
								writer.WriteLine($"{to.LangName} converted = ({to.LangName}){methodName}<{bit32To.LangName}>(value);");
								writer.WriteLine($"return Unsafe.As<{to.LangName}, TTo>(ref converted);");
							}
							using (new Else(writer))
							{
								writer.WriteLine($"{to.LangName} converted = ({to.LangName}){methodName}<{bit64To.LangName}>(value);");
								writer.WriteLine($"return Unsafe.As<{to.LangName}, TTo>(ref converted);");
							}
						}
						else if (to.IsSignedInteger(out CSharpPrimitives.Data? unsignedTo))
						{
							writer.WriteLine($"{to.LangName} converted = {ChangeSign}({methodName}<{unsignedTo.LangName}>(value));");
							writer.WriteLine($"return Unsafe.As<{to.LangName}, TTo>(ref converted);");
						}
						else if (from.IsFloatingPoint)
						{
							if (to.IsFloatingPoint)
							{
								writer.WriteLine($"{to.LangName} converted = ({to.LangName})value;");
								writer.WriteLine($"return Unsafe.As<{to.LangName}, TTo>(ref converted);");
							}
							else
							{
								Debug.Assert(to.IsUnsignedInteger);
								CSharpPrimitives.Data conversionType = CSharpPrimitives.GetDataForIntegerFloatingPointConversion(to, from);
								if (conversionType != from)
								{
									writer.WriteComment($"We use {conversionType.LangName} because it has enough precision to convert from {from.LangName} to {to.LangName}.");
									writer.WriteLine($"return {ConvertMethodName(conversionType.Type)}<TTo>(({conversionType.LangName})value);");
								}
								else
								{
									writer.WriteComment("x must be clamped because of rounding errors.");
									string minValue = to.MinValue;
									string maxValue = to.MaxValue;
									writer.WriteLine($"{from.LangName} x = value * ({from.LangName}){maxValue};");
									writer.WriteLine($"{to.LangName} converted = ({from.LangName}){maxValue} < x ? {maxValue} : (x > ({from.LangName}){minValue} ? ({to.LangName})x : {minValue});");
									writer.WriteLine($"return Unsafe.As<{to.LangName}, TTo>(ref converted);");
								}
							}
						}
						else
						{
							Debug.Assert(from.IsUnsignedInteger);
							if (to.IsFloatingPoint)
							{
								CSharpPrimitives.Data conversionType = CSharpPrimitives.GetDataForIntegerFloatingPointConversion(from, to);
								if (conversionType != to)
								{
									writer.WriteComment($"There isn't enough precision to convert from {from.LangName} to {to.LangName}, so we convert to {conversionType.LangName} first.");
									writer.WriteLine($"{conversionType.LangName} x = {methodName}<{conversionType.LangName}>(value);");
									writer.WriteLine($"{to.LangName} converted = ({to.LangName})x;");
									writer.WriteLine($"return Unsafe.As<{to.LangName}, TTo>(ref converted);");
								}
								else
								{
									writer.WriteLine($"{to.LangName} converted = ({to.LangName})value / ({to.LangName}){from.MaxValue};");
									writer.WriteLine($"return Unsafe.As<{to.LangName}, TTo>(ref converted);");
								}
							}
							else
							{
								Debug.Assert(to.IsUnsignedInteger);
								writer.WriteComment("See https://github.com/AssetRipper/TextureDecoder/issues/19");
								if (from.Size < to.Size)
								{
									int divided = to.Size / from.Size;
									string conversionType = to.Size < sizeof(uint) ? "uint" : to.LangName;
									writer.WriteLine("unchecked");
									using (new CurlyBrackets(writer))
									{
										writer.Write($"{to.LangName} converted = ({to.LangName})(");
										for (int i = divided - 1; i > 0; i--)
										{
											writer.Write($"(({conversionType})value << {i * from.Size * 8}) | ");
										}
										writer.WriteLine("value);");
										writer.WriteLine($"return Unsafe.As<{to.LangName}, TTo>(ref converted);");
									}
								}
								else
								{
									Debug.Assert(from.Size > to.Size);
									if (from.Type == typeof(ushort) && to.Type == typeof(byte))
									{
										writer.WriteComment("This is a special case where we already know an optimal algorithm.");
										writer.WriteLine("uint x = (value * 255u + 32895u) >> 16;");
										writer.WriteLine("byte converted = unchecked((byte)x);");
										writer.WriteLine("return Unsafe.As<byte, TTo>(ref converted);");
									}
									else if (from.Size < sizeof(double) && to.Size < sizeof(double))
									{
										writer.WriteLine($"double interpolated = (double)value / (double){from.MaxValue};");
										writer.WriteLine($"double exact = interpolated * (double){to.MaxValue};");
										writer.WriteLine($"{to.LangName} converted = ({to.LangName})double.Round(exact, MidpointRounding.AwayFromZero);");
										writer.WriteLine($"return Unsafe.As<{to.LangName}, TTo>(ref converted);");
									}
									else
									{
										writer.WriteComment($"There are more accurate ways to map {from.TypeName} onto {to.TypeName}, but this is the simplest.");
										string conversionType = from.Size < sizeof(uint) ? "uint" : from.LangName;
										writer.WriteLine("unchecked");
										using (new CurlyBrackets(writer))
										{
											int offset = (from.Size / to.Size - 1) * to.Size * 8;
											writer.WriteLine($"{to.LangName} converted = ({to.LangName})(({conversionType})value >> {offset});");
											writer.WriteLine($"return Unsafe.As<{to.LangName}, TTo>(ref converted);");
										}
									}
								}
							}
						}
					}
				}
				writer.WriteLine("else");
				using (new CurlyBrackets(writer))
				{
					writer.WriteLine("return ThrowOrReturnDefault<TTo>();");
				}
			}
		}
		writer.WriteLineNoTabs();
	}

	private static void WriteGetValueMethod(IndentedTextWriter writer, string methodName)
	{
		writer.WriteLine("[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]");
		writer.WriteLine($"public static T {methodName}<T>() where T : unmanaged");
		using (new CurlyBrackets(writer))
		{
			foreach (CSharpPrimitives.Data from in CSharpPrimitives.List)
			{
				string ifOrElseIf = from == CSharpPrimitives.FirstData ? "if" : "else if";
				writer.WriteLine($"{ifOrElseIf} (typeof(T) == typeof({from.LangName}))");
				using (new CurlyBrackets(writer))
				{
					writer.WriteLine($"{from.LangName} value = {methodName}Safe<{from.LangName}>();");
					writer.WriteLine($"return Unsafe.As<{from.LangName}, T>(ref value);");
				}
			}
			writer.WriteLine("else");
			using (new CurlyBrackets(writer))
			{
				writer.WriteLine("return ThrowOrReturnDefault<T>();");
			}
		}
		writer.WriteLineNoTabs();
	}

	private static string ConvertMethodName(Type from)
	{
		return $"Convert{from.Name}";
	}
}
