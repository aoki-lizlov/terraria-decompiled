using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000341 RID: 833
	internal sealed class TaskSchedulerAwaitTaskContinuation : AwaitTaskContinuation
	{
		// Token: 0x06002442 RID: 9282 RVA: 0x00082E79 File Offset: 0x00081079
		internal TaskSchedulerAwaitTaskContinuation(TaskScheduler scheduler, Action action, bool flowExecutionContext)
			: base(action, flowExecutionContext)
		{
			this.m_scheduler = scheduler;
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x00082E8C File Offset: 0x0008108C
		internal sealed override void Run(Task ignored, bool canInlineContinuationTask)
		{
			if (this.m_scheduler == TaskScheduler.Default)
			{
				base.Run(ignored, canInlineContinuationTask);
				return;
			}
			bool flag = canInlineContinuationTask && (TaskScheduler.InternalCurrent == this.m_scheduler || ThreadPool.IsThreadPoolThread);
			Task task = base.CreateTask(delegate(object state)
			{
				try
				{
					((Action)state)();
				}
				catch (Exception ex)
				{
					AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
				}
			}, this.m_action, this.m_scheduler);
			if (flag)
			{
				TaskContinuation.InlineIfPossibleOrElseQueue(task, false);
				return;
			}
			try
			{
				task.ScheduleAndStart(false);
			}
			catch (TaskSchedulerException)
			{
			}
		}

		// Token: 0x04001C04 RID: 7172
		private readonly TaskScheduler m_scheduler;

		// Token: 0x02000342 RID: 834
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002444 RID: 9284 RVA: 0x00082F24 File Offset: 0x00081124
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002445 RID: 9285 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06002446 RID: 9286 RVA: 0x00082F30 File Offset: 0x00081130
			internal void <Run>b__2_0(object state)
			{
				try
				{
					((Action)state)();
				}
				catch (Exception ex)
				{
					AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
				}
			}

			// Token: 0x04001C05 RID: 7173
			public static readonly TaskSchedulerAwaitTaskContinuation.<>c <>9 = new TaskSchedulerAwaitTaskContinuation.<>c();

			// Token: 0x04001C06 RID: 7174
			public static Action<object> <>9__2_0;
		}
	}
}
