using System;

namespace Steamworks
{
	// Token: 0x020001AB RID: 427
	[Serializable]
	public struct HServerListRequest : IEquatable<HServerListRequest>
	{
		// Token: 0x06000A48 RID: 2632 RVA: 0x0000F946 File Offset: 0x0000DB46
		public HServerListRequest(IntPtr value)
		{
			this.m_HServerListRequest = value;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0000F94F File Offset: 0x0000DB4F
		public override string ToString()
		{
			return this.m_HServerListRequest.ToString();
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0000F95C File Offset: 0x0000DB5C
		public override bool Equals(object other)
		{
			return other is HServerListRequest && this == (HServerListRequest)other;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0000F979 File Offset: 0x0000DB79
		public override int GetHashCode()
		{
			return this.m_HServerListRequest.GetHashCode();
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0000F986 File Offset: 0x0000DB86
		public static bool operator ==(HServerListRequest x, HServerListRequest y)
		{
			return x.m_HServerListRequest == y.m_HServerListRequest;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0000F999 File Offset: 0x0000DB99
		public static bool operator !=(HServerListRequest x, HServerListRequest y)
		{
			return !(x == y);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0000F9A5 File Offset: 0x0000DBA5
		public static explicit operator HServerListRequest(IntPtr value)
		{
			return new HServerListRequest(value);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0000F9AD File Offset: 0x0000DBAD
		public static explicit operator IntPtr(HServerListRequest that)
		{
			return that.m_HServerListRequest;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0000F986 File Offset: 0x0000DB86
		public bool Equals(HServerListRequest other)
		{
			return this.m_HServerListRequest == other.m_HServerListRequest;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0000F9B5 File Offset: 0x0000DBB5
		// Note: this type is marked as 'beforefieldinit'.
		static HServerListRequest()
		{
		}

		// Token: 0x04000AD8 RID: 2776
		public static readonly HServerListRequest Invalid = new HServerListRequest(IntPtr.Zero);

		// Token: 0x04000AD9 RID: 2777
		public IntPtr m_HServerListRequest;
	}
}
