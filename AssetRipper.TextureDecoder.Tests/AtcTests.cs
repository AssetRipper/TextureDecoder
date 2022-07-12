namespace AssetRipper.TextureDecoder.Tests
{
	public sealed class AtcTests
	{
		[Test]
		public void DecompressAtcRgb4Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.AtcTestFilesFolder + "test.atc_rgb4");
			int bytesRead = Atc.AtcDecoder.DecompressAtcRgb4(data, 256, 256, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressAtcRgba8Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(PathConstants.AtcTestFilesFolder + "test.atc_rgba8");
			int bytesRead = Atc.AtcDecoder.DecompressAtcRgba8(data, 256, 256, out _);
			Assert.That(bytesRead, Is.EqualTo(data.Length));
		}
	}
}