namespace AssetRipper.TextureDecoder.Tests
{
	public sealed class PvrtcTests
	{
		[Test]
		public void DecompressPVRTCTest()
		{
			ReadOnlySpan<byte> data = File.ReadAllBytes(TestFileFolders.PvrtcTestFiles + "test.pvrtc4");
			int totalBytesRead = 0;
			foreach (int size in new int[] { 512, 256, 128, 64, 32, 16, 8, 4, 2, 1 }) //mip maps
			{
				int bytesRead = Pvrtc.PvrtcDecoder.DecompressPVRTC(data, size, size, false, out _);
				totalBytesRead += bytesRead;
			}
			Assert.That(totalBytesRead, Is.EqualTo(data.Length));
		}
	}
}
