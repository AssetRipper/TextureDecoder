using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;

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
				int bytesRead = ConvertSquare<ColorA<byte>, byte>(data.Slice(totalBytesRead), size);
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
				int bytesRead = ConvertSquare<ColorRGB<byte>, byte>(data.Slice(totalBytesRead), size);
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
				int bytesRead = ConvertSquare<ColorRGBA<byte>, byte>(data.Slice(totalBytesRead), size);
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
				int bytesRead = ConvertSquare<ColorARGB<byte>, byte>(data.Slice(totalBytesRead), size);
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
				int bytesRead = ConvertSquare<ColorRGB16, byte>(data.Slice(totalBytesRead), size);
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
				int bytesRead = ConvertSquare<ColorRG<byte>, byte>(data.Slice(totalBytesRead), size);
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
				int bytesRead = ConvertSquare<ColorR<byte>, byte>(data.Slice(totalBytesRead), size);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRHalfTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rhalf");
			int bytesRead = ConvertSquare<ColorR<Half>, Half>(data, 256);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGHalfTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rghalf");
			int bytesRead = ConvertSquare<ColorRG<Half>, Half>(data, 256);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGBAHalfTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rgbahalf");
			int bytesRead = ConvertSquare<ColorRGBA<Half>, Half>(data, 256);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRFloatTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rfloat");
			int bytesRead = ConvertSquare<ColorR<float>, float>(data, 256);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGFloatTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rgfloat");
			int bytesRead = ConvertSquare<ColorRG<float>, float>(data, 256);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGBAFloatTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rgbafloat");
			int bytesRead = ConvertSquare<ColorRGBA<float>, float>(data, 256);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGB9e5FloatTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rgb9e5float");
			int bytesRead = ConvertSquare<ColorRGB9e5, double>(data, 256);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRG32Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rg32");
			int bytesRead = ConvertSquare<ColorRG<ushort>, ushort>(data, 512);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGB48Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rgb48");
			int bytesRead = ConvertSquare<ColorRGB<ushort>, ushort>(data, 512);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void ConvertRGBA64Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.RgbTestFiles + "test.rgba64");
			int bytesRead = ConvertSquare<ColorRGBA<ushort>, ushort>(data, 512);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		private static int ConvertSquare<TColor, TChannel>(ReadOnlySpan<byte> data, int size)
			where TColor : unmanaged, IColor<TChannel>
			where TChannel : unmanaged
		{
			return RgbConverter.Convert<TColor, TChannel, ColorBGRA<byte>, byte>(data, size, size, out _);
		}
	}
}
