using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000AEE RID: 2798
	internal sealed class IDictionaryDebugView<K, V>
	{
		// Token: 0x06006728 RID: 26408 RVA: 0x0015E118 File Offset: 0x0015C318
		public IDictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dict = dictionary;
		}

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06006729 RID: 26409 RVA: 0x0015E138 File Offset: 0x0015C338
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<K, V>[] Items
		{
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this._dict.Count];
				this._dict.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04003C10 RID: 15376
		private readonly IDictionary<K, V> _dict;
	}
}
