namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	public struct ColorRGBAHalf : IColor<Half>
	{
		public Half R { get; set; }
		public Half G { get; set; }
		public Half B { get; set; }
		public Half A { get; set; }

		public void GetChannels(out Half r, out Half g, out Half b, out Half a)
		{
			DefaultColorMethods.GetChannels(this, out r, out g, out b, out a);
		}

		public void SetChannels(Half r, Half g, Half b, Half a)
		{
			DefaultColorMethods.SetChannels(ref this, r, g, b, a);
		}
	}
}
