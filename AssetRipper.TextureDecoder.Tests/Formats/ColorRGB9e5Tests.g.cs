// Auto-generated code. Do not modify manually.

using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.Tests.Formats;

public partial class ColorRGB9e5Tests
{
	[Test]
	public void CorrectSizeTest()
	{
		Assert.That(Unsafe.SizeOf<ColorRGB9e5>(), Is.EqualTo(4));
	}

	[Test]
	public void GetMethodMatchesProperties()
	{
		var color = MakeRandomColor();
		color.GetChannels(out var r, out var g, out var b, out var a);
		Assert.Multiple(() =>
		{
			Assert.That(color.R, Is.EqualTo(r));
			Assert.That(color.G, Is.EqualTo(g));
			Assert.That(color.B, Is.EqualTo(b));
			Assert.That(color.A, Is.EqualTo(a));
		});
	}

	[Test]
	public void MethodsAreSymmetric()
	{
		var color = MakeRandomColor();
		color.GetChannels(out var r, out var g, out var b, out var a);
		color.SetChannels(r, g, b, a);
		Assert.Multiple(() =>
		{
			Assert.That(color.R, Is.EqualTo(r));
			Assert.That(color.G, Is.EqualTo(g));
			Assert.That(color.B, Is.EqualTo(b));
			Assert.That(color.A, Is.EqualTo(a));
		});
	}

	public static ColorRGB9e5 MakeRandomColor() => ColorRandom<ColorRGB9e5, double>.MakeRandomColor();

	public static double MakeRandomValue() => ColorRandom<ColorRGB9e5, double>.MakeRandomValue();

	[Test]
	public void ConversionIsLosslessToColorRGBA_double()
	{
		LosslessConversion.Assert<ColorRGB9e5, double, ColorRGBA<double>, double>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGB_double()
	{
		LosslessConversion.Assert<ColorRGB9e5, double, ColorRGB<double>, double>();
	}
}
