using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using StbImageWriteSharp;
using System.Runtime.InteropServices;

namespace AssetRipper.TextureDecoder.ConsoleApp;

public struct DirectBitmap
{
	public DirectBitmap(int width, int height)
	{
		Width = width;
		Height = height;
		Data = new byte[width * height * 4];
	}

	public DirectBitmap(int width, int height, byte[] data)
	{
		Width = width;
		Height = height;
		Data = data;
	}

	public void FlipY()
	{
		Span<uint> pixels = MemoryMarshal.Cast<byte, uint>(Bits);
		for (int row = 0, irow = Height - 1; row < irow; row++, irow--)
		{
			Span<uint> rowTop = pixels.Slice(row * Width, Width);
			Span<uint> rowBottom = pixels.Slice(irow * Width, Width);
			for (int i = 0; i < Width; i++)
			{
				(rowTop[i], rowBottom[i]) = (rowBottom[i], rowTop[i]);
			}
		}
	}

	public void SaveAsPng(string path)
	{
		using Stream stream = File.OpenWrite(path);
		ImageWriter writer = new ImageWriter();
		RgbConverter.Convert<ColorBGRA32, byte, ColorRGBA32, byte>(Bits, Width, Height, out byte[] output);
		writer.WritePng(output, Width, Height, ColorComponents.RedGreenBlueAlpha, stream);
	}

	public int Height { get; }
	public int Width { get; }
	public int Size => Width * Height * 4;
	public Span<byte> Bits => new Span<byte>(Data, 0, Size);
	private byte[] Data { get; }
}
