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
	public void PropertyIsSymmetric_R()
	{
		var color = MakeRandomColor();
		var r = color.R;
		color.R = r;
		Assert.That(color.R, Is.EqualTo(r));
	}
	
	[Test]
	public void PropertyIsSymmetric_G()
	{
		var color = MakeRandomColor();
		var g = color.G;
		color.G = g;
		Assert.That(color.G, Is.EqualTo(g));
	}
	
	[Test]
	public void PropertyIsSymmetric_B()
	{
		var color = MakeRandomColor();
		var b = color.B;
		color.B = b;
		Assert.That(color.B, Is.EqualTo(b));
	}
	
	[Test]
	public void PropertyIsSymmetric_A()
	{
		var color = MakeRandomColor();
		var a = color.A;
		color.A = a;
		Assert.That(color.A, Is.EqualTo(a));
	}
	
	[Test]
	public void ChannelsAreIndependent_R()
	{
		var color = MakeRandomColor();
		var g = color.G;
		var b = color.B;
		var a = color.A;
		color.R = MakeRandomValue();
		Assert.Multiple(() =>
		{
			Assert.That(color.G, Is.EqualTo(g));
			Assert.That(color.B, Is.EqualTo(b));
			Assert.That(color.A, Is.EqualTo(a));
		});
	}
	
	[Test]
	public void ChannelsAreIndependent_G()
	{
		var color = MakeRandomColor();
		var r = color.R;
		var b = color.B;
		var a = color.A;
		color.G = MakeRandomValue();
		Assert.Multiple(() =>
		{
			Assert.That(color.R, Is.EqualTo(r));
			Assert.That(color.B, Is.EqualTo(b));
			Assert.That(color.A, Is.EqualTo(a));
		});
	}
	
	[Test]
	public void ChannelsAreIndependent_B()
	{
		var color = MakeRandomColor();
		var r = color.R;
		var g = color.G;
		var a = color.A;
		color.B = MakeRandomValue();
		Assert.Multiple(() =>
		{
			Assert.That(color.R, Is.EqualTo(r));
			Assert.That(color.G, Is.EqualTo(g));
			Assert.That(color.A, Is.EqualTo(a));
		});
	}
	
	[Test]
	public void ChannelsAreIndependent_A()
	{
		var color = MakeRandomColor();
		var r = color.R;
		var g = color.G;
		var b = color.B;
		color.A = MakeRandomValue();
		Assert.Multiple(() =>
		{
			Assert.That(color.R, Is.EqualTo(r));
			Assert.That(color.G, Is.EqualTo(g));
			Assert.That(color.B, Is.EqualTo(b));
		});
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
