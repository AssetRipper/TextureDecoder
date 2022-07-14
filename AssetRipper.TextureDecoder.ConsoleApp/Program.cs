using AssetRipper.TextureDecoder.Astc;
using AssetRipper.TextureDecoder.Atc;
using AssetRipper.TextureDecoder.Dxt;
using AssetRipper.TextureDecoder.Etc;
using AssetRipper.TextureDecoder.Pvrtc;
using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Yuy2;

namespace AssetRipper.TextureDecoder.ConsoleApp;

internal static class Program
{
	static void Main(string[] args)
	{
		if (args.Length < 4)
		{
			Console.WriteLine("Format: {path} {type} {width} {height} {args4} {args5}");
			return;
		}
		string path = args[0];
		string type = args[1].ToLowerInvariant();
		int width = int.Parse(args[2]);
		int height = int.Parse(args[3]);
		string args4 = args.GetArgument(4);
		string args5 = args.GetArgument(5);
		byte[] data = File.ReadAllBytes(path);

		DirectBitmap bitmap = new DirectBitmap(width, height);
		switch (type)
		{
			case "astc":
				DecodeAstc(data, width, height, args4, args5, bitmap.Bits);
				break;
			case "atc":
				DecodeAtc(data, width, height, args4, bitmap.Bits);
				break;
			case "dxt":
				DecodeDxt(data, width, height, args4, bitmap.Bits);
				break;
			case "etc":
				DecodeEtc(data, width, height, args4, bitmap.Bits);
				break;
			case "pvrtc":
				DecodePvrtc(data, width, height, args4, bitmap.Bits);
				break;
			case "rgb":
				DecodeRgb(data, width, height, args4, bitmap.Bits);
				break;
			case "yuy2":
				Yuy2Decoder.DecompressYUY2(data, width, height, bitmap.Bits);
				break;
			default:
				throw new NotSupportedException(type);
		}

		string dirPath = Path.GetDirectoryName(path) ?? Environment.CurrentDirectory;
		string name = Path.GetFileNameWithoutExtension(path);
		string newPath = Path.Combine(dirPath, name + ".png");
		if (OperatingSystem.IsWindows())
		{
			bitmap.FlipY();
			bitmap.SaveAsPng(newPath);
		}
		else
		{
			throw new NotSupportedException("Only supported on Windows");
		}

		Console.WriteLine("Done!");
	}

	private static string GetArgument(this string[] args, int index)
	{
		return index < args.Length ? args[index] : string.Empty;
	}

	private static void DecodeAstc(ReadOnlySpan<byte> input, int width, int height, string blockXSizeString, string blockYSizeString, Span<byte> output)
	{
		Console.WriteLine("Arg at index 4 : blockXSize");
		Console.WriteLine("Arg at index 5 : blockYSize");
		int blockXSize = int.Parse(blockXSizeString);
		int blockYSize = int.Parse(blockYSizeString);
		AstcDecoder.DecodeASTC(input, width, height, blockXSize, blockYSize, output);
	}

	private static void DecodeAtc(ReadOnlySpan<byte> input, int width, int height, string modeString, Span<byte> output)
	{
		Console.WriteLine("Arg at index 4 : mode");
		Console.WriteLine("  0 - ATC RGB4");
		Console.WriteLine("  1 - ATC RGBA8");
		int mode = int.Parse(modeString);
		switch (mode)
		{
			case 0:
				AtcDecoder.DecompressAtcRgb4(input, width, height, output);
				break;
			case 1:
				AtcDecoder.DecompressAtcRgba8(input, width, height, output);
				break;

			default:
				throw new NotSupportedException(mode.ToString());
		}
	}

	private static void DecodeDxt(ReadOnlySpan<byte> input, int width, int height, string modeString, Span<byte> output)
	{
		Console.WriteLine("Arg at index 4 : mode");
		Console.WriteLine("  0 - DXT1");
		Console.WriteLine("  1 - DXT3");
		Console.WriteLine("  2 - DXT5");
		int mode = int.Parse(modeString);
		switch (mode)
		{
			case 0:
				DxtDecoder.DecompressDXT1(input, width, height, output);
				break;
			case 1:
				DxtDecoder.DecompressDXT3(input, width, height, output);
				break;
			case 2:
				DxtDecoder.DecompressDXT5(input, width, height, output);
				break;

			default:
				throw new NotSupportedException(mode.ToString());
		}
	}

