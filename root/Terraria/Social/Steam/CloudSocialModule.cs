using System;
using System.Collections.Generic;
using Steamworks;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x02000145 RID: 325
	public class CloudSocialModule : CloudSocialModule
	{
		// Token: 0x06001CBD RID: 7357 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Initialize()
		{
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Shutdown()
		{
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x004FF7D0 File Offset: 0x004FD9D0
		public override IEnumerable<string> GetFiles()
		{
			object obj = this.ioLock;
			IEnumerable<string> enumerable;
			lock (obj)
			{
				int fileCount = SteamRemoteStorage.GetFileCount();
				List<string> list = new List<string>(fileCount);
				for (int i = 0; i < fileCount; i++)
				{
					int num;
					list.Add(SteamRemoteStorage.GetFileNameAndSize(i, ref num));
				}
				enumerable = list;
			}
			return enumerable;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x004FF83C File Offset: 0x004FDA3C
		public override bool Write(string path, byte[] data, int length)
		{
			object obj = this.ioLock;
			bool flag3;
			lock (obj)
			{
				UGCFileWriteStreamHandle_t ugcfileWriteStreamHandle_t = SteamRemoteStorage.FileWriteStreamOpen(path);
				bool flag2 = false;
				uint num = 0U;
				while ((ulong)num < (ulong)((long)length))
				{
					int num2 = (int)Math.Min(1024L, (long)length - (long)((ulong)num));
					Array.Copy(data, (long)((ulong)num), this.writeBuffer, 0L, (long)num2);
					if (!SteamRemoteStorage.FileWriteStreamWriteChunk(ugcfileWriteStreamHandle_t, this.writeBuffer, num2))
					{
						flag2 = true;
						break;
					}
					num += 1024U;
				}
				if (flag2)
				{
					SteamRemoteStorage.FileWriteStreamCancel(ugcfileWriteStreamHandle_t);
					flag3 = false;
				}
				else
				{
					flag3 = SteamRemoteStorage.FileWriteStreamClose(ugcfileWriteStreamHandle_t);
				}
			}
			return flag3;
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x004FF8EC File Offset: 0x004FDAEC
		public override int GetFileSize(string path)
		{
			object obj = this.ioLock;
			int fileSize;
			lock (obj)
			{
				fileSize = SteamRemoteStorage.GetFileSize(path);
			}
			return fileSize;
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x004FF930 File Offset: 0x004FDB30
		public override void Read(string path, byte[] buffer, int size)
		{
			object obj = this.ioLock;
			lock (obj)
			{
				SteamRemoteStorage.FileRead(path, buffer, size);
			}
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x004FF974 File Offset: 0x004FDB74
		public override bool HasFile(string path)
		{
			object obj = this.ioLock;
			bool flag2;
			lock (obj)
			{
				flag2 = SteamRemoteStorage.FileExists(path);
			}
			return flag2;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x004FF9B8 File Offset: 0x004FDBB8
		public override bool Delete(string path)
		{
			object obj = this.ioLock;
			bool flag2;
			lock (obj)
			{
				flag2 = SteamRemoteStorage.FileDelete(path);
			}
			return flag2;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x004FF9FC File Offset: 0x004FDBFC
		public override bool Forget(string path)
		{
			object obj = this.ioLock;
			bool flag2;
			lock (obj)
			{
				flag2 = SteamRemoteStorage.FileForget(path);
			}
			return flag2;
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x004FFA40 File Offset: 0x004FDC40
		public CloudSocialModule()
		{
		}

		// Token: 0x040015EE RID: 5614
		private const uint WRITE_CHUNK_SIZE = 1024U;

		// Token: 0x040015EF RID: 5615
		private object ioLock = new object();

		// Token: 0x040015F0 RID: 5616
		private byte[] writeBuffer = new byte[1024];
	}
}
