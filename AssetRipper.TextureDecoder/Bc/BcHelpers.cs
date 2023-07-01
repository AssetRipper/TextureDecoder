using System.Buffers.Binary;

namespace AssetRipper.TextureDecoder.Bc;

internal static class BcHelpers
{
	public static void DecompressBc1(ReadOnlySpan<byte> compressedBlock, Span<byte> decompressedBlock, int destinationPitch)
	{
		ColorBlock(compressedBlock, decompressedBlock, destinationPitch, 0);
	}

	public static void DecompressBc2(ReadOnlySpan<byte> compressedBlock, Span<byte> decompressedBlock, int destinationPitch)
	{
		ColorBlock(compressedBlock.Slice(8), decompressedBlock, destinationPitch, 1);
		SharpAlphaBlock(compressedBlock, decompressedBlock.Slice(3), destinationPitch);
	}

	public static void DecompressBc3(ReadOnlySpan<byte> compressedBlock, Span<byte> decompressedBlock, int destinationPitch)
	{
		ColorBlock(compressedBlock.Slice(8), decompressedBlock, destinationPitch, 1);
		SmoothAlphaBlock(compressedBlock, decompressedBlock.Slice(3), destinationPitch, 4);
	}

	public static void DecompressBc4(ReadOnlySpan<byte> compressedBlock, Span<byte> decompressedBlock, int destinationPitch)
	{
		SmoothAlphaBlock(compressedBlock, decompressedBlock, destinationPitch, 1);
	}

	public static void DecompressBc5(ReadOnlySpan<byte> compressedBlock, Span<byte> decompressedBlock, int destinationPitch)
	{
		SmoothAlphaBlock(compressedBlock, decompressedBlock, destinationPitch, 2);
		SmoothAlphaBlock(compressedBlock.Slice(8), decompressedBlock.Slice(1), destinationPitch, 2);
	}

