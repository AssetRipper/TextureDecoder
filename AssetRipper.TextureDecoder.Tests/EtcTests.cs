using AssetRipper.TextureDecoder.Etc;

namespace AssetRipper.TextureDecoder.Tests;

public sealed class EtcTests
{
	private static DecodingDelegate ETCDelegate { get; } = (data, width, height) => EtcDecoder.DecompressETC(data, width, height, out _);
	private static DecodingDelegate ETC2Delegate { get; } = (data, width, height) => EtcDecoder.DecompressETC2(data, width, height, out _);
	private static DecodingDelegate ETC2A1Delegate { get; } = (data, width, height) => EtcDecoder.DecompressETC2A1(data, width, height, out _);
	private static DecodingDelegate ETC2A8Delegate { get; } = (data, width, height) => EtcDecoder.DecompressETC2A8(data, width, height, out _);
	private static DecodingDelegate EACRSignedDelegate { get; } = (data, width, height) => EtcDecoder.DecompressEACRSigned(data, width, height, out _);
	private static DecodingDelegate EACRUnsignedDelegate { get; } = (data, width, height) => EtcDecoder.DecompressEACRUnsigned(data, width, height, out _);
	private static DecodingDelegate EACRGSignedDelegate { get; } = (data, width, height) => EtcDecoder.DecompressEACRGSigned(data, width, height, out _);
	private static DecodingDelegate EACRGUnsignedDelegate { get; } = (data, width, height) => EtcDecoder.DecompressEACRGUnsigned(data, width, height, out _);

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

	private delegate int DecodingDelegate(ArraySegment<byte> data, int width, int height);

	private static void AssertCorrectByteCountReadFor256SquareWithMips(string path, DecodingDelegate decoder)
	{
		byte[] data = File.ReadAllBytes(path);
		int totalBytesRead = 0;
		foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
		{
			int bytesRead = decoder.Invoke(new ArraySegment<byte>(data, totalBytesRead, data.Length - totalBytesRead), size, size);
			totalBytesRead += bytesRead;
		}
		Assert.That(totalBytesRead, Is.EqualTo(data.Length));
	}
}
