using System;

namespace Steamworks
{
	// Token: 0x020001C7 RID: 455
	[Serializable]
	public struct SteamAPICall_t : IEquatable<SteamAPICall_t>, IComparable<SteamAPICall_t>
	{
		// Token: 0x06000B50 RID: 2896 RVA: 0x000106CB File Offset: 0x0000E8CB
		public SteamAPICall_t(ulong value)
		{
			this.m_SteamAPICall = value;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000106D4 File Offset: 0x0000E8D4
		public override string ToString()
		{
			return this.m_SteamAPICall.ToString();
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x000106E1 File Offset: 0x0000E8E1
		public override bool Equals(object other)
		{
			return other is SteamAPICall_t && this == (SteamAPICall_t)other;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000106FE File Offset: 0x0000E8FE
		public override int GetHashCode()
		{
			return this.m_SteamAPICall.GetHashCode();
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0001070B File Offset: 0x0000E90B
		public static bool operator ==(SteamAPICall_t x, SteamAPICall_t y)
		{
			return x.m_SteamAPICall == y.m_SteamAPICall;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0001071B File Offset: 0x0000E91B
		public static bool operator !=(SteamAPICall_t x, SteamAPICall_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00010727 File Offset: 0x0000E927
		public static explicit operator SteamAPICall_t(ulong value)
		{
			return new SteamAPICall_t(value);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0001072F File Offset: 0x0000E92F
		public static explicit operator ulong(SteamAPICall_t that)
		{
			return that.m_SteamAPICall;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0001070B File Offset: 0x0000E90B
		public bool Equals(SteamAPICall_t other)
		{
			return this.m_SteamAPICall == other.m_SteamAPICall;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00010737 File Offset: 0x0000E937
		public int CompareTo(SteamAPICall_t other)
		{
			return this.m_SteamAPICall.CompareTo(other.m_SteamAPICall);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0001074A File Offset: 0x0000E94A
		// Note: this type is marked as 'beforefieldinit'.
		static SteamAPICall_t()
		{
		}

		// Token: 0x04000B35 RID: 2869
		public static readonly SteamAPICall_t Invalid = new SteamAPICall_t(0UL);

		// Token: 0x04000B36 RID: 2870
		public ulong m_SteamAPICall;
	}
}
