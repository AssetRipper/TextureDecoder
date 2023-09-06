using AssetRipper.TextureDecoder.Rgb.Formats;
using AssetRipper.TextureDecoder.Yuy2;

namespace AssetRipper.TextureDecoder.Tests
{
	public sealed class Yuy2Tests
	{
		private const int TestImageWidth = 256;
		private const int TestImageHeight = 256;

		[Test]
		public void DecompressYUY2Test()
		{
			byte[] input = File.ReadAllBytes(TestFileFolders.Yuy2TestFiles + "test.yuy2");
			int bytesRead = Yuy2Decoder.DecompressYUY2(input, TestImageWidth, TestImageHeight, out byte[] originalOutput);
			Assert.That(bytesRead, Is.EqualTo(input.Length));
			Yuy2Decoder.DecompressYUY2<ColorBGRA32, byte>(input, TestImageWidth, TestImageHeight, out byte[] genericOutput);
			Assert.That(originalOutput, Is.EqualTo(genericOutput));
		}
	}
}
