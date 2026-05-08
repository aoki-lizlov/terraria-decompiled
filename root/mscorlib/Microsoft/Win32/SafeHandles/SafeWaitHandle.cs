using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200009C RID: 156
	[SecurityCritical]
	public sealed class SafeWaitHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x00018256 File Offset: 0x00016456
		private SafeWaitHandle()
			: base(true)
		{
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001825F File Offset: 0x0001645F
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public SafeWaitHandle(IntPtr existingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000182BC File Offset: 0x000164BC
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			NativeEventCalls.CloseEvent_internal(this.handle);
			return true;
		}
	}
}
