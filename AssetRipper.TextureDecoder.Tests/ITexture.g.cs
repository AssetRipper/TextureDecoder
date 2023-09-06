// Auto-generated code. Do not modify manually.

namespace AssetRipper.TextureDecoder.Tests;

public partial interface ITexture
{
	static abstract TextureType Type { get; }
	static abstract TextureFormat Format { get; }
	static abstract int FileSize { get; }
	static abstract int ImageSize { get; }
	static abstract int ImageCount { get; }
	static abstract bool Mips { get; }
	static abstract int Width { get; }
	static abstract int Height { get; }
	static abstract string Path { get; }
	static abstract byte[] Data { get; }
}
