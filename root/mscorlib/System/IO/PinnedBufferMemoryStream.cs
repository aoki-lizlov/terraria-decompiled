using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x0200092E RID: 2350
	internal sealed class PinnedBufferMemoryStream : UnmanagedMemoryStream
	{
		// Token: 0x060053DA RID: 21466 RVA: 0x0011A0F0 File Offset: 0x001182F0
		internal unsafe PinnedBufferMemoryStream(byte[] array)
		{
			this._array = array;
			this._pinningHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			int num = array.Length;
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(array))
			{
				byte* ptr = reference;
				base.Initialize(ptr, (long)num, (long)num, FileAccess.Read);
			}
		}

		// Token: 0x060053DB RID: 21467 RVA: 0x0011A139 File Offset: 0x00118339
		public override int Read(Span<byte> buffer)
		{
			return base.ReadCore(buffer);
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x0011A142 File Offset: 0x00118342
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			base.WriteCore(buffer);
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x0011A14C File Offset: 0x0011834C
		~PinnedBufferMemoryStream()
		{
			this.Dispose(false);
		}

		// Token: 0x060053DE RID: 21470 RVA: 0x0011A17C File Offset: 0x0011837C
		protected override void Dispose(bool disposing)
		{
			if (this._pinningHandle.IsAllocated)
			{
				this._pinningHandle.Free();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04003342 RID: 13122
		private byte[] _array;

		// Token: 0x04003343 RID: 13123
		private GCHandle _pinningHandle;
	}
}
