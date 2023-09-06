using AssetRipper.TextureDecoder.Bc;
using AssetRipper.TextureDecoder.Dxt;

namespace AssetRipper.TextureDecoder.Tests
{
	public sealed class BcTests
	{
		private const double MaxMeanDeviationBc6h = 0.5;
		private const double MaxMeanDeviationBc7 = 0.1;//Bc7 is more accurate than Bc6h
		private const double MaxStandardDeviationBc6h = 0.8;
		private const double MaxStandardDeviationBc7 = 0.8;
		
		private static byte[] originalBgra32LogoData = Array.Empty<byte>();

		[SetUp]
		public void InitializeOriginalData()
		{
			originalBgra32LogoData = File.ReadAllBytes(TestFileFolders.BcTestFiles + "test.bgra32");
		}
		
		[Test]
		public void DecompressDXT1Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.DxtTestFiles + "test.dxt1");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4 }) //mipmaps 2 and 1 cause a buffer overrun for the output
			{
				int bytesRead = Bc1.Decompress(data.Slice(totalBytesRead), size, size, out byte[] bcDecodedData);
				DxtDecoder.DecompressDXT1(data.Slice(totalBytesRead), size, size, out byte[] dxtDecodedData);
				totalBytesRead += bytesRead;
				AssertAlmostEqual(dxtDecodedData, bcDecodedData);
			}
			Assert.That(totalBytesRead + 16, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressDXT3Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.DxtTestFiles + "test.dxt3");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4 }) //mipmaps 2 and 1 cause a buffer overrun for the output
			{
				int bytesRead = Bc2.Decompress(data.Slice(totalBytesRead), size, size, out byte[] bcDecodedData);
				DxtDecoder.DecompressDXT3(data.Slice(totalBytesRead), size, size, out byte[] dxtDecodedData);
				totalBytesRead += bytesRead;
				AssertAlmostEqual(dxtDecodedData, bcDecodedData);
			}
			Assert.That(totalBytesRead + 32, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressDXT5Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.DxtTestFiles + "test.dxt5");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4 }) //mipmaps 2 and 1 cause a buffer overrun for the output
			{
				int bytesRead = Bc3.Decompress(data.Slice(totalBytesRead), size, size, out byte[] bcDecodedData);
				DxtDecoder.DecompressDXT5(data.Slice(totalBytesRead), size, size, out byte[] dxtDecodedData);
				totalBytesRead += bytesRead;
				AssertAlmostEqual(dxtDecodedData, bcDecodedData);
			}
			Assert.That(totalBytesRead + 32, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressBC4Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.BcTestFiles + "test.bc4");
			int bytesRead = Bc4.Decompress(data, 512, 512, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressBC5Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.BcTestFiles + "test.bc5");
			int bytesRead = Bc5.Decompress(data, 512, 512, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressBC6HFastTest()
		{
			AssertCorrectBC6HDecompression(TestFileFolders.BcTestFiles + "test.bc6h_fast", 512, 512, false);
		}

		[Test]
		public void DecompressBC6HNormalTest()
		{
			AssertCorrectBC6HDecompression(TestFileFolders.BcTestFiles + "test.bc6h_normal", 512, 512, false);
		}

		[Test]
		public void DecompressBC6HBestTest()
		{
			AssertCorrectBC6HDecompression(TestFileFolders.BcTestFiles + "test.bc6h_best", 512, 512, false);
		}

		[Test]
		public void DecompressBC7FastTest()
		{
			AssertCorrectBC7Decompression(TestFileFolders.BcTestFiles + "test.bc7_fast", 512, 512);
		}

		[Test]
		public void DecompressBC7NormalTest()
		{
			AssertCorrectBC7Decompression(TestFileFolders.BcTestFiles + "test.bc7_normal", 512, 512);
		}

		[Test]
		public void DecompressBC7BestTest()
		{
			AssertCorrectBC7Decompression(TestFileFolders.BcTestFiles + "test.bc7_best", 512, 512);
		}

		private static void AssertCorrectBC6HDecompression(string path, int width, int height, bool isSigned)
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(path);
			int bytesRead = Bc6h.Decompress(data, width, height, isSigned, out byte[] decodedData);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
			ByteArrayDeviation.AssertMinimalDeviation(decodedData, originalBgra32LogoData, MaxMeanDeviationBc6h, MaxStandardDeviationBc6h);
		}

		private static void AssertCorrectBC7Decompression(string path, int width, int height)
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(path);
			int bytesRead = Bc7.Decompress(data, width, height, out byte[] decodedData);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
			ByteArrayDeviation.AssertMinimalDeviation(decodedData, originalBgra32LogoData, MaxMeanDeviationBc7, MaxStandardDeviationBc7);
		}

		private static void AssertAlmostEqual(byte[] array1, byte[] array2, int maxDifference = 2)
		{
			if (array1.Length != array2.Length)
			{
				Assert.Fail($"Differing array lengths\nArray 1 length {array1.Length}\nArray 2 length {array2.Length}");
				return;
			}

			for (int i = 0; i < array1.Length; i++)
			{
				int difference = Math.Abs(array1[i] - array2[i]);
				if (difference > maxDifference)
				{
					Assert.Fail($"Differing values at index {i}\nArray 1: {array1[i]}\nArray 2: {array2[i]}");
					return;
				}
			}
		}
	}
}
