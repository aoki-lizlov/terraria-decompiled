using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006C8 RID: 1736
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibVarFlags
	{
		// Token: 0x040029E5 RID: 10725
		FReadOnly = 1,
		// Token: 0x040029E6 RID: 10726
		FSource = 2,
		// Token: 0x040029E7 RID: 10727
		FBindable = 4,
		// Token: 0x040029E8 RID: 10728
		FRequestEdit = 8,
		// Token: 0x040029E9 RID: 10729
		FDisplayBind = 16,
		// Token: 0x040029EA RID: 10730
		FDefaultBind = 32,
		// Token: 0x040029EB RID: 10731
		FHidden = 64,
		// Token: 0x040029EC RID: 10732
		FRestricted = 128,
		// Token: 0x040029ED RID: 10733
		FDefaultCollelem = 256,
		// Token: 0x040029EE RID: 10734
		FUiDefault = 512,
		// Token: 0x040029EF RID: 10735
		FNonBrowsable = 1024,
		// Token: 0x040029F0 RID: 10736
		FReplaceable = 2048,
		// Token: 0x040029F1 RID: 10737
		FImmediateBind = 4096
	}
}
