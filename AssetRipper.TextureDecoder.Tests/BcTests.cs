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
			originalBgra32LogoData = File.ReadAllBytes(PathConstants.BcTestFilesFolder + "test.bgra32");
		}
		
		[Test]
		public void DecompressDXT1Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.DxtTestFilesFolder + "test.dxt1");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4 }) //mipmaps 2 and 1 cause a buffer overrun for the output
			{
				int bytesRead = BcDecoder.DecompressBC1(data.Slice(totalBytesRead), size, size, out byte[] bcDecodedData);
				DxtDecoder.DecompressDXT1(data.Slice(totalBytesRead), size, size, out byte[] dxtDecodedData);
				totalBytesRead += bytesRead;
				AssertAlmostEqual(dxtDecodedData, bcDecodedData);
			}
			Assert.That(totalBytesRead + 16, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressDXT3Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.DxtTestFilesFolder + "test.dxt3");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4 }) //mipmaps 2 and 1 cause a buffer overrun for the output
			{
				int bytesRead = BcDecoder.DecompressBC2(data.Slice(totalBytesRead), size, size, out byte[] bcDecodedData);
				DxtDecoder.DecompressDXT3(data.Slice(totalBytesRead), size, size, out byte[] dxtDecodedData);
				totalBytesRead += bytesRead;
				AssertAlmostEqual(dxtDecodedData, bcDecodedData);
			}
			Assert.That(totalBytesRead + 32, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressDXT5Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.DxtTestFilesFolder + "test.dxt5");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4 }) //mipmaps 2 and 1 cause a buffer overrun for the output
			{
				int bytesRead = BcDecoder.DecompressBC3(data.Slice(totalBytesRead), size, size, out byte[] bcDecodedData);
				DxtDecoder.DecompressDXT5(data.Slice(totalBytesRead), size, size, out byte[] dxtDecodedData);
				totalBytesRead += bytesRead;
				AssertAlmostEqual(dxtDecodedData, bcDecodedData);
			}
			Assert.That(totalBytesRead + 32, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressBC4Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.BcTestFilesFolder + "test.bc4");
			int bytesRead = BcDecoder.DecompressBC4(data, 512, 512, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressBC5Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.BcTestFilesFolder + "test.bc5");
			int bytesRead = BcDecoder.DecompressBC5(data, 512, 512, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressBC6HFastTest()
		{
			AssertCorrectBC6HDecompression(PathConstants.BcTestFilesFolder + "test.bc6h_fast", 512, 512, false);
		}

		[Test]
		public void DecompressBC6HNormalTest()
		{
			AssertCorrectBC6HDecompression(PathConstants.BcTestFilesFolder + "test.bc6h_normal", 512, 512, false);
		}

		[Test]
		public void DecompressBC6HBestTest()
		{
			AssertCorrectBC6HDecompression(PathConstants.BcTestFilesFolder + "test.bc6h_best", 512, 512, false);
		}

		[Test]
		public void DecompressBC7FastTest()
		{
			AssertCorrectBC7Decompression(PathConstants.BcTestFilesFolder + "test.bc7_fast", 512, 512);
		}

		[Test]
		public void DecompressBC7NormalTest()
		{
			AssertCorrectBC7Decompression(PathConstants.BcTestFilesFolder + "test.bc7_normal", 512, 512);
		}

		[Test]
		public void DecompressBC7BestTest()
		{
			AssertCorrectBC7Decompression(PathConstants.BcTestFilesFolder + "test.bc7_best", 512, 512);
		}

		private static void AssertCorrectBC6HDecompression(string path, int width, int height, bool isSigned)
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(path);
			int bytesRead = BcDecoder.DecompressBC6H(data, width, height, isSigned, out byte[] decodedData);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
			AssertMinimalDeviation(decodedData, originalBgra32LogoData, MaxMeanDeviationBc6h, MaxStandardDeviationBc6h);
		}

		private static void AssertCorrectBC7Decompression(string path, int width, int height)
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(path);
			int bytesRead = BcDecoder.DecompressBC7(data, width, height, out byte[] decodedData);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
			AssertMinimalDeviation(decodedData, originalBgra32LogoData, MaxMeanDeviationBc7, MaxStandardDeviationBc7);
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

		private static void AssertMinimalDeviation(byte[] decoded, byte[] original, double maxMeanDeviation, double maxStandardDeviation)
		{
			if (decoded.Length != original.Length)
			{
				Assert.Fail($"Differing array lengths\nDecoded length {decoded.Length}\nOriginal length {original.Length}");
				return;
			}

			long differenceSum = 0;
			
			for (int i = 0; i < decoded.Length; i++)
			{
				differenceSum += decoded[i] - original[i];
			}

			double mean = differenceSum / (double)decoded.Length;

			double sumOfSquaredDeviations = 0;
			
			for (int i = 0; i < decoded.Length; i++)
			{
				double deviation = decoded[i] - original[i] - mean;
				sumOfSquaredDeviations += deviation * deviation;
			}

			double standardDeviation = Math.Sqrt(sumOfSquaredDeviations / (decoded.Length - 1)); 
			//Not sure if Bessel's correction is needed here, but it doesn't hurt, especially since length is around 1 million in the current use.

			Assert.Multiple(() =>
			{
				Assert.That(mean, Is.LessThan(maxMeanDeviation), "Mean too far positive");
				Assert.That(mean, Is.GreaterThan(-maxMeanDeviation), "Mean too far negative");
				Assert.That(standardDeviation, Is.LessThan(maxStandardDeviation), "Standard deviation too large");
			});
		}
	}
}