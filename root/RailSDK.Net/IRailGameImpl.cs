using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000014 RID: 20
	public class IRailGameImpl : RailObject, IRailGame
	{
		// Token: 0x0600116F RID: 4463 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailGameImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x000038D0 File Offset: 0x00001AD0
		~IRailGameImpl()
		{
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x000038F8 File Offset: 0x00001AF8
		public virtual RailGameID GetGameID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailGame_GetGameID(this.swigCPtr_);
			RailGameID railGameID = new RailGameID();
			RailConverter.Cpp2Csharp(intPtr, railGameID);
			return railGameID;
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0000391D File Offset: 0x00001B1D
		public virtual RailResult ReportGameContentDamaged(EnumRailGameContentDamageFlag flag)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGame_ReportGameContentDamaged(this.swigCPtr_, (int)flag);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0000392C File Offset: 0x00001B2C
		public virtual RailResult GetGameInstallPath(out string app_path)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGame_GetGameInstallPath(this.swigCPtr_, intPtr);
			}
			finally
			{
				app_path = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00003970 File Offset: 0x00001B70
		public virtual RailResult AsyncQuerySubscribeWishPlayState(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGame_AsyncQuerySubscribeWishPlayState(this.swigCPtr_, user_data);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00003980 File Offset: 0x00001B80
		public virtual RailResult GetPlayerSelectedLanguageCode(out string language_code)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGame_GetPlayerSelectedLanguageCode(this.swigCPtr_, intPtr);
			}
			finally
			{
				language_code = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000039C4 File Offset: 0x00001BC4
		public virtual RailResult GetGameSupportedLanguageCodes(List<string> language_codes)
		{
			IntPtr intPtr = ((language_codes == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGame_GetGameSupportedLanguageCodes(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (language_codes != null)
				{
					RailConverter.Cpp2Csharp(intPtr, language_codes);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00003A14 File Offset: 0x00001C14
		public virtual RailResult SetGameState(EnumRailGamePlayingState game_state)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGame_SetGameState(this.swigCPtr_, (int)game_state);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00003A24 File Offset: 0x00001C24
		public virtual RailResult GetGameState(out EnumRailGamePlayingState game_state)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.NewInt();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGame_GetGameState(this.swigCPtr_, intPtr);
			}
			finally
			{
				game_state = (EnumRailGamePlayingState)RAIL_API_PINVOKE.GetInt(intPtr);
				RAIL_API_PINVOKE.DeleteInt(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00003A68 File Offset: 0x00001C68
		public virtual RailResult RegisterGameDefineGamePlayingState(List<RailGameDefineGamePlayingState> game_playing_states)
		{
			IntPtr intPtr = ((game_playing_states == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailGameDefineGamePlayingState__SWIG_0());
			if (game_playing_states != null)
			{
				RailConverter.Csharp2Cpp(game_playing_states, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGame_RegisterGameDefineGamePlayingState(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailGameDefineGamePlayingState(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public virtual RailResult SetGameDefineGamePlayingState(uint game_playing_state)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGame_SetGameDefineGamePlayingState(this.swigCPtr_, game_playing_state);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00003AC6 File Offset: 0x00001CC6
		public virtual RailResult GetGameDefineGamePlayingState(out uint game_playing_state)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGame_GetGameDefineGamePlayingState(this.swigCPtr_, out game_playing_state);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00003AD4 File Offset: 0x00001CD4
		public virtual uint GetBranchBuildNumber()
		{
			return RAIL_API_PINVOKE.IRailGame_GetBranchBuildNumber(this.swigCPtr_);
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00003AE4 File Offset: 0x00001CE4
		public virtual RailResult GetCurrentBranchInfo(RailBranchInfo branch_info)
		{
			IntPtr intPtr = ((branch_info == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailBranchInfo__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGame_GetCurrentBranchInfo(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (branch_info != null)
				{
					RailConverter.Cpp2Csharp(intPtr, branch_info);
				}
				RAIL_API_PINVOKE.delete_RailBranchInfo(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00003B34 File Offset: 0x00001D34
		public virtual RailResult StartGameTimeCounting(string counting_key)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGame_StartGameTimeCounting(this.swigCPtr_, counting_key);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00003B42 File Offset: 0x00001D42
		public virtual RailResult EndGameTimeCounting(string counting_key)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGame_EndGameTimeCounting(this.swigCPtr_, counting_key);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00003B50 File Offset: 0x00001D50
		public virtual RailID GetGamePurchasePlayerRailID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailGame_GetGamePurchasePlayerRailID(this.swigCPtr_);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00003B75 File Offset: 0x00001D75
		public virtual uint GetGameEarliestPurchaseTime()
		{
			return RAIL_API_PINVOKE.IRailGame_GetGameEarliestPurchaseTime(this.swigCPtr_);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00003B82 File Offset: 0x00001D82
		public virtual uint GetTimeCountSinceGameActivated()
		{
			return RAIL_API_PINVOKE.IRailGame_GetTimeCountSinceGameActivated(this.swigCPtr_);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00003B8F File Offset: 0x00001D8F
		public virtual uint GetTimeCountSinceLastMouseMoved()
		{
			return RAIL_API_PINVOKE.IRailGame_GetTimeCountSinceLastMouseMoved(this.swigCPtr_);
		}
	}
}
