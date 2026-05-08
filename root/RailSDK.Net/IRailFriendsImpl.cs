using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000013 RID: 19
	public class IRailFriendsImpl : RailObject, IRailFriends
	{
		// Token: 0x0600115F RID: 4447 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailFriendsImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00003500 File Offset: 0x00001700
		~IRailFriendsImpl()
		{
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00003528 File Offset: 0x00001728
		public virtual RailResult AsyncGetPersonalInfo(List<RailID> rail_ids, string user_data)
		{
			IntPtr intPtr = ((rail_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			if (rail_ids != null)
			{
				RailConverter.Csharp2Cpp(rail_ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncGetPersonalInfo(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00003578 File Offset: 0x00001778
		public virtual RailResult AsyncGetFriendMetadata(RailID rail_id, List<string> keys, string user_data)
		{
			IntPtr intPtr = ((rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (rail_id != null)
			{
				RailConverter.Csharp2Cpp(rail_id, intPtr);
			}
			IntPtr intPtr2 = ((keys == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (keys != null)
			{
				RailConverter.Csharp2Cpp(keys, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncGetFriendMetadata(this.swigCPtr_, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x000035F8 File Offset: 0x000017F8
		public virtual RailResult AsyncSetMyMetadata(List<RailKeyValue> key_values, string user_data)
		{
			IntPtr intPtr = ((key_values == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0());
			if (key_values != null)
			{
				RailConverter.Csharp2Cpp(key_values, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncSetMyMetadata(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00003648 File Offset: 0x00001848
		public virtual RailResult AsyncClearAllMyMetadata(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncClearAllMyMetadata(this.swigCPtr_, user_data);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00003656 File Offset: 0x00001856
		public virtual RailResult AsyncSetInviteCommandLine(string command_line, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncSetInviteCommandLine(this.swigCPtr_, command_line, user_data);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00003668 File Offset: 0x00001868
		public virtual RailResult AsyncGetInviteCommandLine(RailID rail_id, string user_data)
		{
			IntPtr intPtr = ((rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (rail_id != null)
			{
				RailConverter.Csharp2Cpp(rail_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncGetInviteCommandLine(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x000036C4 File Offset: 0x000018C4
		public virtual RailResult AsyncReportPlayedWithUserList(List<RailUserPlayedWith> player_list, string user_data)
		{
			IntPtr intPtr = ((player_list == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailUserPlayedWith__SWIG_0());
			if (player_list != null)
			{
				RailConverter.Csharp2Cpp(player_list, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncReportPlayedWithUserList(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailUserPlayedWith(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00003714 File Offset: 0x00001914
		public virtual RailResult GetFriendsList(List<RailFriendInfo> friends_list)
		{
			IntPtr intPtr = ((friends_list == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailFriendInfo__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_GetFriendsList(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (friends_list != null)
				{
					RailConverter.Cpp2Csharp(intPtr, friends_list);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailFriendInfo(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00003764 File Offset: 0x00001964
		public virtual RailResult AsyncQueryFriendPlayedGamesInfo(RailID rail_id, string user_data)
		{
			IntPtr intPtr = ((rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (rail_id != null)
			{
				RailConverter.Csharp2Cpp(rail_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncQueryFriendPlayedGamesInfo(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x000037C0 File Offset: 0x000019C0
		public virtual RailResult AsyncQueryPlayedWithFriendsList(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncQueryPlayedWithFriendsList(this.swigCPtr_, user_data);
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x000037D0 File Offset: 0x000019D0
		public virtual RailResult AsyncQueryPlayedWithFriendsTime(List<RailID> rail_ids, string user_data)
		{
			IntPtr intPtr = ((rail_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			if (rail_ids != null)
			{
				RailConverter.Csharp2Cpp(rail_ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncQueryPlayedWithFriendsTime(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00003820 File Offset: 0x00001A20
		public virtual RailResult AsyncQueryPlayedWithFriendsGames(List<RailID> rail_ids, string user_data)
		{
			IntPtr intPtr = ((rail_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			if (rail_ids != null)
			{
				RailConverter.Csharp2Cpp(rail_ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncQueryPlayedWithFriendsGames(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00003870 File Offset: 0x00001A70
		public virtual RailResult AsyncAddFriend(RailFriendsAddFriendRequest request, string user_data)
		{
			IntPtr intPtr = ((request == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailFriendsAddFriendRequest__SWIG_0());
			if (request != null)
			{
				RailConverter.Csharp2Cpp(request, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncAddFriend(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailFriendsAddFriendRequest(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000038C0 File Offset: 0x00001AC0
		public virtual RailResult AsyncUpdateFriendsData(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFriends_AsyncUpdateFriendsData(this.swigCPtr_, user_data);
		}
	}
}
