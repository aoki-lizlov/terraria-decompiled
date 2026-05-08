using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AF3 RID: 2803
	public interface IEqualityComparer<in T>
	{
		// Token: 0x06006730 RID: 26416
		bool Equals(T x, T y);

		// Token: 0x06006731 RID: 26417
		int GetHashCode(T obj);
	}
}
