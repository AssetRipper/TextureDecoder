namespace AssetRipper.TextureDecoder.Attributes
{
	[AttributeUsage(AttributeTargets.Struct)]
	public sealed class RgbaAttribute : Attribute
	{
		/// <summary>
		/// Does the format have a red channel?
		/// </summary>
		public bool RedChannel { get; init; }
		/// <summary>
		/// Does the format have a green channel?
		/// </summary>
		public bool GreenChannel { get; init; }
		/// <summary>
		/// Does the format have a blue channel?
		/// </summary>
		public bool BlueChannel { get; init; }
		/// <summary>
		/// Does the format have an alpha channel?
		/// </summary>
		public bool AlphaChannel { get; init; }
		/// <summary>
		/// Channels utilize the full range of possible values for their underlying type.
		/// </summary>
		/// <remarks>
		/// In the case of floating point numbers, this means that the precision is the same in storage and memory.
		/// </remarks>
		public bool FullyUtilizedChannels { get; init; }
	}
}