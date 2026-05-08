using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000030 RID: 48
	public class IRailUsersHelperImpl : RailObject, IRailUsersHelper
	{
		// Token: 0x06001306 RID: 4870 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailUsersHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00007748 File Offset: 0x00005948
		~IRailUsersHelperImpl()
		{
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00007770 File Offset: 0x00005970
		public virtual RailResult AsyncGetUsersInfo(List<RailID> rail_ids, string user_data)
		{
			IntPtr intPtr = ((rail_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			if (rail_ids != null)
			{
				RailConverter.Csharp2Cpp(rail_ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUsersHelper_AsyncGetUsersInfo(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x000077C0 File Offset: 0x000059C0
		public virtual RailResult AsyncInviteUsers(string command_line, List<RailID> users, RailInviteOptions options, string user_data)
		{
			IntPtr intPtr = ((users == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			if (users != null)
			{
				RailConverter.Csharp2Cpp(users, intPtr);
			}
			IntPtr intPtr2 = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailInviteOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUsersHelper_AsyncInviteUsers(this.swigCPtr_, command_line, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
				RAIL_API_PINVOKE.delete_RailInviteOptions(intPtr2);
			}
			return railResult;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00007834 File Offset: 0x00005A34
		public virtual RailResult AsyncGetInviteDetail(RailID inviter, EnumRailUsersInviteType invite_type, string user_data)
		{
			IntPtr intPtr = ((inviter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (inviter != null)
			{
				RailConverter.Csharp2Cpp(inviter, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUsersHelper_AsyncGetInviteDetail(this.swigCPtr_, intPtr, (int)invite_type, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00007894 File Offset: 0x00005A94
		public virtual RailResult AsyncCancelInvite(EnumRailUsersInviteType invite_type, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailUsersHelper_AsyncCancelInvite(this.swigCPtr_, (int)invite_type, user_data);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x000078A3 File Offset: 0x00005AA3
		public virtual RailResult AsyncCancelAllInvites(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailUsersHelper_AsyncCancelAllInvites(this.swigCPtr_, user_data);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x000078B4 File Offset: 0x00005AB4
		public virtual RailResult AsyncGetUserLimits(RailID user_id, string user_data)
		{
			IntPtr intPtr = ((user_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (user_id != null)
			{
				RailConverter.Csharp2Cpp(user_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUsersHelper_AsyncGetUserLimits(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00007910 File Offset: 0x00005B10
		public virtual RailResult AsyncShowChatWindowWithFriend(RailID rail_id, string user_data)
		{
			IntPtr intPtr = ((rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (rail_id != null)
			{
				RailConverter.Csharp2Cpp(rail_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUsersHelper_AsyncShowChatWindowWithFriend(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0000796C File Offset: 0x00005B6C
		public virtual RailResult AsyncShowUserHomepageWindow(RailID rail_id, string user_data)
		{
			IntPtr intPtr = ((rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (rail_id != null)
			{
				RailConverter.Csharp2Cpp(rail_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUsersHelper_AsyncShowUserHomepageWindow(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}
	}
}