	public static void DecompressBc6h(ReadOnlySpan<byte> compressedBlock, Span<byte> decompressedBlock, int destinationPitch, bool isSigned)
	{
		BitStream bstream = new()
		{
			low = BinaryPrimitives.ReadUInt64LittleEndian(compressedBlock),
			high = BinaryPrimitives.ReadUInt64LittleEndian(compressedBlock.Slice(sizeof(ulong)))
		};
		Span<int> r = stackalloc int[4]; // wxyz
		Span<int> g = stackalloc int[4];
		Span<int> b = stackalloc int[4];

		int decompressedOffset = 0;

		r.Clear();
		g.Clear();
		b.Clear();

		int mode;

		//modes >= 11 (10 in my code) are using 0 one, others will read it from the bitstream
		int partition;

		switch (ReadBc6hModeBits(ref bstream))
		{
			// mode 1 
			case 0b00:
				{
					// Partitition indices: 46 bits
					// Partition: 5 bits
					// Color Endpoints: 75 bits (10.555, 10.555, 10.555)
					g[2] |= bstream.ReadBit() << 4; // gy[4]
					b[2] |= bstream.ReadBit() << 4; // by[4]
					b[3] |= bstream.ReadBit() << 4; // bz[4]
					r[0] |= bstream.ReadBits(10); // rw[9:0]
					g[0] |= bstream.ReadBits(10); // gw[9:0]
					b[0] |= bstream.ReadBits(10); // bw[9:0]
					r[1] |= bstream.ReadBits(5); // rx[4:0]
					g[3] |= bstream.ReadBit() << 4; // gz[4]
					g[2] |= bstream.ReadBits(4); // gy[3:0]
					g[1] |= bstream.ReadBits(5); // gx[4:0]
					b[3] |= bstream.ReadBit(); // bz[0]
					g[3] |= bstream.ReadBits(4); // gz[3:0]
					b[1] |= bstream.ReadBits(5); // bx[4:0]
					b[3] |= bstream.ReadBit() << 1; // bz[1]
					b[2] |= bstream.ReadBits(4); // by[3:0]
					r[2] |= bstream.ReadBits(5); // ry[4:0]
					b[3] |= bstream.ReadBit() << 2; // bz[2]
					r[3] |= bstream.ReadBits(5); // rz[4:0]
					b[3] |= bstream.ReadBit() << 3; // bz[3]
					partition = bstream.ReadBits(5); // d[4:0]
					mode = 0;
				}
				break;

			// mode 2 
			case 0b01:
				{
					// Partitition indices: 46 bits
					// Partition: 5 bits
					// Color Endpoints: 75 bits (7666, 7666, 7666)
					g[2] |= bstream.ReadBit() << 5; // gy[5]
					g[3] |= bstream.ReadBit() << 4; // gz[4]
					g[3] |= bstream.ReadBit() << 5; // gz[5]
					r[0] |= bstream.ReadBits(7); // rw[6:0]
					b[3] |= bstream.ReadBit(); // bz[0]
					b[3] |= bstream.ReadBit() << 1; // bz[1]
					b[2] |= bstream.ReadBit() << 4; // by[4]
					g[0] |= bstream.ReadBits(7); // gw[6:0]
					b[2] |= bstream.ReadBit() << 5; // by[5]
					b[3] |= bstream.ReadBit() << 2; // bz[2]
					g[2] |= bstream.ReadBit() << 4; // gy[4]
					b[0] |= bstream.ReadBits(7); // bw[6:0]
					b[3] |= bstream.ReadBit() << 3; // bz[3]
					b[3] |= bstream.ReadBit() << 5; // bz[5]
					b[3] |= bstream.ReadBit() << 4; // bz[4]
					r[1] |= bstream.ReadBits(6); // rx[5:0]
					g[2] |= bstream.ReadBits(4); // gy[3:0]
					g[1] |= bstream.ReadBits(6); // gx[5:0]
					g[3] |= bstream.ReadBits(4); // gz[3:0]
					b[1] |= bstream.ReadBits(6); // bx[5:0]
					b[2] |= bstream.ReadBits(4); // by[3:0]
					r[2] |= bstream.ReadBits(6); // ry[5:0]
					r[3] |= bstream.ReadBits(6); // rz[5:0]
					partition = bstream.ReadBits(5); // d[4:0]
					mode = 1;
				}
				break;

			// mode 3 
			case 0b00010:
				{
					// Partitition indices: 46 bits
					// Partition: 5 bits
					// Color Endpoints: 72 bits (11.555, 11.444, 11.444)
					r[0] |= bstream.ReadBits(10); // rw[9:0]
					g[0] |= bstream.ReadBits(10); // gw[9:0]
					b[0] |= bstream.ReadBits(10); // bw[9:0]
					r[1] |= bstream.ReadBits(5); // rx[4:0]
					r[0] |= bstream.ReadBit() << 10; // rw[10]
					g[2] |= bstream.ReadBits(4); // gy[3:0]
					g[1] |= bstream.ReadBits(4); // gx[3:0]
					g[0] |= bstream.ReadBit() << 10; // gw[10]
					b[3] |= bstream.ReadBit(); // bz[0]
					g[3] |= bstream.ReadBits(4); // gz[3:0]
					b[1] |= bstream.ReadBits(4); // bx[3:0]
					b[0] |= bstream.ReadBit() << 10; // bw[10]
					b[3] |= bstream.ReadBit() << 1; // bz[1]
					b[2] |= bstream.ReadBits(4); // by[3:0]
					r[2] |= bstream.ReadBits(5); // ry[4:0]
					b[3] |= bstream.ReadBit() << 2; // bz[2]
					r[3] |= bstream.ReadBits(5); // rz[4:0]
					b[3] |= bstream.ReadBit() << 3; // bz[3]
					partition = bstream.ReadBits(5); // d[4:0]
					mode = 2;
				}
				break;

			// mode 4 
			case 0b00110:
				{
					// Partitition indices: 46 bits
					// Partition: 5 bits
					// Color Endpoints: 72 bits (11.444, 11.555, 11.444)
					r[0] |= bstream.ReadBits(10); // rw[9:0]
					g[0] |= bstream.ReadBits(10); // gw[9:0]
					b[0] |= bstream.ReadBits(10); // bw[9:0]
					r[1] |= bstream.ReadBits(4); // rx[3:0]
					r[0] |= bstream.ReadBit() << 10; // rw[10]
					g[3] |= bstream.ReadBit() << 4; // gz[4]
					g[2] |= bstream.ReadBits(4); // gy[3:0]
					g[1] |= bstream.ReadBits(5); // gx[4:0]
					g[0] |= bstream.ReadBit() << 10; // gw[10]
					g[3] |= bstream.ReadBits(4); // gz[3:0]
					b[1] |= bstream.ReadBits(4); // bx[3:0]
					b[0] |= bstream.ReadBit() << 10; // bw[10]
					b[3] |= bstream.ReadBit() << 1; // bz[1]
					b[2] |= bstream.ReadBits(4); // by[3:0]
					r[2] |= bstream.ReadBits(4); // ry[3:0]
					b[3] |= bstream.ReadBit(); // bz[0]
					b[3] |= bstream.ReadBit() << 2; // bz[2]
					r[3] |= bstream.ReadBits(4); // rz[3:0]
					g[2] |= bstream.ReadBit() << 4; // gy[4]
					b[3] |= bstream.ReadBit() << 3; // bz[3]
					partition = bstream.ReadBits(5); // d[4:0]
					mode = 3;
				}
				break;

			// mode 5 
			case 0b01010:
				{
					// Partitition indices: 46 bits
					// Partition: 5 bits
					// Color Endpoints: 72 bits (11.444, 11.444, 11.555)
					r[0] |= bstream.ReadBits(10); // rw[9:0]
					g[0] |= bstream.ReadBits(10); // gw[9:0]
					b[0] |= bstream.ReadBits(10); // bw[9:0]
					r[1] |= bstream.ReadBits(4); // rx[3:0]
					r[0] |= bstream.ReadBit() << 10; // rw[10]
					b[2] |= bstream.ReadBit() << 4; // by[4]
					g[2] |= bstream.ReadBits(4); // gy[3:0]
					g[1] |= bstream.ReadBits(4); // gx[3:0]
					g[0] |= bstream.ReadBit() << 10; // gw[10]
					b[3] |= bstream.ReadBit(); // bz[0]
					g[3] |= bstream.ReadBits(4); // gz[3:0]
					b[1] |= bstream.ReadBits(5); // bx[4:0]
					b[0] |= bstream.ReadBit() << 10; // bw[10]
					b[2] |= bstream.ReadBits(4); // by[3:0]
					r[2] |= bstream.ReadBits(4); // ry[3:0]
					b[3] |= bstream.ReadBit() << 1; // bz[1]
					b[3] |= bstream.ReadBit() << 2; // bz[2]
					r[3] |= bstream.ReadBits(4); // rz[3:0]
					b[3] |= bstream.ReadBit() << 4; // bz[4]
					b[3] |= bstream.ReadBit() << 3; // bz[3]
					partition = bstream.ReadBits(5); // d[4:0]
					mode = 4;
				}
				break;

			// mode 6 
			case 0b01110:
				{
					// Partitition indices: 46 bits
					// Partition: 5 bits
					// Color Endpoints: 72 bits (9555, 9555, 9555)
					r[0] |= bstream.ReadBits(9); // rw[8:0]
					b[2] |= bstream.ReadBit() << 4; // by[4]
					g[0] |= bstream.ReadBits(9); // gw[8:0]
					g[2] |= bstream.ReadBit() << 4; // gy[4]
					b[0] |= bstream.ReadBits(9); // bw[8:0]
					b[3] |= bstream.ReadBit() << 4; // bz[4]
					r[1] |= bstream.ReadBits(5); // rx[4:0]
					g[3] |= bstream.ReadBit() << 4; // gz[4]
					g[2] |= bstream.ReadBits(4); // gy[3:0]
					g[1] |= bstream.ReadBits(5); // gx[4:0]
					b[3] |= bstream.ReadBit(); // bz[0]
					g[3] |= bstream.ReadBits(4); // gx[3:0]
					b[1] |= bstream.ReadBits(5); // bx[4:0]
					b[3] |= bstream.ReadBit() << 1; // bz[1]
					b[2] |= bstream.ReadBits(4); // by[3:0]
					r[2] |= bstream.ReadBits(5); // ry[4:0]
					b[3] |= bstream.ReadBit() << 2; // bz[2]
					r[3] |= bstream.ReadBits(5); // rz[4:0]
					b[3] |= bstream.ReadBit() << 3; // bz[3]
					partition = bstream.ReadBits(5); // d[4:0]
					mode = 5;
				}
				break;

			// mode 7 
			case 0b10010:
				{
					// Partitition indices: 46 bits
					// Partition: 5 bits
					// Color Endpoints: 72 bits (8666, 8555, 8555)
					r[0] |= bstream.ReadBits(8); // rw[7:0]
					g[3] |= bstream.ReadBit() << 4; // gz[4]
					b[2] |= bstream.ReadBit() << 4; // by[4]
					g[0] |= bstream.ReadBits(8); // gw[7:0]
					b[3] |= bstream.ReadBit() << 2; // bz[2]
					g[2] |= bstream.ReadBit() << 4; // gy[4]
					b[0] |= bstream.ReadBits(8); // bw[7:0]
					b[3] |= bstream.ReadBit() << 3; // bz[3]
					b[3] |= bstream.ReadBit() << 4; // bz[4]
					r[1] |= bstream.ReadBits(6); // rx[5:0]
					g[2] |= bstream.ReadBits(4); // gy[3:0]
					g[1] |= bstream.ReadBits(5); // gx[4:0]
					b[3] |= bstream.ReadBit(); // bz[0]
					g[3] |= bstream.ReadBits(4); // gz[3:0]
					b[1] |= bstream.ReadBits(5); // bx[4:0]
					b[3] |= bstream.ReadBit() << 1; // bz[1]
					b[2] |= bstream.ReadBits(4); // by[3:0]
					r[2] |= bstream.ReadBits(6); // ry[5:0]
					r[3] |= bstream.ReadBits(6); // rz[5:0]
					partition = bstream.ReadBits(5); // d[4:0]
					mode = 6;
				}
				break;

			// mode 8 
			case 0b10110:
				{
					// Partitition indices: 46 bits
					// Partition: 5 bits
					// Color Endpoints: 72 bits (8555, 8666, 8555)
					r[0] |= bstream.ReadBits(8); // rw[7:0]
					b[3] |= bstream.ReadBit(); // bz[0]
					b[2] |= bstream.ReadBit() << 4; // by[4]
					g[0] |= bstream.ReadBits(8); // gw[7:0]
					g[2] |= bstream.ReadBit() << 5; // gy[5]
					g[2] |= bstream.ReadBit() << 4; // gy[4]
					b[0] |= bstream.ReadBits(8); // bw[7:0]
					g[3] |= bstream.ReadBit() << 5; // gz[5]
					b[3] |= bstream.ReadBit() << 4; // bz[4]
					r[1] |= bstream.ReadBits(5); // rx[4:0]
					g[3] |= bstream.ReadBit() << 4; // gz[4]
					g[2] |= bstream.ReadBits(4); // gy[3:0]
					g[1] |= bstream.ReadBits(6); // gx[5:0]
					g[3] |= bstream.ReadBits(4); // zx[3:0]
					b[1] |= bstream.ReadBits(5); // bx[4:0]
					b[3] |= bstream.ReadBit() << 1; // bz[1]
					b[2] |= bstream.ReadBits(4); // by[3:0]
					r[2] |= bstream.ReadBits(5); // ry[4:0]
					b[3] |= bstream.ReadBit() << 2; // bz[2]
					r[3] |= bstream.ReadBits(5); // rz[4:0]
					b[3] |= bstream.ReadBit() << 3; // bz[3]
					partition = bstream.ReadBits(5); // d[4:0]
					mode = 7;
				}
				break;

			// mode 9 
			case 0b11010:
				{
					// Partitition indices: 46 bits
					// Partition: 5 bits
					// Color Endpoints: 72 bits (8555, 8555, 8666)
					r[0] |= bstream.ReadBits(8); // rw[7:0]
					b[3] |= bstream.ReadBit() << 1; // bz[1]
					b[2] |= bstream.ReadBit() << 4; // by[4]
					g[0] |= bstream.ReadBits(8); // gw[7:0]
					b[2] |= bstream.ReadBit() << 5; // by[5]
					g[2] |= bstream.ReadBit() << 4; // gy[4]
					b[0] |= bstream.ReadBits(8); // bw[7:0]
					b[3] |= bstream.ReadBit() << 5; // bz[5]
					b[3] |= bstream.ReadBit() << 4; // bz[4]
					r[1] |= bstream.ReadBits(5); // bw[4:0]
					g[3] |= bstream.ReadBit() << 4; // gz[4]
					g[2] |= bstream.ReadBits(4); // gy[3:0]
					g[1] |= bstream.ReadBits(5); // gx[4:0]
					b[3] |= bstream.ReadBit(); // bz[0]
					g[3] |= bstream.ReadBits(4); // gz[3:0]
					b[1] |= bstream.ReadBits(6); // bx[5:0]
					b[2] |= bstream.ReadBits(4); // by[3:0]
					r[2] |= bstream.ReadBits(5); // ry[4:0]
					b[3] |= bstream.ReadBit() << 2; // bz[2]
					r[3] |= bstream.ReadBits(5); // rz[4:0]
					b[3] |= bstream.ReadBit() << 3; // bz[3]
					partition = bstream.ReadBits(5); // d[4:0]
					mode = 8;
				}
				break;

			// mode 10 
			case 0b11110:
				{
					// Partitition indices: 46 bits
					// Partition: 5 bits
					// Color Endpoints: 72 bits (6666, 6666, 6666)
					r[0] |= bstream.ReadBits(6); // rw[5:0]
					g[3] |= bstream.ReadBit() << 4; // gz[4]
					b[3] |= bstream.ReadBit(); // bz[0]
					b[3] |= bstream.ReadBit() << 1; // bz[1]
					b[2] |= bstream.ReadBit() << 4; // by[4]
					g[0] |= bstream.ReadBits(6); // gw[5:0]
					g[2] |= bstream.ReadBit() << 5; // gy[5]
					b[2] |= bstream.ReadBit() << 5; // by[5]
					b[3] |= bstream.ReadBit() << 2; // bz[2]
					g[2] |= bstream.ReadBit() << 4; // gy[4]
					b[0] |= bstream.ReadBits(6); // bw[5:0]
					g[3] |= bstream.ReadBit() << 5; // gz[5]
					b[3] |= bstream.ReadBit() << 3; // bz[3]
					b[3] |= bstream.ReadBit() << 5; // bz[5]
					b[3] |= bstream.ReadBit() << 4; // bz[4]
					r[1] |= bstream.ReadBits(6); // rx[5:0]
					g[2] |= bstream.ReadBits(4); // gy[3:0]
					g[1] |= bstream.ReadBits(6); // gx[5:0]
					g[3] |= bstream.ReadBits(4); // gz[3:0]
					b[1] |= bstream.ReadBits(6); // bx[5:0]
					b[2] |= bstream.ReadBits(4); // by[3:0]
					r[2] |= bstream.ReadBits(6); // ry[5:0]
					r[3] |= bstream.ReadBits(6); // rz[5:0]
					partition = bstream.ReadBits(5); // d[4:0]
					mode = 9;
				}
				break;

			// mode 11 
			case 0b00011:
				{
					// Partitition indices: 63 bits
					// Partition: 0 bits
					// Color Endpoints: 60 bits (10.10, 10.10, 10.10)
					r[0] |= bstream.ReadBits(10); // rw[9:0]
					g[0] |= bstream.ReadBits(10); // gw[9:0]
					b[0] |= bstream.ReadBits(10); // bw[9:0]
					r[1] |= bstream.ReadBits(10); // rx[9:0]
					g[1] |= bstream.ReadBits(10); // gx[9:0]
					b[1] |= bstream.ReadBits(10); // bx[9:0]
					partition = 0;
					mode = 10;
				}
				break;

			// mode 12 
			case 0b00111:
				{
					// Partitition indices: 63 bits
					// Partition: 0 bits
					// Color Endpoints: 60 bits (11.9, 11.9, 11.9)
					r[0] |= bstream.ReadBits(10); // rw[9:0]
					g[0] |= bstream.ReadBits(10); // gw[9:0]
					b[0] |= bstream.ReadBits(10); // bw[9:0]
					r[1] |= bstream.ReadBits(9); // rx[8:0]
					r[0] |= bstream.ReadBit() << 10; // rw[10]
					g[1] |= bstream.ReadBits(9); // gx[8:0]
					g[0] |= bstream.ReadBit() << 10; // gw[10]
					b[1] |= bstream.ReadBits(9); // bx[8:0]
					b[0] |= bstream.ReadBit() << 10; // bw[10]
					partition = 0;
					mode = 11;
				}
				break;

			// mode 13 
			case 0b01011:
				{
					// Partitition indices: 63 bits
					// Partition: 0 bits
					// Color Endpoints: 60 bits (12.8, 12.8, 12.8)
					r[0] |= bstream.ReadBits(10); // rw[9:0]
					g[0] |= bstream.ReadBits(10); // gw[9:0]
					b[0] |= bstream.ReadBits(10); // bw[9:0]
					r[1] |= bstream.ReadBits(8); // rx[7:0]
					r[0] |= bstream.ReadBitsReversed(2) << 10; // rx[10:11]
					g[1] |= bstream.ReadBits(8); // gx[7:0]
					g[0] |= bstream.ReadBitsReversed(2) << 10; // gx[10:11]
					b[1] |= bstream.ReadBits(8); // bx[7:0]
					b[0] |= bstream.ReadBitsReversed(2) << 10; // bx[10:11]
					partition = 0;
					mode = 12;
				}
				break;

			// mode 14 
			case 0b01111:
				{
					// Partitition indices: 63 bits
					// Partition: 0 bits
					// Color Endpoints: 60 bits (16.4, 16.4, 16.4)
					r[0] |= bstream.ReadBits(10); // rw[9:0]
					g[0] |= bstream.ReadBits(10); // gw[9:0]
					b[0] |= bstream.ReadBits(10); // bw[9:0]
					r[1] |= bstream.ReadBits(4); // rx[3:0]
					r[0] |= bstream.ReadBitsReversed(6) << 10; // rw[10:15]
					g[1] |= bstream.ReadBits(4); // gx[3:0]
					g[0] |= bstream.ReadBitsReversed(6) << 10; // gw[10:15]
					b[1] |= bstream.ReadBits(4); // bx[3:0]
					b[0] |= bstream.ReadBitsReversed(6) << 10; // bw[10:15]
					partition = 0;
					mode = 13;
				}
				break;

			default:
				{
					// Modes 10011, 10111, 11011, and 11111(not shown) are reserved.
					// Do not use these in your encoder. If the hardware is passed blocks
					// with one of these modes specified, the resulting decompressed block
					// must contain all zeroes in all channels except for the alpha channel.
					for (int i = 0; i < 4; ++i)
					{
						for (int j = 0; j < 4; ++j)
						{
							Span<byte> pixel = decompressedBlock.Slice(decompressedOffset + (j * 3 * sizeof(ushort)), 3 * sizeof(ushort));
							pixel.Clear();
						}
						decompressedOffset += destinationPitch * sizeof(ushort);
					}

					return;
				}
		}

		int numPartitions = mode >= 10 ? 0 : 1;
		byte actualBits0Mode = Bc6hTables.ActualBitsCount[0][mode];
		if (isSigned)
		{
			r[0] = ExtendSign(r[0], actualBits0Mode);
			g[0] = ExtendSign(g[0], actualBits0Mode);
			b[0] = ExtendSign(b[0], actualBits0Mode);
		}

		// Mode 11 (like Mode 10) does not use delta compression,
		// and instead stores both color endpoints explicitly.
		if ((mode != 9 && mode != 10) || isSigned)
		{
			for (int i = 1; i < (numPartitions + 1) * 2; ++i)
			{
				r[i] = ExtendSign(r[i], Bc6hTables.ActualBitsCount[1][mode]);
				g[i] = ExtendSign(g[i], Bc6hTables.ActualBitsCount[2][mode]);
				b[i] = ExtendSign(b[i], Bc6hTables.ActualBitsCount[3][mode]);
			}
		}

		if (mode != 9 && mode != 10)
		{
			for (int i = 1; i < (numPartitions + 1) * 2; ++i)
			{
				r[i] = TransformInverse(r[i], r[0], actualBits0Mode, isSigned);
				g[i] = TransformInverse(g[i], g[0], actualBits0Mode, isSigned);
				b[i] = TransformInverse(b[i], b[0], actualBits0Mode, isSigned);
			}
		}

		for (int i = 0; i < (numPartitions + 1) * 2; i++)
		{
			r[i] = Unquantize(r[i], actualBits0Mode, isSigned);
			g[i] = Unquantize(g[i], actualBits0Mode, isSigned);
			b[i] = Unquantize(b[i], actualBits0Mode, isSigned);
		}

		ReadOnlySpan<int> weights = (mode >= 10) ? Bc6hTables.AWeight4 : Bc6hTables.AWeight3;
		for (int i = 0; i < 4; ++i)
		{
			for (int j = 0; j < 4; ++j)
			{
				int partitionSet = (mode >= 10) ? ((i | j) != 0 ? 0 : 128) : Bc6hTables.PartitionSets[partition][i][j];

				int indexBits = (mode >= 10) ? 4 : 3;
				// fix-up index is specified with one less bit 
				// The fix-up index for subset 0 is always index 0 
				if ((partitionSet & 0x80) != 0)
				{
					indexBits--;
				}
				partitionSet &= 0x01;

				int index = bstream.ReadBits(indexBits);

				int ep_i = (partitionSet * 2);

				int pixelOffset = decompressedOffset + (j * 3 * sizeof(ushort));

				int rOffset = pixelOffset + 0 * sizeof(ushort);
				ushort rFinal = FinishUnquantize(Interpolate(r[ep_i], r[ep_i + 1], weights, index), isSigned);
				BinaryPrimitives.WriteUInt16LittleEndian(decompressedBlock.Slice(rOffset), rFinal);

				int gOffset = pixelOffset + 1 * sizeof(ushort);
				ushort gFinal = FinishUnquantize(Interpolate(g[ep_i], g[ep_i + 1], weights, index), isSigned);
				BinaryPrimitives.WriteUInt16LittleEndian(decompressedBlock.Slice(gOffset), gFinal);

				int bOffset = pixelOffset + 2 * sizeof(ushort);
				ushort bFinal = FinishUnquantize(Interpolate(b[ep_i], b[ep_i + 1], weights, index), isSigned);
				BinaryPrimitives.WriteUInt16LittleEndian(decompressedBlock.Slice(bOffset), bFinal);
			}

			decompressedOffset += destinationPitch * sizeof(ushort);
		}

		static int ReadBc6hModeBits(ref BitStream bstream)
		{
			int twoBits = bstream.ReadBits(2);
			if (twoBits > 1)
			{
				int threeBits = bstream.ReadBits(3);
				return (threeBits << 2) | twoBits;
			}
			else
			{
				return twoBits;
			}
		}
	}

