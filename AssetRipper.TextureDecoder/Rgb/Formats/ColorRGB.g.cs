// Auto-generated code. Do not modify manually.

namespace AssetRipper.TextureDecoder.Rgb.Formats;

public partial struct ColorRGB<T> : IColor<T> where T : unmanaged, INumberBase<T>, IMinMaxValue<T>
{
	public T R { get; set; }

	public T G { get; set; }

	public T B { get; set; }

	public readonly T A 
	{
		get => NumericConversion.GetMaximumValueSafe<T>();
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

	static bool IColorBase.HasRedChannel => true;
	static bool IColorBase.HasGreenChannel => true;
	static bool IColorBase.HasBlueChannel => true;
	static bool IColorBase.HasAlphaChannel => false;
	static bool IColorBase.ChannelsAreFullyUtilized => true;
	static Type IColorBase.ChannelType => typeof(T);

	public override string ToString()
	{
		return $"{{ R: {R}, G: {G}, B: {B} }}";
	}
}
