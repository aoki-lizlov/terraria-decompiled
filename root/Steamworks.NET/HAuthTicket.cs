using System;

namespace Steamworks
{
	// Token: 0x02000198 RID: 408
	[Serializable]
	public struct HAuthTicket : IEquatable<HAuthTicket>, IComparable<HAuthTicket>
	{
		// Token: 0x060009B0 RID: 2480 RVA: 0x0000F250 File Offset: 0x0000D450
		public HAuthTicket(uint value)
		{
			this.m_HAuthTicket = value;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0000F259 File Offset: 0x0000D459
		public override string ToString()
		{
			return this.m_HAuthTicket.ToString();
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0000F266 File Offset: 0x0000D466
		public override bool Equals(object other)
		{
			return other is HAuthTicket && this == (HAuthTicket)other;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0000F283 File Offset: 0x0000D483
		public override int GetHashCode()
		{
			return this.m_HAuthTicket.GetHashCode();
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0000F290 File Offset: 0x0000D490
		public static bool operator ==(HAuthTicket x, HAuthTicket y)
		{
			return x.m_HAuthTicket == y.m_HAuthTicket;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0000F2A0 File Offset: 0x0000D4A0
		public static bool operator !=(HAuthTicket x, HAuthTicket y)
		{
			return !(x == y);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0000F2AC File Offset: 0x0000D4AC
		public static explicit operator HAuthTicket(uint value)
		{
			return new HAuthTicket(value);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0000F2B4 File Offset: 0x0000D4B4
		public static explicit operator uint(HAuthTicket that)
		{
			return that.m_HAuthTicket;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0000F290 File Offset: 0x0000D490
		public bool Equals(HAuthTicket other)
		{
			return this.m_HAuthTicket == other.m_HAuthTicket;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0000F2BC File Offset: 0x0000D4BC
		public int CompareTo(HAuthTicket other)
		{
			return this.m_HAuthTicket.CompareTo(other.m_HAuthTicket);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0000F2CF File Offset: 0x0000D4CF
		// Note: this type is marked as 'beforefieldinit'.
		static HAuthTicket()
		{
		}

		// Token: 0x04000AB4 RID: 2740
		public static readonly HAuthTicket Invalid = new HAuthTicket(0U);

		// Token: 0x04000AB5 RID: 2741
		public uint m_HAuthTicket;
	}
}