	public static void DecompressBc7(ReadOnlySpan<byte> compressedBlock, Span<byte> decompressedBlock, int destinationPitch)
	{
		BitStream bstream = new()
		{
			low = BinaryPrimitives.ReadUInt64LittleEndian(compressedBlock),
			high = BinaryPrimitives.ReadUInt64LittleEndian(compressedBlock.Slice(sizeof(ulong)))
		};
		int[][] endpoints = CreateRectangularArray<int>(6, 4);
		int[][] indices = CreateRectangularArray<int>(4, 4);

		int decompressedOffset = 0;

		int mode = ReadBc7Mode(ref bstream);

		// unexpected mode, clear the block (transparent black)
		if (mode >= 8)
		{
			for (int i = 0; i < 4; ++i)
			{
				for (int j = 0; j < 4; ++j)
				{
					decompressedBlock[decompressedOffset + (j * 4) + 0] = 0;
					decompressedBlock[decompressedOffset + (j * 4) + 1] = 0;
					decompressedBlock[decompressedOffset + (j * 4) + 2] = 0;
					decompressedBlock[decompressedOffset + (j * 4) + 3] = 0;
				}
				decompressedOffset += destinationPitch;
			}

			return;
		}

		int partition = 0;
		int numPartitions = 1;
		int rotation = 0;
		int indexSelectionBit = 0;

		if (mode == 0 || mode == 1 || mode == 2 || mode == 3 || mode == 7)
		{
			numPartitions = (mode == 0 || mode == 2) ? 3 : 2;
			partition = bstream.ReadBits((mode == 0) ? 4 : 6);
		}

		int numEndpoints = numPartitions * 2;

		if (mode == 4 || mode == 5)
		{
			rotation = bstream.ReadBits(2);

			if (mode == 4)
			{
				indexSelectionBit = bstream.ReadBit();
			}
		}

		// Extract endpoints 
		// RGB 
		for (int i = 0; i < 3; ++i)
		{
			for (int j = 0; j < numEndpoints; ++j)
			{
				endpoints[j][i] = bstream.ReadBits(Bc7Tables.ActualBitsCount[0][mode]);
			}
		}
		// Alpha (if any) 
		if (Bc7Tables.ActualBitsCount[1][mode] > 0)
		{
			for (int j = 0; j < numEndpoints; ++j)
			{
				endpoints[j][3] = bstream.ReadBits(Bc7Tables.ActualBitsCount[1][mode]);
			}
		}

		// Fully decode endpoints 
		// First handle modes that have P-bits 
		if (mode == 0 || mode == 1 || mode == 3 || mode == 6 || mode == 7)
		{
			for (int i = 0; i < numEndpoints; ++i)
			{
				// component-wise left-shift 
				for (int j = 0; j < 4; ++j)
				{
					endpoints[i][j] <<= 1;
				}
			}

			// if P-bit is shared 
			if (mode == 1)
			{
				int i = bstream.ReadBit();
				int j = bstream.ReadBit();

				// rgb component-wise insert pbits 
				for (int k = 0; k < 3; ++k)
				{
					endpoints[0][k] |= i;
					endpoints[1][k] |= i;
					endpoints[2][k] |= j;
					endpoints[3][k] |= j;
				}
			}
			else if ((Bc7Tables.bcdec_bc7_sModeHasPBits & (1 << mode)) != 0)
			{
				// unique P-bit per endpoint 
				for (int i = 0; i < numEndpoints; ++i)
				{
					int j = bstream.ReadBit();
					for (int k = 0; k < 4; ++k)
					{
						endpoints[i][k] |= j;
					}
				}
			}
		}

		for (int i = 0; i < numEndpoints; ++i)
		{
			// get color components precision including pbit 
			int j = Bc7Tables.ActualBitsCount[0][mode] + ((Bc7Tables.bcdec_bc7_sModeHasPBits >> mode) & 1);

			for (int k = 0; k < 3; ++k)
			{
				// left shift endpoint components so that their MSB lies in bit 7 
				endpoints[i][k] = endpoints[i][k] << (8 - j);
				// Replicate each component's MSB into the LSBs revealed by the left-shift operation above 
				endpoints[i][k] = endpoints[i][k] | (endpoints[i][k] >> j);
			}

			// get alpha component precision including pbit 
			j = Bc7Tables.ActualBitsCount[1][mode] + ((Bc7Tables.bcdec_bc7_sModeHasPBits >> mode) & 1);

			// left shift endpoint components so that their MSB lies in bit 7 
			endpoints[i][3] = endpoints[i][3] << (8 - j);
			// Replicate each component's MSB into the LSBs revealed by the left-shift operation above 
			endpoints[i][3] = endpoints[i][3] | (endpoints[i][3] >> j);
		}

		// If this mode does not explicitly define the alpha component 
		// set alpha equal to 1.0 
		if (Bc7Tables.ActualBitsCount[1][mode] == 0)
		{
			for (int j = 0; j < numEndpoints; ++j)
			{
				endpoints[j][3] = 0xFF;
			}
		}

		// Determine weights tables 
		int indexBits = (mode == 0 || mode == 1) ? 3 : ((mode == 6) ? 4 : 2);
		int indexBits2 = (mode == 4) ? 3 : ((mode == 5) ? 2 : 0);
		int[] weights = (indexBits == 2) ? Bc7Tables.AWeight2 : ((indexBits == 3) ? Bc7Tables.AWeight3 : Bc7Tables.AWeight4);
		int[] weights2 = (indexBits2 == 2) ? Bc7Tables.AWeight2 : Bc7Tables.AWeight3;

		// Quite inconvenient that indices aren't interleaved so we have to make 2 passes here 
		// Pass #1: collecting color indices 
		for (int i = 0; i < 4; ++i)
		{
			for (int j = 0; j < 4; ++j)
			{
				int partitionSet = (numPartitions == 1) ? ((i | j) != 0 ? 0 : 128) : Bc7Tables.PartitionSets[numPartitions - 2][partition][i][j];

				indexBits = (mode == 0 || mode == 1) ? 3 : ((mode == 6) ? 4 : 2);
				// fix-up index is specified with one less bit 
				// The fix-up index for subset 0 is always index 0 
				if ((partitionSet & 0x80) != 0)
				{
					indexBits--;
				}

				indices[i][j] = bstream.ReadBits(indexBits);
			}
		}

		// Pass #2: reading alpha indices (if any) and interpolating & rotating 
		for (int i = 0; i < 4; ++i)
		{
			for (int j = 0; j < 4; ++j)
			{
				int partitionSet = (numPartitions == 1)
					? ((i | j) != 0 ? 0 : 128)
					: Bc7Tables.PartitionSets[numPartitions - 2][partition][i][j];
				partitionSet &= 0x03;

				int index = indices[i][j];

				int r;
				int g;
				int b;
				int a;

				if (indexBits2 == 0)
				{
					r = Interpolate(endpoints[partitionSet * 2][0], endpoints[(partitionSet * 2) + 1][0], weights, index);
					g = Interpolate(endpoints[partitionSet * 2][1], endpoints[(partitionSet * 2) + 1][1], weights, index);
					b = Interpolate(endpoints[partitionSet * 2][2], endpoints[(partitionSet * 2) + 1][2], weights, index);
					a = Interpolate(endpoints[partitionSet * 2][3], endpoints[(partitionSet * 2) + 1][3], weights, index);
				}
				else
				{
					int index2 = bstream.ReadBits((i | j) != 0 ? indexBits2 : (indexBits2 - 1));
					// The index value for interpolating color comes from the secondary index bits for the texel
					// if the mode has an index selection bit and its value is one, and from the primary index bits otherwise.
					// The alpha index comes from the secondary index bits if the block has a secondary index
					// and the block either doesn't have an index selection bit or that bit is zero,
					// and from the primary index bits otherwise.
					if (indexSelectionBit == 0)
					{
						r = Interpolate(endpoints[partitionSet * 2][0], endpoints[(partitionSet * 2) + 1][0], weights, index);
						g = Interpolate(endpoints[partitionSet * 2][1], endpoints[(partitionSet * 2) + 1][1], weights, index);
						b = Interpolate(endpoints[partitionSet * 2][2], endpoints[(partitionSet * 2) + 1][2], weights, index);
						a = Interpolate(endpoints[partitionSet * 2][3], endpoints[(partitionSet * 2) + 1][3], weights2, index2);
					}
					else
					{
						r = Interpolate(endpoints[partitionSet * 2][0], endpoints[(partitionSet * 2) + 1][0], weights2, index2);
						g = Interpolate(endpoints[partitionSet * 2][1], endpoints[(partitionSet * 2) + 1][1], weights2, index2);
						b = Interpolate(endpoints[partitionSet * 2][2], endpoints[(partitionSet * 2) + 1][2], weights2, index2);
						a = Interpolate(endpoints[partitionSet * 2][3], endpoints[(partitionSet * 2) + 1][3], weights, index);
					}
				}

				switch (rotation)
				{
					case 1:
						{
							// 01 – Block format is Scalar(R) Vector(AGB) - swap A and R
							SwapValues(ref a, ref r);
						}
						break;
					case 2:
						{
							// 10 – Block format is Scalar(G) Vector(RAB) - swap A and G
							SwapValues(ref a, ref g);
						}
						break;
					case 3:
						{
							// 11 - Block format is Scalar(B) Vector(RGA) - swap A and B
							SwapValues(ref a, ref b);
						}
						break;
				}

				decompressedBlock[decompressedOffset + (j * 4) + 0] = (byte)r;
				decompressedBlock[decompressedOffset + (j * 4) + 1] = (byte)g;
				decompressedBlock[decompressedOffset + (j * 4) + 2] = (byte)b;
				decompressedBlock[decompressedOffset + (j * 4) + 3] = (byte)a;
			}

			decompressedOffset += destinationPitch;
		}

		static int ReadBc7Mode(ref BitStream bstream)
		{
			int i = 0;
			while (i < 8 && bstream.ReadBit() == 0)
			{
				i++;
			}
			return i;
		}
	}


