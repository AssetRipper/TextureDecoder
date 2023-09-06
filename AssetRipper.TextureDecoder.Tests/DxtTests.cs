namespace AssetRipper.TextureDecoder.Tests
{
	public sealed class DxtTests
	{
		[Test]
		public void DecompressDXT1Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.DxtTestFiles + "test.dxt1");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Dxt.DxtDecoder.DecompressDXT1(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressDXT3Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.DxtTestFiles + "test.dxt3");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Dxt.DxtDecoder.DecompressDXT3(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}

		[Test]
		public void DecompressDXT5Test()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.DxtTestFiles + "test.dxt5");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Dxt.DxtDecoder.DecompressDXT5(data.Slice(totalBytesRead), size, size, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}
	}
}
