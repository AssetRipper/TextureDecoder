using AssetRipper.TextureDecoder.Rgb.Formats;
using AssetRipper.TextureDecoder.Yuy2;
using System.Runtime.CompilerServices;

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
			int bytesRead = Yuy2Decoder.DecompressYUY2<ColorBGRA<byte>, byte>(input, TestImageWidth, TestImageHeight, out byte[] decodedData);
			Assert.Multiple(() =>
			{
				Assert.That(bytesRead, Is.EqualTo(input.Length));
				Assert.That(decodedData, Has.Length.EqualTo(TestImageWidth * TestImageHeight * Unsafe.SizeOf<ColorBGRA<byte>>()));
			});
		}
	}
}
