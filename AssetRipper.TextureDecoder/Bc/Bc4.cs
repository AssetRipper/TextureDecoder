using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;

namespace AssetRipper.TextureDecoder.Bc;

public static class Bc4
{
	public static int Decompress(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
	{
		output = new byte[width * height * Unsafe.SizeOf<ColorBGRA32>()];
		return Decompress(input, width, height, output);
	}

	public static int Decompress(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
	{
		int inputOffset = 0;
		Span<byte> buffer = new byte[width * height * Unsafe.SizeOf<ColorR8>()];
		for (int i = 0; i < height; i += 4)
		{
			for (int j = 0; j < width; j += 4)
			{
				int bufferOffset = ((i * width) + j) * Unsafe.SizeOf<ColorR8>();
				BcHelpers.DecompressBc4(input.Slice(inputOffset), buffer.Slice(bufferOffset), width);
				inputOffset += DefineConstants.BCDEC_BC4_BLOCK_SIZE;
			}
		}
		RgbConverter.Convert<ColorR8, byte, ColorBGRA32, byte>(buffer, width, height, output);
		return inputOffset;
	}
}