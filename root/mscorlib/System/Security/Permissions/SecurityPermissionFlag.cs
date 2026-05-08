using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000429 RID: 1065
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum SecurityPermissionFlag
	{
		// Token: 0x04001F64 RID: 8036
		NoFlags = 0,
		// Token: 0x04001F65 RID: 8037
		Assertion = 1,
		// Token: 0x04001F66 RID: 8038
		UnmanagedCode = 2,
		// Token: 0x04001F67 RID: 8039
		SkipVerification = 4,
		// Token: 0x04001F68 RID: 8040
		Execution = 8,
		// Token: 0x04001F69 RID: 8041
		ControlThread = 16,
		// Token: 0x04001F6A RID: 8042
		ControlEvidence = 32,
		// Token: 0x04001F6B RID: 8043
		ControlPolicy = 64,
		// Token: 0x04001F6C RID: 8044
		SerializationFormatter = 128,
		// Token: 0x04001F6D RID: 8045
		ControlDomainPolicy = 256,
		// Token: 0x04001F6E RID: 8046
		ControlPrincipal = 512,
		// Token: 0x04001F6F RID: 8047
		ControlAppDomain = 1024,
		// Token: 0x04001F70 RID: 8048
		RemotingConfiguration = 2048,
		// Token: 0x04001F71 RID: 8049
		Infrastructure = 4096,
		// Token: 0x04001F72 RID: 8050
		BindingRedirects = 8192,
		// Token: 0x04001F73 RID: 8051
		AllFlags = 16383
	}
}
