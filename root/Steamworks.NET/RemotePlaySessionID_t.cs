using System;

namespace Steamworks
{
	// Token: 0x020001BC RID: 444
	[Serializable]
	public struct RemotePlaySessionID_t : IEquatable<RemotePlaySessionID_t>, IComparable<RemotePlaySessionID_t>
	{
		// Token: 0x06000AD9 RID: 2777 RVA: 0x000100DC File Offset: 0x0000E2DC
		public RemotePlaySessionID_t(uint value)
		{
			this.m_RemotePlaySessionID = value;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x000100E5 File Offset: 0x0000E2E5
		public override string ToString()
		{
			return this.m_RemotePlaySessionID.ToString();
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000100F2 File Offset: 0x0000E2F2
		public override bool Equals(object other)
		{
			return other is RemotePlaySessionID_t && this == (RemotePlaySessionID_t)other;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0001010F File Offset: 0x0000E30F
		public override int GetHashCode()
		{
			return this.m_RemotePlaySessionID.GetHashCode();
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0001011C File Offset: 0x0000E31C
		public static bool operator ==(RemotePlaySessionID_t x, RemotePlaySessionID_t y)
		{
			return x.m_RemotePlaySessionID == y.m_RemotePlaySessionID;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0001012C File Offset: 0x0000E32C
		public static bool operator !=(RemotePlaySessionID_t x, RemotePlaySessionID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00010138 File Offset: 0x0000E338
		public static explicit operator RemotePlaySessionID_t(uint value)
		{
			return new RemotePlaySessionID_t(value);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00010140 File Offset: 0x0000E340
		public static explicit operator uint(RemotePlaySessionID_t that)
		{
			return that.m_RemotePlaySessionID;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0001011C File Offset: 0x0000E31C
		public bool Equals(RemotePlaySessionID_t other)
		{
			return this.m_RemotePlaySessionID == other.m_RemotePlaySessionID;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00010148 File Offset: 0x0000E348
		public int CompareTo(RemotePlaySessionID_t other)
		{
			return this.m_RemotePlaySessionID.CompareTo(other.m_RemotePlaySessionID);
		}

		// Token: 0x04000B21 RID: 2849
		public uint m_RemotePlaySessionID;
	}
}
