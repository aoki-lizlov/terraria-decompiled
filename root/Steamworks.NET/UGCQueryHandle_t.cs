using System;

namespace Steamworks
{
	// Token: 0x020001C9 RID: 457
	[Serializable]
	public struct UGCQueryHandle_t : IEquatable<UGCQueryHandle_t>, IComparable<UGCQueryHandle_t>
	{
		// Token: 0x06000B60 RID: 2912 RVA: 0x00010936 File Offset: 0x0000EB36
		public UGCQueryHandle_t(ulong value)
		{
			this.m_UGCQueryHandle = value;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0001093F File Offset: 0x0000EB3F
		public override string ToString()
		{
			return this.m_UGCQueryHandle.ToString();
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0001094C File Offset: 0x0000EB4C
		public override bool Equals(object other)
		{
			return other is UGCQueryHandle_t && this == (UGCQueryHandle_t)other;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00010969 File Offset: 0x0000EB69
		public override int GetHashCode()
		{
			return this.m_UGCQueryHandle.GetHashCode();
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00010976 File Offset: 0x0000EB76
		public static bool operator ==(UGCQueryHandle_t x, UGCQueryHandle_t y)
		{
			return x.m_UGCQueryHandle == y.m_UGCQueryHandle;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00010986 File Offset: 0x0000EB86
		public static bool operator !=(UGCQueryHandle_t x, UGCQueryHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00010992 File Offset: 0x0000EB92
		public static explicit operator UGCQueryHandle_t(ulong value)
		{
			return new UGCQueryHandle_t(value);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0001099A File Offset: 0x0000EB9A
		public static explicit operator ulong(UGCQueryHandle_t that)
		{
			return that.m_UGCQueryHandle;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00010976 File Offset: 0x0000EB76
		public bool Equals(UGCQueryHandle_t other)
		{
			return this.m_UGCQueryHandle == other.m_UGCQueryHandle;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x000109A2 File Offset: 0x0000EBA2
		public int CompareTo(UGCQueryHandle_t other)
		{
			return this.m_UGCQueryHandle.CompareTo(other.m_UGCQueryHandle);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x000109B5 File Offset: 0x0000EBB5
		// Note: this type is marked as 'beforefieldinit'.
		static UGCQueryHandle_t()
		{
		}

		// Token: 0x04000B3A RID: 2874
		public static readonly UGCQueryHandle_t Invalid = new UGCQueryHandle_t(ulong.MaxValue);

		// Token: 0x04000B3B RID: 2875
		public ulong m_UGCQueryHandle;
	}
}
