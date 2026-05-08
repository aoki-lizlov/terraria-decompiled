using System;
using System.Text;

namespace System.Collections.Generic
{
	// Token: 0x02000AF9 RID: 2809
	public static class KeyValuePair
	{
		// Token: 0x06006742 RID: 26434 RVA: 0x0015E23D File Offset: 0x0015C43D
		public static KeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value)
		{
			return new KeyValuePair<TKey, TValue>(key, value);
		}

		// Token: 0x06006743 RID: 26435 RVA: 0x0015E248 File Offset: 0x0015C448
		internal static string PairToString(object key, object value)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			stringBuilder.Append('[');
			if (key != null)
			{
				stringBuilder.Append(key);
			}
			stringBuilder.Append(", ");
			if (value != null)
			{
				stringBuilder.Append(value);
			}
			stringBuilder.Append(']');
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}
	}
}
