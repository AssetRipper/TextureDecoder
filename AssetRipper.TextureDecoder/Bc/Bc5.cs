using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;

namespace AssetRipper.TextureDecoder.Bc;

public static class Bc5
{
	public static int Decompress(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
	{
		output = new byte[width * height * Unsafe.SizeOf<ColorBGRA32>()];
		return Decompress(input, width, height, output);
	}

	public static int Decompress(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
	{
		int inputOffset = 0;
		Span<byte> buffer = new byte[width * height * Unsafe.SizeOf<ColorRG16>()];
		for (int i = 0; i < height; i += 4)
		{
			for (int j = 0; j < width; j += 4)
			{
				int bufferOffset = ((i * width) + j) * Unsafe.SizeOf<ColorRG16>();
				BcHelpers.DecompressBc5(input.Slice(inputOffset), buffer.Slice(bufferOffset), width * 2);
				inputOffset += DefineConstants.BCDEC_BC5_BLOCK_SIZE;
			}
		}
		RgbConverter.Convert<ColorRG16, byte, ColorBGRA32, byte>(buffer, width, height, output);
		return inputOffset;
	}
}