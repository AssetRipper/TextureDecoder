namespace AssetRipper.TextureDecoder.Rgb.Formats
{
    /// <summary>
    /// Also called R11G11B10_FLOAT
    /// </summary>
    public struct ColorRGB32Half : IColor<Half>
    {
        private uint bits;

        [MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
        private static Half ToHalf(ushort value)
        {
        #if NET5_0_OR_GREATER
            return Unsafe.As<ushort, Half>(ref value);
        #else
            return ToHalf(value);
        #endif
        }

        [MethodImpl(OptimizationConstants.AggressiveInliningAndOptimization)]
        private static uint FromHalf(Half value)
        {
        #if NET5_0_OR_GREATER
            return Unsafe.As<Half, ushort>(ref value);
        #else
            return FromHalf(value);
        #endif
        }

        /// <summary>
        /// 5 bits
        /// </summary>
        public Half R
        {
            get => ToHalf((ushort) ((bits << 4) & 0x7FF0));
            set => bits = (uint) ((bits & ~0x7FF) | (FromHalf(value) & 0x7FF0) >> 4);
        }

        /// <summary>
        /// 6 bits
        /// </summary>
        public Half G
        {
            get => ToHalf((ushort) ((bits >> 7) & 0x7FF0));
            set => bits = (uint) ((bits & ~0x3FF800) | ((FromHalf(value) >> 4) & 0x7FF0) << 11);
        }

        /// <summary>
        /// 5 bits
        /// </summary>
        public Half B
        {
            get => ToHalf((ushort) ((bits >> 17) & 0x7FE0));
            set => bits = (bits & ~0xFFC00000) | (((FromHalf(value) >> 5) & 0x3FF) << 22);
        }

        public Half A
        {
            get => (Half) 1.0f;
            set { }
        }

        public void GetChannels(out Half r, out Half g, out Half b, out Half a)
        {
            DefaultColorMethods.GetChannels(this, out r, out g, out b, out a);
        }

        public void SetChannels(Half r, Half g, Half b, Half a)
        {
            bits = ((FromHalf(r) >> 4) & 0x7FF) |
                   (((FromHalf(g) >> 4) & 0x7FF) << 11) |
                   (((FromHalf(b) >> 5) & 0x3FF) << 22);
        }
    }
}
