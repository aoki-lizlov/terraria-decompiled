using System;

namespace System.Security.AccessControl
{
	// Token: 0x020004D6 RID: 1238
	public enum AceType : byte
	{
		// Token: 0x040023A7 RID: 9127
		AccessAllowed,
		// Token: 0x040023A8 RID: 9128
		AccessDenied,
		// Token: 0x040023A9 RID: 9129
		SystemAudit,
		// Token: 0x040023AA RID: 9130
		SystemAlarm,
		// Token: 0x040023AB RID: 9131
		AccessAllowedCompound,
		// Token: 0x040023AC RID: 9132
		AccessAllowedObject,
		// Token: 0x040023AD RID: 9133
		AccessDeniedObject,
		// Token: 0x040023AE RID: 9134
		SystemAuditObject,
		// Token: 0x040023AF RID: 9135
		SystemAlarmObject,
		// Token: 0x040023B0 RID: 9136
		AccessAllowedCallback,
		// Token: 0x040023B1 RID: 9137
		AccessDeniedCallback,
		// Token: 0x040023B2 RID: 9138
		AccessAllowedCallbackObject,
		// Token: 0x040023B3 RID: 9139
		AccessDeniedCallbackObject,
		// Token: 0x040023B4 RID: 9140
		SystemAuditCallback,
		// Token: 0x040023B5 RID: 9141
		SystemAlarmCallback,
		// Token: 0x040023B6 RID: 9142
		SystemAuditCallbackObject,
		// Token: 0x040023B7 RID: 9143
		SystemAlarmCallbackObject,
		// Token: 0x040023B8 RID: 9144
		MaxDefinedAceType = 16
	}
}
