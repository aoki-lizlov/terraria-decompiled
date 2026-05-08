using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007EE RID: 2030
	internal struct AsyncMethodBuilderCore
	{
		// Token: 0x0600461B RID: 17947 RVA: 0x000E6614 File Offset: 0x000E4814
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		internal static void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			if (stateMachine == null)
			{
				throw new ArgumentNullException("stateMachine");
			}
			Thread currentThread = Thread.CurrentThread;
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.EstablishCopyOnWriteScope(ref executionContextSwitcher);
				stateMachine.MoveNext();
			}
			finally
			{
				executionContextSwitcher.Undo();
			}
		}

		// Token: 0x0600461C RID: 17948 RVA: 0x000E667C File Offset: 0x000E487C
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			if (stateMachine == null)
			{
				throw new ArgumentNullException("stateMachine");
			}
			if (this.m_stateMachine != null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The builder was not properly initialized."));
			}
			this.m_stateMachine = stateMachine;
		}

		// Token: 0x0600461D RID: 17949 RVA: 0x000E66AC File Offset: 0x000E48AC
		[SecuritySafeCritical]
		internal Action GetCompletionAction(Task taskForTracing, ref AsyncMethodBuilderCore.MoveNextRunner runnerToInitialize)
		{
			Debugger.NotifyOfCrossThreadDependency();
			ExecutionContext executionContext = ExecutionContext.FastCapture();
			Action action;
			AsyncMethodBuilderCore.MoveNextRunner moveNextRunner;
			if (executionContext != null && executionContext.IsPreAllocatedDefault)
			{
				action = this.m_defaultContextAction;
				if (action != null)
				{
					return action;
				}
				moveNextRunner = new AsyncMethodBuilderCore.MoveNextRunner(executionContext, this.m_stateMachine);
				action = new Action(moveNextRunner.Run);
				if (taskForTracing != null)
				{
					action = (this.m_defaultContextAction = this.OutputAsyncCausalityEvents(taskForTracing, action));
				}
				else
				{
					this.m_defaultContextAction = action;
				}
			}
			else
			{
				moveNextRunner = new AsyncMethodBuilderCore.MoveNextRunner(executionContext, this.m_stateMachine);
				action = new Action(moveNextRunner.Run);
				if (taskForTracing != null)
				{
					action = this.OutputAsyncCausalityEvents(taskForTracing, action);
				}
			}
			if (this.m_stateMachine == null)
			{
				runnerToInitialize = moveNextRunner;
			}
			return action;
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x000E6748 File Offset: 0x000E4948
		private Action OutputAsyncCausalityEvents(Task innerTask, Action continuation)
		{
			return AsyncMethodBuilderCore.CreateContinuationWrapper(continuation, delegate
			{
				AsyncCausalityTracer.TraceSynchronousWorkStart(CausalityTraceLevel.Required, innerTask.Id, CausalitySynchronousWork.Execution);
				continuation();
				AsyncCausalityTracer.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.Execution);
			}, innerTask);
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x000E6788 File Offset: 0x000E4988
		internal void PostBoxInitialization(IAsyncStateMachine stateMachine, AsyncMethodBuilderCore.MoveNextRunner runner, Task builtTask)
		{
			if (builtTask != null)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, builtTask.Id, "Async: " + stateMachine.GetType().Name, 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(builtTask);
				}
			}
			this.m_stateMachine = stateMachine;
			this.m_stateMachine.SetStateMachine(this.m_stateMachine);
			runner.m_stateMachine = this.m_stateMachine;
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x000E67F4 File Offset: 0x000E49F4
		internal static void ThrowAsync(Exception exception, SynchronizationContext targetContext)
		{
			ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(exception);
			if (targetContext != null)
			{
				try
				{
					targetContext.Post(delegate(object state)
					{
						((ExceptionDispatchInfo)state).Throw();
					}, exceptionDispatchInfo);
					return;
				}
				catch (Exception ex)
				{
					exceptionDispatchInfo = ExceptionDispatchInfo.Capture(new AggregateException(new Exception[] { exception, ex }));
				}
			}
			if (!WindowsRuntimeMarshal.ReportUnhandledError(exceptionDispatchInfo.SourceException))
			{
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					((ExceptionDispatchInfo)state).Throw();
				}, exceptionDispatchInfo);
			}
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x000E6894 File Offset: 0x000E4A94
		internal static Action CreateContinuationWrapper(Action continuation, Action invokeAction, Task innerTask = null)
		{
			return new Action(new AsyncMethodBuilderCore.ContinuationWrapper(continuation, invokeAction, innerTask).Invoke);
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x000E68AC File Offset: 0x000E4AAC
		internal static Action TryGetStateMachineForDebugger(Action action)
		{
			object target = action.Target;
			AsyncMethodBuilderCore.MoveNextRunner moveNextRunner = target as AsyncMethodBuilderCore.MoveNextRunner;
			if (moveNextRunner != null)
			{
				return new Action(moveNextRunner.m_stateMachine.MoveNext);
			}
			AsyncMethodBuilderCore.ContinuationWrapper continuationWrapper = target as AsyncMethodBuilderCore.ContinuationWrapper;
			if (continuationWrapper != null)
			{
				return AsyncMethodBuilderCore.TryGetStateMachineForDebugger(continuationWrapper.m_continuation);
			}
			return action;
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x000E68F4 File Offset: 0x000E4AF4
		internal static Task TryGetContinuationTask(Action action)
		{
			if (action != null)
			{
				AsyncMethodBuilderCore.ContinuationWrapper continuationWrapper = action.Target as AsyncMethodBuilderCore.ContinuationWrapper;
				if (continuationWrapper != null)
				{
					return continuationWrapper.m_innerTask;
				}
			}
			return null;
		}

		// Token: 0x04002CE4 RID: 11492
		internal IAsyncStateMachine m_stateMachine;

		// Token: 0x04002CE5 RID: 11493
		internal Action m_defaultContextAction;

		// Token: 0x020007EF RID: 2031
		internal sealed class MoveNextRunner
		{
			// Token: 0x06004624 RID: 17956 RVA: 0x000E691B File Offset: 0x000E4B1B
			[SecurityCritical]
			internal MoveNextRunner(ExecutionContext context, IAsyncStateMachine stateMachine)
			{
				this.m_context = context;
				this.m_stateMachine = stateMachine;
			}

			// Token: 0x06004625 RID: 17957 RVA: 0x000E6934 File Offset: 0x000E4B34
			[SecuritySafeCritical]
			internal void Run()
			{
				if (this.m_context != null)
				{
					try
					{
						ContextCallback contextCallback = AsyncMethodBuilderCore.MoveNextRunner.s_invokeMoveNext;
						if (contextCallback == null)
						{
							contextCallback = (AsyncMethodBuilderCore.MoveNextRunner.s_invokeMoveNext = new ContextCallback(AsyncMethodBuilderCore.MoveNextRunner.InvokeMoveNext));
						}
						ExecutionContext.Run(this.m_context, contextCallback, this.m_stateMachine, true);
						return;
					}
					finally
					{
						this.m_context.Dispose();
					}
				}
				this.m_stateMachine.MoveNext();
			}

			// Token: 0x06004626 RID: 17958 RVA: 0x000E69A4 File Offset: 0x000E4BA4
			[SecurityCritical]
			private static void InvokeMoveNext(object stateMachine)
			{
				((IAsyncStateMachine)stateMachine).MoveNext();
			}

			// Token: 0x04002CE6 RID: 11494
			private readonly ExecutionContext m_context;

			// Token: 0x04002CE7 RID: 11495
			internal IAsyncStateMachine m_stateMachine;

			// Token: 0x04002CE8 RID: 11496
			[SecurityCritical]
			private static ContextCallback s_invokeMoveNext;
		}

		// Token: 0x020007F0 RID: 2032
		private class ContinuationWrapper
		{
			// Token: 0x06004627 RID: 17959 RVA: 0x000E69B1 File Offset: 0x000E4BB1
			internal ContinuationWrapper(Action continuation, Action invokeAction, Task innerTask)
			{
				if (innerTask == null)
				{
					innerTask = AsyncMethodBuilderCore.TryGetContinuationTask(continuation);
				}
				this.m_continuation = continuation;
				this.m_innerTask = innerTask;
				this.m_invokeAction = invokeAction;
			}

			// Token: 0x06004628 RID: 17960 RVA: 0x000E69D9 File Offset: 0x000E4BD9
			internal void Invoke()
			{
				this.m_invokeAction();
			}

			// Token: 0x04002CE9 RID: 11497
			internal readonly Action m_continuation;

			// Token: 0x04002CEA RID: 11498
			private readonly Action m_invokeAction;

			// Token: 0x04002CEB RID: 11499
			internal readonly Task m_innerTask;
		}

		// Token: 0x020007F1 RID: 2033
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x06004629 RID: 17961 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x0600462A RID: 17962 RVA: 0x000E69E6 File Offset: 0x000E4BE6
			internal void <OutputAsyncCausalityEvents>b__0()
			{
				AsyncCausalityTracer.TraceSynchronousWorkStart(CausalityTraceLevel.Required, this.innerTask.Id, CausalitySynchronousWork.Execution);
				this.continuation();
				AsyncCausalityTracer.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.Execution);
			}

			// Token: 0x04002CEC RID: 11500
			public Task innerTask;

			// Token: 0x04002CED RID: 11501
			public Action continuation;
		}

		// Token: 0x020007F2 RID: 2034
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600462B RID: 17963 RVA: 0x000E6A0C File Offset: 0x000E4C0C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600462C RID: 17964 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x0600462D RID: 17965 RVA: 0x000E6A18 File Offset: 0x000E4C18
			internal void <ThrowAsync>b__7_0(object state)
			{
				((ExceptionDispatchInfo)state).Throw();
			}

			// Token: 0x0600462E RID: 17966 RVA: 0x000E6A18 File Offset: 0x000E4C18
			internal void <ThrowAsync>b__7_1(object state)
			{
				((ExceptionDispatchInfo)state).Throw();
			}

			// Token: 0x04002CEE RID: 11502
			public static readonly AsyncMethodBuilderCore.<>c <>9 = new AsyncMethodBuilderCore.<>c();

			// Token: 0x04002CEF RID: 11503
			public static SendOrPostCallback <>9__7_0;

			// Token: 0x04002CF0 RID: 11504
			public static WaitCallback <>9__7_1;
		}
	}
}
