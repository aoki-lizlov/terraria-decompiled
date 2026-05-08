using System;

namespace Steamworks
{
	// Token: 0x020001BA RID: 442
	[Serializable]
	public struct SNetListenSocket_t : IEquatable<SNetListenSocket_t>, IComparable<SNetListenSocket_t>
	{
		// Token: 0x06000AC5 RID: 2757 RVA: 0x0000FFDE File Offset: 0x0000E1DE
		public SNetListenSocket_t(uint value)
		{
			this.m_SNetListenSocket = value;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0000FFE7 File Offset: 0x0000E1E7
		public override string ToString()
		{
			return this.m_SNetListenSocket.ToString();
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0000FFF4 File Offset: 0x0000E1F4
		public override bool Equals(object other)
		{
			return other is SNetListenSocket_t && this == (SNetListenSocket_t)other;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00010011 File Offset: 0x0000E211
		public override int GetHashCode()
		{
			return this.m_SNetListenSocket.GetHashCode();
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0001001E File Offset: 0x0000E21E
		public static bool operator ==(SNetListenSocket_t x, SNetListenSocket_t y)
		{
			return x.m_SNetListenSocket == y.m_SNetListenSocket;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0001002E File Offset: 0x0000E22E
		public static bool operator !=(SNetListenSocket_t x, SNetListenSocket_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0001003A File Offset: 0x0000E23A
		public static explicit operator SNetListenSocket_t(uint value)
		{
			return new SNetListenSocket_t(value);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00010042 File Offset: 0x0000E242
		public static explicit operator uint(SNetListenSocket_t that)
		{
			return that.m_SNetListenSocket;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0001001E File Offset: 0x0000E21E
		public bool Equals(SNetListenSocket_t other)
		{
			return this.m_SNetListenSocket == other.m_SNetListenSocket;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0001004A File Offset: 0x0000E24A
		public int CompareTo(SNetListenSocket_t other)
		{
			return this.m_SNetListenSocket.CompareTo(other.m_SNetListenSocket);
		}

		// Token: 0x04000B1F RID: 2847
		public uint m_SNetListenSocket;
	}
}
