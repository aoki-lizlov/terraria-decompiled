using System;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
	// Token: 0x02000AE9 RID: 2793
	public interface IAsyncEnumerator<out T> : IAsyncDisposable
	{
		// Token: 0x06006714 RID: 26388
		ValueTask<bool> MoveNextAsync();

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06006715 RID: 26389
		T Current { get; }
	}
}
