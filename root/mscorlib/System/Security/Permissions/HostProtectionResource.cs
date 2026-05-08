using System;

namespace System.Security.Permissions
{
	// Token: 0x020003FA RID: 1018
	[Flags]
	public enum HostProtectionResource
	{
		// Token: 0x04001E9E RID: 7838
		All = 511,
		// Token: 0x04001E9F RID: 7839
		ExternalProcessMgmt = 4,
		// Token: 0x04001EA0 RID: 7840
		ExternalThreading = 16,
		// Token: 0x04001EA1 RID: 7841
		MayLeakOnAbort = 256,
		// Token: 0x04001EA2 RID: 7842
		None = 0,
		// Token: 0x04001EA3 RID: 7843
		SecurityInfrastructure = 64,
		// Token: 0x04001EA4 RID: 7844
		SelfAffectingProcessMgmt = 8,
		// Token: 0x04001EA5 RID: 7845
		SelfAffectingThreading = 32,
		// Token: 0x04001EA6 RID: 7846
		SharedState = 2,
		// Token: 0x04001EA7 RID: 7847
		Synchronization = 1,
		// Token: 0x04001EA8 RID: 7848
		UI = 128
	}
}
