using System;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005E7 RID: 1511
	[Serializable]
	internal class EnvoyTerminatorSink : IMessageSink
	{
		// Token: 0x06003A6D RID: 14957 RVA: 0x000CDCF1 File Offset: 0x000CBEF1
		public IMessage SyncProcessMessage(IMessage msg)
		{
			return Thread.CurrentContext.GetClientContextSinkChain().SyncProcessMessage(msg);
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x000CDD03 File Offset: 0x000CBF03
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			return Thread.CurrentContext.GetClientContextSinkChain().AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06003A6F RID: 14959 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x000025BE File Offset: 0x000007BE
		public EnvoyTerminatorSink()
		{
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x000CDD16 File Offset: 0x000CBF16
		// Note: this type is marked as 'beforefieldinit'.
		static EnvoyTerminatorSink()
		{
		}

		// Token: 0x04002610 RID: 9744
		public static EnvoyTerminatorSink Instance = new EnvoyTerminatorSink();
	}
}
