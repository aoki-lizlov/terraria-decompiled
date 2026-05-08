using System;

namespace ReLogic.Peripherals.RGB.Razer
{
	// Token: 0x02000034 RID: 52
	public enum RzResult
	{
		// Token: 0x0400009B RID: 155
		Invalid = -1,
		// Token: 0x0400009C RID: 156
		Success,
		// Token: 0x0400009D RID: 157
		AccessDenied = 5,
		// Token: 0x0400009E RID: 158
		InvalidHandle,
		// Token: 0x0400009F RID: 159
		NotSupported = 50,
		// Token: 0x040000A0 RID: 160
		InvalidParameter = 87,
		// Token: 0x040000A1 RID: 161
		ServiceNotActive = 1062,
		// Token: 0x040000A2 RID: 162
		SingleInstanceApp = 1152,
		// Token: 0x040000A3 RID: 163
		DeviceNotConnected = 1167,
		// Token: 0x040000A4 RID: 164
		NotFound,
		// Token: 0x040000A5 RID: 165
		RequestAborted = 1235,
		// Token: 0x040000A6 RID: 166
		AlreadyInitialized = 1247,
		// Token: 0x040000A7 RID: 167
		ResourceDisabled = 4309,
		// Token: 0x040000A8 RID: 168
		DeviceNotAvailable = 4319,
		// Token: 0x040000A9 RID: 169
		NotValidState = 5023,
		// Token: 0x040000AA RID: 170
		NoMoreItems = 259,
		// Token: 0x040000AB RID: 171
		Failed = -2147467259
	}
}
