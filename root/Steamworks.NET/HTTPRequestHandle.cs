using System;

namespace Steamworks
{
	// Token: 0x020001A0 RID: 416
	[Serializable]
	public struct HTTPRequestHandle : IEquatable<HTTPRequestHandle>, IComparable<HTTPRequestHandle>
	{
		// Token: 0x060009E6 RID: 2534 RVA: 0x0000F499 File Offset: 0x0000D699
		public HTTPRequestHandle(uint value)
		{
			this.m_HTTPRequestHandle = value;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0000F4A2 File Offset: 0x0000D6A2
		public override string ToString()
		{
			return this.m_HTTPRequestHandle.ToString();
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0000F4AF File Offset: 0x0000D6AF
		public override bool Equals(object other)
		{
			return other is HTTPRequestHandle && this == (HTTPRequestHandle)other;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0000F4CC File Offset: 0x0000D6CC
		public override int GetHashCode()
		{
			return this.m_HTTPRequestHandle.GetHashCode();
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0000F4D9 File Offset: 0x0000D6D9
		public static bool operator ==(HTTPRequestHandle x, HTTPRequestHandle y)
		{
			return x.m_HTTPRequestHandle == y.m_HTTPRequestHandle;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0000F4E9 File Offset: 0x0000D6E9
		public static bool operator !=(HTTPRequestHandle x, HTTPRequestHandle y)
		{
			return !(x == y);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0000F4F5 File Offset: 0x0000D6F5
		public static explicit operator HTTPRequestHandle(uint value)
		{
			return new HTTPRequestHandle(value);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0000F4FD File Offset: 0x0000D6FD
		public static explicit operator uint(HTTPRequestHandle that)
		{
			return that.m_HTTPRequestHandle;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0000F4D9 File Offset: 0x0000D6D9
		public bool Equals(HTTPRequestHandle other)
		{
			return this.m_HTTPRequestHandle == other.m_HTTPRequestHandle;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0000F505 File Offset: 0x0000D705
		public int CompareTo(HTTPRequestHandle other)
		{
			return this.m_HTTPRequestHandle.CompareTo(other.m_HTTPRequestHandle);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0000F518 File Offset: 0x0000D718
		// Note: this type is marked as 'beforefieldinit'.
		static HTTPRequestHandle()
		{
		}

		// Token: 0x04000AC8 RID: 2760
		public static readonly HTTPRequestHandle Invalid = new HTTPRequestHandle(0U);

		// Token: 0x04000AC9 RID: 2761
		public uint m_HTTPRequestHandle;
	}
}
