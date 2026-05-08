using System;
using System.Collections.Generic;
using Terraria.IO;

namespace Terraria.Social.Base
{
	// Token: 0x02000161 RID: 353
	public abstract class CloudSocialModule : ISocialModule
	{
		// Token: 0x06001D7E RID: 7550 RVA: 0x00501CA1 File Offset: 0x004FFEA1
		public virtual void BindTo(Preferences preferences)
		{
			preferences.OnSave += this.Configuration_OnSave;
			preferences.OnLoad += this.Configuration_OnLoad;
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x00501CC7 File Offset: 0x004FFEC7
		private void Configuration_OnLoad(Preferences preferences)
		{
			this.EnabledByDefault = preferences.Get<bool>("CloudSavingDefault", false);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x00501CDB File Offset: 0x004FFEDB
		private void Configuration_OnSave(Preferences preferences)
		{
			preferences.Put("CloudSavingDefault", this.EnabledByDefault);
		}

		// Token: 0x06001D81 RID: 7553
		public abstract void Initialize();

		// Token: 0x06001D82 RID: 7554
		public abstract void Shutdown();

		// Token: 0x06001D83 RID: 7555
		public abstract IEnumerable<string> GetFiles();

		// Token: 0x06001D84 RID: 7556
		public abstract bool Write(string path, byte[] data, int length);

		// Token: 0x06001D85 RID: 7557
		public abstract void Read(string path, byte[] buffer, int length);

		// Token: 0x06001D86 RID: 7558
		public abstract bool HasFile(string path);

		// Token: 0x06001D87 RID: 7559
		public abstract int GetFileSize(string path);

		// Token: 0x06001D88 RID: 7560
		public abstract bool Delete(string path);

		// Token: 0x06001D89 RID: 7561
		public abstract bool Forget(string path);

		// Token: 0x06001D8A RID: 7562 RVA: 0x00501CF4 File Offset: 0x004FFEF4
		public byte[] Read(string path)
		{
			byte[] array = new byte[this.GetFileSize(path)];
			this.Read(path, array, array.Length);
			return array;
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x00501D1A File Offset: 0x004FFF1A
		public void Read(string path, byte[] buffer)
		{
			this.Read(path, buffer, buffer.Length);
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x00501D27 File Offset: 0x004FFF27
		public bool Write(string path, byte[] data)
		{
			return this.Write(path, data, data.Length);
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x0000357B File Offset: 0x0000177B
		protected CloudSocialModule()
		{
		}

		// Token: 0x04001659 RID: 5721
		public bool EnabledByDefault;
	}
}
