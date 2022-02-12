using System;
using System.Drawing.Imaging;
using System.IO;
using AssetRipper.TextureDecoder.Astc;

namespace AstcTest
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length < 4)
			{
				Console.WriteLine("Format: {path} {width} {height} {blockXSize} {blockYSize}");
			}
			else
			{
				string path = args[0];
				int width = int.Parse(args[1]);
				int height = int.Parse(args[2]);
				int blockXSize = int.Parse(args[3]);
				int blockYSize = int.Parse(args[4]);
				byte[] data = File.ReadAllBytes(path);

				using (DirectBitmap bitmap = new DirectBitmap(width, height))
				{
					AstcDecoder.DecodeASTC(data, width, height, blockXSize, blockYSize, bitmap.Bits);

					string dirPath = Path.GetDirectoryName(path);
					string name = Path.GetFileNameWithoutExtension(path);
					string newPath = Path.Combine(dirPath, name + "_decoded.png");
					bitmap.Bitmap.Save(newPath, ImageFormat.Png);
				}

				Console.WriteLine("Finished");
			}

			Console.ReadKey();
		}
	}
}
