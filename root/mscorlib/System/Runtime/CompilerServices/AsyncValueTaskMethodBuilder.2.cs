using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007AE RID: 1966
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncValueTaskMethodBuilder<TResult>
	{
		// Token: 0x06004560 RID: 17760 RVA: 0x000E4CD4 File Offset: 0x000E2ED4
		public static AsyncValueTaskMethodBuilder<TResult> Create()
		{
			return default(AsyncValueTaskMethodBuilder<TResult>);
		}

		// Token: 0x06004561 RID: 17761 RVA: 0x000E4CEA File Offset: 0x000E2EEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x06004562 RID: 17762 RVA: 0x000E4CF8 File Offset: 0x000E2EF8
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this._methodBuilder.SetStateMachine(stateMachine);
		}

		// Token: 0x06004563 RID: 17763 RVA: 0x000E4D06 File Offset: 0x000E2F06
		public void SetResult(TResult result)
		{
			if (this._useBuilder)
			{
				this._methodBuilder.SetResult(result);
				return;
			}
			this._result = result;
			this._haveResult = true;
		}

		// Token: 0x06004564 RID: 17764 RVA: 0x000E4D2B File Offset: 0x000E2F2B
		public void SetException(Exception exception)
		{
			this._methodBuilder.SetException(exception);
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06004565 RID: 17765 RVA: 0x000E4D39 File Offset: 0x000E2F39
		public ValueTask<TResult> Task
		{
			get
			{
				if (this._haveResult)
				{
					return new ValueTask<TResult>(this._result);
				}
				this._useBuilder = true;
				return new ValueTask<TResult>(this._methodBuilder.Task);
			}
		}

		// Token: 0x06004566 RID: 17766 RVA: 0x000E4D66 File Offset: 0x000E2F66
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._useBuilder = true;
			this._methodBuilder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x06004567 RID: 17767 RVA: 0x000E4D7C File Offset: 0x000E2F7C
		[SecuritySafeCritical]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._useBuilder = true;
			this._methodBuilder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x04002CA8 RID: 11432
		private AsyncTaskMethodBuilder<TResult> _methodBuilder;

		// Token: 0x04002CA9 RID: 11433
		private TResult _result;

		// Token: 0x04002CAA RID: 11434
		private bool _haveResult;

		// Token: 0x04002CAB RID: 11435
		private bool _useBuilder;
	}
}
