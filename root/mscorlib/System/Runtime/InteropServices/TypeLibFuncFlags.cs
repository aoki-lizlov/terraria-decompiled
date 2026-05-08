using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006C7 RID: 1735
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibFuncFlags
	{
		// Token: 0x040029D7 RID: 10711
		FRestricted = 1,
		// Token: 0x040029D8 RID: 10712
		FSource = 2,
		// Token: 0x040029D9 RID: 10713
		FBindable = 4,
		// Token: 0x040029DA RID: 10714
		FRequestEdit = 8,
		// Token: 0x040029DB RID: 10715
		FDisplayBind = 16,
		// Token: 0x040029DC RID: 10716
		FDefaultBind = 32,
		// Token: 0x040029DD RID: 10717
		FHidden = 64,
		// Token: 0x040029DE RID: 10718
		FUsesGetLastError = 128,
		// Token: 0x040029DF RID: 10719
		FDefaultCollelem = 256,
		// Token: 0x040029E0 RID: 10720
		FUiDefault = 512,
		// Token: 0x040029E1 RID: 10721
		FNonBrowsable = 1024,
		// Token: 0x040029E2 RID: 10722
		FReplaceable = 2048,
		// Token: 0x040029E3 RID: 10723
		FImmediateBind = 4096
	}
}
