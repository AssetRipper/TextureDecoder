using AssetRipper.TextureDecoder.Etc;

namespace AssetRipper.TextureDecoder.Tests;

public sealed class EtcTests
{
	[Test]
	public void DecompressETCTest()
	{
		AssertCorrectByteCountReadFor256SquareWithMips(TestFileFolders.EtcTestFiles + "test.etc", (data, size) =>
		{
			return EtcDecoder.DecompressETC(data, size, size, out _);
		});
	}

	[Test]
	public void DecompressETC2Test()
	{
		AssertCorrectByteCountReadFor256SquareWithMips(TestFileFolders.EtcTestFiles + "test.etc2", (data, size) =>
		{
			return EtcDecoder.DecompressETC2(data, size, size, out _);
		});
	}

	[Test]
	public void DecompressETC2A1Test()
	{
		AssertCorrectByteCountReadFor256SquareWithMips(TestFileFolders.EtcTestFiles + "test.etc2a1", (data, size) =>
		{
			return EtcDecoder.DecompressETC2A1(data, size, size, out _);
		});
	}

	[Test]
	public void DecompressETC2A8Test()
	{
		AssertCorrectByteCountReadFor256SquareWithMips(TestFileFolders.EtcTestFiles + "test.etc2a8", (data, size) =>
		{
			return EtcDecoder.DecompressETC2A8(data, size, size, out _);
		});
	}

	[Test]
	public void DecompressEACRSignedTest()
	{
		AssertCorrectByteCountReadFor256SquareWithMips(TestFileFolders.EtcTestFiles + "test.eac_r", (data, size) =>
		{
			return EtcDecoder.DecompressEACRSigned(data, size, size, out _);
		});
	}

	[Test]
	public void DecompressEACRUnsignedTest()
	{
		AssertCorrectByteCountReadFor256SquareWithMips(TestFileFolders.EtcTestFiles + "test.eac_r", (data, size) =>
		{
			return EtcDecoder.DecompressEACRUnsigned(data, size, size, out _);
		});
	}

	[Test]
	public void DecompressEACRGSignedTest()
	{
		AssertCorrectByteCountReadFor256SquareWithMips(TestFileFolders.EtcTestFiles + "test.eac_rg", (data, size) =>
		{
			return EtcDecoder.DecompressEACRGSigned(data, size, size, out _);
		});
	}
	
	[Test]
	public void DecompressEACRGUnsignedTest()
	{
		AssertCorrectByteCountReadFor256SquareWithMips(TestFileFolders.EtcTestFiles + "test.eac_rg", (data, size) =>
		{
			return EtcDecoder.DecompressEACRGUnsigned(data, size, size, out _);
		});
	}

	private delegate int DecodingDelegate(ArraySegment<byte> data, int size);

	private static void AssertCorrectByteCountReadFor256SquareWithMips(string path, DecodingDelegate decoder)
	{
		byte[] data = File.ReadAllBytes(path);
		int totalBytesRead = 0;
		foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
		{
			int bytesRead = decoder.Invoke(new ArraySegment<byte>(data, totalBytesRead, data.Length - totalBytesRead), size);
			totalBytesRead += bytesRead;
		}
		Assert.That(totalBytesRead, Is.EqualTo(data.Length));
	}
}
