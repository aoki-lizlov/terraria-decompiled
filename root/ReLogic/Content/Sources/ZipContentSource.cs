using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace ReLogic.Content.Sources
{
	// Token: 0x020000A8 RID: 168
	public class ZipContentSource : IContentSource, IDisposable
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000DA0A File Offset: 0x0000BC0A
		public int EntryCount
		{
			get
			{
				return this._entries.Count;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000DA17 File Offset: 0x0000BC17
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000DA1F File Offset: 0x0000BC1F
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

		// Token: 0x060003EF RID: 1007 RVA: 0x0000DA28 File Offset: 0x0000BC28
		public ZipContentSource(string path)
			: this(path, "")
		{
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000DA36 File Offset: 0x0000BC36
		public ZipContentSource(string path, string contentDir)
			: this(ZipFile.Read(path), contentDir)
		{
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000DA48 File Offset: 0x0000BC48
		public ZipContentSource(ZipFile zip, string contentDir)
		{
			this._zipFile = zip;
			if (ZipContentSource.ZipPathContainsInvalidCharacters(contentDir))
			{
				throw new ArgumentException("Content directory cannot contain \"..\"", "contentDir");
			}
			this._basePath = ZipContentSource.CleanZipPath(contentDir);
			this.Refresh();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000DAA4 File Offset: 0x0000BCA4
		public bool HasAsset(string assetName)
		{
			ZipEntry zipEntry;
			return this._entries.TryGetValue(assetName, ref zipEntry) && !this._rejections.IsRejected(assetName);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		public List<string> GetAllAssetsStartingWith(string assetNameStart)
		{
			List<string> list = new List<string>();
			foreach (string text in this._entries.Keys)
			{
				if (text.ToLower().StartsWith(assetNameStart))
				{
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000DB44 File Offset: 0x0000BD44
		public string GetExtension(string assetName)
		{
			ZipEntry zipEntry;
			if (!this._entries.TryGetValue(assetName, ref zipEntry))
			{
				throw AssetLoadException.FromMissingAsset(assetName, null);
			}
			return Path.GetExtension(zipEntry.FileName) ?? "";
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000DB80 File Offset: 0x0000BD80
		public Stream OpenStream(string assetName)
		{
			ZipEntry zipEntry;
			if (!this._entries.TryGetValue(assetName, ref zipEntry))
			{
				throw AssetLoadException.FromMissingAsset(assetName, null);
			}
			MemoryStream memoryStream = new MemoryStream((int)zipEntry.UncompressedSize);
			zipEntry.Extract(memoryStream);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000DBC4 File Offset: 0x0000BDC4
		public void Refresh()
		{
			this._entries.Clear();
			foreach (ZipEntry zipEntry in Enumerable.Where<ZipEntry>(this._zipFile.Entries, (ZipEntry entry) => !entry.IsDirectory && entry.FileName.StartsWith(this._basePath)))
			{
				string fileName = zipEntry.FileName;
				string text = Path.GetExtension(fileName) ?? "";
				string text2 = fileName.Substring(this._basePath.Length, fileName.Length - text.Length - this._basePath.Length);
				text2 = AssetPathHelper.CleanPath(text2);
				this._entries[text2] = zipEntry;
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000DC88 File Offset: 0x0000BE88
		private static bool ZipPathContainsInvalidCharacters(string path)
		{
			return path.Contains("../") || path.Contains("..\\");
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000DCA4 File Offset: 0x0000BEA4
		private static string CleanZipPath(string path)
		{
			path = path.Replace('\\', '/');
			path = Regex.Replace(path, "^[./]+", "");
			if (path.Length != 0 && !path.EndsWith("/"))
			{
				path += "/";
			}
			return path;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000DCF2 File Offset: 0x0000BEF2
		protected virtual void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			if (disposing)
			{
				this._entries.Clear();
				this._zipFile.Dispose();
			}
			this._isDisposed = true;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000DD1D File Offset: 0x0000BF1D
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000DD26 File Offset: 0x0000BF26
		public void RejectAsset(string assetName, IRejectionReason reason)
		{
			this._rejections.Reject(assetName, reason);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000DD35 File Offset: 0x0000BF35
		public void ClearRejections()
		{
			this._rejections.Clear();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000DD42 File Offset: 0x0000BF42
		public bool TryGetRejections(List<string> rejectionReasons)
		{
			return this._rejections.TryGetRejections(rejectionReasons);
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000D844 File Offset: 0x0000BA44
		public string FileWatcherPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000DD50 File Offset: 0x0000BF50
		[CompilerGenerated]
		private bool <Refresh>b__16_0(ZipEntry entry)
		{
			return !entry.IsDirectory && entry.FileName.StartsWith(this._basePath);
		}

		// Token: 0x04000535 RID: 1333
		[CompilerGenerated]
		private IContentValidator <ContentValidator>k__BackingField;

		// Token: 0x04000536 RID: 1334
		private readonly ZipFile _zipFile;

		// Token: 0x04000537 RID: 1335
		private readonly Dictionary<string, ZipEntry> _entries = new Dictionary<string, ZipEntry>();

		// Token: 0x04000538 RID: 1336
		private readonly string _basePath;

		// Token: 0x04000539 RID: 1337
		private bool _isDisposed;

		// Token: 0x0400053A RID: 1338
		private readonly RejectedAssetCollection _rejections = new RejectedAssetCollection();
	}
}
