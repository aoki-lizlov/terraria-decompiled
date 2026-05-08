using System;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x02000AE8 RID: 2792
	public interface IAsyncEnumerable<out T>
	{
		// Token: 0x06006713 RID: 26387
		IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken));
	}
}
