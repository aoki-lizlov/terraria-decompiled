using System;

namespace System.Collections.Generic
{
	// Token: 0x02000B20 RID: 2848
	[Serializable]
	internal class ObjectEqualityComparer<T> : EqualityComparer<T>
	{
		// Token: 0x060068A6 RID: 26790 RVA: 0x00163246 File Offset: 0x00161446
		public override bool Equals(T x, T y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x060068A7 RID: 26791 RVA: 0x00162FF2 File Offset: 0x001611F2
		public override int GetHashCode(T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x060068A8 RID: 26792 RVA: 0x0016327C File Offset: 0x0016147C
		internal override int IndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex + count;
			if (value == null)
			{
				for (int i = startIndex; i < num; i++)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j < num; j++)
				{
					if (array[j] != null && array[j].Equals(value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x060068A9 RID: 26793 RVA: 0x001632F0 File Offset: 0x001614F0
		internal override int LastIndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			if (value == null)
			{
				for (int i = startIndex; i >= num; i--)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j >= num; j--)
				{
					if (array[j] != null && array[j].Equals(value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x060068AA RID: 26794 RVA: 0x00163363 File Offset: 0x00161563
		public override bool Equals(object obj)
		{
			return obj is ObjectEqualityComparer<T>;
		}

		// Token: 0x060068AB RID: 26795 RVA: 0x00162CCA File Offset: 0x00160ECA
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x060068AC RID: 26796 RVA: 0x001630F1 File Offset: 0x001612F1
		public ObjectEqualityComparer()
		{
		}
	}
}
