using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.SourceGeneration.Common;

public static class CSharpPrimitives
{
	public static IReadOnlyDictionary<Type, Data> Dictionary { get; } = new Dictionary<Type, Data>()
	{
		{ typeof(sbyte), new Data<sbyte>() },
		{ typeof(byte), new Data<byte>() },
		{ typeof(short), new Data<short>() },
		{ typeof(ushort), new Data<ushort>() },
		{ typeof(int), new Data<int>() },
		{ typeof(uint), new Data<uint>() },
		{ typeof(long), new Data<long>() },
		{ typeof(ulong), new Data<ulong>() },
		{ typeof(Int128), new Data<Int128>() },
		{ typeof(UInt128), new Data<UInt128>() },
		{ typeof(Half), new Data<Half>() },
		{ typeof(float), new Data<float>() },
		{ typeof(double), new Data<double>() },
		{ typeof(decimal), new Data<decimal>() },
	};

	public static IReadOnlyList<Data> List { get; } = Dictionary.Values.ToList();

	public static IReadOnlyList<Type> Types { get; } = Dictionary.Keys.ToList();

	public static Type FirstType => Types[0];

	public static Data FirstData => List[0];

	public static bool IsFloatingPoint(Type type)
	{
		return type == typeof(Half) || type == typeof(float) || type == typeof(double) || type == typeof(decimal);
	}

	public static bool IsUnsignedInteger(Type type)
	{
		return type == typeof(byte) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong) || type == typeof(UInt128);
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
		else if (type == typeof(Int128))
		{
			unsignedType = typeof(UInt128);
		}
		else
		{
			unsignedType = null;
		}
		return unsignedType is not null;
	}

	public static bool CanBeConstant(Type type)
	{
		return type == typeof(byte)
			|| type == typeof(sbyte)
			|| type == typeof(ushort)
			|| type == typeof(short)
			|| type == typeof(uint)
			|| type == typeof(int)
			|| type == typeof(ulong)
			|| type == typeof(long)
			|| type == typeof(float)
			|| type == typeof(double)
			|| type == typeof(bool)
			|| type == typeof(char)
			|| type == typeof(string);
	}

	public static string GetLangName(Type type)
	{
		if (type == typeof(sbyte))
		{
			return "sbyte";
		}
		else if (type == typeof(byte))
		{
			return "byte";
		}
		else if (type == typeof(short))
		{
			return "short";
		}
		else if (type == typeof(ushort))
		{
			return "ushort";
		}
		else if (type == typeof(int))
		{
			return "int";
		}
		else if (type == typeof(uint))
		{
			return "uint";
		}
		else if (type == typeof(long))
		{
			return "long";
		}
		else if (type == typeof(ulong))
		{
			return "ulong";
		}
		else if (type == typeof(float))
		{
			return "float";
		}
		else if (type == typeof(double))
		{
			return "double";
		}
		else if (type == typeof(decimal))
		{
			return "decimal";
		}
		else if (type == typeof(char))
		{
			return "char";
		}
		else if (type == typeof(bool))
		{
			return "bool";
		}
		else if (type == typeof(string))
		{
			return "string";
		}
		else
		{
			return type.Name;
		}
	}

	public abstract class Data
	{
		public abstract Type Type { get; }
		public string TypeName => Type.Name;
		public string LangName => GetLangName(Type);
		public bool CanBeConstant => CanBeConstant(Type);
		public bool IsFloatingPoint => IsFloatingPoint(Type);
		public bool IsUnsignedInteger => IsUnsignedInteger(Type);
		public bool IsSignedInteger([NotNullWhen(true)] out Data? unsignedData)
		{
			if (CSharpPrimitives.IsSignedInteger(Type, out Type? unsignedType))
			{
				unsignedData = Dictionary[unsignedType];
				return true;
			}
			else
			{
				unsignedData = null;
				return false;
			}
		}
		public abstract int Size { get; }
		public override string ToString() => TypeName;
		public string MinValue => IsFloatingPoint ? $"{LangName}.Zero" : $"{LangName}.MinValue";
		public string MaxValue => IsFloatingPoint ? $"{LangName}.One" : $"{LangName}.MaxValue";
	}

	private sealed class Data<T> : Data
	{
		public override Type Type => typeof(T);

		public override int Size => Unsafe.SizeOf<T>();
	}
}