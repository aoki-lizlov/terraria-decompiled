using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200031B RID: 795
	internal static class GenericDelegateCache<TAntecedentResult, TResult>
	{
		// Token: 0x06002309 RID: 8969 RVA: 0x0007E968 File Offset: 0x0007CB68
		// Note: this type is marked as 'beforefieldinit'.
		static GenericDelegateCache()
		{
		}

		// Token: 0x04001B71 RID: 7025
		internal static Func<Task<Task>, object, TResult> CWAnyFuncDelegate = delegate(Task<Task> wrappedWinner, object state)
		{
			Func<Task<TAntecedentResult>, TResult> func = (Func<Task<TAntecedentResult>, TResult>)state;
			Task<TAntecedentResult> task = (Task<TAntecedentResult>)wrappedWinner.Result;
			return func(task);
		};

		// Token: 0x04001B72 RID: 7026
		internal static Func<Task<Task>, object, TResult> CWAnyActionDelegate = delegate(Task<Task> wrappedWinner, object state)
		{
			Action<Task<TAntecedentResult>> action = (Action<Task<TAntecedentResult>>)state;
			Task<TAntecedentResult> task2 = (Task<TAntecedentResult>)wrappedWinner.Result;
			action(task2);
			return default(TResult);
		};

		// Token: 0x04001B73 RID: 7027
		internal static Func<Task<Task<TAntecedentResult>[]>, object, TResult> CWAllFuncDelegate = delegate(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
		{
			wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
			return ((Func<Task<TAntecedentResult>[], TResult>)state)(wrappedAntecedents.Result);
		};

		// Token: 0x04001B74 RID: 7028
		internal static Func<Task<Task<TAntecedentResult>[]>, object, TResult> CWAllActionDelegate = delegate(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
		{
			wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
			((Action<Task<TAntecedentResult>[]>)state)(wrappedAntecedents.Result);
			return default(TResult);
		};

		// Token: 0x0200031C RID: 796
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600230A RID: 8970 RVA: 0x0007E9C9 File Offset: 0x0007CBC9
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600230B RID: 8971 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x0600230C RID: 8972 RVA: 0x0007E9D8 File Offset: 0x0007CBD8
			internal TResult <.cctor>b__4_0(Task<Task> wrappedWinner, object state)
			{
				Func<Task<TAntecedentResult>, TResult> func = (Func<Task<TAntecedentResult>, TResult>)state;
				Task<TAntecedentResult> task = (Task<TAntecedentResult>)wrappedWinner.Result;
				return func(task);
			}

			// Token: 0x0600230D RID: 8973 RVA: 0x0007EA00 File Offset: 0x0007CC00
			internal TResult <.cctor>b__4_1(Task<Task> wrappedWinner, object state)
			{
				Action<Task<TAntecedentResult>> action = (Action<Task<TAntecedentResult>>)state;
				Task<TAntecedentResult> task = (Task<TAntecedentResult>)wrappedWinner.Result;
				action(task);
				return default(TResult);
			}

			// Token: 0x0600230E RID: 8974 RVA: 0x0007EA2E File Offset: 0x0007CC2E
			internal TResult <.cctor>b__4_2(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
			{
				wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
				return ((Func<Task<TAntecedentResult>[], TResult>)state)(wrappedAntecedents.Result);
			}

			// Token: 0x0600230F RID: 8975 RVA: 0x0007EA48 File Offset: 0x0007CC48
			internal TResult <.cctor>b__4_3(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
			{
				wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
				((Action<Task<TAntecedentResult>[]>)state)(wrappedAntecedents.Result);
				return default(TResult);
			}

			// Token: 0x04001B75 RID: 7029
			public static readonly GenericDelegateCache<TAntecedentResult, TResult>.<>c <>9 = new GenericDelegateCache<TAntecedentResult, TResult>.<>c();
		}
	}
}
