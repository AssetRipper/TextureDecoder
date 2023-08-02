// Auto-generated code. Do not modify manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats;

[RgbaAttribute(RedChannel = true, GreenChannel = false, BlueChannel = false, AlphaChannel = false, FullyUtilizedChannels = true)]
public partial struct ColorR<T> : IColor<T> where T : unmanaged, INumberBase<T>, IMinMaxValue<T>
{
	public T R { get; set; }

	public readonly T G 
	{
		get => NumericConversion.GetMinimumValue<T>();
		set { }
	}

	public readonly T B 
	{
		get => NumericConversion.GetMinimumValue<T>();
		set { }
	}

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
	}

	static bool IColor<T>.HasRedChannel => true;
	static bool IColor<T>.HasGreenChannel => false;
	static bool IColor<T>.HasBlueChannel => false;
	static bool IColor<T>.HasAlphaChannel => false;

	public override string ToString()
	{
		return $"{{ R: {R} }}";
	}
}
