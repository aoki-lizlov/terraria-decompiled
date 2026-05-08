using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000AEF RID: 2799
	internal sealed class DictionaryKeyCollectionDebugView<TKey, TValue>
	{
		// Token: 0x0600672A RID: 26410 RVA: 0x0015E164 File Offset: 0x0015C364
		public DictionaryKeyCollectionDebugView(ICollection<TKey> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x0600672B RID: 26411 RVA: 0x0015E184 File Offset: 0x0015C384
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TKey[] Items
		{
			get
			{
				TKey[] array = new TKey[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04003C11 RID: 15377
		private readonly ICollection<TKey> _collection;
	}
}
