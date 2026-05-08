using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000131 RID: 305
	public interface IRailScreenshot : IRailComponent
	{
		// Token: 0x060017D5 RID: 6101
		bool SetLocation(string location);

		// Token: 0x060017D6 RID: 6102
		bool SetUsers(List<RailID> users);

		// Token: 0x060017D7 RID: 6103
		bool AssociatePublishedFiles(List<SpaceWorkID> work_files);

		// Token: 0x060017D8 RID: 6104
		RailResult AsyncPublishScreenshot(string work_name, string user_data);
	}
}
