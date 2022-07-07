namespace AssetRipper.TextureDecoder.Tests
{
	public sealed class RgbTests
	{
		[Test]
		public void ConvertA8Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.alpha8");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Rgb.RgbConverter.A8ToBGRA32(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertARGB16Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.argb4444");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Rgb.RgbConverter.ARGB16ToBGRA32(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGB24Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.rgb24");
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
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.rgba32");
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
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.argb32");
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
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.rgb565");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Rgb.RgbConverter.RGB16ToBGRA32(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertR16Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.r16");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Rgb.RgbConverter.R16ToBGRA32(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGBA16Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.rgba4444");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Rgb.RgbConverter.RGBA16ToBGRA32(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRG16Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.rg16");
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
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.r8");
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
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.rhalf");
			int bytesRead = Rgb.RgbConverter.RHalfToBGRA32(data, 256, 256, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGHalfTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.rghalf");
			int bytesRead = Rgb.RgbConverter.RGHalfToBGRA32(data, 256, 256, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGBAHalfTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.rgbahalf");
			int bytesRead = Rgb.RgbConverter.RGBAHalfToBGRA32(data, 256, 256, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRFloatTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.rfloat");
			int bytesRead = Rgb.RgbConverter.RFloatToBGRA32(data, 256, 256, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGFloatTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.rgfloat");
			int bytesRead = Rgb.RgbConverter.RGFloatToBGRA32(data, 256, 256, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGBAFloatTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.rgbafloat");
			int bytesRead = Rgb.RgbConverter.RGBAFloatToBGRA32(data, 256, 256, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGB9e5FloatTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.rgb9e5float");
			int bytesRead = Rgb.RgbConverter.RGB9e5FloatToBGRA32(data, 256, 256, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRG32Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.rg32");
			int bytesRead = Rgb.RgbConverter.RG32ToBGRA32(data, 512, 512, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGB48Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.rgb48");
			int bytesRead = Rgb.RgbConverter.RGB48ToBGRA32(data, 512, 512, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGBA64Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.RgbTestProjectRootFolder + "test.rgba64");
			int bytesRead = Rgb.RgbConverter.RGBA64ToBGRA32(data, 512, 512, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}
	}
}