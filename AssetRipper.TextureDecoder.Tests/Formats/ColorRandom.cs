using AssetRipper.TextureDecoder.Rgb;

namespace AssetRipper.TextureDecoder.Tests.Formats;
internal static class ColorRandom<TColor, TChannel> where TColor : unmanaged, IColor<TChannel> where TChannel : unmanaged
{
	public static TChannel MakeRandomValue()
	{
		ulong value = TestContext.CurrentContext.Random.NextULong();
		return NumericConversion.Convert<ulong, TChannel>(value);
	}

	public static TColor MakeRandomColor()
	{
		TColor color = default;
		color.SetChannels(MakeRandomValue(), MakeRandomValue(), MakeRandomValue(), MakeRandomValue());
		return color;
	}
}
