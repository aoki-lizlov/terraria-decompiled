using System;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006E8 RID: 1768
	[ComVisible(false)]
	public interface ICustomQueryInterface
	{
		// Token: 0x0600406C RID: 16492
		[SecurityCritical]
		CustomQueryInterfaceResult GetInterface([In] ref Guid iid, out IntPtr ppv);
	}
}
