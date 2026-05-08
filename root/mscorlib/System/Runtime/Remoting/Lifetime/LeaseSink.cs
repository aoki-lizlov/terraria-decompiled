using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000559 RID: 1369
	internal class LeaseSink : IMessageSink
	{
		// Token: 0x06003712 RID: 14098 RVA: 0x000C79BF File Offset: 0x000C5BBF
		public LeaseSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x000C79CE File Offset: 0x000C5BCE
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this.RenewLease(msg);
			return this._nextSink.SyncProcessMessage(msg);
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x000C79E3 File Offset: 0x000C5BE3
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			this.RenewLease(msg);
			return this._nextSink.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x000C79FC File Offset: 0x000C5BFC
		private void RenewLease(IMessage msg)
		{
			ILease lease = ((ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg)).Lease;
			if (lease != null && lease.CurrentLeaseTime < lease.RenewOnCallTime)
			{
				lease.Renew(lease.RenewOnCallTime);
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06003716 RID: 14102 RVA: 0x000C7A3D File Offset: 0x000C5C3D
		public IMessageSink NextSink
		{
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x04002518 RID: 9496
		private IMessageSink _nextSink;
	}
}
