using System;
using System.IO;

namespace MonoGame.Utilities
{
	// Token: 0x02000009 RID: 9
	internal static class FileHelpers
	{
		// Token: 0x060008C9 RID: 2249 RVA: 0x0000508C File Offset: 0x0000328C
		public static string NormalizeFilePathSeparators(string name)
		{
			return name.Replace(FileHelpers.NotSeparator, FileHelpers.Separator);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000050A0 File Offset: 0x000032A0
		public static string ResolveRelativePath(string filePath, string relativeFile)
		{
			filePath = filePath.Replace(FileHelpers.BackwardSlash, FileHelpers.ForwardSlash);
			while (filePath.Contains("//"))
			{
				filePath = filePath.Replace("//", "/");
			}
			bool flag = filePath.StartsWith(FileHelpers.ForwardSlashString);
			if (!flag)
			{
				filePath = FileHelpers.ForwardSlashString + filePath;
			}
			string text = new Uri(new Uri("file://" + filePath), relativeFile).LocalPath;
			if (!flag && text.StartsWith("/"))
			{
				text = text.Substring(1);
			}
			return FileHelpers.NormalizeFilePathSeparators(text);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00005134 File Offset: 0x00003334
		// Note: this type is marked as 'beforefieldinit'.
		static FileHelpers()
		{
		}

		// Token: 0x04000402 RID: 1026
		public static readonly char ForwardSlash = '/';

		// Token: 0x04000403 RID: 1027
		public static readonly string ForwardSlashString = new string(FileHelpers.ForwardSlash, 1);

		// Token: 0x04000404 RID: 1028
		public static readonly char BackwardSlash = '\\';

		// Token: 0x04000405 RID: 1029
		public static readonly char NotSeparator = ((Path.DirectorySeparatorChar == FileHelpers.BackwardSlash) ? FileHelpers.ForwardSlash : FileHelpers.BackwardSlash);

		// Token: 0x04000406 RID: 1030
		public static readonly char Separator = Path.DirectorySeparatorChar;
	}
}
