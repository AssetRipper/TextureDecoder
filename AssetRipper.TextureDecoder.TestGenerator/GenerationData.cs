using AssetRipper.TextureDecoder.Rgb;

﻿namespace AssetRipper.TextureDecoder.TestGenerator
{
	internal struct GenerationData
	{
		public Type ColorType;
		public Type ChannelType;
		public int ColorSize;

		public GenerationData(Type colorType, int colorSize)
		{
			ColorType = colorType;
			ChannelType = ColorType.GetInterfaces().Single(t => t.Name == $"{nameof(IColor<byte>)}`1").GenericTypeArguments[0];
			ColorSize = colorSize;
		}
	}
}