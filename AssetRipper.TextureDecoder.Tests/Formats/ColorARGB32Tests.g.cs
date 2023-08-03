// Auto-generated code. Do not modify manually.

using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.Tests.Formats;

public partial class ColorARGB32Tests
{
	[Test]
	public void CorrectSizeTest()
	{
		Assert.That(Unsafe.SizeOf<ColorARGB32>(), Is.EqualTo(4));
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

	public static ColorARGB32 MakeRandomColor() => ColorRandom<ColorARGB32, byte>.MakeRandomColor();

	public static byte MakeRandomValue() => ColorRandom<ColorARGB32, byte>.MakeRandomValue();

	[Test]
	public void ConversionIsLosslessToColorARGB32()
	{
		LosslessConversion.Assert<ColorARGB32, byte, ColorARGB32, byte>();
	}

	[Test]
	public void ConversionIsLosslessToColorBGRA32()
	{
		LosslessConversion.Assert<ColorARGB32, byte, ColorBGRA32, byte>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_sbyte()
	{
		LosslessConversion.Assert<ColorARGB32, byte, ColorRGBA<sbyte>, sbyte>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_byte()
	{
		LosslessConversion.Assert<ColorARGB32, byte, ColorRGBA<byte>, byte>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_short()
	{
		LosslessConversion.Assert<ColorARGB32, byte, ColorRGBA<short>, short>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_ushort()
	{
		LosslessConversion.Assert<ColorARGB32, byte, ColorRGBA<ushort>, ushort>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_int()
	{
		LosslessConversion.Assert<ColorARGB32, byte, ColorRGBA<int>, int>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_uint()
	{
		LosslessConversion.Assert<ColorARGB32, byte, ColorRGBA<uint>, uint>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_long()
	{
		LosslessConversion.Assert<ColorARGB32, byte, ColorRGBA<long>, long>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_ulong()
	{
		LosslessConversion.Assert<ColorARGB32, byte, ColorRGBA<ulong>, ulong>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_Int128()
	{
		LosslessConversion.Assert<ColorARGB32, byte, ColorRGBA<Int128>, Int128>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_UInt128()
	{
		LosslessConversion.Assert<ColorARGB32, byte, ColorRGBA<UInt128>, UInt128>();
	}
}
