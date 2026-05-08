using System;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007F3 RID: 2035
	public readonly struct YieldAwaitable
	{
		// Token: 0x0600462F RID: 17967 RVA: 0x000E6A28 File Offset: 0x000E4C28
		public YieldAwaitable.YieldAwaiter GetAwaiter()
		{
			return default(YieldAwaitable.YieldAwaiter);
		}

		// Token: 0x020007F4 RID: 2036
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public readonly struct YieldAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x17000AD2 RID: 2770
			// (get) Token: 0x06004630 RID: 17968 RVA: 0x0000408A File Offset: 0x0000228A
			public bool IsCompleted
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004631 RID: 17969 RVA: 0x000E6A3E File Offset: 0x000E4C3E
			[SecuritySafeCritical]
			public void OnCompleted(Action continuation)
			{
				YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, true);
			}

			// Token: 0x06004632 RID: 17970 RVA: 0x000E6A47 File Offset: 0x000E4C47
			[SecurityCritical]
			public void UnsafeOnCompleted(Action continuation)
			{
				YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, false);
			}

			// Token: 0x06004633 RID: 17971 RVA: 0x000E6A50 File Offset: 0x000E4C50
			[SecurityCritical]
			private static void QueueContinuation(Action continuation, bool flowContext)
			{
				if (continuation == null)
				{
					throw new ArgumentNullException("continuation");
				}
				SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
				if (currentNoFlow != null && currentNoFlow.GetType() != typeof(SynchronizationContext))
				{
					currentNoFlow.Post(YieldAwaitable.YieldAwaiter.s_sendOrPostCallbackRunAction, continuation);
					return;
				}
				TaskScheduler taskScheduler = TaskScheduler.Current;
				if (taskScheduler != TaskScheduler.Default)
				{
					Task.Factory.StartNew(continuation, default(CancellationToken), TaskCreationOptions.PreferFairness, taskScheduler);
					return;
				}
				if (flowContext)
				{
					ThreadPool.QueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, continuation);
					return;
				}
				ThreadPool.UnsafeQueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, continuation);
			}

			// Token: 0x06004634 RID: 17972 RVA: 0x00083042 File Offset: 0x00081242
			private static void RunAction(object state)
			{
				((Action)state)();
			}

			// Token: 0x06004635 RID: 17973 RVA: 0x00004088 File Offset: 0x00002288
			public void GetResult()
			{
			}

			// Token: 0x06004636 RID: 17974 RVA: 0x000E6ADA File Offset: 0x000E4CDA
			// Note: this type is marked as 'beforefieldinit'.
			static YieldAwaiter()
			{
			}

			// Token: 0x04002CF1 RID: 11505
			private static readonly WaitCallback s_waitCallbackRunAction = new WaitCallback(YieldAwaitable.YieldAwaiter.RunAction);

			// Token: 0x04002CF2 RID: 11506
			private static readonly SendOrPostCallback s_sendOrPostCallbackRunAction = new SendOrPostCallback(YieldAwaitable.YieldAwaiter.RunAction);
		}
	}
}
