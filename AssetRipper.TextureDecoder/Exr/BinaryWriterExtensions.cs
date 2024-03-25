using System.Buffers;
using System.Text;

namespace AssetRipper.TextureDecoder.Exr;

internal static class BinaryWriterExtensions
{
	public static void WriteLengthPrefixedString(this BinaryWriter writer, string value)
	{
		int length = Encoding.UTF8.GetByteCount(value);
		writer.Write(length);

		byte[]? rentedArray;
		scoped Span<byte> buffer;
		if (length <= 1024)
		{
			rentedArray = null;
			buffer = stackalloc byte[length];
		}
		else
		{
			rentedArray = ArrayPool<byte>.Shared.Rent(length);
			buffer = rentedArray.AsSpan(0, length);
		}

		Encoding.UTF8.GetBytes(value, buffer);
		writer.Write(buffer);

		if (rentedArray is not null)
		{
			ArrayPool<byte>.Shared.Return(rentedArray);
		}
	}

	public static void WriteNullTerminatedString(this BinaryWriter writer, string value)
	{
		int length = Encoding.UTF8.GetByteCount(value);

		byte[]? rentedArray;
		scoped Span<byte> buffer;
		if (length <= 1024)
		{
			rentedArray = null;
			buffer = stackalloc byte[length];
		}
		else
		{
			rentedArray = ArrayPool<byte>.Shared.Rent(length);
			buffer = rentedArray.AsSpan(0, length);
		}

		Encoding.UTF8.GetBytes(value, buffer);
		writer.Write(buffer);

		if (rentedArray is not null)
		{
			ArrayPool<byte>.Shared.Return(rentedArray);
		}

		writer.WriteNullByte();
	}

	public static void WriteNullTerminatedString(this BinaryWriter writer, ReadOnlySpan<byte> utf8String)
	{
		writer.Write(utf8String);
		writer.WriteNullByte();
	}

	public static void WriteNullByte(this BinaryWriter writer)
	{
		writer.Write(default(byte));
	}

	public static void Write<T>(this BinaryWriter writer, T value) where T : unmanaged, Enum
	{
		switch (Unsafe.SizeOf<T>())
		{
			case sizeof(byte):
				writer.Write(Unsafe.As<T, byte>(ref value));
				break;
			case sizeof(ushort):
				writer.Write(Unsafe.As<T, ushort>(ref value));
				break;
			case sizeof(uint):
				writer.Write(Unsafe.As<T, uint>(ref value));
				break;
			case sizeof(ulong):
				writer.Write(Unsafe.As<T, ulong>(ref value));
				break;
		}
	}

	public static void WriteAttribute<T>(this BinaryWriter writer, ReadOnlySpan<byte> attributeName, T attributeValue) where T : IExrDataType
	{
		writer.Write(attributeName);
		writer.WriteNullByte();
		writer.Write(T.TypeName);
		writer.WriteNullByte();
		writer.Write(attributeValue.Size);
		attributeValue.Write(writer);
	}

	public static void WriteAttribute<T>(this BinaryWriter writer, ReadOnlySpan<byte> attributeName, ReadOnlySpan<byte> attributeType, T attributeValue) where T : unmanaged, Enum
	{
		writer.Write(attributeName);
		writer.WriteNullByte();
		writer.Write(attributeType);
		writer.WriteNullByte();
		writer.Write(Unsafe.SizeOf<T>());
		writer.Write(attributeValue);
	}
}
