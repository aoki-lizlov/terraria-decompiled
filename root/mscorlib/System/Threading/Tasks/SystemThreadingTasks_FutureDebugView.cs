using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000312 RID: 786
	internal class SystemThreadingTasks_FutureDebugView<TResult>
	{
		// Token: 0x060022AE RID: 8878 RVA: 0x0007D68C File Offset: 0x0007B88C
		public SystemThreadingTasks_FutureDebugView(Task<TResult> task)
		{
			this.m_task = task;
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x0007D69C File Offset: 0x0007B89C
		public TResult Result
		{
			get
			{
				if (this.m_task.Status != TaskStatus.RanToCompletion)
				{
					return default(TResult);
				}
				return this.m_task.Result;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x0007D6CC File Offset: 0x0007B8CC
		public object AsyncState
		{
			get
			{
				return this.m_task.AsyncState;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x0007D6D9 File Offset: 0x0007B8D9
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_task.CreationOptions;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x0007D6E6 File Offset: 0x0007B8E6
		public Exception Exception
		{
			get
			{
				return this.m_task.Exception;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x0007D6F3 File Offset: 0x0007B8F3
		public int Id
		{
			get
			{
				return this.m_task.Id;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x0007D700 File Offset: 0x0007B900
		public bool CancellationPending
		{
			get
			{
				return this.m_task.Status == TaskStatus.WaitingToRun && this.m_task.CancellationToken.IsCancellationRequested;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x0007D730 File Offset: 0x0007B930
		public TaskStatus Status
		{
			get
			{
				return this.m_task.Status;
			}
		}

		// Token: 0x04001B52 RID: 6994
		private Task<TResult> m_task;
	}
}
