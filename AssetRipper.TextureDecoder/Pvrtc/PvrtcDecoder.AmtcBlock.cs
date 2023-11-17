namespace AssetRipper.TextureDecoder.Pvrtc;

public static partial class PvrtcDecoder
{
	/// <summary>
	/// 8 bytes (64 bits) per block
	/// </summary>
	private readonly record struct AmtcBlock(uint PackedData0, uint PackedData1)
	{
	}
}
