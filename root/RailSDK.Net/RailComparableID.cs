using System;

namespace rail
{
	// Token: 0x0200003D RID: 61
	public class RailComparableID : IEquatable<RailComparableID>, IComparable<RailComparableID>
	{
		// Token: 0x060015BA RID: 5562 RVA: 0x00002119 File Offset: 0x00000319
		public RailComparableID()
		{
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x0000EF7E File Offset: 0x0000D17E
		public RailComparableID(ulong id)
		{
			this.id_ = id;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x0000EF8D File Offset: 0x0000D18D
		public bool IsValid()
		{
			return this.id_ > 0UL;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x0000EF99 File Offset: 0x0000D199
		public override bool Equals(object other)
		{
			return other is RailComparableID && this == (RailComparableID)other;
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x0000EFB1 File Offset: 0x0000D1B1
		public override int GetHashCode()
		{
			return this.id_.GetHashCode();
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0000EFBE File Offset: 0x0000D1BE
		public static bool operator ==(RailComparableID x, RailComparableID y)
		{
			if (x == null)
			{
				return y == null;
			}
			return x.Equals(y);
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0000EFCF File Offset: 0x0000D1CF
		public static bool operator !=(RailComparableID x, RailComparableID y)
		{
			return !(x == y);
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x0000EFDB File Offset: 0x0000D1DB
		public static explicit operator RailComparableID(ulong value)
		{
			return new RailComparableID(value);
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x0000EFE3 File Offset: 0x0000D1E3
		public static explicit operator ulong(RailComparableID that)
		{
			return that.id_;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x0000EFEB File Offset: 0x0000D1EB
		public bool Equals(RailComparableID other)
		{
			return other != null && (this == other || (!(base.GetType() != other.GetType()) && other.id_ == this.id_));
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0000F01B File Offset: 0x0000D21B
		public int CompareTo(RailComparableID other)
		{
			return this.id_.CompareTo(other.id_);
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0000F02E File Offset: 0x0000D22E
		public override string ToString()
		{
			return this.id_.ToString();
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0000F03B File Offset: 0x0000D23B
		// Note: this type is marked as 'beforefieldinit'.
		static RailComparableID()
		{
		}

		// Token: 0x04000006 RID: 6
		public static readonly RailComparableID Nil = new RailComparableID(0UL);

		// Token: 0x04000007 RID: 7
		public ulong id_;
	}
}
