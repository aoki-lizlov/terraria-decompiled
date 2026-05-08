using System;
using System.Runtime.CompilerServices;

namespace NVorbis
{
	// Token: 0x0200000F RID: 15
	internal class Memory<T>
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000043A9 File Offset: 0x000025A9
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000043B1 File Offset: 0x000025B1
		public Span<T> Span
		{
			get
			{
				return new Span<T>(this.arr, this.start, this.length);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000043CA File Offset: 0x000025CA
		public static Memory<T> Empty
		{
			[CompilerGenerated]
			get
			{
				return Memory<T>.<Empty>k__BackingField;
			}
		} = new Memory<T>(new T[0]);

		// Token: 0x06000076 RID: 118 RVA: 0x000043D1 File Offset: 0x000025D1
		public Memory(T[] arr)
			: this(arr, 0, arr.Length)
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000043DE File Offset: 0x000025DE
		public Memory(T[] arr, int start, int length)
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

		// Token: 0x06000078 RID: 120 RVA: 0x0000441D File Offset: 0x0000261D
		internal void CopyTo(Memory<T> target)
		{
			if (target.Length < this.Length)
			{
				throw new ArgumentException("Target too short");
			}
			Array.Copy(this.arr, this.start, target.arr, target.start, this.length);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000445B File Offset: 0x0000265B
		internal Memory<T> Slice(int offset)
		{
			return new Memory<T>(this.arr, this.start + offset, this.length - offset);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004478 File Offset: 0x00002678
		internal Memory<T> Slice(int offset, int length)
		{
			return new Memory<T>(this.arr, this.start + offset, length);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000448E File Offset: 0x0000268E
		// Note: this type is marked as 'beforefieldinit'.
		static Memory()
		{
		}

		// Token: 0x04000040 RID: 64
		private readonly T[] arr;

		// Token: 0x04000041 RID: 65
		private readonly int start;

		// Token: 0x04000042 RID: 66
		private readonly int length;

		// Token: 0x04000043 RID: 67
		[CompilerGenerated]
		private static readonly Memory<T> <Empty>k__BackingField;
	}
}
