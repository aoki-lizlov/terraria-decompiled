using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007AD RID: 1965
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncValueTaskMethodBuilder
	{
		// Token: 0x06004558 RID: 17752 RVA: 0x000E4C10 File Offset: 0x000E2E10
		public static AsyncValueTaskMethodBuilder Create()
		{
			return default(AsyncValueTaskMethodBuilder);
		}

		// Token: 0x06004559 RID: 17753 RVA: 0x000E4C26 File Offset: 0x000E2E26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x0600455A RID: 17754 RVA: 0x000E4C34 File Offset: 0x000E2E34
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this._methodBuilder.SetStateMachine(stateMachine);
		}

		// Token: 0x0600455B RID: 17755 RVA: 0x000E4C42 File Offset: 0x000E2E42
		public void SetResult()
		{
			if (this._useBuilder)
			{
				this._methodBuilder.SetResult();
				return;
			}
			this._haveResult = true;
		}

		// Token: 0x0600455C RID: 17756 RVA: 0x000E4C5F File Offset: 0x000E2E5F
		public void SetException(Exception exception)
		{
			this._methodBuilder.SetException(exception);
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x0600455D RID: 17757 RVA: 0x000E4C70 File Offset: 0x000E2E70
		public ValueTask Task
		{
			get
			{
				if (this._haveResult)
				{
					return default(ValueTask);
				}
				this._useBuilder = true;
				return new ValueTask(this._methodBuilder.Task);
			}
		}

		// Token: 0x0600455E RID: 17758 RVA: 0x000E4CA6 File Offset: 0x000E2EA6
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._useBuilder = true;
			this._methodBuilder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x000E4CBC File Offset: 0x000E2EBC
		[SecuritySafeCritical]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._useBuilder = true;
			this._methodBuilder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x04002CA5 RID: 11429
		private AsyncTaskMethodBuilder _methodBuilder;

		// Token: 0x04002CA6 RID: 11430
		private bool _haveResult;

		// Token: 0x04002CA7 RID: 11431
		private bool _useBuilder;
	}
}
