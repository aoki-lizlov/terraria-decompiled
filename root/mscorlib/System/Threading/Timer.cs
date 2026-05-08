using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Threading
{
	// Token: 0x020002C7 RID: 711
	[ComVisible(true)]
	public sealed class Timer : MarshalByRefObject, IDisposable, IAsyncDisposable
	{
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x00077D4F File Offset: 0x00075F4F
		private static Timer.Scheduler scheduler
		{
			get
			{
				return Timer.Scheduler.Instance;
			}
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x00077D56 File Offset: 0x00075F56
		public Timer(TimerCallback callback, object state, int dueTime, int period)
		{
			this.Init(callback, state, (long)dueTime, (long)period);
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x00077D6B File Offset: 0x00075F6B
		public Timer(TimerCallback callback, object state, long dueTime, long period)
		{
			this.Init(callback, state, dueTime, period);
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x00077D7E File Offset: 0x00075F7E
		public Timer(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period)
		{
			this.Init(callback, state, (long)dueTime.TotalMilliseconds, (long)period.TotalMilliseconds);
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x00077DA0 File Offset: 0x00075FA0
		[CLSCompliant(false)]
		public Timer(TimerCallback callback, object state, uint dueTime, uint period)
		{
			long num = (long)((dueTime == uint.MaxValue) ? ulong.MaxValue : ((ulong)dueTime));
			long num2 = (long)((period == uint.MaxValue) ? ulong.MaxValue : ((ulong)period));
			this.Init(callback, state, num, num2);
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x00077DD5 File Offset: 0x00075FD5
		public Timer(TimerCallback callback)
		{
			this.Init(callback, this, -1L, -1L);
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x00077DE9 File Offset: 0x00075FE9
		private void Init(TimerCallback callback, object state, long dueTime, long period)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this.callback = callback;
			this.state = state;
			this.is_dead = false;
			this.is_added = false;
			this.Change(dueTime, period, true);
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x00077E20 File Offset: 0x00076020
		public bool Change(int dueTime, int period)
		{
			return this.Change((long)dueTime, (long)period, false);
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x00077E2D File Offset: 0x0007602D
		public bool Change(TimeSpan dueTime, TimeSpan period)
		{
			return this.Change((long)dueTime.TotalMilliseconds, (long)period.TotalMilliseconds, false);
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x00077E48 File Offset: 0x00076048
		[CLSCompliant(false)]
		public bool Change(uint dueTime, uint period)
		{
			long num = (long)((dueTime == uint.MaxValue) ? ulong.MaxValue : ((ulong)dueTime));
			long num2 = (long)((period == uint.MaxValue) ? ulong.MaxValue : ((ulong)period));
			return this.Change(num, num2, false);
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x00077E74 File Offset: 0x00076074
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			Timer.scheduler.Remove(this);
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x00077E91 File Offset: 0x00076091
		public bool Change(long dueTime, long period)
		{
			return this.Change(dueTime, period, false);
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x00077E9C File Offset: 0x0007609C
		private bool Change(long dueTime, long period, bool first)
		{
			if (dueTime > (long)((ulong)(-2)))
			{
				throw new ArgumentOutOfRangeException("dueTime", "Due time too large");
			}
			if (period > (long)((ulong)(-2)))
			{
				throw new ArgumentOutOfRangeException("period", "Period too large");
			}
			if (dueTime < -1L)
			{
				throw new ArgumentOutOfRangeException("dueTime");
			}
			if (period < -1L)
			{
				throw new ArgumentOutOfRangeException("period");
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a disposed object."));
			}
			this.due_time_ms = dueTime;
			this.period_ms = period;
			long num;
			if (dueTime == 0L)
			{
				num = 0L;
			}
			else if (dueTime < 0L)
			{
				num = long.MaxValue;
				if (first)
				{
					this.next_run = num;
					return true;
				}
			}
			else
			{
				num = dueTime * 10000L + Timer.GetTimeMonotonic();
			}
			Timer.scheduler.Change(this, num);
			return true;
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x00077F5B File Offset: 0x0007615B
		public bool Dispose(WaitHandle notifyObject)
		{
			if (notifyObject == null)
			{
				throw new ArgumentNullException("notifyObject");
			}
			this.Dispose();
			NativeEventCalls.SetEvent(notifyObject.SafeWaitHandle);
			return true;
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x00077F7E File Offset: 0x0007617E
		public ValueTask DisposeAsync()
		{
			this.Dispose();
			return new ValueTask(Task.FromResult<object>(null));
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x00004088 File Offset: 0x00002288
		internal void KeepRootedWhileScheduled()
		{
		}

		// Token: 0x060020E5 RID: 8421
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetTimeMonotonic();

		// Token: 0x04001A79 RID: 6777
		private TimerCallback callback;

		// Token: 0x04001A7A RID: 6778
		private object state;

		// Token: 0x04001A7B RID: 6779
		private long due_time_ms;

		// Token: 0x04001A7C RID: 6780
		private long period_ms;

		// Token: 0x04001A7D RID: 6781
		private long next_run;

		// Token: 0x04001A7E RID: 6782
		private bool disposed;

		// Token: 0x04001A7F RID: 6783
		private bool is_dead;

		// Token: 0x04001A80 RID: 6784
		private bool is_added;

		// Token: 0x04001A81 RID: 6785
		private const long MaxValue = 4294967294L;

		// Token: 0x020002C8 RID: 712
		private struct TimerComparer : IComparer, IComparer<Timer>
		{
			// Token: 0x060020E6 RID: 8422 RVA: 0x00077F94 File Offset: 0x00076194
			int IComparer.Compare(object x, object y)
			{
				if (x == y)
				{
					return 0;
				}
				Timer timer = x as Timer;
				if (timer == null)
				{
					return -1;
				}
				Timer timer2 = y as Timer;
				if (timer2 == null)
				{
					return 1;
				}
				return this.Compare(timer, timer2);
			}

			// Token: 0x060020E7 RID: 8423 RVA: 0x00077FC7 File Offset: 0x000761C7
			public int Compare(Timer tx, Timer ty)
			{
				return Math.Sign(tx.next_run - ty.next_run);
			}
		}

		// Token: 0x020002C9 RID: 713
		private sealed class Scheduler
		{
			// Token: 0x060020E8 RID: 8424 RVA: 0x00077FDB File Offset: 0x000761DB
			private void InitScheduler()
			{
				this.changed = new ManualResetEvent(false);
				new Thread(new ThreadStart(this.SchedulerThread))
				{
					IsBackground = true
				}.Start();
			}

			// Token: 0x060020E9 RID: 8425 RVA: 0x00078006 File Offset: 0x00076206
			private void WakeupScheduler()
			{
				this.changed.Set();
			}

			// Token: 0x060020EA RID: 8426 RVA: 0x00078014 File Offset: 0x00076214
			private void SchedulerThread()
			{
				Thread.CurrentThread.Name = "Timer-Scheduler";
				for (;;)
				{
					int num = -1;
					lock (this)
					{
						this.changed.Reset();
						num = this.RunSchedulerLoop();
					}
					this.changed.WaitOne(num);
				}
			}

			// Token: 0x170003DE RID: 990
			// (get) Token: 0x060020EB RID: 8427 RVA: 0x0007807C File Offset: 0x0007627C
			public static Timer.Scheduler Instance
			{
				get
				{
					return Timer.Scheduler.instance;
				}
			}

			// Token: 0x060020EC RID: 8428 RVA: 0x00078083 File Offset: 0x00076283
			private Scheduler()
			{
				this.list = new List<Timer>(1024);
				this.InitScheduler();
			}

			// Token: 0x060020ED RID: 8429 RVA: 0x000780BC File Offset: 0x000762BC
			public void Remove(Timer timer)
			{
				lock (this)
				{
					this.InternalRemove(timer);
				}
			}

			// Token: 0x060020EE RID: 8430 RVA: 0x000780F8 File Offset: 0x000762F8
			public void Change(Timer timer, long new_next_run)
			{
				if (timer.is_dead)
				{
					timer.is_dead = false;
				}
				bool flag = false;
				lock (this)
				{
					this.needReSort = true;
					if (!timer.is_added)
					{
						timer.next_run = new_next_run;
						this.Add(timer);
						flag = this.current_next_run > new_next_run;
					}
					else
					{
						if (new_next_run == 9223372036854775807L)
						{
							timer.next_run = new_next_run;
							this.InternalRemove(timer);
							return;
						}
						if (!timer.disposed)
						{
							timer.next_run = new_next_run;
							flag = this.current_next_run > new_next_run;
						}
					}
				}
				if (flag)
				{
					this.WakeupScheduler();
				}
			}

			// Token: 0x060020EF RID: 8431 RVA: 0x000781A8 File Offset: 0x000763A8
			private void Add(Timer timer)
			{
				timer.is_added = true;
				this.needReSort = true;
				this.list.Add(timer);
				if (this.list.Count == 1)
				{
					this.WakeupScheduler();
				}
			}

			// Token: 0x060020F0 RID: 8432 RVA: 0x000781DA File Offset: 0x000763DA
			private void InternalRemove(Timer timer)
			{
				timer.is_dead = true;
				this.needReSort = true;
			}

			// Token: 0x060020F1 RID: 8433 RVA: 0x000781EC File Offset: 0x000763EC
			private static void TimerCB(object o)
			{
				Timer timer = (Timer)o;
				timer.callback(timer.state);
			}

			// Token: 0x060020F2 RID: 8434 RVA: 0x00078214 File Offset: 0x00076414
			private void FireTimer(Timer timer)
			{
				long period_ms = timer.period_ms;
				long due_time_ms = timer.due_time_ms;
				if (period_ms == -1L || ((period_ms == 0L || period_ms == -1L) && due_time_ms != -1L))
				{
					timer.next_run = long.MaxValue;
					timer.is_dead = true;
				}
				else
				{
					timer.next_run = Timer.GetTimeMonotonic() + 10000L * timer.period_ms;
					timer.is_dead = false;
				}
				ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(Timer.Scheduler.TimerCB), timer);
			}

			// Token: 0x060020F3 RID: 8435 RVA: 0x00078298 File Offset: 0x00076498
			private int RunSchedulerLoop()
			{
				long timeMonotonic = Timer.GetTimeMonotonic();
				Timer.TimerComparer timerComparer = default(Timer.TimerComparer);
				if (this.needReSort)
				{
					this.list.Sort(timerComparer);
					this.needReSort = false;
				}
				long num = long.MaxValue;
				for (int i = 0; i < this.list.Count; i++)
				{
					Timer timer = this.list[i];
					if (!timer.is_dead)
					{
						if (timer.next_run <= timeMonotonic)
						{
							this.FireTimer(timer);
						}
						num = Math.Min(num, timer.next_run);
						if (timer.next_run > timeMonotonic && timer.next_run < 9223372036854775807L)
						{
							timer.is_dead = false;
						}
					}
				}
				for (int i = 0; i < this.list.Count; i++)
				{
					Timer timer2 = this.list[i];
					if (timer2.is_dead)
					{
						timer2.is_added = false;
						this.needReSort = true;
						this.list[i] = this.list[this.list.Count - 1];
						i--;
						this.list.RemoveAt(this.list.Count - 1);
						if (this.list.Count == 0)
						{
							break;
						}
					}
				}
				if (this.needReSort)
				{
					this.list.Sort(timerComparer);
					this.needReSort = false;
				}
				int num2 = -1;
				this.current_next_run = num;
				if (num != 9223372036854775807L)
				{
					long num3 = (num - Timer.GetTimeMonotonic()) / 10000L;
					if (num3 > 2147483647L)
					{
						num2 = 2147483646;
					}
					else
					{
						num2 = (int)num3;
						if (num2 < 0)
						{
							num2 = 0;
						}
					}
				}
				return num2;
			}

			// Token: 0x060020F4 RID: 8436 RVA: 0x0007844E File Offset: 0x0007664E
			// Note: this type is marked as 'beforefieldinit'.
			static Scheduler()
			{
			}

			// Token: 0x04001A82 RID: 6786
			private static readonly Timer.Scheduler instance = new Timer.Scheduler();

			// Token: 0x04001A83 RID: 6787
			private volatile bool needReSort = true;

			// Token: 0x04001A84 RID: 6788
			private List<Timer> list;

			// Token: 0x04001A85 RID: 6789
			private long current_next_run = long.MaxValue;

			// Token: 0x04001A86 RID: 6790
			private ManualResetEvent changed;
		}
	}
}
