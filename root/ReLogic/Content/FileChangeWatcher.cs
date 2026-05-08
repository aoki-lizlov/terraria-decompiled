using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ReLogic.Content
{
	// Token: 0x02000095 RID: 149
	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "Class only contains managed resources")]
	public class FileChangeWatcher<T> : IDisposable
	{
		// Token: 0x0600036E RID: 878 RVA: 0x0000CED8 File Offset: 0x0000B0D8
		public FileSystemWatcher NewWatcher(T source, string path)
		{
			FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
			fileSystemWatcher.Path = path;
			fileSystemWatcher.NotifyFilter = 19;
			fileSystemWatcher.IncludeSubdirectories = true;
			fileSystemWatcher.Changed += delegate(object sender, FileSystemEventArgs e)
			{
				this.AddFileUpdate(source, e);
			};
			fileSystemWatcher.Created += delegate(object sender, FileSystemEventArgs e)
			{
				this.AddFileOrDirectoryUpdateRecursive(source, e);
			};
			fileSystemWatcher.Renamed += delegate(object sender, RenamedEventArgs e)
			{
				this.AddFileOrDirectoryUpdateRecursive(source, e);
			};
			fileSystemWatcher.EnableRaisingEvents = true;
			return fileSystemWatcher;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000CF51 File Offset: 0x0000B151
		private void AddFileUpdate(T source, FileSystemEventArgs e)
		{
			if (!Directory.Exists(e.FullPath))
			{
				this.AddUpdate(source, e);
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000CF68 File Offset: 0x0000B168
		private void AddFileOrDirectoryUpdateRecursive(T source, FileSystemEventArgs e)
		{
			if (!Directory.Exists(e.FullPath))
			{
				this.AddUpdate(source, e);
				return;
			}
			string text = e.FullPath.Substring(0, e.FullPath.Length - e.Name.Length);
			foreach (string text2 in Directory.EnumerateFiles(e.FullPath, "*", 1))
			{
				this.AddUpdate(source, new FileSystemEventArgs(e.ChangeType, text, text2.Substring(text.Length)));
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000D014 File Offset: 0x0000B214
		private void AddUpdate(T source, FileSystemEventArgs e)
		{
			HashSet<FileChangeWatcher<T>.FileUpdate> updates = this._updates;
			lock (updates)
			{
				this._updates.Add(new FileChangeWatcher<T>.FileUpdate
				{
					Source = source,
					Path = e.Name,
					FullPath = e.FullPath
				});
				this._cooldown = 10;
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000D090 File Offset: 0x0000B290
		public FileChangeWatcher<T>.FileUpdate[] GetUpdates()
		{
			HashSet<FileChangeWatcher<T>.FileUpdate> updates = this._updates;
			FileChangeWatcher<T>.FileUpdate[] array;
			lock (updates)
			{
				if (this._cooldown > 0)
				{
					int num = this._cooldown - 1;
					this._cooldown = num;
					if (num <= 0)
					{
						array = Enumerable.ToArray<FileChangeWatcher<T>.FileUpdate>(this._updates);
						this._updates.Clear();
						return array;
					}
				}
				return FileChangeWatcher<T>._empty;
			}
			return array;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000D10C File Offset: 0x0000B30C
		[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "Class only contains managed resources")]
		public void Dispose()
		{
			if (this._disposed)
			{
				return;
			}
			foreach (FileSystemWatcher fileSystemWatcher in this._watchers)
			{
				fileSystemWatcher.Dispose();
			}
			this._watchers.Clear();
			this._disposed = true;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000D178 File Offset: 0x0000B378
		public FileChangeWatcher()
		{
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000D196 File Offset: 0x0000B396
		// Note: this type is marked as 'beforefieldinit'.
		static FileChangeWatcher()
		{
		}

		// Token: 0x0400051E RID: 1310
		private const int UPDATE_COOLDOWN = 10;

		// Token: 0x0400051F RID: 1311
		private readonly List<FileSystemWatcher> _watchers = new List<FileSystemWatcher>();

		// Token: 0x04000520 RID: 1312
		private readonly HashSet<FileChangeWatcher<T>.FileUpdate> _updates = new HashSet<FileChangeWatcher<T>.FileUpdate>();

		// Token: 0x04000521 RID: 1313
		private int _cooldown;

		// Token: 0x04000522 RID: 1314
		protected bool _disposed;

		// Token: 0x04000523 RID: 1315
		private static FileChangeWatcher<T>.FileUpdate[] _empty = new FileChangeWatcher<T>.FileUpdate[0];

		// Token: 0x020000F2 RID: 242
		public struct FileUpdate : IEquatable<FileChangeWatcher<T>.FileUpdate>
		{
			// Token: 0x0600048D RID: 1165 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
			public override int GetHashCode()
			{
				return this.Source.GetHashCode() ^ this.Path.GetHashCode();
			}

			// Token: 0x0600048E RID: 1166 RVA: 0x0000E9D3 File Offset: 0x0000CBD3
			public bool Equals(FileChangeWatcher<T>.FileUpdate other)
			{
				return this == other;
			}

			// Token: 0x0600048F RID: 1167 RVA: 0x0000E9E1 File Offset: 0x0000CBE1
			public override bool Equals(object obj)
			{
				return obj is FileChangeWatcher<T>.FileUpdate && this.Equals((FileChangeWatcher<T>.FileUpdate)obj);
			}

			// Token: 0x06000490 RID: 1168 RVA: 0x0000E9F9 File Offset: 0x0000CBF9
			public static bool operator ==(FileChangeWatcher<T>.FileUpdate a, FileChangeWatcher<T>.FileUpdate b)
			{
				return EqualityComparer<T>.Default.Equals(a.Source, b.Source) && a.Path == b.Path;
			}

			// Token: 0x06000491 RID: 1169 RVA: 0x0000EA26 File Offset: 0x0000CC26
			public static bool operator !=(FileChangeWatcher<T>.FileUpdate a, FileChangeWatcher<T>.FileUpdate b)
			{
				return !(a == b);
			}

			// Token: 0x04000632 RID: 1586
			public T Source;

			// Token: 0x04000633 RID: 1587
			public string Path;

			// Token: 0x04000634 RID: 1588
			public string FullPath;
		}

		// Token: 0x020000F3 RID: 243
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x06000492 RID: 1170 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06000493 RID: 1171 RVA: 0x0000EA32 File Offset: 0x0000CC32
			internal void <NewWatcher>b__0(object sender, FileSystemEventArgs e)
			{
				this.<>4__this.AddFileUpdate(this.source, e);
			}

			// Token: 0x06000494 RID: 1172 RVA: 0x0000EA46 File Offset: 0x0000CC46
			internal void <NewWatcher>b__1(object sender, FileSystemEventArgs e)
			{
				this.<>4__this.AddFileOrDirectoryUpdateRecursive(this.source, e);
			}

			// Token: 0x06000495 RID: 1173 RVA: 0x0000EA46 File Offset: 0x0000CC46
			internal void <NewWatcher>b__2(object sender, RenamedEventArgs e)
			{
				this.<>4__this.AddFileOrDirectoryUpdateRecursive(this.source, e);
			}

			// Token: 0x04000635 RID: 1589
			public FileChangeWatcher<T> <>4__this;

			// Token: 0x04000636 RID: 1590
			public T source;
		}
	}
}
