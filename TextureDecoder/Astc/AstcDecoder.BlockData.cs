namespace AssetRipper.TextureDecoder.Astc
{
	public static partial class AstcDecoder
	{
		private unsafe struct BlockData
		{
			public int bw;
			public int bh;
			public int width;
			public int height;
			public int part_num;
			public int dual_plane;
			public int plane_selector;
			public int weight_range;
			/// <summary>
			/// max: 120
			/// </summary>
			public int weight_num;
			public fixed int cem[4];
			public int cem_range;
			/// <summary>
			/// max: 32
			/// </summary>
			public int endpoint_value_num;
			public fixed int endpoints[4 * 8];
			public fixed int weights[144 * 2];
			public fixed int partition[144];
		}
	}
}
