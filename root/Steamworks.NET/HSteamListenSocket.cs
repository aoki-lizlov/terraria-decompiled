using System;

namespace Steamworks
{
	// Token: 0x020001B0 RID: 432
	[Serializable]
	public struct HSteamListenSocket : IEquatable<HSteamListenSocket>, IComparable<HSteamListenSocket>
	{
		// Token: 0x06000A65 RID: 2661 RVA: 0x0000FA7D File Offset: 0x0000DC7D
		public HSteamListenSocket(uint value)
		{
			this.m_HSteamListenSocket = value;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0000FA86 File Offset: 0x0000DC86
		public override string ToString()
		{
			return this.m_HSteamListenSocket.ToString();
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0000FA93 File Offset: 0x0000DC93
		public override bool Equals(object other)
		{
			return other is HSteamListenSocket && this == (HSteamListenSocket)other;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0000FAB0 File Offset: 0x0000DCB0
		public override int GetHashCode()
		{
			return this.m_HSteamListenSocket.GetHashCode();
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0000FABD File Offset: 0x0000DCBD
		public static bool operator ==(HSteamListenSocket x, HSteamListenSocket y)
		{
			return x.m_HSteamListenSocket == y.m_HSteamListenSocket;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0000FACD File Offset: 0x0000DCCD
		public static bool operator !=(HSteamListenSocket x, HSteamListenSocket y)
		{
			return !(x == y);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0000FAD9 File Offset: 0x0000DCD9
		public static explicit operator HSteamListenSocket(uint value)
		{
			return new HSteamListenSocket(value);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0000FAE1 File Offset: 0x0000DCE1
		public static explicit operator uint(HSteamListenSocket that)
		{
			return that.m_HSteamListenSocket;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0000FABD File Offset: 0x0000DCBD
		public bool Equals(HSteamListenSocket other)
		{
			return this.m_HSteamListenSocket == other.m_HSteamListenSocket;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0000FAE9 File Offset: 0x0000DCE9
		public int CompareTo(HSteamListenSocket other)
		{
			return this.m_HSteamListenSocket.CompareTo(other.m_HSteamListenSocket);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0000FAFC File Offset: 0x0000DCFC
		// Note: this type is marked as 'beforefieldinit'.
		static HSteamListenSocket()
		{
		}

		// Token: 0x04000ADC RID: 2780
		public static readonly HSteamListenSocket Invalid = new HSteamListenSocket(0U);

		// Token: 0x04000ADD RID: 2781
		public uint m_HSteamListenSocket;
	}
}
