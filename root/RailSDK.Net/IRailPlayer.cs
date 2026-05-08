using System;

namespace rail
{
	// Token: 0x020000FF RID: 255
	public interface IRailPlayer
	{
		// Token: 0x06001779 RID: 6009
		bool AlreadyLoggedIn();

		// Token: 0x0600177A RID: 6010
		RailID GetRailID();

		// Token: 0x0600177B RID: 6011
		RailResult GetPlayerDataPath(out string path);

		// Token: 0x0600177C RID: 6012
		RailResult AsyncAcquireSessionTicket(string user_data);

		// Token: 0x0600177D RID: 6013
		RailResult AsyncStartSessionWithPlayer(RailSessionTicket player_ticket, RailID player_rail_id, string user_data);

		// Token: 0x0600177E RID: 6014
		void TerminateSessionOfPlayer(RailID player_rail_id);

		// Token: 0x0600177F RID: 6015
		void AbandonSessionTicket(RailSessionTicket session_ticket);

		// Token: 0x06001780 RID: 6016
		RailResult GetPlayerName(out string name);

		// Token: 0x06001781 RID: 6017
		EnumRailPlayerOwnershipType GetPlayerOwnershipType();

		// Token: 0x06001782 RID: 6018
		RailResult AsyncGetGamePurchaseKey(string user_data);

		// Token: 0x06001783 RID: 6019
		bool IsGameRevenueLimited();

		// Token: 0x06001784 RID: 6020
		float GetRateOfGameRevenue();

		// Token: 0x06001785 RID: 6021
		RailResult AsyncQueryPlayerBannedStatus(string user_data);

		// Token: 0x06001786 RID: 6022
		RailResult AsyncGetAuthenticateURL(string url, string user_data);

		// Token: 0x06001787 RID: 6023
		RailResult GetPlayerMetadata(string key, out string value);
	}
}
