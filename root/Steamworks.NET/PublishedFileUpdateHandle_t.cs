using System;

namespace Steamworks
{
	// Token: 0x020001BE RID: 446
	[Serializable]
	public struct PublishedFileUpdateHandle_t : IEquatable<PublishedFileUpdateHandle_t>, IComparable<PublishedFileUpdateHandle_t>
	{
		// Token: 0x06000AEE RID: 2798 RVA: 0x000101E8 File Offset: 0x0000E3E8
		public PublishedFileUpdateHandle_t(ulong value)
		{
			this.m_PublishedFileUpdateHandle = value;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000101F1 File Offset: 0x0000E3F1
		public override string ToString()
		{
			return this.m_PublishedFileUpdateHandle.ToString();
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000101FE File Offset: 0x0000E3FE
		public override bool Equals(object other)
		{
			return other is PublishedFileUpdateHandle_t && this == (PublishedFileUpdateHandle_t)other;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0001021B File Offset: 0x0000E41B
		public override int GetHashCode()
		{
			return this.m_PublishedFileUpdateHandle.GetHashCode();
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00010228 File Offset: 0x0000E428
		public static bool operator ==(PublishedFileUpdateHandle_t x, PublishedFileUpdateHandle_t y)
		{
			return x.m_PublishedFileUpdateHandle == y.m_PublishedFileUpdateHandle;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00010238 File Offset: 0x0000E438
		public static bool operator !=(PublishedFileUpdateHandle_t x, PublishedFileUpdateHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00010244 File Offset: 0x0000E444
		public static explicit operator PublishedFileUpdateHandle_t(ulong value)
		{
			return new PublishedFileUpdateHandle_t(value);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0001024C File Offset: 0x0000E44C
		public static explicit operator ulong(PublishedFileUpdateHandle_t that)
		{
			return that.m_PublishedFileUpdateHandle;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00010228 File Offset: 0x0000E428
		public bool Equals(PublishedFileUpdateHandle_t other)
		{
			return this.m_PublishedFileUpdateHandle == other.m_PublishedFileUpdateHandle;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00010254 File Offset: 0x0000E454
		public int CompareTo(PublishedFileUpdateHandle_t other)
		{
			return this.m_PublishedFileUpdateHandle.CompareTo(other.m_PublishedFileUpdateHandle);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00010267 File Offset: 0x0000E467
		// Note: this type is marked as 'beforefieldinit'.
		static PublishedFileUpdateHandle_t()
		{
		}

		// Token: 0x04000B24 RID: 2852
		public static readonly PublishedFileUpdateHandle_t Invalid = new PublishedFileUpdateHandle_t(ulong.MaxValue);

		// Token: 0x04000B25 RID: 2853
		public ulong m_PublishedFileUpdateHandle;
	}
}
