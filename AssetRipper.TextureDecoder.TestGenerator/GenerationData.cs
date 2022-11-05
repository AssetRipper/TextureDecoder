using AssetRipper.TextureDecoder.Attributes;
using AssetRipper.TextureDecoder.Rgb;
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

		public bool IsFloatingPoint
		{
			get
			{
				return ChannelType == typeof(Half) || ChannelType == typeof(float) || ChannelType == typeof(double);
			}
		}

		public int BitSizePerChannel
		{
			get
			{
				if (ChannelType == typeof(byte) || ChannelType == typeof(sbyte))
				{
					return 8;
				}
				else if (ChannelType == typeof(ushort) || ChannelType == typeof(short) || ChannelType == typeof(Half))
				{
					return 16;
				}
				else if (ChannelType == typeof(float))
				{
					return 32;
				}
				else if (ChannelType == typeof(double))
				{
					return 64;
				}
				else
				{
					throw new NotSupportedException(ChannelType.Name);
				}
			}
		}

		public string ChannelTypeName
		{
			get
			{
				return ChannelType.Name switch
				{
					"Byte" => "byte",
					"SByte" => "sbyte",
					"UInt16" => "ushort",
					"Int16" => "short",
					"Single" => "float",
					"Double" => "double",
					_ => ChannelType.Name,
				};
			}
		}

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

			if (!IsFloatingPoint && other.IsFloatingPoint)
			{
				return false;
			}
			
			if (IsFloatingPoint && !other.IsFloatingPoint)
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