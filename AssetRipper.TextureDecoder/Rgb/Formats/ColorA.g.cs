// Auto-generated code. Do not modify manually.

using AssetRipper.TextureDecoder.Attributes;

namespace AssetRipper.TextureDecoder.Rgb.Formats;

[RgbaAttribute(RedChannel = false, GreenChannel = false, BlueChannel = false, AlphaChannel = true, FullyUtilizedChannels = true)]
public partial struct ColorA<T> : IColor<T> where T : unmanaged, INumberBase<T>, IMinMaxValue<T>
{
	public readonly T R 
	{
		get => NumericConversion.GetMinimumValue<T>();
		set { }
	}

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

	public T A { get; set; }

	public readonly void GetChannels(out T r, out T g, out T b, out T a)
	{
		r = R;
		g = G;
		b = B;
		a = A;
	}

	public void SetChannels(T r, T g, T b, T a)
	{
		A = a;
	}

	static bool IColor<T>.HasRedChannel => false;
	static bool IColor<T>.HasGreenChannel => false;
	static bool IColor<T>.HasBlueChannel => false;
	static bool IColor<T>.HasAlphaChannel => true;

	public override string ToString()
	{
		return $"{{ A: {A} }}";
	}
}
