using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.ColorGenerator;

internal static class CSharpPrimitives
{
	internal static IReadOnlyDictionary<Type, string> MinimumValues { get; } = new Dictionary<Type, string>()
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

	internal static IReadOnlyDictionary<Type, string> MaximumValues { get; } = new Dictionary<Type, string>()
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

	internal static IReadOnlyDictionary<Type, string> TypeNames { get; } = new Dictionary<Type, string>()
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

	internal static IReadOnlyDictionary<Type, int> Sizes { get; } = new Dictionary<Type, int>()
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

	internal static IEnumerable<Type> Types => TypeNames.Keys;

	internal static Type FirstType { get; } = Types.First();
}