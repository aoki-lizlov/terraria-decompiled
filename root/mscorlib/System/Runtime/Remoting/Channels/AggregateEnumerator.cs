using System;
using System.Collections;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000574 RID: 1396
	internal class AggregateEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x060037A5 RID: 14245 RVA: 0x000C8DC6 File Offset: 0x000C6FC6
		public AggregateEnumerator(IDictionary[] dics)
		{
			this.dictionaries = dics;
			this.Reset();
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060037A6 RID: 14246 RVA: 0x000C8DDB File Offset: 0x000C6FDB
		public DictionaryEntry Entry
		{
			get
			{
				return this.currente.Entry;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060037A7 RID: 14247 RVA: 0x000C8DE8 File Offset: 0x000C6FE8
		public object Key
		{
			get
			{
				return this.currente.Key;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060037A8 RID: 14248 RVA: 0x000C8DF5 File Offset: 0x000C6FF5
		public object Value
		{
			get
			{
				return this.currente.Value;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060037A9 RID: 14249 RVA: 0x000C8E02 File Offset: 0x000C7002
		public object Current
		{
			get
			{
				return this.currente.Current;
			}
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x000C8E10 File Offset: 0x000C7010
		public bool MoveNext()
		{
			if (this.pos >= this.dictionaries.Length)
			{
				return false;
			}
			if (this.currente.MoveNext())
			{
				return true;
			}
			this.pos++;
			if (this.pos >= this.dictionaries.Length)
			{
				return false;
			}
			this.currente = this.dictionaries[this.pos].GetEnumerator();
			return this.MoveNext();
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x000C8E7C File Offset: 0x000C707C
		public void Reset()
		{
			this.pos = 0;
			if (this.dictionaries.Length != 0)
			{
				this.currente = this.dictionaries[0].GetEnumerator();
			}
		}

		// Token: 0x0400254E RID: 9550
		private IDictionary[] dictionaries;

		// Token: 0x0400254F RID: 9551
		private int pos;

		// Token: 0x04002550 RID: 9552
		private IDictionaryEnumerator currente;
	}
}
