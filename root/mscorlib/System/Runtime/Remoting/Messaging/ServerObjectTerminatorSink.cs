using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000608 RID: 1544
	internal class ServerObjectTerminatorSink : IMessageSink
	{
		// Token: 0x06003BAA RID: 15274 RVA: 0x000D080E File Offset: 0x000CEA0E
		public ServerObjectTerminatorSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x000D0820 File Offset: 0x000CEA20
		public IMessage SyncProcessMessage(IMessage msg)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			serverIdentity.NotifyServerDynamicSinks(true, msg, false, false);
			IMessage message = this._nextSink.SyncProcessMessage(msg);
			serverIdentity.NotifyServerDynamicSinks(false, msg, false, false);
			return message;
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x000D085C File Offset: 0x000CEA5C
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			if (serverIdentity.HasServerDynamicSinks)
			{
				serverIdentity.NotifyServerDynamicSinks(true, msg, false, true);
				if (replySink != null)
				{
					replySink = new ServerObjectReplySink(serverIdentity, replySink);
				}
			}
			IMessageCtrl messageCtrl = this._nextSink.AsyncProcessMessage(msg, replySink);
			if (replySink == null)
			{
				serverIdentity.NotifyServerDynamicSinks(false, msg, true, true);
			}
			return messageCtrl;
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06003BAD RID: 15277 RVA: 0x000D08AD File Offset: 0x000CEAAD
		public IMessageSink NextSink
		{
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x0400266D RID: 9837
		private IMessageSink _nextSink;
	}
}
