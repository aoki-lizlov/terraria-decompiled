using System;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007EB RID: 2027
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct AsyncTaskMethodBuilder
	{
		// Token: 0x06004600 RID: 17920 RVA: 0x000E5F6C File Offset: 0x000E416C
		public static AsyncTaskMethodBuilder Create()
		{
			return default(AsyncTaskMethodBuilder);
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x000E5F84 File Offset: 0x000E4184
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			if (stateMachine == null)
			{
				throw new ArgumentNullException("stateMachine");
			}
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

		// Token: 0x06004602 RID: 17922 RVA: 0x000E5FE4 File Offset: 0x000E41E4
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.m_builder.SetStateMachine(stateMachine);
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x000E5FF2 File Offset: 0x000E41F2
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this.m_builder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x000E6001 File Offset: 0x000E4201
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this.m_builder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06004605 RID: 17925 RVA: 0x000E6010 File Offset: 0x000E4210
		public Task Task
		{
			get
			{
				return this.m_builder.Task;
			}
		}

		// Token: 0x06004606 RID: 17926 RVA: 0x000E601D File Offset: 0x000E421D
		public void SetResult()
		{
			this.m_builder.SetResult(AsyncTaskMethodBuilder.s_cachedCompleted);
		}

		// Token: 0x06004607 RID: 17927 RVA: 0x000E602F File Offset: 0x000E422F
		public void SetException(Exception exception)
		{
			this.m_builder.SetException(exception);
		}

		// Token: 0x06004608 RID: 17928 RVA: 0x000E603D File Offset: 0x000E423D
		internal void SetNotificationForWaitCompletion(bool enabled)
		{
			this.m_builder.SetNotificationForWaitCompletion(enabled);
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06004609 RID: 17929 RVA: 0x000E604B File Offset: 0x000E424B
		internal object ObjectIdForDebugger
		{
			get
			{
				return this.Task;
			}
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x000E6053 File Offset: 0x000E4253
		// Note: this type is marked as 'beforefieldinit'.
		static AsyncTaskMethodBuilder()
		{
		}

		// Token: 0x04002CDA RID: 11482
		private static readonly Task<VoidTaskResult> s_cachedCompleted = AsyncTaskMethodBuilder<VoidTaskResult>.s_defaultResultTask;

		// Token: 0x04002CDB RID: 11483
		private AsyncTaskMethodBuilder<VoidTaskResult> m_builder;
	}
}
