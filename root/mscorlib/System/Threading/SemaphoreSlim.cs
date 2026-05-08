using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Threading
{
	// Token: 0x02000289 RID: 649
	[ComVisible(false)]
	[DebuggerDisplay("Current Count = {m_currentCount}")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class SemaphoreSlim : IDisposable
	{
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x00070F4E File Offset: 0x0006F14E
		public int CurrentCount
		{
			get
			{
				return this.m_currentCount;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x00070F58 File Offset: 0x0006F158
		public WaitHandle AvailableWaitHandle
		{
			get
			{
				this.CheckDispose();
				if (this.m_waitHandle != null)
				{
					return this.m_waitHandle;
				}
				object lockObj = this.m_lockObj;
				lock (lockObj)
				{
					if (this.m_waitHandle == null)
					{
						this.m_waitHandle = new ManualResetEvent(this.m_currentCount != 0);
					}
				}
				return this.m_waitHandle;
			}
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x00070FD8 File Offset: 0x0006F1D8
		public SemaphoreSlim(int initialCount)
			: this(initialCount, int.MaxValue)
		{
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x00070FE8 File Offset: 0x0006F1E8
		public SemaphoreSlim(int initialCount, int maxCount)
		{
			if (initialCount < 0 || initialCount > maxCount)
			{
				throw new ArgumentOutOfRangeException("initialCount", initialCount, SemaphoreSlim.GetResourceString("The initialCount argument must be non-negative and less than or equal to the maximumCount."));
			}
			if (maxCount <= 0)
			{
				throw new ArgumentOutOfRangeException("maxCount", maxCount, SemaphoreSlim.GetResourceString("The maximumCount argument must be a positive number. If a maximum is not required, use the constructor without a maxCount parameter."));
			}
			this.m_maxCount = maxCount;
			this.m_lockObj = new object();
			this.m_currentCount = initialCount;
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x00071058 File Offset: 0x0006F258
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x00071076 File Offset: 0x0006F276
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x00071084 File Offset: 0x0006F284
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SemaphoreSlim.GetResourceString("The timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			return this.Wait((int)timeout.TotalMilliseconds, default(CancellationToken));
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x000710DC File Offset: 0x0006F2DC
		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SemaphoreSlim.GetResourceString("The timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			return this.Wait((int)timeout.TotalMilliseconds, cancellationToken);
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0007112C File Offset: 0x0006F32C
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0007114C File Offset: 0x0006F34C
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.CheckDispose();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("totalMilliSeconds", millisecondsTimeout, SemaphoreSlim.GetResourceString("The timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			cancellationToken.ThrowIfCancellationRequested();
			if (millisecondsTimeout == 0 && this.m_currentCount == 0)
			{
				return false;
			}
			uint num = 0U;
			if (millisecondsTimeout != -1 && millisecondsTimeout > 0)
			{
				num = TimeoutHelper.GetTime();
			}
			bool flag = false;
			Task<bool> task = null;
			bool flag2 = false;
			CancellationTokenRegistration cancellationTokenRegistration = cancellationToken.InternalRegisterWithoutEC(SemaphoreSlim.s_cancellationTokenCanceledEventHandler, this);
			try
			{
				SpinWait spinWait = default(SpinWait);
				while (this.m_currentCount == 0 && !spinWait.NextSpinWillYield)
				{
					spinWait.SpinOnce();
				}
				try
				{
				}
				finally
				{
					Monitor.Enter(this.m_lockObj, ref flag2);
					if (flag2)
					{
						this.m_waitCount++;
					}
				}
				if (this.m_asyncHead != null)
				{
					task = this.WaitAsync(millisecondsTimeout, cancellationToken);
				}
				else
				{
					OperationCanceledException ex = null;
					if (this.m_currentCount == 0)
					{
						if (millisecondsTimeout == 0)
						{
							return false;
						}
						try
						{
							flag = this.WaitUntilCountOrTimeout(millisecondsTimeout, num, cancellationToken);
						}
						catch (OperationCanceledException ex)
						{
						}
					}
					if (this.m_currentCount > 0)
					{
						flag = true;
						this.m_currentCount--;
					}
					else if (ex != null)
					{
						throw ex;
					}
					if (this.m_waitHandle != null && this.m_currentCount == 0)
					{
						this.m_waitHandle.Reset();
					}
				}
			}
			finally
			{
				if (flag2)
				{
					this.m_waitCount--;
					Monitor.Exit(this.m_lockObj);
				}
				cancellationTokenRegistration.Dispose();
			}
			if (task == null)
			{
				return flag;
			}
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x000712F8 File Offset: 0x0006F4F8
		private bool WaitUntilCountOrTimeout(int millisecondsTimeout, uint startTime, CancellationToken cancellationToken)
		{
			int num = -1;
			while (this.m_currentCount == 0)
			{
				cancellationToken.ThrowIfCancellationRequested();
				if (millisecondsTimeout != -1)
				{
					num = TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout);
					if (num <= 0)
					{
						return false;
					}
				}
				if (!Monitor.Wait(this.m_lockObj, num))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x00071340 File Offset: 0x0006F540
		public Task WaitAsync()
		{
			return this.WaitAsync(-1, default(CancellationToken));
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x0007135D File Offset: 0x0006F55D
		public Task WaitAsync(CancellationToken cancellationToken)
		{
			return this.WaitAsync(-1, cancellationToken);
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x00071368 File Offset: 0x0006F568
		public Task<bool> WaitAsync(int millisecondsTimeout)
		{
			return this.WaitAsync(millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00071388 File Offset: 0x0006F588
		public Task<bool> WaitAsync(TimeSpan timeout)
		{
			return this.WaitAsync(timeout, default(CancellationToken));
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x000713A8 File Offset: 0x0006F5A8
		public Task<bool> WaitAsync(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SemaphoreSlim.GetResourceString("The timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			return this.WaitAsync((int)timeout.TotalMilliseconds, cancellationToken);
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x000713F8 File Offset: 0x0006F5F8
		public Task<bool> WaitAsync(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.CheckDispose();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("totalMilliSeconds", millisecondsTimeout, SemaphoreSlim.GetResourceString("The timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation<bool>(cancellationToken);
			}
			object lockObj = this.m_lockObj;
			Task<bool> task;
			lock (lockObj)
			{
				if (this.m_currentCount > 0)
				{
					this.m_currentCount--;
					if (this.m_waitHandle != null && this.m_currentCount == 0)
					{
						this.m_waitHandle.Reset();
					}
					task = SemaphoreSlim.s_trueTask;
				}
				else if (millisecondsTimeout == 0)
				{
					task = SemaphoreSlim.s_falseTask;
				}
				else
				{
					SemaphoreSlim.TaskNode taskNode = this.CreateAndAddAsyncWaiter();
					task = ((millisecondsTimeout == -1 && !cancellationToken.CanBeCanceled) ? taskNode : this.WaitUntilCountOrTimeoutAsync(taskNode, millisecondsTimeout, cancellationToken));
				}
			}
			return task;
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x000714DC File Offset: 0x0006F6DC
		private SemaphoreSlim.TaskNode CreateAndAddAsyncWaiter()
		{
			SemaphoreSlim.TaskNode taskNode = new SemaphoreSlim.TaskNode();
			if (this.m_asyncHead == null)
			{
				this.m_asyncHead = taskNode;
				this.m_asyncTail = taskNode;
			}
			else
			{
				this.m_asyncTail.Next = taskNode;
				taskNode.Prev = this.m_asyncTail;
				this.m_asyncTail = taskNode;
			}
			return taskNode;
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x00071528 File Offset: 0x0006F728
		private bool RemoveAsyncWaiter(SemaphoreSlim.TaskNode task)
		{
			bool flag = this.m_asyncHead == task || task.Prev != null;
			if (task.Next != null)
			{
				task.Next.Prev = task.Prev;
			}
			if (task.Prev != null)
			{
				task.Prev.Next = task.Next;
			}
			if (this.m_asyncHead == task)
			{
				this.m_asyncHead = task.Next;
			}
			if (this.m_asyncTail == task)
			{
				this.m_asyncTail = task.Prev;
			}
			task.Next = (task.Prev = null);
			return flag;
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x000715B8 File Offset: 0x0006F7B8
		private async Task<bool> WaitUntilCountOrTimeoutAsync(SemaphoreSlim.TaskNode asyncWaiter, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			using (CancellationTokenSource cts = (cancellationToken.CanBeCanceled ? CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, default(CancellationToken)) : new CancellationTokenSource()))
			{
				Task<Task> task = Task.WhenAny(new Task[]
				{
					asyncWaiter,
					Task.Delay(millisecondsTimeout, cts.Token)
				});
				object obj = asyncWaiter;
				Task task2 = await task.ConfigureAwait(false);
				if (obj == task2)
				{
					obj = null;
					cts.Cancel();
					return true;
				}
			}
			CancellationTokenSource cts = null;
			object lockObj = this.m_lockObj;
			lock (lockObj)
			{
				if (this.RemoveAsyncWaiter(asyncWaiter))
				{
					cancellationToken.ThrowIfCancellationRequested();
					return false;
				}
			}
			return await asyncWaiter.ConfigureAwait(false);
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x00071613 File Offset: 0x0006F813
		public int Release()
		{
			return this.Release(1);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x0007161C File Offset: 0x0006F81C
		public int Release(int releaseCount)
		{
			this.CheckDispose();
			if (releaseCount < 1)
			{
				throw new ArgumentOutOfRangeException("releaseCount", releaseCount, SemaphoreSlim.GetResourceString("The releaseCount argument must be greater than zero."));
			}
			object lockObj = this.m_lockObj;
			int num2;
			lock (lockObj)
			{
				int num = this.m_currentCount;
				num2 = num;
				if (this.m_maxCount - num < releaseCount)
				{
					throw new SemaphoreFullException();
				}
				num += releaseCount;
				int waitCount = this.m_waitCount;
				if (num == 1 || waitCount == 1)
				{
					Monitor.Pulse(this.m_lockObj);
				}
				else if (waitCount > 1)
				{
					Monitor.PulseAll(this.m_lockObj);
				}
				if (this.m_asyncHead != null)
				{
					int num3 = num - waitCount;
					while (num3 > 0 && this.m_asyncHead != null)
					{
						num--;
						num3--;
						SemaphoreSlim.TaskNode asyncHead = this.m_asyncHead;
						this.RemoveAsyncWaiter(asyncHead);
						SemaphoreSlim.QueueWaiterTask(asyncHead);
					}
				}
				this.m_currentCount = num;
				if (this.m_waitHandle != null && num2 == 0 && num > 0)
				{
					this.m_waitHandle.Set();
				}
			}
			return num2;
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x00071734 File Offset: 0x0006F934
		[SecuritySafeCritical]
		private static void QueueWaiterTask(SemaphoreSlim.TaskNode waiterTask)
		{
			ThreadPool.UnsafeQueueCustomWorkItem(waiterTask, false);
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x0007173D File Offset: 0x0006F93D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x0007174C File Offset: 0x0006F94C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_waitHandle != null)
				{
					this.m_waitHandle.Close();
					this.m_waitHandle = null;
				}
				this.m_lockObj = null;
				this.m_asyncHead = null;
				this.m_asyncTail = null;
			}
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x00071788 File Offset: 0x0006F988
		private static void CancellationTokenCanceledEventHandler(object obj)
		{
			SemaphoreSlim semaphoreSlim = obj as SemaphoreSlim;
			object lockObj = semaphoreSlim.m_lockObj;
			lock (lockObj)
			{
				Monitor.PulseAll(semaphoreSlim.m_lockObj);
			}
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x000717D4 File Offset: 0x0006F9D4
		private void CheckDispose()
		{
			if (this.m_lockObj == null)
			{
				throw new ObjectDisposedException(null, SemaphoreSlim.GetResourceString("The semaphore has been disposed."));
			}
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x000717EF File Offset: 0x0006F9EF
		private static string GetResourceString(string str)
		{
			return Environment.GetResourceString(str);
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x000717F8 File Offset: 0x0006F9F8
		// Note: this type is marked as 'beforefieldinit'.
		static SemaphoreSlim()
		{
		}

		// Token: 0x04001984 RID: 6532
		private volatile int m_currentCount;

		// Token: 0x04001985 RID: 6533
		private readonly int m_maxCount;

		// Token: 0x04001986 RID: 6534
		private volatile int m_waitCount;

		// Token: 0x04001987 RID: 6535
		private object m_lockObj;

		// Token: 0x04001988 RID: 6536
		private volatile ManualResetEvent m_waitHandle;

		// Token: 0x04001989 RID: 6537
		private SemaphoreSlim.TaskNode m_asyncHead;

		// Token: 0x0400198A RID: 6538
		private SemaphoreSlim.TaskNode m_asyncTail;

		// Token: 0x0400198B RID: 6539
		private static readonly Task<bool> s_trueTask = new Task<bool>(false, true, (TaskCreationOptions)16384, default(CancellationToken));

		// Token: 0x0400198C RID: 6540
		private static readonly Task<bool> s_falseTask = new Task<bool>(false, false, (TaskCreationOptions)16384, default(CancellationToken));

		// Token: 0x0400198D RID: 6541
		private const int NO_MAXIMUM = 2147483647;

		// Token: 0x0400198E RID: 6542
		private static Action<object> s_cancellationTokenCanceledEventHandler = new Action<object>(SemaphoreSlim.CancellationTokenCanceledEventHandler);

		// Token: 0x0200028A RID: 650
		private sealed class TaskNode : Task<bool>, IThreadPoolWorkItem
		{
			// Token: 0x06001E48 RID: 7752 RVA: 0x0007184A File Offset: 0x0006FA4A
			internal TaskNode()
			{
			}

			// Token: 0x06001E49 RID: 7753 RVA: 0x00071852 File Offset: 0x0006FA52
			[SecurityCritical]
			void IThreadPoolWorkItem.ExecuteWorkItem()
			{
				base.TrySetResult(true);
			}

			// Token: 0x06001E4A RID: 7754 RVA: 0x00004088 File Offset: 0x00002288
			[SecurityCritical]
			void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
			{
			}

			// Token: 0x0400198F RID: 6543
			internal SemaphoreSlim.TaskNode Prev;

			// Token: 0x04001990 RID: 6544
			internal SemaphoreSlim.TaskNode Next;
		}

		// Token: 0x0200028B RID: 651
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WaitUntilCountOrTimeoutAsync>d__32 : IAsyncStateMachine
		{
			// Token: 0x06001E4B RID: 7755 RVA: 0x0007185C File Offset: 0x0006FA5C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				SemaphoreSlim semaphoreSlim = this;
				bool flag;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							num = (num2 = -1);
							goto IL_01CF;
						}
						cts = (cancellationToken.CanBeCanceled ? CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, default(CancellationToken)) : new CancellationTokenSource());
					}
					try
					{
						ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter3;
						if (num != 0)
						{
							Task<Task> task = Task.WhenAny(new Task[]
							{
								asyncWaiter,
								Task.Delay(millisecondsTimeout, cts.Token)
							});
							obj = asyncWaiter;
							configuredTaskAwaiter3 = task.ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter3.IsCompleted)
							{
								num = (num2 = 0);
								ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter4 = configuredTaskAwaiter3;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter, SemaphoreSlim.<WaitUntilCountOrTimeoutAsync>d__32>(ref configuredTaskAwaiter3, ref this);
								return;
							}
						}
						else
						{
							ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
							configuredTaskAwaiter3 = configuredTaskAwaiter4;
							configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
							num = (num2 = -1);
						}
						Task result = configuredTaskAwaiter3.GetResult();
						if (obj == result)
						{
							obj = null;
							cts.Cancel();
							flag = true;
							goto IL_01F2;
						}
					}
					finally
					{
						if (num < 0 && cts != null)
						{
							((IDisposable)cts).Dispose();
						}
					}
					cts = null;
					object lockObj = semaphoreSlim.m_lockObj;
					bool flag2 = false;
					try
					{
						Monitor.Enter(lockObj, ref flag2);
						if (semaphoreSlim.RemoveAsyncWaiter(asyncWaiter))
						{
							cancellationToken.ThrowIfCancellationRequested();
							flag = false;
							goto IL_01F2;
						}
					}
					finally
					{
						if (num < 0 && flag2)
						{
							Monitor.Exit(lockObj);
						}
					}
					configuredTaskAwaiter = asyncWaiter.ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						num = (num2 = 1);
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, SemaphoreSlim.<WaitUntilCountOrTimeoutAsync>d__32>(ref configuredTaskAwaiter, ref this);
						return;
					}
					IL_01CF:
					flag = configuredTaskAwaiter.GetResult();
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				IL_01F2:
				num2 = -2;
				this.<>t__builder.SetResult(flag);
			}

			// Token: 0x06001E4C RID: 7756 RVA: 0x00071ABC File Offset: 0x0006FCBC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001991 RID: 6545
			public int <>1__state;

			// Token: 0x04001992 RID: 6546
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04001993 RID: 6547
			public CancellationToken cancellationToken;

			// Token: 0x04001994 RID: 6548
			public SemaphoreSlim.TaskNode asyncWaiter;

			// Token: 0x04001995 RID: 6549
			public int millisecondsTimeout;

			// Token: 0x04001996 RID: 6550
			public SemaphoreSlim <>4__this;

			// Token: 0x04001997 RID: 6551
			private CancellationTokenSource <cts>5__2;

			// Token: 0x04001998 RID: 6552
			private object <>7__wrap2;

			// Token: 0x04001999 RID: 6553
			private ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x0400199A RID: 6554
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}
	}
}
