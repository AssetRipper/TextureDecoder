﻿using AssetRipper.TextureDecoder.Rgb;
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

	public static int Decompress(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
	{
		output = new byte[width * height * Unsafe.SizeOf<ColorBGRA32>()];
		return Decompress(input, width, height, output);
	}

	public static int Decompress(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
	{
		int naturalSize = width * height * PixelSize;
		byte[] rentedArray = ArrayPool<byte>.Shared.Rent(naturalSize);
		Span<byte> naturalPixels = new Span<byte>(rentedArray, 0, naturalSize);
		Span<byte> buffer = stackalloc byte[DecodedBlockSize];
		int inputOffset = 0;
		for (int i = 0; i < height; i += BlockHeight)
		{
			for (int j = 0; j < width; j += BlockWidth)
			{
				BcHelpers.DecompressBc5(input.Slice(inputOffset), buffer);
				BcHelpers.CopyBufferToOutput(buffer, naturalPixels, width, height, j, i, BlockWidth, BlockHeight, PixelSize);
				inputOffset += BlockSize;
			}
		}
		RgbConverter.Convert<ColorRG<byte>, byte, ColorBGRA32, byte>(naturalPixels, width, height, output);
		ArrayPool<byte>.Shared.Return(rentedArray);
		return inputOffset;
	}

	internal static int GetCompressedSize(int w, int h) => ((w) >> 2) * ((h) >> 2) * BlockSize;
}
