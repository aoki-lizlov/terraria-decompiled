using System;
using System.IO;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200009A RID: 154
	[SecurityCritical]
	internal sealed class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x00018256 File Offset: 0x00016456
		[SecurityCritical]
		internal SafeFindHandle()
			: base(true)
		{
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001828F File Offset: 0x0001648F
		internal SafeFindHandle(IntPtr preexistingHandle)
			: base(true)
		{
			base.SetHandle(preexistingHandle);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001829F File Offset: 0x0001649F
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return MonoIO.FindCloseFile(this.handle);
		}
	}
}
