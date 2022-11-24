//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;

namespace AssetRipper.TextureDecoder.ConsoleApp;
static partial class Program
{
	private static void DecodeRgb(ReadOnlySpan<byte> input, int width, int height, string modeString, Span<byte> output)
	{
		Console.WriteLine("Arg at index 5 : mode");
		Console.WriteLine("  0 - A8");
		Console.WriteLine("  1 - ARGB16");
		Console.WriteLine("  2 - ARGB32");
		Console.WriteLine("  3 - BGRA32");
		Console.WriteLine("  4 - R16");
		Console.WriteLine("  5 - R16Half");
		Console.WriteLine("  6 - R16Signed");
		Console.WriteLine("  7 - R32Single");
		Console.WriteLine("  8 - R8");
		Console.WriteLine("  9 - R8Signed");
		Console.WriteLine("  10 - RG16");
		Console.WriteLine("  11 - RG32Half");
		Console.WriteLine("  12 - RG16Signed");
		Console.WriteLine("  13 - RG32");
		Console.WriteLine("  14 - RG32Signed");
		Console.WriteLine("  15 - RG64Single");
		Console.WriteLine("  16 - RGB16");
		Console.WriteLine("  17 - RGB24");
		Console.WriteLine("  18 - RGB24Signed");
		Console.WriteLine("  19 - RGB32Half");
		Console.WriteLine("  20 - RGB48");
		Console.WriteLine("  21 - RGB48Half");
		Console.WriteLine("  22 - RGB48Signed");
		Console.WriteLine("  23 - RGB96Single");
		Console.WriteLine("  24 - RGB9e5");
		Console.WriteLine("  25 - RGBA128Single");
		Console.WriteLine("  26 - RGBA16");
		Console.WriteLine("  27 - RGBA32");
		Console.WriteLine("  28 - RGBA32Signed");
		Console.WriteLine("  29 - RGBA64");
		Console.WriteLine("  30 - RGBA64Half");
		Console.WriteLine("  31 - RGBA64Signed");
		int mode = int.Parse(modeString);
		switch(mode)
		{
			case 0:
				RgbConverter.Convert<ColorA8, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 1:
				RgbConverter.Convert<ColorARGB16, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 2:
				RgbConverter.Convert<ColorARGB32, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 3:
				RgbConverter.Convert<ColorBGRA32, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 4:
				RgbConverter.Convert<ColorR16, ushort, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 5:
				RgbConverter.Convert<ColorR16Half, Half, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 6:
				RgbConverter.Convert<ColorR16Signed, short, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 7:
				RgbConverter.Convert<ColorR32Single, float, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 8:
				RgbConverter.Convert<ColorR8, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 9:
				RgbConverter.Convert<ColorR8Signed, sbyte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 10:
				RgbConverter.Convert<ColorRG16, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 11:
				RgbConverter.Convert<ColorRG32Half, Half, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 12:
				RgbConverter.Convert<ColorRG16Signed, sbyte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 13:
				RgbConverter.Convert<ColorRG32, ushort, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 14:
				RgbConverter.Convert<ColorRG32Signed, short, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 15:
				RgbConverter.Convert<ColorRG64Single, float, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 16:
				RgbConverter.Convert<ColorRGB16, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 17:
				RgbConverter.Convert<ColorRGB24, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 18:
				RgbConverter.Convert<ColorRGB24Signed, sbyte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 19:
				RgbConverter.Convert<ColorRGB32Half, Half, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 20:
				RgbConverter.Convert<ColorRGB48, ushort, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 21:
				RgbConverter.Convert<ColorRGB48Half, Half, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 22:
				RgbConverter.Convert<ColorRGB48Signed, short, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 23:
				RgbConverter.Convert<ColorRGB96Single, float, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 24:
				RgbConverter.Convert<ColorRGB9e5, double, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 25:
				RgbConverter.Convert<ColorRGBA128Single, float, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 26:
				RgbConverter.Convert<ColorRGBA16, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 27:
				RgbConverter.Convert<ColorRGBA32, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 28:
				RgbConverter.Convert<ColorRGBA32Signed, sbyte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 29:
				RgbConverter.Convert<ColorRGBA64, ushort, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 30:
				RgbConverter.Convert<ColorRGBA64Half, Half, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 31:
				RgbConverter.Convert<ColorRGBA64Signed, short, ColorBGRA32, byte>(input, width, height, output);
				break;
			default:
				throw new NotSupportedException(mode.ToString());
		}
	}
}
