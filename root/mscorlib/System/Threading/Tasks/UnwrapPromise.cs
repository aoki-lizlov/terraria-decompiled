using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using Internal.Runtime.Augments;

namespace System.Threading.Tasks
{
	// Token: 0x02000337 RID: 823
	internal sealed class UnwrapPromise<TResult> : Task<TResult>, ITaskCompletionAction
	{
		// Token: 0x06002420 RID: 9248 RVA: 0x00082704 File Offset: 0x00080904
		public UnwrapPromise(Task outerTask, bool lookForOce)
			: base(null, outerTask.CreationOptions & TaskCreationOptions.AttachedToParent)
		{
			this._lookForOce = lookForOce;
			this._state = 0;
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, this, "Task.Unwrap", 0UL);
			}
			DebuggerSupport.AddToActiveTasks(this);
			if (outerTask.IsCompleted)
			{
				this.ProcessCompletedOuterTask(outerTask);
				return;
			}
			outerTask.AddCompletionAction(this);
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x00082760 File Offset: 0x00080960
		public void Invoke(Task completingTask)
		{
			StackGuard currentStackGuard = Task.CurrentStackGuard;
			if (currentStackGuard.TryBeginInliningScope())
			{
				try
				{
					this.InvokeCore(completingTask);
					return;
				}
				finally
				{
					currentStackGuard.EndInliningScope();
				}
			}
			this.InvokeCoreAsync(completingTask);
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x000827A4 File Offset: 0x000809A4
		private void InvokeCore(Task completingTask)
		{
			byte state = this._state;
			if (state == 0)
			{
				this.ProcessCompletedOuterTask(completingTask);
				return;
			}
			if (state != 1)
			{
				return;
			}
			this.TrySetFromTask(completingTask, false);
			this._state = 2;
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x000827D8 File Offset: 0x000809D8
		private void InvokeCoreAsync(Task completingTask)
		{
			ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
			{
				Tuple<UnwrapPromise<TResult>, Task> tuple = (Tuple<UnwrapPromise<TResult>, Task>)state;
				tuple.Item1.InvokeCore(tuple.Item2);
			}, Tuple.Create<UnwrapPromise<TResult>, Task>(this, completingTask));
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x00082808 File Offset: 0x00080A08
		private void ProcessCompletedOuterTask(Task task)
		{
			this._state = 1;
			TaskStatus status = task.Status;
			if (status != TaskStatus.RanToCompletion)
			{
				if (status - TaskStatus.Canceled <= 1)
				{
					this.TrySetFromTask(task, this._lookForOce);
					return;
				}
			}
			else
			{
				Task<Task<TResult>> task2 = task as Task<Task<TResult>>;
				this.ProcessInnerTask((task2 != null) ? task2.Result : ((Task<Task>)task).Result);
			}
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x00082860 File Offset: 0x00080A60
		private bool TrySetFromTask(Task task, bool lookForOce)
		{
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationRelation(CausalityTraceLevel.Important, this, CausalityRelation.Join);
			}
			bool flag = false;
			switch (task.Status)
			{
			case TaskStatus.RanToCompletion:
			{
				Task<TResult> task2 = task as Task<TResult>;
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Completed);
				}
				DebuggerSupport.RemoveFromActiveTasks(this);
				flag = base.TrySetResult((task2 != null) ? task2.Result : default(TResult));
				break;
			}
			case TaskStatus.Canceled:
				flag = base.TrySetCanceled(task.CancellationToken, task.GetCancellationExceptionDispatchInfo());
				break;
			case TaskStatus.Faulted:
			{
				ReadOnlyCollection<ExceptionDispatchInfo> exceptionDispatchInfos = task.GetExceptionDispatchInfos();
				ExceptionDispatchInfo exceptionDispatchInfo;
				OperationCanceledException ex;
				if (lookForOce && exceptionDispatchInfos.Count > 0 && (exceptionDispatchInfo = exceptionDispatchInfos[0]) != null && (ex = exceptionDispatchInfo.SourceException as OperationCanceledException) != null)
				{
					flag = base.TrySetCanceled(ex.CancellationToken, exceptionDispatchInfo);
				}
				else
				{
					flag = base.TrySetException(exceptionDispatchInfos);
				}
				break;
			}
			}
			return flag;
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x0008293C File Offset: 0x00080B3C
		private void ProcessInnerTask(Task task)
		{
			if (task == null)
			{
				base.TrySetCanceled(default(CancellationToken));
				this._state = 2;
				return;
			}
			if (task.IsCompleted)
			{
				this.TrySetFromTask(task, false);
				this._state = 2;
				return;
			}
			task.AddCompletionAction(this);
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06002427 RID: 9255 RVA: 0x00003FB7 File Offset: 0x000021B7
		public bool InvokeMayRunArbitraryCode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001BF2 RID: 7154
		private const byte STATE_WAITING_ON_OUTER_TASK = 0;

		// Token: 0x04001BF3 RID: 7155
		private const byte STATE_WAITING_ON_INNER_TASK = 1;

		// Token: 0x04001BF4 RID: 7156
		private const byte STATE_DONE = 2;

		// Token: 0x04001BF5 RID: 7157
		private byte _state;

		// Token: 0x04001BF6 RID: 7158
		private readonly bool _lookForOce;

		// Token: 0x02000338 RID: 824
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002428 RID: 9256 RVA: 0x00082984 File Offset: 0x00080B84
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002429 RID: 9257 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x0600242A RID: 9258 RVA: 0x00082990 File Offset: 0x00080B90
			internal void <InvokeCoreAsync>b__8_0(object state)
			{
				Tuple<UnwrapPromise<TResult>, Task> tuple = (Tuple<UnwrapPromise<TResult>, Task>)state;
				tuple.Item1.InvokeCore(tuple.Item2);
			}

			// Token: 0x04001BF7 RID: 7159
			public static readonly UnwrapPromise<TResult>.<>c <>9 = new UnwrapPromise<TResult>.<>c();

			// Token: 0x04001BF8 RID: 7160
			public static WaitCallback <>9__8_0;
		}
	}
}