	public static void ColorBlock(ReadOnlySpan<byte> compressedBlock, Span<byte> decompressedBlock, int destinationPitch, int onlyOpaqueMode)
	{
		Span<uint> refColors = stackalloc uint[4]; // 0xAABBGGRR

		ushort c0 = BinaryPrimitives.ReadUInt16LittleEndian(compressedBlock);
		ushort c1 = BinaryPrimitives.ReadUInt16LittleEndian(compressedBlock.Slice(sizeof(ushort)));

		// Expand 565 ref colors to 888 
		uint r0 = (uint)(((((c0 >> 11) & 0x1F) * 527) + 23) >> 6);
		uint g0 = (uint)(((((c0 >> 5) & 0x3F) * 259) + 33) >> 6);
		uint b0 = (uint)((((c0 & 0x1F) * 527) + 23) >> 6);
		refColors[0] = 0xFF000000 | (b0 << 16) | (g0 << 8) | r0;

		uint r1 = (uint)(((((c1 >> 11) & 0x1F) * 527) + 23) >> 6);
		uint g1 = (uint)(((((c1 >> 5) & 0x3F) * 259) + 33) >> 6);
		uint b1 = (uint)((((c1 & 0x1F) * 527) + 23) >> 6);
		refColors[1] = 0xFF000000 | (b1 << 16) | (g1 << 8) | r1;

		uint r;
		uint g;
		uint b;

		if (c0 > c1 || onlyOpaqueMode != 0)
		{
			// Standard BC1 mode (also BC3 color block uses ONLY this mode)
			// color_2 = 2/3*color_0 + 1/3*color_1
			// color_3 = 1/3*color_0 + 2/3*color_1
			r = (uint)(((2 * r0) + r1 + 1) / 3);
			g = (uint)(((2 * g0) + g1 + 1) / 3);
			b = (uint)(((2 * b0) + b1 + 1) / 3);
			refColors[2] = 0xFF000000 | (b << 16) | (g << 8) | r;

			r = (uint)((r0 + (2 * r1) + 1) / 3);
			g = (uint)((g0 + (2 * g1) + 1) / 3);
			b = (uint)((b0 + (2 * b1) + 1) / 3);
			refColors[3] = 0xFF000000 | (b << 16) | (g << 8) | r;
		}
		else
		{
			// Quite rare BC1A mode
			// color_2 = 1/2*color_0 + 1/2*color_1;
			// color_3 = 0;
			r = (uint)((r0 + r1 + 1) >> 1);
			g = (uint)((g0 + g1 + 1) >> 1);
			b = (uint)((b0 + b1 + 1) >> 1);
			refColors[2] = 0xFF000000 | (b << 16) | (g << 8) | r;

			refColors[3] = 0x00000000;
		}

		uint colorIndices = BinaryPrimitives.ReadUInt32LittleEndian(compressedBlock.Slice(sizeof(ushort) + sizeof(ushort)));

		// Fill out the decompressed color block 
		for (int i = 0; i < 4; ++i)
		{
			for (int j = 0; j < 4; ++j)
			{
				int idx = (int)(colorIndices & 0x03);
				int decompressedBlockOffset = i * destinationPitch + j * sizeof(uint);
				if (decompressedBlockOffset + sizeof(uint) > decompressedBlock.Length)
				{
					throw new Exception($"Not enough space in decompressed block.\nLength: {decompressedBlock.Length}\nOffset: {decompressedBlockOffset}\nPitch: {destinationPitch}\ni: {i}\nj: {j}");
				}
				BinaryPrimitives.WriteUInt32LittleEndian(decompressedBlock.Slice(decompressedBlockOffset), refColors[idx]);
				colorIndices >>= 2;
			}
		}
	}

