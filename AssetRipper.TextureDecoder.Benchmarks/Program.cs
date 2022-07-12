using BenchmarkDotNet.Running;

namespace AssetRipper.TextureDecoder.Benchmarks;

internal class Program
{
	static void Main()
	{
		BenchmarkRunner.Run(typeof(Program).Assembly);
	}
}