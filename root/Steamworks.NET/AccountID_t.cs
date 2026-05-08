using System;

namespace Steamworks
{
	// Token: 0x020001C2 RID: 450
	[Serializable]
	public struct AccountID_t : IEquatable<AccountID_t>, IComparable<AccountID_t>
	{
		// Token: 0x06000B1A RID: 2842 RVA: 0x0001041B File Offset: 0x0000E61B
		public AccountID_t(uint value)
		{
			this.m_AccountID = value;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00010424 File Offset: 0x0000E624
		public override string ToString()
		{
			return this.m_AccountID.ToString();
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00010431 File Offset: 0x0000E631
		public override bool Equals(object other)
		{
			return other is AccountID_t && this == (AccountID_t)other;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0001044E File Offset: 0x0000E64E
		public override int GetHashCode()
		{
			return this.m_AccountID.GetHashCode();
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0001045B File Offset: 0x0000E65B
		public static bool operator ==(AccountID_t x, AccountID_t y)
		{
			return x.m_AccountID == y.m_AccountID;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0001046B File Offset: 0x0000E66B
		public static bool operator !=(AccountID_t x, AccountID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00010477 File Offset: 0x0000E677
		public static explicit operator AccountID_t(uint value)
		{
			return new AccountID_t(value);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0001047F File Offset: 0x0000E67F
		public static explicit operator uint(AccountID_t that)
		{
			return that.m_AccountID;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0001045B File Offset: 0x0000E65B
		public bool Equals(AccountID_t other)
		{
			return this.m_AccountID == other.m_AccountID;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00010487 File Offset: 0x0000E687
		public int CompareTo(AccountID_t other)
		{
			return this.m_AccountID.CompareTo(other.m_AccountID);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0001049A File Offset: 0x0000E69A
		// Note: this type is marked as 'beforefieldinit'.
		static AccountID_t()
		{
		}

		// Token: 0x04000B2C RID: 2860
		public static readonly AccountID_t Invalid = new AccountID_t(0U);

		// Token: 0x04000B2D RID: 2861
		public uint m_AccountID;
	}
}
