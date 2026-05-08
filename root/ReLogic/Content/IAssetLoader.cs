using System;
using System.IO;
using ReLogic.Content.Sources;

namespace ReLogic.Content
{
	// Token: 0x020000A0 RID: 160
	public interface IAssetLoader
	{
		// Token: 0x060003B0 RID: 944
		bool TryLoad<T>(string assetName, IContentSource source, out T resultAsset) where T : class;

		// Token: 0x060003B1 RID: 945
		bool TryLoad<T>(string extension, Func<Stream> getStream, out T resultAsset) where T : class;
	}
}
