using System;

namespace rail
{
	// Token: 0x0200008C RID: 140
	public interface IRailFactory
	{
		// Token: 0x06001660 RID: 5728
		IRailPlayer RailPlayer();

		// Token: 0x06001661 RID: 5729
		IRailUsersHelper RailUsersHelper();

		// Token: 0x06001662 RID: 5730
		IRailFriends RailFriends();

		// Token: 0x06001663 RID: 5731
		IRailFloatingWindow RailFloatingWindow();

		// Token: 0x06001664 RID: 5732
		IRailBrowserHelper RailBrowserHelper();

		// Token: 0x06001665 RID: 5733
		IRailInGamePurchase RailInGamePurchase();

		// Token: 0x06001666 RID: 5734
		IRailZoneHelper RailZoneHelper();

		// Token: 0x06001667 RID: 5735
		IRailRoomHelper RailRoomHelper();

		// Token: 0x06001668 RID: 5736
		IRailGameServerHelper RailGameServerHelper();

		// Token: 0x06001669 RID: 5737
		IRailStorageHelper RailStorageHelper();

		// Token: 0x0600166A RID: 5738
		IRailUserSpaceHelper RailUserSpaceHelper();

		// Token: 0x0600166B RID: 5739
		IRailStatisticHelper RailStatisticHelper();

		// Token: 0x0600166C RID: 5740
		IRailLeaderboardHelper RailLeaderboardHelper();

		// Token: 0x0600166D RID: 5741
		IRailAchievementHelper RailAchievementHelper();

		// Token: 0x0600166E RID: 5742
		IRailNetwork RailNetworkHelper();

		// Token: 0x0600166F RID: 5743
		IRailApps RailApps();

		// Token: 0x06001670 RID: 5744
		IRailGame RailGame();

		// Token: 0x06001671 RID: 5745
		IRailUtils RailUtils();

		// Token: 0x06001672 RID: 5746
		IRailAssetsHelper RailAssetsHelper();

		// Token: 0x06001673 RID: 5747
		IRailDlcHelper RailDlcHelper();

		// Token: 0x06001674 RID: 5748
		IRailScreenshotHelper RailScreenshotHelper();

		// Token: 0x06001675 RID: 5749
		IRailVoiceHelper RailVoiceHelper();

		// Token: 0x06001676 RID: 5750
		IRailSystemHelper RailSystemHelper();

		// Token: 0x06001677 RID: 5751
		IRailTextInputHelper RailTextInputHelper();

		// Token: 0x06001678 RID: 5752
		IRailIMEHelper RailIMETextInputHelper();

		// Token: 0x06001679 RID: 5753
		IRailHttpSessionHelper RailHttpSessionHelper();

		// Token: 0x0600167A RID: 5754
		IRailSmallObjectServiceHelper RailSmallObjectServiceHelper();

		// Token: 0x0600167B RID: 5755
		IRailZoneServerHelper RailZoneServerHelper();
	}
}
