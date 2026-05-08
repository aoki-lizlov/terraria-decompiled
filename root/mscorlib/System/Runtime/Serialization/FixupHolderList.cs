using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000635 RID: 1589
	[Serializable]
	internal class FixupHolderList
	{
		// Token: 0x06003CD1 RID: 15569 RVA: 0x000D3D03 File Offset: 0x000D1F03
		internal FixupHolderList()
			: this(2)
		{
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x000D3D0C File Offset: 0x000D1F0C
		internal FixupHolderList(int startingSize)
		{
			this.m_count = 0;
			this.m_values = new FixupHolder[startingSize];
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x000D3D28 File Offset: 0x000D1F28
		internal virtual void Add(long id, object fixupInfo)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			this.m_values[this.m_count].m_id = id;
			FixupHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count].m_fixupInfo = fixupInfo;
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x000D3D7C File Offset: 0x000D1F7C
		internal virtual void Add(FixupHolder fixup)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			FixupHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count] = fixup;
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x000D3DB8 File Offset: 0x000D1FB8
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
			FixupHolder[] array = new FixupHolder[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x040026ED RID: 9965
		internal const int InitialSize = 2;

		// Token: 0x040026EE RID: 9966
		internal FixupHolder[] m_values;

		// Token: 0x040026EF RID: 9967
		internal int m_count;
	}
}
