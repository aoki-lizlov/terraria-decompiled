using System;
using System.Diagnostics;

namespace System.Threading.Tasks
{
	// Token: 0x020002F8 RID: 760
	[DebuggerDisplay("ShouldExitCurrentIteration = {ShouldExitCurrentIteration}")]
	public class ParallelLoopState
	{
		// Token: 0x060021EF RID: 8687 RVA: 0x0007BA80 File Offset: 0x00079C80
		internal ParallelLoopState(ParallelLoopStateFlags fbase)
		{
			this._flagsBase = fbase;
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x0007BA8F File Offset: 0x00079C8F
		internal virtual bool InternalShouldExitCurrentIteration
		{
			get
			{
				throw new NotSupportedException("This method is not supported.");
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x0007BA9B File Offset: 0x00079C9B
		public bool ShouldExitCurrentIteration
		{
			get
			{
				return this.InternalShouldExitCurrentIteration;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x0007BAA3 File Offset: 0x00079CA3
		public bool IsStopped
		{
			get
			{
				return (this._flagsBase.LoopStateFlags & 4) != 0;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x0007BAB5 File Offset: 0x00079CB5
		public bool IsExceptional
		{
			get
			{
				return (this._flagsBase.LoopStateFlags & 1) != 0;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x0007BA8F File Offset: 0x00079C8F
		internal virtual long? InternalLowestBreakIteration
		{
			get
			{
				throw new NotSupportedException("This method is not supported.");
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x0007BAC7 File Offset: 0x00079CC7
		public long? LowestBreakIteration
		{
			get
			{
				return this.InternalLowestBreakIteration;
			}
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x0007BACF File Offset: 0x00079CCF
		public void Stop()
		{
			this._flagsBase.Stop();
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x0007BA8F File Offset: 0x00079C8F
		internal virtual void InternalBreak()
		{
			throw new NotSupportedException("This method is not supported.");
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0007BADC File Offset: 0x00079CDC
		public void Break()
		{
			this.InternalBreak();
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0007BAE4 File Offset: 0x00079CE4
		internal static void Break(int iteration, ParallelLoopStateFlags32 pflags)
		{
			int num = 0;
			if (pflags.AtomicLoopStateUpdate(2, 13, ref num))
			{
				int num2 = pflags._lowestBreakIteration;
				if (iteration < num2)
				{
					SpinWait spinWait = default(SpinWait);
					while (Interlocked.CompareExchange(ref pflags._lowestBreakIteration, iteration, num2) != num2)
					{
						spinWait.SpinOnce();
						num2 = pflags._lowestBreakIteration;
						if (iteration > num2)
						{
							break;
						}
					}
				}
				return;
			}
			if ((num & 4) != 0)
			{
				throw new InvalidOperationException("Break was called after Stop was called.");
			}
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0007BB4C File Offset: 0x00079D4C
		internal static void Break(long iteration, ParallelLoopStateFlags64 pflags)
		{
			int num = 0;
			if (pflags.AtomicLoopStateUpdate(2, 13, ref num))
			{
				long num2 = pflags.LowestBreakIteration;
				if (iteration < num2)
				{
					SpinWait spinWait = default(SpinWait);
					while (Interlocked.CompareExchange(ref pflags._lowestBreakIteration, iteration, num2) != num2)
					{
						spinWait.SpinOnce();
						num2 = pflags.LowestBreakIteration;
						if (iteration > num2)
						{
							break;
						}
					}
				}
				return;
			}
			if ((num & 4) != 0)
			{
				throw new InvalidOperationException("Break was called after Stop was called.");
			}
		}

		// Token: 0x04001AFE RID: 6910
		private readonly ParallelLoopStateFlags _flagsBase;
	}
}
