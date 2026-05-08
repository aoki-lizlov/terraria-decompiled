using System;

namespace Steamworks
{
	// Token: 0x020001CB RID: 459
	[Serializable]
	public struct SteamLeaderboardEntries_t : IEquatable<SteamLeaderboardEntries_t>, IComparable<SteamLeaderboardEntries_t>
	{
		// Token: 0x06000B76 RID: 2934 RVA: 0x00010A50 File Offset: 0x0000EC50
		public SteamLeaderboardEntries_t(ulong value)
		{
			this.m_SteamLeaderboardEntries = value;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00010A59 File Offset: 0x0000EC59
		public override string ToString()
		{
			return this.m_SteamLeaderboardEntries.ToString();
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00010A66 File Offset: 0x0000EC66
		public override bool Equals(object other)
		{
			return other is SteamLeaderboardEntries_t && this == (SteamLeaderboardEntries_t)other;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00010A83 File Offset: 0x0000EC83
		public override int GetHashCode()
		{
			return this.m_SteamLeaderboardEntries.GetHashCode();
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00010A90 File Offset: 0x0000EC90
		public static bool operator ==(SteamLeaderboardEntries_t x, SteamLeaderboardEntries_t y)
		{
			return x.m_SteamLeaderboardEntries == y.m_SteamLeaderboardEntries;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00010AA0 File Offset: 0x0000ECA0
		public static bool operator !=(SteamLeaderboardEntries_t x, SteamLeaderboardEntries_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00010AAC File Offset: 0x0000ECAC
		public static explicit operator SteamLeaderboardEntries_t(ulong value)
		{
			return new SteamLeaderboardEntries_t(value);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00010AB4 File Offset: 0x0000ECB4
		public static explicit operator ulong(SteamLeaderboardEntries_t that)
		{
			return that.m_SteamLeaderboardEntries;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00010A90 File Offset: 0x0000EC90
		public bool Equals(SteamLeaderboardEntries_t other)
		{
			return this.m_SteamLeaderboardEntries == other.m_SteamLeaderboardEntries;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00010ABC File Offset: 0x0000ECBC
		public int CompareTo(SteamLeaderboardEntries_t other)
		{
			return this.m_SteamLeaderboardEntries.CompareTo(other.m_SteamLeaderboardEntries);
		}

		// Token: 0x04000B3E RID: 2878
		public ulong m_SteamLeaderboardEntries;
	}
}
