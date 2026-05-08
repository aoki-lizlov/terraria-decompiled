using System;
using System.Collections.Generic;

namespace System.Collections.Concurrent
{
	// Token: 0x02000AB9 RID: 2745
	public interface IProducerConsumerCollection<T> : IEnumerable<T>, IEnumerable, ICollection
	{
		// Token: 0x0600657E RID: 25982
		void CopyTo(T[] array, int index);

		// Token: 0x0600657F RID: 25983
		bool TryAdd(T item);

		// Token: 0x06006580 RID: 25984
		bool TryTake(out T item);

		// Token: 0x06006581 RID: 25985
		T[] ToArray();
	}
}
