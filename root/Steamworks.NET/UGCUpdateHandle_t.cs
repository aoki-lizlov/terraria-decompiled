using System;

namespace Steamworks
{
	// Token: 0x020001CA RID: 458
	[Serializable]
	public struct UGCUpdateHandle_t : IEquatable<UGCUpdateHandle_t>, IComparable<UGCUpdateHandle_t>
	{
		// Token: 0x06000B6B RID: 2923 RVA: 0x000109C3 File Offset: 0x0000EBC3
		public UGCUpdateHandle_t(ulong value)
		{
			this.m_UGCUpdateHandle = value;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x000109CC File Offset: 0x0000EBCC
		public override string ToString()
		{
			return this.m_UGCUpdateHandle.ToString();
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x000109D9 File Offset: 0x0000EBD9
		public override bool Equals(object other)
		{
			return other is UGCUpdateHandle_t && this == (UGCUpdateHandle_t)other;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x000109F6 File Offset: 0x0000EBF6
		public override int GetHashCode()
		{
			return this.m_UGCUpdateHandle.GetHashCode();
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00010A03 File Offset: 0x0000EC03
		public static bool operator ==(UGCUpdateHandle_t x, UGCUpdateHandle_t y)
		{
			return x.m_UGCUpdateHandle == y.m_UGCUpdateHandle;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00010A13 File Offset: 0x0000EC13
		public static bool operator !=(UGCUpdateHandle_t x, UGCUpdateHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00010A1F File Offset: 0x0000EC1F
		public static explicit operator UGCUpdateHandle_t(ulong value)
		{
			return new UGCUpdateHandle_t(value);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00010A27 File Offset: 0x0000EC27
		public static explicit operator ulong(UGCUpdateHandle_t that)
		{
			return that.m_UGCUpdateHandle;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00010A03 File Offset: 0x0000EC03
		public bool Equals(UGCUpdateHandle_t other)
		{
			return this.m_UGCUpdateHandle == other.m_UGCUpdateHandle;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00010A2F File Offset: 0x0000EC2F
		public int CompareTo(UGCUpdateHandle_t other)
		{
			return this.m_UGCUpdateHandle.CompareTo(other.m_UGCUpdateHandle);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00010A42 File Offset: 0x0000EC42
		// Note: this type is marked as 'beforefieldinit'.
		static UGCUpdateHandle_t()
		{
		}

		// Token: 0x04000B3C RID: 2876
		public static readonly UGCUpdateHandle_t Invalid = new UGCUpdateHandle_t(ulong.MaxValue);

		// Token: 0x04000B3D RID: 2877
		public ulong m_UGCUpdateHandle;
	}
}
