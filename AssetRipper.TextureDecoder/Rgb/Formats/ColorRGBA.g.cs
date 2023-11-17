// Auto-generated code. Do not modify manually.

namespace AssetRipper.TextureDecoder.Rgb.Formats;

public partial struct ColorRGBA<T> : IColor<T> where T : unmanaged
{
	public T R { get; set; }

	public T G { get; set; }

	public T B { get; set; }

	public T A { get; set; }

	public ColorRGBA(T r, T g, T b, T a)
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

	public override string ToString()
	{
		return $"{{ R: {R}, G: {G}, B: {B}, A: {A} }}";
	}
}
