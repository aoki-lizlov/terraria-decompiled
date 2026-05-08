using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace ReLogic.Content.Sources
{
	// Token: 0x020000A4 RID: 164
	public class FileSystemContentSource : IContentSource
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000D3A9 File Offset: 0x0000B5A9
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000D3B1 File Offset: 0x0000B5B1
		public IContentValidator ContentValidator
		{
			[CompilerGenerated]
			get
			{
				return this.<ContentValidator>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ContentValidator>k__BackingField = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000D3BA File Offset: 0x0000B5BA
		public int FileCount
		{
			get
			{
				return this._nameToAbsolutePath.Count;
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000D3C8 File Offset: 0x0000B5C8
		public FileSystemContentSource(string basePath)
		{
			this._basePath = Path.GetFullPath(basePath);
			if (!this._basePath.EndsWith("/") && !this._basePath.EndsWith("\\"))
			{
				string basePath2 = this._basePath;
				char directorySeparatorChar = Path.DirectorySeparatorChar;
				this._basePath = basePath2 + directorySeparatorChar.ToString();
			}
			this.Refresh();
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000D445 File Offset: 0x0000B645
		public bool HasAsset(string assetName)
		{
			return !this._rejections.IsRejected(assetName) && this._nameToAbsolutePath.ContainsKey(assetName);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000D464 File Offset: 0x0000B664
		public List<string> GetAllAssetsStartingWith(string assetNameStart)
		{
			List<string> list = new List<string>();
			foreach (string text in this._nameToAbsolutePath.Keys)
			{
				if (text.ToLower().StartsWith(assetNameStart))
				{
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000D4D4 File Offset: 0x0000B6D4
		public string GetExtension(string assetName)
		{
			string text;
			if (!this._nameToAbsolutePath.TryGetValue(assetName, ref text))
			{
				throw AssetLoadException.FromMissingAsset(assetName, null);
			}
			return Path.GetExtension(text) ?? "";
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000D508 File Offset: 0x0000B708
		public Stream OpenStream(string assetName)
		{
			string text;
			if (!this._nameToAbsolutePath.TryGetValue(assetName, ref text))
			{
				throw AssetLoadException.FromMissingAsset(assetName, null);
			}
			if (!File.Exists(text))
			{
				throw AssetLoadException.FromMissingAsset(assetName, null);
			}
			Stream stream;
			try
			{
				stream = File.OpenRead(text);
			}
			catch (Exception ex)
			{
				throw AssetLoadException.FromMissingAsset(assetName, ex);
			}
			return stream;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000D564 File Offset: 0x0000B764
		public void Refresh()
		{
			this._nameToAbsolutePath.Clear();
			if (!Directory.Exists(this._basePath))
			{
				return;
			}
			string[] files = Directory.GetFiles(this._basePath, "*", 1);
			for (int i = 0; i < files.Length; i++)
			{
				string fullPath = Path.GetFullPath(files[i]);
				string text = Path.GetExtension(fullPath) ?? "";
				string text2 = fullPath.Substring(this._basePath.Length, fullPath.Length - text.Length - this._basePath.Length);
				text2 = AssetPathHelper.CleanPath(text2);
				this._nameToAbsolutePath[text2] = fullPath;
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000D606 File Offset: 0x0000B806
		public void RejectAsset(string assetName, IRejectionReason reason)
		{
			this._rejections.Reject(assetName, reason);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000D615 File Offset: 0x0000B815
		public void ClearRejections()
		{
			this._rejections.Clear();
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000D622 File Offset: 0x0000B822
		public bool TryGetRejections(List<string> rejectionReasons)
		{
			return this._rejections.TryGetRejections(rejectionReasons);
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000D630 File Offset: 0x0000B830
		public string FileWatcherPath
		{
			get
			{
				return this._basePath;
			}
		}

		// Token: 0x04000529 RID: 1321
		[CompilerGenerated]
		private IContentValidator <ContentValidator>k__BackingField;

		// Token: 0x0400052A RID: 1322
		private readonly string _basePath;

		// Token: 0x0400052B RID: 1323
		private readonly Dictionary<string, string> _nameToAbsolutePath = new Dictionary<string, string>();

		// Token: 0x0400052C RID: 1324
		private readonly RejectedAssetCollection _rejections = new RejectedAssetCollection();
	}
}
