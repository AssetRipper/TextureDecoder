using AssetRipper.TextureDecoder.Rgb;
using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.Tests.Formats.Generic;

internal partial class GenericColorTests<TColor, TChannelValue> where TColor : unmanaged, IColor<TChannelValue> where TChannelValue : unmanaged
{
	[Test]
	public void CorrectSizeTest()
	{
		Assert.That(Unsafe.SizeOf<TColor>(), Is.EqualTo(Color.GetChannelCount<TColor>() * Unsafe.SizeOf<TChannelValue>()));
	}

	[Test]
	public void PropertyIsSymmetric_R()
	{
		TColor color = MakeRandomColor();
		TChannelValue r = color.R;
		color.R = r;
		Assert.That(color.R, Is.EqualTo(r));
	}

	[Test]
	public void PropertyIsSymmetric_G()
	{
		TColor color = MakeRandomColor();
		TChannelValue g = color.G;
		color.G = g;
		Assert.That(color.G, Is.EqualTo(g));
	}

	[Test]
	public void PropertyIsSymmetric_B()
	{
		TColor color = MakeRandomColor();
		TChannelValue b = color.B;
		color.B = b;
		Assert.That(color.B, Is.EqualTo(b));
	}

	[Test]
	public void PropertyIsSymmetric_A()
	{
		TColor color = MakeRandomColor();
		TChannelValue a = color.A;
		color.A = a;
		Assert.That(color.A, Is.EqualTo(a));
	}

	[Test]
	public void ChannelsAreIndependent_R()
	{
		TColor color = MakeRandomColor();
		TChannelValue g = color.G;
		TChannelValue b = color.B;
		TChannelValue a = color.A;
		color.R = MakeRandomValue();
		using (Assert.EnterMultipleScope())
		{
			Assert.That(color.G, Is.EqualTo(g));
			Assert.That(color.B, Is.EqualTo(b));
			Assert.That(color.A, Is.EqualTo(a));
		}
	}

	[Test]
	public void ChannelsAreIndependent_G()
	{
		TColor color = MakeRandomColor();
		TChannelValue r = color.R;
		TChannelValue b = color.B;
		TChannelValue a = color.A;
		color.G = MakeRandomValue();
		using (Assert.EnterMultipleScope())
		{
			Assert.That(color.R, Is.EqualTo(r));
			Assert.That(color.B, Is.EqualTo(b));
			Assert.That(color.A, Is.EqualTo(a));
		}
	}

	[Test]
	public void ChannelsAreIndependent_B()
	{
		TColor color = MakeRandomColor();
		TChannelValue r = color.R;
		TChannelValue g = color.G;
		TChannelValue a = color.A;
		color.B = MakeRandomValue();
		using (Assert.EnterMultipleScope())
		{
			Assert.That(color.R, Is.EqualTo(r));
			Assert.That(color.G, Is.EqualTo(g));
			Assert.That(color.A, Is.EqualTo(a));
		}
	}

	[Test]
	public void ChannelsAreIndependent_A()
	{
		TColor color = MakeRandomColor();
		TChannelValue r = color.R;
		TChannelValue g = color.G;
		TChannelValue b = color.B;
		color.A = MakeRandomValue();
		using (Assert.EnterMultipleScope())
		{
			Assert.That(color.R, Is.EqualTo(r));
			Assert.That(color.G, Is.EqualTo(g));
			Assert.That(color.B, Is.EqualTo(b));
		}
	}

	[Test]
	public void GetMethodMatchesProperties()
	{
		TColor color = MakeRandomColor();
		color.GetChannels(out TChannelValue r, out TChannelValue g, out TChannelValue b, out TChannelValue a);
		using (Assert.EnterMultipleScope())
		{
			Assert.That(color.R, Is.EqualTo(r));
			Assert.That(color.G, Is.EqualTo(g));
			Assert.That(color.B, Is.EqualTo(b));
			Assert.That(color.A, Is.EqualTo(a));
		}
	}

	[Test]
	public void MethodsAreSymmetric()
	{
		TColor color = MakeRandomColor();
		color.GetChannels(out TChannelValue r, out TChannelValue g, out TChannelValue b, out TChannelValue a);
		color.SetChannels(r, g, b, a);
		using (Assert.EnterMultipleScope())
		{
			Assert.That(color.R, Is.EqualTo(r));
			Assert.That(color.G, Is.EqualTo(g));
			Assert.That(color.B, Is.EqualTo(b));
			Assert.That(color.A, Is.EqualTo(a));
		}
	}

	private static TChannelValue MakeRandomValue() => ColorRandom<TColor, TChannelValue>.MakeRandomValue();

	private static TColor MakeRandomColor() => ColorRandom<TColor, TChannelValue>.MakeRandomColor();
}
