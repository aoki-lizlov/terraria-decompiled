using System;

namespace rail
{
	// Token: 0x0200001F RID: 31
	public class IRailLeaderboardEntriesImpl : RailObject, IRailLeaderboardEntries, IRailComponent
	{
		// Token: 0x0600120E RID: 4622 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailLeaderboardEntriesImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00004F5C File Offset: 0x0000315C
		~IRailLeaderboardEntriesImpl()
		{
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00004F84 File Offset: 0x00003184
		public virtual RailID GetRailID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailLeaderboardEntries_GetRailID(this.swigCPtr_);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00004FA9 File Offset: 0x000031A9
		public virtual string GetLeaderboardName()
		{
			return RAIL_API_PINVOKE.IRailLeaderboardEntries_GetLeaderboardName(this.swigCPtr_);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00004FB8 File Offset: 0x000031B8
		public virtual RailResult AsyncRequestLeaderboardEntries(RailID player, RequestLeaderboardEntryParam param, string user_data)
		{
			IntPtr intPtr = ((player == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (player != null)
			{
				RailConverter.Csharp2Cpp(player, intPtr);
			}
			IntPtr intPtr2 = ((param == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RequestLeaderboardEntryParam__SWIG_0());
			if (param != null)
			{
				RailConverter.Csharp2Cpp(param, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailLeaderboardEntries_AsyncRequestLeaderboardEntries(this.swigCPtr_, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RequestLeaderboardEntryParam(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00005038 File Offset: 0x00003238
		public virtual RequestLeaderboardEntryParam GetEntriesParam()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailLeaderboardEntries_GetEntriesParam(this.swigCPtr_);
			RequestLeaderboardEntryParam requestLeaderboardEntryParam = new RequestLeaderboardEntryParam();
			RailConverter.Cpp2Csharp(intPtr, requestLeaderboardEntryParam);
			return requestLeaderboardEntryParam;
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0000505D File Offset: 0x0000325D
		public virtual int GetEntriesCount()
		{
			return RAIL_API_PINVOKE.IRailLeaderboardEntries_GetEntriesCount(this.swigCPtr_);
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0000506C File Offset: 0x0000326C
		public virtual RailResult GetLeaderboardEntry(int index, LeaderboardEntry leaderboard_entry)
		{
			IntPtr intPtr = ((leaderboard_entry == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_LeaderboardEntry__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailLeaderboardEntries_GetLeaderboardEntry(this.swigCPtr_, index, intPtr);
			}
			finally
			{
				if (leaderboard_entry != null)
				{
					RailConverter.Cpp2Csharp(intPtr, leaderboard_entry);
				}
				RAIL_API_PINVOKE.delete_LeaderboardEntry(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
