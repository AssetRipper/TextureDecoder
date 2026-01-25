namespace AssetRipper.TextureDecoder.Rgb;

internal static class SRgb
{
	public static T ToLinear<T>(T value) where T : unmanaged
	{
		double d = NumericConversion.Convert<T, double>(value);
		d = SRgbToLinear(d);
		return NumericConversion.Convert<double, T>(d);
	}

	public static T FromLinear<T>(T value) where T : unmanaged
	{
		double d = NumericConversion.Convert<T, double>(value);
		d = LinearToSRgb(d);
		return NumericConversion.Convert<double, T>(d);
	}

	private static double SRgbToLinear(double value)
	{
		if (value <= 0.04045)
		{
			return value / 12.92;
		}
		else
		{
			return double.Pow((value + 0.055) / 1.055, 2.4);
		}
	}

	private static double LinearToSRgb(double value)
	{
		if (value <= 0.0031308)
		{
			return value * 12.92;
		}
		else
		{
			return 1.055 * double.Pow(value, 1.0 / 2.4) - 0.055;
		}
	}
}
