using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000590 RID: 1424
	public class DoubleStack<T1>
	{
		// Token: 0x06003849 RID: 14409 RVA: 0x00631894 File Offset: 0x0062FA94
		public DoubleStack(int segmentSize = 1024, int initialSize = 0)
		{
			if (segmentSize < 16)
			{
				segmentSize = 16;
			}
			this._start = segmentSize / 2;
			this._end = this._start;
			this._size = 0;
			this._segmentShiftPosition = segmentSize + this._start;
			initialSize += this._start;
			int num = initialSize / segmentSize + 1;
			this._segmentList = new T1[num][];
			for (int i = 0; i < num; i++)
			{
				this._segmentList[i] = new T1[segmentSize];
			}
			this._segmentSize = segmentSize;
			this._segmentCount = num;
			this._last = this._segmentSize * this._segmentCount - 1;
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x00631934 File Offset: 0x0062FB34
		public void PushFront(T1 front)
		{
			if (this._start == 0)
			{
				T1[][] array = new T1[this._segmentCount + 1][];
				for (int i = 0; i < this._segmentCount; i++)
				{
					array[i + 1] = this._segmentList[i];
				}
				array[0] = new T1[this._segmentSize];
				this._segmentList = array;
				this._segmentCount++;
				this._start += this._segmentSize;
				this._end += this._segmentSize;
				this._last += this._segmentSize;
			}
			this._start--;
			T1[] array2 = this._segmentList[this._start / this._segmentSize];
			int num = this._start % this._segmentSize;
			array2[num] = front;
			this._size++;
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x00631A1C File Offset: 0x0062FC1C
		public T1 PopFront()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException("The DoubleStack is empty.");
			}
			T1[] array = this._segmentList[this._start / this._segmentSize];
			int num = this._start % this._segmentSize;
			T1 t = array[num];
			array[num] = default(T1);
			this._start++;
			this._size--;
			if (this._start >= this._segmentShiftPosition)
			{
				T1[] array2 = this._segmentList[0];
				for (int i = 0; i < this._segmentCount - 1; i++)
				{
					this._segmentList[i] = this._segmentList[i + 1];
				}
				this._segmentList[this._segmentCount - 1] = array2;
				this._start -= this._segmentSize;
				this._end -= this._segmentSize;
			}
			if (this._size == 0)
			{
				this._start = this._segmentSize / 2;
				this._end = this._start;
			}
			return t;
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x00631B2C File Offset: 0x0062FD2C
		public T1 PeekFront()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException("The DoubleStack is empty.");
			}
			T1[] array = this._segmentList[this._start / this._segmentSize];
			int num = this._start % this._segmentSize;
			return array[num];
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x00631B74 File Offset: 0x0062FD74
		public void PushBack(T1 back)
		{
			if (this._end == this._last)
			{
				T1[][] array = new T1[this._segmentCount + 1][];
				for (int i = 0; i < this._segmentCount; i++)
				{
					array[i] = this._segmentList[i];
				}
				array[this._segmentCount] = new T1[this._segmentSize];
				this._segmentCount++;
				this._segmentList = array;
				this._last += this._segmentSize;
			}
			T1[] array2 = this._segmentList[this._end / this._segmentSize];
			int num = this._end % this._segmentSize;
			array2[num] = back;
			this._end++;
			this._size++;
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x00631C3C File Offset: 0x0062FE3C
		public T1 PopBack()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException("The DoubleStack is empty.");
			}
			T1[] array = this._segmentList[this._end / this._segmentSize];
			int num = this._end % this._segmentSize;
			T1 t = array[num];
			array[num] = default(T1);
			this._end--;
			this._size--;
			if (this._size == 0)
			{
				this._start = this._segmentSize / 2;
				this._end = this._start;
			}
			return t;
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x00631CD4 File Offset: 0x0062FED4
		public T1 PeekBack()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException("The DoubleStack is empty.");
			}
			T1[] array = this._segmentList[this._end / this._segmentSize];
			int num = this._end % this._segmentSize;
			return array[num];
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x00631D1C File Offset: 0x0062FF1C
		public void Clear(bool quickClear = false)
		{
			if (!quickClear)
			{
				for (int i = 0; i < this._segmentCount; i++)
				{
					Array.Clear(this._segmentList[i], 0, this._segmentSize);
				}
			}
			this._start = this._segmentSize / 2;
			this._end = this._start;
			this._size = 0;
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06003851 RID: 14417 RVA: 0x00631D72 File Offset: 0x0062FF72
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x04005C6F RID: 23663
		private T1[][] _segmentList;

		// Token: 0x04005C70 RID: 23664
		private readonly int _segmentSize;

		// Token: 0x04005C71 RID: 23665
		private int _segmentCount;

		// Token: 0x04005C72 RID: 23666
		private readonly int _segmentShiftPosition;

		// Token: 0x04005C73 RID: 23667
		private int _start;

		// Token: 0x04005C74 RID: 23668
		private int _end;

		// Token: 0x04005C75 RID: 23669
		private int _size;

		// Token: 0x04005C76 RID: 23670
		private int _last;
	}
}
