using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000575 RID: 1397
	[ComVisible(true)]
	public abstract class BaseChannelObjectWithProperties : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x060037AC RID: 14252 RVA: 0x000C8EA1 File Offset: 0x000C70A1
		protected BaseChannelObjectWithProperties()
		{
			this.table = new Hashtable();
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060037AD RID: 14253 RVA: 0x000C8EB4 File Offset: 0x000C70B4
		public virtual int Count
		{
			get
			{
				return this.table.Count;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x00003FB7 File Offset: 0x000021B7
		public virtual bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060037AF RID: 14255 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060037B0 RID: 14256 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007E1 RID: 2017
		public virtual object this[object key]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060037B3 RID: 14259 RVA: 0x000C8EC1 File Offset: 0x000C70C1
		public virtual ICollection Keys
		{
			get
			{
				return this.table.Keys;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060037B4 RID: 14260 RVA: 0x000025CE File Offset: 0x000007CE
		public virtual IDictionary Properties
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060037B5 RID: 14261 RVA: 0x000025CE File Offset: 0x000007CE
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060037B6 RID: 14262 RVA: 0x000C8ECE File Offset: 0x000C70CE
		public virtual ICollection Values
		{
			get
			{
				return this.table.Values;
			}
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x00047E00 File Offset: 0x00046000
		public virtual void Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x00047E00 File Offset: 0x00046000
		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x000C8EDB File Offset: 0x000C70DB
		public virtual bool Contains(object key)
		{
			return this.table.Contains(key);
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x00047E00 File Offset: 0x00046000
		public virtual void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x000C8EE9 File Offset: 0x000C70E9
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return this.table.GetEnumerator();
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x000C8EE9 File Offset: 0x000C70E9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.table.GetEnumerator();
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x00047E00 File Offset: 0x00046000
		public virtual void Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04002551 RID: 9553
		private Hashtable table;
	}
}
