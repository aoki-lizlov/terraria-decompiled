using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x020004BC RID: 1212
	[ComVisible(true)]
	[Serializable]
	public enum WindowsBuiltInRole
	{
		// Token: 0x040022B8 RID: 8888
		Administrator = 544,
		// Token: 0x040022B9 RID: 8889
		User,
		// Token: 0x040022BA RID: 8890
		Guest,
		// Token: 0x040022BB RID: 8891
		PowerUser,
		// Token: 0x040022BC RID: 8892
		AccountOperator,
		// Token: 0x040022BD RID: 8893
		SystemOperator,
		// Token: 0x040022BE RID: 8894
		PrintOperator,
		// Token: 0x040022BF RID: 8895
		BackupOperator,
		// Token: 0x040022C0 RID: 8896
		Replicator
	}
}
