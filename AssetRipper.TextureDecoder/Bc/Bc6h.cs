using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Buffers;

namespace AssetRipper.TextureDecoder.Bc;

public static class Bc6h
{
	internal const int BlockSize = 16;

	public static int Decompress(ReadOnlySpan<byte> input, int width, int height, bool isSigned, out byte[] output)
	{
		output = new byte[width * height * Unsafe.SizeOf<ColorBGRA32>()];
		return Decompress(input, width, height, isSigned, output);
	}

	public static int Decompress(ReadOnlySpan<byte> input, int width, int height, bool isSigned, Span<byte> output)
	{
		int bufferSize = width * height * Unsafe.SizeOf<ColorRGB<Half>>();
		byte[] bufferArray = ArrayPool<byte>.Shared.Rent(bufferSize);
		Span<byte> buffer = new Span<byte>(bufferArray, 0, bufferSize);
		int inputOffset = 0;
		for (int i = 0; i < height; i += 4)
		{
			for (int j = 0; j < width; j += 4)
			{
				int outputOffset = ((i * width) + j) * Unsafe.SizeOf<ColorRGB<Half>>();
				BcHelpers.DecompressBc6h(
					input.Slice(inputOffset, BlockSize),
					buffer.Slice(outputOffset),
					width * 3,
					isSigned);
				inputOffset += BlockSize;
			}
		}
		RgbConverter.Convert<ColorRGB<Half>, Half, ColorBGRA32, byte>(buffer, width, height, output);
		ArrayPool<byte>.Shared.Return(bufferArray);
		return inputOffset;
	}

	internal static int GetCompressedSize(int w, int h) => ((w) >> 2) * ((h) >> 2) * BlockSize;
}