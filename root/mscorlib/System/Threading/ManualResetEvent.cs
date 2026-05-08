using System;

namespace System.Threading
{
	// Token: 0x02000267 RID: 615
	public sealed class ManualResetEvent : EventWaitHandle
	{
		// Token: 0x06001D66 RID: 7526 RVA: 0x0006F1B6 File Offset: 0x0006D3B6
		public ManualResetEvent(bool initialState)
			: base(initialState, EventResetMode.ManualReset)
		{
		}
	}
}
