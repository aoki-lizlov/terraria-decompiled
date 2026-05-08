using System;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000607 RID: 1543
	internal class ServerContextTerminatorSink : IMessageSink
	{
		// Token: 0x06003BA6 RID: 15270 RVA: 0x000D07D3 File Offset: 0x000CE9D3
		public IMessage SyncProcessMessage(IMessage msg)
		{
			if (msg is IConstructionCallMessage)
			{
				return ActivationServices.CreateInstanceFromMessage((IConstructionCallMessage)msg);
			}
			return ((ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg)).SyncObjectProcessMessage(msg);
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x000D07FA File Offset: 0x000CE9FA
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			return ((ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg)).AsyncObjectProcessMessage(msg, replySink);
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06003BA8 RID: 15272 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x000025BE File Offset: 0x000007BE
		public ServerContextTerminatorSink()
		{
		}
	}
}
