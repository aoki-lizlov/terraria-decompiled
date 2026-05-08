using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A6 RID: 166
	[CallbackIdentity(1281)]
	public struct SteamRelayNetworkStatus_t
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0000C35B File Offset: 0x0000A55B
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x0000C368 File Offset: 0x0000A568
		public string m_debugMsg
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_debugMsg_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_debugMsg_, 256);
			}
		}

		// Token: 0x040001C4 RID: 452
		public const int k_iCallback = 1281;

		// Token: 0x040001C5 RID: 453
		public ESteamNetworkingAvailability m_eAvail;

		// Token: 0x040001C6 RID: 454
		public int m_bPingMeasurementInProgress;

		// Token: 0x040001C7 RID: 455
		public ESteamNetworkingAvailability m_eAvailNetworkConfig;

		// Token: 0x040001C8 RID: 456
		public ESteamNetworkingAvailability m_eAvailAnyRelay;

		// Token: 0x040001C9 RID: 457
		[MarshalAs(30, SizeConst = 256)]
		private byte[] m_debugMsg_;
	}
}
