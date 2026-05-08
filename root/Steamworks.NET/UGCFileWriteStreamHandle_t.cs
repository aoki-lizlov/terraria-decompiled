using System;

namespace Steamworks
{
	// Token: 0x020001BF RID: 447
	[Serializable]
	public struct UGCFileWriteStreamHandle_t : IEquatable<UGCFileWriteStreamHandle_t>, IComparable<UGCFileWriteStreamHandle_t>
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x00010275 File Offset: 0x0000E475
		public UGCFileWriteStreamHandle_t(ulong value)
		{
			this.m_UGCFileWriteStreamHandle = value;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0001027E File Offset: 0x0000E47E
		public override string ToString()
		{
			return this.m_UGCFileWriteStreamHandle.ToString();
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0001028B File Offset: 0x0000E48B
		public override bool Equals(object other)
		{
			return other is UGCFileWriteStreamHandle_t && this == (UGCFileWriteStreamHandle_t)other;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x000102A8 File Offset: 0x0000E4A8
		public override int GetHashCode()
		{
			return this.m_UGCFileWriteStreamHandle.GetHashCode();
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000102B5 File Offset: 0x0000E4B5
		public static bool operator ==(UGCFileWriteStreamHandle_t x, UGCFileWriteStreamHandle_t y)
		{
			return x.m_UGCFileWriteStreamHandle == y.m_UGCFileWriteStreamHandle;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000102C5 File Offset: 0x0000E4C5
		public static bool operator !=(UGCFileWriteStreamHandle_t x, UGCFileWriteStreamHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000102D1 File Offset: 0x0000E4D1
		public static explicit operator UGCFileWriteStreamHandle_t(ulong value)
		{
			return new UGCFileWriteStreamHandle_t(value);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x000102D9 File Offset: 0x0000E4D9
		public static explicit operator ulong(UGCFileWriteStreamHandle_t that)
		{
			return that.m_UGCFileWriteStreamHandle;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000102B5 File Offset: 0x0000E4B5
		public bool Equals(UGCFileWriteStreamHandle_t other)
		{
			return this.m_UGCFileWriteStreamHandle == other.m_UGCFileWriteStreamHandle;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000102E1 File Offset: 0x0000E4E1
		public int CompareTo(UGCFileWriteStreamHandle_t other)
		{
			return this.m_UGCFileWriteStreamHandle.CompareTo(other.m_UGCFileWriteStreamHandle);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000102F4 File Offset: 0x0000E4F4
		// Note: this type is marked as 'beforefieldinit'.
		static UGCFileWriteStreamHandle_t()
		{
		}

		// Token: 0x04000B26 RID: 2854
		public static readonly UGCFileWriteStreamHandle_t Invalid = new UGCFileWriteStreamHandle_t(ulong.MaxValue);

		// Token: 0x04000B27 RID: 2855
		public ulong m_UGCFileWriteStreamHandle;
	}
}
