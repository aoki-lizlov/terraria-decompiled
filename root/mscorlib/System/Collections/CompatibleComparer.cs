using System;

namespace System.Collections
{
	// Token: 0x02000A79 RID: 2681
	[Serializable]
	internal sealed class CompatibleComparer : IEqualityComparer
	{
		// Token: 0x060061D1 RID: 25041 RVA: 0x0014E381 File Offset: 0x0014C581
		internal CompatibleComparer(IHashCodeProvider hashCodeProvider, IComparer comparer)
		{
			this._hcp = hashCodeProvider;
			this._comparer = comparer;
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x060061D2 RID: 25042 RVA: 0x0014E397 File Offset: 0x0014C597
		internal IHashCodeProvider HashCodeProvider
		{
			get
			{
				return this._hcp;
			}
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x060061D3 RID: 25043 RVA: 0x0014E39F File Offset: 0x0014C59F
		internal IComparer Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x060061D4 RID: 25044 RVA: 0x0014E3A7 File Offset: 0x0014C5A7
		public bool Equals(object a, object b)
		{
			return this.Compare(a, b) == 0;
		}

		// Token: 0x060061D5 RID: 25045 RVA: 0x0014E3B4 File Offset: 0x0014C5B4
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			if (this._comparer != null)
			{
				return this._comparer.Compare(a, b);
			}
			IComparable comparable = a as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(b);
			}
			throw new ArgumentException("At least one object must implement IComparable.");
		}

		// Token: 0x060061D6 RID: 25046 RVA: 0x0014E403 File Offset: 0x0014C603
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._hcp == null)
			{
				return obj.GetHashCode();
			}
			return this._hcp.GetHashCode(obj);
		}

		// Token: 0x04003AAD RID: 15021
		private readonly IHashCodeProvider _hcp;

		// Token: 0x04003AAE RID: 15022
		private readonly IComparer _comparer;
	}
}
