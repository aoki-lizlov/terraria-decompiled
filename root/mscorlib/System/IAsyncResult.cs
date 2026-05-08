using System;
using System.Threading;

namespace System
{
	// Token: 0x020000FE RID: 254
	public interface IAsyncResult
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000A16 RID: 2582
		bool IsCompleted { get; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000A17 RID: 2583
		WaitHandle AsyncWaitHandle { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000A18 RID: 2584
		object AsyncState { get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000A19 RID: 2585
		bool CompletedSynchronously { get; }
	}
}
