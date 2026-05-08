using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000417 RID: 1047
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntryCollection : ICollection, IEnumerable
	{
		// Token: 0x06002C0F RID: 11279 RVA: 0x0009FBA9 File Offset: 0x0009DDA9
		internal KeyContainerPermissionAccessEntryCollection()
		{
			this._list = new ArrayList();
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x0009FBBC File Offset: 0x0009DDBC
		internal KeyContainerPermissionAccessEntryCollection(KeyContainerPermissionAccessEntry[] entries)
		{
			if (entries != null)
			{
				foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in entries)
				{
					this.Add(keyContainerPermissionAccessEntry);
				}
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06002C11 RID: 11281 RVA: 0x0009FBEE File Offset: 0x0009DDEE
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06002C12 RID: 11282 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700059E RID: 1438
		public KeyContainerPermissionAccessEntry this[int index]
		{
			get
			{
				return (KeyContainerPermissionAccessEntry)this._list[index];
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x000025CE File Offset: 0x000007CE
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x0009FC0E File Offset: 0x0009DE0E
		public int Add(KeyContainerPermissionAccessEntry accessEntry)
		{
			return this._list.Add(accessEntry);
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x0009FC1C File Offset: 0x0009DE1C
		public void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x0009FC29 File Offset: 0x0009DE29
		public void CopyTo(KeyContainerPermissionAccessEntry[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x0009FC29 File Offset: 0x0009DE29
		void ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x0009FC38 File Offset: 0x0009DE38
		public KeyContainerPermissionAccessEntryEnumerator GetEnumerator()
		{
			return new KeyContainerPermissionAccessEntryEnumerator(this._list);
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x0009FC38 File Offset: 0x0009DE38
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new KeyContainerPermissionAccessEntryEnumerator(this._list);
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x0009FC48 File Offset: 0x0009DE48
		public int IndexOf(KeyContainerPermissionAccessEntry accessEntry)
		{
			if (accessEntry == null)
			{
				throw new ArgumentNullException("accessEntry");
			}
			for (int i = 0; i < this._list.Count; i++)
			{
				if (accessEntry.Equals(this._list[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x0009FC90 File Offset: 0x0009DE90
		public void Remove(KeyContainerPermissionAccessEntry accessEntry)
		{
			if (accessEntry == null)
			{
				throw new ArgumentNullException("accessEntry");
			}
			for (int i = 0; i < this._list.Count; i++)
			{
				if (accessEntry.Equals(this._list[i]))
				{
					this._list.RemoveAt(i);
				}
			}
		}

		// Token: 0x04001F1E RID: 7966
		private ArrayList _list;
	}
}
