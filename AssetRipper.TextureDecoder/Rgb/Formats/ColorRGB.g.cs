// Auto-generated code. Do not modify manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats;

[RgbaAttribute(RedChannel = true, GreenChannel = true, BlueChannel = true, AlphaChannel = false, FullyUtilizedChannels = true)]
public partial struct ColorRGB<T> : IColor<T> where T : unmanaged, INumberBase<T>, IMinMaxValue<T>
{
	public T R { get; set; }

	public T G { get; set; }

	public T B { get; set; }

	public readonly T A 
	{
		get => NumericConversion.GetMaximumValue<T>();
		set { }
	}

	public readonly void GetChannels(out T r, out T g, out T b, out T a)
	{
		r = R;
		g = G;
		b = B;
		a = A;
	}

	public void SetChannels(T r, T g, T b, T a)
	{
		R = r;
		G = g;
		B = b;
	}

	static bool IColor<T>.HasRedChannel => true;
	static bool IColor<T>.HasGreenChannel => true;
	static bool IColor<T>.HasBlueChannel => true;
	static bool IColor<T>.HasAlphaChannel => false;

	public override string ToString()
	{
		return $"{{ R: {R}, G: {G}, B: {B} }}";
	}
}
