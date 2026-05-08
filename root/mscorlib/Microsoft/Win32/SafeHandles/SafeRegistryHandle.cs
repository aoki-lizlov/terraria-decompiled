using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200009B RID: 155
	[SecurityCritical]
	public sealed class SafeRegistryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x00018256 File Offset: 0x00016456
		[SecurityCritical]
		internal SafeRegistryHandle()
			: base(true)
		{
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001825F File Offset: 0x0001645F
		[SecurityCritical]
		public SafeRegistryHandle(IntPtr preexistingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000182AC File Offset: 0x000164AC
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeRegistryHandle.RegCloseKey(this.handle) == 0;
		}

		// Token: 0x060004B0 RID: 1200
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll")]
		internal static extern int RegCloseKey(IntPtr hKey);
	}
}
