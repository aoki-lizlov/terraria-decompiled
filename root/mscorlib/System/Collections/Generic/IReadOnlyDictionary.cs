using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AF6 RID: 2806
	public interface IReadOnlyDictionary<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x06006738 RID: 26424
		bool ContainsKey(TKey key);

		// Token: 0x06006739 RID: 26425
		bool TryGetValue(TKey key, out TValue value);

		// Token: 0x17001212 RID: 4626
		TValue this[TKey key] { get; }

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x0600673B RID: 26427
		IEnumerable<TKey> Keys { get; }

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x0600673C RID: 26428
		IEnumerable<TValue> Values { get; }
	}
}
