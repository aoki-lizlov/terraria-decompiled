using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200057A RID: 1402
	internal class ExceptionFilterSink : IMessageSink
	{
		// Token: 0x060037DC RID: 14300 RVA: 0x000C9BA0 File Offset: 0x000C7DA0
		public ExceptionFilterSink(IMessage call, IMessageSink next)
		{
			this._call = call;
			this._next = next;
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000C9BB6 File Offset: 0x000C7DB6
		public IMessage SyncProcessMessage(IMessage msg)
		{
			return this._next.SyncProcessMessage(ChannelServices.CheckReturnMessage(this._call, msg));
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x00084CDD File Offset: 0x00082EDD
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060037DF RID: 14303 RVA: 0x000C9BCF File Offset: 0x000C7DCF
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x0400255A RID: 9562
		private IMessageSink _next;

		// Token: 0x0400255B RID: 9563
		private IMessage _call;
	}
}
