using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200033E RID: 830
	internal class StandardTaskContinuation : TaskContinuation
	{
		// Token: 0x06002437 RID: 9271 RVA: 0x00082C74 File Offset: 0x00080E74
		internal StandardTaskContinuation(Task task, TaskContinuationOptions options, TaskScheduler scheduler)
		{
			this.m_task = task;
			this.m_options = options;
			this.m_taskScheduler = scheduler;
			if (DebuggerSupport.LoggingOn)
			{
				CausalityTraceLevel causalityTraceLevel = CausalityTraceLevel.Required;
				Task task2 = this.m_task;
				string text = "Task.ContinueWith: ";
				Delegate action = task.m_action;
				DebuggerSupport.TraceOperationCreation(causalityTraceLevel, task2, text + ((action != null) ? action.ToString() : null), 0UL);
			}
			DebuggerSupport.AddToActiveTasks(this.m_task);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x00082CD8 File Offset: 0x00080ED8
		internal override void Run(Task completedTask, bool bCanInlineContinuationTask)
		{
			TaskContinuationOptions options = this.m_options;
			bool flag = (completedTask.IsCompletedSuccessfully ? ((options & TaskContinuationOptions.NotOnRanToCompletion) == TaskContinuationOptions.None) : (completedTask.IsCanceled ? ((options & TaskContinuationOptions.NotOnCanceled) == TaskContinuationOptions.None) : ((options & TaskContinuationOptions.NotOnFaulted) == TaskContinuationOptions.None)));
			Task task = this.m_task;
			if (flag)
			{
				if (!task.IsCanceled && DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationRelation(CausalityTraceLevel.Important, task, CausalityRelation.AssignDelegate);
				}
				task.m_taskScheduler = this.m_taskScheduler;
				if (bCanInlineContinuationTask && (options & TaskContinuationOptions.ExecuteSynchronously) != TaskContinuationOptions.None)
				{
					TaskContinuation.InlineIfPossibleOrElseQueue(task, true);
					return;
				}
				try
				{
					task.ScheduleAndStart(true);
					return;
				}
				catch (TaskSchedulerException)
				{
					return;
				}
			}
			task.InternalCancel(false);
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x00082D84 File Offset: 0x00080F84
		internal override Delegate[] GetDelegateContinuationsForDebugger()
		{
			if (this.m_task.m_action == null)
			{
				return this.m_task.GetDelegateContinuationsForDebugger();
			}
			return new Delegate[] { this.m_task.m_action };
		}

		// Token: 0x04001BFD RID: 7165
		internal readonly Task m_task;

		// Token: 0x04001BFE RID: 7166
		internal readonly TaskContinuationOptions m_options;

		// Token: 0x04001BFF RID: 7167
		private readonly TaskScheduler m_taskScheduler;
	}
}
