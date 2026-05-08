using System;
using System.IO;
using System.Runtime.CompilerServices;
using ReLogic.Content.Sources;

namespace ReLogic.Content
{
	// Token: 0x0200008A RID: 138
	public class AssetLoader : IAssetLoader
	{
		// Token: 0x06000328 RID: 808 RVA: 0x0000BF7B File Offset: 0x0000A17B
		public AssetLoader(AssetReaderCollection readers)
		{
			this._readers = readers;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000BF8C File Offset: 0x0000A18C
		public bool TryLoad<T>(string assetName, IContentSource source, out T resultAsset) where T : class
		{
			return this.TryLoad<T>(source.GetExtension(assetName), () => source.OpenStream(assetName), out resultAsset);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000BFD4 File Offset: 0x0000A1D4
		public bool TryLoad<T>(string extension, Func<Stream> getStream, out T resultAsset) where T : class
		{
			resultAsset = default(T);
			if (!this._readers.CanReadExtension(extension))
			{
				return false;
			}
			using (Stream stream = getStream.Invoke())
			{
				resultAsset = this._readers.Read<T>(stream, extension);
			}
			return true;
		}

		// Token: 0x040004FB RID: 1275
		private readonly AssetReaderCollection _readers;

		// Token: 0x020000E8 RID: 232
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0<T> where T : class
		{
			// Token: 0x06000478 RID: 1144 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x06000479 RID: 1145 RVA: 0x0000E644 File Offset: 0x0000C844
			internal Stream <TryLoad>b__0()
			{
				return this.source.OpenStream(this.assetName);
			}

			// Token: 0x04000619 RID: 1561
			public IContentSource source;

			// Token: 0x0400061A RID: 1562
			public string assetName;
		}
	}
}
