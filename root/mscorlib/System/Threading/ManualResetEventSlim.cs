using System;
using System.Diagnostics;

namespace System.Threading
{
	// Token: 0x02000276 RID: 630
	[DebuggerDisplay("Set = {IsSet}")]
	public class ManualResetEventSlim : IDisposable
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x0006F835 File Offset: 0x0006DA35
		public WaitHandle WaitHandle
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.m_eventObj == null)
				{
					this.LazyInitializeEvent();
				}
				return this.m_eventObj;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x0006F856 File Offset: 0x0006DA56
		// (set) Token: 0x06001DB6 RID: 7606 RVA: 0x0006F86D File Offset: 0x0006DA6D
		public bool IsSet
		{
			get
			{
				return ManualResetEventSlim.ExtractStatePortion(this.m_combinedState, int.MinValue) != 0;
			}
			private set
			{
				this.UpdateStateAtomically((value ? 1 : 0) << 31, int.MinValue);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x0006F884 File Offset: 0x0006DA84
		// (set) Token: 0x06001DB8 RID: 7608 RVA: 0x0006F89A File Offset: 0x0006DA9A
		public int SpinCount
		{
			get
			{
				return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 1073217536, 19);
			}
			private set
			{
				this.m_combinedState = (this.m_combinedState & -1073217537) | (value << 19);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x0006F8B7 File Offset: 0x0006DAB7
		// (set) Token: 0x06001DBA RID: 7610 RVA: 0x0006F8CC File Offset: 0x0006DACC
		private int Waiters
		{
			get
			{
				return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 524287, 0);
			}
			set
			{
				if (value >= 524287)
				{
					throw new InvalidOperationException(string.Format("There are too many threads currently waiting on the event. A maximum of {0} waiting threads are supported.", 524287));
				}
				this.UpdateStateAtomically(value, 524287);
			}
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x0006F8FC File Offset: 0x0006DAFC
		public ManualResetEventSlim()
			: this(false)
		{
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x0006F905 File Offset: 0x0006DB05
		public ManualResetEventSlim(bool initialState)
		{
			this.Initialize(initialState, SpinWait.SpinCountforSpinBeforeWait);
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x0006F91C File Offset: 0x0006DB1C
		public ManualResetEventSlim(bool initialState, int spinCount)
		{
			if (spinCount < 0)
			{
				throw new ArgumentOutOfRangeException("spinCount");
			}
			if (spinCount > 2047)
			{
				throw new ArgumentOutOfRangeException("spinCount", string.Format("The spinCount argument must be in the range 0 to {0}, inclusive.", 2047));
			}
			this.Initialize(initialState, spinCount);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x0006F96D File Offset: 0x0006DB6D
		private void Initialize(bool initialState, int spinCount)
		{
			this.m_combinedState = (initialState ? int.MinValue : 0);
			this.SpinCount = (PlatformHelper.IsSingleProcessor ? 1 : spinCount);
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x0006F994 File Offset: 0x0006DB94
		private void EnsureLockObjectCreated()
		{
			if (this.m_lock != null)
			{
				return;
			}
			object obj = new object();
			Interlocked.CompareExchange(ref this.m_lock, obj, null);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x0006F9C0 File Offset: 0x0006DBC0
		private bool LazyInitializeEvent()
		{
			bool isSet = this.IsSet;
			ManualResetEvent manualResetEvent = new ManualResetEvent(isSet);
			if (Interlocked.CompareExchange<ManualResetEvent>(ref this.m_eventObj, manualResetEvent, null) != null)
			{
				manualResetEvent.Dispose();
				return false;
			}
			if (this.IsSet != isSet)
			{
				ManualResetEvent manualResetEvent2 = manualResetEvent;
				lock (manualResetEvent2)
				{
					if (this.m_eventObj == manualResetEvent)
					{
						manualResetEvent.Set();
					}
				}
			}
			return true;
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x0006FA38 File Offset: 0x0006DC38
		public void Set()
		{
			this.Set(false);
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x0006FA44 File Offset: 0x0006DC44
		private void Set(bool duringCancellation)
		{
			this.IsSet = true;
			if (this.Waiters > 0)
			{
				object @lock = this.m_lock;
				lock (@lock)
				{
					Monitor.PulseAll(this.m_lock);
				}
			}
			ManualResetEvent eventObj = this.m_eventObj;
			if (eventObj != null && !duringCancellation)
			{
				ManualResetEvent manualResetEvent = eventObj;
				lock (manualResetEvent)
				{
					if (this.m_eventObj != null)
					{
						this.m_eventObj.Set();
					}
				}
			}
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x0006FAE8 File Offset: 0x0006DCE8
		public void Reset()
		{
			this.ThrowIfDisposed();
			if (this.m_eventObj != null)
			{
				this.m_eventObj.Reset();
			}
			this.IsSet = false;
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x0006FB10 File Offset: 0x0006DD10
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x0006FB2E File Offset: 0x0006DD2E
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x0006FB3C File Offset: 0x0006DD3C
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, default(CancellationToken));
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x0006FB7C File Offset: 0x0006DD7C
		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, cancellationToken);
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x0006FBB4 File Offset: 0x0006DDB4
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x0006FBD4 File Offset: 0x0006DDD4
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			cancellationToken.ThrowIfCancellationRequested();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (!this.IsSet)
			{
				if (millisecondsTimeout == 0)
				{
					return false;
				}
				uint num = 0U;
				bool flag = false;
				int num2 = millisecondsTimeout;
				if (millisecondsTimeout != -1)
				{
					num = TimeoutHelper.GetTime();
					flag = true;
				}
				int spinCount = this.SpinCount;
				SpinWait spinWait = default(SpinWait);
				while (spinWait.Count < spinCount)
				{
					spinWait.SpinOnce(40);
					if (this.IsSet)
					{
						return true;
					}
					if (spinWait.Count >= 100 && spinWait.Count % 10 == 0)
					{
						cancellationToken.ThrowIfCancellationRequested();
					}
				}
				this.EnsureLockObjectCreated();
				using (cancellationToken.InternalRegisterWithoutEC(ManualResetEventSlim.s_cancellationTokenCallback, this))
				{
					object @lock = this.m_lock;
					lock (@lock)
					{
						while (!this.IsSet)
						{
							cancellationToken.ThrowIfCancellationRequested();
							if (flag)
							{
								num2 = TimeoutHelper.UpdateTimeOut(num, millisecondsTimeout);
								if (num2 <= 0)
								{
									return false;
								}
							}
							this.Waiters++;
							if (this.IsSet)
							{
								int waiters = this.Waiters;
								this.Waiters = waiters - 1;
								return true;
							}
							try
							{
								if (!Monitor.Wait(this.m_lock, num2))
								{
									return false;
								}
							}
							finally
							{
								this.Waiters--;
							}
						}
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x0006FD58 File Offset: 0x0006DF58
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x0006FD68 File Offset: 0x0006DF68
		protected virtual void Dispose(bool disposing)
		{
			if ((this.m_combinedState & 1073741824) != 0)
			{
				return;
			}
			this.m_combinedState |= 1073741824;
			if (disposing)
			{
				ManualResetEvent eventObj = this.m_eventObj;
				if (eventObj != null)
				{
					ManualResetEvent manualResetEvent = eventObj;
					lock (manualResetEvent)
					{
						eventObj.Dispose();
						this.m_eventObj = null;
					}
				}
			}
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x0006FDE4 File Offset: 0x0006DFE4
		private void ThrowIfDisposed()
		{
			if ((this.m_combinedState & 1073741824) != 0)
			{
				throw new ObjectDisposedException("The event has been disposed.");
			}
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x0006FE04 File Offset: 0x0006E004
		private static void CancellationTokenCallback(object obj)
		{
			ManualResetEventSlim manualResetEventSlim = obj as ManualResetEventSlim;
			object @lock = manualResetEventSlim.m_lock;
			lock (@lock)
			{
				Monitor.PulseAll(manualResetEventSlim.m_lock);
			}
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x0006FE54 File Offset: 0x0006E054
		private void UpdateStateAtomically(int newBits, int updateBitsMask)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int combinedState = this.m_combinedState;
				int num = (combinedState & ~updateBitsMask) | newBits;
				if (Interlocked.CompareExchange(ref this.m_combinedState, num, combinedState) == combinedState)
				{
					break;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x0006FE92 File Offset: 0x0006E092
		private static int ExtractStatePortionAndShiftRight(int state, int mask, int rightBitShiftCount)
		{
			return (int)((uint)(state & mask) >> rightBitShiftCount);
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0006FE9C File Offset: 0x0006E09C
		private static int ExtractStatePortion(int state, int mask)
		{
			return state & mask;
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0006FEA1 File Offset: 0x0006E0A1
		// Note: this type is marked as 'beforefieldinit'.
		static ManualResetEventSlim()
		{
		}

		// Token: 0x0400193E RID: 6462
		private const int DEFAULT_SPIN_SP = 1;

		// Token: 0x0400193F RID: 6463
		private volatile object m_lock;

		// Token: 0x04001940 RID: 6464
		private volatile ManualResetEvent m_eventObj;

		// Token: 0x04001941 RID: 6465
		private volatile int m_combinedState;

		// Token: 0x04001942 RID: 6466
		private const int SignalledState_BitMask = -2147483648;

		// Token: 0x04001943 RID: 6467
		private const int SignalledState_ShiftCount = 31;

		// Token: 0x04001944 RID: 6468
		private const int Dispose_BitMask = 1073741824;

		// Token: 0x04001945 RID: 6469
		private const int SpinCountState_BitMask = 1073217536;

		// Token: 0x04001946 RID: 6470
		private const int SpinCountState_ShiftCount = 19;

		// Token: 0x04001947 RID: 6471
		private const int SpinCountState_MaxValue = 2047;

		// Token: 0x04001948 RID: 6472
		private const int NumWaitersState_BitMask = 524287;

		// Token: 0x04001949 RID: 6473
		private const int NumWaitersState_ShiftCount = 0;

		// Token: 0x0400194A RID: 6474
		private const int NumWaitersState_MaxValue = 524287;

		// Token: 0x0400194B RID: 6475
		private static Action<object> s_cancellationTokenCallback = new Action<object>(ManualResetEventSlim.CancellationTokenCallback);
	}
}
