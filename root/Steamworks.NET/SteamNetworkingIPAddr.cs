using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B6 RID: 438
	[Serializable]
	[StructLayout(0, Pack = 1)]
	public struct SteamNetworkingIPAddr : IEquatable<SteamNetworkingIPAddr>
	{
		// Token: 0x06000AA1 RID: 2721 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
		public void Clear()
		{
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_Clear(ref this);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		public bool IsIPv6AllZeros()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsIPv6AllZeros(ref this);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0000FDF4 File Offset: 0x0000DFF4
		public void SetIPv6(byte[] ipv6, ushort nPort)
		{
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv6(ref this, ipv6, nPort);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0000FDFE File Offset: 0x0000DFFE
		public void SetIPv4(uint nIP, ushort nPort)
		{
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv4(ref this, nIP, nPort);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0000FE08 File Offset: 0x0000E008
		public bool IsIPv4()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsIPv4(ref this);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0000FE10 File Offset: 0x0000E010
		public uint GetIPv4()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_GetIPv4(ref this);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0000FE18 File Offset: 0x0000E018
		public void SetIPv6LocalHost(ushort nPort = 0)
		{
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv6LocalHost(ref this, nPort);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0000FE21 File Offset: 0x0000E021
		public bool IsLocalHost()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsLocalHost(ref this);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0000FE2C File Offset: 0x0000E02C
		public void ToString(out string buf, bool bWithPort)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(48);
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_ToString(ref this, intPtr, 48U, bWithPort);
			buf = InteropHelp.PtrToStringUTF8(intPtr);
			Marshal.FreeHGlobal(intPtr);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0000FE5C File Offset: 0x0000E05C
		public bool ParseString(string pszStr)
		{
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszStr))
			{
				flag = NativeMethods.SteamAPI_SteamNetworkingIPAddr_ParseString(ref this, utf8StringHandle);
			}
			return flag;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0000FE98 File Offset: 0x0000E098
		public bool Equals(SteamNetworkingIPAddr x)
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsEqualTo(ref this, ref x);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0000FEA2 File Offset: 0x0000E0A2
		public ESteamNetworkingFakeIPType GetFakeIPType()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_GetFakeIPType(ref this);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0000FEAA File Offset: 0x0000E0AA
		public bool IsFakeIP()
		{
			return this.GetFakeIPType() > ESteamNetworkingFakeIPType.k_ESteamNetworkingFakeIPType_NotFake;
		}

		// Token: 0x04000B0C RID: 2828
		[MarshalAs(30, SizeConst = 16)]
		public byte[] m_ipv6;

		// Token: 0x04000B0D RID: 2829
		public ushort m_port;

		// Token: 0x04000B0E RID: 2830
		public const int k_cchMaxString = 48;
	}
}
