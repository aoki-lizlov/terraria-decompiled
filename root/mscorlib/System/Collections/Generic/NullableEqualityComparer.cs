using System;

namespace System.Collections.Generic
{
	// Token: 0x02000B1F RID: 2847
	[Serializable]
	internal class NullableEqualityComparer<T> : EqualityComparer<T?> where T : struct, IEquatable<T>
	{
		// Token: 0x0600689F RID: 26783 RVA: 0x001630F9 File Offset: 0x001612F9
		public override bool Equals(T? x, T? y)
		{
			if (x != null)
			{
				return y != null && x.value.Equals(y.value);
			}
			return y == null;
		}

		// Token: 0x060068A0 RID: 26784 RVA: 0x00163134 File Offset: 0x00161334
		public override int GetHashCode(T? obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x060068A1 RID: 26785 RVA: 0x00163144 File Offset: 0x00161344
		internal override int IndexOf(T?[] array, T? value, int startIndex, int count)
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
					if (array[j] != null && array[j].value.Equals(value.value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x060068A2 RID: 26786 RVA: 0x001631BC File Offset: 0x001613BC
		internal override int LastIndexOf(T?[] array, T? value, int startIndex, int count)
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
					if (array[j] != null && array[j].value.Equals(value.value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x060068A3 RID: 26787 RVA: 0x00163233 File Offset: 0x00161433
		public override bool Equals(object obj)
		{
			return obj is NullableEqualityComparer<T>;
		}

		// Token: 0x060068A4 RID: 26788 RVA: 0x00162CCA File Offset: 0x00160ECA
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x060068A5 RID: 26789 RVA: 0x0016323E File Offset: 0x0016143E
		public NullableEqualityComparer()
		{
		}
	}
}
