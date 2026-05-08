using System;

namespace ReLogic.OS
{
	// Token: 0x02000063 RID: 99
	public interface IPathService
	{
		// Token: 0x06000220 RID: 544
		string GetStoragePath();

		// Token: 0x06000221 RID: 545
		string GetStoragePath(string subfolder);

		// Token: 0x06000222 RID: 546
		string ExpandPathVariables(string path);

		// Token: 0x06000223 RID: 547
		void OpenURL(string url);

		// Token: 0x06000224 RID: 548
		void MoveToRecycleBin(string path);
	}
}
