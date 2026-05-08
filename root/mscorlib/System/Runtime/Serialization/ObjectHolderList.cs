using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000637 RID: 1591
	internal class ObjectHolderList
	{
		// Token: 0x06003CDF RID: 15583 RVA: 0x000D3F8E File Offset: 0x000D218E
		internal ObjectHolderList()
			: this(8)
		{
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x000D3F97 File Offset: 0x000D2197
		internal ObjectHolderList(int startingSize)
		{
			this.m_count = 0;
			this.m_values = new ObjectHolder[startingSize];
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x000D3FB4 File Offset: 0x000D21B4
		internal virtual void Add(ObjectHolder value)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			ObjectHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count] = value;
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x000D3FF0 File Offset: 0x000D21F0
		internal ObjectHolderListEnumerator GetFixupEnumerator()
		{
			return new ObjectHolderListEnumerator(this, true);
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x000D3FFC File Offset: 0x000D21FC
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
			ObjectHolder[] array = new ObjectHolder[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06003CE4 RID: 15588 RVA: 0x000D4056 File Offset: 0x000D2256
		internal int Version
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06003CE5 RID: 15589 RVA: 0x000D4056 File Offset: 0x000D2256
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x040026F5 RID: 9973
		internal const int DefaultInitialSize = 8;

		// Token: 0x040026F6 RID: 9974
		internal ObjectHolder[] m_values;

		// Token: 0x040026F7 RID: 9975
		internal int m_count;
	}
}
