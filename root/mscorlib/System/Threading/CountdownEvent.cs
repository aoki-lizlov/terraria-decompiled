using System;
using System.Diagnostics;

namespace System.Threading
{
	// Token: 0x02000273 RID: 627
	[DebuggerDisplay("Initial Count={InitialCount}, Current Count={CurrentCount}")]
	public class CountdownEvent : IDisposable
	{
		// Token: 0x06001D87 RID: 7559 RVA: 0x0006F2EF File Offset: 0x0006D4EF
		public CountdownEvent(int initialCount)
		{
			if (initialCount < 0)
			{
				throw new ArgumentOutOfRangeException("initialCount");
			}
			this._initialCount = initialCount;
			this._currentCount = initialCount;
			this._event = new ManualResetEventSlim();
			if (initialCount == 0)
			{
				this._event.Set();
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001D88 RID: 7560 RVA: 0x0006F330 File Offset: 0x0006D530
		public int CurrentCount
		{
			get
			{
				int currentCount = this._currentCount;
				if (currentCount >= 0)
				{
					return currentCount;
				}
				return 0;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x0006F34D File Offset: 0x0006D54D
		public int InitialCount
		{
			get
			{
				return this._initialCount;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001D8A RID: 7562 RVA: 0x0006F355 File Offset: 0x0006D555
		public bool IsSet
		{
			get
			{
				return this._currentCount <= 0;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x0006F365 File Offset: 0x0006D565
		public WaitHandle WaitHandle
		{
			get
			{
				this.ThrowIfDisposed();
				return this._event.WaitHandle;
			}
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x0006F378 File Offset: 0x0006D578
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x0006F387 File Offset: 0x0006D587
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._event.Dispose();
				this._disposed = true;
			}
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x0006F3A0 File Offset: 0x0006D5A0
		public bool Signal()
		{
			this.ThrowIfDisposed();
			if (this._currentCount <= 0)
			{
				throw new InvalidOperationException("Invalid attempt made to decrement the event's count below zero.");
			}
			int num = Interlocked.Decrement(ref this._currentCount);
			if (num == 0)
			{
				this._event.Set();
				return true;
			}
			if (num < 0)
			{
				throw new InvalidOperationException("Invalid attempt made to decrement the event's count below zero.");
			}
			return false;
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0006F3F8 File Offset: 0x0006D5F8
		public bool Signal(int signalCount)
		{
			if (signalCount <= 0)
			{
				throw new ArgumentOutOfRangeException("signalCount");
			}
			this.ThrowIfDisposed();
			SpinWait spinWait = default(SpinWait);
			int currentCount;
			for (;;)
			{
				currentCount = this._currentCount;
				if (currentCount < signalCount)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this._currentCount, currentCount - signalCount, currentCount) == currentCount)
				{
					goto IL_0050;
				}
				spinWait.SpinOnce();
			}
			throw new InvalidOperationException("Invalid attempt made to decrement the event's count below zero.");
			IL_0050:
			if (currentCount == signalCount)
			{
				this._event.Set();
				return true;
			}
			return false;
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x0006F467 File Offset: 0x0006D667
		public void AddCount()
		{
			this.AddCount(1);
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x0006F470 File Offset: 0x0006D670
		public bool TryAddCount()
		{
			return this.TryAddCount(1);
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0006F479 File Offset: 0x0006D679
		public void AddCount(int signalCount)
		{
			if (!this.TryAddCount(signalCount))
			{
				throw new InvalidOperationException("The event is already signaled and cannot be incremented.");
			}
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x0006F490 File Offset: 0x0006D690
		public bool TryAddCount(int signalCount)
		{
			if (signalCount <= 0)
			{
				throw new ArgumentOutOfRangeException("signalCount");
			}
			this.ThrowIfDisposed();
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int currentCount = this._currentCount;
				if (currentCount <= 0)
				{
					break;
				}
				if (currentCount > 2147483647 - signalCount)
				{
					goto Block_3;
				}
				if (Interlocked.CompareExchange(ref this._currentCount, currentCount + signalCount, currentCount) == currentCount)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
			Block_3:
			throw new InvalidOperationException("The increment operation would cause the CurrentCount to overflow.");
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x0006F4FA File Offset: 0x0006D6FA
		public void Reset()
		{
			this.Reset(this._initialCount);
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x0006F508 File Offset: 0x0006D708
		public void Reset(int count)
		{
			this.ThrowIfDisposed();
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this._currentCount = count;
			this._initialCount = count;
			if (count == 0)
			{
				this._event.Set();
				return;
			}
			this._event.Reset();
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x0006F554 File Offset: 0x0006D754
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x0006F572 File Offset: 0x0006D772
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x0006F580 File Offset: 0x0006D780
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, default(CancellationToken));
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x0006F5C0 File Offset: 0x0006D7C0
		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, cancellationToken);
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x0006F5F8 File Offset: 0x0006D7F8
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x0006F618 File Offset: 0x0006D818
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			this.ThrowIfDisposed();
			cancellationToken.ThrowIfCancellationRequested();
			bool flag = this.IsSet;
			if (!flag)
			{
				flag = this._event.Wait(millisecondsTimeout, cancellationToken);
			}
			return flag;
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x0006F65A File Offset: 0x0006D85A
		private void ThrowIfDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("CountdownEvent");
			}
		}

		// Token: 0x04001937 RID: 6455
		private int _initialCount;

		// Token: 0x04001938 RID: 6456
		private volatile int _currentCount;

		// Token: 0x04001939 RID: 6457
		private ManualResetEventSlim _event;

		// Token: 0x0400193A RID: 6458
		private volatile bool _disposed;
	}
}
