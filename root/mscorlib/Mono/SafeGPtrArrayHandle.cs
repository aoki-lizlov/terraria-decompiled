using System;

namespace Mono
{
	// Token: 0x0200003E RID: 62
	internal struct SafeGPtrArrayHandle : IDisposable
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00004565 File Offset: 0x00002765
		internal SafeGPtrArrayHandle(IntPtr ptr)
		{
			this.handle = new RuntimeGPtrArrayHandle(ptr);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004573 File Offset: 0x00002773
		public void Dispose()
		{
			RuntimeGPtrArrayHandle.DestroyAndFree(ref this.handle);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004580 File Offset: 0x00002780
		internal int Length
		{
			get
			{
				return this.handle.Length;
			}
		}

		// Token: 0x17000011 RID: 17
		internal IntPtr this[int i]
		{
			get
			{
				return this.handle[i];
			}
		}

		// Token: 0x04000D0E RID: 3342
		private RuntimeGPtrArrayHandle handle;
	}
}
