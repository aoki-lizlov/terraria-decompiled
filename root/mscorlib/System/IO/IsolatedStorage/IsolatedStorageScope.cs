using System;

namespace System.IO.IsolatedStorage
{
	// Token: 0x02000991 RID: 2449
	[Flags]
	public enum IsolatedStorageScope
	{
		// Token: 0x04003571 RID: 13681
		None = 0,
		// Token: 0x04003572 RID: 13682
		User = 1,
		// Token: 0x04003573 RID: 13683
		Domain = 2,
		// Token: 0x04003574 RID: 13684
		Assembly = 4,
		// Token: 0x04003575 RID: 13685
		Roaming = 8,
		// Token: 0x04003576 RID: 13686
		Machine = 16,
		// Token: 0x04003577 RID: 13687
		Application = 32
	}
}
