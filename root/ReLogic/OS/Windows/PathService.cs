using System;
using System.Diagnostics;
using System.IO;
using ReLogic.OS.Base;

namespace ReLogic.OS.Windows
{
	// Token: 0x02000067 RID: 103
	internal class PathService : PathService
	{
		// Token: 0x06000239 RID: 569 RVA: 0x00009B54 File Offset: 0x00007D54
		public override string GetStoragePath()
		{
			return Path.Combine(Environment.GetFolderPath(5), "My Games");
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009B66 File Offset: 0x00007D66
		public override void OpenURL(string url)
		{
			Process.Start("explorer.exe", "\"" + url + "\"");
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00009B83 File Offset: 0x00007D83
		public override void MoveToRecycleBin(string path)
		{
			NativeMethods.MoveToRecycleBin(path);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009B8C File Offset: 0x00007D8C
		public PathService()
		{
		}
	}
}
