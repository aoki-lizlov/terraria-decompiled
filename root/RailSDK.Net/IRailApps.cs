using System;

namespace rail
{
	// Token: 0x02000045 RID: 69
	public interface IRailApps
	{
		// Token: 0x060015E5 RID: 5605
		bool IsGameInstalled(RailGameID game_id);

		// Token: 0x060015E6 RID: 5606
		RailResult AsyncQuerySubscribeWishPlayState(RailGameID game_id, string user_data);
	}
}
