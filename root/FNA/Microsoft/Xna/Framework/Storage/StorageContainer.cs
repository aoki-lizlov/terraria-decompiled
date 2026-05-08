using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Xna.Framework.Storage
{
	// Token: 0x0200003C RID: 60
	public class StorageContainer : IDisposable
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x0001EFCA File Offset: 0x0001D1CA
		// (set) Token: 0x06000E4D RID: 3661 RVA: 0x0001EFD2 File Offset: 0x0001D1D2
		public string DisplayName
		{
			[CompilerGenerated]
			get
			{
				return this.<DisplayName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DisplayName>k__BackingField = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x0001EFDB File Offset: 0x0001D1DB
		// (set) Token: 0x06000E4F RID: 3663 RVA: 0x0001EFE3 File Offset: 0x0001D1E3
		public bool IsDisposed
		{
			[CompilerGenerated]
			get
			{
				return this.<IsDisposed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsDisposed>k__BackingField = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x0001EFEC File Offset: 0x0001D1EC
		// (set) Token: 0x06000E51 RID: 3665 RVA: 0x0001EFF4 File Offset: 0x0001D1F4
		public StorageDevice StorageDevice
		{
			[CompilerGenerated]
			get
			{
				return this.<StorageDevice>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<StorageDevice>k__BackingField = value;
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000E52 RID: 3666 RVA: 0x0001F000 File Offset: 0x0001D200
		// (remove) Token: 0x06000E53 RID: 3667 RVA: 0x0001F038 File Offset: 0x0001D238
		public event EventHandler<EventArgs> Disposing
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.Disposing;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.Disposing, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.Disposing;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.Disposing, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0001F070 File Offset: 0x0001D270
		internal StorageContainer(StorageDevice device, string name, string rootPath, PlayerIndex? playerIndex)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("A title name has to be provided in parameter name.");
			}
			this.StorageDevice = device;
			this.DisplayName = name;
			this.storagePath = Path.Combine(rootPath, name, (playerIndex != null) ? ("Player" + ((int)(playerIndex.Value + 1)).ToString()) : "AllPlayers");
			if (!Directory.Exists(this.storagePath))
			{
				Directory.CreateDirectory(this.storagePath);
			}
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0001F0F5 File Offset: 0x0001D2F5
		public void Dispose()
		{
			if (this.Disposing != null)
			{
				this.Disposing(this, null);
			}
			this.IsDisposed = true;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0001F114 File Offset: 0x0001D314
		public void CreateDirectory(string directory)
		{
			if (string.IsNullOrEmpty(directory))
			{
				throw new ArgumentNullException("Parameter directory must contain a value.");
			}
			string text = Path.Combine(this.storagePath, directory);
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0001F150 File Offset: 0x0001D350
		public Stream CreateFile(string file)
		{
			if (string.IsNullOrEmpty(file))
			{
				throw new ArgumentNullException("Parameter file must contain a value.");
			}
			return File.Create(Path.Combine(this.storagePath, file));
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0001F176 File Offset: 0x0001D376
		public void DeleteDirectory(string directory)
		{
			if (string.IsNullOrEmpty(directory))
			{
				throw new ArgumentNullException("Parameter directory must contain a value.");
			}
			Directory.Delete(Path.Combine(this.storagePath, directory));
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x0001F19C File Offset: 0x0001D39C
		public void DeleteFile(string file)
		{
			if (string.IsNullOrEmpty(file))
			{
				throw new ArgumentNullException("Parameter file must contain a value.");
			}
			File.Delete(Path.Combine(this.storagePath, file));
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0001F1C2 File Offset: 0x0001D3C2
		public bool DirectoryExists(string directory)
		{
			if (string.IsNullOrEmpty(directory))
			{
				throw new ArgumentNullException("Parameter directory must contain a value.");
			}
			return Directory.Exists(Path.Combine(this.storagePath, directory));
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0001F1E8 File Offset: 0x0001D3E8
		public bool FileExists(string file)
		{
			if (string.IsNullOrEmpty(file))
			{
				throw new ArgumentNullException("Parameter file must contain a value.");
			}
			return File.Exists(Path.Combine(this.storagePath, file));
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0001F210 File Offset: 0x0001D410
		public string[] GetDirectoryNames()
		{
			string[] directories = Directory.GetDirectories(this.storagePath);
			for (int i = 0; i < directories.Length; i++)
			{
				directories[i] = directories[i].Substring(this.storagePath.Length + 1);
			}
			return directories;
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0001F250 File Offset: 0x0001D450
		public string[] GetDirectoryNames(string searchPattern)
		{
			if (string.IsNullOrEmpty(searchPattern))
			{
				throw new ArgumentNullException("Parameter searchPattern must contain a value.");
			}
			string[] directories = Directory.GetDirectories(this.storagePath, searchPattern);
			for (int i = 0; i < directories.Length; i++)
			{
				directories[i] = directories[i].Substring(this.storagePath.Length + 1);
			}
			return directories;
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0001F2A4 File Offset: 0x0001D4A4
		public string[] GetFileNames()
		{
			string[] files = Directory.GetFiles(this.storagePath);
			for (int i = 0; i < files.Length; i++)
			{
				files[i] = files[i].Substring(this.storagePath.Length + 1);
			}
			return files;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0001F2E4 File Offset: 0x0001D4E4
		public string[] GetFileNames(string searchPattern)
		{
			if (string.IsNullOrEmpty(searchPattern))
			{
				throw new ArgumentNullException("Parameter searchPattern must contain a value.");
			}
			string[] files = Directory.GetFiles(this.storagePath, searchPattern);
			for (int i = 0; i < files.Length; i++)
			{
				files[i] = files[i].Substring(this.storagePath.Length + 1);
			}
			return files;
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0001F338 File Offset: 0x0001D538
		public Stream OpenFile(string file, FileMode fileMode)
		{
			return this.OpenFile(file, fileMode, FileAccess.ReadWrite, FileShare.ReadWrite);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0001F344 File Offset: 0x0001D544
		public Stream OpenFile(string file, FileMode fileMode, FileAccess fileAccess)
		{
			return this.OpenFile(file, fileMode, fileAccess, FileShare.ReadWrite);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0001F350 File Offset: 0x0001D550
		public Stream OpenFile(string file, FileMode fileMode, FileAccess fileAccess, FileShare fileShare)
		{
			if (string.IsNullOrEmpty(file))
			{
				throw new ArgumentNullException("Parameter file must contain a value.");
			}
			return File.Open(Path.Combine(this.storagePath, file), fileMode, fileAccess, fileShare);
		}

		// Token: 0x040005DB RID: 1499
		[CompilerGenerated]
		private string <DisplayName>k__BackingField;

		// Token: 0x040005DC RID: 1500
		[CompilerGenerated]
		private bool <IsDisposed>k__BackingField;

		// Token: 0x040005DD RID: 1501
		[CompilerGenerated]
		private StorageDevice <StorageDevice>k__BackingField;

		// Token: 0x040005DE RID: 1502
		private readonly string storagePath;

		// Token: 0x040005DF RID: 1503
		[CompilerGenerated]
		private EventHandler<EventArgs> Disposing;
	}
}
