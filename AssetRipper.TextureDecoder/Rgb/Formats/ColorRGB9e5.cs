using static System.Math;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	/// <summary>
	/// 9 bits each for RGB and 5 bits for an exponent
	/// </summary>
	public struct ColorRGB9e5 : IColor<double>
	{
		private uint bits;

		public double R
		{
			get => RBits * Scale;
			set
			{
				GetChannels(out _, out double g, out double b, out _);
				SetChannels(value, g, b, default);
			}
		}

		public double G
		{
			get => GBits * Scale;
			set
			{
				GetChannels(out double r, out _, out double b, out _);
				SetChannels(r, value, b, default);
			}
		}

		public double B
		{
			get => BBits * Scale;
			set
			{
				GetChannels(out double r, out double g, out _, out _);
				SetChannels(r, g, value, default);
			}
		}

		public double A
		{
			get => 1;
			set { }
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public void GetChannels(out double r, out double g, out double b, out double a)
		{
			double scale = Scale;
			r = RBits * scale;
			g = GBits * scale;
			b = BBits * scale;
			a = 1;
		}

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		public void SetChannels(double r, double g, double b, double a)
		{
			int exponent = CalculateExponent(r, g, b);
			double scale = Pow(2, exponent);
			uint rBits = (uint)(r / scale) & 0x1FF;
			uint gBits = (uint)(g / scale) & 0x1FF;
			uint bBits = (uint)(b / scale) & 0x1FF;
			uint exponentBits = unchecked((uint)(exponent + 24));
			bits = (exponentBits << 27) | (bBits << 18) | (gBits << 9) | (rBits << 0);
		}

		/// <summary>
		/// Range: -24 to 7 inclusive
		/// </summary>
		private int Exponent => unchecked((int)(bits >> 27) - 24);
		private double Scale => Pow(2, Exponent);
		private uint RBits => (bits >> 0) & 0x1FF;
		private uint GBits => (bits >> 9) & 0x1FF;
		private uint BBits => (bits >> 18) & 0x1FF;

		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static int CalculateExponent(double r, double g, double b)
		{
			double maxChannel = Max(r, Max(g, b));
			double minExponent = Log2(maxChannel / 0x1FF);
			return (int)Ceiling(minExponent);
		}

#if NETSTANDARD
		private static readonly double LogTwoConstant = Log(2);
		[MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
		private static double Log2(double value) => Log(value) / LogTwoConstant;
#endif
	}
}
