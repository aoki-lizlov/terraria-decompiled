using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x020002DC RID: 732
	internal static class TaskToApm
	{
		// Token: 0x0600213A RID: 8506 RVA: 0x00078838 File Offset: 0x00076A38
		public static IAsyncResult Begin(Task task, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult;
			if (task.IsCompleted)
			{
				asyncResult = new TaskToApm.TaskWrapperAsyncResult(task, state, true);
				if (callback != null)
				{
					callback(asyncResult);
				}
			}
			else
			{
				IAsyncResult asyncResult3;
				if (task.AsyncState != state)
				{
					IAsyncResult asyncResult2 = new TaskToApm.TaskWrapperAsyncResult(task, state, false);
					asyncResult3 = asyncResult2;
				}
				else
				{
					asyncResult3 = task;
				}
				asyncResult = asyncResult3;
				if (callback != null)
				{
					TaskToApm.InvokeCallbackWhenTaskCompletes(task, callback, asyncResult);
				}
			}
			return asyncResult;
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x00078888 File Offset: 0x00076A88
		public static void End(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task task;
			if (taskWrapperAsyncResult != null)
			{
				task = taskWrapperAsyncResult.Task;
			}
			else
			{
				task = asyncResult as Task;
			}
			if (task == null)
			{
				throw new ArgumentNullException();
			}
			task.GetAwaiter().GetResult();
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x000788C8 File Offset: 0x00076AC8
		public static TResult End<TResult>(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task<TResult> task;
			if (taskWrapperAsyncResult != null)
			{
				task = taskWrapperAsyncResult.Task as Task<TResult>;
			}
			else
			{
				task = asyncResult as Task<TResult>;
			}
			if (task == null)
			{
				throw new ArgumentNullException();
			}
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x0007890C File Offset: 0x00076B0C
		private static void InvokeCallbackWhenTaskCompletes(Task antecedent, AsyncCallback callback, IAsyncResult asyncResult)
		{
			antecedent.ConfigureAwait(false).GetAwaiter().OnCompleted(delegate
			{
				callback(asyncResult);
			});
		}

		// Token: 0x020002DD RID: 733
		private sealed class TaskWrapperAsyncResult : IAsyncResult
		{
			// Token: 0x0600213E RID: 8510 RVA: 0x00078950 File Offset: 0x00076B50
			internal TaskWrapperAsyncResult(Task task, object state, bool completedSynchronously)
			{
				this.Task = task;
				this._state = state;
				this._completedSynchronously = completedSynchronously;
			}

			// Token: 0x170003E2 RID: 994
			// (get) Token: 0x0600213F RID: 8511 RVA: 0x0007896D File Offset: 0x00076B6D
			object IAsyncResult.AsyncState
			{
				get
				{
					return this._state;
				}
			}

			// Token: 0x170003E3 RID: 995
			// (get) Token: 0x06002140 RID: 8512 RVA: 0x00078975 File Offset: 0x00076B75
			bool IAsyncResult.CompletedSynchronously
			{
				get
				{
					return this._completedSynchronously;
				}
			}

			// Token: 0x170003E4 RID: 996
			// (get) Token: 0x06002141 RID: 8513 RVA: 0x0007897D File Offset: 0x00076B7D
			bool IAsyncResult.IsCompleted
			{
				get
				{
					return this.Task.IsCompleted;
				}
			}

			// Token: 0x170003E5 RID: 997
			// (get) Token: 0x06002142 RID: 8514 RVA: 0x0007898A File Offset: 0x00076B8A
			WaitHandle IAsyncResult.AsyncWaitHandle
			{
				get
				{
					return ((IAsyncResult)this.Task).AsyncWaitHandle;
				}
			}

			// Token: 0x04001A94 RID: 6804
			internal readonly Task Task;

			// Token: 0x04001A95 RID: 6805
			private readonly object _state;

			// Token: 0x04001A96 RID: 6806
			private readonly bool _completedSynchronously;
		}

		// Token: 0x020002DE RID: 734
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x06002143 RID: 8515 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x06002144 RID: 8516 RVA: 0x00078997 File Offset: 0x00076B97
			internal void <InvokeCallbackWhenTaskCompletes>b__0()
			{
				this.callback(this.asyncResult);
			}

			// Token: 0x04001A97 RID: 6807
			public AsyncCallback callback;

			// Token: 0x04001A98 RID: 6808
			public IAsyncResult asyncResult;
		}
	}
}
