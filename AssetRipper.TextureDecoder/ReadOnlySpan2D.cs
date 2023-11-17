namespace AssetRipper.TextureDecoder;

public readonly ref struct ReadOnlySpan2D<T>
{
	public int Width { get; }
	public int Height { get; }
	public ReadOnlySpan<T> Data { get; }

	public ReadOnlySpan2D(ReadOnlySpan<T> data, int width, int height)
	{
		Validate(data, width, height);
		Data = data;
		Width = width;
		Height = height;

		static void Validate(ReadOnlySpan<T> data, int width, int height)
		{
			ArgumentOutOfRangeException.ThrowIfNegative(width);
			ArgumentOutOfRangeException.ThrowIfNegative(height);
			if (data.Length != width * height)
			{
				throw new ArgumentException("Data length does not match width and height.", nameof(data));
			}
		}
	}

	//public ReadOnlySpan<T> this[int y] => Data.Slice(y * Width, Width);

	public T this[int x, int y] => Data[y * Width + x];

	public static explicit operator ReadOnlySpan2D<T>(T[] array) => new(array, array.Length, 1);

	public static explicit operator ReadOnlySpan2D<T>(ReadOnlySpan<T> span) => new(span, span.Length, 1);
}
