namespace AssetRipper.TextureDecoder.Bc;

internal static class Bc7Tables
{
	internal static byte[][] bcdec_bc7_actual_bits_count =
	{
		new byte[] { 4, 6, 5, 7, 5, 7, 7, 5 },
		new byte[] { 0, 0, 0, 0, 6, 8, 7, 5 }
	};

	/// <summary>
	/// There are 64 possible partition sets for a two-region tile.
	/// Each 4x4 block represents a single shape.
	/// Here also every fix-up index has MSB bit set.
	/// </summary>
	internal static byte[][][][] bcdec_bc7_partition_sets =
	{
		new byte[][][]
		{
			new byte[][]
			{
				new byte[] {128, 0, 1, 1},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 0, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 1},
				new byte[] {0, 0, 0, 1},
				new byte[] {0, 0, 0, 1},
				new byte[] {0, 0, 0, 129}
			},
			new byte[][]
			{
				new byte[] {128, 1, 1, 1},
				new byte[] {0, 1, 1, 1},
				new byte[] {0, 1, 1, 1},
				new byte[] {0, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 1},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 0, 0, 1},
				new byte[] {0, 0, 0, 1},
				new byte[] {0, 0, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 1, 1},
				new byte[] {0, 1, 1, 1},
				new byte[] {0, 1, 1, 1},
				new byte[] {1, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 1},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 1, 1, 1},
				new byte[] {1, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 0, 0, 1},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 0, 0, 0},
				new byte[] {0, 0, 0, 1},
				new byte[] {0, 0, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 1, 1},
				new byte[] {0, 1, 1, 1},
				new byte[] {1, 1, 1, 1},
				new byte[] {1, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 0, 0, 1},
				new byte[] {0, 1, 1, 1},
				new byte[] {1, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 0, 0, 0},
				new byte[] {0, 0, 0, 1},
				new byte[] {0, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 1},
				new byte[] {0, 1, 1, 1},
				new byte[] {1, 1, 1, 1},
				new byte[] {1, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 0, 0, 0},
				new byte[] {1, 1, 1, 1},
				new byte[] {1, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {1, 1, 1, 1},
				new byte[] {1, 1, 1, 1},
				new byte[] {1, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 0, 0, 0},
				new byte[] {0, 0, 0, 0},
				new byte[] {1, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {1, 0, 0, 0},
				new byte[] {1, 1, 1, 0},
				new byte[] {1, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 1, 129, 1},
				new byte[] {0, 0, 0, 1},
				new byte[] {0, 0, 0, 0},
				new byte[] {0, 0, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 0, 0, 0},
				new byte[] {129, 0, 0, 0},
				new byte[] {1, 1, 1, 0}
			},
			new byte[][]
			{
				new byte[] {128, 1, 129, 1},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 0, 0, 1},
				new byte[] {0, 0, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 129, 1},
				new byte[] {0, 0, 0, 1},
				new byte[] {0, 0, 0, 0},
				new byte[] {0, 0, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {1, 0, 0, 0},
				new byte[] {129, 1, 0, 0},
				new byte[] {1, 1, 1, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 0, 0, 0},
				new byte[] {129, 0, 0, 0},
				new byte[] {1, 1, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 1, 1, 1},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 0, 0, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 129, 1},
				new byte[] {0, 0, 0, 1},
				new byte[] {0, 0, 0, 1},
				new byte[] {0, 0, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {1, 0, 0, 0},
				new byte[] {129, 0, 0, 0},
				new byte[] {1, 1, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 1, 129, 0},
				new byte[] {0, 1, 1, 0},
				new byte[] {0, 1, 1, 0},
				new byte[] {0, 1, 1, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 129, 1},
				new byte[] {0, 1, 1, 0},
				new byte[] {0, 1, 1, 0},
				new byte[] {1, 1, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 1},
				new byte[] {0, 1, 1, 1},
				new byte[] {129, 1, 1, 0},
				new byte[] {1, 0, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {1, 1, 1, 1},
				new byte[] {129, 1, 1, 1},
				new byte[] {0, 0, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 1, 129, 1},
				new byte[] {0, 0, 0, 1},
				new byte[] {1, 0, 0, 0},
				new byte[] {1, 1, 1, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 129, 1},
				new byte[] {1, 0, 0, 1},
				new byte[] {1, 0, 0, 1},
				new byte[] {1, 1, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 1, 0, 1},
				new byte[] {0, 1, 0, 1},
				new byte[] {0, 1, 0, 1},
				new byte[] {0, 1, 0, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {1, 1, 1, 1},
				new byte[] {0, 0, 0, 0},
				new byte[] {1, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 1, 0, 1},
				new byte[] {1, 0, 129, 0},
				new byte[] {0, 1, 0, 1},
				new byte[] {1, 0, 1, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 1, 1},
				new byte[] {0, 0, 1, 1},
				new byte[] {129, 1, 0, 0},
				new byte[] {1, 1, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 129, 1},
				new byte[] {1, 1, 0, 0},
				new byte[] {0, 0, 1, 1},
				new byte[] {1, 1, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 1, 0, 1},
				new byte[] {0, 1, 0, 1},
				new byte[] {129, 0, 1, 0},
				new byte[] {1, 0, 1, 0}
			},
			new byte[][]
			{
				new byte[] {128, 1, 1, 0},
				new byte[] {1, 0, 0, 1},
				new byte[] {0, 1, 1, 0},
				new byte[] {1, 0, 0, 129}
			},
			new byte[][]
			{
				new byte[] {128, 1, 0, 1},
				new byte[] {1, 0, 1, 0},
				new byte[] {1, 0, 1, 0},
				new byte[] {0, 1, 0, 129}
			},
			new byte[][]
			{
				new byte[] {128, 1, 129, 1},
				new byte[] {0, 0, 1, 1},
				new byte[] {1, 1, 0, 0},
				new byte[] {1, 1, 1, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 1},
				new byte[] {0, 0, 1, 1},
				new byte[] {129, 1, 0, 0},
				new byte[] {1, 0, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 129, 1},
				new byte[] {0, 0, 1, 0},
				new byte[] {0, 1, 0, 0},
				new byte[] {1, 1, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 129, 1},
				new byte[] {1, 0, 1, 1},
				new byte[] {1, 1, 0, 1},
				new byte[] {1, 1, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 1, 129, 0},
				new byte[] {1, 0, 0, 1},
				new byte[] {1, 0, 0, 1},
				new byte[] {0, 1, 1, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 1, 1},
				new byte[] {1, 1, 0, 0},
				new byte[] {1, 1, 0, 0},
				new byte[] {0, 0, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 1, 1, 0},
				new byte[] {0, 1, 1, 0},
				new byte[] {1, 0, 0, 1},
				new byte[] {1, 0, 0, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 1, 129, 0},
				new byte[] {0, 1, 1, 0},
				new byte[] {0, 0, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 1, 0, 0},
				new byte[] {1, 1, 129, 0},
				new byte[] {0, 1, 0, 0},
				new byte[] {0, 0, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 129, 0},
				new byte[] {0, 1, 1, 1},
				new byte[] {0, 0, 1, 0},
				new byte[] {0, 0, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 0, 129, 0},
				new byte[] {0, 1, 1, 1},
				new byte[] {0, 0, 1, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 1, 0, 0},
				new byte[] {129, 1, 1, 0},
				new byte[] {0, 1, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 1, 1, 0},
				new byte[] {1, 1, 0, 0},
				new byte[] {1, 0, 0, 1},
				new byte[] {0, 0, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 1, 1},
				new byte[] {0, 1, 1, 0},
				new byte[] {1, 1, 0, 0},
				new byte[] {1, 0, 0, 129}
			},
			new byte[][]
			{
				new byte[] {128, 1, 129, 0},
				new byte[] {0, 0, 1, 1},
				new byte[] {1, 0, 0, 1},
				new byte[] {1, 1, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 129, 1},
				new byte[] {1, 0, 0, 1},
				new byte[] {1, 1, 0, 0},
				new byte[] {0, 1, 1, 0}
			},
			new byte[][]
			{
				new byte[] {128, 1, 1, 0},
				new byte[] {1, 1, 0, 0},
				new byte[] {1, 1, 0, 0},
				new byte[] {1, 0, 0, 129}
			},
			new byte[][]
			{
				new byte[] {128, 1, 1, 0},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 0, 1, 1},
				new byte[] {1, 0, 0, 129}
			},
			new byte[][]
			{
				new byte[] {128, 1, 1, 1},
				new byte[] {1, 1, 1, 0},
				new byte[] {1, 0, 0, 0},
				new byte[] {0, 0, 0, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 1},
				new byte[] {1, 0, 0, 0},
				new byte[] {1, 1, 1, 0},
				new byte[] {0, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {1, 1, 1, 1},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 0, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 129, 1},
				new byte[] {0, 0, 1, 1},
				new byte[] {1, 1, 1, 1},
				new byte[] {0, 0, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 129, 0},
				new byte[] {0, 0, 1, 0},
				new byte[] {1, 1, 1, 0},
				new byte[] {1, 1, 1, 0}
			},
			new byte[][]
			{
				new byte[] {128, 1, 0, 0},
				new byte[] {0, 1, 0, 0},
				new byte[] {0, 1, 1, 1},
				new byte[] {0, 1, 1, 129}
			}
		},
		new byte[][][]
		{
			new byte[][]
			{
				new byte[] {128, 0, 1, 129},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 2, 2, 1},
				new byte[] {2, 2, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 129},
				new byte[] {0, 0, 1, 1},
				new byte[] {130, 2, 1, 1},
				new byte[] {2, 2, 2, 1}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {2, 0, 0, 1},
				new byte[] {130, 2, 1, 1},
				new byte[] {2, 2, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 2, 2, 130},
				new byte[] {0, 0, 2, 2},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 0, 0, 0},
				new byte[] {129, 1, 2, 2},
				new byte[] {1, 1, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 1, 129},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 0, 2, 2},
				new byte[] {0, 0, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 2, 130},
				new byte[] {0, 0, 2, 2},
				new byte[] {1, 1, 1, 1},
				new byte[] {1, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 1, 1},
				new byte[] {0, 0, 1, 1},
				new byte[] {130, 2, 1, 1},
				new byte[] {2, 2, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 0, 0, 0},
				new byte[] {129, 1, 1, 1},
				new byte[] {2, 2, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {1, 1, 1, 1},
				new byte[] {129, 1, 1, 1},
				new byte[] {2, 2, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {1, 1, 129, 1},
				new byte[] {2, 2, 2, 2},
				new byte[] {2, 2, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 1, 2},
				new byte[] {0, 0, 129, 2},
				new byte[] {0, 0, 1, 2},
				new byte[] {0, 0, 1, 130}
			},
			new byte[][]
			{
				new byte[] {128, 1, 1, 2},
				new byte[] {0, 1, 129, 2},
				new byte[] {0, 1, 1, 2},
				new byte[] {0, 1, 1, 130}
			},
			new byte[][]
			{
				new byte[] {128, 1, 2, 2},
				new byte[] {0, 129, 2, 2},
				new byte[] {0, 1, 2, 2},
				new byte[] {0, 1, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 1, 129},
				new byte[] {0, 1, 1, 2},
				new byte[] {1, 1, 2, 2},
				new byte[] {1, 2, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 1, 129},
				new byte[] {2, 0, 0, 1},
				new byte[] {130, 2, 0, 0},
				new byte[] {2, 2, 2, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 129},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 1, 1, 2},
				new byte[] {1, 1, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 1, 1, 129},
				new byte[] {0, 0, 1, 1},
				new byte[] {130, 0, 0, 1},
				new byte[] {2, 2, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {1, 1, 2, 2},
				new byte[] {129, 1, 2, 2},
				new byte[] {1, 1, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 2, 130},
				new byte[] {0, 0, 2, 2},
				new byte[] {0, 0, 2, 2},
				new byte[] {1, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 1, 1, 129},
				new byte[] {0, 1, 1, 1},
				new byte[] {0, 2, 2, 2},
				new byte[] {0, 2, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 129},
				new byte[] {0, 0, 0, 1},
				new byte[] {130, 2, 2, 1},
				new byte[] {2, 2, 2, 1}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 0, 129, 1},
				new byte[] {0, 1, 2, 2},
				new byte[] {0, 1, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {1, 1, 0, 0},
				new byte[] {130, 2, 129, 0},
				new byte[] {2, 2, 1, 0}
			},
			new byte[][]
			{
				new byte[] {128, 1, 2, 130},
				new byte[] {0, 129, 2, 2},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 0, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 1, 2},
				new byte[] {0, 0, 1, 2},
				new byte[] {129, 1, 2, 2},
				new byte[] {2, 2, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 1, 1, 0},
				new byte[] {1, 2, 130, 1},
				new byte[] {129, 2, 2, 1},
				new byte[] {0, 1, 1, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 1, 129, 0},
				new byte[] {1, 2, 130, 1},
				new byte[] {1, 2, 2, 1}
			},
			new byte[][]
			{
				new byte[] {128, 0, 2, 2},
				new byte[] {1, 1, 0, 2},
				new byte[] {129, 1, 0, 2},
				new byte[] {0, 0, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 1, 1, 0},
				new byte[] {0, 129, 1, 0},
				new byte[] {2, 0, 0, 2},
				new byte[] {2, 2, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 1, 1},
				new byte[] {0, 1, 2, 2},
				new byte[] {0, 1, 130, 2},
				new byte[] {0, 0, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {2, 0, 0, 0},
				new byte[] {130, 2, 1, 1},
				new byte[] {2, 2, 2, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 0, 0, 2},
				new byte[] {129, 1, 2, 2},
				new byte[] {1, 2, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 2, 2, 130},
				new byte[] {0, 0, 2, 2},
				new byte[] {0, 0, 1, 2},
				new byte[] {0, 0, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 1, 129},
				new byte[] {0, 0, 1, 2},
				new byte[] {0, 0, 2, 2},
				new byte[] {0, 2, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 1, 2, 0},
				new byte[] {0, 129, 2, 0},
				new byte[] {0, 1, 130, 0},
				new byte[] {0, 1, 2, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {1, 1, 129, 1},
				new byte[] {2, 2, 130, 2},
				new byte[] {0, 0, 0, 0}
			},
			new byte[][]
			{
				new byte[] {128, 1, 2, 0},
				new byte[] {1, 2, 0, 1},
				new byte[] {130, 0, 129, 2},
				new byte[] {0, 1, 2, 0}
			},
			new byte[][]
			{
				new byte[] {128, 1, 2, 0},
				new byte[] {2, 0, 1, 2},
				new byte[] {129, 130, 0, 1},
				new byte[] {0, 1, 2, 0}
			},
			new byte[][]
			{
				new byte[] {128, 0, 1, 1},
				new byte[] {2, 2, 0, 0},
				new byte[] {1, 1, 130, 2},
				new byte[] {0, 0, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 1, 1},
				new byte[] {1, 1, 130, 2},
				new byte[] {2, 2, 0, 0},
				new byte[] {0, 0, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 1, 0, 129},
				new byte[] {0, 1, 0, 1},
				new byte[] {2, 2, 2, 2},
				new byte[] {2, 2, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 0, 0, 0},
				new byte[] {130, 1, 2, 1},
				new byte[] {2, 1, 2, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 2, 2},
				new byte[] {1, 129, 2, 2},
				new byte[] {0, 0, 2, 2},
				new byte[] {1, 1, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 2, 130},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 0, 2, 2},
				new byte[] {0, 0, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 2, 2, 0},
				new byte[] {1, 2, 130, 1},
				new byte[] {0, 2, 2, 0},
				new byte[] {1, 2, 2, 129}
			},
			new byte[][]
			{
				new byte[] {128, 1, 0, 1},
				new byte[] {2, 2, 130, 2},
				new byte[] {2, 2, 2, 2},
				new byte[] {0, 1, 0, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {2, 1, 2, 1},
				new byte[] {130, 1, 2, 1},
				new byte[] {2, 1, 2, 129}
			},
			new byte[][]
			{
				new byte[] {128, 1, 0, 129},
				new byte[] {0, 1, 0, 1},
				new byte[] {0, 1, 0, 1},
				new byte[] {2, 2, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 2, 2, 130},
				new byte[] {0, 1, 1, 1},
				new byte[] {0, 2, 2, 2},
				new byte[] {0, 1, 1, 129}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 2},
				new byte[] {1, 129, 1, 2},
				new byte[] {0, 0, 0, 2},
				new byte[] {1, 1, 1, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {2, 129, 1, 2},
				new byte[] {2, 1, 1, 2},
				new byte[] {2, 1, 1, 130}
			},
			new byte[][]
			{
				new byte[] {128, 2, 2, 2},
				new byte[] {0, 129, 1, 1},
				new byte[] {0, 1, 1, 1},
				new byte[] {0, 2, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 2},
				new byte[] {1, 1, 1, 2},
				new byte[] {129, 1, 1, 2},
				new byte[] {0, 0, 0, 130}
			},
			new byte[][]
			{
				new byte[] {128, 1, 1, 0},
				new byte[] {0, 129, 1, 0},
				new byte[] {0, 1, 1, 0},
				new byte[] {2, 2, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 0, 0, 0},
				new byte[] {2, 1, 129, 2},
				new byte[] {2, 1, 1, 130}
			},
			new byte[][]
			{
				new byte[] {128, 1, 1, 0},
				new byte[] {0, 129, 1, 0},
				new byte[] {2, 2, 2, 2},
				new byte[] {2, 2, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 2, 2},
				new byte[] {0, 0, 1, 1},
				new byte[] {0, 0, 129, 1},
				new byte[] {0, 0, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 2, 2},
				new byte[] {1, 1, 2, 2},
				new byte[] {129, 1, 2, 2},
				new byte[] {0, 0, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 0},
				new byte[] {0, 0, 0, 0},
				new byte[] {0, 0, 0, 0},
				new byte[] {2, 129, 1, 130}
			},
			new byte[][]
			{
				new byte[] {128, 0, 0, 130},
				new byte[] {0, 0, 0, 1},
				new byte[] {0, 0, 0, 2},
				new byte[] {0, 0, 0, 129}
			},
			new byte[][]
			{
				new byte[] {128, 2, 2, 2},
				new byte[] {1, 2, 2, 2},
				new byte[] {0, 2, 2, 2},
				new byte[] {129, 2, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 1, 0, 129},
				new byte[] {2, 2, 2, 2},
				new byte[] {2, 2, 2, 2},
				new byte[] {2, 2, 2, 130}
			},
			new byte[][]
			{
				new byte[] {128, 1, 1, 129},
				new byte[] {2, 0, 1, 1},
				new byte[] {130, 2, 0, 1},
				new byte[] {2, 2, 2, 0}
			}
		}
	};

	internal static int[] AWeight2 = { 0, 21, 43, 64 };

	internal static int[] AWeight3 = { 0, 9, 18, 27, 37, 46, 55, 64 };

	internal static int[] AWeight4 = { 0, 4, 9, 13, 17, 21, 26, 30, 34, 38, 43, 47, 51, 55, 60, 64 };

	internal const byte bcdec_bc7_sModeHasPBits = 0b11001011;
}
