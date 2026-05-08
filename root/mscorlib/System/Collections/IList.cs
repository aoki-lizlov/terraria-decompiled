using System;

namespace System.Collections
{
	// Token: 0x02000A71 RID: 2673
	public interface IList : ICollection, IEnumerable
	{
		// Token: 0x17001095 RID: 4245
		object this[int index] { get; set; }

		// Token: 0x060061A2 RID: 24994
		int Add(object value);

		// Token: 0x060061A3 RID: 24995
		bool Contains(object value);

		// Token: 0x060061A4 RID: 24996
		void Clear();

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x060061A5 RID: 24997
		bool IsReadOnly { get; }

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x060061A6 RID: 24998
		bool IsFixedSize { get; }

		// Token: 0x060061A7 RID: 24999
		int IndexOf(object value);

		// Token: 0x060061A8 RID: 25000
		void Insert(int index, object value);

		// Token: 0x060061A9 RID: 25001
		void Remove(object value);

		// Token: 0x060061AA RID: 25002
		void RemoveAt(int index);
	}
}
