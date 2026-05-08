using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AEC RID: 2796
	public interface IComparer<in T>
	{
		// Token: 0x0600671F RID: 26399
		int Compare(T x, T y);
	}
}
