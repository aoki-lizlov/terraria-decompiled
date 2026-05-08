using System;

namespace Steamworks
{
	// Token: 0x020001A7 RID: 423
	[Serializable]
	public struct SteamInventoryResult_t : IEquatable<SteamInventoryResult_t>, IComparable<SteamInventoryResult_t>
	{
		// Token: 0x06000A1D RID: 2589 RVA: 0x0000F721 File Offset: 0x0000D921
		public SteamInventoryResult_t(int value)
		{
			this.m_SteamInventoryResult = value;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0000F72A File Offset: 0x0000D92A
		public override string ToString()
		{
			return this.m_SteamInventoryResult.ToString();
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0000F737 File Offset: 0x0000D937
		public override bool Equals(object other)
		{
			return other is SteamInventoryResult_t && this == (SteamInventoryResult_t)other;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0000F754 File Offset: 0x0000D954
		public override int GetHashCode()
		{
			return this.m_SteamInventoryResult.GetHashCode();
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0000F761 File Offset: 0x0000D961
		public static bool operator ==(SteamInventoryResult_t x, SteamInventoryResult_t y)
		{
			return x.m_SteamInventoryResult == y.m_SteamInventoryResult;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0000F771 File Offset: 0x0000D971
		public static bool operator !=(SteamInventoryResult_t x, SteamInventoryResult_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0000F77D File Offset: 0x0000D97D
		public static explicit operator SteamInventoryResult_t(int value)
		{
			return new SteamInventoryResult_t(value);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0000F785 File Offset: 0x0000D985
		public static explicit operator int(SteamInventoryResult_t that)
		{
			return that.m_SteamInventoryResult;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0000F761 File Offset: 0x0000D961
		public bool Equals(SteamInventoryResult_t other)
		{
			return this.m_SteamInventoryResult == other.m_SteamInventoryResult;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0000F78D File Offset: 0x0000D98D
		public int CompareTo(SteamInventoryResult_t other)
		{
			return this.m_SteamInventoryResult.CompareTo(other.m_SteamInventoryResult);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0000F7A0 File Offset: 0x0000D9A0
		// Note: this type is marked as 'beforefieldinit'.
		static SteamInventoryResult_t()
		{
		}

		// Token: 0x04000AD1 RID: 2769
		public static readonly SteamInventoryResult_t Invalid = new SteamInventoryResult_t(-1);

		// Token: 0x04000AD2 RID: 2770
		public int m_SteamInventoryResult;
	}
}
