using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200041A RID: 1050
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum KeyContainerPermissionFlags
	{
		// Token: 0x04001F27 RID: 7975
		NoFlags = 0,
		// Token: 0x04001F28 RID: 7976
		Create = 1,
		// Token: 0x04001F29 RID: 7977
		Open = 2,
		// Token: 0x04001F2A RID: 7978
		Delete = 4,
		// Token: 0x04001F2B RID: 7979
		Import = 16,
		// Token: 0x04001F2C RID: 7980
		Export = 32,
		// Token: 0x04001F2D RID: 7981
		Sign = 256,
		// Token: 0x04001F2E RID: 7982
		Decrypt = 512,
		// Token: 0x04001F2F RID: 7983
		ViewAcl = 4096,
		// Token: 0x04001F30 RID: 7984
		ChangeAcl = 8192,
		// Token: 0x04001F31 RID: 7985
		AllFlags = 13111
	}
}
