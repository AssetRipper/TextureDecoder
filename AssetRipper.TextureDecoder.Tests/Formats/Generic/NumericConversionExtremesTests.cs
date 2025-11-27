using AssetRipper.TextureDecoder.Rgb;
using System.Numerics;

namespace AssetRipper.TextureDecoder.Tests.Formats.Generic;

[TestFixture(TypeArgs = [typeof(sbyte), typeof(byte)])]
[TestFixture(TypeArgs = [typeof(short), typeof(ushort)])]
[TestFixture(TypeArgs = [typeof(int), typeof(uint)])]
[TestFixture(TypeArgs = [typeof(long), typeof(ulong)])]
[TestFixture(TypeArgs = [typeof(Int128), typeof(UInt128)])]
internal class NumericConversionExtremesTests<TSigned, TUnsigned>
	where TSigned : unmanaged, IMinMaxValue<TSigned>
	where TUnsigned : unmanaged, IMinMaxValue<TUnsigned>
{
	[Test]
	public void SignedToUnsignedExtremesAreCorrect()
	{
		using (Assert.EnterMultipleScope())
		{
			Assert.That(NumericConversion.Convert<TSigned, TUnsigned>(TSigned.MinValue), Is.EqualTo(TUnsigned.MinValue));
			Assert.That(NumericConversion.Convert<TSigned, TUnsigned>(TSigned.MaxValue), Is.EqualTo(TUnsigned.MaxValue));
		}
	}

	[Test]
	public void UnsignedToSignedExtremesAreCorrect()
	{
		using (Assert.EnterMultipleScope())
		{
			Assert.That(NumericConversion.Convert<TUnsigned, TSigned>(TUnsigned.MinValue), Is.EqualTo(TSigned.MinValue));
			Assert.That(NumericConversion.Convert<TUnsigned, TSigned>(TUnsigned.MaxValue), Is.EqualTo(TSigned.MaxValue));
		}
	}
}
