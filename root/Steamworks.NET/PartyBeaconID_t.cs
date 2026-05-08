using System;

namespace Steamworks
{
	// Token: 0x020001C5 RID: 453
	[Serializable]
	public struct PartyBeaconID_t : IEquatable<PartyBeaconID_t>, IComparable<PartyBeaconID_t>
	{
		// Token: 0x06000B3B RID: 2875 RVA: 0x000105BF File Offset: 0x0000E7BF
		public PartyBeaconID_t(ulong value)
		{
			this.m_PartyBeaconID = value;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000105C8 File Offset: 0x0000E7C8
		public override string ToString()
		{
			return this.m_PartyBeaconID.ToString();
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000105D5 File Offset: 0x0000E7D5
		public override bool Equals(object other)
		{
			return other is PartyBeaconID_t && this == (PartyBeaconID_t)other;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x000105F2 File Offset: 0x0000E7F2
		public override int GetHashCode()
		{
			return this.m_PartyBeaconID.GetHashCode();
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x000105FF File Offset: 0x0000E7FF
		public static bool operator ==(PartyBeaconID_t x, PartyBeaconID_t y)
		{
			return x.m_PartyBeaconID == y.m_PartyBeaconID;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0001060F File Offset: 0x0000E80F
		public static bool operator !=(PartyBeaconID_t x, PartyBeaconID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0001061B File Offset: 0x0000E81B
		public static explicit operator PartyBeaconID_t(ulong value)
		{
			return new PartyBeaconID_t(value);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00010623 File Offset: 0x0000E823
		public static explicit operator ulong(PartyBeaconID_t that)
		{
			return that.m_PartyBeaconID;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000105FF File Offset: 0x0000E7FF
		public bool Equals(PartyBeaconID_t other)
		{
			return this.m_PartyBeaconID == other.m_PartyBeaconID;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0001062B File Offset: 0x0000E82B
		public int CompareTo(PartyBeaconID_t other)
		{
			return this.m_PartyBeaconID.CompareTo(other.m_PartyBeaconID);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0001063E File Offset: 0x0000E83E
		// Note: this type is marked as 'beforefieldinit'.
		static PartyBeaconID_t()
		{
		}

		// Token: 0x04000B32 RID: 2866
		public static readonly PartyBeaconID_t Invalid = new PartyBeaconID_t(0UL);

		// Token: 0x04000B33 RID: 2867
		public ulong m_PartyBeaconID;
	}
}
