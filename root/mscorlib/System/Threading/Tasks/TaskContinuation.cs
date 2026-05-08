using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200033D RID: 829
	internal abstract class TaskContinuation
	{
		// Token: 0x06002433 RID: 9267
		internal abstract void Run(Task completedTask, bool bCanInlineContinuationTask);

		// Token: 0x06002434 RID: 9268 RVA: 0x00082C00 File Offset: 0x00080E00
		protected static void InlineIfPossibleOrElseQueue(Task task, bool needsProtection)
		{
			if (needsProtection)
			{
				if (!task.MarkStarted())
				{
					return;
				}
			}
			else
			{
				task.m_stateFlags |= 65536;
			}
			try
			{
				if (!task.m_taskScheduler.TryRunInline(task, false))
				{
					task.m_taskScheduler.QueueTask(task);
				}
			}
			catch (Exception ex)
			{
				TaskSchedulerException ex2 = new TaskSchedulerException(ex);
				task.AddException(ex2);
				task.Finish(false);
			}
		}

		// Token: 0x06002435 RID: 9269
		internal abstract Delegate[] GetDelegateContinuationsForDebugger();

		// Token: 0x06002436 RID: 9270 RVA: 0x000025BE File Offset: 0x000007BE
		protected TaskContinuation()
		{
		}
	}
}
