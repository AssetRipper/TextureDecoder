namespace AssetRipper.TextureDecoder.Tests
{
	public sealed class PvrtcTests
	{
		[Test]
		public void DecompressPVRTCTest()
		{
			byte[] data = File.ReadAllBytes(PathConstants.PvrtcTestProjectRootFolder + "test.pvrtc4");
			byte[] outputData = new byte[512 * 512 * 4];
			Pvrtc.PvrtcDecoder.DecompressPVRTC(data, 512, 512, false, outputData);
		}
	}
}