using System;
using System.Collections;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000598 RID: 1432
	internal class ServerDispatchSinkProvider : IServerFormatterSinkProvider, IServerChannelSinkProvider
	{
		// Token: 0x06003846 RID: 14406 RVA: 0x000025BE File Offset: 0x000007BE
		public ServerDispatchSinkProvider()
		{
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x000025BE File Offset: 0x000007BE
		public ServerDispatchSinkProvider(IDictionary properties, ICollection providerData)
		{
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06003848 RID: 14408 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		// (set) Token: 0x06003849 RID: 14409 RVA: 0x00047E00 File Offset: 0x00046000
		public IServerChannelSinkProvider Next
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x000CA275 File Offset: 0x000C8475
		public IServerChannelSink CreateSink(IChannelReceiver channel)
		{
			return new ServerDispatchSink();
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x00004088 File Offset: 0x00002288
		public void GetChannelData(IChannelDataStore channelData)
		{
		}
	}
}
