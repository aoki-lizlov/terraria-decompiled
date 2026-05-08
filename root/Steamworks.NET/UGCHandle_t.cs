using System;

namespace Steamworks
{
	// Token: 0x020001C0 RID: 448
	[Serializable]
	public struct UGCHandle_t : IEquatable<UGCHandle_t>, IComparable<UGCHandle_t>
	{
		// Token: 0x06000B04 RID: 2820 RVA: 0x00010302 File Offset: 0x0000E502
		public UGCHandle_t(ulong value)
		{
			this.m_UGCHandle = value;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0001030B File Offset: 0x0000E50B
		public override string ToString()
		{
			return this.m_UGCHandle.ToString();
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00010318 File Offset: 0x0000E518
		public override bool Equals(object other)
		{
			return other is UGCHandle_t && this == (UGCHandle_t)other;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00010335 File Offset: 0x0000E535
		public override int GetHashCode()
		{
			return this.m_UGCHandle.GetHashCode();
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00010342 File Offset: 0x0000E542
		public static bool operator ==(UGCHandle_t x, UGCHandle_t y)
		{
			return x.m_UGCHandle == y.m_UGCHandle;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00010352 File Offset: 0x0000E552
		public static bool operator !=(UGCHandle_t x, UGCHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0001035E File Offset: 0x0000E55E
		public static explicit operator UGCHandle_t(ulong value)
		{
			return new UGCHandle_t(value);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00010366 File Offset: 0x0000E566
		public static explicit operator ulong(UGCHandle_t that)
		{
			return that.m_UGCHandle;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00010342 File Offset: 0x0000E542
		public bool Equals(UGCHandle_t other)
		{
			return this.m_UGCHandle == other.m_UGCHandle;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0001036E File Offset: 0x0000E56E
		public int CompareTo(UGCHandle_t other)
		{
			return this.m_UGCHandle.CompareTo(other.m_UGCHandle);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00010381 File Offset: 0x0000E581
		// Note: this type is marked as 'beforefieldinit'.
		static UGCHandle_t()
		{
		}

		// Token: 0x04000B28 RID: 2856
		public static readonly UGCHandle_t Invalid = new UGCHandle_t(ulong.MaxValue);

		// Token: 0x04000B29 RID: 2857
		public ulong m_UGCHandle;
	}
}
