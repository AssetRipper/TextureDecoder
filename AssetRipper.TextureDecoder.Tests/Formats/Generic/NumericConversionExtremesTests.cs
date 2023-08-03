using AssetRipper.TextureDecoder.Rgb;
using System.Numerics;

namespace AssetRipper.TextureDecoder.Tests.Formats.Generic;

[TestFixture(TypeArgs = new Type[] { typeof(sbyte), typeof(byte) })]
[TestFixture(TypeArgs = new Type[] { typeof(short), typeof(ushort) })]
[TestFixture(TypeArgs = new Type[] { typeof(int), typeof(uint) })]
[TestFixture(TypeArgs = new Type[] { typeof(long), typeof(ulong) })]
[TestFixture(TypeArgs = new Type[] { typeof(Int128), typeof(UInt128) })]
internal class NumericConversionExtremesTests<TSigned, TUnsigned>
    where TSigned : unmanaged, IMinMaxValue<TSigned>
    where TUnsigned : unmanaged, IMinMaxValue<TUnsigned>
{
    [Test]
    public void SignedToUnsignedExtremesAreCorrect()
    {
        Assert.Multiple(() =>
        {
            Assert.That(NumericConversion.Convert<TSigned, TUnsigned>(TSigned.MinValue), Is.EqualTo(TUnsigned.MinValue));
            Assert.That(NumericConversion.Convert<TSigned, TUnsigned>(TSigned.MaxValue), Is.EqualTo(TUnsigned.MaxValue));
        });
    }

    [Test]
    public void UnsignedToSignedExtremesAreCorrect()
    {
        Assert.Multiple(() =>
        {
            Assert.That(NumericConversion.Convert<TUnsigned, TSigned>(TUnsigned.MinValue), Is.EqualTo(TSigned.MinValue));
            Assert.That(NumericConversion.Convert<TUnsigned, TSigned>(TUnsigned.MaxValue), Is.EqualTo(TSigned.MaxValue));
        });
    }
}
