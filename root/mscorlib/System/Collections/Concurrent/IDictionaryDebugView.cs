using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x02000AB5 RID: 2741
	internal sealed class IDictionaryDebugView<K, V>
	{
		// Token: 0x06006557 RID: 25943 RVA: 0x0015997C File Offset: 0x00157B7C
		public IDictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dictionary = dictionary;
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x06006558 RID: 25944 RVA: 0x0015999C File Offset: 0x00157B9C
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<K, V>[] Items
		{
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this._dictionary.Count];
				this._dictionary.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04003B82 RID: 15234
		private readonly IDictionary<K, V> _dictionary;
	}
}
