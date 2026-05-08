using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using ReLogic.Content.Sources;

namespace ReLogic.Content
{
	// Token: 0x02000089 RID: 137
	public sealed class AssetChangeWatcher : FileChangeWatcher<IContentSource>
	{
		// Token: 0x06000326 RID: 806 RVA: 0x0000BDF8 File Offset: 0x00009FF8
		public void UpdateSources(IEnumerable<IContentSource> sources)
		{
			Dictionary<FileSystemWatcher, IContentSource> watchers = this._watchers;
			lock (watchers)
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("AssetFileWatchers");
				}
				List<FileSystemWatcher> list = Enumerable.ToList<FileSystemWatcher>(this._watchers.Keys);
				this._watchers.Clear();
				foreach (IContentSource contentSource in sources)
				{
					string path = contentSource.FileWatcherPath;
					if (path != null && Directory.Exists(path))
					{
						FileSystemWatcher fileSystemWatcher = Enumerable.FirstOrDefault<FileSystemWatcher>(list, (FileSystemWatcher e) => e.Path == path);
						if (fileSystemWatcher == null)
						{
							fileSystemWatcher = base.NewWatcher(contentSource, path);
						}
						this._watchers[fileSystemWatcher] = contentSource;
					}
				}
				foreach (FileSystemWatcher fileSystemWatcher2 in list)
				{
					if (!this._watchers.ContainsKey(fileSystemWatcher2))
					{
						fileSystemWatcher2.Dispose();
					}
				}
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000BF68 File Offset: 0x0000A168
		public AssetChangeWatcher()
		{
		}

		// Token: 0x040004FA RID: 1274
		private readonly Dictionary<FileSystemWatcher, IContentSource> _watchers = new Dictionary<FileSystemWatcher, IContentSource>();

		// Token: 0x020000E7 RID: 231
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x06000476 RID: 1142 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x06000477 RID: 1143 RVA: 0x0000E631 File Offset: 0x0000C831
			internal bool <UpdateSources>b__0(FileSystemWatcher e)
			{
				return e.Path == this.path;
			}

			// Token: 0x04000618 RID: 1560
			public string path;
		}
	}
}
