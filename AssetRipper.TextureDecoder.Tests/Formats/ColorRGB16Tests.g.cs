//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.Tests.Formats;

public partial class ColorRGB16Tests
{
	[Test]
	public void CorrectSizeTest()
	{
		Assert.That(Unsafe.SizeOf<ColorRGB16>(), Is.EqualTo(2));
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
		color.R = 0b01001110;
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
		color.G = 0b01001110;
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
		color.B = 0b01001110;
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
		color.A = 0b01001110;
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
	
	public static ColorRGB16 MakeRandomColor()
	{
		return new()
		{
			R = 0b11010101,
			G = 0b01110010,
			B = 0b10001111,
			A = 0b11000111,
		};
	}
	
	[Test]
	public void ConversionToColorARGB32IsLossless()
	{
		ColorRGB16 original = MakeRandomColor();
		ColorARGB32 converted = original.Convert<ColorRGB16, byte, ColorARGB32, byte>();
		ColorRGB16 convertedBack = converted.Convert<ColorARGB32, byte, ColorRGB16, byte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorBGRA32IsLossless()
	{
		ColorRGB16 original = MakeRandomColor();
		ColorBGRA32 converted = original.Convert<ColorRGB16, byte, ColorBGRA32, byte>();
		ColorRGB16 convertedBack = converted.Convert<ColorBGRA32, byte, ColorRGB16, byte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGB24IsLossless()
	{
		ColorRGB16 original = MakeRandomColor();
		ColorRGB24 converted = original.Convert<ColorRGB16, byte, ColorRGB24, byte>();
		ColorRGB16 convertedBack = converted.Convert<ColorRGB24, byte, ColorRGB16, byte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGB24SignedIsLossless()
	{
		ColorRGB16 original = MakeRandomColor();
		ColorRGB24Signed converted = original.Convert<ColorRGB16, byte, ColorRGB24Signed, sbyte>();
		ColorRGB16 convertedBack = converted.Convert<ColorRGB24Signed, sbyte, ColorRGB16, byte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGB48IsLossless()
	{
		ColorRGB16 original = MakeRandomColor();
		ColorRGB48 converted = original.Convert<ColorRGB16, byte, ColorRGB48, ushort>();
		ColorRGB16 convertedBack = converted.Convert<ColorRGB48, ushort, ColorRGB16, byte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGB48SignedIsLossless()
	{
		ColorRGB16 original = MakeRandomColor();
		ColorRGB48Signed converted = original.Convert<ColorRGB16, byte, ColorRGB48Signed, short>();
		ColorRGB16 convertedBack = converted.Convert<ColorRGB48Signed, short, ColorRGB16, byte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBA32IsLossless()
	{
		ColorRGB16 original = MakeRandomColor();
		ColorRGBA32 converted = original.Convert<ColorRGB16, byte, ColorRGBA32, byte>();
		ColorRGB16 convertedBack = converted.Convert<ColorRGBA32, byte, ColorRGB16, byte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBA32SignedIsLossless()
	{
		ColorRGB16 original = MakeRandomColor();
		ColorRGBA32Signed converted = original.Convert<ColorRGB16, byte, ColorRGBA32Signed, sbyte>();
		ColorRGB16 convertedBack = converted.Convert<ColorRGBA32Signed, sbyte, ColorRGB16, byte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBA64IsLossless()
	{
		ColorRGB16 original = MakeRandomColor();
		ColorRGBA64 converted = original.Convert<ColorRGB16, byte, ColorRGBA64, ushort>();
		ColorRGB16 convertedBack = converted.Convert<ColorRGBA64, ushort, ColorRGB16, byte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBA64SignedIsLossless()
	{
		ColorRGB16 original = MakeRandomColor();
		ColorRGBA64Signed converted = original.Convert<ColorRGB16, byte, ColorRGBA64Signed, short>();
		ColorRGB16 convertedBack = converted.Convert<ColorRGBA64Signed, short, ColorRGB16, byte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
}
