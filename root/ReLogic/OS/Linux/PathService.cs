using System;
using System.Diagnostics;
using System.IO;
using ReLogic.OS.Base;

namespace ReLogic.OS.Linux
{
	// Token: 0x02000073 RID: 115
	internal class PathService : PathService
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000A714 File Offset: 0x00008914
		public override string GetStoragePath()
		{
			string text = Environment.GetEnvironmentVariable("XDG_DATA_HOME");
			if (string.IsNullOrEmpty(text))
			{
				text = Environment.GetEnvironmentVariable("HOME");
				if (string.IsNullOrEmpty(text))
				{
					return ".";
				}
				text += "/.local/share";
			}
			return text;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A75A File Offset: 0x0000895A
		public override void OpenURL(string url)
		{
			Process.Start("xdg-open", "\"" + url + "\"");
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000A778 File Offset: 0x00008978
		public override void MoveToRecycleBin(string path)
		{
			string text = Environment.GetEnvironmentVariable("XDG_DATA_HOME");
			if (string.IsNullOrEmpty(text))
			{
				text = Path.Combine(Environment.GetFolderPath(40), ".local", "share");
			}
			string text2 = Path.Combine(text, "Trash", "files");
			string text3 = Path.Combine(text, "Trash", "info");
			Directory.CreateDirectory(text2);
			Directory.CreateDirectory(text3);
			string fileName = Path.GetFileName(path);
			string text4 = Path.Combine(text2, fileName);
			int num = 1;
			while (File.Exists(text4))
			{
				text4 = Path.Combine(text2, string.Concat(new object[]
				{
					Path.GetFileNameWithoutExtension(fileName),
					"_",
					num++,
					Path.GetExtension(fileName)
				}));
			}
			string text5 = Path.Combine(text3, Path.GetFileName(text4) + ".trashinfo");
			string text6 = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
			File.WriteAllText(text5, string.Concat(new string[] { "[Trash Info]\nPath=", path, "\nDeletionDate=", text6, "\n" }));
			File.Move(path, text4);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00009B8C File Offset: 0x00007D8C
		public PathService()
		{
		}
	}
}
