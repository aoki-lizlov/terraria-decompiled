using System;

namespace System.Collections
{
	// Token: 0x02000A6A RID: 2666
	public interface ICollection : IEnumerable
	{
		// Token: 0x06006187 RID: 24967
		void CopyTo(Array array, int index);

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x06006188 RID: 24968
		int Count { get; }

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x06006189 RID: 24969
		object SyncRoot { get; }

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x0600618A RID: 24970
		bool IsSynchronized { get; }
	}
}
