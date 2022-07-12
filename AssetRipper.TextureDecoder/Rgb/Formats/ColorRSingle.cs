namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	public struct ColorRSingle : IColor<float>
	{
		public float R { get; set; }
		public float G
		{
			get => default;
			set { }
		}
		public float B
		{
			get => default;
			set { }
		}
		public float A
		{
			get => 1;
			set { }
		}

		public void GetChannels(out float r, out float g, out float b, out float a)
		{
			DefaultColorMethods.GetChannels(this, out r, out g, out b, out a);
		}

		public void SetChannels(float r, float g, float b, float a)
		{
			DefaultColorMethods.SetChannels(ref this, r, g, b, a);
		}
	}
}
