using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006C6 RID: 1734
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibTypeFlags
	{
		// Token: 0x040029C8 RID: 10696
		FAppObject = 1,
		// Token: 0x040029C9 RID: 10697
		FCanCreate = 2,
		// Token: 0x040029CA RID: 10698
		FLicensed = 4,
		// Token: 0x040029CB RID: 10699
		FPreDeclId = 8,
		// Token: 0x040029CC RID: 10700
		FHidden = 16,
		// Token: 0x040029CD RID: 10701
		FControl = 32,
		// Token: 0x040029CE RID: 10702
		FDual = 64,
		// Token: 0x040029CF RID: 10703
		FNonExtensible = 128,
		// Token: 0x040029D0 RID: 10704
		FOleAutomation = 256,
		// Token: 0x040029D1 RID: 10705
		FRestricted = 512,
		// Token: 0x040029D2 RID: 10706
		FAggregatable = 1024,
		// Token: 0x040029D3 RID: 10707
		FReplaceable = 2048,
		// Token: 0x040029D4 RID: 10708
		FDispatchable = 4096,
		// Token: 0x040029D5 RID: 10709
		FReverseBind = 8192
	}
}
