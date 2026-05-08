using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Internal.Runtime.Augments;
using Internal.Threading.Tasks.Tracing;

namespace System.Threading.Tasks
{
	// Token: 0x0200034E RID: 846
	internal sealed class ThreadPoolTaskScheduler : TaskScheduler
	{
		// Token: 0x060024ED RID: 9453 RVA: 0x00084623 File Offset: 0x00082823
		internal ThreadPoolTaskScheduler()
		{
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x0008462C File Offset: 0x0008282C
		protected internal override void QueueTask(Task task)
		{
			if (TaskTrace.Enabled)
			{
				Task internalCurrent = Task.InternalCurrent;
				Task parent = task.m_parent;
				TaskTrace.TaskScheduled(base.Id, (internalCurrent == null) ? 0 : internalCurrent.Id, task.Id, (parent == null) ? 0 : parent.Id, (int)task.Options);
			}
			if ((task.Options & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
			{
				RuntimeThread runtimeThread = RuntimeThread.Create(ThreadPoolTaskScheduler.s_longRunningThreadWork, 0);
				runtimeThread.IsBackground = true;
				runtimeThread.Start(task);
				return;
			}
			bool flag = (task.Options & TaskCreationOptions.PreferFairness) > TaskCreationOptions.None;
			ThreadPool.UnsafeQueueCustomWorkItem(task, flag);
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000846B4 File Offset: 0x000828B4
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			if (taskWasPreviouslyQueued && !ThreadPool.TryPopCustomWorkItem(task))
			{
				return false;
			}
			bool flag = false;
			try
			{
				flag = task.ExecuteEntry(false);
			}
			finally
			{
				if (taskWasPreviouslyQueued)
				{
					this.NotifyWorkItemProgress();
				}
			}
			return flag;
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000846F8 File Offset: 0x000828F8
		protected internal override bool TryDequeue(Task task)
		{
			return ThreadPool.TryPopCustomWorkItem(task);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x00084700 File Offset: 0x00082900
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return this.FilterTasksFromWorkItems(ThreadPool.GetQueuedWorkItems());
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x0008470D File Offset: 0x0008290D
		private IEnumerable<Task> FilterTasksFromWorkItems(IEnumerable<IThreadPoolWorkItem> tpwItems)
		{
			foreach (IThreadPoolWorkItem threadPoolWorkItem in tpwItems)
			{
				if (threadPoolWorkItem is Task)
				{
					yield return (Task)threadPoolWorkItem;
				}
			}
			IEnumerator<IThreadPoolWorkItem> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x0008471D File Offset: 0x0008291D
		internal override void NotifyWorkItemProgress()
		{
			ThreadPool.NotifyWorkItemProgress();
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x0000408A File Offset: 0x0000228A
		internal override bool RequiresAtomicStartTransition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x00084724 File Offset: 0x00082924
		// Note: this type is marked as 'beforefieldinit'.
		static ThreadPoolTaskScheduler()
		{
		}

		// Token: 0x04001C24 RID: 7204
		private static readonly ParameterizedThreadStart s_longRunningThreadWork = delegate(object s)
		{
			((Task)s).ExecuteEntry(false);
		};

		// Token: 0x0200034F RID: 847
		[CompilerGenerated]
		private sealed class <FilterTasksFromWorkItems>d__6 : IEnumerable<Task>, IEnumerable, IEnumerator<Task>, IDisposable, IEnumerator
		{
			// Token: 0x060024F6 RID: 9462 RVA: 0x0008473B File Offset: 0x0008293B
			[DebuggerHidden]
			public <FilterTasksFromWorkItems>d__6(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060024F7 RID: 9463 RVA: 0x00084758 File Offset: 0x00082958
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x060024F8 RID: 9464 RVA: 0x00084790 File Offset: 0x00082990
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
					}
					else
					{
						this.<>1__state = -1;
						enumerator = tpwItems.GetEnumerator();
						this.<>1__state = -3;
					}
					while (enumerator.MoveNext())
					{
						IThreadPoolWorkItem threadPoolWorkItem = enumerator.Current;
						if (threadPoolWorkItem is Task)
						{
							this.<>2__current = (Task)threadPoolWorkItem;
							this.<>1__state = 1;
							return true;
						}
					}
					this.<>m__Finally1();
					enumerator = null;
					flag = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x060024F9 RID: 9465 RVA: 0x0008483C File Offset: 0x00082A3C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x17000481 RID: 1153
			// (get) Token: 0x060024FA RID: 9466 RVA: 0x00084858 File Offset: 0x00082A58
			Task IEnumerator<Task>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060024FB RID: 9467 RVA: 0x00047E00 File Offset: 0x00046000
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000482 RID: 1154
			// (get) Token: 0x060024FC RID: 9468 RVA: 0x00084858 File Offset: 0x00082A58
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060024FD RID: 9469 RVA: 0x00084860 File Offset: 0x00082A60
			[DebuggerHidden]
			IEnumerator<Task> IEnumerable<Task>.GetEnumerator()
			{
				ThreadPoolTaskScheduler.<FilterTasksFromWorkItems>d__6 <FilterTasksFromWorkItems>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<FilterTasksFromWorkItems>d__ = this;
				}
				else
				{
					<FilterTasksFromWorkItems>d__ = new ThreadPoolTaskScheduler.<FilterTasksFromWorkItems>d__6(0);
				}
				<FilterTasksFromWorkItems>d__.tpwItems = tpwItems;
				return <FilterTasksFromWorkItems>d__;
			}

			// Token: 0x060024FE RID: 9470 RVA: 0x000848A3 File Offset: 0x00082AA3
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Threading.Tasks.Task>.GetEnumerator();
			}

			// Token: 0x04001C25 RID: 7205
			private int <>1__state;

			// Token: 0x04001C26 RID: 7206
			private Task <>2__current;

			// Token: 0x04001C27 RID: 7207
			private int <>l__initialThreadId;

			// Token: 0x04001C28 RID: 7208
			private IEnumerable<IThreadPoolWorkItem> tpwItems;

			// Token: 0x04001C29 RID: 7209
			public IEnumerable<IThreadPoolWorkItem> <>3__tpwItems;

			// Token: 0x04001C2A RID: 7210
			private IEnumerator<IThreadPoolWorkItem> <>7__wrap1;
		}

		// Token: 0x02000350 RID: 848
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060024FF RID: 9471 RVA: 0x000848AB File Offset: 0x00082AAB
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002500 RID: 9472 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06002501 RID: 9473 RVA: 0x000848B7 File Offset: 0x00082AB7
			internal void <.cctor>b__10_0(object s)
			{
				((Task)s).ExecuteEntry(false);
			}

			// Token: 0x04001C2B RID: 7211
			public static readonly ThreadPoolTaskScheduler.<>c <>9 = new ThreadPoolTaskScheduler.<>c();
		}
	}
}
