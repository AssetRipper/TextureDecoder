namespace AssetRipper.TextureDecoder.Rgb.Formats
{
    /// <summary>
    /// Also called R11G11B10_FLOAT
    /// </summary>
    /// <remarks>
    /// <see href="https://github.com/microsoft/DirectX-Graphics-Samples/blob/e5ea2ac7430ce39e6f6d619fd85ae32581931589/MiniEngine/Core/Shaders/PixelPacking_R11G11B10.hlsli#L31-L37" />
    /// </remarks>
    public partial struct ColorRGB32Half : IColor<Half>
    {
        private uint bits;

        /// <summary>
        /// 11 bits
        /// </summary>
        public Half R {
			readonly get {
                var value = (ushort) ((bits << 4) & 0x7FF0);
                return Unsafe.As<ushort, Half>(ref value);
            }
            set => bits = (uint) ((bits & ~0x7FF) | ((uint) Unsafe.As<Half, ushort>(ref value) & 0x7FF0) >> 4);
        }

        /// <summary>
        /// 11 bits
        /// </summary>
        public Half G {
			readonly get {
                var value = (ushort) ((bits >> 7) & 0x7FF0);
                return Unsafe.As<ushort, Half>(ref value);
            }
            set => bits = (uint) ((bits & ~0x3FF800) | (((uint) Unsafe.As<Half, ushort>(ref value) >> 4) & 0x7FF0) << 11);
        }

        /// <summary>
        /// 10 bits
        /// </summary>
        public Half B {
			readonly get {
                var value = (ushort) ((bits >> 17) & 0x7FE0);
                return Unsafe.As<ushort, Half>(ref value);
            }
            set => bits = (bits & ~0xFFC00000) | ((((uint) Unsafe.As<Half, ushort>(ref value) >> 5) & 0x3FF) << 22);
        }

        public readonly Half A
        {
            get => (Half) 1.0f;
			set { }
        }

        public readonly void GetChannels(out Half r, out Half g, out Half b, out Half a)
        {
            DefaultColorMethods.GetChannels(this, out r, out g, out b, out a);
        }

        public void SetChannels(Half r, Half g, Half b, Half a)
        {
            bits = (((uint) Unsafe.As<Half, ushort>(ref r) >> 4) & 0x7FF) |
                   ((((uint) Unsafe.As<Half, ushort>(ref g) >> 4) & 0x7FF) << 11) |
                   ((((uint) Unsafe.As<Half, ushort>(ref b) >> 5) & 0x3FF) << 22);
        }
    }
}
