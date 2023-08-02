using AssetRipper.TextureDecoder.Rgb;

namespace AssetRipper.TextureDecoder.Tests.Formats;
internal static class LosslessConversion
{
	internal static void Assert<TColor1, TChannel1, TColor2, TChannel2>()
		where TChannel1 : unmanaged
		where TChannel2 : unmanaged
		where TColor1 : unmanaged, IColor<TChannel1>
		where TColor2 : unmanaged, IColor<TChannel2>
	{
		TColor1 original = ColorRandom<TColor1, TChannel1>.MakeRandomColor();
		TColor2 converted = original.Convert<TColor1, TChannel1, TColor2, TChannel2>();
		TColor1 convertedBack = converted.Convert<TColor2, TChannel2, TColor1, TChannel1>();
		NUnit.Framework.Assert.That(convertedBack, Is.EqualTo(original));
	}
}