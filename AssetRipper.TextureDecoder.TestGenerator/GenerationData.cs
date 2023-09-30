using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.SourceGeneration.Common;

namespace AssetRipper.TextureDecoder.TestGenerator
{
	internal readonly struct GenerationData
	{
		public Type ColorType { get; private init; }
		public Type ChannelType { get; private init; }
		public int ColorSize { get; private init; }

		public bool RedChannel { get; private init; }
		public bool GreenChannel { get; private init; }
		public bool BlueChannel { get; private init; }
		public bool AlphaChannel { get; private init; }
		public bool FullyUtilizedChannels { get; private init; }

		public bool IsFloatingPoint => CSharpPrimitives.IsFloatingPoint(ChannelType);

		public int BitSizePerChannel => CSharpPrimitives.Dictionary[ChannelType].Size * 8;

		public string ChannelTypeName => CSharpPrimitives.Dictionary[ChannelType].LangName;

		public static GenerationData Create<T>(int colorSize) where T : IColorBase
		{
			return new GenerationData()
			{
				ColorType = typeof(T),
				ChannelType = T.ChannelType,
				ColorSize = colorSize,
				RedChannel = T.HasRedChannel,
				GreenChannel = T.HasGreenChannel,
				BlueChannel = T.HasBlueChannel,
				AlphaChannel = T.HasAlphaChannel,
				FullyUtilizedChannels = T.ChannelsAreFullyUtilized,
			};
		}

		public bool Contains(GenerationData other)
		{
			if (!FullyUtilizedChannels)
			{
				return false;
			}

			if (IsFloatingPoint != other.IsFloatingPoint)
			{
				return false;
			}

			if (other.RedChannel && !RedChannel)
			{
				return false;
			}

			if (other.GreenChannel && !GreenChannel)
			{
				return false;
			}

			if (other.BlueChannel && !BlueChannel)
			{
				return false;
			}

			if (other.AlphaChannel && !AlphaChannel)
			{
				return false;
			}

			return BitSizePerChannel >= other.BitSizePerChannel;
		}
	}
}
