using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002FD RID: 765
	internal class ParallelLoopStateFlags64 : ParallelLoopStateFlags
	{
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x0007BDB1 File Offset: 0x00079FB1
		internal long LowestBreakIteration
		{
			get
			{
				if (IntPtr.Size >= 8)
				{
					return this._lowestBreakIteration;
				}
				return Interlocked.Read(ref this._lowestBreakIteration);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x0007BDD0 File Offset: 0x00079FD0
		internal long? NullableLowestBreakIteration
		{
			get
			{
				if (this._lowestBreakIteration == 9223372036854775807L)
				{
					return null;
				}
				if (IntPtr.Size >= 8)
				{
					return new long?(this._lowestBreakIteration);
				}
				return new long?(Interlocked.Read(ref this._lowestBreakIteration));
			}
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x0007BE1C File Offset: 0x0007A01C
		internal bool ShouldExitLoop(long CallerIteration)
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != 0 && ((loopStateFlags & 13) != 0 || ((loopStateFlags & 2) != 0 && CallerIteration > this.LowestBreakIteration));
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x0007BE50 File Offset: 0x0007A050
		internal bool ShouldExitLoop()
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != 0 && (loopStateFlags & 9) != 0;
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x0007BE70 File Offset: 0x0007A070
		public ParallelLoopStateFlags64()
		{
		}

		// Token: 0x04001B0A RID: 6922
		internal long _lowestBreakIteration = long.MaxValue;
	}
}
