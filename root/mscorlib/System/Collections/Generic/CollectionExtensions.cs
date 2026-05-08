using System;

namespace System.Collections.Generic
{
	// Token: 0x02000B07 RID: 2823
	public static class CollectionExtensions
	{
		// Token: 0x060067DC RID: 26588 RVA: 0x001600A4 File Offset: 0x0015E2A4
		public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
		{
			return dictionary.GetValueOrDefault(key, default(TValue));
		}

		// Token: 0x060067DD RID: 26589 RVA: 0x001600C4 File Offset: 0x0015E2C4
		public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			TValue tvalue;
			if (!dictionary.TryGetValue(key, out tvalue))
			{
				return defaultValue;
			}
			return tvalue;
		}

		// Token: 0x060067DE RID: 26590 RVA: 0x001600ED File Offset: 0x0015E2ED
		public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			if (!dictionary.ContainsKey(key))
			{
				dictionary.Add(key, value);
				return true;
			}
			return false;
		}

		// Token: 0x060067DF RID: 26591 RVA: 0x00160111 File Offset: 0x0015E311
		public static bool Remove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			if (dictionary.TryGetValue(key, out value))
			{
				dictionary.Remove(key);
				return true;
			}
			value = default(TValue);
			return false;
		}
	}
}
