using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200005A RID: 90
	internal static class StringReferenceExtensions
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x000123CC File Offset: 0x000105CC
		public static int IndexOf(this StringReference s, char c, int startIndex, int length)
		{
			int num = Array.IndexOf<char>(s.Chars, c, s.StartIndex + startIndex, length);
			if (num == -1)
			{
				return -1;
			}
			return num - s.StartIndex;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00012400 File Offset: 0x00010600
		public static bool StartsWith(this StringReference s, string text)
		{
			if (text.Length > s.Length)
			{
				return false;
			}
			char[] chars = s.Chars;
			for (int i = 0; i < text.Length; i++)
			{
				if (text.get_Chars(i) != chars[i + s.StartIndex])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00012450 File Offset: 0x00010650
		public static bool EndsWith(this StringReference s, string text)
		{
			if (text.Length > s.Length)
			{
				return false;
			}
			char[] chars = s.Chars;
			int num = s.StartIndex + s.Length - text.Length;
			for (int i = 0; i < text.Length; i++)
			{
				if (text.get_Chars(i) != chars[i + num])
				{
					return false;
				}
			}
			return true;
		}
	}
}
