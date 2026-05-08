using System;

namespace rail
{
	// Token: 0x02000020 RID: 32
	public class IRailLeaderboardHelperImpl : RailObject, IRailLeaderboardHelper
	{
		// Token: 0x06001218 RID: 4632 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailLeaderboardHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000050BC File Offset: 0x000032BC
		~IRailLeaderboardHelperImpl()
		{
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x000050E4 File Offset: 0x000032E4
		public virtual IRailLeaderboard OpenLeaderboard(string leaderboard_name)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailLeaderboardHelper_OpenLeaderboard(this.swigCPtr_, leaderboard_name);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailLeaderboardImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00005114 File Offset: 0x00003314
		public virtual IRailLeaderboard AsyncCreateLeaderboard(string leaderboard_name, LeaderboardSortType sort_type, LeaderboardDisplayType display_type, string user_data, out RailResult result)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailLeaderboardHelper_AsyncCreateLeaderboard(this.swigCPtr_, leaderboard_name, (int)sort_type, (int)display_type, user_data, out result);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailLeaderboardImpl(intPtr);
			}
			return null;
		}
	}
}
