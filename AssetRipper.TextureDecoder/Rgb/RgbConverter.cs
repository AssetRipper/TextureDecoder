namespace AssetRipper.TextureDecoder.Rgb
{
	public static class RgbConverter
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int Convert<TSourceColor, TSourceChannel, TDestinationColor, TDestinationChannel>(ReadOnlySpan<byte> input, int width, int height, out byte[] output)
			where TSourceChannel : unmanaged
			where TSourceColor : unmanaged, IColor<TSourceChannel>
			where TDestinationChannel : unmanaged
			where TDestinationColor : unmanaged, IColor<TDestinationChannel>
		{
			output = new byte[width * height * Unsafe.SizeOf<TDestinationColor>()];
			return Convert<TSourceColor, TSourceChannel, TDestinationColor, TDestinationChannel>(input, width, height, output);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static int Convert<TSourceColor, TSourceChannel, TDestinationColor, TDestinationChannel>(ReadOnlySpan<byte> input, int width, int height, Span<byte> output)
			where TSourceChannel : unmanaged
			where TSourceColor : unmanaged, IColor<TSourceChannel>
			where TDestinationChannel : unmanaged
			where TDestinationColor : unmanaged, IColor<TDestinationChannel>
		{
			ThrowHelper.ThrowIfNotLittleEndian();
			ReadOnlySpan<TSourceColor> sourceSpan = MemoryMarshal.Cast<byte, TSourceColor>(input).Slice(0, width * height);
			Span<TDestinationColor> destinationSpan = MemoryMarshal.Cast<byte, TDestinationColor>(output).Slice(0, width * height);
			Convert<TSourceColor, TSourceChannel, TDestinationColor, TDestinationChannel>(sourceSpan, destinationSpan);
			return width * height * Unsafe.SizeOf<TSourceColor>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static void Convert<TSourceColor, TSourceChannel, TDestinationColor, TDestinationChannel>(ReadOnlySpan<TSourceColor> sourceSpan, Span<TDestinationColor> destinationSpan)
			where TSourceChannel : unmanaged
			where TSourceColor : unmanaged, IColor<TSourceChannel>
			where TDestinationChannel : unmanaged
			where TDestinationColor : unmanaged, IColor<TDestinationChannel>
		{
			if (sourceSpan.Length is 0)
			{
				// Do nothing if the source span is empty
			}
			else if (destinationSpan.Length < sourceSpan.Length)
			{
				ThrowDestinationSpanNotLargeEnough();
			}
			else if (typeof(TSourceColor) != typeof(TDestinationColor))
			{
				for (int i = 0; i < sourceSpan.Length; i++)
				{
					destinationSpan[i] = sourceSpan[i].Convert<TSourceColor, TSourceChannel, TDestinationColor, TDestinationChannel>();
				}
			}
			else if (Unsafe.AreSame(in sourceSpan[0], ref Unsafe.As<TDestinationColor, TSourceColor>(ref destinationSpan[0])))
			{
				// Do nothing because the source and destination point to the same memory
			}
			else
			{
				// Note: This assumes that the source and destination will never overlap.
				sourceSpan.CopyTo(MemoryMarshal.Cast<TDestinationColor, TSourceColor>(destinationSpan));
			}

			static void ThrowDestinationSpanNotLargeEnough()
			{
				throw new ArgumentException("Destination span is not large enough to hold the converted data.", nameof(destinationSpan));
			}
		}

		public static void SRgbToLinear<TColor, TChannel>(Span<TColor> span)
			where TChannel : unmanaged
			where TColor : unmanaged, IColor<TChannel>
		{
			for (int i = 0; i < span.Length; i++)
			{
				span[i] = span[i].SRgbToLinear<TColor, TChannel>();
			}
		}

		public static void LinearToSRgb<TColor, TChannel>(Span<TColor> span)
			where TChannel : unmanaged
			where TColor : unmanaged, IColor<TChannel>
		{
			for (int i = 0; i < span.Length; i++)
			{
				span[i] = span[i].LinearToSRgb<TColor, TChannel>();
			}
		}
	}
}
