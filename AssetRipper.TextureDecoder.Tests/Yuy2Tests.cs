namespace AssetRipper.TextureDecoder.Tests
{
	public sealed class Yuy2Tests
	{
		private const int TestImageWidth = 256;
		private const int TestImageHeight = 256;

		[Test]
		public void DecompressYUY2Test()
		{
			byte[] data = File.ReadAllBytes(PathConstants.Yuy2TestProjectRootFolder + "test.yuy2");
			Yuy2.Yuy2Decoder.DecompressYUY2(data, TestImageWidth, TestImageHeight, out _);
		}
	}
}