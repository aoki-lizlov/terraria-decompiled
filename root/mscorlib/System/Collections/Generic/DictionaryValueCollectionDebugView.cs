using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000AF0 RID: 2800
	internal sealed class DictionaryValueCollectionDebugView<TKey, TValue>
	{
		// Token: 0x0600672C RID: 26412 RVA: 0x0015E1B0 File Offset: 0x0015C3B0
		public DictionaryValueCollectionDebugView(ICollection<TValue> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x0600672D RID: 26413 RVA: 0x0015E1D0 File Offset: 0x0015C3D0
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TValue[] Items
		{
			get
			{
				TValue[] array = new TValue[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04003C12 RID: 15378
		private readonly ICollection<TValue> _collection;
	}
}
