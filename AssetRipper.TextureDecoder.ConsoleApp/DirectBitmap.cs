using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using StbImageWriteSharp;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AssetRipper.TextureDecoder.ConsoleApp;

public readonly struct DirectBitmap<TColor, TColorArg>
	where TColorArg : unmanaged
	where TColor : unmanaged, IColor<TColorArg>
{
	private static readonly ImageWriter imageWriter = new();
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
		GetDataAndComponentsForSaving(out byte[] data, out ColorComponents components);
		using Stream stream = File.OpenWrite(path);
		lock (imageWriter)
		{
			imageWriter.WriteBmp(data, Width, Height, components, stream);
		}
	}

	public void SaveAsHdr(string path)
	{
		GetDataAndComponentsForSaving(out byte[] data, out ColorComponents components);
		using Stream stream = File.OpenWrite(path);
		lock (imageWriter)
		{
			imageWriter.WriteHdr(data, Width, Height, components, stream);
		}
	}

	public void SaveAsJpg(string path)
	{
		GetDataAndComponentsForSaving(out byte[] data, out ColorComponents components);
		using Stream stream = File.OpenWrite(path);
		lock (imageWriter)
		{
			imageWriter.WriteJpg(data, Width, Height, components, stream, default);
		}
	}

	public void SaveAsPng(string path)
	{
		GetDataAndComponentsForSaving(out byte[] data, out ColorComponents components);
		using Stream stream = File.OpenWrite(path);
		lock (imageWriter)
		{
			imageWriter.WritePng(data, Width, Height, components, stream);
		}
	}

	public void SaveAsTga(string path)
	{
		GetDataAndComponentsForSaving(out byte[] data, out ColorComponents components);
		using Stream stream = File.OpenWrite(path);
		lock (imageWriter)
		{
			imageWriter.WriteTga(data, Width, Height, components, stream);
		}
	}

	private void GetDataAndComponentsForSaving(out byte[] data, out ColorComponents components)
	{
		if (typeof(TColor) == typeof(ColorRGBA<byte>))
		{
			data = Data;
			components = ColorComponents.RedGreenBlueAlpha;
		}
		else if (typeof(TColor) == typeof(ColorRGB<byte>))
		{
			data = Data;
			components = ColorComponents.RedGreenBlue;
		}
		else if (TColor.HasAlphaChannel)
		{
			RgbConverter.Convert<TColor, TColorArg, ColorRGBA<byte>, byte>(Bits, Width, Height, out data);
			components = ColorComponents.RedGreenBlueAlpha;
		}
		else
		{
			RgbConverter.Convert<TColor, TColorArg, ColorRGB<byte>, byte>(Bits, Width, Height, out data);
			components = ColorComponents.RedGreenBlue;
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
