using AssetRipper.TextureDecoder.Bc;
using AssetRipper.TextureDecoder.Dxt;
using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Runtime.CompilerServices;

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
		public void Decompress_BC1()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.DxtTestFiles + "test.dxt1");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4 }) //mipmaps 2 and 1 cause a buffer overrun for the output
			{
				int bytesRead = Bc1.Decompress<ColorBGRA<byte>, byte>(data.Slice(totalBytesRead), size, size, out byte[] bcDecodedData);
				DxtDecoder.DecompressDXT1<ColorBGRA<byte>, byte>(data.Slice(totalBytesRead), size, size, out byte[] dxtDecodedData);
				totalBytesRead += bytesRead;
				AssertAlmostEqual(dxtDecodedData, bcDecodedData);
			}
			Assert.That(totalBytesRead + 16, Is.EqualTo(data.Length));
		}

		[Test]
		public void Decompress_BC2()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.DxtTestFiles + "test.dxt3");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4 }) //mipmaps 2 and 1 cause a buffer overrun for the output
			{
				int bytesRead = Bc2.Decompress<ColorBGRA<byte>, byte>(data.Slice(totalBytesRead), size, size, out byte[] bcDecodedData);
				DxtDecoder.DecompressDXT3<ColorBGRA<byte>, byte>(data.Slice(totalBytesRead), size, size, out byte[] dxtDecodedData);
				totalBytesRead += bytesRead;
				AssertAlmostEqual(dxtDecodedData, bcDecodedData);
			}
			Assert.That(totalBytesRead + 32, Is.EqualTo(data.Length));
		}

		[Test]
		public void Decompress_BC3()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.DxtTestFiles + "test.dxt5");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4 }) //mipmaps 2 and 1 cause a buffer overrun for the output
			{
				int bytesRead = Bc3.Decompress<ColorBGRA<byte>, byte>(data.Slice(totalBytesRead), size, size, out byte[] bcDecodedData);
				DxtDecoder.DecompressDXT5<ColorBGRA<byte>, byte>(data.Slice(totalBytesRead), size, size, out byte[] dxtDecodedData);
				totalBytesRead += bytesRead;
				AssertAlmostEqual(dxtDecodedData, bcDecodedData);
			}
			Assert.That(totalBytesRead + 32, Is.EqualTo(data.Length));
		}

		[Test]
		public void Decompress_BC4()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.BcTestFiles + "test.bc4");
			int bytesRead = Bc4.Decompress<ColorBGRA<byte>, byte>(data, 512, 512, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void Decompress_BC5()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.BcTestFiles + "test.bc5");
			int bytesRead = Bc5.Decompress<ColorBGRA<byte>, byte>(data, 512, 512, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[TestCase("test.bc6h_fast")]
		[TestCase("test.bc6h_normal")]
		[TestCase("test.bc6h_best")]
		public void Decompress_BC6H(string fileName)
		{
			AssertCorrectBC6HDecompression(TestFileFolders.BcTestFiles + fileName, 512, 512, false);
		}

		[TestCase("test.bc7_fast")]
		[TestCase("test.bc7_normal")]
		[TestCase("test.bc7_best")]
		public void Decompress_BC7(string fileName)
		{
			AssertCorrectBC7Decompression(TestFileFolders.BcTestFiles + fileName, 512, 512);
		}

		[Test]
		public void PartialBlock_BC1([Range(1, 4)] int width, [Range(1, 4)] int height)
		{
			using (Assert.EnterMultipleScope())
			{
				ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.DxtTestFiles + "test.dxt1").AsSpan()[..Bc1.BlockSize];
				int bytesRead = Bc1.Decompress<ColorBGRA<byte>, byte>(data, width, height, out byte[] decodedData);
				Assert.That(bytesRead, Is.EqualTo(data.Length));
				Assert.That(decodedData, Has.Length.EqualTo(width * height * Unsafe.SizeOf<ColorBGRA<byte>>()));
			}
		}

		[Test]
		public void PartialBlock_BC2([Range(1, 4)] int width, [Range(1, 4)] int height)
		{
			using (Assert.EnterMultipleScope())
			{
				ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.DxtTestFiles + "test.dxt3").AsSpan()[..Bc2.BlockSize];
				int bytesRead = Bc2.Decompress<ColorBGRA<byte>, byte>(data, width, height, out byte[] decodedData);
				Assert.That(bytesRead, Is.EqualTo(data.Length));
				Assert.That(decodedData, Has.Length.EqualTo(width * height * Unsafe.SizeOf<ColorBGRA<byte>>()));
			}
		}

		[Test]
		public void PartialBlock_BC3([Range(1, 4)] int width, [Range(1, 4)] int height)
		{
			using (Assert.EnterMultipleScope())
			{
				ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.DxtTestFiles + "test.dxt5").AsSpan()[..Bc3.BlockSize];
				int bytesRead = Bc3.Decompress<ColorBGRA<byte>, byte>(data, width, height, out byte[] decodedData);
				Assert.That(bytesRead, Is.EqualTo(data.Length));
				Assert.That(decodedData, Has.Length.EqualTo(width * height * Unsafe.SizeOf<ColorBGRA<byte>>()));
			}
		}

		[Test]
		public void PartialBlock_BC4([Range(1, 4)] int width, [Range(1, 4)] int height)
		{
			using (Assert.EnterMultipleScope())
			{
				ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.BcTestFiles + "test.bc4").AsSpan()[..Bc4.BlockSize];
				int bytesRead = Bc4.Decompress<ColorBGRA<byte>, byte>(data, width, height, out byte[] decodedData);
				Assert.That(bytesRead, Is.EqualTo(data.Length));
				Assert.That(decodedData, Has.Length.EqualTo(width * height * Unsafe.SizeOf<ColorBGRA<byte>>()));
			}
		}

		[Test]
		public void PartialBlock_BC5([Range(1, 4)] int width, [Range(1, 4)] int height)
		{
			using (Assert.EnterMultipleScope())
			{
				ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.BcTestFiles + "test.bc5").AsSpan()[..Bc5.BlockSize];
				int bytesRead = Bc5.Decompress<ColorBGRA<byte>, byte>(data, width, height, out byte[] decodedData);
				Assert.That(bytesRead, Is.EqualTo(data.Length));
				Assert.That(decodedData, Has.Length.EqualTo(width * height * Unsafe.SizeOf<ColorBGRA<byte>>()));
			}
		}

		[Test]
		public void PartialBlock_BC6H([Range(1, 4)] int width, [Range(1, 4)] int height)
		{
			using (Assert.EnterMultipleScope())
			{
				ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.BcTestFiles + "test.bc6h_best").AsSpan()[..Bc6h.BlockSize];
				int bytesRead = Bc6h.Decompress<ColorBGRA<byte>, byte>(data, width, height, false, out byte[] decodedData);
				Assert.That(bytesRead, Is.EqualTo(data.Length));
				Assert.That(decodedData, Has.Length.EqualTo(width * height * Unsafe.SizeOf<ColorBGRA<byte>>()));
			}
		}

		[Test]
		public void PartialBlock_BC7([Range(1, 4)] int width, [Range(1, 4)] int height)
		{
			using (Assert.EnterMultipleScope())
			{
				ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.BcTestFiles + "test.bc7_best").AsSpan()[..Bc7.BlockSize];
				int bytesRead = Bc7.Decompress<ColorBGRA<byte>, byte>(data, width, height, out byte[] decodedData);
				Assert.That(bytesRead, Is.EqualTo(data.Length));
				Assert.That(decodedData, Has.Length.EqualTo(width * height * Unsafe.SizeOf<ColorBGRA<byte>>()));
			}
		}

		private static void AssertCorrectBC6HDecompression(string path, int width, int height, bool isSigned)
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(path);
			int bytesRead = Bc6h.Decompress<ColorBGRA<byte>, byte>(data, width, height, isSigned, out byte[] decodedData);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
			ByteArrayDeviation.AssertMinimalDeviation(decodedData, originalBgra32LogoData, MaxMeanDeviationBc6h, MaxStandardDeviationBc6h);
		}

		private static void AssertCorrectBC7Decompression(string path, int width, int height)
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(path);
			int bytesRead = Bc7.Decompress<ColorBGRA<byte>, byte>(data, width, height, out byte[] decodedData);
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
