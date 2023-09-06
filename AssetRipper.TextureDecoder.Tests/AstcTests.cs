using AssetRipper.TextureDecoder.Astc;

namespace AssetRipper.TextureDecoder.Tests;

public sealed class AstcTests
{
	private const double MaxMeanDeviation4x4 = 0.2;
	private const double MaxStandardDeviation4x4 = 1;

	private const double MaxMeanDeviation5x5 = 0.2;
	private const double MaxStandardDeviation5x5 = 1;

	private const double MaxMeanDeviation6x6 = 0.2;
	private const double MaxStandardDeviation6x6 = 1;

	private const double MaxMeanDeviation8x8 = 0.2;
	private const double MaxStandardDeviation8x8 = 2;

	private const double MaxMeanDeviation10x10 = 0.2;
	private const double MaxStandardDeviation10x10 = 2.5;

	private const double MaxMeanDeviation12x12 = 0.3;
	private const double MaxStandardDeviation12x12 = 3;

	[Test]
	public void DecompressAstc4x4() => AssertCorrectDecompression<AndroidTextures.Logo_06>(4, 4, MaxMeanDeviation4x4, MaxStandardDeviation4x4);

	[Test]
	public void DecompressAstc5x5() => AssertCorrectDecompression<AndroidTextures.Logo_05>(5, 5, MaxMeanDeviation5x5, MaxStandardDeviation5x5);

	[Test]
	public void DecompressAstc6x6() => AssertCorrectDecompression<AndroidTextures.Logo_04>(6, 6, MaxMeanDeviation6x6, MaxStandardDeviation6x6);

	[Test]
	public void DecompressAstc8x8() => AssertCorrectDecompression<AndroidTextures.Logo_03>(8, 8, MaxMeanDeviation8x8, MaxStandardDeviation8x8);

	[Test]
	public void DecompressAstc10x10() => AssertCorrectDecompression<AndroidTextures.Logo_02>(10, 10, MaxMeanDeviation10x10, MaxStandardDeviation10x10);

	[Test]
	public void DecompressAstc12x12() => AssertCorrectDecompression<AndroidTextures.Logo_01>(12, 12, MaxMeanDeviation12x12, MaxStandardDeviation12x12);

	[Test]
	[Ignore("High dynamic range textures are not supported yet.")]
	public void DecompressAstc4x4HDR() => AssertCorrectDecompression<AndroidTextures.Logo_22>(4, 4, MaxMeanDeviation4x4, MaxStandardDeviation4x4);

	[Test]
	[Ignore("High dynamic range textures are not supported yet.")]
	public void DecompressAstc5x5HDR() => AssertCorrectDecompression<AndroidTextures.Logo_21>(5, 5, MaxMeanDeviation5x5, MaxStandardDeviation5x5);

	[Test]
	[Ignore("High dynamic range textures are not supported yet.")]
	public void DecompressAstc6x6HDR() => AssertCorrectDecompression<AndroidTextures.Logo_20>(6, 6, MaxMeanDeviation6x6, MaxStandardDeviation6x6);

	[Test]
	[Ignore("High dynamic range textures are not supported yet.")]
	public void DecompressAstc8x8HDR() => AssertCorrectDecompression<AndroidTextures.Logo_19>(8, 8, MaxMeanDeviation8x8, MaxStandardDeviation8x8);

	[Test]
	[Ignore("High dynamic range textures are not supported yet.")]
	public void DecompressAstc10x10HDR() => AssertCorrectDecompression<AndroidTextures.Logo_18>(10, 10, MaxMeanDeviation10x10, MaxStandardDeviation10x10);

	[Test]
	[Ignore("High dynamic range textures are not supported yet.")]
	public void DecompressAstc12x12HDR() => AssertCorrectDecompression<AndroidTextures.Logo_17>(4, 4, MaxMeanDeviation12x12, MaxStandardDeviation12x12);

	private static void AssertCorrectDecompression<T>(int blockWidth, int blockHeight, double maxMeanDeviation, double maxStandardDeviation) where T : ITexture
	{
		ReadOnlySpan<byte> data = T.Data;
		int bytesRead = AstcDecoder.DecodeASTC(data, T.Width, T.Height, blockWidth, blockHeight, out byte[] decompressedData);
		if (!T.Mips)
		{
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}
		ByteArrayDeviation.AssertMinimalDeviation(decompressedData, AndroidTextures.Logo_00.Data, maxMeanDeviation, maxStandardDeviation);
	}
}
