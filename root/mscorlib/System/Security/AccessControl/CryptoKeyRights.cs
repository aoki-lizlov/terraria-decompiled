using System;

namespace System.Security.AccessControl
{
	// Token: 0x020004EB RID: 1259
	[Flags]
	public enum CryptoKeyRights
	{
		// Token: 0x040023F8 RID: 9208
		ReadData = 1,
		// Token: 0x040023F9 RID: 9209
		WriteData = 2,
		// Token: 0x040023FA RID: 9210
		ReadExtendedAttributes = 8,
		// Token: 0x040023FB RID: 9211
		WriteExtendedAttributes = 16,
		// Token: 0x040023FC RID: 9212
		ReadAttributes = 128,
		// Token: 0x040023FD RID: 9213
		WriteAttributes = 256,
		// Token: 0x040023FE RID: 9214
		Delete = 65536,
		// Token: 0x040023FF RID: 9215
		ReadPermissions = 131072,
		// Token: 0x04002400 RID: 9216
		ChangePermissions = 262144,
		// Token: 0x04002401 RID: 9217
		TakeOwnership = 524288,
		// Token: 0x04002402 RID: 9218
		Synchronize = 1048576,
		// Token: 0x04002403 RID: 9219
		FullControl = 2032027,
		// Token: 0x04002404 RID: 9220
		GenericAll = 268435456,
		// Token: 0x04002405 RID: 9221
		GenericExecute = 536870912,
		// Token: 0x04002406 RID: 9222
		GenericWrite = 1073741824,
		// Token: 0x04002407 RID: 9223
		GenericRead = -2147483648
	}
}
