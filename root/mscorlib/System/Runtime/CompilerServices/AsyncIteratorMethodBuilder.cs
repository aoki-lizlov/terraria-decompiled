using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007A9 RID: 1961
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncIteratorMethodBuilder
	{
		// Token: 0x0600454E RID: 17742 RVA: 0x000E4B98 File Offset: 0x000E2D98
		public static AsyncIteratorMethodBuilder Create()
		{
			return default(AsyncIteratorMethodBuilder);
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x000E4BAE File Offset: 0x000E2DAE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void MoveNext<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			AsyncMethodBuilderCore.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x000E4BB6 File Offset: 0x000E2DB6
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x06004551 RID: 17745 RVA: 0x000E4BC5 File Offset: 0x000E2DC5
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x000E4BD4 File Offset: 0x000E2DD4
		public void Complete()
		{
			this._methodBuilder.SetResult();
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06004553 RID: 17747 RVA: 0x000E4BE1 File Offset: 0x000E2DE1
		internal object ObjectIdForDebugger
		{
			get
			{
				return this._methodBuilder.ObjectIdForDebugger;
			}
		}

		// Token: 0x04002CA3 RID: 11427
		private AsyncTaskMethodBuilder _methodBuilder;
	}
}
