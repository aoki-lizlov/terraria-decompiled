using System;

namespace Steamworks
{
	// Token: 0x020001C1 RID: 449
	[Serializable]
	public struct ScreenshotHandle : IEquatable<ScreenshotHandle>, IComparable<ScreenshotHandle>
	{
		// Token: 0x06000B0F RID: 2831 RVA: 0x0001038F File Offset: 0x0000E58F
		public ScreenshotHandle(uint value)
		{
			this.m_ScreenshotHandle = value;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00010398 File Offset: 0x0000E598
		public override string ToString()
		{
			return this.m_ScreenshotHandle.ToString();
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x000103A5 File Offset: 0x0000E5A5
		public override bool Equals(object other)
		{
			return other is ScreenshotHandle && this == (ScreenshotHandle)other;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x000103C2 File Offset: 0x0000E5C2
		public override int GetHashCode()
		{
			return this.m_ScreenshotHandle.GetHashCode();
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x000103CF File Offset: 0x0000E5CF
		public static bool operator ==(ScreenshotHandle x, ScreenshotHandle y)
		{
			return x.m_ScreenshotHandle == y.m_ScreenshotHandle;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x000103DF File Offset: 0x0000E5DF
		public static bool operator !=(ScreenshotHandle x, ScreenshotHandle y)
		{
			return !(x == y);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000103EB File Offset: 0x0000E5EB
		public static explicit operator ScreenshotHandle(uint value)
		{
			return new ScreenshotHandle(value);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x000103F3 File Offset: 0x0000E5F3
		public static explicit operator uint(ScreenshotHandle that)
		{
			return that.m_ScreenshotHandle;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x000103CF File Offset: 0x0000E5CF
		public bool Equals(ScreenshotHandle other)
		{
			return this.m_ScreenshotHandle == other.m_ScreenshotHandle;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x000103FB File Offset: 0x0000E5FB
		public int CompareTo(ScreenshotHandle other)
		{
			return this.m_ScreenshotHandle.CompareTo(other.m_ScreenshotHandle);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0001040E File Offset: 0x0000E60E
		// Note: this type is marked as 'beforefieldinit'.
		static ScreenshotHandle()
		{
		}

		// Token: 0x04000B2A RID: 2858
		public static readonly ScreenshotHandle Invalid = new ScreenshotHandle(0U);

		// Token: 0x04000B2B RID: 2859
		public uint m_ScreenshotHandle;
	}
}
