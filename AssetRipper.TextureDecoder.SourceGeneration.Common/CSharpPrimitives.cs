using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.SourceGeneration.Common;

public static class CSharpPrimitives
{
	public static IReadOnlyDictionary<Type, string> MinimumValues { get; } = new Dictionary<Type, string>()
	{
		{ typeof(sbyte), "sbyte.MinValue" },
		{ typeof(byte), "byte.MinValue" },
		{ typeof(short), "short.MinValue" },
		{ typeof(ushort), "ushort.MinValue" },
		{ typeof(int), "int.MinValue" },
		{ typeof(uint), "uint.MinValue" },
		{ typeof(long), "long.MinValue" },
		{ typeof(ulong), "ulong.MinValue" },
		{ typeof(Half), "default" },
		{ typeof(float), "0f" },
		{ typeof(double), "0d" },
		{ typeof(decimal), "0m" },
	};

	public static IReadOnlyDictionary<Type, string> MaximumValues { get; } = new Dictionary<Type, string>()
	{
		{ typeof(sbyte), "sbyte.MaxValue" },
		{ typeof(byte), "byte.MaxValue" },
		{ typeof(short), "short.MaxValue" },
		{ typeof(ushort), "ushort.MaxValue" },
		{ typeof(int), "int.MaxValue" },
		{ typeof(uint), "uint.MaxValue" },
		{ typeof(long), "long.MaxValue" },
		{ typeof(ulong), "ulong.MaxValue" },
		{ typeof(Half), "Half.One" },
		{ typeof(float), "1f" },
		{ typeof(double), "1d" },
		{ typeof(decimal), "1m" },
	};

	public static IReadOnlyDictionary<Type, string> TypeNames { get; } = new Dictionary<Type, string>()
	{
		{ typeof(sbyte), "sbyte" },
		{ typeof(byte), "byte" },
		{ typeof(short), "short" },
		{ typeof(ushort), "ushort" },
		{ typeof(int), "int" },
		{ typeof(uint), "uint" },
		{ typeof(long), "long" },
		{ typeof(ulong), "ulong" },
		{ typeof(Half), "Half" },
		{ typeof(float), "float" },
		{ typeof(double), "double" },
		{ typeof(decimal), "decimal" },
	};

	public static IReadOnlyDictionary<Type, int> Sizes { get; } = new Dictionary<Type, int>()
	{
		{ typeof(sbyte), sizeof(sbyte) },
		{ typeof(byte), sizeof(byte) },
		{ typeof(short), sizeof(short) },
		{ typeof(ushort), sizeof(ushort) },
		{ typeof(int), sizeof(int) },
		{ typeof(uint), sizeof(uint) },
		{ typeof(long), sizeof(long) },
		{ typeof(ulong), sizeof(ulong) },
		{ typeof(Half), Unsafe.SizeOf<Half>() },
		{ typeof(float), sizeof(float) },
		{ typeof(double), sizeof(double) },
		{ typeof(decimal), sizeof(decimal) }
	};

	public static IEnumerable<Type> Types => TypeNames.Keys;

	public static Type FirstType { get; } = Types.First();

	public static bool IsFloatingPoint(Type type)
	{
		return type == typeof(Half) || type == typeof(float) || type == typeof(double) || type == typeof(decimal);
	}

	public static bool IsSignedInteger(Type type, [NotNullWhen(true)] out Type? unsignedType)
	{
		if (type == typeof(sbyte))
		{
			unsignedType = typeof(byte);
		}
		else if (type == typeof(short))
		{
			unsignedType = typeof(ushort);
		}
		else if (type == typeof(int))
		{
			unsignedType = typeof(uint);
		}
		else if (type == typeof(long))
		{
			unsignedType = typeof(ulong);
		}
		else
		{
			unsignedType = null;
		}
		return unsignedType is not null;
	}

	public static bool IsUnsignedInteger(Type type)
	{
		return type == typeof(byte) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong);
	}
}