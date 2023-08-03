// Auto-generated code. Do not modify manually.

using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;

namespace AssetRipper.TextureDecoder.ConsoleApp;
static partial class Program
{
	private static void DecodeRgb(ReadOnlySpan<byte> input, int width, int height, string modeString, Span<byte> output)
	{
		Console.WriteLine("Arg at index 5 : mode");
		Console.WriteLine("  0 - R8SByte");
		Console.WriteLine("  1 - RG8SByte");
		Console.WriteLine("  2 - RGB8SByte");
		Console.WriteLine("  3 - RGBA8SByte");
		Console.WriteLine("  4 - A8SByte");
		Console.WriteLine("  5 - R8Byte");
		Console.WriteLine("  6 - RG8Byte");
		Console.WriteLine("  7 - RGB8Byte");
		Console.WriteLine("  8 - RGBA8Byte");
		Console.WriteLine("  9 - A8Byte");
		Console.WriteLine("  10 - R16Int16");
		Console.WriteLine("  11 - RG16Int16");
		Console.WriteLine("  12 - RGB16Int16");
		Console.WriteLine("  13 - RGBA16Int16");
		Console.WriteLine("  14 - A16Int16");
		Console.WriteLine("  15 - R16UInt16");
		Console.WriteLine("  16 - RG16UInt16");
		Console.WriteLine("  17 - RGB16UInt16");
		Console.WriteLine("  18 - RGBA16UInt16");
		Console.WriteLine("  19 - A16UInt16");
		Console.WriteLine("  20 - R32Int32");
		Console.WriteLine("  21 - RG32Int32");
		Console.WriteLine("  22 - RGB32Int32");
		Console.WriteLine("  23 - RGBA32Int32");
		Console.WriteLine("  24 - A32Int32");
		Console.WriteLine("  25 - R32UInt32");
		Console.WriteLine("  26 - RG32UInt32");
		Console.WriteLine("  27 - RGB32UInt32");
		Console.WriteLine("  28 - RGBA32UInt32");
		Console.WriteLine("  29 - A32UInt32");
		Console.WriteLine("  30 - R64Int64");
		Console.WriteLine("  31 - RG64Int64");
		Console.WriteLine("  32 - RGB64Int64");
		Console.WriteLine("  33 - RGBA64Int64");
		Console.WriteLine("  34 - A64Int64");
		Console.WriteLine("  35 - R64UInt64");
		Console.WriteLine("  36 - RG64UInt64");
		Console.WriteLine("  37 - RGB64UInt64");
		Console.WriteLine("  38 - RGBA64UInt64");
		Console.WriteLine("  39 - A64UInt64");
		Console.WriteLine("  40 - R128Int128");
		Console.WriteLine("  41 - RG128Int128");
		Console.WriteLine("  42 - RGB128Int128");
		Console.WriteLine("  43 - RGBA128Int128");
		Console.WriteLine("  44 - A128Int128");
		Console.WriteLine("  45 - R128UInt128");
		Console.WriteLine("  46 - RG128UInt128");
		Console.WriteLine("  47 - RGB128UInt128");
		Console.WriteLine("  48 - RGBA128UInt128");
		Console.WriteLine("  49 - A128UInt128");
		Console.WriteLine("  50 - R16Half");
		Console.WriteLine("  51 - RG16Half");
		Console.WriteLine("  52 - RGB16Half");
		Console.WriteLine("  53 - RGBA16Half");
		Console.WriteLine("  54 - A16Half");
		Console.WriteLine("  55 - R32Single");
		Console.WriteLine("  56 - RG32Single");
		Console.WriteLine("  57 - RGB32Single");
		Console.WriteLine("  58 - RGBA32Single");
		Console.WriteLine("  59 - A32Single");
		Console.WriteLine("  60 - R64Double");
		Console.WriteLine("  61 - RG64Double");
		Console.WriteLine("  62 - RGB64Double");
		Console.WriteLine("  63 - RGBA64Double");
		Console.WriteLine("  64 - A64Double");
		Console.WriteLine("  65 - R128Decimal");
		Console.WriteLine("  66 - RG128Decimal");
		Console.WriteLine("  67 - RGB128Decimal");
		Console.WriteLine("  68 - RGBA128Decimal");
		Console.WriteLine("  69 - A128Decimal");
		Console.WriteLine("  70 - ARGB16");
		Console.WriteLine("  71 - ARGB32");
		Console.WriteLine("  72 - BGRA32");
		Console.WriteLine("  73 - RGB16");
		Console.WriteLine("  74 - RGB9e5");
		Console.WriteLine("  75 - RGBA16");
		int mode = int.Parse(modeString);
		switch(mode)
		{
			case 0:
				RgbConverter.Convert<ColorR<sbyte>, sbyte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 1:
				RgbConverter.Convert<ColorRG<sbyte>, sbyte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 2:
				RgbConverter.Convert<ColorRGB<sbyte>, sbyte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 3:
				RgbConverter.Convert<ColorRGBA<sbyte>, sbyte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 4:
				RgbConverter.Convert<ColorA<sbyte>, sbyte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 5:
				RgbConverter.Convert<ColorR<byte>, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 6:
				RgbConverter.Convert<ColorRG<byte>, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 7:
				RgbConverter.Convert<ColorRGB<byte>, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 8:
				RgbConverter.Convert<ColorRGBA<byte>, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 9:
				RgbConverter.Convert<ColorA<byte>, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 10:
				RgbConverter.Convert<ColorR<short>, short, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 11:
				RgbConverter.Convert<ColorRG<short>, short, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 12:
				RgbConverter.Convert<ColorRGB<short>, short, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 13:
				RgbConverter.Convert<ColorRGBA<short>, short, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 14:
				RgbConverter.Convert<ColorA<short>, short, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 15:
				RgbConverter.Convert<ColorR<ushort>, ushort, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 16:
				RgbConverter.Convert<ColorRG<ushort>, ushort, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 17:
				RgbConverter.Convert<ColorRGB<ushort>, ushort, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 18:
				RgbConverter.Convert<ColorRGBA<ushort>, ushort, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 19:
				RgbConverter.Convert<ColorA<ushort>, ushort, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 20:
				RgbConverter.Convert<ColorR<int>, int, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 21:
				RgbConverter.Convert<ColorRG<int>, int, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 22:
				RgbConverter.Convert<ColorRGB<int>, int, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 23:
				RgbConverter.Convert<ColorRGBA<int>, int, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 24:
				RgbConverter.Convert<ColorA<int>, int, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 25:
				RgbConverter.Convert<ColorR<uint>, uint, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 26:
				RgbConverter.Convert<ColorRG<uint>, uint, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 27:
				RgbConverter.Convert<ColorRGB<uint>, uint, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 28:
				RgbConverter.Convert<ColorRGBA<uint>, uint, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 29:
				RgbConverter.Convert<ColorA<uint>, uint, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 30:
				RgbConverter.Convert<ColorR<long>, long, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 31:
				RgbConverter.Convert<ColorRG<long>, long, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 32:
				RgbConverter.Convert<ColorRGB<long>, long, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 33:
				RgbConverter.Convert<ColorRGBA<long>, long, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 34:
				RgbConverter.Convert<ColorA<long>, long, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 35:
				RgbConverter.Convert<ColorR<ulong>, ulong, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 36:
				RgbConverter.Convert<ColorRG<ulong>, ulong, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 37:
				RgbConverter.Convert<ColorRGB<ulong>, ulong, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 38:
				RgbConverter.Convert<ColorRGBA<ulong>, ulong, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 39:
				RgbConverter.Convert<ColorA<ulong>, ulong, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 40:
				RgbConverter.Convert<ColorR<Int128>, Int128, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 41:
				RgbConverter.Convert<ColorRG<Int128>, Int128, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 42:
				RgbConverter.Convert<ColorRGB<Int128>, Int128, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 43:
				RgbConverter.Convert<ColorRGBA<Int128>, Int128, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 44:
				RgbConverter.Convert<ColorA<Int128>, Int128, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 45:
				RgbConverter.Convert<ColorR<UInt128>, UInt128, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 46:
				RgbConverter.Convert<ColorRG<UInt128>, UInt128, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 47:
				RgbConverter.Convert<ColorRGB<UInt128>, UInt128, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 48:
				RgbConverter.Convert<ColorRGBA<UInt128>, UInt128, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 49:
				RgbConverter.Convert<ColorA<UInt128>, UInt128, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 50:
				RgbConverter.Convert<ColorR<Half>, Half, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 51:
				RgbConverter.Convert<ColorRG<Half>, Half, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 52:
				RgbConverter.Convert<ColorRGB<Half>, Half, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 53:
				RgbConverter.Convert<ColorRGBA<Half>, Half, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 54:
				RgbConverter.Convert<ColorA<Half>, Half, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 55:
				RgbConverter.Convert<ColorR<float>, float, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 56:
				RgbConverter.Convert<ColorRG<float>, float, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 57:
				RgbConverter.Convert<ColorRGB<float>, float, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 58:
				RgbConverter.Convert<ColorRGBA<float>, float, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 59:
				RgbConverter.Convert<ColorA<float>, float, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 60:
				RgbConverter.Convert<ColorR<double>, double, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 61:
				RgbConverter.Convert<ColorRG<double>, double, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 62:
				RgbConverter.Convert<ColorRGB<double>, double, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 63:
				RgbConverter.Convert<ColorRGBA<double>, double, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 64:
				RgbConverter.Convert<ColorA<double>, double, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 65:
				RgbConverter.Convert<ColorR<decimal>, decimal, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 66:
				RgbConverter.Convert<ColorRG<decimal>, decimal, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 67:
				RgbConverter.Convert<ColorRGB<decimal>, decimal, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 68:
				RgbConverter.Convert<ColorRGBA<decimal>, decimal, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 69:
				RgbConverter.Convert<ColorA<decimal>, decimal, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 70:
				RgbConverter.Convert<ColorARGB16, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 71:
				RgbConverter.Convert<ColorARGB32, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 72:
				RgbConverter.Convert<ColorBGRA32, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 73:
				RgbConverter.Convert<ColorRGB16, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 74:
				RgbConverter.Convert<ColorRGB9e5, double, ColorBGRA32, byte>(input, width, height, output);
				break;
			case 75:
				RgbConverter.Convert<ColorRGBA16, byte, ColorBGRA32, byte>(input, width, height, output);
				break;
			default:
				throw new NotSupportedException(mode.ToString());
		}
	}
}
