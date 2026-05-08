using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000609 RID: 1545
	internal class ServerObjectReplySink : IMessageSink
	{
		// Token: 0x06003BAE RID: 15278 RVA: 0x000D08B5 File Offset: 0x000CEAB5
		public ServerObjectReplySink(ServerIdentity identity, IMessageSink replySink)
		{
			this._replySink = replySink;
			this._identity = identity;
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x000D08CB File Offset: 0x000CEACB
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this._identity.NotifyServerDynamicSinks(false, msg, true, true);
			return this._replySink.SyncProcessMessage(msg);
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x00047E00 File Offset: 0x00046000
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06003BB1 RID: 15281 RVA: 0x000D08E8 File Offset: 0x000CEAE8
		public IMessageSink NextSink
		{
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x0400266E RID: 9838
		private IMessageSink _replySink;

		// Token: 0x0400266F RID: 9839
		private ServerIdentity _identity;
	}
}
