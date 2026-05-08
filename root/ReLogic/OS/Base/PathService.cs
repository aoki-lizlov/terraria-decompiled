using System;
using System.IO;

namespace ReLogic.OS.Base
{
	// Token: 0x02000078 RID: 120
	internal abstract class PathService : IPathService
	{
		// Token: 0x06000292 RID: 658 RVA: 0x0000A96A File Offset: 0x00008B6A
		public string ExpandPathVariables(string path)
		{
			return Environment.ExpandEnvironmentVariables(path);
		}

		// Token: 0x06000293 RID: 659
		public abstract string GetStoragePath();

		// Token: 0x06000294 RID: 660 RVA: 0x0000A972 File Offset: 0x00008B72
		public string GetStoragePath(string subfolder)
		{
			return Path.Combine(this.GetStoragePath(), subfolder);
		}

		// Token: 0x06000295 RID: 661
		public abstract void OpenURL(string url);

		// Token: 0x06000296 RID: 662
		public abstract void MoveToRecycleBin(string path);

		// Token: 0x06000297 RID: 663 RVA: 0x0000448A File Offset: 0x0000268A
		protected PathService()
		{
		}
	}
}
