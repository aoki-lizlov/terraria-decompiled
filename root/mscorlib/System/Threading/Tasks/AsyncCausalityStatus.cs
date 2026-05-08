using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000352 RID: 850
	[FriendAccessAllowed]
	internal enum AsyncCausalityStatus
	{
		// Token: 0x04001C31 RID: 7217
		Started,
		// Token: 0x04001C32 RID: 7218
		Completed,
		// Token: 0x04001C33 RID: 7219
		Canceled,
		// Token: 0x04001C34 RID: 7220
		Error
	}
}
