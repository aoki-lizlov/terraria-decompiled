using System;

namespace System.Collections
{
	// Token: 0x02000A68 RID: 2664
	[Serializable]
	public struct DictionaryEntry
	{
		// Token: 0x0600617C RID: 24956 RVA: 0x0014DB9E File Offset: 0x0014BD9E
		public DictionaryEntry(object key, object value)
		{
			this._key = key;
			this._value = value;
		}

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x0600617D RID: 24957 RVA: 0x0014DBAE File Offset: 0x0014BDAE
		// (set) Token: 0x0600617E RID: 24958 RVA: 0x0014DBB6 File Offset: 0x0014BDB6
		public object Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x0600617F RID: 24959 RVA: 0x0014DBBF File Offset: 0x0014BDBF
		// (set) Token: 0x06006180 RID: 24960 RVA: 0x0014DBC7 File Offset: 0x0014BDC7
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06006181 RID: 24961 RVA: 0x0014DBD0 File Offset: 0x0014BDD0
		public void Deconstruct(out object key, out object value)
		{
			key = this.Key;
			value = this.Value;
		}

		// Token: 0x04003A94 RID: 14996
		private object _key;

		// Token: 0x04003A95 RID: 14997
		private object _value;
	}
}
