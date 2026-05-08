using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000B0F RID: 2831
	internal sealed class DictionaryDebugView<K, V>
	{
		// Token: 0x06006821 RID: 26657 RVA: 0x001610B8 File Offset: 0x0015F2B8
		public DictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dict = dictionary;
		}

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06006822 RID: 26658 RVA: 0x001610D8 File Offset: 0x0015F2D8
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

		// Token: 0x04003C54 RID: 15444
		private readonly IDictionary<K, V> _dict;
	}
}
