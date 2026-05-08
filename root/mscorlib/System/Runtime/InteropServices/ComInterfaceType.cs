using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006B6 RID: 1718
	[ComVisible(true)]
	[Serializable]
	public enum ComInterfaceType
	{
		// Token: 0x040029B1 RID: 10673
		InterfaceIsDual,
		// Token: 0x040029B2 RID: 10674
		InterfaceIsIUnknown,
		// Token: 0x040029B3 RID: 10675
		InterfaceIsIDispatch,
		// Token: 0x040029B4 RID: 10676
		[ComVisible(false)]
		InterfaceIsIInspectable
	}
}
