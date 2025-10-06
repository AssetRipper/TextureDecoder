using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Buffers;

namespace AssetRipper.TextureDecoder.Bc;

public static class Bc5
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
	private static int PixelSize => Unsafe.SizeOf<ColorRG<byte>>();
	/// <summary>
	/// The size of a decoded block, in bytes.
	/// </summary>
	internal static int DecodedBlockSize => BlockWidth * BlockHeight * PixelSize;

	public static int Decompress<TOutputColor, TOutputChannelValue>(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
		where TOutputChannelValue : unmanaged
		where TOutputColor : unmanaged, IColor<TOutputChannelValue>
	{
		output = new byte[width * height * Unsafe.SizeOf<TOutputColor>()];
		return Decompress<TOutputColor, TOutputChannelValue>(input, width, height, output);
	}

	public static int Decompress<TOutputColor, TOutputChannelValue>(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
		where TOutputChannelValue : unmanaged
		where TOutputColor : unmanaged, IColor<TOutputChannelValue>
	{
		ThrowHelper.ThrowIfNotLittleEndian();
		return Decompress<TOutputColor, TOutputChannelValue>(input, width, height, MemoryMarshal.Cast<byte, TOutputColor>(output));
	}

	public static int Decompress<TOutputColor, TOutputChannelValue>(ReadOnlySpan<byte> input, int width, int height, Span<TOutputColor> output)
		where TOutputChannelValue : unmanaged
		where TOutputColor : unmanaged, IColor<TOutputChannelValue>
	{
		ThrowHelper.ThrowIfNotLittleEndian();
		ThrowHelper.ThrowIfNotEnoughSpace(output.Length, width * height);
		Span<byte> buffer = stackalloc byte[DecodedBlockSize];
		int inputOffset = 0;
		for (int i = 0; i < height; i += BlockHeight)
		{
			for (int j = 0; j < width; j += BlockWidth)
			{
				BcHelpers.DecompressBc5(input.Slice(inputOffset, BlockSize), buffer);
				BcHelpers.CopyBufferToOutput<ColorRG<byte>, byte, TOutputColor, TOutputChannelValue>(MemoryMarshal.Cast<byte, ColorRG<byte>>(buffer), output, width, height, j, i, BlockWidth, BlockHeight);
				inputOffset += BlockSize;
			}
		}
		return inputOffset;
	}

	internal static int GetCompressedSize(int w, int h) => ((w) >> 2) * ((h) >> 2) * BlockSize;
}
