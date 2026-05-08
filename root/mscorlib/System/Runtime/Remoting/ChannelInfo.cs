using System;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting
{
	// Token: 0x02000528 RID: 1320
	[Serializable]
	internal class ChannelInfo : IChannelInfo
	{
		// Token: 0x06003565 RID: 13669 RVA: 0x000C1C0D File Offset: 0x000BFE0D
		public ChannelInfo()
		{
			this.channelData = ChannelServices.GetCurrentChannelInfo();
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x000C1C20 File Offset: 0x000BFE20
		public ChannelInfo(object remoteChannelData)
		{
			this.channelData = new object[] { remoteChannelData };
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06003567 RID: 13671 RVA: 0x000C1C38 File Offset: 0x000BFE38
		// (set) Token: 0x06003568 RID: 13672 RVA: 0x000C1C40 File Offset: 0x000BFE40
		public object[] ChannelData
		{
			get
			{
				return this.channelData;
			}
			set
			{
				this.channelData = value;
			}
		}

		// Token: 0x04002497 RID: 9367
		private object[] channelData;
	}
}
