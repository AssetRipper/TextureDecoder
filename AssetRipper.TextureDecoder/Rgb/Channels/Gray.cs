namespace AssetRipper.TextureDecoder.Rgb.Channels;

public readonly struct Gray : IChannel
{
	static bool IChannel.IsRed => true;
	static bool IChannel.IsGreen => true;
	static bool IChannel.IsBlue => true;
	static bool IChannel.FullyUtilized => false;
}
