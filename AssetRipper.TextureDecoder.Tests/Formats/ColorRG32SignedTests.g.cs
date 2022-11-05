//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.Tests.Formats;

public partial class ColorRG32SignedTests
{
	[Test]
	public void CorrectSizeTest()
	{
		Assert.That(Unsafe.SizeOf<ColorRG32Signed>(), Is.EqualTo(4));
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
		color.R = 31871;
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
		color.G = 31871;
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
		color.B = 31871;
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
		color.A = 31871;
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
	
	public static ColorRG32Signed MakeRandomColor()
	{
		return new()
		{
			R = -24000,
			G = 21354,
			B = -80,
			A = 347,
		};
	}
	
	[Test]
	public void ConversionToColorRG32IsLossless()
	{
		ColorRG32Signed original = MakeRandomColor();
		ColorRG32 converted = original.Convert<ColorRG32Signed, short, ColorRG32, ushort>();
		ColorRG32Signed convertedBack = converted.Convert<ColorRG32, ushort, ColorRG32Signed, short>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRG32SignedIsLossless()
	{
		ColorRG32Signed original = MakeRandomColor();
		ColorRG32Signed converted = original.Convert<ColorRG32Signed, short, ColorRG32Signed, short>();
		ColorRG32Signed convertedBack = converted.Convert<ColorRG32Signed, short, ColorRG32Signed, short>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGB48IsLossless()
	{
		ColorRG32Signed original = MakeRandomColor();
		ColorRGB48 converted = original.Convert<ColorRG32Signed, short, ColorRGB48, ushort>();
		ColorRG32Signed convertedBack = converted.Convert<ColorRGB48, ushort, ColorRG32Signed, short>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGB48SignedIsLossless()
	{
		ColorRG32Signed original = MakeRandomColor();
		ColorRGB48Signed converted = original.Convert<ColorRG32Signed, short, ColorRGB48Signed, short>();
		ColorRG32Signed convertedBack = converted.Convert<ColorRGB48Signed, short, ColorRG32Signed, short>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBA64IsLossless()
	{
		ColorRG32Signed original = MakeRandomColor();
		ColorRGBA64 converted = original.Convert<ColorRG32Signed, short, ColorRGBA64, ushort>();
		ColorRG32Signed convertedBack = converted.Convert<ColorRGBA64, ushort, ColorRG32Signed, short>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBA64SignedIsLossless()
	{
		ColorRG32Signed original = MakeRandomColor();
		ColorRGBA64Signed converted = original.Convert<ColorRG32Signed, short, ColorRGBA64Signed, short>();
		ColorRG32Signed convertedBack = converted.Convert<ColorRGBA64Signed, short, ColorRG32Signed, short>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
}
