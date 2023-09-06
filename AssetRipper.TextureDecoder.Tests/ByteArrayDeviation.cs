namespace AssetRipper.TextureDecoder.Tests;

internal static class ByteArrayDeviation
{
	internal static void AssertMinimalDeviation(byte[] decoded, byte[] original, double maxMeanDeviation, double maxStandardDeviation)
	{
		if (decoded.Length != original.Length)
		{
			Assert.Fail($"Differing array lengths\nDecoded length {decoded.Length}\nOriginal length {original.Length}");
			return;
		}

		long differenceSum = 0;

		for (int i = 0; i < decoded.Length; i++)
		{
			differenceSum += decoded[i] - original[i];
		}

		double mean = differenceSum / (double)decoded.Length;

		double sumOfSquaredDeviations = 0;

		for (int i = 0; i < decoded.Length; i++)
		{
			double deviation = decoded[i] - original[i] - mean;
			sumOfSquaredDeviations += deviation * deviation;
		}

		double standardDeviation = Math.Sqrt(sumOfSquaredDeviations / (decoded.Length - 1));
		//Not sure if Bessel's correction is needed here, but it doesn't hurt, especially since length is around 1 million in the current use.

		Assert.Multiple(() =>
		{
			Assert.That(mean, Is.LessThan(maxMeanDeviation), "Mean too far positive");
			Assert.That(mean, Is.GreaterThan(-maxMeanDeviation), "Mean too far negative");
			Assert.That(standardDeviation, Is.LessThan(maxStandardDeviation), "Standard deviation too large");
		});
	}
}
