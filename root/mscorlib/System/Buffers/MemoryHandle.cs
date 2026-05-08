using System;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000B30 RID: 2864
	public struct MemoryHandle : IDisposable
	{
		// Token: 0x060068EE RID: 26862 RVA: 0x00163BBC File Offset: 0x00161DBC
		[CLSCompliant(false)]
		public unsafe MemoryHandle(void* pointer, GCHandle handle = default(GCHandle), IPinnable pinnable = null)
		{
			this._pointer = pointer;
			this._handle = handle;
			this._pinnable = pinnable;
		}

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x060068EF RID: 26863 RVA: 0x00163BD3 File Offset: 0x00161DD3
		[CLSCompliant(false)]
		public unsafe void* Pointer
		{
			get
			{
				return this._pointer;
			}
		}

		// Token: 0x060068F0 RID: 26864 RVA: 0x00163BDB File Offset: 0x00161DDB
		public void Dispose()
		{
			if (this._handle.IsAllocated)
			{
				this._handle.Free();
			}
			if (this._pinnable != null)
			{
				this._pinnable.Unpin();
				this._pinnable = null;
			}
			this._pointer = null;
		}

		// Token: 0x04003C72 RID: 15474
		private unsafe void* _pointer;

		// Token: 0x04003C73 RID: 15475
		private GCHandle _handle;

		// Token: 0x04003C74 RID: 15476
		private IPinnable _pinnable;
	}
}
