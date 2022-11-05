//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.Tests.Formats;

public partial class ColorRSingleTests
{
	[Test]
	public void CorrectSizeTest()
	{
		Assert.That(Unsafe.SizeOf<ColorRSingle>(), Is.EqualTo(4));
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
	
	public static ColorRSingle MakeRandomColor()
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
	public void ConversionToColorRGBASingleIsLossless()
	{
		ColorRSingle original = MakeRandomColor();
		ColorRGBASingle converted = original.Convert<ColorRSingle, float, ColorRGBASingle, float>();
		ColorRSingle convertedBack = converted.Convert<ColorRGBASingle, float, ColorRSingle, float>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBSingleIsLossless()
	{
		ColorRSingle original = MakeRandomColor();
		ColorRGBSingle converted = original.Convert<ColorRSingle, float, ColorRGBSingle, float>();
		ColorRSingle convertedBack = converted.Convert<ColorRGBSingle, float, ColorRSingle, float>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGSingleIsLossless()
	{
		ColorRSingle original = MakeRandomColor();
		ColorRGSingle converted = original.Convert<ColorRSingle, float, ColorRGSingle, float>();
		ColorRSingle convertedBack = converted.Convert<ColorRGSingle, float, ColorRSingle, float>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRSingleIsLossless()
	{
		ColorRSingle original = MakeRandomColor();
		ColorRSingle converted = original.Convert<ColorRSingle, float, ColorRSingle, float>();
		ColorRSingle convertedBack = converted.Convert<ColorRSingle, float, ColorRSingle, float>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
}
