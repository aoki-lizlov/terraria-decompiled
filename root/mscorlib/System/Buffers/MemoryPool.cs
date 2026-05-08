using System;

namespace System.Buffers
{
	// Token: 0x02000B3B RID: 2875
	public abstract class MemoryPool<T> : IDisposable
	{
		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x06006920 RID: 26912 RVA: 0x001646A1 File Offset: 0x001628A1
		public static MemoryPool<T> Shared
		{
			get
			{
				return MemoryPool<T>.s_shared;
			}
		}

		// Token: 0x06006921 RID: 26913
		public abstract IMemoryOwner<T> Rent(int minBufferSize = -1);

		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x06006922 RID: 26914
		public abstract int MaxBufferSize { get; }

		// Token: 0x06006923 RID: 26915 RVA: 0x000025BE File Offset: 0x000007BE
		protected MemoryPool()
		{
		}

		// Token: 0x06006924 RID: 26916 RVA: 0x001646A8 File Offset: 0x001628A8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06006925 RID: 26917
		protected abstract void Dispose(bool disposing);

		// Token: 0x06006926 RID: 26918 RVA: 0x001646B7 File Offset: 0x001628B7
		// Note: this type is marked as 'beforefieldinit'.
		static MemoryPool()
		{
		}

		// Token: 0x04003C88 RID: 15496
		private static readonly MemoryPool<T> s_shared = new ArrayMemoryPool<T>();
	}
}
