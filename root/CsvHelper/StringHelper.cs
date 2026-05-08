using System;

namespace CsvHelper
{
	// Token: 0x02000018 RID: 24
	internal static class StringHelper
	{
		// Token: 0x0600010F RID: 271 RVA: 0x00005A3C File Offset: 0x00003C3C
		public static bool IsNullOrWhiteSpace(string s)
		{
			if (s == null)
			{
				return true;
			}
			for (int i = 0; i < s.Length; i++)
			{
				if (!char.IsWhiteSpace(s.get_Chars(i)))
				{
					return false;
				}
			}
			return true;
		}
	}
}
