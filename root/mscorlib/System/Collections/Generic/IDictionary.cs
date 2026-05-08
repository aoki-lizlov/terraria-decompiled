using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AED RID: 2797
	public interface IDictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x17001209 RID: 4617
		TValue this[TKey key] { get; set; }

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06006722 RID: 26402
		ICollection<TKey> Keys { get; }

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06006723 RID: 26403
		ICollection<TValue> Values { get; }

		// Token: 0x06006724 RID: 26404
		bool ContainsKey(TKey key);

		// Token: 0x06006725 RID: 26405
		void Add(TKey key, TValue value);

		// Token: 0x06006726 RID: 26406
		bool Remove(TKey key);

		// Token: 0x06006727 RID: 26407
		bool TryGetValue(TKey key, out TValue value);
	}
}
