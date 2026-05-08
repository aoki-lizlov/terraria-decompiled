using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000AF RID: 175
	public interface IRailGame
	{
		// Token: 0x060016A9 RID: 5801
		RailGameID GetGameID();

		// Token: 0x060016AA RID: 5802
		RailResult ReportGameContentDamaged(EnumRailGameContentDamageFlag flag);

		// Token: 0x060016AB RID: 5803
		RailResult GetGameInstallPath(out string app_path);

		// Token: 0x060016AC RID: 5804
		RailResult AsyncQuerySubscribeWishPlayState(string user_data);

		// Token: 0x060016AD RID: 5805
		RailResult GetPlayerSelectedLanguageCode(out string language_code);

		// Token: 0x060016AE RID: 5806
		RailResult GetGameSupportedLanguageCodes(List<string> language_codes);

		// Token: 0x060016AF RID: 5807
		RailResult SetGameState(EnumRailGamePlayingState game_state);

		// Token: 0x060016B0 RID: 5808
		RailResult GetGameState(out EnumRailGamePlayingState game_state);

		// Token: 0x060016B1 RID: 5809
		RailResult RegisterGameDefineGamePlayingState(List<RailGameDefineGamePlayingState> game_playing_states);

		// Token: 0x060016B2 RID: 5810
		RailResult SetGameDefineGamePlayingState(uint game_playing_state);

		// Token: 0x060016B3 RID: 5811
		RailResult GetGameDefineGamePlayingState(out uint game_playing_state);

		// Token: 0x060016B4 RID: 5812
		uint GetBranchBuildNumber();

		// Token: 0x060016B5 RID: 5813
		RailResult GetCurrentBranchInfo(RailBranchInfo branch_info);

		// Token: 0x060016B6 RID: 5814
		RailResult StartGameTimeCounting(string counting_key);

		// Token: 0x060016B7 RID: 5815
		RailResult EndGameTimeCounting(string counting_key);

		// Token: 0x060016B8 RID: 5816
		RailID GetGamePurchasePlayerRailID();

		// Token: 0x060016B9 RID: 5817
		uint GetGameEarliestPurchaseTime();

		// Token: 0x060016BA RID: 5818
		uint GetTimeCountSinceGameActivated();

		// Token: 0x060016BB RID: 5819
		uint GetTimeCountSinceLastMouseMoved();
	}
}
