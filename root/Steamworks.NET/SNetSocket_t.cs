using System;

namespace Steamworks
{
	// Token: 0x020001BB RID: 443
	[Serializable]
	public struct SNetSocket_t : IEquatable<SNetSocket_t>, IComparable<SNetSocket_t>
	{
		// Token: 0x06000ACF RID: 2767 RVA: 0x0001005D File Offset: 0x0000E25D
		public SNetSocket_t(uint value)
		{
			this.m_SNetSocket = value;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00010066 File Offset: 0x0000E266
		public override string ToString()
		{
			return this.m_SNetSocket.ToString();
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00010073 File Offset: 0x0000E273
		public override bool Equals(object other)
		{
			return other is SNetSocket_t && this == (SNetSocket_t)other;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00010090 File Offset: 0x0000E290
		public override int GetHashCode()
		{
			return this.m_SNetSocket.GetHashCode();
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001009D File Offset: 0x0000E29D
		public static bool operator ==(SNetSocket_t x, SNetSocket_t y)
		{
			return x.m_SNetSocket == y.m_SNetSocket;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x000100AD File Offset: 0x0000E2AD
		public static bool operator !=(SNetSocket_t x, SNetSocket_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000100B9 File Offset: 0x0000E2B9
		public static explicit operator SNetSocket_t(uint value)
		{
			return new SNetSocket_t(value);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000100C1 File Offset: 0x0000E2C1
		public static explicit operator uint(SNetSocket_t that)
		{
			return that.m_SNetSocket;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0001009D File Offset: 0x0000E29D
		public bool Equals(SNetSocket_t other)
		{
			return this.m_SNetSocket == other.m_SNetSocket;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000100C9 File Offset: 0x0000E2C9
		public int CompareTo(SNetSocket_t other)
		{
			return this.m_SNetSocket.CompareTo(other.m_SNetSocket);
		}

		// Token: 0x04000B20 RID: 2848
		public uint m_SNetSocket;
	}
}
