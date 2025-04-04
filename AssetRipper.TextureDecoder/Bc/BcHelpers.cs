﻿using System.Buffers.Binary;
using System.Diagnostics;

namespace AssetRipper.TextureDecoder.Bc;

internal static class BcHelpers
{
	public static void DecompressBc1(ReadOnlySpan<byte> compressedBlock, Span<byte> decompressedBlock)
	{
		Debug.Assert(decompressedBlock.Length == Bc1.DecodedBlockSize);
		int destinationPitch = 4 * 4;
		ColorBlock(compressedBlock, decompressedBlock, destinationPitch, 0);
	}

	public static void DecompressBc2(ReadOnlySpan<byte> compressedBlock, Span<byte> decompressedBlock)
	{
		Debug.Assert(decompressedBlock.Length == Bc2.DecodedBlockSize);
		int destinationPitch = 4 * 4;
		ColorBlock(compressedBlock.Slice(8), decompressedBlock, destinationPitch, 1);
		SharpAlphaBlock(compressedBlock, decompressedBlock.Slice(3), destinationPitch);
	}

	public static void DecompressBc3(ReadOnlySpan<byte> compressedBlock, Span<byte> decompressedBlock)
	{
		Debug.Assert(decompressedBlock.Length == Bc3.DecodedBlockSize);
		int destinationPitch = 4 * 4;
		ColorBlock(compressedBlock.Slice(8), decompressedBlock, destinationPitch, 1);
		SmoothAlphaBlock(compressedBlock, decompressedBlock.Slice(3), destinationPitch, 4);
	}

	public static void DecompressBc4(ReadOnlySpan<byte> compressedBlock, Span<byte> decompressedBlock)
	{
		Debug.Assert(decompressedBlock.Length == Bc4.DecodedBlockSize);
		int destinationPitch = 4 * 1;
		SmoothAlphaBlock(compressedBlock, decompressedBlock, destinationPitch, 1);
	}

	public static void DecompressBc5(ReadOnlySpan<byte> compressedBlock, Span<byte> decompressedBlock)
	{
		Debug.Assert(decompressedBlock.Length == Bc5.DecodedBlockSize);
		int destinationPitch = 4 * 2;
		SmoothAlphaBlock(compressedBlock, decompressedBlock, destinationPitch, 2);
		SmoothAlphaBlock(compressedBlock.Slice(8), decompressedBlock.Slice(1), destinationPitch, 2);
	}

	public static void DecompressBc6h(ReadOnlySpan<byte> compressedBlock, Span<byte> decompressedBlock, bool isSigned)
	{
		Debug.Assert(decompressedBlock.Length == Bc6h.DecodedBlockSize);

		BitStream bstream = new(compressedBlock);
		Span<uint> r = stackalloc uint[4]; // wxyz
		Span<uint> g = stackalloc uint[4];
		Span<uint> b = stackalloc uint[4];

		int decompressedOffset = 0;

		r.Clear();
		g.Clear();
		b.Clear();

		int mode;

		//modes >= 11 (10 in my code) are using 0 one, others will read it from the bitstream
		uint partition;

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
					decompressedBlock.Clear();

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
		if ((mode is not 9 and not 10) || isSigned)
		{
			for (int i = 1; i < (numPartitions + 1) * 2; ++i)
			{
				r[i] = ExtendSign(r[i], Bc6hTables.ActualBitsCount[1][mode]);
				g[i] = ExtendSign(g[i], Bc6hTables.ActualBitsCount[2][mode]);
				b[i] = ExtendSign(b[i], Bc6hTables.ActualBitsCount[3][mode]);
			}
		}

		if (mode is not 9 and not 10)
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

				int index = bstream.ReadBits(indexBits).ToSigned();

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

			decompressedOffset += 4 * 3 * sizeof(ushort);
		}

