using AssetRipper.TextureDecoder.Rgb;

namespace AssetRipper.TextureDecoder.Tests.Formats;
internal static class LosslessConversion
{
	internal static void Assert<TColor1, TChannelValue1, TColor2, TChannelValue2>()
		where TChannelValue1 : unmanaged
		where TChannelValue2 : unmanaged
		where TColor1 : unmanaged, IColor<TChannelValue1>
		where TColor2 : unmanaged, IColor<TChannelValue2>
	{
		TColor1 original = ColorRandom<TColor1, TChannelValue1>.MakeRandomColor();
		TColor2 converted = original.Convert<TColor1, TChannelValue1, TColor2, TChannelValue2>();
		TColor1 convertedBack = converted.Convert<TColor2, TChannelValue2, TColor1, TChannelValue1>();
		NUnit.Framework.Assert.That(convertedBack, Is.EqualTo(original));
	}
}
