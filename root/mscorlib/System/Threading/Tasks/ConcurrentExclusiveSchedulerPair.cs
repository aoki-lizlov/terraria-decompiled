using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000307 RID: 775
	[DebuggerDisplay("Concurrent={ConcurrentTaskCountForDebugger}, Exclusive={ExclusiveTaskCountForDebugger}, Mode={ModeForDebugger}")]
	[DebuggerTypeProxy(typeof(ConcurrentExclusiveSchedulerPair.DebugView))]
	public class ConcurrentExclusiveSchedulerPair
	{
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x0007C497 File Offset: 0x0007A697
		private static int DefaultMaxConcurrencyLevel
		{
			get
			{
				return Environment.ProcessorCount;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x0007C49E File Offset: 0x0007A69E
		private object ValueLock
		{
			get
			{
				return this.m_threadProcessingMode;
			}
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x0007C4A6 File Offset: 0x0007A6A6
		public ConcurrentExclusiveSchedulerPair()
			: this(TaskScheduler.Default, ConcurrentExclusiveSchedulerPair.DefaultMaxConcurrencyLevel, -1)
		{
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x0007C4B9 File Offset: 0x0007A6B9
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler)
			: this(taskScheduler, ConcurrentExclusiveSchedulerPair.DefaultMaxConcurrencyLevel, -1)
		{
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x0007C4C8 File Offset: 0x0007A6C8
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler, int maxConcurrencyLevel)
			: this(taskScheduler, maxConcurrencyLevel, -1)
		{
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x0007C4D4 File Offset: 0x0007A6D4
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler, int maxConcurrencyLevel, int maxItemsPerTask)
		{
			if (taskScheduler == null)
			{
				throw new ArgumentNullException("taskScheduler");
			}
			if (maxConcurrencyLevel == 0 || maxConcurrencyLevel < -1)
			{
				throw new ArgumentOutOfRangeException("maxConcurrencyLevel");
			}
			if (maxItemsPerTask == 0 || maxItemsPerTask < -1)
			{
				throw new ArgumentOutOfRangeException("maxItemsPerTask");
			}
			this.m_underlyingTaskScheduler = taskScheduler;
			this.m_maxConcurrencyLevel = maxConcurrencyLevel;
			this.m_maxItemsPerTask = maxItemsPerTask;
			int maximumConcurrencyLevel = taskScheduler.MaximumConcurrencyLevel;
			if (maximumConcurrencyLevel > 0 && maximumConcurrencyLevel < this.m_maxConcurrencyLevel)
			{
				this.m_maxConcurrencyLevel = maximumConcurrencyLevel;
			}
			if (this.m_maxConcurrencyLevel == -1)
			{
				this.m_maxConcurrencyLevel = int.MaxValue;
			}
			if (this.m_maxItemsPerTask == -1)
			{
				this.m_maxItemsPerTask = int.MaxValue;
			}
			this.m_exclusiveTaskScheduler = new ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler(this, 1, ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask);
			this.m_concurrentTaskScheduler = new ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler(this, this.m_maxConcurrencyLevel, ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks);
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x0007C5A0 File Offset: 0x0007A7A0
		public void Complete()
		{
			object valueLock = this.ValueLock;
			lock (valueLock)
			{
				if (!this.CompletionRequested)
				{
					this.RequestCompletion();
					this.CleanupStateIfCompletingAndQuiesced();
				}
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x0007C5F0 File Offset: 0x0007A7F0
		public Task Completion
		{
			get
			{
				return this.EnsureCompletionStateInitialized().Task;
			}
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x0007C5FD File Offset: 0x0007A7FD
		private ConcurrentExclusiveSchedulerPair.CompletionState EnsureCompletionStateInitialized()
		{
			return LazyInitializer.EnsureInitialized<ConcurrentExclusiveSchedulerPair.CompletionState>(ref this.m_completionState, () => new ConcurrentExclusiveSchedulerPair.CompletionState());
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x0007C629 File Offset: 0x0007A829
		private bool CompletionRequested
		{
			get
			{
				return this.m_completionState != null && Volatile.Read(ref this.m_completionState.m_completionRequested);
			}
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x0007C645 File Offset: 0x0007A845
		private void RequestCompletion()
		{
			this.EnsureCompletionStateInitialized().m_completionRequested = true;
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x0007C653 File Offset: 0x0007A853
		private void CleanupStateIfCompletingAndQuiesced()
		{
			if (this.ReadyToComplete)
			{
				this.CompleteTaskAsync();
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06002240 RID: 8768 RVA: 0x0007C664 File Offset: 0x0007A864
		private bool ReadyToComplete
		{
			get
			{
				if (!this.CompletionRequested || this.m_processingCount != 0)
				{
					return false;
				}
				ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
				return (completionState.m_exceptions != null && completionState.m_exceptions.Count > 0) || (this.m_concurrentTaskScheduler.m_tasks.IsEmpty && this.m_exclusiveTaskScheduler.m_tasks.IsEmpty);
			}
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x0007C6C8 File Offset: 0x0007A8C8
		private void CompleteTaskAsync()
		{
			ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
			if (!completionState.m_completionQueued)
			{
				completionState.m_completionQueued = true;
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					ConcurrentExclusiveSchedulerPair concurrentExclusiveSchedulerPair = (ConcurrentExclusiveSchedulerPair)state;
					List<Exception> exceptions = concurrentExclusiveSchedulerPair.m_completionState.m_exceptions;
					if (exceptions == null || exceptions.Count <= 0)
					{
						concurrentExclusiveSchedulerPair.m_completionState.TrySetResult(default(VoidTaskResult));
					}
					else
					{
						concurrentExclusiveSchedulerPair.m_completionState.TrySetException(exceptions);
					}
					concurrentExclusiveSchedulerPair.m_threadProcessingMode.Dispose();
				}, this);
			}
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x0007C714 File Offset: 0x0007A914
		private void FaultWithTask(Task faultedTask)
		{
			ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
			if (completionState.m_exceptions == null)
			{
				completionState.m_exceptions = new List<Exception>();
			}
			completionState.m_exceptions.AddRange(faultedTask.Exception.InnerExceptions);
			this.RequestCompletion();
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x0007C757 File Offset: 0x0007A957
		public TaskScheduler ConcurrentScheduler
		{
			get
			{
				return this.m_concurrentTaskScheduler;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06002244 RID: 8772 RVA: 0x0007C75F File Offset: 0x0007A95F
		public TaskScheduler ExclusiveScheduler
		{
			get
			{
				return this.m_exclusiveTaskScheduler;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x0007C767 File Offset: 0x0007A967
		private int ConcurrentTaskCountForDebugger
		{
			get
			{
				return this.m_concurrentTaskScheduler.m_tasks.Count;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x0007C779 File Offset: 0x0007A979
		private int ExclusiveTaskCountForDebugger
		{
			get
			{
				return this.m_exclusiveTaskScheduler.m_tasks.Count;
			}
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x0007C78C File Offset: 0x0007A98C
		private void ProcessAsyncIfNecessary(bool fairly = false)
		{
			if (this.m_processingCount >= 0)
			{
				bool flag = !this.m_exclusiveTaskScheduler.m_tasks.IsEmpty;
				Task task = null;
				if (this.m_processingCount == 0 && flag)
				{
					this.m_processingCount = -1;
					try
					{
						task = new Task(delegate(object thisPair)
						{
							((ConcurrentExclusiveSchedulerPair)thisPair).ProcessExclusiveTasks();
						}, this, default(CancellationToken), ConcurrentExclusiveSchedulerPair.GetCreationOptionsForTask(fairly));
						task.Start(this.m_underlyingTaskScheduler);
						goto IL_0149;
					}
					catch
					{
						this.m_processingCount = 0;
						this.FaultWithTask(task);
						goto IL_0149;
					}
				}
				int count = this.m_concurrentTaskScheduler.m_tasks.Count;
				if (count > 0 && !flag && this.m_processingCount < this.m_maxConcurrencyLevel)
				{
					int num = 0;
					while (num < count && this.m_processingCount < this.m_maxConcurrencyLevel)
					{
						this.m_processingCount++;
						try
						{
							task = new Task(delegate(object thisPair)
							{
								((ConcurrentExclusiveSchedulerPair)thisPair).ProcessConcurrentTasks();
							}, this, default(CancellationToken), ConcurrentExclusiveSchedulerPair.GetCreationOptionsForTask(fairly));
							task.Start(this.m_underlyingTaskScheduler);
						}
						catch
						{
							this.m_processingCount--;
							this.FaultWithTask(task);
						}
						num++;
					}
				}
				IL_0149:
				this.CleanupStateIfCompletingAndQuiesced();
			}
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x0007C904 File Offset: 0x0007AB04
		private void ProcessExclusiveTasks()
		{
			try
			{
				this.m_threadProcessingMode.Value = ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask;
				for (int i = 0; i < this.m_maxItemsPerTask; i++)
				{
					Task task;
					if (!this.m_exclusiveTaskScheduler.m_tasks.TryDequeue(out task))
					{
						break;
					}
					if (!task.IsFaulted)
					{
						this.m_exclusiveTaskScheduler.ExecuteTask(task);
					}
				}
			}
			finally
			{
				this.m_threadProcessingMode.Value = ConcurrentExclusiveSchedulerPair.ProcessingMode.NotCurrentlyProcessing;
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					this.m_processingCount = 0;
					this.ProcessAsyncIfNecessary(true);
				}
			}
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x0007C9B0 File Offset: 0x0007ABB0
		private void ProcessConcurrentTasks()
		{
			try
			{
				this.m_threadProcessingMode.Value = ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks;
				for (int i = 0; i < this.m_maxItemsPerTask; i++)
				{
					Task task;
					if (!this.m_concurrentTaskScheduler.m_tasks.TryDequeue(out task))
					{
						break;
					}
					if (!task.IsFaulted)
					{
						this.m_concurrentTaskScheduler.ExecuteTask(task);
					}
					if (!this.m_exclusiveTaskScheduler.m_tasks.IsEmpty)
					{
						break;
					}
				}
			}
			finally
			{
				this.m_threadProcessingMode.Value = ConcurrentExclusiveSchedulerPair.ProcessingMode.NotCurrentlyProcessing;
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					if (this.m_processingCount > 0)
					{
						this.m_processingCount--;
					}
					this.ProcessAsyncIfNecessary(true);
				}
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x0007CA80 File Offset: 0x0007AC80
		private ConcurrentExclusiveSchedulerPair.ProcessingMode ModeForDebugger
		{
			get
			{
				if (this.m_completionState != null && this.m_completionState.Task.IsCompleted)
				{
					return ConcurrentExclusiveSchedulerPair.ProcessingMode.Completed;
				}
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode = ConcurrentExclusiveSchedulerPair.ProcessingMode.NotCurrentlyProcessing;
				if (this.m_processingCount == -1)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask;
				}
				if (this.m_processingCount >= 1)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks;
				}
				if (this.CompletionRequested)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.Completing;
				}
				return processingMode;
			}
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("DEBUG")]
		private static void ContractAssertMonitorStatus(object syncObj, bool held)
		{
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x0007CAD4 File Offset: 0x0007ACD4
		internal static TaskCreationOptions GetCreationOptionsForTask(bool isReplacementReplica = false)
		{
			TaskCreationOptions taskCreationOptions = TaskCreationOptions.DenyChildAttach;
			if (isReplacementReplica)
			{
				taskCreationOptions |= TaskCreationOptions.PreferFairness;
			}
			return taskCreationOptions;
		}

		// Token: 0x04001B2C RID: 6956
		private readonly ThreadLocal<ConcurrentExclusiveSchedulerPair.ProcessingMode> m_threadProcessingMode = new ThreadLocal<ConcurrentExclusiveSchedulerPair.ProcessingMode>();

		// Token: 0x04001B2D RID: 6957
		private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_concurrentTaskScheduler;

		// Token: 0x04001B2E RID: 6958
		private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_exclusiveTaskScheduler;

		// Token: 0x04001B2F RID: 6959
		private readonly TaskScheduler m_underlyingTaskScheduler;

		// Token: 0x04001B30 RID: 6960
		private readonly int m_maxConcurrencyLevel;

		// Token: 0x04001B31 RID: 6961
		private readonly int m_maxItemsPerTask;

		// Token: 0x04001B32 RID: 6962
		private int m_processingCount;

		// Token: 0x04001B33 RID: 6963
		private ConcurrentExclusiveSchedulerPair.CompletionState m_completionState;

		// Token: 0x04001B34 RID: 6964
		private const int UNLIMITED_PROCESSING = -1;

		// Token: 0x04001B35 RID: 6965
		private const int EXCLUSIVE_PROCESSING_SENTINEL = -1;

		// Token: 0x04001B36 RID: 6966
		private const int DEFAULT_MAXITEMSPERTASK = -1;

		// Token: 0x02000308 RID: 776
		private sealed class CompletionState : TaskCompletionSource<VoidTaskResult>
		{
			// Token: 0x0600224D RID: 8781 RVA: 0x0007CAEB File Offset: 0x0007ACEB
			public CompletionState()
			{
			}

			// Token: 0x04001B37 RID: 6967
			internal bool m_completionRequested;

			// Token: 0x04001B38 RID: 6968
			internal bool m_completionQueued;

			// Token: 0x04001B39 RID: 6969
			internal List<Exception> m_exceptions;
		}

		// Token: 0x02000309 RID: 777
		[DebuggerDisplay("Count={CountForDebugger}, MaxConcurrencyLevel={m_maxConcurrencyLevel}, Id={Id}")]
		[DebuggerTypeProxy(typeof(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.DebugView))]
		private sealed class ConcurrentExclusiveTaskScheduler : TaskScheduler
		{
			// Token: 0x0600224E RID: 8782 RVA: 0x0007CAF4 File Offset: 0x0007ACF4
			internal ConcurrentExclusiveTaskScheduler(ConcurrentExclusiveSchedulerPair pair, int maxConcurrencyLevel, ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode)
			{
				this.m_pair = pair;
				this.m_maxConcurrencyLevel = maxConcurrencyLevel;
				this.m_processingMode = processingMode;
				IProducerConsumerQueue<Task> producerConsumerQueue2;
				if (processingMode != ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask)
				{
					IProducerConsumerQueue<Task> producerConsumerQueue = new MultiProducerMultiConsumerQueue<Task>();
					producerConsumerQueue2 = producerConsumerQueue;
				}
				else
				{
					IProducerConsumerQueue<Task> producerConsumerQueue = new SingleProducerSingleConsumerQueue<Task>();
					producerConsumerQueue2 = producerConsumerQueue;
				}
				this.m_tasks = producerConsumerQueue2;
			}

			// Token: 0x17000416 RID: 1046
			// (get) Token: 0x0600224F RID: 8783 RVA: 0x0007CB36 File Offset: 0x0007AD36
			public override int MaximumConcurrencyLevel
			{
				get
				{
					return this.m_maxConcurrencyLevel;
				}
			}

			// Token: 0x06002250 RID: 8784 RVA: 0x0007CB40 File Offset: 0x0007AD40
			protected internal override void QueueTask(Task task)
			{
				object valueLock = this.m_pair.ValueLock;
				lock (valueLock)
				{
					if (this.m_pair.CompletionRequested)
					{
						throw new InvalidOperationException(base.GetType().ToString());
					}
					this.m_tasks.Enqueue(task);
					this.m_pair.ProcessAsyncIfNecessary(false);
				}
			}

			// Token: 0x06002251 RID: 8785 RVA: 0x0007CBB8 File Offset: 0x0007ADB8
			internal void ExecuteTask(Task task)
			{
				base.TryExecuteTask(task);
			}

			// Token: 0x06002252 RID: 8786 RVA: 0x0007CBC4 File Offset: 0x0007ADC4
			protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
			{
				if (!taskWasPreviouslyQueued && this.m_pair.CompletionRequested)
				{
					return false;
				}
				bool flag = this.m_pair.m_underlyingTaskScheduler == TaskScheduler.Default;
				if (flag && taskWasPreviouslyQueued && !Thread.CurrentThread.IsThreadPoolThread)
				{
					return false;
				}
				if (this.m_pair.m_threadProcessingMode.Value != this.m_processingMode)
				{
					return false;
				}
				if (!flag || taskWasPreviouslyQueued)
				{
					return this.TryExecuteTaskInlineOnTargetScheduler(task);
				}
				return base.TryExecuteTask(task);
			}

			// Token: 0x06002253 RID: 8787 RVA: 0x0007CC38 File Offset: 0x0007AE38
			private bool TryExecuteTaskInlineOnTargetScheduler(Task task)
			{
				Task<bool> task2 = new Task<bool>(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.s_tryExecuteTaskShim, Tuple.Create<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task>(this, task));
				bool result;
				try
				{
					task2.RunSynchronously(this.m_pair.m_underlyingTaskScheduler);
					result = task2.Result;
				}
				catch
				{
					AggregateException exception = task2.Exception;
					throw;
				}
				finally
				{
					task2.Dispose();
				}
				return result;
			}

			// Token: 0x06002254 RID: 8788 RVA: 0x0007CCA0 File Offset: 0x0007AEA0
			private static bool TryExecuteTaskShim(object state)
			{
				Tuple<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task> tuple = (Tuple<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task>)state;
				return tuple.Item1.TryExecuteTask(tuple.Item2);
			}

			// Token: 0x06002255 RID: 8789 RVA: 0x0007CCC5 File Offset: 0x0007AEC5
			protected override IEnumerable<Task> GetScheduledTasks()
			{
				return this.m_tasks;
			}

			// Token: 0x17000417 RID: 1047
			// (get) Token: 0x06002256 RID: 8790 RVA: 0x0007CCCD File Offset: 0x0007AECD
			private int CountForDebugger
			{
				get
				{
					return this.m_tasks.Count;
				}
			}

			// Token: 0x06002257 RID: 8791 RVA: 0x0007CCDA File Offset: 0x0007AEDA
			// Note: this type is marked as 'beforefieldinit'.
			static ConcurrentExclusiveTaskScheduler()
			{
			}

			// Token: 0x04001B3A RID: 6970
			private static readonly Func<object, bool> s_tryExecuteTaskShim = new Func<object, bool>(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.TryExecuteTaskShim);

			// Token: 0x04001B3B RID: 6971
			private readonly ConcurrentExclusiveSchedulerPair m_pair;

			// Token: 0x04001B3C RID: 6972
			private readonly int m_maxConcurrencyLevel;

			// Token: 0x04001B3D RID: 6973
			private readonly ConcurrentExclusiveSchedulerPair.ProcessingMode m_processingMode;

			// Token: 0x04001B3E RID: 6974
			internal readonly IProducerConsumerQueue<Task> m_tasks;

			// Token: 0x0200030A RID: 778
			private sealed class DebugView
			{
				// Token: 0x06002258 RID: 8792 RVA: 0x0007CCED File Offset: 0x0007AEED
				public DebugView(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler scheduler)
				{
					this.m_taskScheduler = scheduler;
				}

				// Token: 0x17000418 RID: 1048
				// (get) Token: 0x06002259 RID: 8793 RVA: 0x0007CCFC File Offset: 0x0007AEFC
				public int MaximumConcurrencyLevel
				{
					get
					{
						return this.m_taskScheduler.m_maxConcurrencyLevel;
					}
				}

				// Token: 0x17000419 RID: 1049
				// (get) Token: 0x0600225A RID: 8794 RVA: 0x0007CD09 File Offset: 0x0007AF09
				public IEnumerable<Task> ScheduledTasks
				{
					get
					{
						return this.m_taskScheduler.m_tasks;
					}
				}

				// Token: 0x1700041A RID: 1050
				// (get) Token: 0x0600225B RID: 8795 RVA: 0x0007CD16 File Offset: 0x0007AF16
				public ConcurrentExclusiveSchedulerPair SchedulerPair
				{
					get
					{
						return this.m_taskScheduler.m_pair;
					}
				}

				// Token: 0x04001B3F RID: 6975
				private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_taskScheduler;
			}
		}

		// Token: 0x0200030B RID: 779
		private sealed class DebugView
		{
			// Token: 0x0600225C RID: 8796 RVA: 0x0007CD23 File Offset: 0x0007AF23
			public DebugView(ConcurrentExclusiveSchedulerPair pair)
			{
				this.m_pair = pair;
			}

			// Token: 0x1700041B RID: 1051
			// (get) Token: 0x0600225D RID: 8797 RVA: 0x0007CD32 File Offset: 0x0007AF32
			public ConcurrentExclusiveSchedulerPair.ProcessingMode Mode
			{
				get
				{
					return this.m_pair.ModeForDebugger;
				}
			}

			// Token: 0x1700041C RID: 1052
			// (get) Token: 0x0600225E RID: 8798 RVA: 0x0007CD3F File Offset: 0x0007AF3F
			public IEnumerable<Task> ScheduledExclusive
			{
				get
				{
					return this.m_pair.m_exclusiveTaskScheduler.m_tasks;
				}
			}

			// Token: 0x1700041D RID: 1053
			// (get) Token: 0x0600225F RID: 8799 RVA: 0x0007CD51 File Offset: 0x0007AF51
			public IEnumerable<Task> ScheduledConcurrent
			{
				get
				{
					return this.m_pair.m_concurrentTaskScheduler.m_tasks;
				}
			}

			// Token: 0x1700041E RID: 1054
			// (get) Token: 0x06002260 RID: 8800 RVA: 0x0007CD63 File Offset: 0x0007AF63
			public int CurrentlyExecutingTaskCount
			{
				get
				{
					if (this.m_pair.m_processingCount != -1)
					{
						return this.m_pair.m_processingCount;
					}
					return 1;
				}
			}

			// Token: 0x1700041F RID: 1055
			// (get) Token: 0x06002261 RID: 8801 RVA: 0x0007CD80 File Offset: 0x0007AF80
			public TaskScheduler TargetScheduler
			{
				get
				{
					return this.m_pair.m_underlyingTaskScheduler;
				}
			}

			// Token: 0x04001B40 RID: 6976
			private readonly ConcurrentExclusiveSchedulerPair m_pair;
		}

		// Token: 0x0200030C RID: 780
		[Flags]
		private enum ProcessingMode : byte
		{
			// Token: 0x04001B42 RID: 6978
			NotCurrentlyProcessing = 0,
			// Token: 0x04001B43 RID: 6979
			ProcessingExclusiveTask = 1,
			// Token: 0x04001B44 RID: 6980
			ProcessingConcurrentTasks = 2,
			// Token: 0x04001B45 RID: 6981
			Completing = 4,
			// Token: 0x04001B46 RID: 6982
			Completed = 8
		}

		// Token: 0x0200030D RID: 781
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002262 RID: 8802 RVA: 0x0007CD8D File Offset: 0x0007AF8D
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002263 RID: 8803 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06002264 RID: 8804 RVA: 0x0007CD99 File Offset: 0x0007AF99
			internal ConcurrentExclusiveSchedulerPair.CompletionState <EnsureCompletionStateInitialized>b__22_0()
			{
				return new ConcurrentExclusiveSchedulerPair.CompletionState();
			}

			// Token: 0x06002265 RID: 8805 RVA: 0x0007CDA0 File Offset: 0x0007AFA0
			internal void <CompleteTaskAsync>b__29_0(object state)
			{
				ConcurrentExclusiveSchedulerPair concurrentExclusiveSchedulerPair = (ConcurrentExclusiveSchedulerPair)state;
				List<Exception> exceptions = concurrentExclusiveSchedulerPair.m_completionState.m_exceptions;
				if (exceptions == null || exceptions.Count <= 0)
				{
					concurrentExclusiveSchedulerPair.m_completionState.TrySetResult(default(VoidTaskResult));
				}
				else
				{
					concurrentExclusiveSchedulerPair.m_completionState.TrySetException(exceptions);
				}
				concurrentExclusiveSchedulerPair.m_threadProcessingMode.Dispose();
			}

			// Token: 0x06002266 RID: 8806 RVA: 0x0007CDFB File Offset: 0x0007AFFB
			internal void <ProcessAsyncIfNecessary>b__39_0(object thisPair)
			{
				((ConcurrentExclusiveSchedulerPair)thisPair).ProcessExclusiveTasks();
			}

			// Token: 0x06002267 RID: 8807 RVA: 0x0007CE08 File Offset: 0x0007B008
			internal void <ProcessAsyncIfNecessary>b__39_1(object thisPair)
			{
				((ConcurrentExclusiveSchedulerPair)thisPair).ProcessConcurrentTasks();
			}

			// Token: 0x04001B47 RID: 6983
			public static readonly ConcurrentExclusiveSchedulerPair.<>c <>9 = new ConcurrentExclusiveSchedulerPair.<>c();

			// Token: 0x04001B48 RID: 6984
			public static Func<ConcurrentExclusiveSchedulerPair.CompletionState> <>9__22_0;

			// Token: 0x04001B49 RID: 6985
			public static WaitCallback <>9__29_0;

			// Token: 0x04001B4A RID: 6986
			public static Action<object> <>9__39_0;

			// Token: 0x04001B4B RID: 6987
			public static Action<object> <>9__39_1;
		}
	}
}
