using System;

namespace System.Collections
{
	// Token: 0x02000A73 RID: 2675
	public interface IStructuralEquatable
	{
		// Token: 0x060061AC RID: 25004
		bool Equals(object other, IEqualityComparer comparer);

		// Token: 0x060061AD RID: 25005
		int GetHashCode(IEqualityComparer comparer);
	}
}
