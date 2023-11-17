namespace AssetRipper.TextureDecoder.Astc;

public static partial class AstcDecoder
{
	private struct BlockData
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
		public IntBuffer4 cem;
		public int cem_range;
		/// <summary>
		/// max: 32
		/// </summary>
		public int endpoint_value_num;
		/// <summary>
		/// 4 * 8
		/// </summary>
		public EndpointBuffer endpoints;
		/// <summary>
		/// 144 * 2
		/// </summary>
		public IntBuffer288 weights;
		public IntBuffer144 partition;
	}
}
