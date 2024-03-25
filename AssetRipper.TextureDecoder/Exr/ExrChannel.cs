using System.Text;

namespace AssetRipper.TextureDecoder.Exr;

public readonly record struct ExrChannel(string Name, ExrPixelType PixelType, bool PLinear, int XSampling, int YSampling)
{
	public int Size
	{
		get
		{
			return Encoding.UTF8.GetByteCount(Name)
				+ sizeof(byte)//Null terminator
				+ sizeof(ExrPixelType)
				+ sizeof(bool)
				+ 3 * sizeof(byte)//Reserved bytes
				+ sizeof(int)
				+ sizeof(int);
		}
	}

	public void Write(BinaryWriter writer)
	{
		writer.WriteNullTerminatedString(Name);
		writer.Write(PixelType);
		writer.Write(PLinear);
		writer.WriteNullByte();
		writer.WriteNullByte();
		writer.WriteNullByte();
		writer.Write(XSampling);
		writer.Write(YSampling);
	}
}
