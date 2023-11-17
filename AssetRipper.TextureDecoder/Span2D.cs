namespace AssetRipper.TextureDecoder;

public readonly ref struct Span2D<T>
{
	public int Width { get; }
	public int Height { get; }
	public Span<T> Data { get; }

	public Span2D(Span<T> data, int width, int height)
	{
		Validate(data, width, height);
		Data = data;
		Width = width;
		Height = height;

		static void Validate(Span<T> data, int width, int height)
		{
			ArgumentOutOfRangeException.ThrowIfNegative(width);
			ArgumentOutOfRangeException.ThrowIfNegative(height);
			if (data.Length != width * height)
			{
				throw new ArgumentException("Data length does not match width and height.", nameof(data));
			}
		}
	}

	//public Span<T> this[int y] => Data.Slice(y * Width, Width);

	public T this[int x, int y]
	{
		get => Data[y * Width + x];
		set => Data[y * Width + x] = value;
	}

	public static implicit operator ReadOnlySpan2D<T>(Span2D<T> span) => new(span.Data, span.Width, span.Height);

	public static explicit operator Span2D<T>(T[] array) => new(array, array.Length, 1);

	public static explicit operator Span2D<T>(Span<T> span) => new(span, span.Length, 1);
}
