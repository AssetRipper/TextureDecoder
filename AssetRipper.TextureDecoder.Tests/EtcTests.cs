using AssetRipper.TextureDecoder.Etc;
using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;

namespace AssetRipper.TextureDecoder.Tests;

public sealed class EtcTests
{
	private static DecodingDelegate ETCDelegate { get; } = EtcDecoder.DecompressETC<ColorBGRA<byte>, byte>;
	private static DecodingDelegate ETC2Delegate { get; } = EtcDecoder.DecompressETC2<ColorBGRA<byte>, byte>;
	private static DecodingDelegate ETC2A1Delegate { get; } = EtcDecoder.DecompressETC2A1<ColorBGRA<byte>, byte>;
	private static DecodingDelegate ETC2A8Delegate { get; } = EtcDecoder.DecompressETC2A8<ColorBGRA<byte>, byte>;
	private static DecodingDelegate EACRSignedDelegate { get; } = EtcDecoder.DecompressEACRSigned<ColorBGRA<byte>, byte>;
	private static DecodingDelegate EACRUnsignedDelegate { get; } = EtcDecoder.DecompressEACRUnsigned<ColorBGRA<byte>, byte>;
	private static DecodingDelegate EACRGSignedDelegate { get; } = EtcDecoder.DecompressEACRGSigned<ColorBGRA<byte>, byte>;
	private static DecodingDelegate EACRGUnsignedDelegate { get; } = EtcDecoder.DecompressEACRGUnsigned<ColorBGRA<byte>, byte>;

	[Test]
	public void DecompressETCTest() => AssertCorrectByteCountReadFor256SquareWithMips(TestFileFolders.EtcTestFiles + "test.etc", ETCDelegate);

	[Test]
	public void DecompressETC2Test() => AssertCorrectByteCountReadFor256SquareWithMips(TestFileFolders.EtcTestFiles + "test.etc2", ETC2Delegate);

	[Test]
	public void DecompressETC2A1Test() => AssertCorrectByteCountReadFor256SquareWithMips(TestFileFolders.EtcTestFiles + "test.etc2a1", ETC2A1Delegate);

	[Test]
	public void DecompressETC2A8Test() => AssertCorrectByteCountReadFor256SquareWithMips(TestFileFolders.EtcTestFiles + "test.etc2a8", ETC2A8Delegate);

	[Test]
	public void DecompressEACRSignedTest() => AssertCorrectByteCountReadFor256SquareWithMips(TestFileFolders.EtcTestFiles + "test.eac_r", EACRSignedDelegate);

	[Test]
	public void DecompressEACRUnsignedTest() => AssertCorrectByteCountReadFor256SquareWithMips(TestFileFolders.EtcTestFiles + "test.eac_r", EACRUnsignedDelegate);

	[Test]
	public void DecompressEACRGSignedTest() => AssertCorrectByteCountReadFor256SquareWithMips(TestFileFolders.EtcTestFiles + "test.eac_rg", EACRGSignedDelegate);

	[Test]
	public void DecompressEACRGUnsignedTest() => AssertCorrectByteCountReadFor256SquareWithMips(TestFileFolders.EtcTestFiles + "test.eac_rg", EACRGUnsignedDelegate);

	[Test]
	public void CorrectnessETCTest() => AssertCorrectDecompression<AndroidTextures.Logo_12, ColorRGB<byte>>(ETCDelegate, .05, 2.1);

	[Test]
	public void CorrectnessETC2Test() => AssertCorrectDecompression<AndroidTextures.Logo_11, ColorRGB<byte>>(ETC2Delegate, .15, 1.6);

	[Test]
	public void CorrectnessETC2A1Test() => AssertCorrectDecompression<AndroidTextures.Logo_08>(ETC2A1Delegate, .15, 1.7);

	[Test]
	public void CorrectnessETC2A8Test() => AssertCorrectDecompression<AndroidTextures.Logo_07>(ETC2A8Delegate, .15, 1.6);

	[Test]
	public void CorrectnessEACRUnsignedTest() => AssertCorrectDecompression<AndroidTextures.Logo_16, ColorR<byte>>(EACRUnsignedDelegate, .05, .5);

	[Test]
	public void CorrectnessEACRGUnsignedTest() => AssertCorrectDecompression<AndroidTextures.Logo_15, ColorRG<byte>>(EACRGUnsignedDelegate, .1, .6);

	private delegate int DecodingDelegate(ReadOnlySpan<byte> data, int width, int height, out byte[] decompressedData);

	private static void AssertCorrectByteCountReadFor256SquareWithMips(string path, DecodingDelegate decoder)
	{
		byte[] data = File.ReadAllBytes(path);
		int totalBytesRead = 0;
		foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
		{
			int bytesRead = decoder.Invoke(new ReadOnlySpan<byte>(data, totalBytesRead, data.Length - totalBytesRead), size, size, out _);
			totalBytesRead += bytesRead;
		}
		Assert.That(totalBytesRead, Is.EqualTo(data.Length));
	}

	private static void AssertCorrectDecompression<T>(DecodingDelegate decoder, double maxMeanDeviation, double maxStandardDeviation) where T : ITexture
	{
		AssertCorrectDecompression<T, ColorRGBA<byte>>(decoder, maxMeanDeviation, maxStandardDeviation);
	}

	private static void AssertCorrectDecompression<TTexture, TColor>(DecodingDelegate decoder, double maxMeanDeviation, double maxStandardDeviation) where TTexture : ITexture where TColor : unmanaged, IColor<TColor, byte>
	{
		byte[] data = TTexture.Data;
		int bytesRead = decoder.Invoke(new ReadOnlySpan<byte>(data), TTexture.Width, TTexture.Height, out byte[] decompressedData);
		if (!TTexture.Mips)
		{
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		byte[] comparisonData = AndroidTextures.Logo_00.Data;

		//Remove unused color channels
		if (typeof(TColor) != typeof(ColorBGRA<byte>) && typeof(TColor) != typeof(ColorRGBA<byte>))
		{
			Span<ColorBGRA<byte>> pixels = MemoryMarshal.Cast<byte, ColorBGRA<byte>>((Span<byte>)comparisonData);
			for (int i = 0; i < pixels.Length; i++)
			{
				pixels[i] = pixels[i].Convert<ColorBGRA<byte>, byte, TColor, byte>().Convert<TColor, byte, ColorBGRA<byte>, byte>();
			}
		}

		ByteArrayDeviation.AssertMinimalDeviation(decompressedData, comparisonData, maxMeanDeviation, maxStandardDeviation);
	}
}
