using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000326 RID: 806
	public enum TaskStatus
	{
		// Token: 0x04001B8C RID: 7052
		Created,
		// Token: 0x04001B8D RID: 7053
		WaitingForActivation,
		// Token: 0x04001B8E RID: 7054
		WaitingToRun,
		// Token: 0x04001B8F RID: 7055
		Running,
		// Token: 0x04001B90 RID: 7056
		WaitingForChildrenToComplete,
		// Token: 0x04001B91 RID: 7057
		RanToCompletion,
		// Token: 0x04001B92 RID: 7058
		Canceled,
		// Token: 0x04001B93 RID: 7059
		Faulted
	}
}
