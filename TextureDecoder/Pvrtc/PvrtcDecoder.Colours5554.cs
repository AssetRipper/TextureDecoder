//#define DISABLE_TWINDDLING_ROUTINE
#define ASSUME_IMAGE_TILING


namespace AssetRipper.TextureDecoder.Pvrtc
{
	public static partial class PvrtcDecoder
	{
		/// <summary>
		/// Low precision colours extracted from the blocks
		/// </summary>
		private unsafe struct Colours5554
		{
			public fixed int Reps[2 * 4];
		}
	}
}
