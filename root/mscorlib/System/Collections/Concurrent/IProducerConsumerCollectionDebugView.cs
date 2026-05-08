using System;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x02000ABA RID: 2746
	internal sealed class IProducerConsumerCollectionDebugView<T>
	{
		// Token: 0x06006582 RID: 25986 RVA: 0x00159FC2 File Offset: 0x001581C2
		public IProducerConsumerCollectionDebugView(IProducerConsumerCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x06006583 RID: 25987 RVA: 0x00159FDF File Offset: 0x001581DF
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._collection.ToArray();
			}
		}

		// Token: 0x04003B8B RID: 15243
		private readonly IProducerConsumerCollection<T> _collection;
	}
}
