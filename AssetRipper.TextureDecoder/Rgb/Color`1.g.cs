// Auto-generated code. Do not modify manually.

using AssetRipper.TextureDecoder.Rgb.Channels;

namespace AssetRipper.TextureDecoder.Rgb;

public partial struct Color<TChannelValue, TChannel> : IColor<Color<TChannelValue, TChannel>, TChannelValue>
	where TChannelValue : unmanaged, INumberBase<TChannelValue>, IMinMaxValue<TChannelValue>
	where TChannel : IChannel
{
	private TChannelValue value;

	public TChannelValue R
	{
		readonly get
		{
			if (TChannel.IsRed)
			{
				return TChannel.GetRed(value);
			}
			else
			{
				return NumericConversion.GetMinimumValueSafe<TChannelValue>();
			}
		}
		set
		{
			Channel.SetIfRed<TChannel, TChannelValue>(ref value, value);
		}
	}

	public TChannelValue G
	{
		readonly get
		{
			if (TChannel.IsGreen)
			{
				return TChannel.GetGreen(value);
			}
			else
			{
				return NumericConversion.GetMinimumValueSafe<TChannelValue>();
			}
		}
		set
		{
			Channel.SetIfGreen<TChannel, TChannelValue>(ref value, value);
		}
	}

	public TChannelValue B
	{
		readonly get
		{
			if (TChannel.IsBlue)
			{
				return TChannel.GetBlue(value);
			}
			else
			{
				return NumericConversion.GetMinimumValueSafe<TChannelValue>();
			}
		}
		set
		{
			Channel.SetIfBlue<TChannel, TChannelValue>(ref value, value);
		}
	}

	public TChannelValue A
	{
		readonly get
		{
			if (TChannel.IsAlpha)
			{
				return TChannel.GetAlpha(value);
			}
			else
			{
				return NumericConversion.GetMaximumValueSafe<TChannelValue>();
			}
		}
		set
		{
			Channel.SetIfAlpha<TChannel, TChannelValue>(ref value, value);
		}
	}

	public Color(TChannelValue value)
	{
		this.value = value;
	}

	public readonly void GetChannels(out TChannelValue r, out TChannelValue g, out TChannelValue b, out TChannelValue a)
	{
		r = R;
		g = G;
		b = B;
		a = A;
	}

	public void SetChannels(TChannelValue r, TChannelValue g, TChannelValue b, TChannelValue a)
	{
		R = r;
		G = g;
		B = b;
		A = a;
	}

	static bool IColor.HasRedChannel => TChannel.IsRed;
	static bool IColor.HasGreenChannel => TChannel.IsGreen;
	static bool IColor.HasBlueChannel => TChannel.IsBlue;
	static bool IColor.HasAlphaChannel => TChannel.IsAlpha;
	static bool IColor.ChannelsAreFullyUtilized => TChannel.FullyUtilized;
	static Type IColor.ChannelType => typeof(TChannelValue);

	public static Color<TChannelValue, TChannel> Black
	{
		get => new(TChannel.GetBlack<TChannelValue>());
	}

	public static Color<TChannelValue, TChannel> White
	{
		get => new(TChannel.GetWhite<TChannelValue>());
	}

	public override string ToString()
	{
		return $"{{ R: {R}, G: {G}, B: {B}, A: {A} }}";
	}
}
