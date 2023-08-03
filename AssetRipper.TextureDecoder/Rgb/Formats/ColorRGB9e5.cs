namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	/// <summary>
	/// 9 bits each for RGB and 5 bits for an exponent
	/// </summary>
	public partial struct ColorRGB9e5 : IColor<double>
	{
		private uint bits;

		public double R
		{
			readonly get => RBits * Scale;
			set
			{
				GetChannels(out _, out double g, out double b, out _);
				SetChannels(value, g, b);
			}
		}

		public double G
		{
			readonly get => GBits * Scale;
			set
			{
				GetChannels(out double r, out _, out double b, out _);
				SetChannels(r, value, b);
			}
		}

		public double B
		{
			readonly get => BBits * Scale;
			set
			{
				GetChannels(out double r, out double g, out _, out _);
				SetChannels(r, g, value);
			}
		}

		public readonly double A
		{
			get => 1;
			set { }
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public readonly void GetChannels(out double r, out double g, out double b, out double a)
		{
			double scale = Scale;
			r = RBits * scale;
			g = GBits * scale;
			b = BBits * scale;
			a = 1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public void SetChannels(double r, double g, double b, double a)
		{
			SetChannels(r, g, b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public void SetChannels(double r, double g, double b)
		{
			int exponent = CalculateExponent(r, g, b);
			double scale = double.Pow(2, exponent);
			uint rBits = (uint)(r / scale) & ChannelBitMask;
			uint gBits = (uint)(g / scale) & ChannelBitMask;
			uint bBits = (uint)(b / scale) & ChannelBitMask;
			uint exponentBits = unchecked((uint)(exponent + 24));
			bits = (exponentBits << ExponentOffset) | (bBits << BlueOffset) | (gBits << GreenOffset) | (rBits << RedOffset);
		}

		/// <summary>
		/// Range: -24 to 7 inclusive
		/// </summary>
		private readonly int Exponent => unchecked((int)(bits >> ExponentOffset) - 24);
		private readonly double Scale => double.Pow(2, Exponent);
		private readonly uint RBits => (bits >> RedOffset) & ChannelBitMask;
		private readonly uint GBits => (bits >> GreenOffset) & ChannelBitMask;
		private readonly uint BBits => (bits >> BlueOffset) & ChannelBitMask;

		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		private static int CalculateExponent(double r, double g, double b)
		{
			double maxChannel = double.Max(r, double.Max(g, b));
			double minExponent = double.Log2(maxChannel / ChannelBitMask);
			return (int)double.Ceiling(minExponent);
		}

		private const int ChannelBitMask = 0x1FF;
		private const int RedOffset = 0;
		private const int GreenOffset = 9;
		private const int BlueOffset = 18;
		private const int ExponentOffset = 27;
	}
}
