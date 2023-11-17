using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;

namespace AssetRipper.TextureDecoder.Bc;

public static class Bc7
{
	/// <summary>
	/// The size of an encoded block, in bytes.
	/// </summary>
	public const int BlockSize = 16;
	/// <summary>
	/// The width of a decoded block, in pixels.
	/// </summary>
	private const int BlockWidth = 4;
	/// <summary>
	/// The height of a decoded block, in pixels.
	/// </summary>
	private const int BlockHeight = 4;
	/// <summary>
	/// The size of the natural pixel type.
	/// </summary>
	private static int PixelSize => Unsafe.SizeOf<ColorRGBA<byte>>();

	public static int Decompress(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
	{
		output = new byte[width * height * Unsafe.SizeOf<ColorBGRA32>()];
		return Decompress(input, width, height, output);
	}

	public static int Decompress(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
	{
		int inputOffset = 0;
		for (int i = 0; i < height; i += BlockHeight)
		{
			for (int j = 0; j < width; j += BlockWidth)
			{
				int outputOffset = ((i * width) + j) * PixelSize;
				BcHelpers.DecompressBc7(
					input.Slice(inputOffset, BlockSize),
					output.Slice(outputOffset),
					width * 4);
				inputOffset += BlockSize;
			}
		}
		RgbConverter.Convert<ColorRGBA<byte>, byte, ColorBGRA32, byte>(output, width, height, output);
		return inputOffset;
	}

	internal static int GetCompressedSize(int w, int h) => ((w) >> 2) * ((h) >> 2) * BlockSize;
}
