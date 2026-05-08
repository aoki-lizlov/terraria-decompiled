using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200059D RID: 1437
	[ComVisible(true)]
	[Serializable]
	public enum ActivatorLevel
	{
		// Token: 0x04002578 RID: 9592
		Construction = 4,
		// Token: 0x04002579 RID: 9593
		Context = 8,
		// Token: 0x0400257A RID: 9594
		AppDomain = 12,
		// Token: 0x0400257B RID: 9595
		Process = 16,
		// Token: 0x0400257C RID: 9596
		Machine = 20
	}
}
