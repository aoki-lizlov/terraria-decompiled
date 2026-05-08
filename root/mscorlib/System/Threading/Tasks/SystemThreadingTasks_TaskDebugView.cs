using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000330 RID: 816
	internal class SystemThreadingTasks_TaskDebugView
	{
		// Token: 0x06002414 RID: 9236 RVA: 0x0008263B File Offset: 0x0008083B
		public SystemThreadingTasks_TaskDebugView(Task task)
		{
			this.m_task = task;
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06002415 RID: 9237 RVA: 0x0008264A File Offset: 0x0008084A
		public object AsyncState
		{
			get
			{
				return this.m_task.AsyncState;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06002416 RID: 9238 RVA: 0x00082657 File Offset: 0x00080857
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_task.CreationOptions;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06002417 RID: 9239 RVA: 0x00082664 File Offset: 0x00080864
		public Exception Exception
		{
			get
			{
				return this.m_task.Exception;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x00082671 File Offset: 0x00080871
		public int Id
		{
			get
			{
				return this.m_task.Id;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x00082680 File Offset: 0x00080880
		public bool CancellationPending
		{
			get
			{
				return this.m_task.Status == TaskStatus.WaitingToRun && this.m_task.CancellationToken.IsCancellationRequested;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x000826B0 File Offset: 0x000808B0
		public TaskStatus Status
		{
			get
			{
				return this.m_task.Status;
			}
		}

		// Token: 0x04001BCF RID: 7119
		private Task m_task;
	}
}
