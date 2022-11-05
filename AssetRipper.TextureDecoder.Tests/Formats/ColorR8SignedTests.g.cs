//This code is source generated. Do not edit manually.

using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.Tests.Formats;

public partial class ColorR8SignedTests
{
	[Test]
	public void CorrectSizeTest()
	{
		Assert.That(Unsafe.SizeOf<ColorR8Signed>(), Is.EqualTo(1));
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
	
	public static ColorR8Signed MakeRandomColor()
	{
		return new()
		{
			R = -0b01010101,
			G = 0b01110010,
			B = -0b00001111,
			A = -0b01000111,
		};
	}
	
	[Test]
	public void ConversionToColorARGB32IsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorARGB32 converted = original.Convert<ColorR8Signed, sbyte, ColorARGB32, byte>();
		ColorR8Signed convertedBack = converted.Convert<ColorARGB32, byte, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorBGRA32IsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorBGRA32 converted = original.Convert<ColorR8Signed, sbyte, ColorBGRA32, byte>();
		ColorR8Signed convertedBack = converted.Convert<ColorBGRA32, byte, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorR16IsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorR16 converted = original.Convert<ColorR8Signed, sbyte, ColorR16, ushort>();
		ColorR8Signed convertedBack = converted.Convert<ColorR16, ushort, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorR16SignedIsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorR16Signed converted = original.Convert<ColorR8Signed, sbyte, ColorR16Signed, short>();
		ColorR8Signed convertedBack = converted.Convert<ColorR16Signed, short, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorR8IsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorR8 converted = original.Convert<ColorR8Signed, sbyte, ColorR8, byte>();
		ColorR8Signed convertedBack = converted.Convert<ColorR8, byte, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorR8SignedIsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorR8Signed converted = original.Convert<ColorR8Signed, sbyte, ColorR8Signed, sbyte>();
		ColorR8Signed convertedBack = converted.Convert<ColorR8Signed, sbyte, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRG16IsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorRG16 converted = original.Convert<ColorR8Signed, sbyte, ColorRG16, byte>();
		ColorR8Signed convertedBack = converted.Convert<ColorRG16, byte, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRG16SignedIsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorRG16Signed converted = original.Convert<ColorR8Signed, sbyte, ColorRG16Signed, sbyte>();
		ColorR8Signed convertedBack = converted.Convert<ColorRG16Signed, sbyte, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRG32IsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorRG32 converted = original.Convert<ColorR8Signed, sbyte, ColorRG32, ushort>();
		ColorR8Signed convertedBack = converted.Convert<ColorRG32, ushort, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRG32SignedIsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorRG32Signed converted = original.Convert<ColorR8Signed, sbyte, ColorRG32Signed, short>();
		ColorR8Signed convertedBack = converted.Convert<ColorRG32Signed, short, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGB24IsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorRGB24 converted = original.Convert<ColorR8Signed, sbyte, ColorRGB24, byte>();
		ColorR8Signed convertedBack = converted.Convert<ColorRGB24, byte, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGB24SignedIsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorRGB24Signed converted = original.Convert<ColorR8Signed, sbyte, ColorRGB24Signed, sbyte>();
		ColorR8Signed convertedBack = converted.Convert<ColorRGB24Signed, sbyte, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGB48IsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorRGB48 converted = original.Convert<ColorR8Signed, sbyte, ColorRGB48, ushort>();
		ColorR8Signed convertedBack = converted.Convert<ColorRGB48, ushort, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGB48SignedIsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorRGB48Signed converted = original.Convert<ColorR8Signed, sbyte, ColorRGB48Signed, short>();
		ColorR8Signed convertedBack = converted.Convert<ColorRGB48Signed, short, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBA32IsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorRGBA32 converted = original.Convert<ColorR8Signed, sbyte, ColorRGBA32, byte>();
		ColorR8Signed convertedBack = converted.Convert<ColorRGBA32, byte, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBA32SignedIsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorRGBA32Signed converted = original.Convert<ColorR8Signed, sbyte, ColorRGBA32Signed, sbyte>();
		ColorR8Signed convertedBack = converted.Convert<ColorRGBA32Signed, sbyte, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBA64IsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorRGBA64 converted = original.Convert<ColorR8Signed, sbyte, ColorRGBA64, ushort>();
		ColorR8Signed convertedBack = converted.Convert<ColorRGBA64, ushort, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
	
	[Test]
	public void ConversionToColorRGBA64SignedIsLossless()
	{
		ColorR8Signed original = MakeRandomColor();
		ColorRGBA64Signed converted = original.Convert<ColorR8Signed, sbyte, ColorRGBA64Signed, short>();
		ColorR8Signed convertedBack = converted.Convert<ColorRGBA64Signed, short, ColorR8Signed, sbyte>();
		Assert.That(convertedBack, Is.EqualTo(original));
	}
}
