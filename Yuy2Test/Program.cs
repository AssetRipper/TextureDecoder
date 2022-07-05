using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using AssetRipper.TextureDecoder.Yuy2;

namespace Yuy2Test
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length < 3)
			{
				Console.WriteLine("Format: {path} {width} {height}");
				Console.ReadKey();
				return;
			}

			string path = args[0];
			int width = int.Parse(args[1]);
			int height = int.Parse(args[2]);

			using (DirectBitmap bitmap = new DirectBitmap(width, height))
			{
				ConsoleKeyInfo key;
				byte[] data = File.ReadAllBytes(path);
				Stopwatch stopwatch = new Stopwatch();
				do
				{
					stopwatch.Start();
					Yuy2Decoder.DecompressYUY2(data, width, height, bitmap.Bits);
					stopwatch.Stop();

					Console.WriteLine($"Processed in {stopwatch.ElapsedMilliseconds} ms");
					stopwatch.Reset();
					key = Console.ReadKey();
				}
				while (key.Key == ConsoleKey.Spacebar);

				string dirPath = Path.GetDirectoryName(path);
				string fileName = Path.GetFileNameWithoutExtension(path);
				string outPath = Path.Combine(dirPath, fileName + ".png");
				bitmap.Bitmap.Save(outPath, ImageFormat.Png);
			}

			Console.WriteLine("Finished!");
		}
	}
}
