using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007E1 RID: 2017
	public readonly struct TaskAwaiter<TResult> : ICriticalNotifyCompletion, INotifyCompletion, ITaskAwaiter
	{
		// Token: 0x060045D8 RID: 17880 RVA: 0x000E57D7 File Offset: 0x000E39D7
		internal TaskAwaiter(Task<TResult> task)
		{
			this.m_task = task;
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060045D9 RID: 17881 RVA: 0x000E57E0 File Offset: 0x000E39E0
		public bool IsCompleted
		{
			get
			{
				return this.m_task.IsCompleted;
			}
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x000E57ED File Offset: 0x000E39ED
		public void OnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, true);
		}

		// Token: 0x060045DB RID: 17883 RVA: 0x000E57FD File Offset: 0x000E39FD
		public void UnsafeOnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, false);
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x000E580D File Offset: 0x000E3A0D
		[StackTraceHidden]
		public TResult GetResult()
		{
			TaskAwaiter.ValidateEnd(this.m_task);
			return this.m_task.ResultOnSuccess;
		}

		// Token: 0x04002CCD RID: 11469
		private readonly Task<TResult> m_task;
	}
}
