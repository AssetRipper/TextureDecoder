namespace AssetRipper.TextureDecoder.Astc;

public static partial class AstcDecoder
{
	[InlineArray(4)]
	private struct EndpointBuffer
	{
		private Endpoint _element0;

		[InlineArray(8)]
		public struct Endpoint
		{
			private int _element0;
		}
	}
}
