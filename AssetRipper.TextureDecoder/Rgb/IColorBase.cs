namespace AssetRipper.TextureDecoder.Rgb;

/// <summary>
/// A base <see langword="interface"/> for handling color formats with up to 4 channels.
/// </summary>
/// <remarks>
/// When used as a generic type constraint, the methods and properties get devirtualized by the JIT compiler.
/// This prevents boxing when the implementing type is a <see langword="struct"/>.
/// </remarks>
public interface IColorBase
{
	/// <summary>
	/// Does the format have a red channel?
	/// </summary>
	static abstract bool HasRedChannel { get; }
	/// <summary>
	/// Does the format have a green channel?
	/// </summary>
	static abstract bool HasGreenChannel { get; }
	/// <summary>
	/// Does the format have a blue channel?
	/// </summary>
	static abstract bool HasBlueChannel { get; }
	/// <summary>
	/// Does the format have an alpha channel?
	/// </summary>
	static abstract bool HasAlphaChannel { get; }
	/// <summary>
	/// Channels utilize the full range of possible values for their underlying type.
	/// </summary>
	/// <remarks>
	/// In the case of floating point numbers, this means that the precision is the same in storage and memory.
	/// </remarks>
	static abstract bool ChannelsAreFullyUtilized { get; }
	/// <summary>
	/// The type of the channels.
	/// </summary>
	static abstract Type ChannelType { get; }
}
