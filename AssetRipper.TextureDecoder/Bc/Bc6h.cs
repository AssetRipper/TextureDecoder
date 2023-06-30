using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Buffers;

namespace AssetRipper.TextureDecoder.Bc;

public static class Bc6h
{
	public static int Decompress(ReadOnlySpan<byte> input, int width, int height, bool isSigned, out byte[] output)
	{
		output = new byte[width * height * Unsafe.SizeOf<ColorBGRA32>()];
		return Decompress(input, width, height, isSigned, output);
	}

	public static int Decompress(ReadOnlySpan<byte> input, int width, int height, bool isSigned, Span<byte> output)
	{
		int bufferSize = width * height * Unsafe.SizeOf<ColorRGB96Single>();
		byte[] bufferArray = ArrayPool<byte>.Shared.Rent(bufferSize);
		Span<byte> buffer = new Span<byte>(bufferArray, 0, bufferSize);
		int inputOffset = 0;
		for (int i = 0; i < height; i += 4)
		{
			for (int j = 0; j < width; j += 4)
			{
				int outputOffset = ((i * width) + j) * Unsafe.SizeOf<ColorRGB96Single>();
				BcHelpers.DecompressBc6h_Float(
					input.Slice(inputOffset, DefineConstants.BCDEC_BC6H_BLOCK_SIZE),
					buffer.Slice(outputOffset),
					width * 3,
					isSigned);
				inputOffset += DefineConstants.BCDEC_BC6H_BLOCK_SIZE;
			}
		}
		RgbConverter.Convert<ColorRGB96Single, float, ColorBGRA32, byte>(buffer, width, height, output);
		ArrayPool<byte>.Shared.Return(bufferArray);
		return inputOffset;
	}
}