		static uint ReadBc6hModeBits(ref BitStream bstream)
		{
			uint twoBits = bstream.ReadBits(2);
			if (twoBits > 1)
			{
				uint threeBits = bstream.ReadBits(3);
				return (threeBits << 2) | twoBits;
			}
			else
			{
				return twoBits;
			}
		}
	}

	public static void DecompressBc7(ReadOnlySpan<byte> compressedBlock, Span<byte> decompressedBlock)
	{
		Debug.Assert(decompressedBlock.Length == Bc7.DecodedBlockSize);

		BitStream bstream = new(compressedBlock);
		Span2D<uint> endpoints = new(stackalloc uint[6 * 4], 6, 4);
		Span2D<int> indices = new(stackalloc int[4 * 4], 4, 4);

		int mode = ReadBc7Mode(ref bstream);

		// unexpected mode, clear the block (transparent black)
		if (mode >= 8)
		{
			decompressedBlock.Clear();

			return;
		}

		uint partition = 0;
		int numPartitions = 1;
		uint rotation = 0;
		uint indexSelectionBit = 0;

		switch (mode)
		{
			case 0:
				numPartitions = 3;
				partition = bstream.ReadBits(4);
				break;
			case 1 or 3 or 7:
				numPartitions = 2;
				partition = bstream.ReadBits(6);
				break;
			case 2:
				numPartitions = 3;
				partition = bstream.ReadBits(6);
				break;
			case 4:
				rotation = bstream.ReadBits(2);
				indexSelectionBit = bstream.ReadBit();
				break;
			case 5:
				rotation = bstream.ReadBits(2);
				break;
		}

		int numEndpoints = numPartitions * 2;

		// Extract endpoints 
		// RGB 
		for (int i = 0; i < 3; ++i)
		{
			for (int j = 0; j < numEndpoints; ++j)
			{
				endpoints[j, i] = bstream.ReadBits(Bc7Tables.ActualBitsCount[0][mode]);
			}
		}
		// Alpha (if any) 
		if (Bc7Tables.ActualBitsCount[1][mode] > 0)
		{
			for (int j = 0; j < numEndpoints; ++j)
			{
				endpoints[j, 3] = bstream.ReadBits(Bc7Tables.ActualBitsCount[1][mode]);
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
					endpoints[i, j] <<= 1;
				}
			}

			// if P-bit is shared 
			if (mode == 1)
			{
				uint i = bstream.ReadBit();
				uint j = bstream.ReadBit();

				// rgb component-wise insert pbits 
				for (int k = 0; k < 3; ++k)
				{
					endpoints[0, k] |= i;
					endpoints[1, k] |= i;
					endpoints[2, k] |= j;
					endpoints[3, k] |= j;
				}
			}
			else if ((Bc7Tables.bcdec_bc7_sModeHasPBits & (1 << mode)) != 0)
			{
				// unique P-bit per endpoint 
				for (int i = 0; i < numEndpoints; ++i)
				{
					uint j = bstream.ReadBit();
					for (int k = 0; k < 4; ++k)
					{
						endpoints[i, k] |= j;
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
				endpoints[i, k] = endpoints[i, k] << (8 - j);
				// Replicate each component's MSB into the LSBs revealed by the left-shift operation above 
				endpoints[i, k] = endpoints[i, k] | (endpoints[i, k] >> j);
			}

			// get alpha component precision including pbit 
			j = Bc7Tables.ActualBitsCount[1][mode] + ((Bc7Tables.bcdec_bc7_sModeHasPBits >> mode) & 1);

			// left shift endpoint components so that their MSB lies in bit 7 
			endpoints[i, 3] = endpoints[i, 3] << (8 - j);
			// Replicate each component's MSB into the LSBs revealed by the left-shift operation above 
			endpoints[i, 3] = endpoints[i, 3] | (endpoints[i, 3] >> j);
		}

		// If this mode does not explicitly define the alpha component 
		// set alpha equal to 1.0 
		if (Bc7Tables.ActualBitsCount[1][mode] == 0)
		{
			for (int j = 0; j < numEndpoints; ++j)
			{
				endpoints[j, 3] = 0xFF;
			}
		}

		// Determine weights tables 
		int indexBits = mode switch
		{
			0 or 1 => 3,
			6 => 4,
			_ => 2
		};
		int indexBits2 = mode switch
		{
			4 => 3,
			5 => 2,
			_ => 0
		};
		ReadOnlySpan<int> weights = indexBits switch
		{
			2 => Bc7Tables.AWeight2,
			3 => Bc7Tables.AWeight3,
			_ => Bc7Tables.AWeight4
		};
		ReadOnlySpan<int> weights2 = (indexBits2 == 2) ? Bc7Tables.AWeight2 : Bc7Tables.AWeight3;

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

				indices[i, j] = bstream.ReadBits(indexBits).ToSigned();
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

				int index = indices[i, j];

				uint r;
				uint g;
				uint b;
				uint a;

				if (indexBits2 == 0)
				{
					r = Interpolate(endpoints[partitionSet * 2, 0], endpoints[(partitionSet * 2) + 1, 0], weights, index);
					g = Interpolate(endpoints[partitionSet * 2, 1], endpoints[(partitionSet * 2) + 1, 1], weights, index);
					b = Interpolate(endpoints[partitionSet * 2, 2], endpoints[(partitionSet * 2) + 1, 2], weights, index);
					a = Interpolate(endpoints[partitionSet * 2, 3], endpoints[(partitionSet * 2) + 1, 3], weights, index);
				}
				else
				{
					int index2 = bstream.ReadBits((i | j) != 0 ? indexBits2 : (indexBits2 - 1)).ToSigned();
					// The index value for interpolating color comes from the secondary index bits for the texel
					// if the mode has an index selection bit and its value is one, and from the primary index bits otherwise.
					// The alpha index comes from the secondary index bits if the block has a secondary index
					// and the block either doesn't have an index selection bit or that bit is zero,
					// and from the primary index bits otherwise.
					if (indexSelectionBit == 0)
					{
						r = Interpolate(endpoints[partitionSet * 2, 0], endpoints[(partitionSet * 2) + 1, 0], weights, index);
						g = Interpolate(endpoints[partitionSet * 2, 1], endpoints[(partitionSet * 2) + 1, 1], weights, index);
						b = Interpolate(endpoints[partitionSet * 2, 2], endpoints[(partitionSet * 2) + 1, 2], weights, index);
						a = Interpolate(endpoints[partitionSet * 2, 3], endpoints[(partitionSet * 2) + 1, 3], weights2, index2);
					}
					else
					{
						r = Interpolate(endpoints[partitionSet * 2, 0], endpoints[(partitionSet * 2) + 1, 0], weights2, index2);
						g = Interpolate(endpoints[partitionSet * 2, 1], endpoints[(partitionSet * 2) + 1, 1], weights2, index2);
						b = Interpolate(endpoints[partitionSet * 2, 2], endpoints[(partitionSet * 2) + 1, 2], weights2, index2);
						a = Interpolate(endpoints[partitionSet * 2, 3], endpoints[(partitionSet * 2) + 1, 3], weights, index);
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

				int offset = (i * 16) + (j * 4);
				decompressedBlock[offset + 0] = (byte)r;
				decompressedBlock[offset + 1] = (byte)g;
				decompressedBlock[offset + 2] = (byte)b;
				decompressedBlock[offset + 3] = (byte)a;
			}
		}

		static int ReadBc7Mode(ref BitStream bstream)
		{
			// Read bits until we find a non-zero bit, up to 8 bits.
			// The mode is the number of bits read.
			uint eightBits = bstream.PeakBits(8);
			int zeros = BitOperations.TrailingZeroCount(eightBits);
			int bitCount = zeros >= 8 ? 8 : zeros + 1;
			bstream.Advance(bitCount);
			return int.Min(zeros, 8);
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

	private static uint ExtendSign(uint val, int bits)
	{
		return ExtendSign(val.ToSigned(), bits).ToUnsigned();
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

	private static uint TransformInverse(uint val, uint a0, int bits, bool isSigned)
	{
		return TransformInverse(val.ToSigned(), a0.ToSigned(), bits, isSigned).ToUnsigned();
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

	private static uint Unquantize(uint val, int bits, bool isSigned)
	{
		return Unquantize(val.ToSigned(), bits, isSigned).ToUnsigned();
	}

	public static int Interpolate(int a, int b, ReadOnlySpan<int> weights, int index)
	{
		return ((a * (64 - weights[index])) + (b * weights[index]) + 32) >> 6;
	}

	private static uint Interpolate(uint a, uint b, ReadOnlySpan<int> weights, int index)
	{
		return Interpolate(a.ToSigned(), b.ToSigned(), weights, index).ToUnsigned();
	}

	public static ushort FinishUnquantize(int val, bool isSigned)
	{
		if (!isSigned)
		{
			return (ushort)((val * 31) >> 6); // scale the magnitude by 31 / 64
		}
		else
		{
			val = (val < 0) ? (((-val) * 31) >> 5) : (val * 31) >> 5; // scale the magnitude by 31 / 32
			int s = 0;
			if (val < 0)
			{
				s = 0x8000;
				val = -val;
			}
			return (ushort)(s | val);
		}
	}

	private static ushort FinishUnquantize(uint val, bool isSigned)
	{
		return FinishUnquantize(val.ToSigned(), isSigned);
	}

	public static void SwapValues(ref uint a, ref uint b)
	{
		(a, b) = (b, a);
	}

	/// <summary>
	/// This is for copying a decoded block to the output buffer. It handles the pitch and non-4x4 blocks (if necessary).
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static void CopyBufferToOutput(Span<byte> buffer, Span<byte> output, int width, int height, int w, int h, int blockWidth, int blockHeight, int pixelSize)
	{
		int hEnd = Math.Min(h + blockHeight, height);
		int wEnd = Math.Min(w + blockWidth, width);
		for (int i = h; i < hEnd; i++)
		{
			int outputOffset = ((i * width) + w) * pixelSize;
			int outputBufferOffset = ((i - h) * blockWidth) * pixelSize;
			int copyLength = (wEnd - w) * pixelSize;
			buffer.Slice(outputBufferOffset, copyLength).CopyTo(output.Slice(outputOffset, copyLength));
		}
	}
}
