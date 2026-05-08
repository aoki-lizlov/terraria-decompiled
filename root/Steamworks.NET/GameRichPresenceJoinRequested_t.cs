using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000035 RID: 53
	[CallbackIdentity(337)]
	[StructLayout(0, Pack = 4)]
	public struct GameRichPresenceJoinRequested_t
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x0000C27F File Offset: 0x0000A47F
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x0000C28C File Offset: 0x0000A48C
		public string m_rgchConnect
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchConnect_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchConnect_, 256);
			}
		}

		// Token: 0x0400002F RID: 47
		public const int k_iCallback = 337;

		// Token: 0x04000030 RID: 48
		public CSteamID m_steamIDFriend;

		// Token: 0x04000031 RID: 49
		[MarshalAs(30, SizeConst = 256)]
		private byte[] m_rgchConnect_;
	}
}
