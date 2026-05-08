using System;

namespace Steamworks
{
	// Token: 0x020001CE RID: 462
	[Serializable]
	public struct HSteamUser : IEquatable<HSteamUser>, IComparable<HSteamUser>
	{
		// Token: 0x06000B94 RID: 2964 RVA: 0x00010BCD File Offset: 0x0000EDCD
		public HSteamUser(int value)
		{
			this.m_HSteamUser = value;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00010BD6 File Offset: 0x0000EDD6
		public override string ToString()
		{
			return this.m_HSteamUser.ToString();
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00010BE3 File Offset: 0x0000EDE3
		public override bool Equals(object other)
		{
			return other is HSteamUser && this == (HSteamUser)other;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00010C00 File Offset: 0x0000EE00
		public override int GetHashCode()
		{
			return this.m_HSteamUser.GetHashCode();
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00010C0D File Offset: 0x0000EE0D
		public static bool operator ==(HSteamUser x, HSteamUser y)
		{
			return x.m_HSteamUser == y.m_HSteamUser;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00010C1D File Offset: 0x0000EE1D
		public static bool operator !=(HSteamUser x, HSteamUser y)
		{
			return !(x == y);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00010C29 File Offset: 0x0000EE29
		public static explicit operator HSteamUser(int value)
		{
			return new HSteamUser(value);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00010C31 File Offset: 0x0000EE31
		public static explicit operator int(HSteamUser that)
		{
			return that.m_HSteamUser;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00010C0D File Offset: 0x0000EE0D
		public bool Equals(HSteamUser other)
		{
			return this.m_HSteamUser == other.m_HSteamUser;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00010C39 File Offset: 0x0000EE39
		public int CompareTo(HSteamUser other)
		{
			return this.m_HSteamUser.CompareTo(other.m_HSteamUser);
		}

		// Token: 0x04000B41 RID: 2881
		public int m_HSteamUser;
	}
}
