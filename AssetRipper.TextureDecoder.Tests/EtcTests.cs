namespace AssetRipper.TextureDecoder.Tests
{
	public sealed class EtcTests
	{
		[Test]
		public void DecompressETCTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.EtcTestFiles + "test.etc");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Etc.EtcDecoder.DecompressETC(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressETC2Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.EtcTestFiles + "test.etc2");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Etc.EtcDecoder.DecompressETC2(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressETC2A1Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.EtcTestFiles + "test.etc2a1");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Etc.EtcDecoder.DecompressETC2A1(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressETC2A8Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.EtcTestFiles + "test.etc2a8");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Etc.EtcDecoder.DecompressETC2A8(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressEACRSignedTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.EtcTestFiles + "test.eac_r");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Etc.EtcDecoder.DecompressEACRSigned(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressEACRUnsignedTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.EtcTestFiles + "test.eac_r");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Etc.EtcDecoder.DecompressEACRUnsigned(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressEACRGSignedTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.EtcTestFiles + "test.eac_rg");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Etc.EtcDecoder.DecompressEACRGSigned(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}
		
		[Test]
		public void DecompressEACRGUnsignedTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.EtcTestFiles + "test.eac_rg");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Etc.EtcDecoder.DecompressEACRGUnsigned(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}
	}
}
