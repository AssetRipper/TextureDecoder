using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using AssetRipper.TextureDecoder.Pvrtc;

namespace PvrtcTest
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length	< 4)
			{
				Console.WriteLine("Format: {path} {width} {height} {2bitMode}");
				Console.ReadKey();
				return;
			}

			string path = args[0];
			int width = int.Parse(args[1]);
			int height = int.Parse(args[2]);
			bool do2bit = bool.Parse(args[3]);

			using (DirectBitmap bitmap = new DirectBitmap(width, height))
			{
				byte[] data = File.ReadAllBytes(path);

				ConsoleKeyInfo key;
				Stopwatch stopwatch = new Stopwatch();
				do
				{
					stopwatch.Start();
					PvrtcDecoder.DecompressPVRTC(data, width, height, do2bit, bitmap.Bits);
					stopwatch.Stop();

					Console.WriteLine("Processed " + stopwatch.ElapsedMilliseconds);
					stopwatch.Reset();
					key = Console.ReadKey();
				}
				while (key.Key == ConsoleKey.Spacebar);

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
