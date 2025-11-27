// Auto-generated code. Do not modify manually.

using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Channels;
using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Runtime.InteropServices;

namespace AssetRipper.TextureDecoder.ConsoleApp;
static partial class Program
{
	private static void DecodeRgb(ReadOnlySpan<byte> input, int width, int height, string modeString, Span<byte> output)
	{
		Console.WriteLine("Arg at index 5 : mode");
		Console.WriteLine("  0 - R_SByte");
		Console.WriteLine("  1 - RG_SByte");
		Console.WriteLine("  2 - RGB_SByte");
		Console.WriteLine("  3 - RGBA_SByte");
		Console.WriteLine("  4 - A_SByte");
		Console.WriteLine("  5 - ARGB_SByte");
		Console.WriteLine("  6 - BGRA_SByte");
		Console.WriteLine("  7 - R_Byte");
		Console.WriteLine("  8 - RG_Byte");
		Console.WriteLine("  9 - RGB_Byte");
		Console.WriteLine("  10 - RGBA_Byte");
		Console.WriteLine("  11 - A_Byte");
		Console.WriteLine("  12 - ARGB_Byte");
		Console.WriteLine("  13 - BGRA_Byte");
		Console.WriteLine("  14 - R_Int16");
		Console.WriteLine("  15 - RG_Int16");
		Console.WriteLine("  16 - RGB_Int16");
		Console.WriteLine("  17 - RGBA_Int16");
		Console.WriteLine("  18 - A_Int16");
		Console.WriteLine("  19 - ARGB_Int16");
		Console.WriteLine("  20 - BGRA_Int16");
		Console.WriteLine("  21 - R_UInt16");
		Console.WriteLine("  22 - RG_UInt16");
		Console.WriteLine("  23 - RGB_UInt16");
		Console.WriteLine("  24 - RGBA_UInt16");
		Console.WriteLine("  25 - A_UInt16");
		Console.WriteLine("  26 - ARGB_UInt16");
		Console.WriteLine("  27 - BGRA_UInt16");
		Console.WriteLine("  28 - R_Int32");
		Console.WriteLine("  29 - RG_Int32");
		Console.WriteLine("  30 - RGB_Int32");
		Console.WriteLine("  31 - RGBA_Int32");
		Console.WriteLine("  32 - A_Int32");
		Console.WriteLine("  33 - ARGB_Int32");
		Console.WriteLine("  34 - BGRA_Int32");
		Console.WriteLine("  35 - R_UInt32");
		Console.WriteLine("  36 - RG_UInt32");
		Console.WriteLine("  37 - RGB_UInt32");
		Console.WriteLine("  38 - RGBA_UInt32");
		Console.WriteLine("  39 - A_UInt32");
		Console.WriteLine("  40 - ARGB_UInt32");
		Console.WriteLine("  41 - BGRA_UInt32");
		Console.WriteLine("  42 - R_IntPtr");
		Console.WriteLine("  43 - RG_IntPtr");
		Console.WriteLine("  44 - RGB_IntPtr");
		Console.WriteLine("  45 - RGBA_IntPtr");
		Console.WriteLine("  46 - A_IntPtr");
		Console.WriteLine("  47 - ARGB_IntPtr");
		Console.WriteLine("  48 - BGRA_IntPtr");
		Console.WriteLine("  49 - R_UIntPtr");
		Console.WriteLine("  50 - RG_UIntPtr");
		Console.WriteLine("  51 - RGB_UIntPtr");
		Console.WriteLine("  52 - RGBA_UIntPtr");
		Console.WriteLine("  53 - A_UIntPtr");
		Console.WriteLine("  54 - ARGB_UIntPtr");
		Console.WriteLine("  55 - BGRA_UIntPtr");
		Console.WriteLine("  56 - R_Int64");
		Console.WriteLine("  57 - RG_Int64");
		Console.WriteLine("  58 - RGB_Int64");
		Console.WriteLine("  59 - RGBA_Int64");
		Console.WriteLine("  60 - A_Int64");
		Console.WriteLine("  61 - ARGB_Int64");
		Console.WriteLine("  62 - BGRA_Int64");
		Console.WriteLine("  63 - R_UInt64");
		Console.WriteLine("  64 - RG_UInt64");
		Console.WriteLine("  65 - RGB_UInt64");
		Console.WriteLine("  66 - RGBA_UInt64");
		Console.WriteLine("  67 - A_UInt64");
		Console.WriteLine("  68 - ARGB_UInt64");
		Console.WriteLine("  69 - BGRA_UInt64");
		Console.WriteLine("  70 - R_Int128");
		Console.WriteLine("  71 - RG_Int128");
		Console.WriteLine("  72 - RGB_Int128");
		Console.WriteLine("  73 - RGBA_Int128");
		Console.WriteLine("  74 - A_Int128");
		Console.WriteLine("  75 - ARGB_Int128");
		Console.WriteLine("  76 - BGRA_Int128");
		Console.WriteLine("  77 - R_UInt128");
		Console.WriteLine("  78 - RG_UInt128");
		Console.WriteLine("  79 - RGB_UInt128");
		Console.WriteLine("  80 - RGBA_UInt128");
		Console.WriteLine("  81 - A_UInt128");
		Console.WriteLine("  82 - ARGB_UInt128");
		Console.WriteLine("  83 - BGRA_UInt128");
		Console.WriteLine("  84 - R_Half");
		Console.WriteLine("  85 - RG_Half");
		Console.WriteLine("  86 - RGB_Half");
		Console.WriteLine("  87 - RGBA_Half");
		Console.WriteLine("  88 - A_Half");
		Console.WriteLine("  89 - ARGB_Half");
		Console.WriteLine("  90 - BGRA_Half");
		Console.WriteLine("  91 - R_Single");
		Console.WriteLine("  92 - RG_Single");
		Console.WriteLine("  93 - RGB_Single");
		Console.WriteLine("  94 - RGBA_Single");
		Console.WriteLine("  95 - A_Single");
		Console.WriteLine("  96 - ARGB_Single");
		Console.WriteLine("  97 - BGRA_Single");
		Console.WriteLine("  98 - R_NFloat");
		Console.WriteLine("  99 - RG_NFloat");
		Console.WriteLine("  100 - RGB_NFloat");
		Console.WriteLine("  101 - RGBA_NFloat");
		Console.WriteLine("  102 - A_NFloat");
		Console.WriteLine("  103 - ARGB_NFloat");
		Console.WriteLine("  104 - BGRA_NFloat");
		Console.WriteLine("  105 - R_Double");
		Console.WriteLine("  106 - RG_Double");
		Console.WriteLine("  107 - RGB_Double");
		Console.WriteLine("  108 - RGBA_Double");
		Console.WriteLine("  109 - A_Double");
		Console.WriteLine("  110 - ARGB_Double");
		Console.WriteLine("  111 - BGRA_Double");
		Console.WriteLine("  112 - R_Decimal");
		Console.WriteLine("  113 - RG_Decimal");
		Console.WriteLine("  114 - RGB_Decimal");
		Console.WriteLine("  115 - RGBA_Decimal");
		Console.WriteLine("  116 - A_Decimal");
		Console.WriteLine("  117 - ARGB_Decimal");
		Console.WriteLine("  118 - BGRA_Decimal");
		Console.WriteLine("  119 - ARGB16");
		Console.WriteLine("  120 - RGB16");
		Console.WriteLine("  121 - RGB9e5");
		Console.WriteLine("  122 - RGBA16");
		int mode = int.Parse(modeString);
		switch(mode)
		{
			case 0:
				RgbConverter.Convert<ColorR<sbyte>, sbyte, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 1:
				RgbConverter.Convert<ColorRG<sbyte>, sbyte, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 2:
				RgbConverter.Convert<ColorRGB<sbyte>, sbyte, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 3:
				RgbConverter.Convert<ColorRGBA<sbyte>, sbyte, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 4:
				RgbConverter.Convert<ColorA<sbyte>, sbyte, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 5:
				RgbConverter.Convert<ColorARGB<sbyte>, sbyte, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 6:
				RgbConverter.Convert<ColorBGRA<sbyte>, sbyte, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 7:
				RgbConverter.Convert<ColorR<byte>, byte, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 8:
				RgbConverter.Convert<ColorRG<byte>, byte, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 9:
				RgbConverter.Convert<ColorRGB<byte>, byte, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 10:
				RgbConverter.Convert<ColorRGBA<byte>, byte, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 11:
				RgbConverter.Convert<ColorA<byte>, byte, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 12:
				RgbConverter.Convert<ColorARGB<byte>, byte, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 13:
				RgbConverter.Convert<ColorBGRA<byte>, byte, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 14:
				RgbConverter.Convert<ColorR<short>, short, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 15:
				RgbConverter.Convert<ColorRG<short>, short, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 16:
				RgbConverter.Convert<ColorRGB<short>, short, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 17:
				RgbConverter.Convert<ColorRGBA<short>, short, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 18:
				RgbConverter.Convert<ColorA<short>, short, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 19:
				RgbConverter.Convert<ColorARGB<short>, short, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 20:
				RgbConverter.Convert<ColorBGRA<short>, short, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 21:
				RgbConverter.Convert<ColorR<ushort>, ushort, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 22:
				RgbConverter.Convert<ColorRG<ushort>, ushort, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 23:
				RgbConverter.Convert<ColorRGB<ushort>, ushort, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 24:
				RgbConverter.Convert<ColorRGBA<ushort>, ushort, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 25:
				RgbConverter.Convert<ColorA<ushort>, ushort, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 26:
				RgbConverter.Convert<ColorARGB<ushort>, ushort, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 27:
				RgbConverter.Convert<ColorBGRA<ushort>, ushort, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 28:
				RgbConverter.Convert<ColorR<int>, int, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 29:
				RgbConverter.Convert<ColorRG<int>, int, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 30:
				RgbConverter.Convert<ColorRGB<int>, int, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 31:
				RgbConverter.Convert<ColorRGBA<int>, int, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 32:
				RgbConverter.Convert<ColorA<int>, int, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 33:
				RgbConverter.Convert<ColorARGB<int>, int, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 34:
				RgbConverter.Convert<ColorBGRA<int>, int, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 35:
				RgbConverter.Convert<ColorR<uint>, uint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 36:
				RgbConverter.Convert<ColorRG<uint>, uint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 37:
				RgbConverter.Convert<ColorRGB<uint>, uint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 38:
				RgbConverter.Convert<ColorRGBA<uint>, uint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 39:
				RgbConverter.Convert<ColorA<uint>, uint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 40:
				RgbConverter.Convert<ColorARGB<uint>, uint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 41:
				RgbConverter.Convert<ColorBGRA<uint>, uint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 42:
				RgbConverter.Convert<ColorR<nint>, nint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 43:
				RgbConverter.Convert<ColorRG<nint>, nint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 44:
				RgbConverter.Convert<ColorRGB<nint>, nint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 45:
				RgbConverter.Convert<ColorRGBA<nint>, nint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 46:
				RgbConverter.Convert<ColorA<nint>, nint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 47:
				RgbConverter.Convert<ColorARGB<nint>, nint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 48:
				RgbConverter.Convert<ColorBGRA<nint>, nint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 49:
				RgbConverter.Convert<ColorR<nuint>, nuint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 50:
				RgbConverter.Convert<ColorRG<nuint>, nuint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 51:
				RgbConverter.Convert<ColorRGB<nuint>, nuint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 52:
				RgbConverter.Convert<ColorRGBA<nuint>, nuint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 53:
				RgbConverter.Convert<ColorA<nuint>, nuint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 54:
				RgbConverter.Convert<ColorARGB<nuint>, nuint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 55:
				RgbConverter.Convert<ColorBGRA<nuint>, nuint, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 56:
				RgbConverter.Convert<ColorR<long>, long, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 57:
				RgbConverter.Convert<ColorRG<long>, long, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 58:
				RgbConverter.Convert<ColorRGB<long>, long, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 59:
				RgbConverter.Convert<ColorRGBA<long>, long, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 60:
				RgbConverter.Convert<ColorA<long>, long, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 61:
				RgbConverter.Convert<ColorARGB<long>, long, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 62:
				RgbConverter.Convert<ColorBGRA<long>, long, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 63:
				RgbConverter.Convert<ColorR<ulong>, ulong, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 64:
				RgbConverter.Convert<ColorRG<ulong>, ulong, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 65:
				RgbConverter.Convert<ColorRGB<ulong>, ulong, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 66:
				RgbConverter.Convert<ColorRGBA<ulong>, ulong, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 67:
				RgbConverter.Convert<ColorA<ulong>, ulong, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 68:
				RgbConverter.Convert<ColorARGB<ulong>, ulong, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 69:
				RgbConverter.Convert<ColorBGRA<ulong>, ulong, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 70:
				RgbConverter.Convert<ColorR<Int128>, Int128, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 71:
				RgbConverter.Convert<ColorRG<Int128>, Int128, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 72:
				RgbConverter.Convert<ColorRGB<Int128>, Int128, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 73:
				RgbConverter.Convert<ColorRGBA<Int128>, Int128, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 74:
				RgbConverter.Convert<ColorA<Int128>, Int128, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 75:
				RgbConverter.Convert<ColorARGB<Int128>, Int128, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 76:
				RgbConverter.Convert<ColorBGRA<Int128>, Int128, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 77:
				RgbConverter.Convert<ColorR<UInt128>, UInt128, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 78:
				RgbConverter.Convert<ColorRG<UInt128>, UInt128, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 79:
				RgbConverter.Convert<ColorRGB<UInt128>, UInt128, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 80:
				RgbConverter.Convert<ColorRGBA<UInt128>, UInt128, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 81:
				RgbConverter.Convert<ColorA<UInt128>, UInt128, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 82:
				RgbConverter.Convert<ColorARGB<UInt128>, UInt128, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 83:
				RgbConverter.Convert<ColorBGRA<UInt128>, UInt128, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 84:
				RgbConverter.Convert<ColorR<Half>, Half, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 85:
				RgbConverter.Convert<ColorRG<Half>, Half, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 86:
				RgbConverter.Convert<ColorRGB<Half>, Half, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 87:
				RgbConverter.Convert<ColorRGBA<Half>, Half, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 88:
				RgbConverter.Convert<ColorA<Half>, Half, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 89:
				RgbConverter.Convert<ColorARGB<Half>, Half, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 90:
				RgbConverter.Convert<ColorBGRA<Half>, Half, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 91:
				RgbConverter.Convert<ColorR<float>, float, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 92:
				RgbConverter.Convert<ColorRG<float>, float, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 93:
				RgbConverter.Convert<ColorRGB<float>, float, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 94:
				RgbConverter.Convert<ColorRGBA<float>, float, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 95:
				RgbConverter.Convert<ColorA<float>, float, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 96:
				RgbConverter.Convert<ColorARGB<float>, float, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 97:
				RgbConverter.Convert<ColorBGRA<float>, float, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 98:
				RgbConverter.Convert<ColorR<NFloat>, NFloat, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 99:
				RgbConverter.Convert<ColorRG<NFloat>, NFloat, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 100:
				RgbConverter.Convert<ColorRGB<NFloat>, NFloat, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 101:
				RgbConverter.Convert<ColorRGBA<NFloat>, NFloat, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 102:
				RgbConverter.Convert<ColorA<NFloat>, NFloat, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 103:
				RgbConverter.Convert<ColorARGB<NFloat>, NFloat, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 104:
				RgbConverter.Convert<ColorBGRA<NFloat>, NFloat, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 105:
				RgbConverter.Convert<ColorR<double>, double, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 106:
				RgbConverter.Convert<ColorRG<double>, double, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 107:
				RgbConverter.Convert<ColorRGB<double>, double, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 108:
				RgbConverter.Convert<ColorRGBA<double>, double, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 109:
				RgbConverter.Convert<ColorA<double>, double, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 110:
				RgbConverter.Convert<ColorARGB<double>, double, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 111:
				RgbConverter.Convert<ColorBGRA<double>, double, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 112:
				RgbConverter.Convert<ColorR<decimal>, decimal, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 113:
				RgbConverter.Convert<ColorRG<decimal>, decimal, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 114:
				RgbConverter.Convert<ColorRGB<decimal>, decimal, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 115:
				RgbConverter.Convert<ColorRGBA<decimal>, decimal, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 116:
				RgbConverter.Convert<ColorA<decimal>, decimal, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 117:
				RgbConverter.Convert<ColorARGB<decimal>, decimal, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 118:
				RgbConverter.Convert<ColorBGRA<decimal>, decimal, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 119:
				RgbConverter.Convert<ColorARGB16, byte, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 120:
				RgbConverter.Convert<ColorRGB16, byte, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 121:
				RgbConverter.Convert<ColorRGB9e5, double, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			case 122:
				RgbConverter.Convert<ColorRGBA16, byte, Color<byte, B, G, R, A>, byte>(input, width, height, output);
				break;
			default:
				throw new NotSupportedException(mode.ToString());
		}
	}
}
