using System;

namespace Steamworks
{
	// Token: 0x020001B1 RID: 433
	[Serializable]
	public struct HSteamNetConnection : IEquatable<HSteamNetConnection>, IComparable<HSteamNetConnection>
	{
		// Token: 0x06000A70 RID: 2672 RVA: 0x0000FB09 File Offset: 0x0000DD09
		public HSteamNetConnection(uint value)
		{
			this.m_HSteamNetConnection = value;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0000FB12 File Offset: 0x0000DD12
		public override string ToString()
		{
			return this.m_HSteamNetConnection.ToString();
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0000FB1F File Offset: 0x0000DD1F
		public override bool Equals(object other)
		{
			return other is HSteamNetConnection && this == (HSteamNetConnection)other;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0000FB3C File Offset: 0x0000DD3C
		public override int GetHashCode()
		{
			return this.m_HSteamNetConnection.GetHashCode();
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0000FB49 File Offset: 0x0000DD49
		public static bool operator ==(HSteamNetConnection x, HSteamNetConnection y)
		{
			return x.m_HSteamNetConnection == y.m_HSteamNetConnection;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0000FB59 File Offset: 0x0000DD59
		public static bool operator !=(HSteamNetConnection x, HSteamNetConnection y)
		{
			return !(x == y);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0000FB65 File Offset: 0x0000DD65
		public static explicit operator HSteamNetConnection(uint value)
		{
			return new HSteamNetConnection(value);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0000FB6D File Offset: 0x0000DD6D
		public static explicit operator uint(HSteamNetConnection that)
		{
			return that.m_HSteamNetConnection;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0000FB49 File Offset: 0x0000DD49
		public bool Equals(HSteamNetConnection other)
		{
			return this.m_HSteamNetConnection == other.m_HSteamNetConnection;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0000FB75 File Offset: 0x0000DD75
		public int CompareTo(HSteamNetConnection other)
		{
			return this.m_HSteamNetConnection.CompareTo(other.m_HSteamNetConnection);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0000FB88 File Offset: 0x0000DD88
		// Note: this type is marked as 'beforefieldinit'.
		static HSteamNetConnection()
		{
		}

		// Token: 0x04000ADE RID: 2782
		public static readonly HSteamNetConnection Invalid = new HSteamNetConnection(0U);

		// Token: 0x04000ADF RID: 2783
		public uint m_HSteamNetConnection;
	}
}
