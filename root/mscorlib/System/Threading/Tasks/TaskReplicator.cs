using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000302 RID: 770
	internal class TaskReplicator
	{
		// Token: 0x06002220 RID: 8736 RVA: 0x0007C1C8 File Offset: 0x0007A3C8
		private TaskReplicator(ParallelOptions options, bool stopOnFirstFailure)
		{
			this._scheduler = options.TaskScheduler ?? TaskScheduler.Current;
			this._stopOnFirstFailure = stopOnFirstFailure;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x0007C1F8 File Offset: 0x0007A3F8
		public static void Run<TState>(TaskReplicator.ReplicatableUserAction<TState> action, ParallelOptions options, bool stopOnFirstFailure)
		{
			int num = ((options.EffectiveMaxConcurrencyLevel > 0) ? options.EffectiveMaxConcurrencyLevel : int.MaxValue);
			TaskReplicator taskReplicator = new TaskReplicator(options, stopOnFirstFailure);
			new TaskReplicator.Replica<TState>(taskReplicator, num, 1073741823, action).Start();
			TaskReplicator.Replica replica;
			while (taskReplicator._pendingReplicas.TryDequeue(out replica))
			{
				replica.Wait();
			}
			if (taskReplicator._exceptions != null)
			{
				throw new AggregateException(taskReplicator._exceptions);
			}
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x0007C264 File Offset: 0x0007A464
		private static int GenerateCooperativeMultitaskingTaskTimeout()
		{
			int processorCount = PlatformHelper.ProcessorCount;
			int tickCount = Environment.TickCount;
			return 100 + tickCount % processorCount * 50;
		}

		// Token: 0x04001B1B RID: 6939
		private readonly TaskScheduler _scheduler;

		// Token: 0x04001B1C RID: 6940
		private readonly bool _stopOnFirstFailure;

		// Token: 0x04001B1D RID: 6941
		private readonly ConcurrentQueue<TaskReplicator.Replica> _pendingReplicas = new ConcurrentQueue<TaskReplicator.Replica>();

		// Token: 0x04001B1E RID: 6942
		private ConcurrentQueue<Exception> _exceptions;

		// Token: 0x04001B1F RID: 6943
		private bool _stopReplicating;

		// Token: 0x04001B20 RID: 6944
		private const int CooperativeMultitaskingTaskTimeout_Min = 100;

		// Token: 0x04001B21 RID: 6945
		private const int CooperativeMultitaskingTaskTimeout_Increment = 50;

		// Token: 0x04001B22 RID: 6946
		private const int CooperativeMultitaskingTaskTimeout_RootTask = 1073741823;

		// Token: 0x02000303 RID: 771
		// (Invoke) Token: 0x06002224 RID: 8740
		public delegate void ReplicatableUserAction<TState>(ref TState replicaState, int timeout, out bool yieldedBeforeCompletion);

		// Token: 0x02000304 RID: 772
		private abstract class Replica
		{
			// Token: 0x06002227 RID: 8743 RVA: 0x0007C288 File Offset: 0x0007A488
			protected Replica(TaskReplicator replicator, int maxConcurrency, int timeout)
			{
				this._replicator = replicator;
				this._timeout = timeout;
				this._remainingConcurrency = maxConcurrency - 1;
				this._pendingTask = new Task(delegate(object s)
				{
					((TaskReplicator.Replica)s).Execute();
				}, this);
				this._replicator._pendingReplicas.Enqueue(this);
			}

			// Token: 0x06002228 RID: 8744 RVA: 0x0007C2F0 File Offset: 0x0007A4F0
			public void Start()
			{
				this._pendingTask.RunSynchronously(this._replicator._scheduler);
			}

			// Token: 0x06002229 RID: 8745 RVA: 0x0007C30C File Offset: 0x0007A50C
			public void Wait()
			{
				Task pendingTask;
				while ((pendingTask = this._pendingTask) != null)
				{
					pendingTask.Wait();
				}
			}

			// Token: 0x0600222A RID: 8746 RVA: 0x0007C330 File Offset: 0x0007A530
			public void Execute()
			{
				try
				{
					if (!this._replicator._stopReplicating && this._remainingConcurrency > 0)
					{
						this.CreateNewReplica();
						this._remainingConcurrency = 0;
					}
					bool flag;
					this.ExecuteAction(out flag);
					if (flag)
					{
						this._pendingTask = new Task(delegate(object s)
						{
							((TaskReplicator.Replica)s).Execute();
						}, this, CancellationToken.None, TaskCreationOptions.None);
						this._pendingTask.Start(this._replicator._scheduler);
					}
					else
					{
						this._replicator._stopReplicating = true;
						this._pendingTask = null;
					}
				}
				catch (Exception ex)
				{
					LazyInitializer.EnsureInitialized<ConcurrentQueue<Exception>>(ref this._replicator._exceptions).Enqueue(ex);
					if (this._replicator._stopOnFirstFailure)
					{
						this._replicator._stopReplicating = true;
					}
					this._pendingTask = null;
				}
			}

			// Token: 0x0600222B RID: 8747
			protected abstract void CreateNewReplica();

			// Token: 0x0600222C RID: 8748
			protected abstract void ExecuteAction(out bool yieldedBeforeCompletion);

			// Token: 0x04001B23 RID: 6947
			protected readonly TaskReplicator _replicator;

			// Token: 0x04001B24 RID: 6948
			protected readonly int _timeout;

			// Token: 0x04001B25 RID: 6949
			protected int _remainingConcurrency;

			// Token: 0x04001B26 RID: 6950
			protected volatile Task _pendingTask;

			// Token: 0x02000305 RID: 773
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x0600222D RID: 8749 RVA: 0x0007C41C File Offset: 0x0007A61C
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x0600222E RID: 8750 RVA: 0x000025BE File Offset: 0x000007BE
				public <>c()
				{
				}

				// Token: 0x0600222F RID: 8751 RVA: 0x0007C428 File Offset: 0x0007A628
				internal void <.ctor>b__4_0(object s)
				{
					((TaskReplicator.Replica)s).Execute();
				}

				// Token: 0x06002230 RID: 8752 RVA: 0x0007C428 File Offset: 0x0007A628
				internal void <Execute>b__7_0(object s)
				{
					((TaskReplicator.Replica)s).Execute();
				}

				// Token: 0x04001B27 RID: 6951
				public static readonly TaskReplicator.Replica.<>c <>9 = new TaskReplicator.Replica.<>c();

				// Token: 0x04001B28 RID: 6952
				public static Action<object> <>9__4_0;

				// Token: 0x04001B29 RID: 6953
				public static Action<object> <>9__7_0;
			}
		}

		// Token: 0x02000306 RID: 774
		private sealed class Replica<TState> : TaskReplicator.Replica
		{
			// Token: 0x06002231 RID: 8753 RVA: 0x0007C435 File Offset: 0x0007A635
			public Replica(TaskReplicator replicator, int maxConcurrency, int timeout, TaskReplicator.ReplicatableUserAction<TState> action)
				: base(replicator, maxConcurrency, timeout)
			{
				this._action = action;
			}

			// Token: 0x06002232 RID: 8754 RVA: 0x0007C448 File Offset: 0x0007A648
			protected override void CreateNewReplica()
			{
				new TaskReplicator.Replica<TState>(this._replicator, this._remainingConcurrency, TaskReplicator.GenerateCooperativeMultitaskingTaskTimeout(), this._action)._pendingTask.Start(this._replicator._scheduler);
			}

			// Token: 0x06002233 RID: 8755 RVA: 0x0007C47D File Offset: 0x0007A67D
			protected override void ExecuteAction(out bool yieldedBeforeCompletion)
			{
				this._action(ref this._state, this._timeout, out yieldedBeforeCompletion);
			}

			// Token: 0x04001B2A RID: 6954
			private readonly TaskReplicator.ReplicatableUserAction<TState> _action;

			// Token: 0x04001B2B RID: 6955
			private TState _state;
		}
	}
}
