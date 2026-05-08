using System;

namespace Steamworks
{
	// Token: 0x020001B9 RID: 441
	[Serializable]
	public struct SteamNetworkingPOPID : IEquatable<SteamNetworkingPOPID>, IComparable<SteamNetworkingPOPID>
	{
		// Token: 0x06000ABB RID: 2747 RVA: 0x0000FF5F File Offset: 0x0000E15F
		public SteamNetworkingPOPID(uint value)
		{
			this.m_SteamNetworkingPOPID = value;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0000FF68 File Offset: 0x0000E168
		public override string ToString()
		{
			return this.m_SteamNetworkingPOPID.ToString();
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0000FF75 File Offset: 0x0000E175
		public override bool Equals(object other)
		{
			return other is SteamNetworkingPOPID && this == (SteamNetworkingPOPID)other;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0000FF92 File Offset: 0x0000E192
		public override int GetHashCode()
		{
			return this.m_SteamNetworkingPOPID.GetHashCode();
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0000FF9F File Offset: 0x0000E19F
		public static bool operator ==(SteamNetworkingPOPID x, SteamNetworkingPOPID y)
		{
			return x.m_SteamNetworkingPOPID == y.m_SteamNetworkingPOPID;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0000FFAF File Offset: 0x0000E1AF
		public static bool operator !=(SteamNetworkingPOPID x, SteamNetworkingPOPID y)
		{
			return !(x == y);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0000FFBB File Offset: 0x0000E1BB
		public static explicit operator SteamNetworkingPOPID(uint value)
		{
			return new SteamNetworkingPOPID(value);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0000FFC3 File Offset: 0x0000E1C3
		public static explicit operator uint(SteamNetworkingPOPID that)
		{
			return that.m_SteamNetworkingPOPID;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0000FF9F File Offset: 0x0000E19F
		public bool Equals(SteamNetworkingPOPID other)
		{
			return this.m_SteamNetworkingPOPID == other.m_SteamNetworkingPOPID;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0000FFCB File Offset: 0x0000E1CB
		public int CompareTo(SteamNetworkingPOPID other)
		{
			return this.m_SteamNetworkingPOPID.CompareTo(other.m_SteamNetworkingPOPID);
		}

		// Token: 0x04000B1E RID: 2846
		public uint m_SteamNetworkingPOPID;
	}
}
