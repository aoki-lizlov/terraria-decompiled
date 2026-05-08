using System;

namespace rail
{
	// Token: 0x0200001E RID: 30
	public class IRailLeaderboardImpl : RailObject, IRailLeaderboard, IRailComponent
	{
		// Token: 0x06001201 RID: 4609 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailLeaderboardImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00004DC4 File Offset: 0x00002FC4
		~IRailLeaderboardImpl()
		{
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00004DEC File Offset: 0x00002FEC
		public virtual string GetLeaderboardName()
		{
			return RAIL_API_PINVOKE.IRailLeaderboard_GetLeaderboardName(this.swigCPtr_);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00004DF9 File Offset: 0x00002FF9
		public virtual int GetTotalEntriesCount()
		{
			return RAIL_API_PINVOKE.IRailLeaderboard_GetTotalEntriesCount(this.swigCPtr_);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00004E06 File Offset: 0x00003006
		public virtual RailResult AsyncGetLeaderboard(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailLeaderboard_AsyncGetLeaderboard(this.swigCPtr_, user_data);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00004E14 File Offset: 0x00003014
		public virtual RailResult GetLeaderboardParameters(LeaderboardParameters param)
		{
			IntPtr intPtr = ((param == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_LeaderboardParameters__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailLeaderboard_GetLeaderboardParameters(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (param != null)
				{
					RailConverter.Cpp2Csharp(intPtr, param);
				}
				RAIL_API_PINVOKE.delete_LeaderboardParameters(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00004E64 File Offset: 0x00003064
		public virtual IRailLeaderboardEntries CreateLeaderboardEntries()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailLeaderboard_CreateLeaderboardEntries(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailLeaderboardEntriesImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00004E94 File Offset: 0x00003094
		public virtual RailResult AsyncUploadLeaderboard(UploadLeaderboardParam update_param, string user_data)
		{
			IntPtr intPtr = ((update_param == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_UploadLeaderboardParam__SWIG_0());
			if (update_param != null)
			{
				RailConverter.Csharp2Cpp(update_param, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailLeaderboard_AsyncUploadLeaderboard(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_UploadLeaderboardParam(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00004EE4 File Offset: 0x000030E4
		public virtual RailResult GetLeaderboardSortType(out int sort_type)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailLeaderboard_GetLeaderboardSortType(this.swigCPtr_, out sort_type);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00004EF2 File Offset: 0x000030F2
		public virtual RailResult GetLeaderboardDisplayType(out int display_type)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailLeaderboard_GetLeaderboardDisplayType(this.swigCPtr_, out display_type);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00004F00 File Offset: 0x00003100
		public virtual RailResult AsyncAttachSpaceWork(SpaceWorkID spacework_id, string user_data)
		{
			IntPtr intPtr = ((spacework_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_SpaceWorkID__SWIG_0());
			if (spacework_id != null)
			{
				RailConverter.Csharp2Cpp(spacework_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailLeaderboard_AsyncAttachSpaceWork(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_SpaceWorkID(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
