namespace AssetRipper.TextureDecoder.Bc;

internal static class Bc6hTables
{
	internal static byte[][] ActualBitsCount =
	{
		new byte[] {10, 7, 11, 11, 11, 9, 8, 8, 8, 6, 10, 11, 12, 16},
		new byte[] {5, 6, 5, 4, 4, 5, 6, 5, 5, 6, 10, 9, 8, 4},
		new byte[] {5, 6, 4, 5, 4, 5, 5, 6, 5, 6, 10, 9, 8, 4},
		new byte[] {5, 6, 4, 4, 5, 5, 5, 5, 6, 6, 10, 9, 8, 4}
	};

	/// <summary>
	/// There are 32 possible partition sets for a two-region tile.
	/// Each 4x4 block represents a single shape.
	/// Here also every fix-up index has MSB bit set.
	/// </summary>
	internal static byte[][][] PartitionSets =
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
		}
	};

	internal static int[] AWeight3 = { 0, 9, 18, 27, 37, 46, 55, 64 };

	internal static int[] AWeight4 = { 0, 4, 9, 13, 17, 21, 26, 30, 34, 38, 43, 47, 51, 55, 60, 64 };
}
