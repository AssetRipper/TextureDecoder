namespace AssetRipper.TextureDecoder.Pvrtc;

public static partial class PvrtcDecoder
{
	/// <summary>
	/// Low precision colours extracted from the blocks
	/// </summary>
	/// <remarks>
	/// 2 * 4 integers
	/// </remarks>
	[InlineArray(8)]
	private struct Colours5554
	{
		private int _element0;
	}
}
