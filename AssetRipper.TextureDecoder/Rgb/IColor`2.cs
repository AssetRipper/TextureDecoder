namespace AssetRipper.TextureDecoder.Rgb;

/// <summary>
/// An <see langword="interface"/> for handling color formats with up to 4 channels.
/// </summary>
/// <remarks>
/// When used as a generic type constraint, the methods and properties get devirtualized by the JIT compiler.
/// This prevents boxing when the implementing type is a <see langword="struct"/>.
/// </remarks>
/// <typeparam name="TSelf">
/// The implementing type.
/// </typeparam>
/// <typeparam name="TChannel">
/// Supported types are:
/// <list type="bullet">
/// <item><see cref="sbyte"/></item>
/// <item><see cref="byte"/></item>
/// <item><see cref="short"/></item>
/// <item><see cref="ushort"/></item>
/// <item><see cref="int"/></item>
/// <item><see cref="uint"/></item>
/// <item><see cref="nint"/></item>
/// <item><see cref="nuint"/></item>
/// <item><see cref="long"/></item>
/// <item><see cref="ulong"/></item>
/// <item><see cref="Int128"/></item>
/// <item><see cref="UInt128"/></item>
/// <item><see cref="Half"/></item>
/// <item><see cref="float"/></item>
/// <item><see cref="NFloat"/></item>
/// <item><see cref="double"/></item>
/// <item><see cref="decimal"/></item>
/// </list>
/// </typeparam>
public interface IColor<TSelf, TChannel> : IColor<TChannel>
	where TSelf : unmanaged, IColor<TSelf, TChannel>
	where TChannel : unmanaged
{
	/// <summary>
	/// A black pixel.
	/// </summary>
	static abstract TSelf Black { get; }
	/// <summary>
	/// A white pixel.
	/// </summary>
	static abstract TSelf White { get; }
}
