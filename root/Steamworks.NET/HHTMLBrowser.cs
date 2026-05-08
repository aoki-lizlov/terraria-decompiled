using System;

namespace Steamworks
{
	// Token: 0x0200019E RID: 414
	[Serializable]
	public struct HHTMLBrowser : IEquatable<HHTMLBrowser>, IComparable<HHTMLBrowser>
	{
		// Token: 0x060009D0 RID: 2512 RVA: 0x0000F381 File Offset: 0x0000D581
		public HHTMLBrowser(uint value)
		{
			this.m_HHTMLBrowser = value;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0000F38A File Offset: 0x0000D58A
		public override string ToString()
		{
			return this.m_HHTMLBrowser.ToString();
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0000F397 File Offset: 0x0000D597
		public override bool Equals(object other)
		{
			return other is HHTMLBrowser && this == (HHTMLBrowser)other;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
		public override int GetHashCode()
		{
			return this.m_HHTMLBrowser.GetHashCode();
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0000F3C1 File Offset: 0x0000D5C1
		public static bool operator ==(HHTMLBrowser x, HHTMLBrowser y)
		{
			return x.m_HHTMLBrowser == y.m_HHTMLBrowser;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0000F3D1 File Offset: 0x0000D5D1
		public static bool operator !=(HHTMLBrowser x, HHTMLBrowser y)
		{
			return !(x == y);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0000F3DD File Offset: 0x0000D5DD
		public static explicit operator HHTMLBrowser(uint value)
		{
			return new HHTMLBrowser(value);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0000F3E5 File Offset: 0x0000D5E5
		public static explicit operator uint(HHTMLBrowser that)
		{
			return that.m_HHTMLBrowser;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0000F3C1 File Offset: 0x0000D5C1
		public bool Equals(HHTMLBrowser other)
		{
			return this.m_HHTMLBrowser == other.m_HHTMLBrowser;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0000F3ED File Offset: 0x0000D5ED
		public int CompareTo(HHTMLBrowser other)
		{
			return this.m_HHTMLBrowser.CompareTo(other.m_HHTMLBrowser);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0000F400 File Offset: 0x0000D600
		// Note: this type is marked as 'beforefieldinit'.
		static HHTMLBrowser()
		{
		}

		// Token: 0x04000AC4 RID: 2756
		public static readonly HHTMLBrowser Invalid = new HHTMLBrowser(0U);

		// Token: 0x04000AC5 RID: 2757
		public uint m_HHTMLBrowser;
	}
}
