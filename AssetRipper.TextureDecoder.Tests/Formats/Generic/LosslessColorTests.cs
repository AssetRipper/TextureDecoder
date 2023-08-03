using AssetRipper.TextureDecoder.Rgb.Formats;
using System.Numerics;

namespace AssetRipper.TextureDecoder.Tests.Formats.Generic;

/// <summary>
/// Tests for lossless color conversions.
/// </summary>
/// <typeparam name="T1">The "smaller" type that can be losslessly converted into <typeparamref name="T2"/>.</typeparam>
/// <typeparam name="T2">The "larger" type capable of storing <typeparamref name="T1"/> losslessly.</typeparam>
internal partial class LosslessColorTests<T1, T2>
	where T1 : unmanaged, INumberBase<T1>, IMinMaxValue<T1>
	where T2 : unmanaged, INumberBase<T2>, IMinMaxValue<T2>
{
	[Test]
	public void ConversionIsLossless_R_T1_R_T2() => LosslessConversion.Assert<ColorR<T1>, T1, ColorR<T2>, T2>();

	[Test]
	public void ConversionIsLossless_R_T1_RG_T2() => LosslessConversion.Assert<ColorR<T1>, T1, ColorRG<T2>, T2>();

	[Test]
	public void ConversionIsLossless_R_T1_RGB_T2() => LosslessConversion.Assert<ColorR<T1>, T1, ColorRGB<T2>, T2>();

	[Test]
	public void ConversionIsLossless_R_T1_RGBA_T2() => LosslessConversion.Assert<ColorR<T1>, T1, ColorRGBA<T2>, T2>();

	[Test]
	public void ConversionIsLossless_RG_T1_RGB_T2() => LosslessConversion.Assert<ColorRG<T1>, T1, ColorRGB<T2>, T2>();

	[Test]
	public void ConversionIsLossless_RG_T1_RGBA_T2() => LosslessConversion.Assert<ColorRG<T1>, T1, ColorRGBA<T2>, T2>();

	[Test]
	public void ConversionIsLossless_RGB_T1_RGBA_T2() => LosslessConversion.Assert<ColorRGB<T1>, T1, ColorRGBA<T2>, T2>();

	[Test]
	public void ConversionIsLossless_A_T1_RGBA_T2() => LosslessConversion.Assert<ColorA<T1>, T1, ColorRGBA<T2>, T2>();
}
