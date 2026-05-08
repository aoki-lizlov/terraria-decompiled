using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A43 RID: 2627
	[Flags]
	public enum EventKeywords : long
	{
		// Token: 0x04003A31 RID: 14897
		None = 0L,
		// Token: 0x04003A32 RID: 14898
		All = -1L,
		// Token: 0x04003A33 RID: 14899
		MicrosoftTelemetry = 562949953421312L,
		// Token: 0x04003A34 RID: 14900
		WdiContext = 562949953421312L,
		// Token: 0x04003A35 RID: 14901
		WdiDiagnostic = 1125899906842624L,
		// Token: 0x04003A36 RID: 14902
		Sqm = 2251799813685248L,
		// Token: 0x04003A37 RID: 14903
		AuditFailure = 4503599627370496L,
		// Token: 0x04003A38 RID: 14904
		AuditSuccess = 9007199254740992L,
		// Token: 0x04003A39 RID: 14905
		CorrelationHint = 4503599627370496L,
		// Token: 0x04003A3A RID: 14906
		EventLogClassic = 36028797018963968L
	}
}
