using System;

namespace System.Collections.Generic
{
	// Token: 0x02000B1A RID: 2842
	[Serializable]
	internal class NullableComparer<T> : Comparer<T?> where T : struct, IComparable<T>
	{
		// Token: 0x06006885 RID: 26757 RVA: 0x00162CE4 File Offset: 0x00160EE4
		public override int Compare(T? x, T? y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.value.CompareTo(y.value);
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

		// Token: 0x06006886 RID: 26758 RVA: 0x00162D1F File Offset: 0x00160F1F
		public override bool Equals(object obj)
		{
			return obj is NullableComparer<T>;
		}

		// Token: 0x06006887 RID: 26759 RVA: 0x00162CCA File Offset: 0x00160ECA
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x06006888 RID: 26760 RVA: 0x00162D2A File Offset: 0x00160F2A
		public NullableComparer()
		{
		}
	}
}
