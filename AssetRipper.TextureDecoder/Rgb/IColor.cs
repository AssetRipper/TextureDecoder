namespace AssetRipper.TextureDecoder.Rgb
{
	/// <summary>
	/// An <see langword="interface"/> for handling color formats with up to 4 channels.
	/// </summary>
	/// <remarks>
	/// When used as a generic type constraint, the methods and properties get devirtualized by the JIT compiler.
	/// This prevents boxing when the implementing type is a <see langword="struct"/>.
	/// </remarks>
	/// <typeparam name="T">
	/// Supported types are:
	/// <list type="bullet">
	/// <item><see cref="sbyte"/></item>
	/// <item><see cref="byte"/></item>
	/// <item><see cref="short"/></item>
	/// <item><see cref="ushort"/></item>
	/// <item><see cref="int"/></item>
	/// <item><see cref="uint"/></item>
	/// <item><see cref="long"/></item>
	/// <item><see cref="ulong"/></item>
	/// <item><see cref="Int128"/></item>
	/// <item><see cref="UInt128"/></item>
	/// <item><see cref="Half"/></item>
	/// <item><see cref="float"/></item>
	/// <item><see cref="double"/></item>
	/// <item><see cref="decimal"/></item>
	/// </list>
	/// </typeparam>
	public interface IColor<T> : IColorBase where T : unmanaged
	{
		/// <summary>
		/// The red channel
		/// </summary>
		T R { get; set; }
		/// <summary>
		/// The green channel
		/// </summary>
		T G { get; set; }
		/// <summary>
		/// The blue channel
		/// </summary>
		T B { get; set; }
		/// <summary>
		/// The alpha channel
		/// </summary>
		T A { get; set; }

		void GetChannels(out T r, out T g, out T b, out T a);
		void SetChannels(T r, T g, T b, T a);
	}
}
