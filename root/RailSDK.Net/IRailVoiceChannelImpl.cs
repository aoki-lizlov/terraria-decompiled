using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000033 RID: 51
	public class IRailVoiceChannelImpl : RailObject, IRailVoiceChannel, IRailComponent
	{
		// Token: 0x06001332 RID: 4914 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailVoiceChannelImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x000081F4 File Offset: 0x000063F4
		~IRailVoiceChannelImpl()
		{
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0000821C File Offset: 0x0000641C
		public virtual RailVoiceChannelID GetVoiceChannelID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailVoiceChannel_GetVoiceChannelID(this.swigCPtr_);
			RailVoiceChannelID railVoiceChannelID = new RailVoiceChannelID();
			RailConverter.Cpp2Csharp(intPtr, railVoiceChannelID);
			return railVoiceChannelID;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00008241 File Offset: 0x00006441
		public virtual string GetVoiceChannelName()
		{
			return RAIL_API_PINVOKE.IRailVoiceChannel_GetVoiceChannelName(this.swigCPtr_);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0000824E File Offset: 0x0000644E
		public virtual EnumRailVoiceChannelJoinState GetJoinState()
		{
			return (EnumRailVoiceChannelJoinState)RAIL_API_PINVOKE.IRailVoiceChannel_GetJoinState(this.swigCPtr_);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0000825B File Offset: 0x0000645B
		public virtual RailResult AsyncJoinVoiceChannel(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_AsyncJoinVoiceChannel(this.swigCPtr_, user_data);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00008269 File Offset: 0x00006469
		public virtual RailResult AsyncLeaveVoiceChannel(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_AsyncLeaveVoiceChannel(this.swigCPtr_, user_data);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00008278 File Offset: 0x00006478
		public virtual RailResult GetUsers(List<RailID> user_list)
		{
			IntPtr intPtr = ((user_list == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_GetUsers(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (user_list != null)
				{
					RailConverter.Cpp2Csharp(intPtr, user_list);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000082C8 File Offset: 0x000064C8
		public virtual RailResult AsyncAddUsers(List<RailID> user_list, string user_data)
		{
			IntPtr intPtr = ((user_list == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			if (user_list != null)
			{
				RailConverter.Csharp2Cpp(user_list, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_AsyncAddUsers(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00008318 File Offset: 0x00006518
		public virtual RailResult AsyncRemoveUsers(List<RailID> user_list, string user_data)
		{
			IntPtr intPtr = ((user_list == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			if (user_list != null)
			{
				RailConverter.Csharp2Cpp(user_list, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_AsyncRemoveUsers(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00008368 File Offset: 0x00006568
		public virtual RailResult CloseChannel()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_CloseChannel(this.swigCPtr_);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00008375 File Offset: 0x00006575
		public virtual RailResult SetSelfSpeaking(bool speaking)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_SetSelfSpeaking(this.swigCPtr_, speaking);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00008383 File Offset: 0x00006583
		public virtual bool IsSelfSpeaking()
		{
			return RAIL_API_PINVOKE.IRailVoiceChannel_IsSelfSpeaking(this.swigCPtr_);
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00008390 File Offset: 0x00006590
		public virtual RailResult AsyncSetUsersSpeakingState(List<RailVoiceChannelUserSpeakingState> users_speaking_state, string user_data)
		{
			IntPtr intPtr = ((users_speaking_state == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailVoiceChannelUserSpeakingState__SWIG_0());
			if (users_speaking_state != null)
			{
				RailConverter.Csharp2Cpp(users_speaking_state, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_AsyncSetUsersSpeakingState(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailVoiceChannelUserSpeakingState(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000083E0 File Offset: 0x000065E0
		public virtual RailResult GetUsersSpeakingState(List<RailVoiceChannelUserSpeakingState> users_speaking_state)
		{
			IntPtr intPtr = ((users_speaking_state == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailVoiceChannelUserSpeakingState__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_GetUsersSpeakingState(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (users_speaking_state != null)
				{
					RailConverter.Cpp2Csharp(intPtr, users_speaking_state);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailVoiceChannelUserSpeakingState(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00008430 File Offset: 0x00006630
		public virtual RailResult GetSpeakingUsers(List<RailID> speaking_users, List<RailID> not_speaking_users)
		{
			IntPtr intPtr = ((speaking_users == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			IntPtr intPtr2 = ((not_speaking_users == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceChannel_GetSpeakingUsers(this.swigCPtr_, intPtr, intPtr2);
			}
			finally
			{
				if (speaking_users != null)
				{
					RailConverter.Cpp2Csharp(intPtr, speaking_users);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
				if (not_speaking_users != null)
				{
					RailConverter.Cpp2Csharp(intPtr2, not_speaking_users);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000084A0 File Offset: 0x000066A0
		public virtual bool IsOwner()
		{
			return RAIL_API_PINVOKE.IRailVoiceChannel_IsOwner(this.swigCPtr_);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
