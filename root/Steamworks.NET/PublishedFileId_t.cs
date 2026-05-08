using System;

namespace Steamworks
{
	// Token: 0x020001BD RID: 445
	[Serializable]
	public struct PublishedFileId_t : IEquatable<PublishedFileId_t>, IComparable<PublishedFileId_t>
	{
		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001015B File Offset: 0x0000E35B
		public PublishedFileId_t(ulong value)
		{
			this.m_PublishedFileId = value;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00010164 File Offset: 0x0000E364
		public override string ToString()
		{
			return this.m_PublishedFileId.ToString();
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00010171 File Offset: 0x0000E371
		public override bool Equals(object other)
		{
			return other is PublishedFileId_t && this == (PublishedFileId_t)other;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0001018E File Offset: 0x0000E38E
		public override int GetHashCode()
		{
			return this.m_PublishedFileId.GetHashCode();
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0001019B File Offset: 0x0000E39B
		public static bool operator ==(PublishedFileId_t x, PublishedFileId_t y)
		{
			return x.m_PublishedFileId == y.m_PublishedFileId;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000101AB File Offset: 0x0000E3AB
		public static bool operator !=(PublishedFileId_t x, PublishedFileId_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000101B7 File Offset: 0x0000E3B7
		public static explicit operator PublishedFileId_t(ulong value)
		{
			return new PublishedFileId_t(value);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000101BF File Offset: 0x0000E3BF
		public static explicit operator ulong(PublishedFileId_t that)
		{
			return that.m_PublishedFileId;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0001019B File Offset: 0x0000E39B
		public bool Equals(PublishedFileId_t other)
		{
			return this.m_PublishedFileId == other.m_PublishedFileId;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000101C7 File Offset: 0x0000E3C7
		public int CompareTo(PublishedFileId_t other)
		{
			return this.m_PublishedFileId.CompareTo(other.m_PublishedFileId);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000101DA File Offset: 0x0000E3DA
		// Note: this type is marked as 'beforefieldinit'.
		static PublishedFileId_t()
		{
		}

		// Token: 0x04000B22 RID: 2850
		public static readonly PublishedFileId_t Invalid = new PublishedFileId_t(0UL);

		// Token: 0x04000B23 RID: 2851
		public ulong m_PublishedFileId;
	}
}
