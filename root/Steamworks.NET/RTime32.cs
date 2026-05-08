using System;

namespace Steamworks
{
	// Token: 0x020001C6 RID: 454
	[Serializable]
	public struct RTime32 : IEquatable<RTime32>, IComparable<RTime32>
	{
		// Token: 0x06000B46 RID: 2886 RVA: 0x0001064C File Offset: 0x0000E84C
		public RTime32(uint value)
		{
			this.m_RTime32 = value;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00010655 File Offset: 0x0000E855
		public override string ToString()
		{
			return this.m_RTime32.ToString();
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00010662 File Offset: 0x0000E862
		public override bool Equals(object other)
		{
			return other is RTime32 && this == (RTime32)other;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0001067F File Offset: 0x0000E87F
		public override int GetHashCode()
		{
			return this.m_RTime32.GetHashCode();
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0001068C File Offset: 0x0000E88C
		public static bool operator ==(RTime32 x, RTime32 y)
		{
			return x.m_RTime32 == y.m_RTime32;
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0001069C File Offset: 0x0000E89C
		public static bool operator !=(RTime32 x, RTime32 y)
		{
			return !(x == y);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000106A8 File Offset: 0x0000E8A8
		public static explicit operator RTime32(uint value)
		{
			return new RTime32(value);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000106B0 File Offset: 0x0000E8B0
		public static explicit operator uint(RTime32 that)
		{
			return that.m_RTime32;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0001068C File Offset: 0x0000E88C
		public bool Equals(RTime32 other)
		{
			return this.m_RTime32 == other.m_RTime32;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x000106B8 File Offset: 0x0000E8B8
		public int CompareTo(RTime32 other)
		{
			return this.m_RTime32.CompareTo(other.m_RTime32);
		}

		// Token: 0x04000B34 RID: 2868
		public uint m_RTime32;
	}
}
