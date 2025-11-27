using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using BenchmarkDotNet.Attributes;

namespace AssetRipper.TextureDecoder.Benchmarks;

public class Argb32Decoding
{
	private const int Width = 256;//ignoring smaller mips
	private const int Height = 256;//ignoring smaller mips
	private readonly byte[] inputData;
	private readonly byte[] outputData;

	public Argb32Decoding()
	{
		inputData = File.ReadAllBytes(PathConstants.PathToTestFiles + "Rgb/" + "test.argb32");
		outputData = new byte[Width * Height * sizeof(uint)];
	}

	[Benchmark]
	public int OriginalMethod() => RgbConverter.ARGB32ToBGRA32(inputData, Width, Height, outputData);

	[Benchmark]
	public int GenericMethod() => RgbConverter.Convert<ColorARGB<byte>, byte, ColorBGRA<byte>, byte>(inputData, Width, Height, outputData);
}
