namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	/// <summary>
	/// Also called R11G11B10_FLOAT
	/// </summary>
	/// <remarks>
	/// <see href="https://github.com/microsoft/DirectX-Graphics-Samples/blob/e5ea2ac7430ce39e6f6d619fd85ae32581931589/MiniEngine/Core/Shaders/PixelPacking_R11G11B10.hlsli#L31-L37" />
	/// </remarks>
	public partial struct ColorRGB32Half : IColor<Half>
	{
		private uint bits;

		/// <summary>
		/// 11 bits
		/// </summary>
		public Half R
		{
			readonly get
			{
				ushort value = (ushort)((bits << 4) & 0x7FF0u);
				return ToHalf(value);
			}
			set => bits = (bits & ~0x7FFu) | (ToUInt32(value) & 0x7FF0u) >> 4;
		}

		/// <summary>
		/// 11 bits
		/// </summary>
		public Half G
		{
			readonly get
			{
				ushort value = (ushort)((bits >> 7) & 0x7FF0);
				return ToHalf(value);
			}
			set => bits = (bits & ~0x3FF800u) | ((ToUInt32(value) >> 4) & 0x7FF0u) << 11;
		}

		/// <summary>
		/// 10 bits
		/// </summary>
		public Half B
		{
			readonly get
			{
				ushort value = (ushort)((bits >> 17) & 0x7FE0);
				return ToHalf(value);
			}
			set => bits = (bits & ~0xFFC00000u) | (((ToUInt32(value) >> 5) & 0x3FFu) << 22);
		}

		public readonly Half A
		{
			get => Half.One;
			set { }
		}

		public readonly void GetChannels(out Half r, out Half g, out Half b, out Half a)
		{
			DefaultColorMethods.GetChannels(this, out r, out g, out b, out a);
		}

		public void SetChannels(Half r, Half g, Half b, Half a)
		{
			bits = ((ToUInt32(r) >> 4) & 0x7FF) | (((ToUInt32(g) >> 4) & 0x7FF) << 11) | (((ToUInt32(b) >> 5) & 0x3FF) << 22);
		}

		private static Half ToHalf(ushort value) => Unsafe.As<ushort, Half>(ref Unsafe.AsRef(value));

		private static ushort ToUInt16(Half value) => Unsafe.As<Half, ushort>(ref Unsafe.AsRef(value));

		private static uint ToUInt32(Half value) => ToUInt16(value);
	}
}
