using System;

namespace Steamworks
{
	// Token: 0x020001A9 RID: 425
	[Serializable]
	public struct SteamItemDef_t : IEquatable<SteamItemDef_t>, IComparable<SteamItemDef_t>
	{
		// Token: 0x06000A33 RID: 2611 RVA: 0x0000F83A File Offset: 0x0000DA3A
		public SteamItemDef_t(int value)
		{
			this.m_SteamItemDef = value;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0000F843 File Offset: 0x0000DA43
		public override string ToString()
		{
			return this.m_SteamItemDef.ToString();
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0000F850 File Offset: 0x0000DA50
		public override bool Equals(object other)
		{
			return other is SteamItemDef_t && this == (SteamItemDef_t)other;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0000F86D File Offset: 0x0000DA6D
		public override int GetHashCode()
		{
			return this.m_SteamItemDef.GetHashCode();
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0000F87A File Offset: 0x0000DA7A
		public static bool operator ==(SteamItemDef_t x, SteamItemDef_t y)
		{
			return x.m_SteamItemDef == y.m_SteamItemDef;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0000F88A File Offset: 0x0000DA8A
		public static bool operator !=(SteamItemDef_t x, SteamItemDef_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0000F896 File Offset: 0x0000DA96
		public static explicit operator SteamItemDef_t(int value)
		{
			return new SteamItemDef_t(value);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0000F89E File Offset: 0x0000DA9E
		public static explicit operator int(SteamItemDef_t that)
		{
			return that.m_SteamItemDef;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0000F87A File Offset: 0x0000DA7A
		public bool Equals(SteamItemDef_t other)
		{
			return this.m_SteamItemDef == other.m_SteamItemDef;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0000F8A6 File Offset: 0x0000DAA6
		public int CompareTo(SteamItemDef_t other)
		{
			return this.m_SteamItemDef.CompareTo(other.m_SteamItemDef);
		}

		// Token: 0x04000AD5 RID: 2773
		public int m_SteamItemDef;
	}
}
