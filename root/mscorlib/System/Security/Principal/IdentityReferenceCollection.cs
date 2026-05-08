using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x020004B6 RID: 1206
	[ComVisible(false)]
	public class IdentityReferenceCollection : IEnumerable, ICollection<IdentityReference>, IEnumerable<IdentityReference>
	{
		// Token: 0x060031A5 RID: 12709 RVA: 0x000B7718 File Offset: 0x000B5918
		public IdentityReferenceCollection()
		{
			this._list = new ArrayList();
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000B772B File Offset: 0x000B592B
		public IdentityReferenceCollection(int capacity)
		{
			this._list = new ArrayList(capacity);
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060031A7 RID: 12711 RVA: 0x000B773F File Offset: 0x000B593F
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060031A8 RID: 12712 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006A5 RID: 1701
		public IdentityReference this[int index]
		{
			get
			{
				if (index >= this._list.Count)
				{
					return null;
				}
				return (IdentityReference)this._list[index];
			}
			set
			{
				this._list[index] = value;
			}
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000B777E File Offset: 0x000B597E
		public void Add(IdentityReference identity)
		{
			this._list.Add(identity);
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x000B778D File Offset: 0x000B598D
		public void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000B779C File Offset: 0x000B599C
		public bool Contains(IdentityReference identity)
		{
			using (IEnumerator enumerator = this._list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((IdentityReference)enumerator.Current).Equals(identity))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000174FB File Offset: 0x000156FB
		public void CopyTo(IdentityReference[] array, int offset)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000174FB File Offset: 0x000156FB
		public IEnumerator<IdentityReference> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000174FB File Offset: 0x000156FB
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000B77FC File Offset: 0x000B59FC
		public bool Remove(IdentityReference identity)
		{
			foreach (object obj in this._list)
			{
				IdentityReference identityReference = (IdentityReference)obj;
				if (identityReference.Equals(identity))
				{
					this._list.Remove(identityReference);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000174FB File Offset: 0x000156FB
		public IdentityReferenceCollection Translate(Type targetType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000174FB File Offset: 0x000156FB
		public IdentityReferenceCollection Translate(Type targetType, bool forceSuccess)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002245 RID: 8773
		private ArrayList _list;
	}
}
