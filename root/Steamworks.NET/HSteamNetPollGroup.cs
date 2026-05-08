using System;

namespace Steamworks
{
	// Token: 0x020001B2 RID: 434
	[Serializable]
	public struct HSteamNetPollGroup : IEquatable<HSteamNetPollGroup>, IComparable<HSteamNetPollGroup>
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x0000FB95 File Offset: 0x0000DD95
		public HSteamNetPollGroup(uint value)
		{
			this.m_HSteamNetPollGroup = value;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0000FB9E File Offset: 0x0000DD9E
		public override string ToString()
		{
			return this.m_HSteamNetPollGroup.ToString();
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0000FBAB File Offset: 0x0000DDAB
		public override bool Equals(object other)
		{
			return other is HSteamNetPollGroup && this == (HSteamNetPollGroup)other;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0000FBC8 File Offset: 0x0000DDC8
		public override int GetHashCode()
		{
			return this.m_HSteamNetPollGroup.GetHashCode();
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0000FBD5 File Offset: 0x0000DDD5
		public static bool operator ==(HSteamNetPollGroup x, HSteamNetPollGroup y)
		{
			return x.m_HSteamNetPollGroup == y.m_HSteamNetPollGroup;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0000FBE5 File Offset: 0x0000DDE5
		public static bool operator !=(HSteamNetPollGroup x, HSteamNetPollGroup y)
		{
			return !(x == y);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0000FBF1 File Offset: 0x0000DDF1
		public static explicit operator HSteamNetPollGroup(uint value)
		{
			return new HSteamNetPollGroup(value);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0000FBF9 File Offset: 0x0000DDF9
		public static explicit operator uint(HSteamNetPollGroup that)
		{
			return that.m_HSteamNetPollGroup;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0000FBD5 File Offset: 0x0000DDD5
		public bool Equals(HSteamNetPollGroup other)
		{
			return this.m_HSteamNetPollGroup == other.m_HSteamNetPollGroup;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0000FC01 File Offset: 0x0000DE01
		public int CompareTo(HSteamNetPollGroup other)
		{
			return this.m_HSteamNetPollGroup.CompareTo(other.m_HSteamNetPollGroup);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0000FC14 File Offset: 0x0000DE14
		// Note: this type is marked as 'beforefieldinit'.
		static HSteamNetPollGroup()
		{
		}

		// Token: 0x04000AE0 RID: 2784
		public static readonly HSteamNetPollGroup Invalid = new HSteamNetPollGroup(0U);

		// Token: 0x04000AE1 RID: 2785
		public uint m_HSteamNetPollGroup;
	}
}
