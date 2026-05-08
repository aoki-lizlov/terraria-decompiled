using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002FC RID: 764
	internal class ParallelLoopStateFlags32 : ParallelLoopStateFlags
	{
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x0007BCF2 File Offset: 0x00079EF2
		internal int LowestBreakIteration
		{
			get
			{
				return this._lowestBreakIteration;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600220F RID: 8719 RVA: 0x0007BCFC File Offset: 0x00079EFC
		internal long? NullableLowestBreakIteration
		{
			get
			{
				if (this._lowestBreakIteration == 2147483647)
				{
					return null;
				}
				long num = (long)this._lowestBreakIteration;
				if (IntPtr.Size >= 8)
				{
					return new long?(num);
				}
				return new long?(Interlocked.Read(ref num));
			}
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x0007BD48 File Offset: 0x00079F48
		internal bool ShouldExitLoop(int CallerIteration)
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != 0 && ((loopStateFlags & 13) != 0 || ((loopStateFlags & 2) != 0 && CallerIteration > this.LowestBreakIteration));
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x0007BD7C File Offset: 0x00079F7C
		internal bool ShouldExitLoop()
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != 0 && (loopStateFlags & 9) != 0;
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x0007BD9C File Offset: 0x00079F9C
		public ParallelLoopStateFlags32()
		{
		}

		// Token: 0x04001B09 RID: 6921
		internal volatile int _lowestBreakIteration = int.MaxValue;
	}
}
