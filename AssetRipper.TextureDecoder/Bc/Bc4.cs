using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Buffers;

namespace AssetRipper.TextureDecoder.Bc;

public static class Bc4
{
	internal const int BlockSize = 8;

	public static int Decompress(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
	{
		output = new byte[width * height * Unsafe.SizeOf<ColorBGRA32>()];
		return Decompress(input, width, height, output);
	}

	public static int Decompress(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
	{
		int bufferSize = width * height * Unsafe.SizeOf<ColorR<byte>>();
		byte[] bufferArray = ArrayPool<byte>.Shared.Rent(bufferSize);
		Span<byte> buffer = new Span<byte>(bufferArray, 0, bufferSize);
		int inputOffset = 0;
		for (int i = 0; i < height; i += 4)
		{
			for (int j = 0; j < width; j += 4)
			{
				int bufferOffset = ((i * width) + j) * Unsafe.SizeOf<ColorR<byte>>();
				BcHelpers.DecompressBc4(input.Slice(inputOffset), buffer.Slice(bufferOffset), width);
				inputOffset += BlockSize;
			}
		}
		RgbConverter.Convert<ColorR<byte>, byte, ColorBGRA32, byte>(buffer, width, height, output);
		ArrayPool<byte>.Shared.Return(bufferArray);
		return inputOffset;
	}

	internal static int GetCompressedSize(int w, int h) => ((w) >> 2) * ((h) >> 2) * BlockSize;
}