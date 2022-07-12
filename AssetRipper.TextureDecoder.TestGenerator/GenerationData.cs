namespace AssetRipper.TextureDecoder.TestGenerator
{
	internal struct GenerationData
	{
		public Type ColorType;
		public Type ChannelType;
		public int ColorSize;

		public GenerationData(Type colorType, Type channelType, int colorSize)
		{
			ColorType = colorType;
			ChannelType = channelType;
			ColorSize = colorSize;
		}
	}
}