using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AF4 RID: 2804
	public interface IList<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x17001210 RID: 4624
		T this[int index] { get; set; }

		// Token: 0x06006734 RID: 26420
		int IndexOf(T item);

		// Token: 0x06006735 RID: 26421
		void Insert(int index, T item);

		// Token: 0x06006736 RID: 26422
		void RemoveAt(int index);
	}
}
