using System;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005E3 RID: 1507
	internal class ClientContextReplySink : IMessageSink
	{
		// Token: 0x06003A4D RID: 14925 RVA: 0x000CD8AB File Offset: 0x000CBAAB
		public ClientContextReplySink(Context ctx, IMessageSink replySink)
		{
			this._replySink = replySink;
			this._context = ctx;
		}

		// Token: 0x06003A4E RID: 14926 RVA: 0x000CD8C1 File Offset: 0x000CBAC1
		public IMessage SyncProcessMessage(IMessage msg)
		{
			Context.NotifyGlobalDynamicSinks(false, msg, true, true);
			this._context.NotifyDynamicSinks(false, msg, true, true);
			return this._replySink.SyncProcessMessage(msg);
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x00047E00 File Offset: 0x00046000
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06003A50 RID: 14928 RVA: 0x000CD8E7 File Offset: 0x000CBAE7
		public IMessageSink NextSink
		{
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x04002606 RID: 9734
		private IMessageSink _replySink;

		// Token: 0x04002607 RID: 9735
		private Context _context;
	}
}
