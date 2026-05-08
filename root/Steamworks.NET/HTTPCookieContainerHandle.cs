using System;

namespace Steamworks
{
	// Token: 0x0200019F RID: 415
	[Serializable]
	public struct HTTPCookieContainerHandle : IEquatable<HTTPCookieContainerHandle>, IComparable<HTTPCookieContainerHandle>
	{
		// Token: 0x060009DB RID: 2523 RVA: 0x0000F40D File Offset: 0x0000D60D
		public HTTPCookieContainerHandle(uint value)
		{
			this.m_HTTPCookieContainerHandle = value;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0000F416 File Offset: 0x0000D616
		public override string ToString()
		{
			return this.m_HTTPCookieContainerHandle.ToString();
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0000F423 File Offset: 0x0000D623
		public override bool Equals(object other)
		{
			return other is HTTPCookieContainerHandle && this == (HTTPCookieContainerHandle)other;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0000F440 File Offset: 0x0000D640
		public override int GetHashCode()
		{
			return this.m_HTTPCookieContainerHandle.GetHashCode();
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0000F44D File Offset: 0x0000D64D
		public static bool operator ==(HTTPCookieContainerHandle x, HTTPCookieContainerHandle y)
		{
			return x.m_HTTPCookieContainerHandle == y.m_HTTPCookieContainerHandle;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0000F45D File Offset: 0x0000D65D
		public static bool operator !=(HTTPCookieContainerHandle x, HTTPCookieContainerHandle y)
		{
			return !(x == y);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0000F469 File Offset: 0x0000D669
		public static explicit operator HTTPCookieContainerHandle(uint value)
		{
			return new HTTPCookieContainerHandle(value);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0000F471 File Offset: 0x0000D671
		public static explicit operator uint(HTTPCookieContainerHandle that)
		{
			return that.m_HTTPCookieContainerHandle;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0000F44D File Offset: 0x0000D64D
		public bool Equals(HTTPCookieContainerHandle other)
		{
			return this.m_HTTPCookieContainerHandle == other.m_HTTPCookieContainerHandle;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0000F479 File Offset: 0x0000D679
		public int CompareTo(HTTPCookieContainerHandle other)
		{
			return this.m_HTTPCookieContainerHandle.CompareTo(other.m_HTTPCookieContainerHandle);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0000F48C File Offset: 0x0000D68C
		// Note: this type is marked as 'beforefieldinit'.
		static HTTPCookieContainerHandle()
		{
		}

		// Token: 0x04000AC6 RID: 2758
		public static readonly HTTPCookieContainerHandle Invalid = new HTTPCookieContainerHandle(0U);

		// Token: 0x04000AC7 RID: 2759
		public uint m_HTTPCookieContainerHandle;
	}
}
