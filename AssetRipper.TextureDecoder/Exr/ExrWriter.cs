using AssetRipper.TextureDecoder.Rgb;

namespace AssetRipper.TextureDecoder.Exr;

public static class ExrWriter
{
	public static ReadOnlySpan<byte> Magic => [0x76, 0x2f, 0x31, 0x01];

	public static void Write<TColor, TChannelValue>(Stream stream, int width, int height, ReadOnlySpan<TColor> pixels)
		where TChannelValue : unmanaged
		where TColor : unmanaged, IColor<TChannelValue>
	{
		int ChannelCount = Color.GetChannelCount<TColor>();
		ExrPixelType PixelType = Unsafe.SizeOf<TChannelValue>() <= sizeof(ushort) ? ExrPixelType.Half : ExrPixelType.Single;
		int ChannelSize = PixelType is ExrPixelType.Half ? Unsafe.SizeOf<Half>() : sizeof(float);

		using BinaryWriter writer = new BinaryWriter(stream);

		//Magic bytes
		writer.Write(Magic);

		//Version bytes
		writer.Write((byte)2);//OpenEXR version 2
		writer.Write(ExrImageFormat.ScanLines.ToVersionByte());
		writer.WriteNullByte();
		writer.WriteNullByte();

		ExrChannel[] channels;
		{
			Span<ExrChannel> buffer = [default, default, default, default];
			//Stackalloc doesn't work for managed types.
			//This can be reworked when ref structs can be used in generics.

			int i = 0;

			//The order doesn't really matter here because the format sorts channels alphabetically
			//when reading reading scan lines, but this makes that more clear.
			if (TColor.HasAlphaChannel)
			{
				buffer[i] = new ExrChannel("A", PixelType, false, 1, 1);
				i++;
			}
			if (TColor.HasBlueChannel)
			{
				buffer[i] = new ExrChannel("B", PixelType, false, 1, 1);
				i++;
			}
			if (TColor.HasGreenChannel)
			{
				buffer[i] = new ExrChannel("G", PixelType, false, 1, 1);
				i++;
			}
			if (TColor.HasRedChannel)
			{
				buffer[i] = new ExrChannel("R", PixelType, false, 1, 1);
				i++;
			}

			channels = buffer[..i].ToArray();
		}

		//Attributes
		writer.WriteAttribute(ExrAttributes.Channels, new ExrChannelList(channels));
		writer.WriteAttribute(ExrAttributes.Compression, "compression"u8, ExrCompression.None);
		writer.WriteAttribute(ExrAttributes.DataWindow, new ExrBox2I(0, 0, width - 1, height - 1));
		writer.WriteAttribute(ExrAttributes.DisplayWindow, new ExrBox2I(0, 0, width - 1, height - 1));
		writer.WriteAttribute(ExrAttributes.LineOrder, "lineOrder"u8, ExrLineOrder.IncreasingY);
		writer.WriteAttribute(ExrAttributes.PixelAspectRatio, new ExrSingle(1f));
		writer.WriteAttribute(ExrAttributes.ScreenWindowCenter, new ExrVector2(0f, 0f));
		writer.WriteAttribute(ExrAttributes.ScreenWindowWidth, new ExrSingle(1f));
		writer.WriteNullByte();//End of header

		//Scan line offset table
		long offset = writer.BaseStream.Position + height * sizeof(ulong);
		for (int i = 0; i < height; i++)
		{
			writer.Write(offset);
			offset += sizeof(int) + ChannelCount * ChannelSize * width;
		}

		//Scan lines
		if (PixelType is ExrPixelType.Half)
		{
			for (int i = 0; i < height; i++)
			{
				writer.Write(i);
				writer.Write(ChannelCount * ChannelSize * width);
				if (TColor.HasAlphaChannel)
				{
					for (int j = 0; j < width; j++)
					{
						Half alpha = NumericConversion.Convert<TChannelValue, Half>(pixels[i * width + j].A);
						writer.Write(alpha);
					}
				}
				if (TColor.HasBlueChannel)
				{
					for (int j = 0; j < width; j++)
					{
						float blue = NumericConversion.Convert<TChannelValue, float>(pixels[i * width + j].B);
						if (TColor.HasAlphaChannel)
						{
							float alpha = NumericConversion.Convert<TChannelValue, float>(pixels[i * width + j].A);
							writer.Write((Half)ConvertSRGB(blue * alpha));
						}
						else
						{
							writer.Write((Half)ConvertSRGB(blue));
						}
					}
				}
				if (TColor.HasGreenChannel)
				{
					for (int j = 0; j < width; j++)
					{
						float green = NumericConversion.Convert<TChannelValue, float>(pixels[i * width + j].G);
						if (TColor.HasAlphaChannel)
						{
							float alpha = NumericConversion.Convert<TChannelValue, float>(pixels[i * width + j].A);
							writer.Write((Half)ConvertSRGB(green * alpha));
						}
						else
						{
							writer.Write((Half)ConvertSRGB(green));
						}
					}
				}
				if (TColor.HasRedChannel)
				{
					for (int j = 0; j < width; j++)
					{
						float red = NumericConversion.Convert<TChannelValue, float>(pixels[i * width + j].R);
						if (TColor.HasAlphaChannel)
						{
							float alpha = NumericConversion.Convert<TChannelValue, float>(pixels[i * width + j].A);
							writer.Write((Half)ConvertSRGB(red * alpha));
						}
						else
						{
							writer.Write((Half)ConvertSRGB(red));
						}
					}
				}
			}
		}
		else
		{
			for (int i = 0; i < height; i++)
			{
				writer.Write(i);
				writer.Write(ChannelCount * ChannelSize * width);
				if (TColor.HasAlphaChannel)
				{
					for (int j = 0; j < width; j++)
					{
						float alpha = NumericConversion.Convert<TChannelValue, float>(pixels[i * width + j].A);
						writer.Write(alpha);
					}
				}
				if (TColor.HasBlueChannel)
				{
					for (int j = 0; j < width; j++)
					{
						float blue = NumericConversion.Convert<TChannelValue, float>(pixels[i * width + j].B);
						if (TColor.HasAlphaChannel)
						{
							float alpha = NumericConversion.Convert<TChannelValue, float>(pixels[i * width + j].A);
							writer.Write(ConvertSRGB(blue * alpha));
						}
						else
						{
							writer.Write(ConvertSRGB(blue));
						}
					}
				}
				if (TColor.HasGreenChannel)
				{
					for (int j = 0; j < width; j++)
					{
						float green = NumericConversion.Convert<TChannelValue, float>(pixels[i * width + j].G);
						if (TColor.HasAlphaChannel)
						{
							float alpha = NumericConversion.Convert<TChannelValue, float>(pixels[i * width + j].A);
							writer.Write(ConvertSRGB(green * alpha));
						}
						else
						{
							writer.Write(ConvertSRGB(green));
						}
					}
				}
				if (TColor.HasRedChannel)
				{
					for (int j = 0; j < width; j++)
					{
						float red = NumericConversion.Convert<TChannelValue, float>(pixels[i * width + j].R);
						if (TColor.HasAlphaChannel)
						{
							float alpha = NumericConversion.Convert<TChannelValue, float>(pixels[i * width + j].A);
							writer.Write(ConvertSRGB(red * alpha));
						}
						else
						{
							writer.Write(ConvertSRGB(red));
						}
					}
				}
			}
		}
	}

	private static float ConvertSRGB(float value)
	{
		//https://en.wikipedia.org/wiki/SRGB#From_sRGB_to_CIE_XYZ
		//https://en.wikipedia.org/wiki/Gamma_correction#Computer_displays
		if (value < 0.04045)
		{
			return value / 12.92f;
		}
		else
		{
			return float.Pow((value + 0.055f) / 1.055f, 2.4f);
		}
	}
}
