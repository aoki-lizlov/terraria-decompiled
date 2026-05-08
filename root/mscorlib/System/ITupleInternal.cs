using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x0200016D RID: 365
	internal interface ITupleInternal : ITuple
	{
		// Token: 0x06001020 RID: 4128
		string ToString(StringBuilder sb);

		// Token: 0x06001021 RID: 4129
		int GetHashCode(IEqualityComparer comparer);
	}
}
