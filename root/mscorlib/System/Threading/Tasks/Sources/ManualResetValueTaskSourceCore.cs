using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x0200035B RID: 859
	[StructLayout(LayoutKind.Auto)]
	public struct ManualResetValueTaskSourceCore<TResult>
	{
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06002513 RID: 9491 RVA: 0x0008490B File Offset: 0x00082B0B
		// (set) Token: 0x06002514 RID: 9492 RVA: 0x00084913 File Offset: 0x00082B13
		public bool RunContinuationsAsynchronously
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<RunContinuationsAsynchronously>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RunContinuationsAsynchronously>k__BackingField = value;
			}
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x0008491C File Offset: 0x00082B1C
		public void Reset()
		{
			this._version += 1;
			this._completed = false;
			this._result = default(TResult);
			this._error = null;
			this._executionContext = null;
			this._capturedContext = null;
			this._continuation = null;
			this._continuationState = null;
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x0008496E File Offset: 0x00082B6E
		public void SetResult(TResult result)
		{
			this._result = result;
			this.SignalCompletion();
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x0008497D File Offset: 0x00082B7D
		public void SetException(Exception error)
		{
			this._error = ExceptionDispatchInfo.Capture(error);
			this.SignalCompletion();
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06002518 RID: 9496 RVA: 0x00084991 File Offset: 0x00082B91
		public short Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x00084999 File Offset: 0x00082B99
		public ValueTaskSourceStatus GetStatus(short token)
		{
			this.ValidateToken(token);
			if (!this._completed)
			{
				return ValueTaskSourceStatus.Pending;
			}
			if (this._error == null)
			{
				return ValueTaskSourceStatus.Succeeded;
			}
			if (!(this._error.SourceException is OperationCanceledException))
			{
				return ValueTaskSourceStatus.Faulted;
			}
			return ValueTaskSourceStatus.Canceled;
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x000849CB File Offset: 0x00082BCB
		[StackTraceHidden]
		public TResult GetResult(short token)
		{
			this.ValidateToken(token);
			if (!this._completed)
			{
				ManualResetValueTaskSourceCoreShared.ThrowInvalidOperationException();
			}
			ExceptionDispatchInfo error = this._error;
			if (error != null)
			{
				error.Throw();
			}
			return this._result;
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x000849F8 File Offset: 0x00082BF8
		public void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
		{
			if (continuation == null)
			{
				throw new ArgumentNullException("continuation");
			}
			this.ValidateToken(token);
			if ((flags & ValueTaskSourceOnCompletedFlags.FlowExecutionContext) != ValueTaskSourceOnCompletedFlags.None)
			{
				this._executionContext = ExecutionContext.Capture();
			}
			if ((flags & ValueTaskSourceOnCompletedFlags.UseSchedulingContext) != ValueTaskSourceOnCompletedFlags.None)
			{
				SynchronizationContext synchronizationContext = SynchronizationContext.Current;
				if (synchronizationContext != null && synchronizationContext.GetType() != typeof(SynchronizationContext))
				{
					this._capturedContext = synchronizationContext;
				}
				else
				{
					TaskScheduler taskScheduler = TaskScheduler.Current;
					if (taskScheduler != TaskScheduler.Default)
					{
						this._capturedContext = taskScheduler;
					}
				}
			}
			object obj = this._continuation;
			if (obj == null)
			{
				this._continuationState = state;
				obj = Interlocked.CompareExchange<Action<object>>(ref this._continuation, continuation, null);
			}
			if (obj != null)
			{
				if (obj != ManualResetValueTaskSourceCoreShared.s_sentinel)
				{
					ManualResetValueTaskSourceCoreShared.ThrowInvalidOperationException();
				}
				object capturedContext = this._capturedContext;
				if (capturedContext != null)
				{
					SynchronizationContext synchronizationContext2 = capturedContext as SynchronizationContext;
					if (synchronizationContext2 != null)
					{
						synchronizationContext2.Post(delegate(object s)
						{
							Tuple<Action<object>, object> tuple = (Tuple<Action<object>, object>)s;
							tuple.Item1(tuple.Item2);
						}, Tuple.Create<Action<object>, object>(continuation, state));
						return;
					}
					TaskScheduler taskScheduler2 = capturedContext as TaskScheduler;
					if (taskScheduler2 == null)
					{
						return;
					}
					Task.Factory.StartNew(continuation, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, taskScheduler2);
				}
				else
				{
					if (this._executionContext != null)
					{
						ThreadPool.QueueUserWorkItem<object>(continuation, state, true);
						return;
					}
					ThreadPool.UnsafeQueueUserWorkItem<object>(continuation, state, true);
					return;
				}
			}
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x00084B24 File Offset: 0x00082D24
		private void ValidateToken(short token)
		{
			if (token != this._version)
			{
				ManualResetValueTaskSourceCoreShared.ThrowInvalidOperationException();
			}
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x00084B34 File Offset: 0x00082D34
		private void SignalCompletion()
		{
			if (this._completed)
			{
				ManualResetValueTaskSourceCoreShared.ThrowInvalidOperationException();
			}
			this._completed = true;
			if (this._continuation != null || Interlocked.CompareExchange<Action<object>>(ref this._continuation, ManualResetValueTaskSourceCoreShared.s_sentinel, null) != null)
			{
				if (this._executionContext != null)
				{
					ExecutionContext.RunInternal<ManualResetValueTaskSourceCore<TResult>>(this._executionContext, delegate(ref ManualResetValueTaskSourceCore<TResult> s)
					{
						s.InvokeContinuation();
					}, ref this);
					return;
				}
				this.InvokeContinuation();
			}
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x00084BAC File Offset: 0x00082DAC
		private void InvokeContinuation()
		{
			object capturedContext = this._capturedContext;
			if (capturedContext != null)
			{
				SynchronizationContext synchronizationContext = capturedContext as SynchronizationContext;
				if (synchronizationContext != null)
				{
					synchronizationContext.Post(delegate(object s)
					{
						Tuple<Action<object>, object> tuple = (Tuple<Action<object>, object>)s;
						tuple.Item1(tuple.Item2);
					}, Tuple.Create<Action<object>, object>(this._continuation, this._continuationState));
					return;
				}
				TaskScheduler taskScheduler = capturedContext as TaskScheduler;
				if (taskScheduler == null)
				{
					return;
				}
				Task.Factory.StartNew(this._continuation, this._continuationState, CancellationToken.None, TaskCreationOptions.DenyChildAttach, taskScheduler);
				return;
			}
			else
			{
				if (!this.RunContinuationsAsynchronously)
				{
					this._continuation(this._continuationState);
					return;
				}
				if (this._executionContext != null)
				{
					ThreadPool.QueueUserWorkItem<object>(this._continuation, this._continuationState, true);
					return;
				}
				ThreadPool.UnsafeQueueUserWorkItem<object>(this._continuation, this._continuationState, true);
				return;
			}
		}

		// Token: 0x04001C48 RID: 7240
		private Action<object> _continuation;

		// Token: 0x04001C49 RID: 7241
		private object _continuationState;

		// Token: 0x04001C4A RID: 7242
		private ExecutionContext _executionContext;

		// Token: 0x04001C4B RID: 7243
		private object _capturedContext;

		// Token: 0x04001C4C RID: 7244
		private bool _completed;

		// Token: 0x04001C4D RID: 7245
		private TResult _result;

		// Token: 0x04001C4E RID: 7246
		private ExceptionDispatchInfo _error;

		// Token: 0x04001C4F RID: 7247
		private short _version;

		// Token: 0x04001C50 RID: 7248
		[CompilerGenerated]
		private bool <RunContinuationsAsynchronously>k__BackingField;

		// Token: 0x0200035C RID: 860
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600251F RID: 9503 RVA: 0x00084C7A File Offset: 0x00082E7A
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002520 RID: 9504 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06002521 RID: 9505 RVA: 0x00084C88 File Offset: 0x00082E88
			internal void <OnCompleted>b__19_0(object s)
			{
				Tuple<Action<object>, object> tuple = (Tuple<Action<object>, object>)s;
				tuple.Item1(tuple.Item2);
			}

			// Token: 0x06002522 RID: 9506 RVA: 0x00084CAD File Offset: 0x00082EAD
			internal void <SignalCompletion>b__21_0(ref ManualResetValueTaskSourceCore<TResult> s)
			{
				s.InvokeContinuation();
			}

			// Token: 0x06002523 RID: 9507 RVA: 0x00084CB8 File Offset: 0x00082EB8
			internal void <InvokeContinuation>b__22_0(object s)
			{
				Tuple<Action<object>, object> tuple = (Tuple<Action<object>, object>)s;
				tuple.Item1(tuple.Item2);
			}

			// Token: 0x04001C51 RID: 7249
			public static readonly ManualResetValueTaskSourceCore<TResult>.<>c <>9 = new ManualResetValueTaskSourceCore<TResult>.<>c();

			// Token: 0x04001C52 RID: 7250
			public static SendOrPostCallback <>9__19_0;

			// Token: 0x04001C53 RID: 7251
			public static ContextCallback<ManualResetValueTaskSourceCore<TResult>> <>9__21_0;

			// Token: 0x04001C54 RID: 7252
			public static SendOrPostCallback <>9__22_0;
		}
	}
}
