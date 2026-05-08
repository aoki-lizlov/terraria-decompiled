using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace ReLogic.Content.Sources
{
	// Token: 0x020000A7 RID: 167
	public class XnaDirectContentSource : IContentSource
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000D847 File Offset: 0x0000BA47
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000D84F File Offset: 0x0000BA4F
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

		// Token: 0x060003E2 RID: 994 RVA: 0x0000D858 File Offset: 0x0000BA58
		public XnaDirectContentSource(string rootDirectory)
		{
			this._rootDirectory = AssetPathHelper.CleanPath(rootDirectory);
			this.Refresh();
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000D888 File Offset: 0x0000BA88
		public void Refresh()
		{
			this._files.Clear();
			foreach (string text in Directory.GetFiles(this._rootDirectory, "*.xnb", 1))
			{
				this._files.Add(text.ToLower());
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000D8D8 File Offset: 0x0000BAD8
		public bool HasAsset(string assetName)
		{
			string text = Path.Combine(this._rootDirectory, assetName) + ".xnb";
			return !this._rejections.IsRejected(assetName) && this._files.Contains(text.ToLower());
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000D920 File Offset: 0x0000BB20
		public List<string> GetAllAssetsStartingWith(string assetNameStart)
		{
			string text = Path.Combine(this._rootDirectory, assetNameStart).ToLower();
			List<string> list = new List<string>();
			foreach (string text2 in this._files)
			{
				if (text2.StartsWith(text))
				{
					list.Add(text2);
				}
			}
			return list;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000D78C File Offset: 0x0000B98C
		public string GetExtension(string assetName)
		{
			return ".xnb";
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000D998 File Offset: 0x0000BB98
		public Stream OpenStream(string assetName)
		{
			string text = Path.Combine(this._rootDirectory, assetName) + ".xnb";
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

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000D9E0 File Offset: 0x0000BBE0
		public void RejectAsset(string assetName, IRejectionReason reason)
		{
			this._rejections.Reject(assetName, reason);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000D9EF File Offset: 0x0000BBEF
		public void ClearRejections()
		{
			this._rejections.Clear();
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000D9FC File Offset: 0x0000BBFC
		public bool TryGetRejections(List<string> rejectionReasons)
		{
			return this._rejections.TryGetRejections(rejectionReasons);
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000D844 File Offset: 0x0000BA44
		public string FileWatcherPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000531 RID: 1329
		private readonly string _rootDirectory;

		// Token: 0x04000532 RID: 1330
		private readonly HashSet<string> _files = new HashSet<string>();

		// Token: 0x04000533 RID: 1331
		[CompilerGenerated]
		private IContentValidator <ContentValidator>k__BackingField;

		// Token: 0x04000534 RID: 1332
		private readonly RejectedAssetCollection _rejections = new RejectedAssetCollection();
	}
}