	public static void SharpAlphaBlock(ReadOnlySpan<byte> compressedBlock, Span<byte> decompressedBlock, int destinationPitch)
	{
		for (int i = 0; i < 4; ++i)
		{
			ushort alpha = BinaryPrimitives.ReadUInt16LittleEndian(compressedBlock.Slice(i * sizeof(ushort)));
			for (int j = 0; j < 4; ++j)
			{
				decompressedBlock[j * 4 + i * destinationPitch] = (byte)((((uint)alpha >> (4 * j)) & 0x0F) * 17);
			}
		}
	}

	public static void SmoothAlphaBlock(ReadOnlySpan<byte> compressedBlock, Span<byte> decompressedBlock, int destinationPitch, int pixelSize)
	{
		Span<byte> alpha = stackalloc byte[8];

		ulong block = BinaryPrimitives.ReadUInt64LittleEndian(compressedBlock);

		alpha[0] = (byte)(block & 0xFF);
		alpha[1] = (byte)((block >> 8) & 0xFF);

		if (alpha[0] > alpha[1])
		{
			// 6 interpolated alpha values. 
			alpha[2] = (byte)(((6 * alpha[0]) + alpha[1] + 1) / 7); // 6/7*alpha_0 + 1/7*alpha_1
			alpha[3] = (byte)(((5 * alpha[0]) + (2 * alpha[1]) + 1) / 7); // 5/7*alpha_0 + 2/7*alpha_1
			alpha[4] = (byte)(((4 * alpha[0]) + (3 * alpha[1]) + 1) / 7); // 4/7*alpha_0 + 3/7*alpha_1
			alpha[5] = (byte)(((3 * alpha[0]) + (4 * alpha[1]) + 1) / 7); // 3/7*alpha_0 + 4/7*alpha_1
			alpha[6] = (byte)(((2 * alpha[0]) + (5 * alpha[1]) + 1) / 7); // 2/7*alpha_0 + 5/7*alpha_1
			alpha[7] = (byte)((alpha[0] + (6 * alpha[1]) + 1) / 7); // 1/7*alpha_0 + 6/7*alpha_1
		}
		else
		{
			// 4 interpolated alpha values. 
			alpha[2] = (byte)(((4 * alpha[0]) + alpha[1] + 1) / 5); // 4/5*alpha_0 + 1/5*alpha_1
			alpha[3] = (byte)(((3 * alpha[0]) + (2 * alpha[1]) + 1) / 5); // 3/5*alpha_0 + 2/5*alpha_1
			alpha[4] = (byte)(((2 * alpha[0]) + (3 * alpha[1]) + 1) / 5); // 2/5*alpha_0 + 3/5*alpha_1
			alpha[5] = (byte)((alpha[0] + (4 * alpha[1]) + 1) / 5); // 1/5*alpha_0 + 4/5*alpha_1
			alpha[6] = 0x00;
			alpha[7] = 0xFF;
		}

		ulong indices = (ulong)(block >> 16);
		for (int i = 0; i < 4; ++i)
		{
			for (int j = 0; j < 4; ++j)
			{
				decompressedBlock[j * pixelSize + i * destinationPitch] = alpha[(int)(indices & 0x07)];
				indices >>= 3;
			}
		}
	}

