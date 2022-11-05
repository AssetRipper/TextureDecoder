//Auto-generated code. Do not modify.
namespace AssetRipper.TextureDecoder.Rgb.Formats
{
	public partial struct ColorRGSingle : IColor<float>
	{
		public float R { get; set; }
		
		public float G { get; set; }
		
		public float B 
		{
			get => 0f;
			set { }
		}
		
		public float A 
		{
			get => 1f;
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
