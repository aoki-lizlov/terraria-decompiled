using System;
using System.IO;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000099 RID: 153
	[SecurityCritical]
	public sealed class SafeFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060004A7 RID: 1191 RVA: 0x00018256 File Offset: 0x00016456
		private SafeFileHandle()
			: base(true)
		{
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001825F File Offset: 0x0001645F
		public SafeFileHandle(IntPtr preexistingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00018270 File Offset: 0x00016470
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			MonoIOError monoIOError;
			MonoIO.Close(this.handle, out monoIOError);
			return monoIOError == MonoIOError.ERROR_SUCCESS;
		}
	}
}
