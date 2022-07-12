namespace AssetRipper.TextureDecoder.TestGenerator
{
	internal struct ColorRandomValues
	{
		public string DefaultR;
		public string DefaultG;
		public string DefaultB;
		public string DefaultA;
		public string SetValue;

		public ColorRandomValues(string defaultR, string defaultG, string defaultB, string defaultA, string setValue)
		{
			DefaultR = defaultR;
			DefaultG = defaultG;
			DefaultB = defaultB;
			DefaultA = defaultA;
			SetValue = setValue;
		}
	}
}
