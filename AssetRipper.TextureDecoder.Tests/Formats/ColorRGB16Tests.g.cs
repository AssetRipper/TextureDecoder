// Auto-generated code. Do not modify manually.

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

	public static ColorRGB16 MakeRandomColor() => ColorRandom<ColorRGB16, byte>.MakeRandomColor();

	public static byte MakeRandomValue() => ColorRandom<ColorRGB16, byte>.MakeRandomValue();

	[Test]
	public void ConversionIsLosslessToColorARGB32()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorARGB32, byte>();
	}

	[Test]
	public void ConversionIsLosslessToColorBGRA32()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorBGRA32, byte>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_sbyte()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGBA<sbyte>, sbyte>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGB_sbyte()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGB<sbyte>, sbyte>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_byte()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGBA<byte>, byte>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGB_byte()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGB<byte>, byte>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_short()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGBA<short>, short>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGB_short()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGB<short>, short>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_ushort()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGBA<ushort>, ushort>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGB_ushort()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGB<ushort>, ushort>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_int()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGBA<int>, int>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGB_int()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGB<int>, int>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_uint()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGBA<uint>, uint>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGB_uint()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGB<uint>, uint>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_long()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGBA<long>, long>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGB_long()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGB<long>, long>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_ulong()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGBA<ulong>, ulong>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGB_ulong()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGB<ulong>, ulong>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_Int128()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGBA<Int128>, Int128>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGB_Int128()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGB<Int128>, Int128>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_UInt128()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGBA<UInt128>, UInt128>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGB_UInt128()
	{
		LosslessConversion.Assert<ColorRGB16, byte, ColorRGB<UInt128>, UInt128>();
	}
}
