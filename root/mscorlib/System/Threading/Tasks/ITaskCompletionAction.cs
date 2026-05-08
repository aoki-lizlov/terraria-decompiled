using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000336 RID: 822
	internal interface ITaskCompletionAction
	{
		// Token: 0x0600241E RID: 9246
		void Invoke(Task completingTask);

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600241F RID: 9247
		bool InvokeMayRunArbitraryCode { get; }
	}
}
