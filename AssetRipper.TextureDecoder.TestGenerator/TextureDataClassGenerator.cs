using AssetRipper.Text.SourceGeneration;
using System.CodeDom.Compiler;

namespace AssetRipper.TextureDecoder.TestGenerator;

internal static class TextureDataClassGenerator
{
	private const string TestFilesFolder = $"{nameof(TestFileFolders)}.{nameof(TestFileFolders.AndroidTestFiles)}";
	private const string TextureType = "TextureType";
	private const string TextureFormat = "TextureFormat";
	private const string ITexture = "ITexture";
	private const string Namespace = "AssetRipper.TextureDecoder.Tests";

	public static void Generate()
	{
		IReadOnlyList<TextureMetadata> metadataList = LoadMetadata(TestFileFolders.AndroidTestFiles);
		GenerateTextureType(metadataList);
		GenerateTextureFormat(metadataList);
		GenerateITexture();
		GenerateAndroidTextures(metadataList);
	}

	private static void GenerateTextureType(IReadOnlyList<TextureMetadata> metadataList)
	{
		GenerateEnum(TextureType, metadataList.Select(m => m.Type).Distinct().Order());
	}

	private static void GenerateTextureFormat(IReadOnlyList<TextureMetadata> metadataList)
	{
		GenerateEnum(TextureFormat, metadataList.Select(m => m.Format).Distinct().Order());
	}

	private static void GenerateEnum(string name, IEnumerable<string?> values)
	{
		using IndentedTextWriter writer = IndentedTextWriterFactory.Create(ProjectFolders.TestProject, name);
		writer.WriteGeneratedCodeWarning();
		writer.WriteLine();
		writer.WriteFileScopedNamespace(Namespace);
		writer.WriteLine();
		writer.WriteLine($"public enum {name}");
		using (new CurlyBrackets(writer))
		{
			foreach (string? value in values)
			{
				writer.WriteLine($"{value},");
			}
		}
	}

	private static void GenerateITexture()
	{
		using IndentedTextWriter writer = IndentedTextWriterFactory.Create(ProjectFolders.TestProject, ITexture);
		writer.WriteGeneratedCodeWarning();
		writer.WriteLine();
		writer.WriteFileScopedNamespace(Namespace);
		writer.WriteLine();
		writer.WriteLine($"public partial interface {ITexture}");
		using (new CurlyBrackets(writer))
		{
			writer.WriteLine($"static abstract {TextureType} {nameof(TextureMetadata.Type)} {{ get; }}");
			writer.WriteLine($"static abstract {TextureFormat} {nameof(TextureMetadata.Format)} {{ get; }}");
			writer.WriteLine($"static abstract int {nameof(TextureMetadata.FileSize)} {{ get; }}");
			writer.WriteLine($"static abstract int {nameof(TextureMetadata.ImageSize)} {{ get; }}");
			writer.WriteLine($"static abstract int {nameof(TextureMetadata.ImageCount)} {{ get; }}");
			writer.WriteLine($"static abstract bool {nameof(TextureMetadata.Mips)} {{ get; }}");
			writer.WriteLine($"static abstract int {nameof(TextureMetadata.Width)} {{ get; }}");
			writer.WriteLine($"static abstract int {nameof(TextureMetadata.Height)} {{ get; }}");
			writer.WriteLine("static abstract string Path { get; }");
			writer.WriteLine("static abstract byte[] Data { get; }");
		}
	}

	private static IReadOnlyList<TextureMetadata> LoadMetadata(string directory)
	{
		string[] jsonFiles = Directory.GetFiles(directory, "*.json");
		List<TextureMetadata> metadataList = new(jsonFiles.Length);
		foreach (string jsonFile in jsonFiles)
		{
			metadataList.Add(TextureMetadata.FromFile(jsonFile));
		}
		return metadataList;
	}

	private static void GenerateAndroidTextures(IReadOnlyList<TextureMetadata> metadataList)
	{
		const string AndroidTextures = "AndroidTextures";
		using IndentedTextWriter writer = IndentedTextWriterFactory.Create(ProjectFolders.TestProject, AndroidTextures);
		writer.WriteGeneratedCodeWarning();
		writer.WriteLine();
		writer.WriteFileScopedNamespace(Namespace);
		writer.WriteLine();
		writer.WriteLine($"public static partial class {AndroidTextures}");
		using (new CurlyBrackets(writer))
		{
			foreach (TextureMetadata metadata in metadataList)
			{
				string withOrWithout = metadata.Mips ? "with" : "without";
				writer.WriteSummaryDocumentation($"{metadata.Format} {metadata.Width}x{metadata.Height} {withOrWithout} mips");
				writer.WriteLine($"public sealed class {metadata.Name} : {ITexture}");
				using (new CurlyBrackets(writer))
				{
					writer.WriteLine($"public const {TextureType} {nameof(TextureMetadata.Type)} = {TextureType}.{metadata.Type};");
					writer.WriteLine($"public const {TextureFormat} {nameof(TextureMetadata.Format)} = {TextureFormat}.{metadata.Format};");
					writer.WriteLine($"public const int {nameof(TextureMetadata.FileSize)} = {metadata.FileSize};");
					writer.WriteLine($"public const int {nameof(TextureMetadata.ImageSize)} = {metadata.ImageSize};");
					writer.WriteLine($"public const int {nameof(TextureMetadata.ImageCount)} = {metadata.ImageCount};");
					writer.WriteLine($"public const bool {nameof(TextureMetadata.Mips)} = {metadata.Mips.ToString().ToLowerInvariant()};");
					writer.WriteLine($"public const int {nameof(TextureMetadata.Width)} = {metadata.Width};");
					writer.WriteLine($"public const int {nameof(TextureMetadata.Height)} = {metadata.Height};");
					writer.WriteLine($"public const string Path = {TestFilesFolder} + \"{metadata.Name}\";");
					writer.WriteSummaryDocumentation($"Length: {metadata.FileSize}");
					writer.WriteLine("public static byte[] Data => File.ReadAllBytes(Path);");
					writer.WriteLine();
					writer.WriteLine($"private {metadata.Name}() {{ }}");
					writer.WriteLine($"static {TextureType} {ITexture}.{nameof(TextureMetadata.Type)} => {nameof(TextureMetadata.Type)};");
					writer.WriteLine($"static {TextureFormat} {ITexture}.{nameof(TextureMetadata.Format)} => {nameof(TextureMetadata.Format)};");
					writer.WriteLine($"static int {ITexture}.{nameof(TextureMetadata.FileSize)} => {nameof(TextureMetadata.FileSize)};");
					writer.WriteLine($"static int {ITexture}.{nameof(TextureMetadata.ImageSize)} => {nameof(TextureMetadata.ImageSize)};");
					writer.WriteLine($"static int {ITexture}.{nameof(TextureMetadata.ImageCount)} => {nameof(TextureMetadata.ImageCount)};");
					writer.WriteLine($"static bool {ITexture}.{nameof(TextureMetadata.Mips)} => {nameof(TextureMetadata.Mips)};");
					writer.WriteLine($"static int {ITexture}.{nameof(TextureMetadata.Width)} => {nameof(TextureMetadata.Width)};");
					writer.WriteLine($"static int {ITexture}.{nameof(TextureMetadata.Height)} => {nameof(TextureMetadata.Height)};");
					writer.WriteLine($"static string {ITexture}.Path => Path;");
				}
				writer.WriteLine();
			}
		}
	}
}
