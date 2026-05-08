using System;
using System.Threading.Tasks;

namespace System
{
	// Token: 0x020000FD RID: 253
	public interface IAsyncDisposable
	{
		// Token: 0x06000A15 RID: 2581
		ValueTask DisposeAsync();
	}
}
