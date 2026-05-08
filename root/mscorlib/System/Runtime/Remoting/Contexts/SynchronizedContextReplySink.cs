using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000572 RID: 1394
	internal class SynchronizedContextReplySink : IMessageSink
	{
		// Token: 0x06003790 RID: 14224 RVA: 0x000C8B5C File Offset: 0x000C6D5C
		public SynchronizedContextReplySink(IMessageSink next, SynchronizationAttribute att, bool newLock)
		{
			this._newLock = newLock;
			this._next = next;
			this._att = att;
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06003791 RID: 14225 RVA: 0x000C8B79 File Offset: 0x000C6D79
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x00047E00 File Offset: 0x00046000
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x000C8B84 File Offset: 0x000C6D84
		public IMessage SyncProcessMessage(IMessage msg)
		{
			if (this._newLock)
			{
				this._att.AcquireLock();
			}
			else
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
				if (this._newLock)
				{
					this._att.ReleaseLock();
				}
			}
			return message;
		}

		// Token: 0x04002549 RID: 9545
		private IMessageSink _next;

		// Token: 0x0400254A RID: 9546
		private bool _newLock;

		// Token: 0x0400254B RID: 9547
		private SynchronizationAttribute _att;
	}
}
