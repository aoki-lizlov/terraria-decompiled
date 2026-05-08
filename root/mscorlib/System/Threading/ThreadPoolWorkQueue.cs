using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Threading
{
	// Token: 0x020002AC RID: 684
	internal sealed class ThreadPoolWorkQueue
	{
		// Token: 0x06001FBD RID: 8125 RVA: 0x00074B58 File Offset: 0x00072D58
		public ThreadPoolWorkQueue()
		{
			this.queueTail = (this.queueHead = new ThreadPoolWorkQueue.QueueSegment());
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x00074B83 File Offset: 0x00072D83
		[SecurityCritical]
		public ThreadPoolWorkQueueThreadLocals EnsureCurrentThreadHasQueue()
		{
			if (ThreadPoolWorkQueueThreadLocals.threadLocals == null)
			{
				ThreadPoolWorkQueueThreadLocals.threadLocals = new ThreadPoolWorkQueueThreadLocals(this);
			}
			return ThreadPoolWorkQueueThreadLocals.threadLocals;
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x00074B9C File Offset: 0x00072D9C
		[SecurityCritical]
		internal void EnsureThreadRequested()
		{
			int num;
			for (int i = this.numOutstandingThreadRequests; i < ThreadPoolGlobals.processorCount; i = num)
			{
				num = Interlocked.CompareExchange(ref this.numOutstandingThreadRequests, i + 1, i);
				if (num == i)
				{
					ThreadPool.RequestWorkerThread();
					return;
				}
			}
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x00074BDC File Offset: 0x00072DDC
		[SecurityCritical]
		internal void MarkThreadRequestSatisfied()
		{
			int num;
			for (int i = this.numOutstandingThreadRequests; i > 0; i = num)
			{
				num = Interlocked.CompareExchange(ref this.numOutstandingThreadRequests, i - 1, i);
				if (num == i)
				{
					break;
				}
			}
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x00074C10 File Offset: 0x00072E10
		[SecurityCritical]
		public void Enqueue(IThreadPoolWorkItem callback, bool forceGlobal)
		{
			ThreadPoolWorkQueueThreadLocals threadPoolWorkQueueThreadLocals = null;
			if (!forceGlobal)
			{
				threadPoolWorkQueueThreadLocals = ThreadPoolWorkQueueThreadLocals.threadLocals;
			}
			if (threadPoolWorkQueueThreadLocals != null)
			{
				threadPoolWorkQueueThreadLocals.workStealingQueue.LocalPush(callback);
			}
			else
			{
				ThreadPoolWorkQueue.QueueSegment queueSegment = this.queueHead;
				while (!queueSegment.TryEnqueue(callback))
				{
					Interlocked.CompareExchange<ThreadPoolWorkQueue.QueueSegment>(ref queueSegment.Next, new ThreadPoolWorkQueue.QueueSegment(), null);
					while (queueSegment.Next != null)
					{
						Interlocked.CompareExchange<ThreadPoolWorkQueue.QueueSegment>(ref this.queueHead, queueSegment.Next, queueSegment);
						queueSegment = this.queueHead;
					}
				}
			}
			ThreadPool.NotifyWorkItemQueued();
			this.EnsureThreadRequested();
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x00074C94 File Offset: 0x00072E94
		[SecurityCritical]
		internal bool LocalFindAndPop(IThreadPoolWorkItem callback)
		{
			ThreadPoolWorkQueueThreadLocals threadLocals = ThreadPoolWorkQueueThreadLocals.threadLocals;
			return threadLocals != null && threadLocals.workStealingQueue.LocalFindAndPop(callback);
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x00074CB8 File Offset: 0x00072EB8
		[SecurityCritical]
		public void Dequeue(ThreadPoolWorkQueueThreadLocals tl, out IThreadPoolWorkItem callback, out bool missedSteal)
		{
			callback = null;
			missedSteal = false;
			ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue = tl.workStealingQueue;
			workStealingQueue.LocalPop(out callback);
			if (callback == null)
			{
				ThreadPoolWorkQueue.QueueSegment queueSegment = this.queueTail;
				while (!queueSegment.TryDequeue(out callback) && queueSegment.Next != null && queueSegment.IsUsedUp())
				{
					Interlocked.CompareExchange<ThreadPoolWorkQueue.QueueSegment>(ref this.queueTail, queueSegment.Next, queueSegment);
					queueSegment = this.queueTail;
				}
			}
			if (callback == null)
			{
				ThreadPoolWorkQueue.WorkStealingQueue[] array = ThreadPoolWorkQueue.allThreadQueues.Current;
				int num = tl.random.Next(array.Length);
				for (int i = array.Length; i > 0; i--)
				{
					ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue2 = Volatile.Read<ThreadPoolWorkQueue.WorkStealingQueue>(ref array[num % array.Length]);
					if (workStealingQueue2 != null && workStealingQueue2 != workStealingQueue && workStealingQueue2.TrySteal(out callback, ref missedSteal))
					{
						break;
					}
					num++;
				}
			}
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x00074D7C File Offset: 0x00072F7C
		[SecurityCritical]
		internal static bool Dispatch()
		{
			ThreadPoolWorkQueue workQueue = ThreadPoolGlobals.workQueue;
			int tickCount = Environment.TickCount;
			workQueue.MarkThreadRequestSatisfied();
			bool flag = true;
			IThreadPoolWorkItem threadPoolWorkItem = null;
			try
			{
				ThreadPoolWorkQueueThreadLocals threadPoolWorkQueueThreadLocals = workQueue.EnsureCurrentThreadHasQueue();
				while ((long)(Environment.TickCount - tickCount) < 30L)
				{
					try
					{
					}
					finally
					{
						bool flag2 = false;
						workQueue.Dequeue(threadPoolWorkQueueThreadLocals, out threadPoolWorkItem, out flag2);
						if (threadPoolWorkItem == null)
						{
							flag = flag2;
						}
						else
						{
							workQueue.EnsureThreadRequested();
						}
					}
					if (threadPoolWorkItem == null)
					{
						return true;
					}
					if (ThreadPoolGlobals.enableWorkerTracking)
					{
						bool flag3 = false;
						try
						{
							try
							{
							}
							finally
							{
								ThreadPool.ReportThreadStatus(true);
								flag3 = true;
							}
							threadPoolWorkItem.ExecuteWorkItem();
							threadPoolWorkItem = null;
							goto IL_007C;
						}
						finally
						{
							if (flag3)
							{
								ThreadPool.ReportThreadStatus(false);
							}
						}
						goto IL_0074;
					}
					goto IL_0074;
					IL_007C:
					if (!ThreadPool.NotifyWorkItemComplete())
					{
						return false;
					}
					continue;
					IL_0074:
					threadPoolWorkItem.ExecuteWorkItem();
					threadPoolWorkItem = null;
					goto IL_007C;
				}
				return true;
			}
			catch (ThreadAbortException ex)
			{
				if (threadPoolWorkItem != null)
				{
					threadPoolWorkItem.MarkAborted(ex);
				}
				flag = false;
			}
			finally
			{
				if (flag)
				{
					workQueue.EnsureThreadRequested();
				}
			}
			return true;
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x00074E84 File Offset: 0x00073084
		// Note: this type is marked as 'beforefieldinit'.
		static ThreadPoolWorkQueue()
		{
		}

		// Token: 0x040019F9 RID: 6649
		internal volatile ThreadPoolWorkQueue.QueueSegment queueHead;

		// Token: 0x040019FA RID: 6650
		internal volatile ThreadPoolWorkQueue.QueueSegment queueTail;

		// Token: 0x040019FB RID: 6651
		internal static ThreadPoolWorkQueue.SparseArray<ThreadPoolWorkQueue.WorkStealingQueue> allThreadQueues = new ThreadPoolWorkQueue.SparseArray<ThreadPoolWorkQueue.WorkStealingQueue>(16);

		// Token: 0x040019FC RID: 6652
		private volatile int numOutstandingThreadRequests;

		// Token: 0x020002AD RID: 685
		internal class SparseArray<T> where T : class
		{
			// Token: 0x06001FC6 RID: 8134 RVA: 0x00074E92 File Offset: 0x00073092
			internal SparseArray(int initialSize)
			{
				this.m_array = new T[initialSize];
			}

			// Token: 0x170003CC RID: 972
			// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x00074EA8 File Offset: 0x000730A8
			internal T[] Current
			{
				get
				{
					return this.m_array;
				}
			}

			// Token: 0x06001FC8 RID: 8136 RVA: 0x00074EB4 File Offset: 0x000730B4
			internal int Add(T e)
			{
				for (;;)
				{
					T[] array = this.m_array;
					T[] array2 = array;
					lock (array2)
					{
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i] == null)
							{
								Volatile.Write<T>(ref array[i], e);
								return i;
							}
							if (i == array.Length - 1 && array == this.m_array)
							{
								T[] array3 = new T[array.Length * 2];
								Array.Copy(array, array3, i + 1);
								array3[i + 1] = e;
								this.m_array = array3;
								return i + 1;
							}
						}
						continue;
					}
					break;
				}
				int num;
				return num;
			}

			// Token: 0x06001FC9 RID: 8137 RVA: 0x00074F6C File Offset: 0x0007316C
			internal void Remove(T e)
			{
				T[] array = this.m_array;
				lock (array)
				{
					for (int i = 0; i < this.m_array.Length; i++)
					{
						if (this.m_array[i] == e)
						{
							Volatile.Write<T>(ref this.m_array[i], default(T));
							break;
						}
					}
				}
			}

			// Token: 0x040019FD RID: 6653
			private volatile T[] m_array;
		}

		// Token: 0x020002AE RID: 686
		internal class WorkStealingQueue
		{
			// Token: 0x06001FCA RID: 8138 RVA: 0x00074FF8 File Offset: 0x000731F8
			public void LocalPush(IThreadPoolWorkItem obj)
			{
				int num = this.m_tailIndex;
				if (num == 2147483647)
				{
					bool flag = false;
					try
					{
						this.m_foreignLock.Enter(ref flag);
						if (this.m_tailIndex == 2147483647)
						{
							this.m_headIndex &= this.m_mask;
							num = (this.m_tailIndex &= this.m_mask);
						}
					}
					finally
					{
						if (flag)
						{
							this.m_foreignLock.Exit(true);
						}
					}
				}
				if (num < this.m_headIndex + this.m_mask)
				{
					Volatile.Write<IThreadPoolWorkItem>(ref this.m_array[num & this.m_mask], obj);
					this.m_tailIndex = num + 1;
					return;
				}
				bool flag2 = false;
				try
				{
					this.m_foreignLock.Enter(ref flag2);
					int headIndex = this.m_headIndex;
					int num2 = this.m_tailIndex - this.m_headIndex;
					if (num2 >= this.m_mask)
					{
						IThreadPoolWorkItem[] array = new IThreadPoolWorkItem[this.m_array.Length << 1];
						for (int i = 0; i < this.m_array.Length; i++)
						{
							array[i] = this.m_array[(i + headIndex) & this.m_mask];
						}
						this.m_array = array;
						this.m_headIndex = 0;
						num = (this.m_tailIndex = num2);
						this.m_mask = (this.m_mask << 1) | 1;
					}
					Volatile.Write<IThreadPoolWorkItem>(ref this.m_array[num & this.m_mask], obj);
					this.m_tailIndex = num + 1;
				}
				finally
				{
					if (flag2)
					{
						this.m_foreignLock.Exit(false);
					}
				}
			}

			// Token: 0x06001FCB RID: 8139 RVA: 0x000751C0 File Offset: 0x000733C0
			public bool LocalFindAndPop(IThreadPoolWorkItem obj)
			{
				if (this.m_array[(this.m_tailIndex - 1) & this.m_mask] == obj)
				{
					IThreadPoolWorkItem threadPoolWorkItem;
					return this.LocalPop(out threadPoolWorkItem);
				}
				for (int i = this.m_tailIndex - 2; i >= this.m_headIndex; i--)
				{
					if (this.m_array[i & this.m_mask] == obj)
					{
						bool flag = false;
						try
						{
							this.m_foreignLock.Enter(ref flag);
							if (this.m_array[i & this.m_mask] == null)
							{
								return false;
							}
							Volatile.Write<IThreadPoolWorkItem>(ref this.m_array[i & this.m_mask], null);
							if (i == this.m_tailIndex)
							{
								this.m_tailIndex--;
							}
							else if (i == this.m_headIndex)
							{
								this.m_headIndex++;
							}
							return true;
						}
						finally
						{
							if (flag)
							{
								this.m_foreignLock.Exit(false);
							}
						}
					}
				}
				return false;
			}

			// Token: 0x06001FCC RID: 8140 RVA: 0x000752E0 File Offset: 0x000734E0
			public bool LocalPop(out IThreadPoolWorkItem obj)
			{
				int num3;
				for (;;)
				{
					int num = this.m_tailIndex;
					if (this.m_headIndex >= num)
					{
						break;
					}
					num--;
					Interlocked.Exchange(ref this.m_tailIndex, num);
					if (this.m_headIndex > num)
					{
						bool flag = false;
						bool flag2;
						try
						{
							this.m_foreignLock.Enter(ref flag);
							if (this.m_headIndex <= num)
							{
								int num2 = num & this.m_mask;
								obj = Volatile.Read<IThreadPoolWorkItem>(ref this.m_array[num2]);
								if (obj == null)
								{
									continue;
								}
								this.m_array[num2] = null;
								flag2 = true;
							}
							else
							{
								this.m_tailIndex = num + 1;
								obj = null;
								flag2 = false;
							}
						}
						finally
						{
							if (flag)
							{
								this.m_foreignLock.Exit(false);
							}
						}
						return flag2;
					}
					num3 = num & this.m_mask;
					obj = Volatile.Read<IThreadPoolWorkItem>(ref this.m_array[num3]);
					if (obj != null)
					{
						goto Block_2;
					}
				}
				obj = null;
				return false;
				Block_2:
				this.m_array[num3] = null;
				return true;
			}

			// Token: 0x06001FCD RID: 8141 RVA: 0x000753DC File Offset: 0x000735DC
			public bool TrySteal(out IThreadPoolWorkItem obj, ref bool missedSteal)
			{
				return this.TrySteal(out obj, ref missedSteal, 0);
			}

			// Token: 0x06001FCE RID: 8142 RVA: 0x000753E8 File Offset: 0x000735E8
			private bool TrySteal(out IThreadPoolWorkItem obj, ref bool missedSteal, int millisecondsTimeout)
			{
				obj = null;
				while (this.m_headIndex < this.m_tailIndex)
				{
					bool flag = false;
					try
					{
						this.m_foreignLock.TryEnter(millisecondsTimeout, ref flag);
						if (flag)
						{
							int headIndex = this.m_headIndex;
							Interlocked.Exchange(ref this.m_headIndex, headIndex + 1);
							if (headIndex < this.m_tailIndex)
							{
								int num = headIndex & this.m_mask;
								obj = Volatile.Read<IThreadPoolWorkItem>(ref this.m_array[num]);
								if (obj == null)
								{
									continue;
								}
								this.m_array[num] = null;
								return true;
							}
							else
							{
								this.m_headIndex = headIndex;
								obj = null;
								missedSteal = true;
							}
						}
						else
						{
							missedSteal = true;
						}
					}
					finally
					{
						if (flag)
						{
							this.m_foreignLock.Exit(false);
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x06001FCF RID: 8143 RVA: 0x000754B0 File Offset: 0x000736B0
			public WorkStealingQueue()
			{
			}

			// Token: 0x040019FE RID: 6654
			private const int INITIAL_SIZE = 32;

			// Token: 0x040019FF RID: 6655
			internal volatile IThreadPoolWorkItem[] m_array = new IThreadPoolWorkItem[32];

			// Token: 0x04001A00 RID: 6656
			private volatile int m_mask = 31;

			// Token: 0x04001A01 RID: 6657
			private const int START_INDEX = 0;

			// Token: 0x04001A02 RID: 6658
			private volatile int m_headIndex;

			// Token: 0x04001A03 RID: 6659
			private volatile int m_tailIndex;

			// Token: 0x04001A04 RID: 6660
			private SpinLock m_foreignLock = new SpinLock(false);
		}

		// Token: 0x020002AF RID: 687
		internal class QueueSegment
		{
			// Token: 0x06001FD0 RID: 8144 RVA: 0x000754E0 File Offset: 0x000736E0
			private void GetIndexes(out int upper, out int lower)
			{
				int num = this.indexes;
				upper = (num >> 16) & 65535;
				lower = num & 65535;
			}

			// Token: 0x06001FD1 RID: 8145 RVA: 0x0007550C File Offset: 0x0007370C
			private bool CompareExchangeIndexes(ref int prevUpper, int newUpper, ref int prevLower, int newLower)
			{
				int num = (prevUpper << 16) | (prevLower & 65535);
				int num2 = (newUpper << 16) | (newLower & 65535);
				int num3 = Interlocked.CompareExchange(ref this.indexes, num2, num);
				prevUpper = (num3 >> 16) & 65535;
				prevLower = num3 & 65535;
				return num3 == num;
			}

			// Token: 0x06001FD2 RID: 8146 RVA: 0x0007555D File Offset: 0x0007375D
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			public QueueSegment()
			{
				this.nodes = new IThreadPoolWorkItem[256];
			}

			// Token: 0x06001FD3 RID: 8147 RVA: 0x00075578 File Offset: 0x00073778
			public bool IsUsedUp()
			{
				int num;
				int num2;
				this.GetIndexes(out num, out num2);
				return num == this.nodes.Length && num2 == this.nodes.Length;
			}

			// Token: 0x06001FD4 RID: 8148 RVA: 0x000755A8 File Offset: 0x000737A8
			public bool TryEnqueue(IThreadPoolWorkItem node)
			{
				int num;
				int num2;
				this.GetIndexes(out num, out num2);
				while (num != this.nodes.Length)
				{
					if (this.CompareExchangeIndexes(ref num, num + 1, ref num2, num2))
					{
						Volatile.Write<IThreadPoolWorkItem>(ref this.nodes[num], node);
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001FD5 RID: 8149 RVA: 0x000755F0 File Offset: 0x000737F0
			public bool TryDequeue(out IThreadPoolWorkItem node)
			{
				int num;
				int num2;
				this.GetIndexes(out num, out num2);
				while (num2 != num)
				{
					if (this.CompareExchangeIndexes(ref num, num, ref num2, num2 + 1))
					{
						SpinWait spinWait = default(SpinWait);
						for (;;)
						{
							IThreadPoolWorkItem threadPoolWorkItem;
							node = (threadPoolWorkItem = Volatile.Read<IThreadPoolWorkItem>(ref this.nodes[num2]));
							if (threadPoolWorkItem != null)
							{
								break;
							}
							spinWait.SpinOnce();
						}
						this.nodes[num2] = null;
						return true;
					}
				}
				node = null;
				return false;
			}

			// Token: 0x04001A05 RID: 6661
			internal readonly IThreadPoolWorkItem[] nodes;

			// Token: 0x04001A06 RID: 6662
			private const int QueueSegmentLength = 256;

			// Token: 0x04001A07 RID: 6663
			private volatile int indexes;

			// Token: 0x04001A08 RID: 6664
			public volatile ThreadPoolWorkQueue.QueueSegment Next;

			// Token: 0x04001A09 RID: 6665
			private const int SixteenBits = 65535;
		}
	}
}
