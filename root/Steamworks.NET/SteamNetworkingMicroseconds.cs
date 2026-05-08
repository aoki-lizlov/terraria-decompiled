using System;

namespace Steamworks
{
	// Token: 0x020001B8 RID: 440
	[Serializable]
	public struct SteamNetworkingMicroseconds : IEquatable<SteamNetworkingMicroseconds>, IComparable<SteamNetworkingMicroseconds>
	{
		// Token: 0x06000AB1 RID: 2737 RVA: 0x0000FEE0 File Offset: 0x0000E0E0
		public SteamNetworkingMicroseconds(long value)
		{
			this.m_SteamNetworkingMicroseconds = value;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0000FEE9 File Offset: 0x0000E0E9
		public override string ToString()
		{
			return this.m_SteamNetworkingMicroseconds.ToString();
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0000FEF6 File Offset: 0x0000E0F6
		public override bool Equals(object other)
		{
			return other is SteamNetworkingMicroseconds && this == (SteamNetworkingMicroseconds)other;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0000FF13 File Offset: 0x0000E113
		public override int GetHashCode()
		{
			return this.m_SteamNetworkingMicroseconds.GetHashCode();
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0000FF20 File Offset: 0x0000E120
		public static bool operator ==(SteamNetworkingMicroseconds x, SteamNetworkingMicroseconds y)
		{
			return x.m_SteamNetworkingMicroseconds == y.m_SteamNetworkingMicroseconds;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0000FF30 File Offset: 0x0000E130
		public static bool operator !=(SteamNetworkingMicroseconds x, SteamNetworkingMicroseconds y)
		{
			return !(x == y);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0000FF3C File Offset: 0x0000E13C
		public static explicit operator SteamNetworkingMicroseconds(long value)
		{
			return new SteamNetworkingMicroseconds(value);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0000FF44 File Offset: 0x0000E144
		public static explicit operator long(SteamNetworkingMicroseconds that)
		{
			return that.m_SteamNetworkingMicroseconds;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0000FF20 File Offset: 0x0000E120
		public bool Equals(SteamNetworkingMicroseconds other)
		{
			return this.m_SteamNetworkingMicroseconds == other.m_SteamNetworkingMicroseconds;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0000FF4C File Offset: 0x0000E14C
		public int CompareTo(SteamNetworkingMicroseconds other)
		{
			return this.m_SteamNetworkingMicroseconds.CompareTo(other.m_SteamNetworkingMicroseconds);
		}

		// Token: 0x04000B1D RID: 2845
		public long m_SteamNetworkingMicroseconds;
	}
}
