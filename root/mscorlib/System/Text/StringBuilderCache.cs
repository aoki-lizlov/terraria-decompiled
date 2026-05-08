using System;

namespace System.Text
{
	// Token: 0x0200037C RID: 892
	internal static class StringBuilderCache
	{
		// Token: 0x06002699 RID: 9881 RVA: 0x0008AB44 File Offset: 0x00088D44
		public static StringBuilder Acquire(int capacity = 16)
		{
			if (capacity <= 360)
			{
				StringBuilder stringBuilder = StringBuilderCache.t_cachedInstance;
				if (stringBuilder != null && capacity <= stringBuilder.Capacity)
				{
					StringBuilderCache.t_cachedInstance = null;
					stringBuilder.Clear();
					return stringBuilder;
				}
			}
			return new StringBuilder(capacity);
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x0008AB80 File Offset: 0x00088D80
		public static void Release(StringBuilder sb)
		{
			if (sb.Capacity <= 360)
			{
				StringBuilderCache.t_cachedInstance = sb;
			}
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x0008AB95 File Offset: 0x00088D95
		public static string GetStringAndRelease(StringBuilder sb)
		{
			string text = sb.ToString();
			StringBuilderCache.Release(sb);
			return text;
		}

		// Token: 0x04001CA8 RID: 7336
		private const int MaxBuilderSize = 360;

		// Token: 0x04001CA9 RID: 7337
		private const int DefaultCapacity = 16;

		// Token: 0x04001CAA RID: 7338
		[ThreadStatic]
		private static StringBuilder t_cachedInstance;
	}
}
