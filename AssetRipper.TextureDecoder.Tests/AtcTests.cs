namespace AssetRipper.TextureDecoder.Tests
{
	public sealed class AtcTests
	{
		[Test]
		public void DecompressAtcRgb4Test()
		{
			byte[] data = File.ReadAllBytes(TestFileFolders.AtcTestFiles + "test.atc_rgb4");
			int bytesRead = Atc.AtcDecoder.DecompressAtcRgb4(data, 256, 256, out byte[] decodedData);
			Assert.Multiple(() =>
			{
				Assert.That(bytesRead, Is.EqualTo(data.Length));
				Assert.That(decodedData, Is.EqualTo(File.ReadAllBytes(TestFileFolders.AtcTestFiles + "test.atc_rgb4_decoded")));
			});
		}

		[Test]
		public void DecompressAtcRgba8Test()
		{
			byte[] data = File.ReadAllBytes(TestFileFolders.AtcTestFiles + "test.atc_rgba8");
			int bytesRead = Atc.AtcDecoder.DecompressAtcRgba8(data, 256, 256, out byte[] decodedData);
			Assert.Multiple(() =>
			{
				Assert.That(bytesRead, Is.EqualTo(data.Length));
				Assert.That(decodedData, Is.EqualTo(File.ReadAllBytes(TestFileFolders.AtcTestFiles + "test.atc_rgba8_decoded")));
			});
		}
	}
}
