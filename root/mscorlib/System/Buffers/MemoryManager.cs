using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000B31 RID: 2865
	public abstract class MemoryManager<T> : IMemoryOwner<T>, IDisposable, IPinnable
	{
		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x060068F1 RID: 26865 RVA: 0x00163C18 File Offset: 0x00161E18
		public virtual Memory<T> Memory
		{
			get
			{
				return new Memory<T>(this, this.GetSpan().Length);
			}
		}

		// Token: 0x060068F2 RID: 26866
		public abstract Span<T> GetSpan();

		// Token: 0x060068F3 RID: 26867
		public abstract MemoryHandle Pin(int elementIndex = 0);

		// Token: 0x060068F4 RID: 26868
		public abstract void Unpin();

		// Token: 0x060068F5 RID: 26869 RVA: 0x00163C39 File Offset: 0x00161E39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected Memory<T> CreateMemory(int length)
		{
			return new Memory<T>(this, length);
		}

		// Token: 0x060068F6 RID: 26870 RVA: 0x00163C42 File Offset: 0x00161E42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected Memory<T> CreateMemory(int start, int length)
		{
			return new Memory<T>(this, start, length);
		}

		// Token: 0x060068F7 RID: 26871 RVA: 0x00163C4C File Offset: 0x00161E4C
		protected internal virtual bool TryGetArray(out ArraySegment<T> segment)
		{
			segment = default(ArraySegment<T>);
			return false;
		}

		// Token: 0x060068F8 RID: 26872 RVA: 0x00163C56 File Offset: 0x00161E56
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060068F9 RID: 26873
		protected abstract void Dispose(bool disposing);

		// Token: 0x060068FA RID: 26874 RVA: 0x000025BE File Offset: 0x000007BE
		protected MemoryManager()
		{
		}
	}
}
