using System.Runtime.CompilerServices;

namespace IntegerConversion.ConstantEstimation;

internal class Program
{
	static void Main(string[] args)
	{
		int originalBitCount = int.Parse(args[0]);
		int newBitCount = int.Parse(args[1]);
		bool perfectMatch = EstimateConstants(originalBitCount, newBitCount, out uint a, out long b, out int c);
		Console.WriteLine("Done!");
		Console.WriteLine($"Result: ({a} * x + {b}) >> {c}");
		Console.WriteLine($"Perfect match: {perfectMatch}");
	}

	private static bool EstimateConstants(int originalBitCount, int newBitCount, out uint a, out long b, out int c)
	{
		a = default;
		b = default;
		c = default;
		double bestError = double.MaxValue;//Sum of squared errors
		uint originalMaxValue = GetIntegerMaxValue(originalBitCount);
		uint newMaxValue = GetIntegerMaxValue(newBitCount);
		int bitDiff = newBitCount - originalBitCount;

		//c: Max(0, -bitDiff) to 32 - newBitCount
		//If bitDiff is negative, it means we are going from large numbers to small numbers.
		//In other words, we will always have to shift right by at least enough to overcome the bit surplus.
		int minc = int.Max(0, -bitDiff);
		// We are limited to a 32-bit result, so we cannot shift left so much that we exceed 32 bits.
		int maxc = 32 - newBitCount;

		for (int ic = minc; ic <= maxc; ic++)
		{
			Console.WriteLine(ic);

			//a: 2^c * newMaxValue / originalMaxValue rounded to nearest integer
			double ia_exact = (double)Pow2(ic) * newMaxValue / originalMaxValue;

			ReadOnlySpan<uint> ia_values = ia_exact < 1 ? [(uint)double.Ceiling(ia_exact)] : [(uint)double.Floor(ia_exact), (uint)double.Ceiling(ia_exact)];

			foreach (uint ia in ia_values)
			{
				//b: 0 to 2^c - 1
				//Any bigger and it will cause 0 not to correspond with 0.
				uint minb;
				uint maxb;
				{
					//We can restrict b further by ensuring that originalMaxValue maps to newMaxValue.
					long minb_one = (long)(newMaxValue << ic) - (ia * originalMaxValue);
					long maxb_one = minb_one + Pow2(ic) - 1;

					minb = (uint)long.Max(0, minb_one);
					maxb = (uint)long.Min(Pow2(ic) - 1, long.Max(0, maxb_one));
					minb = uint.Min(minb, maxb);
				}

				//Errors for b are nearly parabolic, so we try to find the minimum by ternary search.
				//This is not guaranteed to find the absolute minimum since there's noise in the error, but it should get close enough.
				uint current_b_low = minb;
				uint current_b_high = maxb;
				while (current_b_high - current_b_low > 3)
				{
					uint range = current_b_high - current_b_low;
					uint b1 = current_b_low + range / 3;
					uint b2 = current_b_high - range / 3;
					double error1 = CalculateMeanSquaredError(ia, b1, ic, originalMaxValue, newMaxValue, out _);
					double error2 = CalculateMeanSquaredError(ia, b2, ic, originalMaxValue, newMaxValue, out _);
					if (error1 < error2)
					{
						current_b_high = b2;
					}
					else
					{
						current_b_low = b1;
					}
				}

				for (uint ib = current_b_low; ib <= current_b_high; ib++)
				{
					double error = CalculateMeanSquaredError(ia, ib, ic, originalMaxValue, newMaxValue, out bool anyIncorrect);
					if (error < bestError)
					{
						bestError = error;
						a = ia;
						b = ib;
						c = ic;
						Console.WriteLine($"New best: a={a}, b={b}, c={c}, MSE={bestError}");
					}
					if (!anyIncorrect)
					{
						//Perfect match
						return true;
					}
				}
			}
		}

		return false;
	}

	private static double CalculateMeanSquaredError(uint a, uint b, int c, uint originalMaxValue, uint newMaxValue, out bool anyIncorrect)
	{
		double meanSquaredError = 0;
		anyIncorrect = false;
		for (uint x = 0; x <= originalMaxValue; x++)
		{
			uint y = ConvertEstimate(x, a, b, c);
			double yExact = (double)x * newMaxValue / originalMaxValue;

			double diff = yExact - y;
			meanSquaredError = meanSquaredError * x / (x + 1) + (diff * diff) / (x + 1);

			uint yExactInt = (uint)double.Round(yExact, MidpointRounding.AwayFromZero);
			anyIncorrect |= (y != yExactInt);
		}
		return meanSquaredError;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static uint ConvertEstimate(uint original, uint a, uint b, int c)
	{
		unchecked
		{
			return ((original * a) + b) >> c;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static uint GetIntegerMaxValue(int bitCount)
	{
		return Pow2(bitCount) - 1;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	private static uint Pow2(int exponent)
	{
		return (1u << exponent);
	}
}
