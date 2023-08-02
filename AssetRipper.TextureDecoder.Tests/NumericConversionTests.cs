using AssetRipper.TextureDecoder.Rgb;

namespace AssetRipper.TextureDecoder.Tests;
internal class NumericConversionTests
{
	[Test]
	public void Int32ToUInt32ExtremesAreCorrect()
	{
		Assert.Multiple(() =>
		{
			Assert.That(NumericConversion.Convert<int, uint>(int.MinValue), Is.EqualTo(uint.MinValue));
			Assert.That(NumericConversion.Convert<int, uint>(int.MaxValue), Is.EqualTo(uint.MaxValue));
		});
	}

	[Test]
	public void UInt32ToInt32ExtremesAreCorrect()
	{
		Assert.Multiple(() =>
		{
			Assert.That(NumericConversion.Convert<uint, int>(uint.MinValue), Is.EqualTo(int.MinValue));
			Assert.That(NumericConversion.Convert<uint, int>(uint.MaxValue), Is.EqualTo(int.MaxValue));
		});
	}
}
