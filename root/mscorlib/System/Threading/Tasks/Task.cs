using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200030F RID: 783
	[DebuggerTypeProxy(typeof(SystemThreadingTasks_FutureDebugView<>))]
	[DebuggerDisplay("Id = {Id}, Status = {Status}, Method = {DebuggerDisplayMethodDescription}, Result = {DebuggerDisplayResultDescription}")]
	public class Task<TResult> : Task
	{
		// Token: 0x06002277 RID: 8823 RVA: 0x0007CF4B File Offset: 0x0007B14B
		internal Task()
		{
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x0007CF53 File Offset: 0x0007B153
		internal Task(object state, TaskCreationOptions options)
			: base(state, options, true)
		{
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x0007CF60 File Offset: 0x0007B160
		internal Task(TResult result)
			: base(false, TaskCreationOptions.None, default(CancellationToken))
		{
			this.m_result = result;
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x0007CF85 File Offset: 0x0007B185
		internal Task(bool canceled, TResult result, TaskCreationOptions creationOptions, CancellationToken ct)
			: base(canceled, creationOptions, ct)
		{
			if (!canceled)
			{
				this.m_result = result;
			}
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x0007CF9C File Offset: 0x0007B19C
		public Task(Func<TResult> function)
			: this(function, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x0007CFBD File Offset: 0x0007B1BD
		public Task(Func<TResult> function, CancellationToken cancellationToken)
			: this(function, null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x0007CFCC File Offset: 0x0007B1CC
		public Task(Func<TResult> function, TaskCreationOptions creationOptions)
			: this(function, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x0007CFF2 File Offset: 0x0007B1F2
		public Task(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
			: this(function, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x0007D008 File Offset: 0x0007B208
		public Task(Func<object, TResult> function, object state)
			: this(function, state, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x0007D02A File Offset: 0x0007B22A
		public Task(Func<object, TResult> function, object state, CancellationToken cancellationToken)
			: this(function, state, null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x0007D03C File Offset: 0x0007B23C
		public Task(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
			: this(function, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x0007D063 File Offset: 0x0007B263
		public Task(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
			: this(function, state, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x0007D079 File Offset: 0x0007B279
		internal Task(Func<TResult> valueSelector, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
			: base(valueSelector, null, parent, cancellationToken, creationOptions, internalOptions, scheduler)
		{
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x0007D08B File Offset: 0x0007B28B
		internal Task(Delegate valueSelector, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
			: base(valueSelector, state, parent, cancellationToken, creationOptions, internalOptions, scheduler)
		{
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x0007D09E File Offset: 0x0007B29E
		internal static Task<TResult> StartNew(Task parent, Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<TResult> task = new Task<TResult>(function, parent, cancellationToken, creationOptions, internalOptions | InternalTaskOptions.QueuedByRuntime, scheduler);
			task.ScheduleAndStart(false);
			return task;
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x0007D0D7 File Offset: 0x0007B2D7
		internal static Task<TResult> StartNew(Task parent, Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<TResult> task = new Task<TResult>(function, state, parent, cancellationToken, creationOptions, internalOptions | InternalTaskOptions.QueuedByRuntime, scheduler);
			task.ScheduleAndStart(false);
			return task;
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x0007D114 File Offset: 0x0007B314
		private string DebuggerDisplayResultDescription
		{
			get
			{
				if (!base.IsCompletedSuccessfully)
				{
					return "{Not yet computed}";
				}
				TResult result = this.m_result;
				return ((result != null) ? result.ToString() : null) ?? "";
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06002288 RID: 8840 RVA: 0x0007D158 File Offset: 0x0007B358
		private string DebuggerDisplayMethodDescription
		{
			get
			{
				Delegate action = this.m_action;
				if (action == null)
				{
					return "{null}";
				}
				return "0x" + action.GetNativeFunctionPointer().ToString("x");
			}
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x0007D194 File Offset: 0x0007B394
		internal bool TrySetResult(TResult result)
		{
			if (base.IsCompleted)
			{
				return false;
			}
			if (base.AtomicStateUpdate(67108864, 90177536))
			{
				this.m_result = result;
				Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 16777216);
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				if (contingentProperties != null)
				{
					contingentProperties.SetCompleted();
				}
				base.FinishStageThree();
				return true;
			}
			return false;
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x0007D1F9 File Offset: 0x0007B3F9
		internal void DangerousSetResult(TResult result)
		{
			if (this.m_parent != null)
			{
				this.TrySetResult(result);
				return;
			}
			this.m_result = result;
			this.m_stateFlags |= 16777216;
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x0007D229 File Offset: 0x0007B429
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public TResult Result
		{
			get
			{
				if (!base.IsWaitNotificationEnabledOrNotRanToCompletion)
				{
					return this.m_result;
				}
				return this.GetResultCore(true);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600228C RID: 8844 RVA: 0x0007D241 File Offset: 0x0007B441
		internal TResult ResultOnSuccess
		{
			get
			{
				return this.m_result;
			}
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x0007D24C File Offset: 0x0007B44C
		internal TResult GetResultCore(bool waitCompletionNotification)
		{
			if (!base.IsCompleted)
			{
				base.InternalWait(-1, default(CancellationToken));
			}
			if (waitCompletionNotification)
			{
				base.NotifyDebuggerOfWaitCompletionIfNecessary();
			}
			if (!base.IsCompletedSuccessfully)
			{
				base.ThrowIfExceptional(true);
			}
			return this.m_result;
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600228E RID: 8846 RVA: 0x0007D291 File Offset: 0x0007B491
		public new static TaskFactory<TResult> Factory
		{
			get
			{
				if (Task<TResult>.s_defaultFactory == null)
				{
					Interlocked.CompareExchange<TaskFactory<TResult>>(ref Task<TResult>.s_defaultFactory, new TaskFactory<TResult>(), null);
				}
				return Task<TResult>.s_defaultFactory;
			}
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x0007D2B0 File Offset: 0x0007B4B0
		internal override void InnerInvoke()
		{
			Func<TResult> func = this.m_action as Func<TResult>;
			if (func != null)
			{
				this.m_result = func();
				return;
			}
			Func<object, TResult> func2 = this.m_action as Func<object, TResult>;
			if (func2 != null)
			{
				this.m_result = func2(this.m_stateObject);
				return;
			}
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x0007D2FB File Offset: 0x0007B4FB
		public new TaskAwaiter<TResult> GetAwaiter()
		{
			return new TaskAwaiter<TResult>(this);
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0007D303 File Offset: 0x0007B503
		public new ConfiguredTaskAwaitable<TResult> ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredTaskAwaitable<TResult>(this, continueOnCapturedContext);
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x0007D30C File Offset: 0x0007B50C
		public Task ContinueWith(Action<Task<TResult>> continuationAction)
		{
			return this.ContinueWith(continuationAction, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x0007D32F File Offset: 0x0007B52F
		public Task ContinueWith(Action<Task<TResult>> continuationAction, CancellationToken cancellationToken)
		{
			return this.ContinueWith(continuationAction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None);
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x0007D340 File Offset: 0x0007B540
		public Task ContinueWith(Action<Task<TResult>> continuationAction, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, scheduler, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x0007D360 File Offset: 0x0007B560
		public Task ContinueWith(Action<Task<TResult>> continuationAction, TaskContinuationOptions continuationOptions)
		{
			return this.ContinueWith(continuationAction, TaskScheduler.Current, default(CancellationToken), continuationOptions);
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x0007D383 File Offset: 0x0007B583
		public Task ContinueWith(Action<Task<TResult>> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x0007D390 File Offset: 0x0007B590
		internal Task ContinueWith(Action<Task<TResult>> continuationAction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions taskCreationOptions;
			InternalTaskOptions internalTaskOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out taskCreationOptions, out internalTaskOptions);
			Task task = new ContinuationTaskFromResultTask<TResult>(this, continuationAction, null, taskCreationOptions, internalTaskOptions);
			base.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x0007D3DC File Offset: 0x0007B5DC
		public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state)
		{
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x0007D400 File Offset: 0x0007B600
		public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, CancellationToken cancellationToken)
		{
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None);
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x0007D414 File Offset: 0x0007B614
		public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, state, scheduler, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x0007D434 File Offset: 0x0007B634
		public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, TaskContinuationOptions continuationOptions)
		{
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, default(CancellationToken), continuationOptions);
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x0007D458 File Offset: 0x0007B658
		public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, state, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x0007D468 File Offset: 0x0007B668
		internal Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions taskCreationOptions;
			InternalTaskOptions internalTaskOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out taskCreationOptions, out internalTaskOptions);
			Task task = new ContinuationTaskFromResultTask<TResult>(this, continuationAction, state, taskCreationOptions, internalTaskOptions);
			base.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x0007D4B4 File Offset: 0x0007B6B4
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x0007D4D7 File Offset: 0x0007B6D7
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, CancellationToken cancellationToken)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None);
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x0007D4E8 File Offset: 0x0007B6E8
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskScheduler scheduler)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, scheduler, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x0007D508 File Offset: 0x0007B708
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, TaskScheduler.Current, default(CancellationToken), continuationOptions);
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0007D52B File Offset: 0x0007B72B
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x0007D538 File Offset: 0x0007B738
		internal Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions taskCreationOptions;
			InternalTaskOptions internalTaskOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out taskCreationOptions, out internalTaskOptions);
			Task<TNewResult> task = new ContinuationResultTaskFromResultTask<TResult, TNewResult>(this, continuationFunction, null, taskCreationOptions, internalTaskOptions);
			base.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x0007D584 File Offset: 0x0007B784
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, state, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x0007D5A8 File Offset: 0x0007B7A8
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, CancellationToken cancellationToken)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None);
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x0007D5BC File Offset: 0x0007B7BC
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, TaskScheduler scheduler)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, state, scheduler, default(CancellationToken), TaskContinuationOptions.None);
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x0007D5DC File Offset: 0x0007B7DC
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, TaskContinuationOptions continuationOptions)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, state, TaskScheduler.Current, default(CancellationToken), continuationOptions);
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x0007D600 File Offset: 0x0007B800
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith<TNewResult>(continuationFunction, state, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x0007D610 File Offset: 0x0007B810
		internal Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions taskCreationOptions;
			InternalTaskOptions internalTaskOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out taskCreationOptions, out internalTaskOptions);
			Task<TNewResult> task = new ContinuationResultTaskFromResultTask<TResult, TNewResult>(this, continuationFunction, state, taskCreationOptions, internalTaskOptions);
			base.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x04001B4E RID: 6990
		internal TResult m_result;

		// Token: 0x04001B4F RID: 6991
		private static TaskFactory<TResult> s_defaultFactory;

		// Token: 0x02000310 RID: 784
		internal static class TaskWhenAnyCast
		{
			// Token: 0x060022AA RID: 8874 RVA: 0x0007D65C File Offset: 0x0007B85C
			// Note: this type is marked as 'beforefieldinit'.
			static TaskWhenAnyCast()
			{
			}

			// Token: 0x04001B50 RID: 6992
			internal static readonly Func<Task<Task>, Task<TResult>> Value = (Task<Task> completed) => (Task<TResult>)completed.Result;

			// Token: 0x02000311 RID: 785
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x060022AB RID: 8875 RVA: 0x0007D673 File Offset: 0x0007B873
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x060022AC RID: 8876 RVA: 0x000025BE File Offset: 0x000007BE
				public <>c()
				{
				}

				// Token: 0x060022AD RID: 8877 RVA: 0x0007D67F File Offset: 0x0007B87F
				internal Task<TResult> <.cctor>b__1_0(Task<Task> completed)
				{
					return (Task<TResult>)completed.Result;
				}

				// Token: 0x04001B51 RID: 6993
				public static readonly Task<TResult>.TaskWhenAnyCast.<>c <>9 = new Task<TResult>.TaskWhenAnyCast.<>c();
			}
		}
	}
}
