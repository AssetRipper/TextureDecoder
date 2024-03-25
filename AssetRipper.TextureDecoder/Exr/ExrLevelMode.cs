namespace AssetRipper.TextureDecoder.Exr;

[Flags]
public enum ExrLevelMode : byte
{
	OneLevel = 0,
	MipMapLevels = 1,
	RipMapLevels = 2,
	RoundDown = 0,
	RoundUp = 16,
}
