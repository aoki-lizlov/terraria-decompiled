using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B5 RID: 437
	[Serializable]
	[StructLayout(0, Pack = 1)]
	public struct SteamNetworkingIdentity : IEquatable<SteamNetworkingIdentity>
	{
		// Token: 0x06000A86 RID: 2694 RVA: 0x0000FC21 File Offset: 0x0000DE21
		public void Clear()
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_Clear(ref this);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0000FC29 File Offset: 0x0000DE29
		public bool IsInvalid()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsInvalid(ref this);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0000FC31 File Offset: 0x0000DE31
		public void SetSteamID(CSteamID steamID)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetSteamID(ref this, (ulong)steamID);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0000FC3F File Offset: 0x0000DE3F
		public CSteamID GetSteamID()
		{
			return (CSteamID)NativeMethods.SteamAPI_SteamNetworkingIdentity_GetSteamID(ref this);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0000FC4C File Offset: 0x0000DE4C
		public void SetSteamID64(ulong steamID)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetSteamID64(ref this, steamID);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0000FC55 File Offset: 0x0000DE55
		public ulong GetSteamID64()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetSteamID64(ref this);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0000FC60 File Offset: 0x0000DE60
		public bool SetXboxPairwiseID(string pszString)
		{
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszString))
			{
				flag = NativeMethods.SteamAPI_SteamNetworkingIdentity_SetXboxPairwiseID(ref this, utf8StringHandle);
			}
			return flag;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0000FC9C File Offset: 0x0000DE9C
		public string GetXboxPairwiseID()
		{
			return InteropHelp.PtrToStringUTF8(NativeMethods.SteamAPI_SteamNetworkingIdentity_GetXboxPairwiseID(ref this));
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0000FCA9 File Offset: 0x0000DEA9
		public void SetPSNID(ulong id)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetPSNID(ref this, id);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0000FCB2 File Offset: 0x0000DEB2
		public ulong GetPSNID()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetPSNID(ref this);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0000FCBA File Offset: 0x0000DEBA
		public void SetStadiaID(ulong id)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetStadiaID(ref this, id);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0000FCC3 File Offset: 0x0000DEC3
		public ulong GetStadiaID()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetStadiaID(ref this);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0000FCCB File Offset: 0x0000DECB
		public void SetIPAddr(SteamNetworkingIPAddr addr)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetIPAddr(ref this, ref addr);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0000FCD6 File Offset: 0x0000DED6
		public SteamNetworkingIPAddr GetIPAddr()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0000FCDD File Offset: 0x0000DEDD
		public void SetIPv4Addr(uint nIPv4, ushort nPort)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetIPv4Addr(ref this, nIPv4, nPort);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0000FCE7 File Offset: 0x0000DEE7
		public uint GetIPv4()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetIPv4(ref this);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0000FCEF File Offset: 0x0000DEEF
		public ESteamNetworkingFakeIPType GetFakeIPType()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetFakeIPType(ref this);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0000FCF7 File Offset: 0x0000DEF7
		public bool IsFakeIP()
		{
			return this.GetFakeIPType() > ESteamNetworkingFakeIPType.k_ESteamNetworkingFakeIPType_NotFake;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0000FD02 File Offset: 0x0000DF02
		public void SetLocalHost()
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetLocalHost(ref this);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0000FD0A File Offset: 0x0000DF0A
		public bool IsLocalHost()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsLocalHost(ref this);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0000FD14 File Offset: 0x0000DF14
		public bool SetGenericString(string pszString)
		{
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszString))
			{
				flag = NativeMethods.SteamAPI_SteamNetworkingIdentity_SetGenericString(ref this, utf8StringHandle);
			}
			return flag;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0000FD50 File Offset: 0x0000DF50
		public string GetGenericString()
		{
			return InteropHelp.PtrToStringUTF8(NativeMethods.SteamAPI_SteamNetworkingIdentity_GetGenericString(ref this));
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0000FD5D File Offset: 0x0000DF5D
		public bool SetGenericBytes(byte[] data, uint cbLen)
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_SetGenericBytes(ref this, data, cbLen);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0000FCD6 File Offset: 0x0000DED6
		public byte[] GetGenericBytes(out int cbLen)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0000FD67 File Offset: 0x0000DF67
		public bool Equals(SteamNetworkingIdentity x)
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsEqualTo(ref this, ref x);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0000FD74 File Offset: 0x0000DF74
		public void ToString(out string buf)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(128);
			NativeMethods.SteamAPI_SteamNetworkingIdentity_ToString(ref this, intPtr, 128U);
			buf = InteropHelp.PtrToStringUTF8(intPtr);
			Marshal.FreeHGlobal(intPtr);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0000FDA8 File Offset: 0x0000DFA8
		public bool ParseString(string pszStr)
		{
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszStr))
			{
				flag = NativeMethods.SteamAPI_SteamNetworkingIdentity_ParseString(ref this, utf8StringHandle);
			}
			return flag;
		}

		// Token: 0x04000AE6 RID: 2790
		public ESteamNetworkingIdentityType m_eType;

		// Token: 0x04000AE7 RID: 2791
		private int m_cbSize;

		// Token: 0x04000AE8 RID: 2792
		private uint m_reserved0;

		// Token: 0x04000AE9 RID: 2793
		private uint m_reserved1;

		// Token: 0x04000AEA RID: 2794
		private uint m_reserved2;

		// Token: 0x04000AEB RID: 2795
		private uint m_reserved3;

		// Token: 0x04000AEC RID: 2796
		private uint m_reserved4;

		// Token: 0x04000AED RID: 2797
		private uint m_reserved5;

		// Token: 0x04000AEE RID: 2798
		private uint m_reserved6;

		// Token: 0x04000AEF RID: 2799
		private uint m_reserved7;

		// Token: 0x04000AF0 RID: 2800
		private uint m_reserved8;

		// Token: 0x04000AF1 RID: 2801
		private uint m_reserved9;

		// Token: 0x04000AF2 RID: 2802
		private uint m_reserved10;

		// Token: 0x04000AF3 RID: 2803
		private uint m_reserved11;

		// Token: 0x04000AF4 RID: 2804
		private uint m_reserved12;

		// Token: 0x04000AF5 RID: 2805
		private uint m_reserved13;

		// Token: 0x04000AF6 RID: 2806
		private uint m_reserved14;

		// Token: 0x04000AF7 RID: 2807
		private uint m_reserved15;

		// Token: 0x04000AF8 RID: 2808
		private uint m_reserved16;

		// Token: 0x04000AF9 RID: 2809
		private uint m_reserved17;

		// Token: 0x04000AFA RID: 2810
		private uint m_reserved18;

		// Token: 0x04000AFB RID: 2811
		private uint m_reserved19;

		// Token: 0x04000AFC RID: 2812
		private uint m_reserved20;

		// Token: 0x04000AFD RID: 2813
		private uint m_reserved21;

		// Token: 0x04000AFE RID: 2814
		private uint m_reserved22;

		// Token: 0x04000AFF RID: 2815
		private uint m_reserved23;

		// Token: 0x04000B00 RID: 2816
		private uint m_reserved24;

		// Token: 0x04000B01 RID: 2817
		private uint m_reserved25;

		// Token: 0x04000B02 RID: 2818
		private uint m_reserved26;

		// Token: 0x04000B03 RID: 2819
		private uint m_reserved27;

		// Token: 0x04000B04 RID: 2820
		private uint m_reserved28;

		// Token: 0x04000B05 RID: 2821
		private uint m_reserved29;

		// Token: 0x04000B06 RID: 2822
		private uint m_reserved30;

		// Token: 0x04000B07 RID: 2823
		private uint m_reserved31;

		// Token: 0x04000B08 RID: 2824
		public const int k_cchMaxString = 128;

		// Token: 0x04000B09 RID: 2825
		public const int k_cchMaxGenericString = 32;

		// Token: 0x04000B0A RID: 2826
		public const int k_cchMaxXboxPairwiseID = 33;

		// Token: 0x04000B0B RID: 2827
		public const int k_cbMaxGenericBytes = 32;
	}
}
