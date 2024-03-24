namespace AssetRipper.TextureDecoder.Rgb.Channels;

public readonly struct A : IChannel
{
	static bool IChannel.IsAlpha => true;
	static T IChannel.GetBlack<T>() => NumericConversion.GetMaximumValueSafe<T>();
}
