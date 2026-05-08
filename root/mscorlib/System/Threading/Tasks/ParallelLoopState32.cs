using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002F9 RID: 761
	internal class ParallelLoopState32 : ParallelLoopState
	{
		// Token: 0x060021FB RID: 8699 RVA: 0x0007BBB0 File Offset: 0x00079DB0
		internal ParallelLoopState32(ParallelLoopStateFlags32 sharedParallelStateFlags)
			: base(sharedParallelStateFlags)
		{
			this._sharedParallelStateFlags = sharedParallelStateFlags;
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060021FC RID: 8700 RVA: 0x0007BBC0 File Offset: 0x00079DC0
		// (set) Token: 0x060021FD RID: 8701 RVA: 0x0007BBC8 File Offset: 0x00079DC8
		internal int CurrentIteration
		{
			get
			{
				return this._currentIteration;
			}
			set
			{
				this._currentIteration = value;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x0007BBD1 File Offset: 0x00079DD1
		internal override bool InternalShouldExitCurrentIteration
		{
			get
			{
				return this._sharedParallelStateFlags.ShouldExitLoop(this.CurrentIteration);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x0007BBE4 File Offset: 0x00079DE4
		internal override long? InternalLowestBreakIteration
		{
			get
			{
				return this._sharedParallelStateFlags.NullableLowestBreakIteration;
			}
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x0007BBF1 File Offset: 0x00079DF1
		internal override void InternalBreak()
		{
			ParallelLoopState.Break(this.CurrentIteration, this._sharedParallelStateFlags);
		}

		// Token: 0x04001AFF RID: 6911
		private readonly ParallelLoopStateFlags32 _sharedParallelStateFlags;

		// Token: 0x04001B00 RID: 6912
		private int _currentIteration;
	}
}
