using System;

namespace System.Threading
{
	// Token: 0x02000262 RID: 610
	public sealed class AutoResetEvent : EventWaitHandle
	{
		// Token: 0x06001D56 RID: 7510 RVA: 0x0006EF44 File Offset: 0x0006D144
		public AutoResetEvent(bool initialState)
			: base(initialState, EventResetMode.AutoReset)
		{
		}
	}
}
