using System;
using System.Collections.Generic;

namespace System.Collections
{
	// Token: 0x02000A92 RID: 2706
	[Serializable]
	internal sealed class StructuralComparer : IComparer
	{
		// Token: 0x06006308 RID: 25352 RVA: 0x00151DF4 File Offset: 0x0014FFF4
		public int Compare(object x, object y)
		{
			if (x == null)
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				IStructuralComparable structuralComparable = x as IStructuralComparable;
				if (structuralComparable != null)
				{
					return structuralComparable.CompareTo(y, this);
				}
				return Comparer<object>.Default.Compare(x, y);
			}
		}

		// Token: 0x06006309 RID: 25353 RVA: 0x000025BE File Offset: 0x000007BE
		public StructuralComparer()
		{
		}
	}
}
