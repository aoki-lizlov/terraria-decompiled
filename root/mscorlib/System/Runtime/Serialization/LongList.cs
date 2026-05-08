using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000636 RID: 1590
	[Serializable]
	internal class LongList
	{
		// Token: 0x06003CD6 RID: 15574 RVA: 0x000D3E12 File Offset: 0x000D2012
		internal LongList()
			: this(2)
		{
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x000D3E1B File Offset: 0x000D201B
		internal LongList(int startingSize)
		{
			this.m_count = 0;
			this.m_totalItems = 0;
			this.m_values = new long[startingSize];
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x000D3E40 File Offset: 0x000D2040
		internal void Add(long value)
		{
			if (this.m_totalItems == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			long[] values = this.m_values;
			int totalItems = this.m_totalItems;
			this.m_totalItems = totalItems + 1;
			values[totalItems] = value;
			this.m_count++;
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06003CD9 RID: 15577 RVA: 0x000D3E8A File Offset: 0x000D208A
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x000D3E92 File Offset: 0x000D2092
		internal void StartEnumeration()
		{
			this.m_currentItem = -1;
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x000D3E9C File Offset: 0x000D209C
		internal bool MoveNext()
		{
			int num;
			do
			{
				num = this.m_currentItem + 1;
				this.m_currentItem = num;
			}
			while (num < this.m_totalItems && this.m_values[this.m_currentItem] == -1L);
			return this.m_currentItem != this.m_totalItems;
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06003CDC RID: 15580 RVA: 0x000D3EE4 File Offset: 0x000D20E4
		internal long Current
		{
			get
			{
				return this.m_values[this.m_currentItem];
			}
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x000D3EF4 File Offset: 0x000D20F4
		internal bool RemoveElement(long value)
		{
			int num = 0;
			while (num < this.m_totalItems && this.m_values[num] != value)
			{
				num++;
			}
			if (num == this.m_totalItems)
			{
				return false;
			}
			this.m_values[num] = -1L;
			return true;
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x000D3F34 File Offset: 0x000D2134
		private void EnlargeArray()
		{
			int num = this.m_values.Length * 2;
			if (num < 0)
			{
				if (num == 2147483647)
				{
					throw new SerializationException(Environment.GetResourceString("The internal array cannot expand to greater than Int32.MaxValue elements."));
				}
				num = int.MaxValue;
			}
			long[] array = new long[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x040026F0 RID: 9968
		private const int InitialSize = 2;

		// Token: 0x040026F1 RID: 9969
		private long[] m_values;

		// Token: 0x040026F2 RID: 9970
		private int m_count;

		// Token: 0x040026F3 RID: 9971
		private int m_totalItems;

		// Token: 0x040026F4 RID: 9972
		private int m_currentItem;
	}
}
