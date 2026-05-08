using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000B0E RID: 2830
	internal sealed class CollectionDebugView<T>
	{
		// Token: 0x0600681F RID: 26655 RVA: 0x0016106D File Offset: 0x0015F26D
		public CollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06006820 RID: 26656 RVA: 0x0016108C File Offset: 0x0015F28C
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

		// Token: 0x04003C53 RID: 15443
		private readonly ICollection<T> _collection;
	}
}
