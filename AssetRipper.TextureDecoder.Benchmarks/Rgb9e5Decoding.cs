using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using BenchmarkDotNet.Attributes;

namespace AssetRipper.TextureDecoder.Benchmarks;

public class Rgb9e5Decoding
{
	private const int Width = 256;//ignoring smaller mips
	private const int Height = 256;//ignoring smaller mips
	private readonly byte[] inputData;
	private readonly byte[] outputData;

	public Rgb9e5Decoding()
	{
		inputData = File.ReadAllBytes(PathConstants.PathToTestFiles + "Rgb/" + "test.rgb9e5float");
		outputData = new byte[Width * Height * sizeof(uint)];
	}

	[Benchmark]
	public int OriginalMethod() => RgbConverter.RGB9e5FloatToBGRA32(inputData, Width, Height, outputData);

	[Benchmark]
	public int GenericMethod() => RgbConverter.Convert<ColorRGB9e5, double, ColorBGRA32, byte>(inputData, Width, Height, outputData);
}
