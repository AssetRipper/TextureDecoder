// Auto-generated code. Do not modify manually.

namespace AssetRipper.TextureDecoder.Rgb.Formats;

public partial struct ColorA<T> : IColor<ColorA<T>, T> where T : unmanaged, INumberBase<T>, IMinMaxValue<T>
{
	public T A { get; set; }

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

	public ColorA(T a)
	{
		A = a;
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
		A = a;
	}

	static bool IColor.HasRedChannel => false;
	static bool IColor.HasGreenChannel => false;
	static bool IColor.HasBlueChannel => false;
	static bool IColor.HasAlphaChannel => true;
	static bool IColor.ChannelsAreFullyUtilized => true;
	static Type IColor.ChannelType => typeof(T);

	public static ColorA<T> Black => new(NumericConversion.GetMaximumValueSafe<T>());
	static ColorA<T> IColor<ColorA<T>, T>.White => throw new NotSupportedException();

	public override string ToString()
	{
		return $"{{ A: {A} }}";
	}
}
