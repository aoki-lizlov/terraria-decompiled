using System;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006B1 RID: 1713
	[ComVisible(true)]
	[Serializable]
	public struct ArrayWithOffset
	{
		// Token: 0x06003FE7 RID: 16359 RVA: 0x000E086E File Offset: 0x000DEA6E
		[SecuritySafeCritical]
		public ArrayWithOffset(object array, int offset)
		{
			this.m_array = array;
			this.m_offset = offset;
			this.m_count = 0;
			this.m_count = this.CalculateCount();
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x000E0891 File Offset: 0x000DEA91
		public object GetArray()
		{
			return this.m_array;
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x000E0899 File Offset: 0x000DEA99
		public int GetOffset()
		{
			return this.m_offset;
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x000E08A1 File Offset: 0x000DEAA1
		public override int GetHashCode()
		{
			return this.m_count + this.m_offset;
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x000E08B0 File Offset: 0x000DEAB0
		public override bool Equals(object obj)
		{
			return obj is ArrayWithOffset && this.Equals((ArrayWithOffset)obj);
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x000E08C8 File Offset: 0x000DEAC8
		public bool Equals(ArrayWithOffset obj)
		{
			return obj.m_array == this.m_array && obj.m_offset == this.m_offset && obj.m_count == this.m_count;
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x000E08F6 File Offset: 0x000DEAF6
		public static bool operator ==(ArrayWithOffset a, ArrayWithOffset b)
		{
			return a.Equals(b);
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x000E0900 File Offset: 0x000DEB00
		public static bool operator !=(ArrayWithOffset a, ArrayWithOffset b)
		{
			return !(a == b);
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x000E090C File Offset: 0x000DEB0C
		private int CalculateCount()
		{
			Array array = this.m_array as Array;
			if (array == null)
			{
				throw new ArgumentException();
			}
			return array.Rank * array.Length - this.m_offset;
		}

		// Token: 0x040029A5 RID: 10661
		private object m_array;

		// Token: 0x040029A6 RID: 10662
		private int m_offset;

		// Token: 0x040029A7 RID: 10663
		private int m_count;
	}
}
