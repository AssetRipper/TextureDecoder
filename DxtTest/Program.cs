using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using AssetRipper.TextureDecoder.Dxt;

namespace DxtTest
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length < 4)
			{
				Console.WriteLine("Format: {fileName} {width} {height} {mode}");
				Console.WriteLine("mode: 0 - DXT1; 1 - DXT3; 2 - DXT5");
				Console.ReadKey();
				return;
			}

			string path = args[0];
			int width = int.Parse(args[1]);
			int height = int.Parse(args[2]);
			int mode = int.Parse(args[3]);

			using (DirectBitmap bitmap = new DirectBitmap(width, height))
			{
				byte[] data = File.ReadAllBytes(path);
				Stopwatch stopwatch = new Stopwatch();
				for (int i = 0; i < 5; i++)
				{
					stopwatch.Start();
					for (int j = 0; j < 5; j++)
					{
						switch (mode)
						{
							case 0:
								DxtDecoder.DecompressDXT1(data, width, height, bitmap.Bits);
								break;
							case 1:
								DxtDecoder.DecompressDXT3(data, width, height, bitmap.Bits);
								break;
							case 2:
								DxtDecoder.DecompressDXT5(data, width, height, bitmap.Bits);
								break;

							default:
								throw new Exception(mode.ToString());
						}
					}
					stopwatch.Stop();

					Console.WriteLine("Processed " + stopwatch.ElapsedMilliseconds);
					stopwatch.Reset();
				}

				string dirPath = Path.GetDirectoryName(path);
				string fileName = Path.GetFileNameWithoutExtension(path);
				string outPath = Path.Combine(dirPath, fileName + ".png");
				bitmap.Bitmap.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY);
				bitmap.Bitmap.Save(outPath, ImageFormat.Png);
			}

			Console.WriteLine("Finished!");
		}
	}
}
