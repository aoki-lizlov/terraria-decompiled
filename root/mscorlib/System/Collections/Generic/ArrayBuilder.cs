using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AFE RID: 2814
	internal struct ArrayBuilder<T>
	{
		// Token: 0x060067A1 RID: 26529 RVA: 0x0015F4EE File Offset: 0x0015D6EE
		public ArrayBuilder(int capacity)
		{
			this = default(ArrayBuilder<T>);
			if (capacity > 0)
			{
				this._array = new T[capacity];
			}
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x060067A2 RID: 26530 RVA: 0x0015F507 File Offset: 0x0015D707
		public int Capacity
		{
			get
			{
				T[] array = this._array;
				if (array == null)
				{
					return 0;
				}
				return array.Length;
			}
		}

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x060067A3 RID: 26531 RVA: 0x0015F517 File Offset: 0x0015D717
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17001227 RID: 4647
		public T this[int index]
		{
			get
			{
				return this._array[index];
			}
			set
			{
				this._array[index] = value;
			}
		}

		// Token: 0x060067A6 RID: 26534 RVA: 0x0015F53C File Offset: 0x0015D73C
		public void Add(T item)
		{
			if (this._count == this.Capacity)
			{
				this.EnsureCapacity(this._count + 1);
			}
			this.UncheckedAdd(item);
		}

		// Token: 0x060067A7 RID: 26535 RVA: 0x0015F561 File Offset: 0x0015D761
		public T First()
		{
			return this._array[0];
		}

		// Token: 0x060067A8 RID: 26536 RVA: 0x0015F56F File Offset: 0x0015D76F
		public T Last()
		{
			return this._array[this._count - 1];
		}

		// Token: 0x060067A9 RID: 26537 RVA: 0x0015F584 File Offset: 0x0015D784
		public T[] ToArray()
		{
			if (this._count == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = this._array;
			if (this._count < array.Length)
			{
				array = new T[this._count];
				Array.Copy(this._array, 0, array, 0, this._count);
			}
			return array;
		}

		// Token: 0x060067AA RID: 26538 RVA: 0x0015F5D4 File Offset: 0x0015D7D4
		public void UncheckedAdd(T item)
		{
			T[] array = this._array;
			int count = this._count;
			this._count = count + 1;
			array[count] = item;
		}

		// Token: 0x060067AB RID: 26539 RVA: 0x0015F600 File Offset: 0x0015D800
		private void EnsureCapacity(int minimum)
		{
			int capacity = this.Capacity;
			int num = ((capacity == 0) ? 4 : (2 * capacity));
			if (num > 2146435071)
			{
				num = Math.Max(capacity + 1, 2146435071);
			}
			num = Math.Max(num, minimum);
			T[] array = new T[num];
			if (this._count > 0)
			{
				Array.Copy(this._array, 0, array, 0, this._count);
			}
			this._array = array;
		}

		// Token: 0x04003C22 RID: 15394
		private const int DefaultCapacity = 4;

		// Token: 0x04003C23 RID: 15395
		private const int MaxCoreClrArrayLength = 2146435071;

		// Token: 0x04003C24 RID: 15396
		private T[] _array;

		// Token: 0x04003C25 RID: 15397
		private int _count;
	}
}
