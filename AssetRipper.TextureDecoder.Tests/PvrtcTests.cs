using AssetRipper.TextureDecoder.Pvrtc;
using AssetRipper.TextureDecoder.Rgb.Formats;

namespace AssetRipper.TextureDecoder.Tests;

public sealed class PvrtcTests
{
	[Test]
	public void DecompressPVRTCTest()
	{
		ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.PvrtcTestFiles + "test.pvrtc4");
		int totalBytesRead = 0;
		foreach (int size in new int[] { 512, 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
		{
			int bytesRead = PvrtcDecoder.DecompressPVRTC<ColorBGRA32, byte>(data, size, size, false, out _);
			totalBytesRead += bytesRead;
		}
		Assert.That(totalBytesRead, Is.EqualTo(data.Length));
	}

	private const double MaxMeanDeviation4 = 0.05;
	private const double MaxStandardDeviation4 = 1.4;

	private const double MaxMeanDeviation2 = 0.10;
	private const double MaxStandardDeviation2 = 2.6;

	[Test]
	public void Decompress_4() => AssertCorrectDecompression<AndroidTextures.Logo_13>(false, MaxMeanDeviation4, MaxStandardDeviation4);

	[Test]
	public void Decompress_2() => AssertCorrectDecompression<AndroidTextures.Logo_14>(true, MaxMeanDeviation2, MaxStandardDeviation2);

	private static void AssertCorrectDecompression<T>(bool do2bitMode, double maxMeanDeviation, double maxStandardDeviation) where T : ITexture
	{
		ReadOnlySpan<byte> data = T.Data;
		int bytesRead = PvrtcDecoder.DecompressPVRTC<ColorBGRA32, byte>(data, T.Width, T.Height, do2bitMode, out byte[] decompressedData);
		if (!T.Mips)
		{
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}
		ByteArrayDeviation.AssertMinimalDeviation(decompressedData, AndroidTextures.Logo_00.Data, maxMeanDeviation, maxStandardDeviation);
	}
}
