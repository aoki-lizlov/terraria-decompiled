using System;

namespace System.Collections.Generic
{
	// Token: 0x02000B1C RID: 2844
	[Serializable]
	internal class ComparisonComparer<T> : Comparer<T>
	{
		// Token: 0x0600688D RID: 26765 RVA: 0x00162D55 File Offset: 0x00160F55
		public ComparisonComparer(Comparison<T> comparison)
		{
			this._comparison = comparison;
		}

		// Token: 0x0600688E RID: 26766 RVA: 0x00162D64 File Offset: 0x00160F64
		public override int Compare(T x, T y)
		{
			return this._comparison(x, y);
		}

		// Token: 0x04003C62 RID: 15458
		private readonly Comparison<T> _comparison;
	}
}
