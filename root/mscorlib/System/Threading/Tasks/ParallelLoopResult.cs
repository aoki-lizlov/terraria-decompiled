using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002FE RID: 766
	public struct ParallelLoopResult
	{
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x0007BE87 File Offset: 0x0007A087
		public bool IsCompleted
		{
			get
			{
				return this._completed;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06002219 RID: 8729 RVA: 0x0007BE8F File Offset: 0x0007A08F
		public long? LowestBreakIteration
		{
			get
			{
				return this._lowestBreakIteration;
			}
		}

		// Token: 0x04001B0B RID: 6923
		internal bool _completed;

		// Token: 0x04001B0C RID: 6924
		internal long? _lowestBreakIteration;
	}
}
