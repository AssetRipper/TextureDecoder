using AssetRipper.TextureDecoder.Rgb;

namespace AssetRipper.TextureDecoder.Tests.Formats;
internal static class ColorRandom<TColor, TChannelValue> where TColor : unmanaged, IColor<TChannelValue> where TChannelValue : unmanaged
{
	public static TChannelValue MakeRandomValue()
	{
		ulong value = TestContext.CurrentContext.Random.NextULong();
		return NumericConversion.Convert<ulong, TChannelValue>(value);
	}

	public static TColor MakeRandomColor()
	{
		TColor color = default;
		color.SetChannels(MakeRandomValue(), MakeRandomValue(), MakeRandomValue(), MakeRandomValue());
		return color;
	}
}
