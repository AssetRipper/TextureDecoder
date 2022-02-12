using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using AssetRipper.TextureDecoder.Atc;

namespace AtcTest
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length < 4)
			{
				Console.WriteLine("Format: {fileName} {width} {height} {format}");
				Console.WriteLine("format: 0 - ATC RGB4; 1 - ATC RGBA8");
				Console.ReadKey();
				return;
			}

			string path = args[0];
			int width = int.Parse(args[1]);
			int height = int.Parse(args[2]);
			int mode = int.Parse(args[3]);

			using (DirectBitmap bitmap = new DirectBitmap(width, height))
			{
				ConsoleKeyInfo key;
				byte[] data = File.ReadAllBytes(path);
				Stopwatch stopwatch = new Stopwatch();
				do
				{
					stopwatch.Start();
					switch (mode)
					{
						case 0:
							AtcDecoder.DecompressAtcRgb4(data, width, height, bitmap.Bits);
							break;
						case 1:
							AtcDecoder.DecompressAtcRgba8(data, width, height, bitmap.Bits);
							break;

						default:
							throw new Exception(mode.ToString());
					}
					stopwatch.Stop();

					Console.WriteLine("Processed " + stopwatch.ElapsedMilliseconds);
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
