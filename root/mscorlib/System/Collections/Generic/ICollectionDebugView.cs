using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000AEB RID: 2795
	internal sealed class ICollectionDebugView<T>
	{
		// Token: 0x0600671D RID: 26397 RVA: 0x0015E0CF File Offset: 0x0015C2CF
		public ICollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x0600671E RID: 26398 RVA: 0x0015E0EC File Offset: 0x0015C2EC
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				T[] array = new T[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04003C0F RID: 15375
		private readonly ICollection<T> _collection;
	}
}
