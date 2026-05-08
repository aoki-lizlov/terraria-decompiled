using System;
using System.IO;

namespace ReLogic.Content
{
	// Token: 0x0200008F RID: 143
	public static class AssetPathHelper
	{
		// Token: 0x06000334 RID: 820 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		public static string CleanPath(string path)
		{
			path = path.Replace('/', '\\');
			path = path.Replace("\\.\\", "\\");
			while (path.StartsWith(".\\"))
			{
				path = path.Substring(".\\".Length);
			}
			while (path.EndsWith("\\."))
			{
				if (path.Length > "\\.".Length)
				{
					path = path.Substring(0, path.Length - "\\.".Length);
				}
				else
				{
					path = "\\";
				}
			}
			for (int i = 1; i < path.Length; i = AssetPathHelper.CollapseParentDirectory(ref path, i, "\\..\\".Length))
			{
				i = path.IndexOf("\\..\\", i);
				if (i < 0)
				{
					break;
				}
			}
			if (path.EndsWith("\\.."))
			{
				int num = path.Length - "\\..".Length;
				if (num > 0)
				{
					AssetPathHelper.CollapseParentDirectory(ref path, num, "\\..".Length);
				}
			}
			if (path == ".")
			{
				path = string.Empty;
			}
			if (Path.DirectorySeparatorChar != '\\')
			{
				path = path.Replace('\\', Path.DirectorySeparatorChar);
			}
			return path;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000C208 File Offset: 0x0000A408
		private static int CollapseParentDirectory(ref string path, int position, int removeLength)
		{
			int num = path.LastIndexOf('\\', position - 1) + 1;
			path = path.Remove(num, position - num + removeLength);
			return Math.Max(num - 1, 1);
		}
	}
}
