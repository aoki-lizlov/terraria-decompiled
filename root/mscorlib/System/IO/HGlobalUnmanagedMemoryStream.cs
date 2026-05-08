using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000982 RID: 2434
	internal class HGlobalUnmanagedMemoryStream : UnmanagedMemoryStream
	{
		// Token: 0x06005898 RID: 22680 RVA: 0x0012BF93 File Offset: 0x0012A193
		public unsafe HGlobalUnmanagedMemoryStream(byte* pointer, long length, IntPtr ptr)
			: base(pointer, length, length, FileAccess.ReadWrite)
		{
			this.ptr = ptr;
		}

		// Token: 0x06005899 RID: 22681 RVA: 0x0012BFA6 File Offset: 0x0012A1A6
		protected override void Dispose(bool disposing)
		{
			if (this._isOpen)
			{
				Marshal.FreeHGlobal(this.ptr);
			}
			base.Dispose(disposing);
		}

		// Token: 0x04003530 RID: 13616
		private IntPtr ptr;
	}
}
