using AssetRipper.Text.SourceGeneration;
using AssetRipper.TextureDecoder.SourceGeneration.Common;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

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
		writer.WriteLine("//This code is source generated. Do not edit manually.");
		writer.WriteLine();
		writer.WriteFileScopedNamespace(OutputNamespace);
		writer.WriteLine();
		writer.WriteLine($"static partial class {ClassName}");
		using (new CurlyBrackets(writer))
		{
			WritePrimaryConvertMethod(writer);
			foreach (Type type in CSharpPrimitives.Types)
			{
				WriteConvertMethod(writer, type);
			}
			WriteChangeSignMethods(writer);
		}
	}

	private static void WriteChangeSignMethods(IndentedTextWriter writer)
	{
		foreach ((string unsignedType, string signedType, int byteSize) in GetIntegerTypes())
		{
			WriteMethod(writer, unsignedType, signedType, byteSize, false);
			WriteMethod(writer, signedType, unsignedType, byteSize, true);
		}

		static void WriteMethod(IndentedTextWriter writer, string parameterType, string returnType, int byteSize, bool needsCast)
		{
			writer.WriteLine("[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]");
			writer.WriteLine($"private static {returnType} {ChangeSign}({parameterType} value)");
			using (new CurlyBrackets(writer))
			{
				writer.WriteLine("unchecked");
				using (new CurlyBrackets(writer))
				{
					string signBitNumber = $"0x8{new string('0', byteSize * 2 - 1)}";
					string signBitType = byteSize <= sizeof(uint) ? "uint" : "ulong";
					string cast = needsCast ? $"({signBitType})" : "";
					writer.WriteLine($"const {signBitType} SignBit = {signBitNumber};");
					writer.WriteLine($"return ({returnType})({cast}value ^ SignBit);");
				}
			}
			writer.WriteLineNoTabs();
		}

		static IEnumerable<(string, string, int)> GetIntegerTypes()
		{
			yield return ("byte", "sbyte", 1);
			yield return ("ushort", "short", 2);
			yield return ("uint", "int", 4);
			yield return ("ulong", "long", 8);
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
			foreach ((Type from, string fromName) in CSharpPrimitives.TypeNames)
			{
				writer.WriteLine($"else if (typeof(TFrom) == typeof({fromName}))");
				using (new CurlyBrackets(writer))
				{
					writer.WriteLine($"return {ConvertMethodName(from)}<TTo>(Unsafe.As<TFrom, {fromName}>(ref value));");
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

	private static void WriteConvertMethod(IndentedTextWriter writer, Type from)
	{
		string methodName = ConvertMethodName(from);
		string fromName = CSharpPrimitives.TypeNames[from];
		writer.WriteLine("[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]");
		writer.WriteLine($"private static TTo {methodName}<TTo>({fromName} value) where TTo : unmanaged");
		using (new CurlyBrackets(writer))
		{
			if (CSharpPrimitives.IsSignedInteger(from, out Type? unsignedFrom))
			{
				writer.WriteLine($"{CSharpPrimitives.TypeNames[unsignedFrom]} unsigned = {ChangeSign}(value);");
				writer.WriteLine($"return {ConvertMethodName(unsignedFrom)}<TTo>(unsigned);");
			}
			else
			{
				foreach ((Type to, string toName) in CSharpPrimitives.TypeNames)
				{
					string ifOrElseIf = to == CSharpPrimitives.FirstType ? "if" : "else if";
					writer.WriteLine($"{ifOrElseIf} (typeof(TTo) == typeof({toName}))");
					using (new CurlyBrackets(writer))
					{
						if (from == to)
						{
							writer.WriteLine($"return Unsafe.As<{toName}, TTo>(ref value);");
						}
						else if (CSharpPrimitives.IsSignedInteger(to, out Type? unsignedTo))
						{
							string unsignedToName = CSharpPrimitives.TypeNames[unsignedTo];
							writer.WriteLine($"{toName} converted = {ChangeSign}({methodName}<{unsignedToName}>(value));");
							writer.WriteLine($"return Unsafe.As<{toName}, TTo>(ref converted);");
						}
						else if (CSharpPrimitives.IsFloatingPoint(from))
						{
							if (CSharpPrimitives.IsFloatingPoint(to))
							{
								writer.WriteLine($"{toName} converted = ({toName})value;");
								writer.WriteLine($"return Unsafe.As<{toName}, TTo>(ref converted);");
							}
							else
							{
								Debug.Assert(CSharpPrimitives.IsUnsignedInteger(to));
								if (from == typeof(Half))
								{
									writer.WriteComment("We use float because it has enough precision to convert from Half to any integer type.");
									writer.WriteLine($"return {ConvertMethodName(typeof(float))}<TTo>((float)value);");
								}
								else
								{
									writer.WriteComment("x must be clamped because of rounding errors.");
									string minValue = CSharpPrimitives.MinimumValues[to];
									string maxValue = CSharpPrimitives.MaximumValues[to];
									writer.WriteLine($"{fromName} x = value * {maxValue};");
									writer.WriteLine($"{toName} converted = {maxValue} < x ? {maxValue} : (x > {minValue} ? ({toName})x : {minValue});");
									writer.WriteLine($"return Unsafe.As<{toName}, TTo>(ref converted);");
								}
							}
						}
						else
						{
							Debug.Assert(CSharpPrimitives.IsUnsignedInteger(from));
							if (CSharpPrimitives.IsFloatingPoint(to))
							{
								if (to == typeof(Half))
								{
									writer.WriteComment("There isn't enough precision to convert from anything bigger than byte to Half, so we convert to float first.");
									writer.WriteLine($"float x = {methodName}<float>(value);");
									writer.WriteLine("Half converted = (Half)x;");
									writer.WriteLine("return Unsafe.As<Half, TTo>(ref converted);");
								}
								else
								{
									string maxValue = CSharpPrimitives.MaximumValues[from];
									writer.WriteLine($"{toName} converted = value / ({toName}){maxValue};");
									writer.WriteLine($"return Unsafe.As<{toName}, TTo>(ref converted);");
								}
							}
							else
							{
								Debug.Assert(CSharpPrimitives.IsUnsignedInteger(to));
								writer.WriteComment("See https://github.com/AssetRipper/TextureDecoder/issues/19");
								int fromSize = CSharpPrimitives.Sizes[from];
								int toSize = CSharpPrimitives.Sizes[to];
								if (fromSize < toSize)
								{
									int divided = toSize / fromSize;
									string conversionType = toSize <= sizeof(uint) ? "uint" : "ulong";
									writer.WriteLine("unchecked");
									using (new CurlyBrackets(writer))
									{
										writer.Write($"{toName} converted = ({toName})(");
										for (int i = divided - 1; i > 0; i--)
										{
											writer.Write($"(({conversionType})value << {i * fromSize * 8}) | ");
										}
										writer.WriteLine("value);");
										writer.WriteLine($"return Unsafe.As<{toName}, TTo>(ref converted);");
									}
								}
								else
								{
									Debug.Assert(fromSize > toSize);
									if (from == typeof(ushort) && to == typeof(byte))
									{
										writer.WriteComment("This is a special case where we already know an optimal algorithm.");
										writer.WriteLine("uint x = (value * 255u + 32895u) >> 16;");
										writer.WriteLine("byte converted = unchecked((byte)x);");
										writer.WriteLine("return Unsafe.As<byte, TTo>(ref converted);");
									}
									else
									{
										writer.WriteComment($"There are more accurate ways to map {fromName} onto {toName}, but this is the simplest.");
										string conversionType = fromSize <= sizeof(uint) ? "uint" : "ulong";
										writer.WriteLine("unchecked");
										using (new CurlyBrackets(writer))
										{
											int offset = (fromSize / toSize - 1) * toSize * 8;
											writer.WriteLine($"{toName} converted = ({toName})(({conversionType})value >> {offset});");
											writer.WriteLine($"return Unsafe.As<{toName}, TTo>(ref converted);");
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

	private static string ConvertMethodName(Type from)
	{
		return $"Convert{from.Name}";
	}
}
