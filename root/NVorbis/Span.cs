using System;

namespace NVorbis
{
	// Token: 0x02000010 RID: 16
	internal struct Span<T>
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000044A0 File Offset: 0x000026A0
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x1700001F RID: 31
		public ref T this[int i]
		{
			get
			{
				if (i < 0 || i >= this.length)
				{
					throw new IndexOutOfRangeException();
				}
				return ref this.arr[this.start + i];
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000044D0 File Offset: 0x000026D0
		public Span(T[] arr)
		{
			this = new Span<T>(arr, 0, arr.Length);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000044DD File Offset: 0x000026DD
		public Span(T[] arr, int start, int length)
		{
			if (start + length > arr.Length)
			{
				throw new ArgumentException("end out of array bounds");
			}
			if (start < 0)
			{
				throw new ArgumentException("start < 0");
			}
			this.arr = arr;
			this.start = start;
			this.length = length;
		}

		// Token: 0x04000044 RID: 68
		private readonly T[] arr;

		// Token: 0x04000045 RID: 69
		private readonly int start;

		// Token: 0x04000046 RID: 70
		private readonly int length;
	}
}
