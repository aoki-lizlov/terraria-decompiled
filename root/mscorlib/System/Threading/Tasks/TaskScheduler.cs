using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000349 RID: 841
	[DebuggerDisplay("Id={Id}")]
	[DebuggerTypeProxy(typeof(TaskScheduler.SystemThreadingTasks_TaskSchedulerDebugView))]
	public abstract class TaskScheduler
	{
		// Token: 0x060024C7 RID: 9415
		protected internal abstract void QueueTask(Task task);

		// Token: 0x060024C8 RID: 9416
		protected abstract bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued);

		// Token: 0x060024C9 RID: 9417
		protected abstract IEnumerable<Task> GetScheduledTasks();

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x000841D2 File Offset: 0x000823D2
		public virtual int MaximumConcurrencyLevel
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000841DC File Offset: 0x000823DC
		internal bool TryRunInline(Task task, bool taskWasPreviouslyQueued)
		{
			TaskScheduler executingTaskScheduler = task.ExecutingTaskScheduler;
			if (executingTaskScheduler != this && executingTaskScheduler != null)
			{
				return executingTaskScheduler.TryRunInline(task, taskWasPreviouslyQueued);
			}
			StackGuard currentStackGuard;
			if (executingTaskScheduler == null || task.m_action == null || task.IsDelegateInvoked || task.IsCanceled || !(currentStackGuard = Task.CurrentStackGuard).TryBeginInliningScope())
			{
				return false;
			}
			bool flag = false;
			try
			{
				flag = this.TryExecuteTaskInline(task, taskWasPreviouslyQueued);
			}
			finally
			{
				currentStackGuard.EndInliningScope();
			}
			if (flag && !task.IsDelegateInvoked && !task.IsCanceled)
			{
				throw new InvalidOperationException("The TryExecuteTaskInline call to the underlying scheduler succeeded, but the task body was not invoked.");
			}
			return flag;
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x0000408A File Offset: 0x0000228A
		protected internal virtual bool TryDequeue(Task task)
		{
			return false;
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x00004088 File Offset: 0x00002288
		internal virtual void NotifyWorkItemProgress()
		{
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x00003FB7 File Offset: 0x000021B7
		internal virtual bool RequiresAtomicStartTransition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000025BE File Offset: 0x000007BE
		protected TaskScheduler()
		{
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x00084270 File Offset: 0x00082470
		private void AddToActiveTaskSchedulers()
		{
			ConditionalWeakTable<TaskScheduler, object> conditionalWeakTable = TaskScheduler.s_activeTaskSchedulers;
			if (conditionalWeakTable == null)
			{
				Interlocked.CompareExchange<ConditionalWeakTable<TaskScheduler, object>>(ref TaskScheduler.s_activeTaskSchedulers, new ConditionalWeakTable<TaskScheduler, object>(), null);
				conditionalWeakTable = TaskScheduler.s_activeTaskSchedulers;
			}
			conditionalWeakTable.Add(this, null);
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x000842A5 File Offset: 0x000824A5
		public static TaskScheduler Default
		{
			get
			{
				return TaskScheduler.s_defaultTaskScheduler;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x000842AC File Offset: 0x000824AC
		public static TaskScheduler Current
		{
			get
			{
				return TaskScheduler.InternalCurrent ?? TaskScheduler.Default;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060024D3 RID: 9427 RVA: 0x000842BC File Offset: 0x000824BC
		internal static TaskScheduler InternalCurrent
		{
			get
			{
				Task internalCurrent = Task.InternalCurrent;
				if (internalCurrent == null || (internalCurrent.CreationOptions & TaskCreationOptions.HideScheduler) != TaskCreationOptions.None)
				{
					return null;
				}
				return internalCurrent.ExecutingTaskScheduler;
			}
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000842E5 File Offset: 0x000824E5
		public static TaskScheduler FromCurrentSynchronizationContext()
		{
			return new SynchronizationContextTaskScheduler();
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060024D5 RID: 9429 RVA: 0x000842EC File Offset: 0x000824EC
		public int Id
		{
			get
			{
				if (this.m_taskSchedulerId == 0)
				{
					int num;
					do
					{
						num = Interlocked.Increment(ref TaskScheduler.s_taskSchedulerIdCounter);
					}
					while (num == 0);
					Interlocked.CompareExchange(ref this.m_taskSchedulerId, num, 0);
				}
				return this.m_taskSchedulerId;
			}
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x00084329 File Offset: 0x00082529
		protected bool TryExecuteTask(Task task)
		{
			if (task.ExecutingTaskScheduler != this)
			{
				throw new InvalidOperationException("ExecuteTask may not be called for a task which was previously queued to a different TaskScheduler.");
			}
			return task.ExecuteEntry(true);
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060024D7 RID: 9431 RVA: 0x00084348 File Offset: 0x00082548
		// (remove) Token: 0x060024D8 RID: 9432 RVA: 0x00084398 File Offset: 0x00082598
		public static event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException
		{
			add
			{
				if (value != null)
				{
					using (LockHolder.Hold(TaskScheduler._unobservedTaskExceptionLockObject))
					{
						TaskScheduler._unobservedTaskException = (EventHandler<UnobservedTaskExceptionEventArgs>)Delegate.Combine(TaskScheduler._unobservedTaskException, value);
					}
				}
			}
			remove
			{
				using (LockHolder.Hold(TaskScheduler._unobservedTaskExceptionLockObject))
				{
					TaskScheduler._unobservedTaskException = (EventHandler<UnobservedTaskExceptionEventArgs>)Delegate.Remove(TaskScheduler._unobservedTaskException, value);
				}
			}
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000843E8 File Offset: 0x000825E8
		internal static void PublishUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs ueea)
		{
			using (LockHolder.Hold(TaskScheduler._unobservedTaskExceptionLockObject))
			{
				EventHandler<UnobservedTaskExceptionEventArgs> unobservedTaskException = TaskScheduler._unobservedTaskException;
				if (unobservedTaskException != null)
				{
					unobservedTaskException(sender, ueea);
				}
			}
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x00084434 File Offset: 0x00082634
		internal Task[] GetScheduledTasksForDebugger()
		{
			IEnumerable<Task> scheduledTasks = this.GetScheduledTasks();
			if (scheduledTasks == null)
			{
				return null;
			}
			Task[] array = scheduledTasks as Task[];
			if (array == null)
			{
				array = new LowLevelList<Task>(scheduledTasks).ToArray();
			}
			Task[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				int id = array2[i].Id;
			}
			return array;
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x00084480 File Offset: 0x00082680
		internal static TaskScheduler[] GetTaskSchedulersForDebugger()
		{
			if (TaskScheduler.s_activeTaskSchedulers == null)
			{
				return new TaskScheduler[] { TaskScheduler.s_defaultTaskScheduler };
			}
			LowLevelList<TaskScheduler> lowLevelList = new LowLevelList<TaskScheduler>();
			foreach (KeyValuePair<TaskScheduler, object> keyValuePair in ((IEnumerable<KeyValuePair<TaskScheduler, object>>)TaskScheduler.s_activeTaskSchedulers))
			{
				lowLevelList.Add(keyValuePair.Key);
			}
			if (!lowLevelList.Contains(TaskScheduler.s_defaultTaskScheduler))
			{
				lowLevelList.Add(TaskScheduler.s_defaultTaskScheduler);
			}
			TaskScheduler[] array = lowLevelList.ToArray();
			TaskScheduler[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				int id = array2[i].Id;
			}
			return array;
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x00084530 File Offset: 0x00082730
		// Note: this type is marked as 'beforefieldinit'.
		static TaskScheduler()
		{
		}

		// Token: 0x04001C18 RID: 7192
		private static ConditionalWeakTable<TaskScheduler, object> s_activeTaskSchedulers;

		// Token: 0x04001C19 RID: 7193
		private static readonly TaskScheduler s_defaultTaskScheduler = new ThreadPoolTaskScheduler();

		// Token: 0x04001C1A RID: 7194
		internal static int s_taskSchedulerIdCounter;

		// Token: 0x04001C1B RID: 7195
		private volatile int m_taskSchedulerId;

		// Token: 0x04001C1C RID: 7196
		private static EventHandler<UnobservedTaskExceptionEventArgs> _unobservedTaskException;

		// Token: 0x04001C1D RID: 7197
		private static readonly Lock _unobservedTaskExceptionLockObject = new Lock();

		// Token: 0x0200034A RID: 842
		internal sealed class SystemThreadingTasks_TaskSchedulerDebugView
		{
			// Token: 0x060024DD RID: 9437 RVA: 0x00084546 File Offset: 0x00082746
			public SystemThreadingTasks_TaskSchedulerDebugView(TaskScheduler scheduler)
			{
				this.m_taskScheduler = scheduler;
			}

			// Token: 0x1700047B RID: 1147
			// (get) Token: 0x060024DE RID: 9438 RVA: 0x00084555 File Offset: 0x00082755
			public int Id
			{
				get
				{
					return this.m_taskScheduler.Id;
				}
			}

			// Token: 0x1700047C RID: 1148
			// (get) Token: 0x060024DF RID: 9439 RVA: 0x00084562 File Offset: 0x00082762
			public IEnumerable<Task> ScheduledTasks
			{
				get
				{
					return this.m_taskScheduler.GetScheduledTasks();
				}
			}

			// Token: 0x04001C1E RID: 7198
			private readonly TaskScheduler m_taskScheduler;
		}
	}
}
