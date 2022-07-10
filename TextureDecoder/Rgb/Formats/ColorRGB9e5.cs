using static System.Math;

namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	/// <summary>
	/// 9 bits each for RGB and 5 bits for an exponent
	/// </summary>
	public struct ColorRGB9e5 : IColor<float>
	{
		private uint bits;

		public float R
		{
			get => (float)(RBits * Scale);
			set
			{
				GetChannels(out _, out float g, out float b, out _);
				SetChannels(value, g, b, default);
			}
		}

		public float G
		{
			get => (float)(GBits * Scale);
			set
			{
				GetChannels(out float r, out _, out float b, out _);
				SetChannels(r, value, b, default);
			}
		}

		public float B
		{
			get => (float)(BBits * Scale);
			set
			{
				GetChannels(out float r, out float g, out _, out _);
				SetChannels(r, g, value, default);
			}
		}

		public float A
		{
			get => 1;
			set { }
		}

		public void GetChannels(out float r, out float g, out float b, out float a)
		{
			double scale = Scale;
			r = (float)(RBits * scale);
			g = (float)(GBits * scale);
			b = (float)(BBits * scale);
			a = 1;
		}

		public void SetChannels(float r, float g, float b, float a)
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

		private static int CalculateExponent(float r, float g, float b)
		{
			float maxChannel = Max(r, Max(g, b));
			double minExponent = Log2(maxChannel / 0x1FF);
			return (int)Ceiling(minExponent);
		}

#if NETSTANDARD
		private static readonly double LogTwoConstant = Log(2);
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static double Log2(double value) => Log(value) / LogTwoConstant;
#endif
	}
}
