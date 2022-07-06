namespace AssetRipper.TextureDecoder.Tests
{
	public sealed class PvrtcTests
	{
		[Test]
		public void DecompressPVRTCTest()
		{
			byte[] data = File.ReadAllBytes(PathConstants.PvrtcTestProjectRootFolder + "test.pvrtc4");
			Pvrtc.PvrtcDecoder.DecompressPVRTC(data, 512, 512, false, out _);
		}
	}
}