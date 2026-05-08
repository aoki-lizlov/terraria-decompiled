using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200057B RID: 1403
	internal class ChanelSinkStackEntry
	{
		// Token: 0x060037E0 RID: 14304 RVA: 0x000C9BD7 File Offset: 0x000C7DD7
		public ChanelSinkStackEntry(IChannelSinkBase sink, object state, ChanelSinkStackEntry next)
		{
			this.Sink = sink;
			this.State = state;
			this.Next = next;
		}

		// Token: 0x0400255C RID: 9564
		public IChannelSinkBase Sink;

		// Token: 0x0400255D RID: 9565
		public object State;

		// Token: 0x0400255E RID: 9566
		public ChanelSinkStackEntry Next;
	}
}
