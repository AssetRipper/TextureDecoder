using System.Runtime.CompilerServices;

namespace IntegerConversion.ConstantEstimation;

internal class Program
{
	static void Main(string[] args)
	{
		int originalBitCount = int.Parse(args[0]);
		int newBitCount = int.Parse(args[1]);
		EstimateConstants(originalBitCount, newBitCount, out uint a, out uint b, out int c);
		Console.WriteLine("Done!");
		Console.WriteLine($"Result: ({a} * x + {b}) >> {c}");
	}

	private static void EstimateConstants(int originalBitCount, int newBitCount, out uint a, out uint b, out int c)
	{
		a = default;
		b = default;
		c = default;
		float bestError = float.MaxValue;//Sum of squared errors
		uint originalMaxValue = GetIntegerMaxValue(originalBitCount);
		uint newMaxValue = GetIntegerMaxValue(newBitCount);
		int bitDiff = newBitCount - originalBitCount;

		//c: Max(0, -bitDiff) to 32 - newBitCount
		//If bitDiff is negative, it means we are going from large numbers to small numbers.
		//In other words, we will always have to shift right by at least enough to overcome the bit surplus.
		int minc = int.Max(0, -bitDiff);
		int maxc = 32 - newBitCount;

		for (int ic = minc; ic <= maxc; ic++)
		{
			Console.WriteLine(ic);

			//a: 2 ^ (c + bitDiff) to 2 ^ (c + bitDiff + 1)
			uint mina = Pow2(ic + bitDiff);
			uint maxa = Pow2(ic + bitDiff + 1);

			//b: 0 to 2^c - 1
			//Any bigger and it will cause 0 not to correspond with 0.
			uint minb = 0;
			uint maxb = Pow2(ic) - 1;

			for (uint ia = mina; ia <= maxa; ia++)
			{
				if (ConvertEstimate(originalMaxValue, ia, minb, ic) > newMaxValue || ConvertEstimate(originalMaxValue, ia, maxb, ic) < newMaxValue)
				{
					continue;
				}

				for (uint ib = minb; ib <= maxb; ib++)
				{
					if (ConvertEstimate(originalMaxValue, ia, ib, ic) == newMaxValue)
					{
						float error = 0;
						bool anyIncorrect = false;
						for (uint x = 1; x < originalMaxValue; x++)
						{
							uint y = ConvertEstimate(x, ia, ib, ic);
							float yExact = (float)x * newMaxValue / originalMaxValue;
							uint yExactRounded = (uint)float.Round(yExact);
							anyIncorrect |= (y != yExactRounded);
							float diff = yExact - y;
							error += diff * diff;
						}
						if (!anyIncorrect)
						{
							a = ia;
							b = ib;
							c = ic;
							return;
						}
						if (error < bestError)
						{
							a = ia;
							b = ib;
							c = ic;
							bestError = error;
						}
					}
				}
			}
		}
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
