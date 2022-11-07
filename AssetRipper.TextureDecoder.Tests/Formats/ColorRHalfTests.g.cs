//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.Tests.Formats;

public partial class ColorRHalfTests
{
	[Test]
	public void CorrectSizeTest()
	{
		Assert.That(Unsafe.SizeOf<ColorRHalf>(), Is.EqualTo(2));
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
	
	public static ColorRHalf MakeRandomColor()
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
	public void ConversionToColorRGBAHalfIsLossless()
	{
		ColorRHalf original = MakeRandomColor();
		ColorRGBAHalf converted = original.Convert<ColorRHalf, Half, ColorRGBAHalf, Half>();
		ColorRHalf convertedBack = converted.Convert<ColorRGBAHalf, Half, ColorRHalf, Half>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBASingleIsLossless()
	{
		ColorRHalf original = MakeRandomColor();
		ColorRGBASingle converted = original.Convert<ColorRHalf, Half, ColorRGBASingle, float>();
		ColorRHalf convertedBack = converted.Convert<ColorRGBASingle, float, ColorRHalf, Half>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBHalfIsLossless()
	{
		ColorRHalf original = MakeRandomColor();
		ColorRGBHalf converted = original.Convert<ColorRHalf, Half, ColorRGBHalf, Half>();
		ColorRHalf convertedBack = converted.Convert<ColorRGBHalf, Half, ColorRHalf, Half>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBSingleIsLossless()
	{
		ColorRHalf original = MakeRandomColor();
		ColorRGBSingle converted = original.Convert<ColorRHalf, Half, ColorRGBSingle, float>();
		ColorRHalf convertedBack = converted.Convert<ColorRGBSingle, float, ColorRHalf, Half>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGHalfIsLossless()
	{
		ColorRHalf original = MakeRandomColor();
		ColorRGHalf converted = original.Convert<ColorRHalf, Half, ColorRGHalf, Half>();
		ColorRHalf convertedBack = converted.Convert<ColorRGHalf, Half, ColorRHalf, Half>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGSingleIsLossless()
	{
		ColorRHalf original = MakeRandomColor();
		ColorRGSingle converted = original.Convert<ColorRHalf, Half, ColorRGSingle, float>();
		ColorRHalf convertedBack = converted.Convert<ColorRGSingle, float, ColorRHalf, Half>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRHalfIsLossless()
	{
		ColorRHalf original = MakeRandomColor();
		ColorRHalf converted = original.Convert<ColorRHalf, Half, ColorRHalf, Half>();
		ColorRHalf convertedBack = converted.Convert<ColorRHalf, Half, ColorRHalf, Half>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRSingleIsLossless()
	{
		ColorRHalf original = MakeRandomColor();
		ColorRSingle converted = original.Convert<ColorRHalf, Half, ColorRSingle, float>();
		ColorRHalf convertedBack = converted.Convert<ColorRSingle, float, ColorRHalf, Half>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
}
