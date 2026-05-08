using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000544 RID: 1348
	internal class DisposerReplySink : IMessageSink
	{
		// Token: 0x06003663 RID: 13923 RVA: 0x000C57A1 File Offset: 0x000C39A1
		public DisposerReplySink(IMessageSink next, IDisposable disposable)
		{
			this._next = next;
			this._disposable = disposable;
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000C57B7 File Offset: 0x000C39B7
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this._disposable.Dispose();
			return this._next.SyncProcessMessage(msg);
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x00047E00 File Offset: 0x00046000
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06003666 RID: 13926 RVA: 0x000C57D0 File Offset: 0x000C39D0
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x040024E4 RID: 9444
		private IMessageSink _next;

		// Token: 0x040024E5 RID: 9445
		private IDisposable _disposable;
	}
}
