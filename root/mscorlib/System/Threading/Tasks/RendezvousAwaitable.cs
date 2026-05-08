using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace System.Threading.Tasks
{
	// Token: 0x020002E9 RID: 745
	internal class RendezvousAwaitable<TResult> : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x0007936E File Offset: 0x0007756E
		// (set) Token: 0x06002184 RID: 8580 RVA: 0x00079376 File Offset: 0x00077576
		public bool RunContinuationsAsynchronously
		{
			[CompilerGenerated]
			get
			{
				return this.<RunContinuationsAsynchronously>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RunContinuationsAsynchronously>k__BackingField = value;
			}
		} = true;

		// Token: 0x06002185 RID: 8581 RVA: 0x000025CE File Offset: 0x000007CE
		public RendezvousAwaitable<TResult> GetAwaiter()
		{
			return this;
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x0007937F File Offset: 0x0007757F
		public bool IsCompleted
		{
			get
			{
				return Volatile.Read<Action>(ref this._continuation) != null;
			}
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x00079390 File Offset: 0x00077590
		public TResult GetResult()
		{
			this._continuation = null;
			ExceptionDispatchInfo error = this._error;
			if (error != null)
			{
				this._error = null;
				error.Throw();
			}
			TResult result = this._result;
			this._result = default(TResult);
			return result;
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x000793CD File Offset: 0x000775CD
		public void SetResult(TResult result)
		{
			this._result = result;
			this.NotifyAwaiter();
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x000793DC File Offset: 0x000775DC
		public void SetCanceled(CancellationToken token = default(CancellationToken))
		{
			this.SetException(token.IsCancellationRequested ? new OperationCanceledException(token) : new OperationCanceledException());
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x000793FA File Offset: 0x000775FA
		public void SetException(Exception exception)
		{
			this._error = ExceptionDispatchInfo.Capture(exception);
			this.NotifyAwaiter();
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x00079410 File Offset: 0x00077610
		private void NotifyAwaiter()
		{
			Action action = this._continuation ?? Interlocked.CompareExchange<Action>(ref this._continuation, RendezvousAwaitable<TResult>.s_completionSentinel, null);
			if (action != null)
			{
				if (this.RunContinuationsAsynchronously)
				{
					Task.Run(action);
					return;
				}
				action();
			}
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x00079452 File Offset: 0x00077652
		public void OnCompleted(Action continuation)
		{
			if ((this._continuation ?? Interlocked.CompareExchange<Action>(ref this._continuation, continuation, null)) != null)
			{
				Task.Run(continuation);
			}
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x00079474 File Offset: 0x00077674
		public void UnsafeOnCompleted(Action continuation)
		{
			this.OnCompleted(continuation);
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("DEBUG")]
		private void AssertResultConsistency(bool expectedCompleted)
		{
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x0007947D File Offset: 0x0007767D
		public RendezvousAwaitable()
		{
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x0007948C File Offset: 0x0007768C
		// Note: this type is marked as 'beforefieldinit'.
		static RendezvousAwaitable()
		{
		}

		// Token: 0x04001AAD RID: 6829
		private static readonly Action s_completionSentinel = delegate
		{
		};

		// Token: 0x04001AAE RID: 6830
		private Action _continuation;

		// Token: 0x04001AAF RID: 6831
		private ExceptionDispatchInfo _error;

		// Token: 0x04001AB0 RID: 6832
		private TResult _result;

		// Token: 0x04001AB1 RID: 6833
		[CompilerGenerated]
		private bool <RunContinuationsAsynchronously>k__BackingField;

		// Token: 0x020002EA RID: 746
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002191 RID: 8593 RVA: 0x000794A3 File Offset: 0x000776A3
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002192 RID: 8594 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06002193 RID: 8595 RVA: 0x00004088 File Offset: 0x00002288
			internal void <.cctor>b__20_0()
			{
			}

			// Token: 0x04001AB2 RID: 6834
			public static readonly RendezvousAwaitable<TResult>.<>c <>9 = new RendezvousAwaitable<TResult>.<>c();
		}
	}
}
