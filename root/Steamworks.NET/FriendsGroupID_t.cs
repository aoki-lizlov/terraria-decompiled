using System;

namespace Steamworks
{
	// Token: 0x0200019D RID: 413
	[Serializable]
	public struct FriendsGroupID_t : IEquatable<FriendsGroupID_t>, IComparable<FriendsGroupID_t>
	{
		// Token: 0x060009C5 RID: 2501 RVA: 0x0000F2F5 File Offset: 0x0000D4F5
		public FriendsGroupID_t(short value)
		{
			this.m_FriendsGroupID = value;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0000F2FE File Offset: 0x0000D4FE
		public override string ToString()
		{
			return this.m_FriendsGroupID.ToString();
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0000F30B File Offset: 0x0000D50B
		public override bool Equals(object other)
		{
			return other is FriendsGroupID_t && this == (FriendsGroupID_t)other;
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0000F328 File Offset: 0x0000D528
		public override int GetHashCode()
		{
			return this.m_FriendsGroupID.GetHashCode();
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0000F335 File Offset: 0x0000D535
		public static bool operator ==(FriendsGroupID_t x, FriendsGroupID_t y)
		{
			return x.m_FriendsGroupID == y.m_FriendsGroupID;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0000F345 File Offset: 0x0000D545
		public static bool operator !=(FriendsGroupID_t x, FriendsGroupID_t y)
		{
			return !(x == y);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0000F351 File Offset: 0x0000D551
		public static explicit operator FriendsGroupID_t(short value)
		{
			return new FriendsGroupID_t(value);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0000F359 File Offset: 0x0000D559
		public static explicit operator short(FriendsGroupID_t that)
		{
			return that.m_FriendsGroupID;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0000F335 File Offset: 0x0000D535
		public bool Equals(FriendsGroupID_t other)
		{
			return this.m_FriendsGroupID == other.m_FriendsGroupID;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0000F361 File Offset: 0x0000D561
		public int CompareTo(FriendsGroupID_t other)
		{
			return this.m_FriendsGroupID.CompareTo(other.m_FriendsGroupID);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0000F374 File Offset: 0x0000D574
		// Note: this type is marked as 'beforefieldinit'.
		static FriendsGroupID_t()
		{
		}

		// Token: 0x04000AC2 RID: 2754
		public static readonly FriendsGroupID_t Invalid = new FriendsGroupID_t(-1);

		// Token: 0x04000AC3 RID: 2755
		public short m_FriendsGroupID;
	}
}
