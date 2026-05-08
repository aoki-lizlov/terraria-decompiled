using System;

namespace Steamworks
{
	// Token: 0x020001C4 RID: 452
	[Serializable]
	public struct DepotId_t : IEquatable<DepotId_t>, IComparable<DepotId_t>
	{
		// Token: 0x06000B30 RID: 2864 RVA: 0x00010533 File Offset: 0x0000E733
		public DepotId_t(uint value)
		{
			this.m_DepotId = value;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0001053C File Offset: 0x0000E73C
		public override string ToString()
		{
			return this.m_DepotId.ToString();
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00010549 File Offset: 0x0000E749
		public override bool Equals(object other)
		{
			return other is DepotId_t && this == (DepotId_t)other;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00010566 File Offset: 0x0000E766
		public override int GetHashCode()
		{
			return this.m_DepotId.GetHashCode();
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00010573 File Offset: 0x0000E773
		public static bool operator ==(DepotId_t x, DepotId_t y)
		{
			return x.m_DepotId == y.m_DepotId;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00010583 File Offset: 0x0000E783
		public static bool operator !=(DepotId_t x, DepotId_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0001058F File Offset: 0x0000E78F
		public static explicit operator DepotId_t(uint value)
		{
			return new DepotId_t(value);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00010597 File Offset: 0x0000E797
		public static explicit operator uint(DepotId_t that)
		{
			return that.m_DepotId;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00010573 File Offset: 0x0000E773
		public bool Equals(DepotId_t other)
		{
			return this.m_DepotId == other.m_DepotId;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0001059F File Offset: 0x0000E79F
		public int CompareTo(DepotId_t other)
		{
			return this.m_DepotId.CompareTo(other.m_DepotId);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000105B2 File Offset: 0x0000E7B2
		// Note: this type is marked as 'beforefieldinit'.
		static DepotId_t()
		{
		}

		// Token: 0x04000B30 RID: 2864
		public static readonly DepotId_t Invalid = new DepotId_t(0U);

		// Token: 0x04000B31 RID: 2865
		public uint m_DepotId;
	}
}
