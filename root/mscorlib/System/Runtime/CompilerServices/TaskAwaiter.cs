using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Internal.Threading.Tasks.Tracing;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007DF RID: 2015
	public readonly struct TaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion, ITaskAwaiter
	{
		// Token: 0x060045CC RID: 17868 RVA: 0x000E55FF File Offset: 0x000E37FF
		internal TaskAwaiter(Task task)
		{
			this.m_task = task;
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060045CD RID: 17869 RVA: 0x000E5608 File Offset: 0x000E3808
		public bool IsCompleted
		{
			get
			{
				return this.m_task.IsCompleted;
			}
		}

		// Token: 0x060045CE RID: 17870 RVA: 0x000E5615 File Offset: 0x000E3815
		public void OnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, true);
		}

		// Token: 0x060045CF RID: 17871 RVA: 0x000E5625 File Offset: 0x000E3825
		public void UnsafeOnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, false);
		}

		// Token: 0x060045D0 RID: 17872 RVA: 0x000E5635 File Offset: 0x000E3835
		[StackTraceHidden]
		public void GetResult()
		{
			TaskAwaiter.ValidateEnd(this.m_task);
		}

		// Token: 0x060045D1 RID: 17873 RVA: 0x000E5642 File Offset: 0x000E3842
		[StackTraceHidden]
		internal static void ValidateEnd(Task task)
		{
			if (task.IsWaitNotificationEnabledOrNotRanToCompletion)
			{
				TaskAwaiter.HandleNonSuccessAndDebuggerNotification(task);
			}
		}

		// Token: 0x060045D2 RID: 17874 RVA: 0x000E5654 File Offset: 0x000E3854
		[StackTraceHidden]
		private static void HandleNonSuccessAndDebuggerNotification(Task task)
		{
			if (!task.IsCompleted)
			{
				task.InternalWait(-1, default(CancellationToken));
			}
			task.NotifyDebuggerOfWaitCompletionIfNecessary();
			if (!task.IsCompletedSuccessfully)
			{
				TaskAwaiter.ThrowForNonSuccess(task);
			}
		}

		// Token: 0x060045D3 RID: 17875 RVA: 0x000E5690 File Offset: 0x000E3890
		[StackTraceHidden]
		private static void ThrowForNonSuccess(Task task)
		{
			TaskStatus status = task.Status;
			if (status == TaskStatus.Canceled)
			{
				ExceptionDispatchInfo cancellationExceptionDispatchInfo = task.GetCancellationExceptionDispatchInfo();
				if (cancellationExceptionDispatchInfo != null)
				{
					cancellationExceptionDispatchInfo.Throw();
				}
				throw new TaskCanceledException(task);
			}
			if (status != TaskStatus.Faulted)
			{
				return;
			}
			ReadOnlyCollection<ExceptionDispatchInfo> exceptionDispatchInfos = task.GetExceptionDispatchInfos();
			if (exceptionDispatchInfos.Count > 0)
			{
				exceptionDispatchInfos[0].Throw();
				return;
			}
			throw task.Exception;
		}

		// Token: 0x060045D4 RID: 17876 RVA: 0x000E56E7 File Offset: 0x000E38E7
		internal static void OnCompletedInternal(Task task, Action continuation, bool continueOnCapturedContext, bool flowExecutionContext)
		{
			if (continuation == null)
			{
				throw new ArgumentNullException("continuation");
			}
			if (TaskTrace.Enabled)
			{
				continuation = TaskAwaiter.OutputWaitEtwEvents(task, continuation);
			}
			task.SetContinuationForAwait(continuation, continueOnCapturedContext, flowExecutionContext);
		}

		// Token: 0x060045D5 RID: 17877 RVA: 0x000E5710 File Offset: 0x000E3910
		private static Action OutputWaitEtwEvents(Task task, Action continuation)
		{
			Task internalCurrent = Task.InternalCurrent;
			TaskTrace.TaskWaitBegin_Asynchronous((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent != null) ? internalCurrent.Id : 0, task.Id);
			return delegate
			{
				if (TaskTrace.Enabled)
				{
					Task internalCurrent2 = Task.InternalCurrent;
					TaskTrace.TaskWaitEnd((internalCurrent2 != null) ? internalCurrent2.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent2 != null) ? internalCurrent2.Id : 0, task.Id);
				}
				continuation();
			};
		}

		// Token: 0x04002CCA RID: 11466
		internal readonly Task m_task;

		// Token: 0x020007E0 RID: 2016
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0
		{
			// Token: 0x060045D6 RID: 17878 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x060045D7 RID: 17879 RVA: 0x000E577C File Offset: 0x000E397C
			internal void <OutputWaitEtwEvents>b__0()
			{
				if (TaskTrace.Enabled)
				{
					Task internalCurrent = Task.InternalCurrent;
					TaskTrace.TaskWaitEnd((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent != null) ? internalCurrent.Id : 0, this.task.Id);
				}
				this.continuation();
			}

			// Token: 0x04002CCB RID: 11467
			public Task task;

			// Token: 0x04002CCC RID: 11468
			public Action continuation;
		}
	}
}
