using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002FB RID: 763
	internal class ParallelLoopStateFlags
	{
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x0007BC58 File Offset: 0x00079E58
		internal int LoopStateFlags
		{
			get
			{
				return this._loopStateFlags;
			}
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x0007BC64 File Offset: 0x00079E64
		internal bool AtomicLoopStateUpdate(int newState, int illegalStates)
		{
			int num = 0;
			return this.AtomicLoopStateUpdate(newState, illegalStates, ref num);
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x0007BC80 File Offset: 0x00079E80
		internal bool AtomicLoopStateUpdate(int newState, int illegalStates, ref int oldState)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				oldState = this._loopStateFlags;
				if ((oldState & illegalStates) != 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this._loopStateFlags, oldState | newState, oldState) == oldState)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x0007BCC6 File Offset: 0x00079EC6
		internal void SetExceptional()
		{
			this.AtomicLoopStateUpdate(1, 0);
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x0007BCD1 File Offset: 0x00079ED1
		internal void Stop()
		{
			if (!this.AtomicLoopStateUpdate(4, 2))
			{
				throw new InvalidOperationException("Stop was called after Break was called.");
			}
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x0007BCE8 File Offset: 0x00079EE8
		internal bool Cancel()
		{
			return this.AtomicLoopStateUpdate(8, 0);
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x000025BE File Offset: 0x000007BE
		public ParallelLoopStateFlags()
		{
		}

		// Token: 0x04001B03 RID: 6915
		internal const int ParallelLoopStateNone = 0;

		// Token: 0x04001B04 RID: 6916
		internal const int ParallelLoopStateExceptional = 1;

		// Token: 0x04001B05 RID: 6917
		internal const int ParallelLoopStateBroken = 2;

		// Token: 0x04001B06 RID: 6918
		internal const int ParallelLoopStateStopped = 4;

		// Token: 0x04001B07 RID: 6919
		internal const int ParallelLoopStateCanceled = 8;

		// Token: 0x04001B08 RID: 6920
		private volatile int _loopStateFlags;
	}
}
