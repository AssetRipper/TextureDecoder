//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.Tests.Formats;

public partial class ColorRG32HalfTests
{
	[Test]
	public void CorrectSizeTest()
	{
		Assert.That(Unsafe.SizeOf<ColorRG32Half>(), Is.EqualTo(4));
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
		color.R = (Half)0.333f;
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
		color.G = (Half)0.333f;
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
		color.B = (Half)0.333f;
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
		color.A = (Half)0.333f;
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
	
	public static ColorRG32Half MakeRandomColor()
	{
		return new()
		{
			R = (Half)0.447f,
			G = (Half)0.224f,
			B = (Half)0.95f,
			A = (Half)0.897f,
		};
	}
	
	[Test]
	public void ConversionToColorRG32HalfIsLossless()
	{
		ColorRG32Half original = MakeRandomColor();
		ColorRG32Half converted = original.Convert<ColorRG32Half, Half, ColorRG32Half, Half>();
		ColorRG32Half convertedBack = converted.Convert<ColorRG32Half, Half, ColorRG32Half, Half>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRG64SingleIsLossless()
	{
		ColorRG32Half original = MakeRandomColor();
		ColorRG64Single converted = original.Convert<ColorRG32Half, Half, ColorRG64Single, float>();
		ColorRG32Half convertedBack = converted.Convert<ColorRG64Single, float, ColorRG32Half, Half>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRG128DoubleIsLossless()
	{
		ColorRG32Half original = MakeRandomColor();
		ColorRG128Double converted = original.Convert<ColorRG32Half, Half, ColorRG128Double, double>();
		ColorRG32Half convertedBack = converted.Convert<ColorRG128Double, double, ColorRG32Half, Half>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGB48HalfIsLossless()
	{
		ColorRG32Half original = MakeRandomColor();
		ColorRGB48Half converted = original.Convert<ColorRG32Half, Half, ColorRGB48Half, Half>();
		ColorRG32Half convertedBack = converted.Convert<ColorRGB48Half, Half, ColorRG32Half, Half>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGB96SingleIsLossless()
	{
		ColorRG32Half original = MakeRandomColor();
		ColorRGB96Single converted = original.Convert<ColorRG32Half, Half, ColorRGB96Single, float>();
		ColorRG32Half convertedBack = converted.Convert<ColorRGB96Single, float, ColorRG32Half, Half>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGB192DoubleIsLossless()
	{
		ColorRG32Half original = MakeRandomColor();
		ColorRGB192Double converted = original.Convert<ColorRG32Half, Half, ColorRGB192Double, double>();
		ColorRG32Half convertedBack = converted.Convert<ColorRGB192Double, double, ColorRG32Half, Half>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBA128SingleIsLossless()
	{
		ColorRG32Half original = MakeRandomColor();
		ColorRGBA128Single converted = original.Convert<ColorRG32Half, Half, ColorRGBA128Single, float>();
		ColorRG32Half convertedBack = converted.Convert<ColorRGBA128Single, float, ColorRG32Half, Half>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBA256DoubleIsLossless()
	{
		ColorRG32Half original = MakeRandomColor();
		ColorRGBA256Double converted = original.Convert<ColorRG32Half, Half, ColorRGBA256Double, double>();
		ColorRG32Half convertedBack = converted.Convert<ColorRGBA256Double, double, ColorRG32Half, Half>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBA64HalfIsLossless()
	{
		ColorRG32Half original = MakeRandomColor();
		ColorRGBA64Half converted = original.Convert<ColorRG32Half, Half, ColorRGBA64Half, Half>();
		ColorRG32Half convertedBack = converted.Convert<ColorRGBA64Half, Half, ColorRG32Half, Half>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
}
