using System;

namespace System.Collections
{
	// Token: 0x02000A6C RID: 2668
	public interface IDictionary : ICollection, IEnumerable
	{
		// Token: 0x1700108C RID: 4236
		object this[object key] { get; set; }

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x0600618E RID: 24974
		ICollection Keys { get; }

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x0600618F RID: 24975
		ICollection Values { get; }

		// Token: 0x06006190 RID: 24976
		bool Contains(object key);

		// Token: 0x06006191 RID: 24977
		void Add(object key, object value);

		// Token: 0x06006192 RID: 24978
		void Clear();

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x06006193 RID: 24979
		bool IsReadOnly { get; }

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x06006194 RID: 24980
		bool IsFixedSize { get; }

		// Token: 0x06006195 RID: 24981
		IDictionaryEnumerator GetEnumerator();

		// Token: 0x06006196 RID: 24982
		void Remove(object key);
	}
}
