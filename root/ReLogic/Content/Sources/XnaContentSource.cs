using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace ReLogic.Content.Sources
{
	// Token: 0x020000A6 RID: 166
	public class XnaContentSource : IContentSource
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000D638 File Offset: 0x0000B838
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x0000D640 File Offset: 0x0000B840
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

		// Token: 0x060003D5 RID: 981 RVA: 0x0000D649 File Offset: 0x0000B849
		public XnaContentSource(string rootDirectory)
		{
			this._rootDirectory = AssetPathHelper.CleanPath(rootDirectory);
			this.Refresh();
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000D67C File Offset: 0x0000B87C
		public void Refresh()
		{
			this._files.Clear();
			foreach (string text in Directory.GetFiles(this._rootDirectory, "*.xnb", 1))
			{
				this._files.Add(text.ToLower());
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000D6CC File Offset: 0x0000B8CC
		public bool HasAsset(string assetName)
		{
			string text = Path.Combine(this._rootDirectory, assetName) + ".xnb";
			return !this._rejections.IsRejected(assetName) && this._files.Contains(text.ToLower());
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000D714 File Offset: 0x0000B914
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

		// Token: 0x060003D9 RID: 985 RVA: 0x0000D78C File Offset: 0x0000B98C
		public string GetExtension(string assetName)
		{
			return ".xnb";
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000D794 File Offset: 0x0000B994
		public Stream OpenStream(string assetName)
		{
			string text = Path.Combine(this._rootDirectory, assetName) + ".xnb";
			Stream stream;
			try
			{
				stream = TitleContainer.OpenStream(text);
			}
			catch (Exception ex)
			{
				throw AssetLoadException.FromMissingAsset(assetName, ex);
			}
			return stream;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000D7DC File Offset: 0x0000B9DC
		public static string GetTitleLocationPath()
		{
			string text = string.Empty;
			Assembly assembly = Assembly.GetEntryAssembly();
			if (assembly == null)
			{
				assembly = Assembly.GetCallingAssembly();
			}
			if (assembly != null)
			{
				text = Path.GetDirectoryName(assembly.Location);
			}
			return text;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000D81A File Offset: 0x0000BA1A
		public void RejectAsset(string assetName, IRejectionReason reason)
		{
			this._rejections.Reject(assetName, reason);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000D829 File Offset: 0x0000BA29
		public void ClearRejections()
		{
			this._rejections.Clear();
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000D836 File Offset: 0x0000BA36
		public bool TryGetRejections(List<string> rejectionReasons)
		{
			return this._rejections.TryGetRejections(rejectionReasons);
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000D844 File Offset: 0x0000BA44
		public string FileWatcherPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400052D RID: 1325
		private readonly string _rootDirectory;

		// Token: 0x0400052E RID: 1326
		private readonly HashSet<string> _files = new HashSet<string>();

		// Token: 0x0400052F RID: 1327
		[CompilerGenerated]
		private IContentValidator <ContentValidator>k__BackingField;

		// Token: 0x04000530 RID: 1328
		private readonly RejectedAssetCollection _rejections = new RejectedAssetCollection();
	}
}
