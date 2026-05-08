using System;
using System.IO;
using MonoGame.Utilities;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000035 RID: 53
	public static class TitleContainer
	{
		// Token: 0x06000D4B RID: 3403 RVA: 0x0001ABD8 File Offset: 0x00018DD8
		public static Stream OpenStream(string name)
		{
			string text = FileHelpers.NormalizeFilePathSeparators(name);
			if (Path.IsPathRooted(text))
			{
				return File.OpenRead(text);
			}
			return File.OpenRead(Path.Combine(TitleLocation.Path, text));
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0001AC0C File Offset: 0x00018E0C
		internal static IntPtr ReadToPointer(string name, out IntPtr size)
		{
			string text = FileHelpers.NormalizeFilePathSeparators(name);
			string text2;
			if (Path.IsPathRooted(text))
			{
				text2 = text;
			}
			else
			{
				text2 = Path.Combine(TitleLocation.Path, text);
			}
			if (!File.Exists(text2))
			{
				throw new FileNotFoundException(text2);
			}
			return FNAPlatform.ReadFileToPointer(text2, out size);
		}
	}
}
