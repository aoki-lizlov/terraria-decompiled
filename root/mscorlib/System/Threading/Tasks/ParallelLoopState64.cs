using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002FA RID: 762
	internal class ParallelLoopState64 : ParallelLoopState
	{
		// Token: 0x06002201 RID: 8705 RVA: 0x0007BC04 File Offset: 0x00079E04
		internal ParallelLoopState64(ParallelLoopStateFlags64 sharedParallelStateFlags)
			: base(sharedParallelStateFlags)
		{
			this._sharedParallelStateFlags = sharedParallelStateFlags;
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x0007BC14 File Offset: 0x00079E14
		// (set) Token: 0x06002203 RID: 8707 RVA: 0x0007BC1C File Offset: 0x00079E1C
		internal long CurrentIteration
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

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x0007BC25 File Offset: 0x00079E25
		internal override bool InternalShouldExitCurrentIteration
		{
			get
			{
				return this._sharedParallelStateFlags.ShouldExitLoop(this.CurrentIteration);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06002205 RID: 8709 RVA: 0x0007BC38 File Offset: 0x00079E38
		internal override long? InternalLowestBreakIteration
		{
			get
			{
				return this._sharedParallelStateFlags.NullableLowestBreakIteration;
			}
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x0007BC45 File Offset: 0x00079E45
		internal override void InternalBreak()
		{
			ParallelLoopState.Break(this.CurrentIteration, this._sharedParallelStateFlags);
		}

		// Token: 0x04001B01 RID: 6913
		private readonly ParallelLoopStateFlags64 _sharedParallelStateFlags;

		// Token: 0x04001B02 RID: 6914
		private long _currentIteration;
	}
}
