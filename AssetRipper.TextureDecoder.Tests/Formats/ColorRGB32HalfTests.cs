using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.Tests.Formats;

[Ignore($"Ignore. Bugs in {nameof(ColorRGB32Half)}")]
public partial class ColorRGB32HalfTests
{
	[Test]
	public void MaxValueIsOne()
	{
		Assert.Multiple(() =>
		{
			ColorRGB32Half color = Unsafe.As<uint, ColorRGB32Half>(ref Unsafe.AsRef(uint.MaxValue));
			Assert.That(color.R, Is.EqualTo(Half.One), "R");
			Assert.That(color.G, Is.EqualTo(Half.One), "G");
			Assert.That(color.B, Is.EqualTo(Half.One), "B");
			Assert.That(color.A, Is.EqualTo(Half.One), "A");
		});
	}
}
