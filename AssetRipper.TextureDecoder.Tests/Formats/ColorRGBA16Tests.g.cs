// Auto-generated code. Do not modify manually.

using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.Tests.Formats;

public partial class ColorRGBA16Tests
{
	[Test]
	public void CorrectSizeTest()
	{
		Assert.That(Unsafe.SizeOf<ColorRGBA16>(), Is.EqualTo(2));
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

	public static ColorRGBA16 MakeRandomColor() => ColorRandom<ColorRGBA16, byte>.MakeRandomColor();

	public static byte MakeRandomValue() => ColorRandom<ColorRGBA16, byte>.MakeRandomValue();

	[Test]
	public void ConversionIsLosslessToColorARGB32()
	{
		LosslessConversion.Assert<ColorRGBA16, byte, ColorARGB32, byte>();
	}

	[Test]
	public void ConversionIsLosslessToColorBGRA32()
	{
		LosslessConversion.Assert<ColorRGBA16, byte, ColorBGRA32, byte>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_sbyte()
	{
		LosslessConversion.Assert<ColorRGBA16, byte, ColorRGBA<sbyte>, sbyte>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_byte()
	{
		LosslessConversion.Assert<ColorRGBA16, byte, ColorRGBA<byte>, byte>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_short()
	{
		LosslessConversion.Assert<ColorRGBA16, byte, ColorRGBA<short>, short>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_ushort()
	{
		LosslessConversion.Assert<ColorRGBA16, byte, ColorRGBA<ushort>, ushort>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_int()
	{
		LosslessConversion.Assert<ColorRGBA16, byte, ColorRGBA<int>, int>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_uint()
	{
		LosslessConversion.Assert<ColorRGBA16, byte, ColorRGBA<uint>, uint>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_long()
	{
		LosslessConversion.Assert<ColorRGBA16, byte, ColorRGBA<long>, long>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_ulong()
	{
		LosslessConversion.Assert<ColorRGBA16, byte, ColorRGBA<ulong>, ulong>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_Int128()
	{
		LosslessConversion.Assert<ColorRGBA16, byte, ColorRGBA<Int128>, Int128>();
	}

	[Test]
	public void ConversionIsLosslessToColorRGBA_UInt128()
	{
		LosslessConversion.Assert<ColorRGBA16, byte, ColorRGBA<UInt128>, UInt128>();
	}
}
