using System;
using Internal.Runtime.Augments;

namespace Internal.Threading.Tasks.Tracing
{
	// Token: 0x02000B57 RID: 2903
	internal static class TaskTrace
	{
		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06006A98 RID: 27288 RVA: 0x0016EBCC File Offset: 0x0016CDCC
		public static bool Enabled
		{
			get
			{
				TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
				return taskTraceCallbacks != null && taskTraceCallbacks.Enabled;
			}
		}

		// Token: 0x06006A99 RID: 27289 RVA: 0x0016EBEF File Offset: 0x0016CDEF
		public static void Initialize(TaskTraceCallbacks callbacks)
		{
			TaskTrace.s_callbacks = callbacks;
		}

		// Token: 0x06006A9A RID: 27290 RVA: 0x0016EBF8 File Offset: 0x0016CDF8
		public static void TaskWaitBegin_Asynchronous(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
		{
			TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
			if (taskTraceCallbacks == null)
			{
				return;
			}
			taskTraceCallbacks.TaskWaitBegin_Asynchronous(OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
		}

		// Token: 0x06006A9B RID: 27291 RVA: 0x0016EC18 File Offset: 0x0016CE18
		public static void TaskWaitBegin_Synchronous(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
		{
			TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
			if (taskTraceCallbacks == null)
			{
				return;
			}
			taskTraceCallbacks.TaskWaitBegin_Synchronous(OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
		}

		// Token: 0x06006A9C RID: 27292 RVA: 0x0016EC38 File Offset: 0x0016CE38
		public static void TaskWaitEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
		{
			TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
			if (taskTraceCallbacks == null)
			{
				return;
			}
			taskTraceCallbacks.TaskWaitEnd(OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
		}

		// Token: 0x06006A9D RID: 27293 RVA: 0x0016EC58 File Offset: 0x0016CE58
		public static void TaskScheduled(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, int CreatingTaskID, int TaskCreationOptions)
		{
			TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
			if (taskTraceCallbacks == null)
			{
				return;
			}
			taskTraceCallbacks.TaskScheduled(OriginatingTaskSchedulerID, OriginatingTaskID, TaskID, CreatingTaskID, TaskCreationOptions);
		}

		// Token: 0x06006A9E RID: 27294 RVA: 0x0016EC7C File Offset: 0x0016CE7C
		public static void TaskStarted(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
		{
			TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
			if (taskTraceCallbacks == null)
			{
				return;
			}
			taskTraceCallbacks.TaskStarted(OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
		}

		// Token: 0x06006A9F RID: 27295 RVA: 0x0016EC9C File Offset: 0x0016CE9C
		public static void TaskCompleted(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, bool IsExceptional)
		{
			TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
			if (taskTraceCallbacks == null)
			{
				return;
			}
			taskTraceCallbacks.TaskCompleted(OriginatingTaskSchedulerID, OriginatingTaskID, TaskID, IsExceptional);
		}

		// Token: 0x04003D57 RID: 15703
		private static TaskTraceCallbacks s_callbacks;
	}
}