	// http://graphics.stanford.edu/~seander/bithacks.html#VariableSignExtend 
	public static int ExtendSign(int val, int bits)
	{
		return (val << (32 - bits)) >> (32 - bits);
	}

	public static int TransformInverse(int val, int a0, int bits, bool isSigned)
	{
		// If the precision of A0 is "p" bits, then the transform algorithm is:
		// B0 = (B0 + A0) & ((1 << p) - 1)
		val = (val + a0) & ((1 << bits) - 1);
		if (isSigned)
		{
			val = ExtendSign(val, bits);
		}
		return val;
	}

	/// <summary>
	/// Essentially copy-paste from documentation
	/// </summary>
	/// <param name="val"></param>
	/// <param name="bits"></param>
	/// <param name="isSigned"></param>
	/// <returns></returns>
	public static int Unquantize(int val, int bits, bool isSigned)
	{
		int unq;
		int s = 0;

		if (!isSigned)
		{
			if (bits >= 15)
			{
				unq = val;
			}
			else if (val == 0)
			{
				unq = 0;
			}
			else if (val == ((1 << bits) - 1))
			{
				unq = 0xFFFF;
			}
			else
			{
				unq = ((val << 16) + 0x8000) >> bits;
			}
		}
		else
		{
			if (bits >= 16)
			{
				unq = val;
			}
			else
			{
				if (val < 0)
				{
					s = 1;
					val = -val;
				}

				if (val == 0)
				{
					unq = 0;
				}
				else if (val >= ((1 << (bits - 1)) - 1))
				{
					unq = 0x7FFF;
				}
				else
				{
					unq = ((val << 15) + 0x4000) >> (bits - 1);
				}

				if (s != 0)
				{
					unq = -unq;
				}
			}
		}
		return unq;
	}

	public static int Interpolate(int a, int b, ReadOnlySpan<int> weights, int index)
	{
		return ((a * (64 - weights[index])) + (b * weights[index]) + 32) >> 6;
	}

	public static ushort FinishUnquantize(int val, bool isSigned)
	{
		if (!isSigned)
		{
			return (ushort)((val * 31) >> 6); // scale the magnitude by 31 / 64
		}
		else
		{
			val = (val < 0) ? -(((-val) * 31) >> 5) : (val * 31) >> 5; // scale the magnitude by 31 / 32
			int s = 0;
			if (val < 0)
			{
				s = 0x8000;
				val = -val;
			}
			return (ushort)(s | val);
		}
	}

	public static void SwapValues(ref int a, ref int b)
	{
		(a, b) = (b, a);
	}

	public static T[][] CreateRectangularArray<T>(int size1, int size2)
	{
		T[][] newArray = new T[size1][];
		for (int array1 = 0; array1 < size1; array1++)
		{
			newArray[array1] = new T[size2];
		}

		return newArray;
	}
}
