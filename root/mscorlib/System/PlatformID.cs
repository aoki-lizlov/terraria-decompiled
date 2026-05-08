using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200021E RID: 542
	[ComVisible(true)]
	[Serializable]
	public enum PlatformID
	{
		// Token: 0x04001655 RID: 5717
		Win32S,
		// Token: 0x04001656 RID: 5718
		Win32Windows,
		// Token: 0x04001657 RID: 5719
		Win32NT,
		// Token: 0x04001658 RID: 5720
		WinCE,
		// Token: 0x04001659 RID: 5721
		Unix,
		// Token: 0x0400165A RID: 5722
		Xbox,
		// Token: 0x0400165B RID: 5723
		MacOSX
	}
}
