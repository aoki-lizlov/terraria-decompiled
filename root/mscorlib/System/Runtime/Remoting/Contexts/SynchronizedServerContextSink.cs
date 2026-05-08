using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000571 RID: 1393
	internal class SynchronizedServerContextSink : IMessageSink
	{
		// Token: 0x0600378C RID: 14220 RVA: 0x000C8AD0 File Offset: 0x000C6CD0
		public SynchronizedServerContextSink(IMessageSink next, SynchronizationAttribute att)
		{
			this._att = att;
			this._next = next;
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x0600378D RID: 14221 RVA: 0x000C8AE6 File Offset: 0x000C6CE6
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000C8AEE File Offset: 0x000C6CEE
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			this._att.AcquireLock();
			replySink = new SynchronizedContextReplySink(replySink, this._att, false);
			return this._next.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x000C8B18 File Offset: 0x000C6D18
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this._att.AcquireLock();
			IMessage message;
			try
			{
				message = this._next.SyncProcessMessage(msg);
			}
			finally
			{
				this._att.ReleaseLock();
			}
			return message;
		}

		// Token: 0x04002547 RID: 9543
		private IMessageSink _next;

		// Token: 0x04002548 RID: 9544
		private SynchronizationAttribute _att;
	}
}
