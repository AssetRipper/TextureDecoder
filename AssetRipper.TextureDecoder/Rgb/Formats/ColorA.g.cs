// Auto-generated code. Do not modify manually.

namespace AssetRipper.TextureDecoder.Rgb.Formats;

public partial struct ColorA<T> : IColor<T> where T : unmanaged, INumberBase<T>, IMinMaxValue<T>
{
	public readonly T R 
	{
		get => NumericConversion.GetMinimumValueSafe<T>();
		set { }
	}

	public readonly T G 
	{
		get => NumericConversion.GetMinimumValueSafe<T>();
		set { }
	}

	public readonly T B 
	{
		get => NumericConversion.GetMinimumValueSafe<T>();
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

	static bool IColorBase.HasRedChannel => false;
	static bool IColorBase.HasGreenChannel => false;
	static bool IColorBase.HasBlueChannel => false;
	static bool IColorBase.HasAlphaChannel => true;
	static bool IColorBase.ChannelsAreFullyUtilized => true;
	static Type IColorBase.ChannelType => typeof(T);

	public override string ToString()
	{
		return $"{{ A: {A} }}";
	}
}
