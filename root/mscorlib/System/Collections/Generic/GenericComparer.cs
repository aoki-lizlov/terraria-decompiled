using System;

namespace System.Collections.Generic
{
	// Token: 0x02000B19 RID: 2841
	[Serializable]
	internal class GenericComparer<T> : Comparer<T> where T : IComparable<T>
	{
		// Token: 0x06006881 RID: 26753 RVA: 0x00162C91 File Offset: 0x00160E91
		public override int Compare(T x, T y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.CompareTo(y);
				}
				return 1;
			}
			else
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x06006882 RID: 26754 RVA: 0x00162CBF File Offset: 0x00160EBF
		public override bool Equals(object obj)
		{
			return obj is GenericComparer<T>;
		}

		// Token: 0x06006883 RID: 26755 RVA: 0x00162CCA File Offset: 0x00160ECA
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x06006884 RID: 26756 RVA: 0x00162CDC File Offset: 0x00160EDC
		public GenericComparer()
		{
		}
	}
}
