using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000570 RID: 1392
	internal class SynchronizedClientContextSink : IMessageSink
	{
		// Token: 0x06003788 RID: 14216 RVA: 0x000C8A1A File Offset: 0x000C6C1A
		public SynchronizedClientContextSink(IMessageSink next, SynchronizationAttribute att)
		{
			this._att = att;
			this._next = next;
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06003789 RID: 14217 RVA: 0x000C8A30 File Offset: 0x000C6C30
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x000C8A38 File Offset: 0x000C6C38
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._att.IsReEntrant)
			{
				this._att.ReleaseLock();
				replySink = new SynchronizedContextReplySink(replySink, this._att, true);
			}
			return this._next.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x000C8A70 File Offset: 0x000C6C70
		public IMessage SyncProcessMessage(IMessage msg)
		{
			if (this._att.IsReEntrant)
			{
				this._att.ReleaseLock();
			}
			IMessage message;
			try
			{
				message = this._next.SyncProcessMessage(msg);
			}
			finally
			{
				if (this._att.IsReEntrant)
				{
					this._att.AcquireLock();
				}
			}
			return message;
		}

		// Token: 0x04002545 RID: 9541
		private IMessageSink _next;

		// Token: 0x04002546 RID: 9542
		private SynchronizationAttribute _att;
	}
}
