using System;

namespace System.Buffers
{
	// Token: 0x02000B46 RID: 2886
	public sealed class ArrayBufferWriter<T> : IBufferWriter<T>
	{
		// Token: 0x06006985 RID: 27013 RVA: 0x00165D13 File Offset: 0x00163F13
		public ArrayBufferWriter()
		{
			this._buffer = Array.Empty<T>();
			this._index = 0;
		}

		// Token: 0x06006986 RID: 27014 RVA: 0x00165D2D File Offset: 0x00163F2D
		public ArrayBufferWriter(int initialCapacity)
		{
			if (initialCapacity <= 0)
			{
				throw new ArgumentException("initialCapacity");
			}
			this._buffer = new T[initialCapacity];
			this._index = 0;
		}

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06006987 RID: 27015 RVA: 0x00165D57 File Offset: 0x00163F57
		public ReadOnlyMemory<T> WrittenMemory
		{
			get
			{
				return this._buffer.AsMemory(0, this._index);
			}
		}

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06006988 RID: 27016 RVA: 0x00165D70 File Offset: 0x00163F70
		public ReadOnlySpan<T> WrittenSpan
		{
			get
			{
				return this._buffer.AsSpan(0, this._index);
			}
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06006989 RID: 27017 RVA: 0x00165D89 File Offset: 0x00163F89
		public int WrittenCount
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x0600698A RID: 27018 RVA: 0x00165D91 File Offset: 0x00163F91
		public int Capacity
		{
			get
			{
				return this._buffer.Length;
			}
		}

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x0600698B RID: 27019 RVA: 0x00165D9B File Offset: 0x00163F9B
		public int FreeCapacity
		{
			get
			{
				return this._buffer.Length - this._index;
			}
		}

		// Token: 0x0600698C RID: 27020 RVA: 0x00165DAC File Offset: 0x00163FAC
		public void Clear()
		{
			this._buffer.AsSpan(0, this._index).Clear();
			this._index = 0;
		}

		// Token: 0x0600698D RID: 27021 RVA: 0x00165DDA File Offset: 0x00163FDA
		public void Advance(int count)
		{
			if (count < 0)
			{
				throw new ArgumentException("count");
			}
			if (this._index > this._buffer.Length - count)
			{
				ArrayBufferWriter<T>.ThrowInvalidOperationException_AdvancedTooFar(this._buffer.Length);
			}
			this._index += count;
		}

		// Token: 0x0600698E RID: 27022 RVA: 0x00165E18 File Offset: 0x00164018
		public Memory<T> GetMemory(int sizeHint = 0)
		{
			this.CheckAndResizeBuffer(sizeHint);
			return this._buffer.AsMemory(this._index);
		}

		// Token: 0x0600698F RID: 27023 RVA: 0x00165E32 File Offset: 0x00164032
		public Span<T> GetSpan(int sizeHint = 0)
		{
			this.CheckAndResizeBuffer(sizeHint);
			return this._buffer.AsSpan(this._index);
		}

		// Token: 0x06006990 RID: 27024 RVA: 0x00165E4C File Offset: 0x0016404C
		private void CheckAndResizeBuffer(int sizeHint)
		{
			if (sizeHint < 0)
			{
				throw new ArgumentException("sizeHint");
			}
			if (sizeHint == 0)
			{
				sizeHint = 1;
			}
			if (sizeHint > this.FreeCapacity)
			{
				int num = Math.Max(sizeHint, this._buffer.Length);
				if (this._buffer.Length == 0)
				{
					num = Math.Max(num, 256);
				}
				int num2 = checked(this._buffer.Length + num);
				Array.Resize<T>(ref this._buffer, num2);
			}
		}

		// Token: 0x06006991 RID: 27025 RVA: 0x00165EB2 File Offset: 0x001640B2
		private static void ThrowInvalidOperationException_AdvancedTooFar(int capacity)
		{
			throw new InvalidOperationException(SR.Format("Cannot advance past the end of the buffer, which has a size of {0}.", capacity));
		}

		// Token: 0x04003CB3 RID: 15539
		private T[] _buffer;

		// Token: 0x04003CB4 RID: 15540
		private int _index;

		// Token: 0x04003CB5 RID: 15541
		private const int DefaultInitialBufferSize = 256;
	}
}
