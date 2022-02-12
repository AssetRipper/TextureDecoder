using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using AssetRipper.TextureDecoder.Etc;

namespace EtcTest
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length < 4)
			{
				Console.WriteLine("Format: {fileName} {width} {height} {mode}");
				Console.WriteLine("mode: 0 - ETC; 1 - ETC2; 2 - ETC2a1; 3 - ETC2a8; 4 - EAC R; 5 - EAC signed R; 6 - EAC RG; 7 - EAC signed RG;");
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
							EtcDecoder.DecompressETC(data, width, height, bitmap.Bits);
							break;
						case 1:
							EtcDecoder.DecompressETC2(data, width, height, bitmap.Bits);
							break;
						case 2:
							EtcDecoder.DecompressETC2A1(data, width, height, bitmap.Bits);
							break;
						case 3:
							EtcDecoder.DecompressETC2A8(data, width, height, bitmap.Bits);
							break;
						case 4:
							EtcDecoder.DecompressEACRUnsigned(data, width, height, bitmap.Bits);
							break;
						case 5:
							EtcDecoder.DecompressEACRSigned(data, width, height, bitmap.Bits);
							break;
						case 6:
							EtcDecoder.DecompressEACRGUnsigned(data, width, height, bitmap.Bits);
							break;
						case 7:
							EtcDecoder.DecompressEACRGSigned(data, width, height, bitmap.Bits);
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
