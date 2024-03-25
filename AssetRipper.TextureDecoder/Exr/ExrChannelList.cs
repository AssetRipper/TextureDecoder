namespace AssetRipper.TextureDecoder.Exr;

public readonly record struct ExrChannelList(ExrChannel[] Channels) : IExrDataType
{
	public static ReadOnlySpan<byte> TypeName => "chlist"u8;

	public int Size => Channels.Sum(c => c.Size);

	public void Write(BinaryWriter writer)
	{
		foreach (ExrChannel channel in Channels)
		{
			channel.Write(writer);
		}
		writer.WriteNullByte();
	}
}
