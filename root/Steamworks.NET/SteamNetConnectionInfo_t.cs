using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017C RID: 380
	[StructLayout(0, Pack = 4)]
	public struct SteamNetConnectionInfo_t
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0000C5EB File Offset: 0x0000A7EB
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x0000C5F8 File Offset: 0x0000A7F8
		public string m_szEndDebug
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_szEndDebug_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_szEndDebug_, 128);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0000C60B File Offset: 0x0000A80B
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x0000C618 File Offset: 0x0000A818
		public string m_szConnectionDescription
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_szConnectionDescription_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_szConnectionDescription_, 128);
			}
		}

		// Token: 0x04000A27 RID: 2599
		public SteamNetworkingIdentity m_identityRemote;

		// Token: 0x04000A28 RID: 2600
		public long m_nUserData;

		// Token: 0x04000A29 RID: 2601
		public HSteamListenSocket m_hListenSocket;

		// Token: 0x04000A2A RID: 2602
		public SteamNetworkingIPAddr m_addrRemote;

		// Token: 0x04000A2B RID: 2603
		public ushort m__pad1;

		// Token: 0x04000A2C RID: 2604
		public SteamNetworkingPOPID m_idPOPRemote;

		// Token: 0x04000A2D RID: 2605
		public SteamNetworkingPOPID m_idPOPRelay;

		// Token: 0x04000A2E RID: 2606
		public ESteamNetworkingConnectionState m_eState;

		// Token: 0x04000A2F RID: 2607
		public int m_eEndReason;

		// Token: 0x04000A30 RID: 2608
		[MarshalAs(30, SizeConst = 128)]
		private byte[] m_szEndDebug_;

		// Token: 0x04000A31 RID: 2609
		[MarshalAs(30, SizeConst = 128)]
		private byte[] m_szConnectionDescription_;

		// Token: 0x04000A32 RID: 2610
		public int m_nFlags;

		// Token: 0x04000A33 RID: 2611
		[MarshalAs(30, SizeConst = 63)]
		public uint[] reserved;
	}
}
