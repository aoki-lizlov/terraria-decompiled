using System;
using System.Runtime.CompilerServices;
using Internal.Runtime.Augments;

namespace System.Threading.Tasks
{
	// Token: 0x02000313 RID: 787
	public class TaskFactory<TResult>
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x0007D73D File Offset: 0x0007B93D
		private TaskScheduler DefaultScheduler
		{
			get
			{
				if (this.m_defaultScheduler == null)
				{
					return TaskScheduler.Current;
				}
				return this.m_defaultScheduler;
			}
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x0007D753 File Offset: 0x0007B953
		private TaskScheduler GetDefaultScheduler(Task currTask)
		{
			if (this.m_defaultScheduler != null)
			{
				return this.m_defaultScheduler;
			}
			if (currTask != null && (currTask.CreationOptions & TaskCreationOptions.HideScheduler) == TaskCreationOptions.None)
			{
				return currTask.ExecutingTaskScheduler;
			}
			return TaskScheduler.Default;
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x0007D780 File Offset: 0x0007B980
		public TaskFactory()
			: this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x0007D79F File Offset: 0x0007B99F
		public TaskFactory(CancellationToken cancellationToken)
			: this(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x0007D7AC File Offset: 0x0007B9AC
		public TaskFactory(TaskScheduler scheduler)
			: this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, scheduler)
		{
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0007D7CC File Offset: 0x0007B9CC
		public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions)
			: this(default(CancellationToken), creationOptions, continuationOptions, null)
		{
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0007D7EB File Offset: 0x0007B9EB
		public TaskFactory(CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			TaskFactory.CheckCreationOptions(creationOptions);
			this.m_defaultCancellationToken = cancellationToken;
			this.m_defaultScheduler = scheduler;
			this.m_defaultCreationOptions = creationOptions;
			this.m_defaultContinuationOptions = continuationOptions;
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x0007D81C File Offset: 0x0007BA1C
		public CancellationToken CancellationToken
		{
			get
			{
				return this.m_defaultCancellationToken;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x0007D824 File Offset: 0x0007BA24
		public TaskScheduler Scheduler
		{
			get
			{
				return this.m_defaultScheduler;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x0007D82C File Offset: 0x0007BA2C
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_defaultCreationOptions;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060022C0 RID: 8896 RVA: 0x0007D834 File Offset: 0x0007BA34
		public TaskContinuationOptions ContinuationOptions
		{
			get
			{
				return this.m_defaultContinuationOptions;
			}
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x0007D83C File Offset: 0x0007BA3C
		public Task<TResult> StartNew(Func<TResult> function)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0007D86C File Offset: 0x0007BA6C
		public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0007D898 File Offset: 0x0007BA98
		public Task<TResult> StartNew(Func<TResult> function, TaskCreationOptions creationOptions)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x0007D8C1 File Offset: 0x0007BAC1
		public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler);
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x0007D8D4 File Offset: 0x0007BAD4
		public Task<TResult> StartNew(Func<object, TResult> function, object state)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x0007D904 File Offset: 0x0007BB04
		public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken cancellationToken)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x0007D930 File Offset: 0x0007BB30
		public Task<TResult> StartNew(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x0007D95A File Offset: 0x0007BB5A
		public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, state, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler);
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x0007D970 File Offset: 0x0007BB70
		private static void FromAsyncCoreLogic(IAsyncResult iar, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, Task<TResult> promise, bool requiresSynchronization)
		{
			Exception ex = null;
			OperationCanceledException ex2 = null;
			TResult tresult = default(TResult);
			try
			{
				if (endFunction != null)
				{
					tresult = endFunction(iar);
				}
				else
				{
					endAction(iar);
				}
			}
			catch (OperationCanceledException ex2)
			{
			}
			catch (Exception ex)
			{
			}
			finally
			{
				if (ex2 != null)
				{
					promise.TrySetCanceled(ex2.CancellationToken, ex2);
				}
				else if (ex != null)
				{
					promise.TrySetException(ex);
				}
				else
				{
					if (DebuggerSupport.LoggingOn)
					{
						DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, promise, AsyncStatus.Completed);
					}
					DebuggerSupport.RemoveFromActiveTasks(promise);
					if (requiresSynchronization)
					{
						promise.TrySetResult(tresult);
					}
					else
					{
						promise.DangerousSetResult(tresult);
					}
				}
			}
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x0007DA18 File Offset: 0x0007BC18
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
		{
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, this.m_defaultCreationOptions, this.DefaultScheduler);
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x0007DA2E File Offset: 0x0007BC2E
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, this.DefaultScheduler);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x0007DA3F File Offset: 0x0007BC3F
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, scheduler);
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x0007DA4C File Offset: 0x0007BC4C
		internal static Task<TResult> FromAsyncImpl(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, false);
			Task<TResult> promise = new Task<TResult>(null, creationOptions);
			Task t = new Task(new Action<object>(delegate
			{
				TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, true);
			}), null, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null);
			if (asyncResult.IsCompleted)
			{
				try
				{
					t.InternalRunSynchronously(scheduler, false);
					goto IL_00EE;
				}
				catch (Exception ex)
				{
					promise.TrySetException(ex);
					goto IL_00EE;
				}
			}
			ThreadPool.RegisterWaitForSingleObject(asyncResult.AsyncWaitHandle, delegate
			{
				try
				{
					t.InternalRunSynchronously(scheduler, false);
				}
				catch (Exception ex2)
				{
					promise.TrySetException(ex2);
				}
			}, null, -1, true);
			IL_00EE:
			return promise;
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x0007DB60 File Offset: 0x0007BD60
		public Task<TResult> FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x0007DB71 File Offset: 0x0007BD71
		public Task<TResult> FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, creationOptions);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x0007DB80 File Offset: 0x0007BD80
		internal static Task<TResult> FromAsyncImpl(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, promise, "TaskFactory.FromAsync: " + ((beginMethod != null) ? beginMethod.ToString() : null), 0UL);
			}
			DebuggerSupport.AddToActiveTasks(promise);
			try
			{
				IAsyncResult asyncResult = beginMethod(delegate(IAsyncResult iar)
				{
					if (!iar.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}
				}, state);
				if (asyncResult.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
				}
			}
			catch
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, promise, AsyncStatus.Error);
				}
				DebuggerSupport.RemoveFromActiveTasks(promise);
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x0007DC9C File Offset: 0x0007BE9C
		public Task<TResult> FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x0007DCAF File Offset: 0x0007BEAF
		public Task<TResult> FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, creationOptions);
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x0007DCC0 File Offset: 0x0007BEC0
		internal static Task<TResult> FromAsyncImpl<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endFunction");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, promise, "TaskFactory.FromAsync: " + ((beginMethod != null) ? beginMethod.ToString() : null), 0UL);
			}
			DebuggerSupport.AddToActiveTasks(promise);
			try
			{
				IAsyncResult asyncResult = beginMethod(arg1, delegate(IAsyncResult iar)
				{
					if (!iar.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}
				}, state);
				if (asyncResult.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
				}
			}
			catch
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, promise, AsyncStatus.Error);
				}
				DebuggerSupport.RemoveFromActiveTasks(promise);
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x0007DDE0 File Offset: 0x0007BFE0
		public Task<TResult> FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x0007DDF5 File Offset: 0x0007BFF5
		public Task<TResult> FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, creationOptions);
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x0007DE08 File Offset: 0x0007C008
		internal static Task<TResult> FromAsyncImpl<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, promise, "TaskFactory.FromAsync: " + ((beginMethod != null) ? beginMethod.ToString() : null), 0UL);
			}
			DebuggerSupport.AddToActiveTasks(promise);
			try
			{
				IAsyncResult asyncResult = beginMethod(arg1, arg2, delegate(IAsyncResult iar)
				{
					if (!iar.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}
				}, state);
				if (asyncResult.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
				}
			}
			catch
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, promise, AsyncStatus.Error);
				}
				DebuggerSupport.RemoveFromActiveTasks(promise);
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x0007DF28 File Offset: 0x0007C128
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x0007DF3F File Offset: 0x0007C13F
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, creationOptions);
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x0007DF54 File Offset: 0x0007C154
		internal static Task<TResult> FromAsyncImpl<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, promise, "TaskFactory.FromAsync: " + ((beginMethod != null) ? beginMethod.ToString() : null), 0UL);
			}
			DebuggerSupport.AddToActiveTasks(promise);
			try
			{
				IAsyncResult asyncResult = beginMethod(arg1, arg2, arg3, delegate(IAsyncResult iar)
				{
					if (!iar.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}
				}, state);
				if (asyncResult.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
				}
			}
			catch
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, promise, AsyncStatus.Error);
				}
				DebuggerSupport.RemoveFromActiveTasks(promise);
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x0007E078 File Offset: 0x0007C278
		internal static Task<TResult> FromAsyncTrim<TInstance, TArgs>(TInstance thisRef, TArgs args, Func<TInstance, TArgs, AsyncCallback, object, IAsyncResult> beginMethod, Func<TInstance, IAsyncResult, TResult> endMethod) where TInstance : class
		{
			TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> fromAsyncTrimPromise = new TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>(thisRef, endMethod);
			IAsyncResult asyncResult = beginMethod(thisRef, args, TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.s_completeFromAsyncResult, fromAsyncTrimPromise);
			if (asyncResult.CompletedSynchronously)
			{
				fromAsyncTrimPromise.Complete(thisRef, endMethod, asyncResult, false);
			}
			return fromAsyncTrimPromise;
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x0007E0B0 File Offset: 0x0007C2B0
		private static Task<TResult> CreateCanceledTask(TaskContinuationOptions continuationOptions, CancellationToken ct)
		{
			TaskCreationOptions taskCreationOptions;
			InternalTaskOptions internalTaskOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out taskCreationOptions, out internalTaskOptions);
			return new Task<TResult>(true, default(TResult), taskCreationOptions, ct);
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x0007E0D8 File Offset: 0x0007C2D8
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x0007E101 File Offset: 0x0007C301
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x0007E125 File Offset: 0x0007C325
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x0007E149 File Offset: 0x0007C349
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x0007E165 File Offset: 0x0007C365
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x0007E18E File Offset: 0x0007C38E
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x0007E1B2 File Offset: 0x0007C3B2
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x0007E1D6 File Offset: 0x0007C3D6
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x0007E1F4 File Offset: 0x0007C3F4
		internal static Task<TResult> ContinueWhenAllImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<TAntecedentResult>[] array = TaskFactory.CheckMultiContinuationTasksAndCopy<TAntecedentResult>(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			return TaskFactory.CommonCWAllLogic<TAntecedentResult>(array).ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAllFuncDelegate, continuationFunction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x0007E258 File Offset: 0x0007C458
		internal static Task<TResult> ContinueWhenAllImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<TAntecedentResult>[] array = TaskFactory.CheckMultiContinuationTasksAndCopy<TAntecedentResult>(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			return TaskFactory.CommonCWAllLogic<TAntecedentResult>(array).ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAllActionDelegate, continuationAction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x0007E2BC File Offset: 0x0007C4BC
		internal static Task<TResult> ContinueWhenAllImpl(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task[] array = TaskFactory.CheckMultiContinuationTasksAndCopy(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			return TaskFactory.CommonCWAllLogic(array).ContinueWith<TResult>(delegate(Task<Task[]> completedTasks, object state)
			{
				completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
				return ((Func<Task[], TResult>)state)(completedTasks.Result);
			}, continuationFunction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x0007E33C File Offset: 0x0007C53C
		internal static Task<TResult> ContinueWhenAllImpl(Task[] tasks, Action<Task[]> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task[] array = TaskFactory.CheckMultiContinuationTasksAndCopy(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			return TaskFactory.CommonCWAllLogic(array).ContinueWith<TResult>(delegate(Task<Task[]> completedTasks, object state)
			{
				completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
				((Action<Task[]>)state)(completedTasks.Result);
				return default(TResult);
			}, continuationAction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x0007E3B9 File Offset: 0x0007C5B9
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x0007E3E2 File Offset: 0x0007C5E2
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x0007E406 File Offset: 0x0007C606
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x0007E42A File Offset: 0x0007C62A
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x0007E446 File Offset: 0x0007C646
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x0007E46F File Offset: 0x0007C66F
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x0007E493 File Offset: 0x0007C693
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x0007E4B7 File Offset: 0x0007C6B7
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x0007E4D4 File Offset: 0x0007C6D4
		internal static Task<TResult> ContinueWhenAnyImpl(Task[] tasks, Action<Task> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException("The tasks argument contains no tasks.", "tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<Task> task = TaskFactory.CommonCWAnyLogic(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			return task.ContinueWith<TResult>(delegate(Task<Task> completedTask, object state)
			{
				((Action<Task>)state)(completedTask.Result);
				return default(TResult);
			}, continuationAction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x0007E560 File Offset: 0x0007C760
		internal static Task<TResult> ContinueWhenAnyImpl(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException("The tasks argument contains no tasks.", "tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<Task> task = TaskFactory.CommonCWAnyLogic(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			return task.ContinueWith<TResult>((Task<Task> completedTask, object state) => ((Func<Task, TResult>)state)(completedTask.Result), continuationFunction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x0007E5EC File Offset: 0x0007C7EC
		internal static Task<TResult> ContinueWhenAnyImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException("The tasks argument contains no tasks.", "tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<Task> task = TaskFactory.CommonCWAnyLogic(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAnyFuncDelegate, continuationFunction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x0007E660 File Offset: 0x0007C860
		internal static Task<TResult> ContinueWhenAnyImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException("The tasks argument contains no tasks.", "tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<Task> task = TaskFactory.CommonCWAnyLogic(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAnyActionDelegate, continuationAction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x04001B53 RID: 6995
		private CancellationToken m_defaultCancellationToken;

		// Token: 0x04001B54 RID: 6996
		private TaskScheduler m_defaultScheduler;

		// Token: 0x04001B55 RID: 6997
		private TaskCreationOptions m_defaultCreationOptions;

		// Token: 0x04001B56 RID: 6998
		private TaskContinuationOptions m_defaultContinuationOptions;

		// Token: 0x02000314 RID: 788
		private sealed class FromAsyncTrimPromise<TInstance> : Task<TResult> where TInstance : class
		{
			// Token: 0x060022F4 RID: 8948 RVA: 0x0007E6D2 File Offset: 0x0007C8D2
			internal FromAsyncTrimPromise(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod)
			{
				this.m_thisRef = thisRef;
				this.m_endMethod = endMethod;
			}

			// Token: 0x060022F5 RID: 8949 RVA: 0x0007E6E8 File Offset: 0x0007C8E8
			internal static void CompleteFromAsyncResult(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> fromAsyncTrimPromise = asyncResult.AsyncState as TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>;
				if (fromAsyncTrimPromise == null)
				{
					throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or the End method was called multiple times with the same IAsyncResult.", "asyncResult");
				}
				TInstance thisRef = fromAsyncTrimPromise.m_thisRef;
				Func<TInstance, IAsyncResult, TResult> endMethod = fromAsyncTrimPromise.m_endMethod;
				fromAsyncTrimPromise.m_thisRef = default(TInstance);
				fromAsyncTrimPromise.m_endMethod = null;
				if (endMethod == null)
				{
					throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or the End method was called multiple times with the same IAsyncResult.", "asyncResult");
				}
				if (!asyncResult.CompletedSynchronously)
				{
					fromAsyncTrimPromise.Complete(thisRef, endMethod, asyncResult, true);
				}
			}

			// Token: 0x060022F6 RID: 8950 RVA: 0x0007E768 File Offset: 0x0007C968
			internal void Complete(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod, IAsyncResult asyncResult, bool requiresSynchronization)
			{
				try
				{
					TResult tresult = endMethod(thisRef, asyncResult);
					if (requiresSynchronization)
					{
						base.TrySetResult(tresult);
					}
					else
					{
						base.DangerousSetResult(tresult);
					}
				}
				catch (OperationCanceledException ex)
				{
					base.TrySetCanceled(ex.CancellationToken, ex);
				}
				catch (Exception ex2)
				{
					base.TrySetException(ex2);
				}
			}

			// Token: 0x060022F7 RID: 8951 RVA: 0x0007E7D0 File Offset: 0x0007C9D0
			// Note: this type is marked as 'beforefieldinit'.
			static FromAsyncTrimPromise()
			{
			}

			// Token: 0x04001B57 RID: 6999
			internal static readonly AsyncCallback s_completeFromAsyncResult = new AsyncCallback(TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.CompleteFromAsyncResult);

			// Token: 0x04001B58 RID: 7000
			private TInstance m_thisRef;

			// Token: 0x04001B59 RID: 7001
			private Func<TInstance, IAsyncResult, TResult> m_endMethod;
		}

		// Token: 0x02000315 RID: 789
		[CompilerGenerated]
		private sealed class <>c__DisplayClass32_0
		{
			// Token: 0x060022F8 RID: 8952 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass32_0()
			{
			}

			// Token: 0x060022F9 RID: 8953 RVA: 0x0007E7E3 File Offset: 0x0007C9E3
			internal void <FromAsyncImpl>b__0(object <p0>)
			{
				TaskFactory<TResult>.FromAsyncCoreLogic(this.asyncResult, this.endFunction, this.endAction, this.promise, true);
			}

			// Token: 0x060022FA RID: 8954 RVA: 0x0007E804 File Offset: 0x0007CA04
			internal void <FromAsyncImpl>b__1(object <p0>, bool <p1>)
			{
				try
				{
					this.t.InternalRunSynchronously(this.scheduler, false);
				}
				catch (Exception ex)
				{
					this.promise.TrySetException(ex);
				}
			}

			// Token: 0x04001B5A RID: 7002
			public IAsyncResult asyncResult;

			// Token: 0x04001B5B RID: 7003
			public Func<IAsyncResult, TResult> endFunction;

			// Token: 0x04001B5C RID: 7004
			public Action<IAsyncResult> endAction;

			// Token: 0x04001B5D RID: 7005
			public Task<TResult> promise;

			// Token: 0x04001B5E RID: 7006
			public Task t;

			// Token: 0x04001B5F RID: 7007
			public TaskScheduler scheduler;
		}

		// Token: 0x02000316 RID: 790
		[CompilerGenerated]
		private sealed class <>c__DisplayClass35_0
		{
			// Token: 0x060022FB RID: 8955 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass35_0()
			{
			}

			// Token: 0x060022FC RID: 8956 RVA: 0x0007E848 File Offset: 0x0007CA48
			internal void <FromAsyncImpl>b__0(IAsyncResult iar)
			{
				if (!iar.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(iar, this.endFunction, this.endAction, this.promise, true);
				}
			}

			// Token: 0x04001B60 RID: 7008
			public Func<IAsyncResult, TResult> endFunction;

			// Token: 0x04001B61 RID: 7009
			public Action<IAsyncResult> endAction;

			// Token: 0x04001B62 RID: 7010
			public Task<TResult> promise;
		}

		// Token: 0x02000317 RID: 791
		[CompilerGenerated]
		private sealed class <>c__DisplayClass38_0<TArg1>
		{
			// Token: 0x060022FD RID: 8957 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass38_0()
			{
			}

			// Token: 0x060022FE RID: 8958 RVA: 0x0007E86B File Offset: 0x0007CA6B
			internal void <FromAsyncImpl>b__0(IAsyncResult iar)
			{
				if (!iar.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(iar, this.endFunction, this.endAction, this.promise, true);
				}
			}

			// Token: 0x04001B63 RID: 7011
			public Func<IAsyncResult, TResult> endFunction;

			// Token: 0x04001B64 RID: 7012
			public Action<IAsyncResult> endAction;

			// Token: 0x04001B65 RID: 7013
			public Task<TResult> promise;
		}

		// Token: 0x02000318 RID: 792
		[CompilerGenerated]
		private sealed class <>c__DisplayClass41_0<TArg1, TArg2>
		{
			// Token: 0x060022FF RID: 8959 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass41_0()
			{
			}

			// Token: 0x06002300 RID: 8960 RVA: 0x0007E88E File Offset: 0x0007CA8E
			internal void <FromAsyncImpl>b__0(IAsyncResult iar)
			{
				if (!iar.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(iar, this.endFunction, this.endAction, this.promise, true);
				}
			}

			// Token: 0x04001B66 RID: 7014
			public Func<IAsyncResult, TResult> endFunction;

			// Token: 0x04001B67 RID: 7015
			public Action<IAsyncResult> endAction;

			// Token: 0x04001B68 RID: 7016
			public Task<TResult> promise;
		}

		// Token: 0x02000319 RID: 793
		[CompilerGenerated]
		private sealed class <>c__DisplayClass44_0<TArg1, TArg2, TArg3>
		{
			// Token: 0x06002301 RID: 8961 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass44_0()
			{
			}

			// Token: 0x06002302 RID: 8962 RVA: 0x0007E8B1 File Offset: 0x0007CAB1
			internal void <FromAsyncImpl>b__0(IAsyncResult iar)
			{
				if (!iar.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(iar, this.endFunction, this.endAction, this.promise, true);
				}
			}

			// Token: 0x04001B69 RID: 7017
			public Func<IAsyncResult, TResult> endFunction;

			// Token: 0x04001B6A RID: 7018
			public Action<IAsyncResult> endAction;

			// Token: 0x04001B6B RID: 7019
			public Task<TResult> promise;
		}

		// Token: 0x0200031A RID: 794
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002303 RID: 8963 RVA: 0x0007E8D4 File Offset: 0x0007CAD4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002304 RID: 8964 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06002305 RID: 8965 RVA: 0x0007E8E0 File Offset: 0x0007CAE0
			internal TResult <ContinueWhenAllImpl>b__58_0(Task<Task[]> completedTasks, object state)
			{
				completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
				return ((Func<Task[], TResult>)state)(completedTasks.Result);
			}

			// Token: 0x06002306 RID: 8966 RVA: 0x0007E8FC File Offset: 0x0007CAFC
			internal TResult <ContinueWhenAllImpl>b__59_0(Task<Task[]> completedTasks, object state)
			{
				completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
				((Action<Task[]>)state)(completedTasks.Result);
				return default(TResult);
			}

			// Token: 0x06002307 RID: 8967 RVA: 0x0007E92C File Offset: 0x0007CB2C
			internal TResult <ContinueWhenAnyImpl>b__68_0(Task<Task> completedTask, object state)
			{
				((Action<Task>)state)(completedTask.Result);
				return default(TResult);
			}

			// Token: 0x06002308 RID: 8968 RVA: 0x0007E953 File Offset: 0x0007CB53
			internal TResult <ContinueWhenAnyImpl>b__69_0(Task<Task> completedTask, object state)
			{
				return ((Func<Task, TResult>)state)(completedTask.Result);
			}

			// Token: 0x04001B6C RID: 7020
			public static readonly TaskFactory<TResult>.<>c <>9 = new TaskFactory<TResult>.<>c();

			// Token: 0x04001B6D RID: 7021
			public static Func<Task<Task[]>, object, TResult> <>9__58_0;

			// Token: 0x04001B6E RID: 7022
			public static Func<Task<Task[]>, object, TResult> <>9__59_0;

			// Token: 0x04001B6F RID: 7023
			public static Func<Task<Task>, object, TResult> <>9__68_0;

			// Token: 0x04001B70 RID: 7024
			public static Func<Task<Task>, object, TResult> <>9__69_0;
		}
	}
}
