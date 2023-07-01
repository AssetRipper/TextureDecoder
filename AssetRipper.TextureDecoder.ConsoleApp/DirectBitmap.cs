using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using StbImageWriteSharp;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AssetRipper.TextureDecoder.ConsoleApp;

public struct DirectBitmap<TColor, TColorArg>
	where TColorArg : unmanaged
	where TColor : unmanaged, IColor<TColorArg>
{
	public DirectBitmap(int width, int height)
	{
		Width = width;
		Height = height;
		Data = new byte[CalculateByteSize(width, height)];
	}

	public DirectBitmap(int width, int height, byte[] data)
	{
		if (data.Length < CalculateByteSize(width, height))
		{
			throw new ArgumentException("Data does not have enough space.", nameof(data));
		}
		
		Width = width;
		Height = height;
		Data = data;
	}

	public void FlipY()
	{
		Span<TColor> pixels = Pixels;
		for (int row = 0, irow = Height - 1; row < irow; row++, irow--)
		{
			Span<TColor> rowTop = pixels.Slice(row * Width, Width);
			Span<TColor> rowBottom = pixels.Slice(irow * Width, Width);
			for (int i = 0; i < Width; i++)
			{
				(rowTop[i], rowBottom[i]) = (rowBottom[i], rowTop[i]);
			}
		}
	}

	public void SaveAsBmp(string path)
	{
		using Stream stream = File.OpenWrite(path);
		ImageWriter writer = new();
		if (typeof(TColor) == typeof(ColorRGBA32))
		{
			writer.WriteBmp(Data, Width, Height, ColorComponents.RedGreenBlueAlpha, stream);
		}
		else if (typeof(TColor) == typeof(ColorRGB24))
		{
			writer.WriteBmp(Data, Width, Height, ColorComponents.RedGreenBlue, stream);
		}
		else
		{
			RgbConverter.Convert<TColor, TColorArg, ColorRGBA32, byte>(Bits, Width, Height, out byte[] output);
			writer.WriteBmp(output, Width, Height, ColorComponents.RedGreenBlueAlpha, stream);
		}
	}

	public void SaveAsHdr(string path)
	{
		using Stream stream = File.OpenWrite(path);
		ImageWriter writer = new();
		if (typeof(TColor) == typeof(ColorRGBA32))
		{
			writer.WriteHdr(Data, Width, Height, ColorComponents.RedGreenBlueAlpha, stream);
		}
		else if (typeof(TColor) == typeof(ColorRGB24))
		{
			writer.WriteHdr(Data, Width, Height, ColorComponents.RedGreenBlue, stream);
		}
		else
		{
			RgbConverter.Convert<TColor, TColorArg, ColorRGBA32, byte>(Bits, Width, Height, out byte[] output);
			writer.WriteHdr(output, Width, Height, ColorComponents.RedGreenBlueAlpha, stream);
		}
	}

	public void SaveAsJpg(string path)
	{
		using Stream stream = File.OpenWrite(path);
		ImageWriter writer = new();
		if (typeof(TColor) == typeof(ColorRGBA32))
		{
			writer.WriteJpg(Data, Width, Height, ColorComponents.RedGreenBlueAlpha, stream, default);
		}
		else if (typeof(TColor) == typeof(ColorRGB24))
		{
			writer.WriteJpg(Data, Width, Height, ColorComponents.RedGreenBlue, stream, default);
		}
		else
		{
			RgbConverter.Convert<TColor, TColorArg, ColorRGBA32, byte>(Bits, Width, Height, out byte[] output);
			writer.WriteJpg(output, Width, Height, ColorComponents.RedGreenBlueAlpha, stream, default);
		}
	}

	public void SaveAsPng(string path)
	{
		using Stream stream = File.OpenWrite(path);
		ImageWriter writer = new();
		if (typeof(TColor) == typeof(ColorRGBA32))
		{
			writer.WritePng(Data, Width, Height, ColorComponents.RedGreenBlueAlpha, stream);
		}
		else if (typeof(TColor) == typeof(ColorRGB24))
		{
			writer.WritePng(Data, Width, Height, ColorComponents.RedGreenBlue, stream);
		}
		else
		{
			RgbConverter.Convert<TColor, TColorArg, ColorRGBA32, byte>(Bits, Width, Height, out byte[] output);
			writer.WritePng(output, Width, Height, ColorComponents.RedGreenBlueAlpha, stream);
		}
	}

	public void SaveAsTga(string path)
	{
		using Stream stream = File.OpenWrite(path);
		ImageWriter writer = new();
		if (typeof(TColor) == typeof(ColorRGBA32))
		{
			writer.WriteTga(Data, Width, Height, ColorComponents.RedGreenBlueAlpha, stream);
		}
		else if (typeof(TColor) == typeof(ColorRGB24))
		{
			writer.WriteTga(Data, Width, Height, ColorComponents.RedGreenBlue, stream);
		}
		else
		{
			RgbConverter.Convert<TColor, TColorArg, ColorRGBA32, byte>(Bits, Width, Height, out byte[] output);
			writer.WriteTga(output, Width, Height, ColorComponents.RedGreenBlueAlpha, stream);
		}
	}

	public int Height { get; }
	public int Width { get; }
	public int ByteSize => CalculateByteSize(Width, Height);
	public Span<byte> Bits => new Span<byte>(Data, 0, ByteSize);
	public Span<TColor> Pixels => MemoryMarshal.Cast<byte, TColor>(Bits);
	private byte[] Data { get; }

	private static int CalculateByteSize(int width, int height)
	{
		return width * height * Unsafe.SizeOf<TColor>();
	}
}
