//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.Tests.Formats;

public partial class ColorRG64SingleTests
{
	[Test]
	public void CorrectSizeTest()
	{
		Assert.That(Unsafe.SizeOf<ColorRG64Single>(), Is.EqualTo(8));
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
		color.R = 0.333f;
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
		color.G = 0.333f;
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
		color.B = 0.333f;
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
		color.A = 0.333f;
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
	
	public static ColorRG64Single MakeRandomColor()
	{
		return new()
		{
			R = 0.447f,
			G = 0.224f,
			B = 0.95f,
			A = 0.897f,
		};
	}
	
	[Test]
	public void ConversionToColorRG64SingleIsLossless()
	{
		ColorRG64Single original = MakeRandomColor();
		ColorRG64Single converted = original.Convert<ColorRG64Single, float, ColorRG64Single, float>();
		ColorRG64Single convertedBack = converted.Convert<ColorRG64Single, float, ColorRG64Single, float>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRG128DoubleIsLossless()
	{
		ColorRG64Single original = MakeRandomColor();
		ColorRG128Double converted = original.Convert<ColorRG64Single, float, ColorRG128Double, double>();
		ColorRG64Single convertedBack = converted.Convert<ColorRG128Double, double, ColorRG64Single, float>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGB96SingleIsLossless()
	{
		ColorRG64Single original = MakeRandomColor();
		ColorRGB96Single converted = original.Convert<ColorRG64Single, float, ColorRGB96Single, float>();
		ColorRG64Single convertedBack = converted.Convert<ColorRGB96Single, float, ColorRG64Single, float>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGB192DoubleIsLossless()
	{
		ColorRG64Single original = MakeRandomColor();
		ColorRGB192Double converted = original.Convert<ColorRG64Single, float, ColorRGB192Double, double>();
		ColorRG64Single convertedBack = converted.Convert<ColorRGB192Double, double, ColorRG64Single, float>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBA128SingleIsLossless()
	{
		ColorRG64Single original = MakeRandomColor();
		ColorRGBA128Single converted = original.Convert<ColorRG64Single, float, ColorRGBA128Single, float>();
		ColorRG64Single convertedBack = converted.Convert<ColorRGBA128Single, float, ColorRG64Single, float>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBA256DoubleIsLossless()
	{
		ColorRG64Single original = MakeRandomColor();
		ColorRGBA256Double converted = original.Convert<ColorRG64Single, float, ColorRGBA256Double, double>();
		ColorRG64Single convertedBack = converted.Convert<ColorRGBA256Double, double, ColorRG64Single, float>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
}
