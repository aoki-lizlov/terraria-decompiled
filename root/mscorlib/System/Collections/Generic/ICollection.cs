using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AEA RID: 2794
	public interface ICollection<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06006716 RID: 26390
		int Count { get; }

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06006717 RID: 26391
		bool IsReadOnly { get; }

		// Token: 0x06006718 RID: 26392
		void Add(T item);

		// Token: 0x06006719 RID: 26393
		void Clear();

		// Token: 0x0600671A RID: 26394
		bool Contains(T item);

		// Token: 0x0600671B RID: 26395
		void CopyTo(T[] array, int arrayIndex);

		// Token: 0x0600671C RID: 26396
		bool Remove(T item);
	}
}
