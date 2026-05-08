using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200007D RID: 125
	public interface IRailDlcHelper
	{
		// Token: 0x0600164A RID: 5706
		RailResult AsyncQueryIsOwnedDlcsOnServer(List<RailDlcID> dlc_ids, string user_data);

		// Token: 0x0600164B RID: 5707
		RailResult AsyncCheckAllDlcsStateReady(string user_data);

		// Token: 0x0600164C RID: 5708
		bool IsDlcInstalled(RailDlcID dlc_id, out string installed_path);

		// Token: 0x0600164D RID: 5709
		bool IsDlcInstalled(RailDlcID dlc_id);

		// Token: 0x0600164E RID: 5710
		bool IsOwnedDlc(RailDlcID dlc_id);

		// Token: 0x0600164F RID: 5711
		uint GetDlcCount();

		// Token: 0x06001650 RID: 5712
		bool GetDlcInfo(uint index, RailDlcInfo dlc_info);

		// Token: 0x06001651 RID: 5713
		bool AsyncInstallDlc(RailDlcID dlc_id, string user_data);

		// Token: 0x06001652 RID: 5714
		bool AsyncRemoveDlc(RailDlcID dlc_id, string user_data);
	}
}
