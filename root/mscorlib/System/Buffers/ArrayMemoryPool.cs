using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000B37 RID: 2871
	internal sealed class ArrayMemoryPool<T> : MemoryPool<T>
	{
		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x0600690F RID: 26895 RVA: 0x000841D2 File Offset: 0x000823D2
		public sealed override int MaxBufferSize
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x06006910 RID: 26896 RVA: 0x001643FE File Offset: 0x001625FE
		public sealed override IMemoryOwner<T> Rent(int minimumBufferSize = -1)
		{
			if (minimumBufferSize == -1)
			{
				minimumBufferSize = 1 + 4095 / Unsafe.SizeOf<T>();
			}
			else if (minimumBufferSize > 2147483647)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.minimumBufferSize);
			}
			return new ArrayMemoryPool<T>.ArrayMemoryPoolBuffer(minimumBufferSize);
		}

		// Token: 0x06006911 RID: 26897 RVA: 0x00004088 File Offset: 0x00002288
		protected sealed override void Dispose(bool disposing)
		{
		}

		// Token: 0x06006912 RID: 26898 RVA: 0x0016442A File Offset: 0x0016262A
		public ArrayMemoryPool()
		{
		}

		// Token: 0x04003C86 RID: 15494
		private const int s_maxBufferSize = 2147483647;

		// Token: 0x02000B38 RID: 2872
		private sealed class ArrayMemoryPoolBuffer : IMemoryOwner<T>, IDisposable
		{
			// Token: 0x06006913 RID: 26899 RVA: 0x00164432 File Offset: 0x00162632
			public ArrayMemoryPoolBuffer(int size)
			{
				this._array = ArrayPool<T>.Shared.Rent(size);
			}

			// Token: 0x1700124F RID: 4687
			// (get) Token: 0x06006914 RID: 26900 RVA: 0x0016444B File Offset: 0x0016264B
			public Memory<T> Memory
			{
				get
				{
					T[] array = this._array;
					if (array == null)
					{
						ThrowHelper.ThrowObjectDisposedException_ArrayMemoryPoolBuffer();
					}
					return new Memory<T>(array);
				}
			}

			// Token: 0x06006915 RID: 26901 RVA: 0x00164460 File Offset: 0x00162660
			public void Dispose()
			{
				T[] array = this._array;
				if (array != null)
				{
					this._array = null;
					ArrayPool<T>.Shared.Return(array, false);
				}
			}

			// Token: 0x04003C87 RID: 15495
			private T[] _array;
		}
	}
}
