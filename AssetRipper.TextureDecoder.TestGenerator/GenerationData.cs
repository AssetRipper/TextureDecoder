using AssetRipper.TextureDecoder.Attributes;
using AssetRipper.TextureDecoder.Rgb;
using AssetRipper.TextureDecoder.SourceGeneration.Common;
using System.Reflection;

namespace AssetRipper.TextureDecoder.TestGenerator
{
	internal readonly struct GenerationData
	{
		public Type ColorType { get; }
		public Type ChannelType { get; }
		public int ColorSize { get; }

		public bool RedChannel { get; }
		public bool GreenChannel { get; }
		public bool BlueChannel { get; }
		public bool AlphaChannel { get; }
		public bool FullyUtilizedChannels { get; }

		public bool IsFloatingPoint => CSharpPrimitives.IsFloatingPoint(ChannelType);

		public int BitSizePerChannel => CSharpPrimitives.Dictionary[ChannelType].Size * 8;

		public string ChannelTypeName => CSharpPrimitives.Dictionary[ChannelType].LangName;

		public GenerationData(Type colorType, int colorSize)
		{
			ColorType = colorType;
			ChannelType = ColorType.GetInterfaces().Single(t => t.Name == $"{nameof(IColor<byte>)}`1").GenericTypeArguments[0];
			ColorSize = colorSize;

			RgbaAttribute attribute = colorType.GetCustomAttribute<RgbaAttribute>() ?? throw new NullReferenceException(colorType.Name);
			RedChannel = attribute.RedChannel;
			GreenChannel = attribute.GreenChannel;
			BlueChannel = attribute.BlueChannel;
			AlphaChannel = attribute.AlphaChannel;
			FullyUtilizedChannels = attribute.FullyUtilizedChannels;
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