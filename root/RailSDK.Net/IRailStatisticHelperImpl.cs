using System;

namespace rail
{
	// Token: 0x0200002B RID: 43
	public class IRailStatisticHelperImpl : RailObject, IRailStatisticHelper
	{
		// Token: 0x060012D5 RID: 4821 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailStatisticHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x000070F8 File Offset: 0x000052F8
		~IRailStatisticHelperImpl()
		{
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00007120 File Offset: 0x00005320
		public virtual IRailPlayerStats CreatePlayerStats(RailID player)
		{
			IntPtr intPtr = ((player == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (player != null)
			{
				RailConverter.Csharp2Cpp(player, intPtr);
			}
			IRailPlayerStats railPlayerStats;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailStatisticHelper_CreatePlayerStats(this.swigCPtr_, intPtr);
				railPlayerStats = ((intPtr2 == IntPtr.Zero) ? null : new IRailPlayerStatsImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railPlayerStats;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00007194 File Offset: 0x00005394
		public virtual IRailGlobalStats GetGlobalStats()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailStatisticHelper_GetGlobalStats(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailGlobalStatsImpl(intPtr);
			}
			return null;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x000071C2 File Offset: 0x000053C2
		public virtual RailResult AsyncGetNumberOfPlayer(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStatisticHelper_AsyncGetNumberOfPlayer(this.swigCPtr_, user_data);
		}
	}
}
