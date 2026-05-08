using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200019C RID: 412
	[Serializable]
	[StructLayout(0, Pack = 4)]
	public struct SteamDatagramRelayAuthTicket
	{
		// Token: 0x060009C4 RID: 2500 RVA: 0x0000D197 File Offset: 0x0000B397
		public void Clear()
		{
		}

		// Token: 0x04000AB8 RID: 2744
		private SteamNetworkingIdentity m_identityGameserver;

		// Token: 0x04000AB9 RID: 2745
		private SteamNetworkingIdentity m_identityAuthorizedClient;

		// Token: 0x04000ABA RID: 2746
		private uint m_unPublicIP;

		// Token: 0x04000ABB RID: 2747
		private RTime32 m_rtimeTicketExpiry;

		// Token: 0x04000ABC RID: 2748
		private SteamDatagramHostedAddress m_routing;

		// Token: 0x04000ABD RID: 2749
		private uint m_nAppID;

		// Token: 0x04000ABE RID: 2750
		private int m_nRestrictToVirtualPort;

		// Token: 0x04000ABF RID: 2751
		private const int k_nMaxExtraFields = 16;

		// Token: 0x04000AC0 RID: 2752
		private int m_nExtraFields;

		// Token: 0x04000AC1 RID: 2753
		[MarshalAs(30, SizeConst = 16)]
		private SteamDatagramRelayAuthTicket.ExtraField[] m_vecExtraFields;

		// Token: 0x020001F0 RID: 496
		[StructLayout(0, Pack = 4)]
		private struct ExtraField
		{
			// Token: 0x04000B5E RID: 2910
			private SteamDatagramRelayAuthTicket.ExtraField.EType m_eType;

			// Token: 0x04000B5F RID: 2911
			[MarshalAs(30, SizeConst = 28)]
			private byte[] m_szName;

			// Token: 0x04000B60 RID: 2912
			private SteamDatagramRelayAuthTicket.ExtraField.OptionValue m_val;

			// Token: 0x020001F5 RID: 501
			private enum EType
			{
				// Token: 0x04000B6D RID: 2925
				k_EType_String,
				// Token: 0x04000B6E RID: 2926
				k_EType_Int,
				// Token: 0x04000B6F RID: 2927
				k_EType_Fixed64
			}

			// Token: 0x020001F6 RID: 502
			[StructLayout(2)]
			private struct OptionValue
			{
				// Token: 0x04000B70 RID: 2928
				[FieldOffset(0)]
				[MarshalAs(30, SizeConst = 128)]
				private byte[] m_szStringValue;

				// Token: 0x04000B71 RID: 2929
				[FieldOffset(0)]
				private long m_nIntValue;

				// Token: 0x04000B72 RID: 2930
				[FieldOffset(0)]
				private ulong m_nFixed64Value;
			}
		}
	}
}