	private static void DecodeEtc(ReadOnlySpan<byte> input, int width, int height, string modeString, Span<byte> output)
	{
		Console.WriteLine("Arg at index 4 : mode");
		Console.WriteLine("  0 - ETC");
		Console.WriteLine("  1 - ETC2");
		Console.WriteLine("  2 - ETC2a1");
		Console.WriteLine("  3 - ETC2a8");
		Console.WriteLine("  4 - EAC R");
		Console.WriteLine("  5 - EAC signed R");
		Console.WriteLine("  6 - EAC RG");
		Console.WriteLine("  7 - EAC signed RG");
		int mode = int.Parse(modeString);
		switch (mode)
		{
			case 0:
				EtcDecoder.DecompressETC(input, width, height, output);
				break;
			case 1:
				EtcDecoder.DecompressETC2(input, width, height, output);
				break;
			case 2:
				EtcDecoder.DecompressETC2A1(input, width, height, output);
				break;
			case 3:
				EtcDecoder.DecompressETC2A8(input, width, height, output);
				break;
			case 4:
				EtcDecoder.DecompressEACRUnsigned(input, width, height, output);
				break;
			case 5:
				EtcDecoder.DecompressEACRSigned(input, width, height, output);
				break;
			case 6:
				EtcDecoder.DecompressEACRGUnsigned(input, width, height, output);
				break;
			case 7:
				EtcDecoder.DecompressEACRGSigned(input, width, height, output);
				break;

			default:
				throw new NotSupportedException(mode.ToString());
		}
	}

	private static void DecodePvrtc(ReadOnlySpan<byte> input, int width, int height, string do2bitModeString, Span<byte> output)
	{
		Console.WriteLine("Arg at index 4 : 2bitMode");
		bool do2bit = bool.Parse(do2bitModeString);
		PvrtcDecoder.DecompressPVRTC(input, width, height, do2bit, output);
	}

	private static void DecodeRgb(ReadOnlySpan<byte> input, int width, int height, string modeString, Span<byte> output)
	{
		Console.WriteLine("Arg at index 4 : mode");
		Console.WriteLine("  0 - Alpha8");
		Console.WriteLine("  1 - Argb4444");
		Console.WriteLine("  2 - Rgb24");
		Console.WriteLine("  3 - Rgba32");
		Console.WriteLine("  4 - Argb32");
		Console.WriteLine("  5 - Rbg16");
		Console.WriteLine("  6 - R16");
		Console.WriteLine("  7 - Rgba4444");
		Console.WriteLine("  8 - Bgra32");
		Console.WriteLine("  9 - Rg16");
		Console.WriteLine("  10 - R8");
		Console.WriteLine("  11 - RHalf");
		Console.WriteLine("  12 - RGHalf");
		Console.WriteLine("  13 - RGBAHalf");
		Console.WriteLine("  14 - RFloat");
		Console.WriteLine("  15 - RGFloat");
		Console.WriteLine("  16 - RGBAFloat");
		Console.WriteLine("  17 - RGB9e5Float");
		Console.WriteLine("  18 - RG32");
		Console.WriteLine("  19 - RGB48");
		Console.WriteLine("  20 - RGBA64");
		int mode = int.Parse(modeString);
		switch (mode)
		{
			case 0:
				RgbConverter.A8ToBGRA32(input, width, height, output);
				break;
			case 1:
				RgbConverter.ARGB16ToBGRA32(input, width, height, output);
				break;
			case 2:
				RgbConverter.RGB24ToBGRA32(input, width, height, output);
				break;
			case 3:
				RgbConverter.RGBA32ToBGRA32(input, width, height, output);
				break;
			case 4:
				RgbConverter.ARGB32ToBGRA32(input, width, height, output);
				break;
			case 5:
				RgbConverter.RGB16ToBGRA32(input, width, height, output);
				break;
			case 6:
				RgbConverter.R16ToBGRA32(input, width, height, output);
				break;
			case 7:
				RgbConverter.RGBA16ToBGRA32(input, width, height, output);
				break;
			case 8:
				input.CopyTo(output);
				break;
			case 9:
				RgbConverter.RG16ToBGRA32(input, width, height, output);
				break;
			case 10:
				RgbConverter.R8ToBGRA32(input, width, height, output);
				break;
			case 11:
				RgbConverter.RHalfToBGRA32(input, width, height, output);
				break;
			case 12:
				RgbConverter.RGHalfToBGRA32(input, width, height, output);
				break;
			case 13:
				RgbConverter.RGBAHalfToBGRA32(input, width, height, output);
				break;
			case 14:
				RgbConverter.RFloatToBGRA32(input, width, height, output);
				break;
			case 15:
				RgbConverter.RGFloatToBGRA32(input, width, height, output);
				break;
			case 16:
				RgbConverter.RGBAFloatToBGRA32(input, width, height, output);
				break;
			case 17:
				RgbConverter.RGB9e5FloatToBGRA32(input, width, height, output);
				break;
			case 18:
				RgbConverter.RG32ToBGRA32(input, width, height, output);
				break;
			case 19:
				RgbConverter.RGB48ToBGRA32(input, width, height, output);
				break;
			case 20:
				RgbConverter.RGBA64ToBGRA32(input, width, height, output);
				break;

			default:
				throw new NotSupportedException(mode.ToString());
		}
	}
}