using System;

namespace System.Collections
{
	// Token: 0x02000A70 RID: 2672
	public interface IEqualityComparer
	{
		// Token: 0x0600619E RID: 24990
		bool Equals(object x, object y);

		// Token: 0x0600619F RID: 24991
		int GetHashCode(object obj);
	}
}
