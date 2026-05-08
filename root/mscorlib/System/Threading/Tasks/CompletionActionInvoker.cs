using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200032F RID: 815
	internal sealed class CompletionActionInvoker : IThreadPoolWorkItem
	{
		// Token: 0x06002411 RID: 9233 RVA: 0x00082612 File Offset: 0x00080812
		internal CompletionActionInvoker(ITaskCompletionAction action, Task completingTask)
		{
			this.m_action = action;
			this.m_completingTask = completingTask;
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x00082628 File Offset: 0x00080828
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.m_action.Invoke(this.m_completingTask);
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x00004088 File Offset: 0x00002288
		public void MarkAborted(ThreadAbortException e)
		{
		}

		// Token: 0x04001BCD RID: 7117
		private readonly ITaskCompletionAction m_action;

		// Token: 0x04001BCE RID: 7118
		private readonly Task m_completingTask;
	}
}
