using System;

namespace Steamworks
{
	// Token: 0x020001AA RID: 426
	[Serializable]
	public struct SteamItemInstanceID_t : IEquatable<SteamItemInstanceID_t>, IComparable<SteamItemInstanceID_t>
	{
		// Token: 0x06000A3D RID: 2621 RVA: 0x0000F8B9 File Offset: 0x0000DAB9
		public SteamItemInstanceID_t(ulong value)
		{
			this.m_SteamItemInstanceID = value;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0000F8C2 File Offset: 0x0000DAC2
		public override string ToString()
		{
			return this.m_SteamItemInstanceID.ToString();
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0000F8CF File Offset: 0x0000DACF
		public override bool Equals(object other)
		{
			return other is SteamItemInstanceID_t && this == (SteamItemInstanceID_t)other;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0000F8EC File Offset: 0x0000DAEC
		public override int GetHashCode()
		{
			return this.m_SteamItemInstanceID.GetHashCode();
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0000F8F9 File Offset: 0x0000DAF9
		public static bool operator ==(SteamItemInstanceID_t x, SteamItemInstanceID_t y)
		{
			return x.m_SteamItemInstanceID == y.m_SteamItemInstanceID;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0000F909 File Offset: 0x0000DB09
		public static bool operator !=(SteamItemInstanceID_t x, SteamItemInstanceID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0000F915 File Offset: 0x0000DB15
		public static explicit operator SteamItemInstanceID_t(ulong value)
		{
			return new SteamItemInstanceID_t(value);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0000F91D File Offset: 0x0000DB1D
		public static explicit operator ulong(SteamItemInstanceID_t that)
		{
			return that.m_SteamItemInstanceID;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0000F8F9 File Offset: 0x0000DAF9
		public bool Equals(SteamItemInstanceID_t other)
		{
			return this.m_SteamItemInstanceID == other.m_SteamItemInstanceID;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0000F925 File Offset: 0x0000DB25
		public int CompareTo(SteamItemInstanceID_t other)
		{
			return this.m_SteamItemInstanceID.CompareTo(other.m_SteamItemInstanceID);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0000F938 File Offset: 0x0000DB38
		// Note: this type is marked as 'beforefieldinit'.
		static SteamItemInstanceID_t()
		{
		}

		// Token: 0x04000AD6 RID: 2774
		public static readonly SteamItemInstanceID_t Invalid = new SteamItemInstanceID_t(ulong.MaxValue);

		// Token: 0x04000AD7 RID: 2775
		public ulong m_SteamItemInstanceID;
	}
}
