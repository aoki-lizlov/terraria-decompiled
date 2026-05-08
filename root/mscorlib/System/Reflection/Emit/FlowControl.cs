using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008F7 RID: 2295
	[ComVisible(true)]
	[Serializable]
	public enum FlowControl
	{
		// Token: 0x040030ED RID: 12525
		Branch,
		// Token: 0x040030EE RID: 12526
		Break,
		// Token: 0x040030EF RID: 12527
		Call,
		// Token: 0x040030F0 RID: 12528
		Cond_Branch,
		// Token: 0x040030F1 RID: 12529
		Meta,
		// Token: 0x040030F2 RID: 12530
		Next,
		// Token: 0x040030F3 RID: 12531
		[Obsolete("This API has been deprecated.")]
		Phi,
		// Token: 0x040030F4 RID: 12532
		Return,
		// Token: 0x040030F5 RID: 12533
		Throw
	}
}
