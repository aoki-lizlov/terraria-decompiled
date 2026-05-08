using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020002B0 RID: 688
	internal sealed class ThreadPoolWorkQueueThreadLocals
	{
		// Token: 0x06001FD6 RID: 8150 RVA: 0x00075653 File Offset: 0x00073853
		public ThreadPoolWorkQueueThreadLocals(ThreadPoolWorkQueue tpq)
		{
			this.workQueue = tpq;
			this.workStealingQueue = new ThreadPoolWorkQueue.WorkStealingQueue();
			ThreadPoolWorkQueue.allThreadQueues.Add(this.workStealingQueue);
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x00075694 File Offset: 0x00073894
		[SecurityCritical]
		private void CleanUp()
		{
			if (this.workStealingQueue != null)
			{
				if (this.workQueue != null)
				{
					bool flag = false;
					while (!flag)
					{
						try
						{
						}
						finally
						{
							IThreadPoolWorkItem threadPoolWorkItem = null;
							if (this.workStealingQueue.LocalPop(out threadPoolWorkItem))
							{
								this.workQueue.Enqueue(threadPoolWorkItem, true);
							}
							else
							{
								flag = true;
							}
						}
					}
				}
				ThreadPoolWorkQueue.allThreadQueues.Remove(this.workStealingQueue);
			}
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x00075700 File Offset: 0x00073900
		[SecuritySafeCritical]
		~ThreadPoolWorkQueueThreadLocals()
		{
			if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
			{
				this.CleanUp();
			}
		}

		// Token: 0x04001A0A RID: 6666
		[ThreadStatic]
		[SecurityCritical]
		public static ThreadPoolWorkQueueThreadLocals threadLocals;

		// Token: 0x04001A0B RID: 6667
		public readonly ThreadPoolWorkQueue workQueue;

		// Token: 0x04001A0C RID: 6668
		public readonly ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue;

		// Token: 0x04001A0D RID: 6669
		public readonly Random random = new Random(Thread.CurrentThread.ManagedThreadId);
	}
}
