using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000638 RID: 1592
	internal class ObjectHolderListEnumerator
	{
		// Token: 0x06003CE6 RID: 15590 RVA: 0x000D405E File Offset: 0x000D225E
		internal ObjectHolderListEnumerator(ObjectHolderList list, bool isFixupEnumerator)
		{
			this.m_list = list;
			this.m_startingVersion = this.m_list.Version;
			this.m_currPos = -1;
			this.m_isFixupEnumerator = isFixupEnumerator;
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x000D408C File Offset: 0x000D228C
		internal bool MoveNext()
		{
			if (this.m_isFixupEnumerator)
			{
				int num;
				do
				{
					num = this.m_currPos + 1;
					this.m_currPos = num;
				}
				while (num < this.m_list.Count && this.m_list.m_values[this.m_currPos].CompletelyFixed);
				return this.m_currPos != this.m_list.Count;
			}
			this.m_currPos++;
			return this.m_currPos != this.m_list.Count;
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06003CE8 RID: 15592 RVA: 0x000D4113 File Offset: 0x000D2313
		internal ObjectHolder Current
		{
			get
			{
				return this.m_list.m_values[this.m_currPos];
			}
		}

		// Token: 0x040026F8 RID: 9976
		private bool m_isFixupEnumerator;

		// Token: 0x040026F9 RID: 9977
		private ObjectHolderList m_list;

		// Token: 0x040026FA RID: 9978
		private int m_startingVersion;

		// Token: 0x040026FB RID: 9979
		private int m_currPos;
	}
}
