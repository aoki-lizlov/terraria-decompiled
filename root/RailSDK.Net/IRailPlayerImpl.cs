using System;

namespace rail
{
	// Token: 0x02000022 RID: 34
	public class IRailPlayerImpl : RailObject, IRailPlayer
	{
		// Token: 0x0600122C RID: 4652 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailPlayerImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00005810 File Offset: 0x00003A10
		~IRailPlayerImpl()
		{
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00005838 File Offset: 0x00003A38
		public virtual bool AlreadyLoggedIn()
		{
			return RAIL_API_PINVOKE.IRailPlayer_AlreadyLoggedIn(this.swigCPtr_);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00005848 File Offset: 0x00003A48
		public virtual RailID GetRailID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailPlayer_GetRailID(this.swigCPtr_);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00005870 File Offset: 0x00003A70
		public virtual RailResult GetPlayerDataPath(out string path)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailPlayer_GetPlayerDataPath(this.swigCPtr_, intPtr);
			}
			finally
			{
				path = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x000058B4 File Offset: 0x00003AB4
		public virtual RailResult AsyncAcquireSessionTicket(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayer_AsyncAcquireSessionTicket(this.swigCPtr_, user_data);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x000058C4 File Offset: 0x00003AC4
		public virtual RailResult AsyncStartSessionWithPlayer(RailSessionTicket player_ticket, RailID player_rail_id, string user_data)
		{
			IntPtr intPtr = ((player_ticket == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSessionTicket());
			if (player_ticket != null)
			{
				RailConverter.Csharp2Cpp(player_ticket, intPtr);
			}
			IntPtr intPtr2 = ((player_rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (player_rail_id != null)
			{
				RailConverter.Csharp2Cpp(player_rail_id, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailPlayer_AsyncStartSessionWithPlayer(this.swigCPtr_, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSessionTicket(intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00005944 File Offset: 0x00003B44
		public virtual void TerminateSessionOfPlayer(RailID player_rail_id)
		{
			IntPtr intPtr = ((player_rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (player_rail_id != null)
			{
				RailConverter.Csharp2Cpp(player_rail_id, intPtr);
			}
			try
			{
				RAIL_API_PINVOKE.IRailPlayer_TerminateSessionOfPlayer(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x000059A0 File Offset: 0x00003BA0
		public virtual void AbandonSessionTicket(RailSessionTicket session_ticket)
		{
			IntPtr intPtr = ((session_ticket == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSessionTicket());
			if (session_ticket != null)
			{
				RailConverter.Csharp2Cpp(session_ticket, intPtr);
			}
			try
			{
				RAIL_API_PINVOKE.IRailPlayer_AbandonSessionTicket(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSessionTicket(intPtr);
			}
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x000059F0 File Offset: 0x00003BF0
		public virtual RailResult GetPlayerName(out string name)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailPlayer_GetPlayerName(this.swigCPtr_, intPtr);
			}
			finally
			{
				name = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00005A34 File Offset: 0x00003C34
		public virtual EnumRailPlayerOwnershipType GetPlayerOwnershipType()
		{
			return (EnumRailPlayerOwnershipType)RAIL_API_PINVOKE.IRailPlayer_GetPlayerOwnershipType(this.swigCPtr_);
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00005A41 File Offset: 0x00003C41
		public virtual RailResult AsyncGetGamePurchaseKey(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayer_AsyncGetGamePurchaseKey(this.swigCPtr_, user_data);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00005A4F File Offset: 0x00003C4F
		public virtual bool IsGameRevenueLimited()
		{
			return RAIL_API_PINVOKE.IRailPlayer_IsGameRevenueLimited(this.swigCPtr_);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00005A5C File Offset: 0x00003C5C
		public virtual float GetRateOfGameRevenue()
		{
			return RAIL_API_PINVOKE.IRailPlayer_GetRateOfGameRevenue(this.swigCPtr_);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00005A69 File Offset: 0x00003C69
		public virtual RailResult AsyncQueryPlayerBannedStatus(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayer_AsyncQueryPlayerBannedStatus(this.swigCPtr_, user_data);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00005A77 File Offset: 0x00003C77
		public virtual RailResult AsyncGetAuthenticateURL(string url, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayer_AsyncGetAuthenticateURL(this.swigCPtr_, url, user_data);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00005A88 File Offset: 0x00003C88
		public virtual RailResult GetPlayerMetadata(string key, out string value)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailPlayer_GetPlayerMetadata(this.swigCPtr_, key, intPtr);
			}
			finally
			{
				value = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}
	}
}
