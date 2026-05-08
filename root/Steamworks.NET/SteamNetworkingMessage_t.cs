using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B7 RID: 439
	[Serializable]
	public struct SteamNetworkingMessage_t
	{
		// Token: 0x06000AAE RID: 2734 RVA: 0x0000FEB5 File Offset: 0x0000E0B5
		public void Release()
		{
			throw new NotImplementedException("Please use the static Release function instead which takes an IntPtr.");
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0000FEC1 File Offset: 0x0000E0C1
		public static void Release(IntPtr pointer)
		{
			NativeMethods.SteamAPI_SteamNetworkingMessage_t_Release(pointer);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0000FEC9 File Offset: 0x0000E0C9
		public static SteamNetworkingMessage_t FromIntPtr(IntPtr pointer)
		{
			return (SteamNetworkingMessage_t)Marshal.PtrToStructure(pointer, typeof(SteamNetworkingMessage_t));
		}

		// Token: 0x04000B0F RID: 2831
		public IntPtr m_pData;

		// Token: 0x04000B10 RID: 2832
		public int m_cbSize;

		// Token: 0x04000B11 RID: 2833
		public HSteamNetConnection m_conn;

		// Token: 0x04000B12 RID: 2834
		public SteamNetworkingIdentity m_identityPeer;

		// Token: 0x04000B13 RID: 2835
		public long m_nConnUserData;

		// Token: 0x04000B14 RID: 2836
		public SteamNetworkingMicroseconds m_usecTimeReceived;

		// Token: 0x04000B15 RID: 2837
		public long m_nMessageNumber;

		// Token: 0x04000B16 RID: 2838
		public IntPtr m_pfnFreeData;

		// Token: 0x04000B17 RID: 2839
		internal IntPtr m_pfnRelease;

		// Token: 0x04000B18 RID: 2840
		public int m_nChannel;

		// Token: 0x04000B19 RID: 2841
		public int m_nFlags;

		// Token: 0x04000B1A RID: 2842
		public long m_nUserData;

		// Token: 0x04000B1B RID: 2843
		public ushort m_idxLane;

		// Token: 0x04000B1C RID: 2844
		public ushort _pad1__;
	}
}
