using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000183 RID: 387
	internal interface IValueTupleInternal : ITuple
	{
		// Token: 0x06001257 RID: 4695
		int GetHashCode(IEqualityComparer comparer);

		// Token: 0x06001258 RID: 4696
		string ToStringEnd();
	}
}
