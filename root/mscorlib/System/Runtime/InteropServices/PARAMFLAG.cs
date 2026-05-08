using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006F8 RID: 1784
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.PARAMFLAG instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum PARAMFLAG : short
	{
		// Token: 0x04002AD6 RID: 10966
		PARAMFLAG_NONE = 0,
		// Token: 0x04002AD7 RID: 10967
		PARAMFLAG_FIN = 1,
		// Token: 0x04002AD8 RID: 10968
		PARAMFLAG_FOUT = 2,
		// Token: 0x04002AD9 RID: 10969
		PARAMFLAG_FLCID = 4,
		// Token: 0x04002ADA RID: 10970
		PARAMFLAG_FRETVAL = 8,
		// Token: 0x04002ADB RID: 10971
		PARAMFLAG_FOPT = 16,
		// Token: 0x04002ADC RID: 10972
		PARAMFLAG_FHASDEFAULT = 32,
		// Token: 0x04002ADD RID: 10973
		PARAMFLAG_FHASCUSTDATA = 64
	}
}
