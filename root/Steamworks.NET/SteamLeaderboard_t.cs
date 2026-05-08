using System;

namespace Steamworks
{
	// Token: 0x020001CC RID: 460
	[Serializable]
	public struct SteamLeaderboard_t : IEquatable<SteamLeaderboard_t>, IComparable<SteamLeaderboard_t>
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x00010ACF File Offset: 0x0000ECCF
		public SteamLeaderboard_t(ulong value)
		{
			this.m_SteamLeaderboard = value;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00010AD8 File Offset: 0x0000ECD8
		public override string ToString()
		{
			return this.m_SteamLeaderboard.ToString();
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00010AE5 File Offset: 0x0000ECE5
		public override bool Equals(object other)
		{
			return other is SteamLeaderboard_t && this == (SteamLeaderboard_t)other;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00010B02 File Offset: 0x0000ED02
		public override int GetHashCode()
		{
			return this.m_SteamLeaderboard.GetHashCode();
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00010B0F File Offset: 0x0000ED0F
		public static bool operator ==(SteamLeaderboard_t x, SteamLeaderboard_t y)
		{
			return x.m_SteamLeaderboard == y.m_SteamLeaderboard;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00010B1F File Offset: 0x0000ED1F
		public static bool operator !=(SteamLeaderboard_t x, SteamLeaderboard_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00010B2B File Offset: 0x0000ED2B
		public static explicit operator SteamLeaderboard_t(ulong value)
		{
			return new SteamLeaderboard_t(value);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00010B33 File Offset: 0x0000ED33
		public static explicit operator ulong(SteamLeaderboard_t that)
		{
			return that.m_SteamLeaderboard;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00010B0F File Offset: 0x0000ED0F
		public bool Equals(SteamLeaderboard_t other)
		{
			return this.m_SteamLeaderboard == other.m_SteamLeaderboard;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00010B3B File Offset: 0x0000ED3B
		public int CompareTo(SteamLeaderboard_t other)
		{
			return this.m_SteamLeaderboard.CompareTo(other.m_SteamLeaderboard);
		}

		// Token: 0x04000B3F RID: 2879
		public ulong m_SteamLeaderboard;
	}
}
