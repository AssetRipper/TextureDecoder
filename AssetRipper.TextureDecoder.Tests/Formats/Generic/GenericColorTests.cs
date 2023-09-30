using AssetRipper.TextureDecoder.Rgb;
using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.Tests.Formats.Generic;

internal partial class GenericColorTests<TColor, TChannel> where TColor : unmanaged, IColor<TChannel> where TChannel : unmanaged
{
	[Test]
	public void CorrectSizeTest()
	{
		Assert.That(Unsafe.SizeOf<TColor>(), Is.EqualTo(Color.GetChannelCount<TColor>() * Unsafe.SizeOf<TChannel>()));
	}

	[Test]
	public void PropertyIsSymmetric_R()
	{
		TColor color = MakeRandomColor();
		TChannel r = color.R;
		color.R = r;
		Assert.That(color.R, Is.EqualTo(r));
	}

	[Test]
	public void PropertyIsSymmetric_G()
	{
		TColor color = MakeRandomColor();
		TChannel g = color.G;
		color.G = g;
		Assert.That(color.G, Is.EqualTo(g));
	}

	[Test]
	public void PropertyIsSymmetric_B()
	{
		TColor color = MakeRandomColor();
		TChannel b = color.B;
		color.B = b;
		Assert.That(color.B, Is.EqualTo(b));
	}

	[Test]
	public void PropertyIsSymmetric_A()
	{
		TColor color = MakeRandomColor();
		TChannel a = color.A;
		color.A = a;
		Assert.That(color.A, Is.EqualTo(a));
	}

	[Test]
	public void ChannelsAreIndependent_R()
	{
		TColor color = MakeRandomColor();
		TChannel g = color.G;
		TChannel b = color.B;
		TChannel a = color.A;
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
		TColor color = MakeRandomColor();
		TChannel r = color.R;
		TChannel b = color.B;
		TChannel a = color.A;
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
		TColor color = MakeRandomColor();
		TChannel r = color.R;
		TChannel g = color.G;
		TChannel a = color.A;
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
		TColor color = MakeRandomColor();
		TChannel r = color.R;
		TChannel g = color.G;
		TChannel b = color.B;
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
		TColor color = MakeRandomColor();
		color.GetChannels(out TChannel r, out TChannel g, out TChannel b, out TChannel a);
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
		TColor color = MakeRandomColor();
		color.GetChannels(out TChannel r, out TChannel g, out TChannel b, out TChannel a);
		color.SetChannels(r, g, b, a);
		Assert.Multiple(() =>
		{
			Assert.That(color.R, Is.EqualTo(r));
			Assert.That(color.G, Is.EqualTo(g));
			Assert.That(color.B, Is.EqualTo(b));
			Assert.That(color.A, Is.EqualTo(a));
		});
	}

	private static TChannel MakeRandomValue() => ColorRandom<TColor, TChannel>.MakeRandomValue();

	private static TColor MakeRandomColor() => ColorRandom<TColor, TChannel>.MakeRandomColor();
}
