// Auto-generated code. Do not modify manually.

namespace AssetRipper.TextureDecoder.Rgb.Formats;

public partial struct ColorARGB<T> : IColor<ColorARGB<T>, T> where T : unmanaged, INumberBase<T>, IMinMaxValue<T>
{
	public T A { get; set; }

	public T R { get; set; }

	public T G { get; set; }

	public T B { get; set; }

	public ColorARGB(T r, T g, T b, T a)
	{
		R = r;
		G = g;
		B = b;
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
		R = r;
		G = g;
		B = b;
		A = a;
	}

	static bool IColor.HasRedChannel => true;
	static bool IColor.HasGreenChannel => true;
	static bool IColor.HasBlueChannel => true;
	static bool IColor.HasAlphaChannel => true;
	static bool IColor.ChannelsAreFullyUtilized => true;
	static Type IColor.ChannelType => typeof(T);

	public static ColorARGB<T> Black => new(NumericConversion.GetMinimumValueSafe<T>(), NumericConversion.GetMinimumValueSafe<T>(), NumericConversion.GetMinimumValueSafe<T>(), NumericConversion.GetMaximumValueSafe<T>());
	public static ColorARGB<T> White => new(NumericConversion.GetMaximumValueSafe<T>(), NumericConversion.GetMaximumValueSafe<T>(), NumericConversion.GetMaximumValueSafe<T>(), NumericConversion.GetMaximumValueSafe<T>());

	public override string ToString()
	{
		return $"{{ R: {R}, G: {G}, B: {B}, A: {A} }}";
	}
}
