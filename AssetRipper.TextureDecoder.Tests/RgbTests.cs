namespace AssetRipper.TextureDecoder.Tests
{
	public sealed class RgbTests
	{
		[Test]
		public void ConvertA8Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.alpha8");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Rgb.RgbConverter.A8ToBGRA32(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGB24Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rgb24");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Rgb.RgbConverter.RGB24ToBGRA32(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGBA32Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rgba32");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Rgb.RgbConverter.RGBA32ToBGRA32(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertARGB32Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.argb32");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Rgb.RgbConverter.ARGB32ToBGRA32(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGB16Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rgb565");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Rgb.RgbConverter.RGB16ToBGRA32(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRG16Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rg16");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Rgb.RgbConverter.RG16ToBGRA32(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertR8Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.r8");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Rgb.RgbConverter.R8ToBGRA32(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRHalfTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rhalf");
			int bytesRead = Rgb.RgbConverter.RHalfToBGRA32(data, 256, 256, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGHalfTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rghalf");
			int bytesRead = Rgb.RgbConverter.RGHalfToBGRA32(data, 256, 256, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGBAHalfTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rgbahalf");
			int bytesRead = Rgb.RgbConverter.RGBAHalfToBGRA32(data, 256, 256, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRFloatTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rfloat");
			int bytesRead = Rgb.RgbConverter.RFloatToBGRA32(data, 256, 256, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGFloatTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rgfloat");
			int bytesRead = Rgb.RgbConverter.RGFloatToBGRA32(data, 256, 256, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGBAFloatTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rgbafloat");
			int bytesRead = Rgb.RgbConverter.RGBAFloatToBGRA32(data, 256, 256, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGB9e5FloatTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rgb9e5float");
			int bytesRead = Rgb.RgbConverter.RGB9e5FloatToBGRA32(data, 256, 256, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRG32Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rg32");
			int bytesRead = Rgb.RgbConverter.RG32ToBGRA32(data, 512, 512, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGB48Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rgb48");
			int bytesRead = Rgb.RgbConverter.RGB48ToBGRA32(data, 512, 512, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGBA64Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rgba64");
			int bytesRead = Rgb.RgbConverter.RGBA64ToBGRA32(data, 512, 512, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}
	}
}
