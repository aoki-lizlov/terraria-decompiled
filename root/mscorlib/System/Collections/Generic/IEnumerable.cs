using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AF1 RID: 2801
	public interface IEnumerable<out T> : IEnumerable
	{
		// Token: 0x0600672E RID: 26414
		IEnumerator<T> GetEnumerator();
	}
}
