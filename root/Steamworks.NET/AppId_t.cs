using System;

namespace Steamworks
{
	// Token: 0x020001C3 RID: 451
	[Serializable]
	public struct AppId_t : IEquatable<AppId_t>, IComparable<AppId_t>
	{
		// Token: 0x06000B25 RID: 2853 RVA: 0x000104A7 File Offset: 0x0000E6A7
		public AppId_t(uint value)
		{
			this.m_AppId = value;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x000104B0 File Offset: 0x0000E6B0
		public override string ToString()
		{
			return this.m_AppId.ToString();
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x000104BD File Offset: 0x0000E6BD
		public override bool Equals(object other)
		{
			return other is AppId_t && this == (AppId_t)other;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000104DA File Offset: 0x0000E6DA
		public override int GetHashCode()
		{
			return this.m_AppId.GetHashCode();
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x000104E7 File Offset: 0x0000E6E7
		public static bool operator ==(AppId_t x, AppId_t y)
		{
			return x.m_AppId == y.m_AppId;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000104F7 File Offset: 0x0000E6F7
		public static bool operator !=(AppId_t x, AppId_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00010503 File Offset: 0x0000E703
		public static explicit operator AppId_t(uint value)
		{
			return new AppId_t(value);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0001050B File Offset: 0x0000E70B
		public static explicit operator uint(AppId_t that)
		{
			return that.m_AppId;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x000104E7 File Offset: 0x0000E6E7
		public bool Equals(AppId_t other)
		{
			return this.m_AppId == other.m_AppId;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00010513 File Offset: 0x0000E713
		public int CompareTo(AppId_t other)
		{
			return this.m_AppId.CompareTo(other.m_AppId);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00010526 File Offset: 0x0000E726
		// Note: this type is marked as 'beforefieldinit'.
		static AppId_t()
		{
		}

		// Token: 0x04000B2E RID: 2862
		public static readonly AppId_t Invalid = new AppId_t(0U);

		// Token: 0x04000B2F RID: 2863
		public uint m_AppId;
	}
}
