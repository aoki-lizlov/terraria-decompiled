using System;
using System.Collections.Generic;
using System.IO;
using ReLogic.Content.Readers;

namespace ReLogic.Content
{
	// Token: 0x02000090 RID: 144
	public class AssetReaderCollection
	{
		// Token: 0x06000336 RID: 822 RVA: 0x0000C23C File Offset: 0x0000A43C
		public void RegisterReader(IAssetReader reader, params string[] extensions)
		{
			foreach (string text in extensions)
			{
				this._readersByExtension[text.ToLower()] = reader;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000C270 File Offset: 0x0000A470
		public T Read<T>(Stream stream, string extension) where T : class
		{
			IAssetReader assetReader;
			if (!this._readersByExtension.TryGetValue(extension.ToLower(), ref assetReader))
			{
				throw AssetLoadException.FromMissingReader(extension);
			}
			return assetReader.FromStream<T>(stream);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000C2A0 File Offset: 0x0000A4A0
		public bool CanReadExtension(string extension)
		{
			return this._readersByExtension.ContainsKey(extension.ToLower());
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000C2B3 File Offset: 0x0000A4B3
		public AssetReaderCollection()
		{
		}

		// Token: 0x040004FD RID: 1277
		private readonly Dictionary<string, IAssetReader> _readersByExtension = new Dictionary<string, IAssetReader>();
	}
}
