using System;

namespace System.Threading
{
	// Token: 0x02000282 RID: 642
	internal class CancellationCallbackInfo
	{
		// Token: 0x06001E16 RID: 7702 RVA: 0x00070BEC File Offset: 0x0006EDEC
		internal CancellationCallbackInfo(Action<object> callback, object stateForCallback, ExecutionContext targetExecutionContext, CancellationTokenSource cancellationTokenSource)
		{
			this.Callback = callback;
			this.StateForCallback = stateForCallback;
			this.TargetExecutionContext = targetExecutionContext;
			this.CancellationTokenSource = cancellationTokenSource;
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x00070C14 File Offset: 0x0006EE14
		internal void ExecuteCallback()
		{
			if (this.TargetExecutionContext != null)
			{
				ContextCallback contextCallback = CancellationCallbackInfo.s_executionContextCallback;
				if (contextCallback == null)
				{
					contextCallback = (CancellationCallbackInfo.s_executionContextCallback = new ContextCallback(CancellationCallbackInfo.ExecutionContextCallback));
				}
				ExecutionContext.Run(this.TargetExecutionContext, contextCallback, this);
				return;
			}
			CancellationCallbackInfo.ExecutionContextCallback(this);
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00070C5C File Offset: 0x0006EE5C
		private static void ExecutionContextCallback(object obj)
		{
			CancellationCallbackInfo cancellationCallbackInfo = obj as CancellationCallbackInfo;
			cancellationCallbackInfo.Callback(cancellationCallbackInfo.StateForCallback);
		}

		// Token: 0x04001974 RID: 6516
		internal readonly Action<object> Callback;

		// Token: 0x04001975 RID: 6517
		internal readonly object StateForCallback;

		// Token: 0x04001976 RID: 6518
		internal readonly ExecutionContext TargetExecutionContext;

		// Token: 0x04001977 RID: 6519
		internal readonly CancellationTokenSource CancellationTokenSource;

		// Token: 0x04001978 RID: 6520
		private static ContextCallback s_executionContextCallback;

		// Token: 0x02000283 RID: 643
		internal sealed class WithSyncContext : CancellationCallbackInfo
		{
			// Token: 0x06001E19 RID: 7705 RVA: 0x00070C81 File Offset: 0x0006EE81
			internal WithSyncContext(Action<object> callback, object stateForCallback, ExecutionContext targetExecutionContext, CancellationTokenSource cancellationTokenSource, SynchronizationContext targetSyncContext)
				: base(callback, stateForCallback, targetExecutionContext, cancellationTokenSource)
			{
				this.TargetSyncContext = targetSyncContext;
			}

			// Token: 0x04001979 RID: 6521
			internal readonly SynchronizationContext TargetSyncContext;
		}
	}
}
