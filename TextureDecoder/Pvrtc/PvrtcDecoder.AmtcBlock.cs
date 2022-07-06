//#define DISABLE_TWINDDLING_ROUTINE
#define ASSUME_IMAGE_TILING


namespace AssetRipper.TextureDecoder.Pvrtc
{
	public static partial class PvrtcDecoder
	{
		/// <summary>
		/// 8 bytes
		/// </summary>
		private struct AmtcBlock
		{
			public AmtcBlock(uint v0, uint v1)
			{
				PackedData0 = v0;
				PackedData1 = v1;
			}

			// Uses 64 bits pre block
			public readonly uint PackedData0;
			public readonly uint PackedData1;
		}
	}
}
