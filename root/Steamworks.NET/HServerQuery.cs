using System;

namespace Steamworks
{
	// Token: 0x020001AC RID: 428
	[Serializable]
	public struct HServerQuery : IEquatable<HServerQuery>, IComparable<HServerQuery>
	{
		// Token: 0x06000A52 RID: 2642 RVA: 0x0000F9C6 File Offset: 0x0000DBC6
		public HServerQuery(int value)
		{
			this.m_HServerQuery = value;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0000F9CF File Offset: 0x0000DBCF
		public override string ToString()
		{
			return this.m_HServerQuery.ToString();
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0000F9DC File Offset: 0x0000DBDC
		public override bool Equals(object other)
		{
			return other is HServerQuery && this == (HServerQuery)other;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0000F9F9 File Offset: 0x0000DBF9
		public override int GetHashCode()
		{
			return this.m_HServerQuery.GetHashCode();
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0000FA06 File Offset: 0x0000DC06
		public static bool operator ==(HServerQuery x, HServerQuery y)
		{
			return x.m_HServerQuery == y.m_HServerQuery;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0000FA16 File Offset: 0x0000DC16
		public static bool operator !=(HServerQuery x, HServerQuery y)
		{
			return !(x == y);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0000FA22 File Offset: 0x0000DC22
		public static explicit operator HServerQuery(int value)
		{
			return new HServerQuery(value);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0000FA2A File Offset: 0x0000DC2A
		public static explicit operator int(HServerQuery that)
		{
			return that.m_HServerQuery;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0000FA06 File Offset: 0x0000DC06
		public bool Equals(HServerQuery other)
		{
			return this.m_HServerQuery == other.m_HServerQuery;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0000FA32 File Offset: 0x0000DC32
		public int CompareTo(HServerQuery other)
		{
			return this.m_HServerQuery.CompareTo(other.m_HServerQuery);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0000FA45 File Offset: 0x0000DC45
		// Note: this type is marked as 'beforefieldinit'.
		static HServerQuery()
		{
		}

		// Token: 0x04000ADA RID: 2778
		public static readonly HServerQuery Invalid = new HServerQuery(-1);

		// Token: 0x04000ADB RID: 2779
		public int m_HServerQuery;
	}
}
