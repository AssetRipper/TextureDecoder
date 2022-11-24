using AssetRipper.TextureDecoder.Astc;
using AssetRipper.TextureDecoder.Atc;
using AssetRipper.TextureDecoder.Bc;
using AssetRipper.TextureDecoder.Dxt;
using AssetRipper.TextureDecoder.Etc;
using AssetRipper.TextureDecoder.Pvrtc;
using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using AssetRipper.TextureDecoder.Yuy2;

namespace AssetRipper.TextureDecoder.ConsoleApp;

internal static partial class Program
{
	static void Main(string[] args)
	{
		if (args.Length < 4)
		{
			Console.WriteLine("Format: {path} {inputType} {outputType} {width} {height} {args5} {args6}");
			return;
		}
		string path = args[0];
		string inputType = args[1].ToLowerInvariant();
		string outputType = args[2].ToLowerInvariant();
		int width = int.Parse(args[3]);
		int height = int.Parse(args[4]);
		string args5 = args.GetArgument(5);
		string args6 = args.GetArgument(6);
		byte[] data = File.ReadAllBytes(path);

		DirectBitmap<ColorBGRA32, byte> bitmap = new DirectBitmap<ColorBGRA32, byte>(width, height);
		switch (inputType)
		{
			case "astc":
				DecodeAstc(data, width, height, args5, args6, bitmap.Bits);
				break;
			case "atc":
				DecodeAtc(data, width, height, args5, bitmap.Bits);
				break;
			case "bc":
				DecodeBc(data, width, height, args5, args6, bitmap.Bits);
				break;
			case "dxt":
				DecodeDxt(data, width, height, args5, bitmap.Bits);
				break;
			case "etc":
				DecodeEtc(data, width, height, args5, bitmap.Bits);
				break;
			case "pvrtc":
				DecodePvrtc(data, width, height, args5, bitmap.Bits);
				break;
			case "rgb":
				DecodeRgb(data, width, height, args5, bitmap.Bits);
				break;
			case "yuy2":
				Yuy2Decoder.DecompressYUY2(data, width, height, bitmap.Bits);
				break;
			default:
				throw new NotSupportedException(inputType);
		}

		string dirPath = Path.GetDirectoryName(path) ?? Environment.CurrentDirectory;
		string name = Path.GetFileName(path);
		
		switch (outputType)
		{
			case "png":
				{
					string newPath = Path.Combine(dirPath, name + ".png");
					bitmap.FlipY();
					bitmap.SaveAsPng(newPath);
				}
				break;
			case "bgra":
				{
					string newPath = Path.Combine(dirPath, name + ".bgra");
					WriteAllBytes(newPath, bitmap.Bits);
				}
				break;
			default:
				throw new NotSupportedException(outputType);
		}

		Console.WriteLine("Done!");
	}

	private static string GetArgument(this string[] args, int index)
	{
		return index < args.Length ? args[index] : string.Empty;
	}

	private static void DecodeAstc(ReadOnlySpan<byte> input, int width, int height, string blockXSizeString, string blockYSizeString, Span<byte> output)
	{
		Console.WriteLine("Arg at index 5 : blockXSize");
		Console.WriteLine("Arg at index 6 : blockYSize");
		int blockXSize = int.Parse(blockXSizeString);
		int blockYSize = int.Parse(blockYSizeString);
		AstcDecoder.DecodeASTC(input, width, height, blockXSize, blockYSize, output);
	}

	private static void DecodeAtc(ReadOnlySpan<byte> input, int width, int height, string modeString, Span<byte> output)
	{
		Console.WriteLine("Arg at index 5 : mode");
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

	private static void DecodeBc(ReadOnlySpan<byte> input, int width, int height, string modeString, string isSignedString, Span<byte> output)
	{
		Console.WriteLine("Arg at index 5 : mode");
		Console.WriteLine("  1 - BC1");
		Console.WriteLine("  2 - BC2");
		Console.WriteLine("  3 - BC3");
		Console.WriteLine("  4 - BC4");
		Console.WriteLine("  5 - BC5");
		Console.WriteLine("  6 - BC6H");
		Console.WriteLine("  7 - BC7");
		Console.WriteLine("Arg at index 6 : isSigned (BC6H only)");
		int mode = int.Parse(modeString);
		switch (mode)
		{
			case 1:
				BcDecoder.DecompressBC1(input, width, height, output);
				break;
			case 2:
				BcDecoder.DecompressBC2(input, width, height, output);
				break;
			case 3:
				BcDecoder.DecompressBC3(input, width, height, output);
				break;
			case 4:
				BcDecoder.DecompressBC4(input, width, height, output);
				break;
			case 5:
				BcDecoder.DecompressBC5(input, width, height, output);
				break;
			case 6:
				BcDecoder.DecompressBC6H(input, width, height, bool.Parse(isSignedString), output);
				break;
			case 7:
				BcDecoder.DecompressBC7(input, width, height, output);
				break;

			default:
				throw new NotSupportedException(mode.ToString());
		}
	}

	private static void DecodeDxt(ReadOnlySpan<byte> input, int width, int height, string modeString, Span<byte> output)
	{
		Console.WriteLine("Arg at index 5 : mode");
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
		Console.WriteLine("Arg at index 5 : mode");
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
		Console.WriteLine("Arg at index 5 : 2bitMode");
		bool do2bit = bool.Parse(do2bitModeString);
		PvrtcDecoder.DecompressPVRTC(input, width, height, do2bit, output);
	}

	public static void WriteAllBytes(string path, ReadOnlySpan<byte> bytes)
	{
		ArgumentException.ThrowIfNullOrEmpty(path, nameof(path));
		using Microsoft.Win32.SafeHandles.SafeFileHandle sfh = File.OpenHandle(path, FileMode.Create, FileAccess.Write, FileShare.Read);
		RandomAccess.Write(sfh, bytes, 0);
	}
}