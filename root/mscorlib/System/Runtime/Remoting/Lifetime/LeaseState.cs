using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x0200055A RID: 1370
	[ComVisible(true)]
	[Serializable]
	public enum LeaseState
	{
		// Token: 0x0400251A RID: 9498
		Null,
		// Token: 0x0400251B RID: 9499
		Initial,
		// Token: 0x0400251C RID: 9500
		Active,
		// Token: 0x0400251D RID: 9501
		Renewing,
		// Token: 0x0400251E RID: 9502
		Expired
	}
}
