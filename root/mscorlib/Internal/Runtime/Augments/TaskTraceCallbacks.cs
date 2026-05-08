using System;

namespace Internal.Runtime.Augments
{
	// Token: 0x02000B5E RID: 2910
	internal abstract class TaskTraceCallbacks
	{
		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06006AA1 RID: 27297
		public abstract bool Enabled { get; }

		// Token: 0x06006AA2 RID: 27298
		public abstract void TaskWaitBegin_Asynchronous(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID);

		// Token: 0x06006AA3 RID: 27299
		public abstract void TaskWaitBegin_Synchronous(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID);

		// Token: 0x06006AA4 RID: 27300
		public abstract void TaskWaitEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID);

		// Token: 0x06006AA5 RID: 27301
		public abstract void TaskScheduled(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, int CreatingTaskID, int TaskCreationOptions);

		// Token: 0x06006AA6 RID: 27302
		public abstract void TaskStarted(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID);

		// Token: 0x06006AA7 RID: 27303
		public abstract void TaskCompleted(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, bool IsExceptional);

		// Token: 0x06006AA8 RID: 27304 RVA: 0x000025BE File Offset: 0x000007BE
		protected TaskTraceCallbacks()
		{
		}
	}
}
