using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000025 RID: 37
	public class IRailRoomImpl : RailObject, IRailRoom, IRailComponent
	{
		// Token: 0x0600125A RID: 4698 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailRoomImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x00005CF8 File Offset: 0x00003EF8
		~IRailRoomImpl()
		{
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00005D20 File Offset: 0x00003F20
		public virtual ulong GetRoomId()
		{
			return RAIL_API_PINVOKE.IRailRoom_GetRoomId(this.swigCPtr_);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00005D30 File Offset: 0x00003F30
		public virtual RailResult GetRoomName(out string name)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_GetRoomName(this.swigCPtr_, intPtr);
			}
			finally
			{
				name = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00005D74 File Offset: 0x00003F74
		public virtual ulong GetZoneId()
		{
			return RAIL_API_PINVOKE.IRailRoom_GetZoneId(this.swigCPtr_);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00005D84 File Offset: 0x00003F84
		public virtual RailID GetOwnerId()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailRoom_GetOwnerId(this.swigCPtr_);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00005DA9 File Offset: 0x00003FA9
		public virtual RailResult GetHasPassword(out bool has_password)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailRoom_GetHasPassword(this.swigCPtr_, out has_password);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00005DB7 File Offset: 0x00003FB7
		public virtual EnumRoomType GetRoomType()
		{
			return (EnumRoomType)RAIL_API_PINVOKE.IRailRoom_GetRoomType(this.swigCPtr_);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00005DC4 File Offset: 0x00003FC4
		public virtual bool SetNewOwner(RailID new_owner_id)
		{
			IntPtr intPtr = ((new_owner_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (new_owner_id != null)
			{
				RailConverter.Csharp2Cpp(new_owner_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailRoom_SetNewOwner(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return flag;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00005E20 File Offset: 0x00004020
		public virtual RailResult AsyncGetRoomMembers(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncGetRoomMembers(this.swigCPtr_, user_data);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00005E2E File Offset: 0x0000402E
		public virtual void Leave()
		{
			RAIL_API_PINVOKE.IRailRoom_Leave(this.swigCPtr_);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00005E3B File Offset: 0x0000403B
		public virtual RailResult AsyncJoinRoom(string password, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncJoinRoom(this.swigCPtr_, password, user_data);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00005E4A File Offset: 0x0000404A
		public virtual RailResult AsyncGetAllRoomData(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncGetAllRoomData(this.swigCPtr_, user_data);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00005E58 File Offset: 0x00004058
		public virtual RailResult AsyncKickOffMember(RailID member_id, string user_data)
		{
			IntPtr intPtr = ((member_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (member_id != null)
			{
				RailConverter.Csharp2Cpp(member_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncKickOffMember(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00005EB4 File Offset: 0x000040B4
		public virtual bool GetRoomMetadata(string key, out string value)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailRoom_GetRoomMetadata(this.swigCPtr_, key, intPtr);
			}
			finally
			{
				value = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00005EF8 File Offset: 0x000040F8
		public virtual bool SetRoomMetadata(string key, string value)
		{
			return RAIL_API_PINVOKE.IRailRoom_SetRoomMetadata(this.swigCPtr_, key, value);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00005F08 File Offset: 0x00004108
		public virtual RailResult AsyncSetRoomMetadata(List<RailKeyValue> key_values, string user_data)
		{
			IntPtr intPtr = ((key_values == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0());
			if (key_values != null)
			{
				RailConverter.Csharp2Cpp(key_values, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncSetRoomMetadata(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00005F58 File Offset: 0x00004158
		public virtual RailResult AsyncGetRoomMetadata(List<string> keys, string user_data)
		{
			IntPtr intPtr = ((keys == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (keys != null)
			{
				RailConverter.Csharp2Cpp(keys, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncGetRoomMetadata(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00005FA8 File Offset: 0x000041A8
		public virtual RailResult AsyncClearRoomMetadata(List<string> keys, string user_data)
		{
			IntPtr intPtr = ((keys == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (keys != null)
			{
				RailConverter.Csharp2Cpp(keys, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncClearRoomMetadata(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00005FF8 File Offset: 0x000041F8
		public virtual bool GetMemberMetadata(RailID member_id, string key, out string value)
		{
			IntPtr intPtr = ((member_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (member_id != null)
			{
				RailConverter.Csharp2Cpp(member_id, intPtr);
			}
			IntPtr intPtr2 = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailRoom_GetMemberMetadata(this.swigCPtr_, intPtr, key, intPtr2);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				value = RAIL_API_PINVOKE.RailString_c_str(intPtr2);
				RAIL_API_PINVOKE.delete_RailString(intPtr2);
			}
			return flag;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00006068 File Offset: 0x00004268
		public virtual bool SetMemberMetadata(RailID member_id, string key, string value)
		{
			IntPtr intPtr = ((member_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (member_id != null)
			{
				RailConverter.Csharp2Cpp(member_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailRoom_SetMemberMetadata(this.swigCPtr_, intPtr, key, value);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return flag;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x000060C8 File Offset: 0x000042C8
		public virtual RailResult AsyncGetMemberMetadata(RailID member_id, List<string> keys, string user_data)
		{
			IntPtr intPtr = ((member_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (member_id != null)
			{
				RailConverter.Csharp2Cpp(member_id, intPtr);
			}
			IntPtr intPtr2 = ((keys == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (keys != null)
			{
				RailConverter.Csharp2Cpp(keys, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncGetMemberMetadata(this.swigCPtr_, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00006148 File Offset: 0x00004348
		public virtual RailResult AsyncSetMemberMetadata(RailID member_id, List<RailKeyValue> key_values, string user_data)
		{
			IntPtr intPtr = ((member_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (member_id != null)
			{
				RailConverter.Csharp2Cpp(member_id, intPtr);
			}
			IntPtr intPtr2 = ((key_values == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0());
			if (key_values != null)
			{
				RailConverter.Csharp2Cpp(key_values, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_AsyncSetMemberMetadata(this.swigCPtr_, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x000061C8 File Offset: 0x000043C8
		public virtual RailResult SendDataToMember(RailID remote_peer, byte[] data_buf, uint data_len, uint message_type)
		{
			IntPtr intPtr = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_SendDataToMember__SWIG_0(this.swigCPtr_, intPtr, data_buf, data_len, message_type);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00006228 File Offset: 0x00004428
		public virtual RailResult SendDataToMember(RailID remote_peer, byte[] data_buf, uint data_len)
		{
			IntPtr intPtr = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_SendDataToMember__SWIG_1(this.swigCPtr_, intPtr, data_buf, data_len);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x00006288 File Offset: 0x00004488
		public virtual uint GetNumOfMembers()
		{
			return RAIL_API_PINVOKE.IRailRoom_GetNumOfMembers(this.swigCPtr_);
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00006298 File Offset: 0x00004498
		public virtual RailID GetMemberByIndex(uint index)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailRoom_GetMemberByIndex(this.swigCPtr_, index);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000062C0 File Offset: 0x000044C0
		public virtual RailResult GetMemberNameByIndex(uint index, out string name)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_GetMemberNameByIndex(this.swigCPtr_, index, intPtr);
			}
			finally
			{
				name = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00006304 File Offset: 0x00004504
		public virtual uint GetMaxMembers()
		{
			return RAIL_API_PINVOKE.IRailRoom_GetMaxMembers(this.swigCPtr_);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00006311 File Offset: 0x00004511
		public virtual bool SetGameServerID(ulong game_server_rail_id)
		{
			return RAIL_API_PINVOKE.IRailRoom_SetGameServerID(this.swigCPtr_, game_server_rail_id);
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0000631F File Offset: 0x0000451F
		public virtual bool GetGameServerID(out ulong game_server_rail_id)
		{
			return RAIL_API_PINVOKE.IRailRoom_GetGameServerID(this.swigCPtr_, out game_server_rail_id);
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0000632D File Offset: 0x0000452D
		public virtual bool SetRoomJoinable(bool is_joinable)
		{
			return RAIL_API_PINVOKE.IRailRoom_SetRoomJoinable(this.swigCPtr_, is_joinable);
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0000633B File Offset: 0x0000453B
		public virtual bool GetRoomJoinable()
		{
			return RAIL_API_PINVOKE.IRailRoom_GetRoomJoinable(this.swigCPtr_);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00006348 File Offset: 0x00004548
		public virtual RailResult GetFriendsInRoom(List<RailID> friend_ids)
		{
			IntPtr intPtr = ((friend_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoom_GetFriendsInRoom(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (friend_ids != null)
				{
					RailConverter.Cpp2Csharp(intPtr, friend_ids);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00006398 File Offset: 0x00004598
		public virtual bool IsUserInRoom(RailID user_rail_id)
		{
			IntPtr intPtr = ((user_rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (user_rail_id != null)
			{
				RailConverter.Csharp2Cpp(user_rail_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailRoom_IsUserInRoom(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return flag;
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x000063F4 File Offset: 0x000045F4
		public virtual RailResult EnableTeamVoice(bool enable)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailRoom_EnableTeamVoice(this.swigCPtr_, enable);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
