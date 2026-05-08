using System;

namespace System.Threading
{
	// Token: 0x020002C0 RID: 704
	internal class LockQueue
	{
		// Token: 0x06002084 RID: 8324 RVA: 0x00076C89 File Offset: 0x00074E89
		public LockQueue(ReaderWriterLock rwlock)
		{
			this.rwlock = rwlock;
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x00076C98 File Offset: 0x00074E98
		public bool Wait(int timeout)
		{
			bool flag = false;
			bool flag3;
			try
			{
				lock (this)
				{
					this.lockCount++;
					Monitor.Exit(this.rwlock);
					flag = true;
					flag3 = Monitor.Wait(this, timeout);
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Enter(this.rwlock);
					this.lockCount--;
				}
			}
			return flag3;
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x00076D1C File Offset: 0x00074F1C
		public bool IsEmpty
		{
			get
			{
				bool flag2;
				lock (this)
				{
					flag2 = this.lockCount == 0;
				}
				return flag2;
			}
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x00076D5C File Offset: 0x00074F5C
		public void Pulse()
		{
			lock (this)
			{
				Monitor.Pulse(this);
			}
		}

		// Token: 0x04001A3B RID: 6715
		private ReaderWriterLock rwlock;

		// Token: 0x04001A3C RID: 6716
		private int lockCount;
	}
}
