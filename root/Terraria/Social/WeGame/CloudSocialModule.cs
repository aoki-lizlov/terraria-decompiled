using System;
using System.Collections.Generic;
using rail;
using Terraria.Social.Base;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000124 RID: 292
	public class CloudSocialModule : CloudSocialModule
	{
		// Token: 0x06001B7E RID: 7038 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Initialize()
		{
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Shutdown()
		{
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x004FB32C File Offset: 0x004F952C
		public override IEnumerable<string> GetFiles()
		{
			object obj = this.ioLock;
			IEnumerable<string> enumerable;
			lock (obj)
			{
				uint fileCount = rail_api.RailFactory().RailStorageHelper().GetFileCount();
				List<string> list = new List<string>((int)fileCount);
				ulong num = 0UL;
				for (uint num2 = 0U; num2 < fileCount; num2 += 1U)
				{
					string text;
					rail_api.RailFactory().RailStorageHelper().GetFileNameAndSize(num2, ref text, ref num);
					list.Add(text);
				}
				enumerable = list;
			}
			return enumerable;
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x004FB3B4 File Offset: 0x004F95B4
		public override bool Write(string path, byte[] data, int length)
		{
			object obj = this.ioLock;
			bool flag3;
			lock (obj)
			{
				bool flag2 = true;
				IRailFile railFile;
				if (rail_api.RailFactory().RailStorageHelper().IsFileExist(path))
				{
					railFile = rail_api.RailFactory().RailStorageHelper().OpenFile(path);
				}
				else
				{
					railFile = rail_api.RailFactory().RailStorageHelper().CreateFile(path);
				}
				if (railFile != null)
				{
					railFile.Write(data, (uint)length);
					railFile.Close();
				}
				else
				{
					flag2 = false;
				}
				flag3 = flag2;
			}
			return flag3;
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x004FB444 File Offset: 0x004F9644
		public override int GetFileSize(string path)
		{
			object obj = this.ioLock;
			int num;
			lock (obj)
			{
				IRailFile railFile = rail_api.RailFactory().RailStorageHelper().OpenFile(path);
				if (railFile != null)
				{
					int size = (int)railFile.GetSize();
					railFile.Close();
					num = size;
				}
				else
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x004FB4AC File Offset: 0x004F96AC
		public override void Read(string path, byte[] buffer, int size)
		{
			object obj = this.ioLock;
			lock (obj)
			{
				IRailFile railFile = rail_api.RailFactory().RailStorageHelper().OpenFile(path);
				if (railFile != null)
				{
					railFile.Read(buffer, (uint)size);
					railFile.Close();
				}
			}
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x004FB50C File Offset: 0x004F970C
		public override bool HasFile(string path)
		{
			object obj = this.ioLock;
			bool flag2;
			lock (obj)
			{
				flag2 = rail_api.RailFactory().RailStorageHelper().IsFileExist(path);
			}
			return flag2;
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x004FB558 File Offset: 0x004F9758
		public override bool Delete(string path)
		{
			object obj = this.ioLock;
			bool flag2;
			lock (obj)
			{
				RailResult railResult = rail_api.RailFactory().RailStorageHelper().RemoveFile(path);
				flag2 = railResult == 0;
			}
			return flag2;
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x004FB5AC File Offset: 0x004F97AC
		public override bool Forget(string path)
		{
			return this.Delete(path);
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x004FB5B5 File Offset: 0x004F97B5
		public CloudSocialModule()
		{
		}

		// Token: 0x04001586 RID: 5510
		private object ioLock = new object();
	}
}
