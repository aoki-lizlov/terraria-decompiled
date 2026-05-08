using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000089 RID: 137
	[CallbackIdentity(5301)]
	[StructLayout(0, Pack = 4)]
	public struct JoinPartyCallback_t
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0000C31B File Offset: 0x0000A51B
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x0000C328 File Offset: 0x0000A528
		public string m_rgchConnectString
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchConnectString_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchConnectString_, 256);
			}
		}

		// Token: 0x04000189 RID: 393
		public const int k_iCallback = 5301;

		// Token: 0x0400018A RID: 394
		public EResult m_eResult;

		// Token: 0x0400018B RID: 395
		public PartyBeaconID_t m_ulBeaconID;

		// Token: 0x0400018C RID: 396
		public CSteamID m_SteamIDBeaconOwner;

		// Token: 0x0400018D RID: 397
		[MarshalAs(30, SizeConst = 256)]
		private byte[] m_rgchConnectString_;
	}
}
