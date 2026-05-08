using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000185 RID: 389
	public class RailSpaceWorkDescriptor
	{
		// Token: 0x060018A3 RID: 6307 RVA: 0x00011094 File Offset: 0x0000F294
		public RailSpaceWorkDescriptor()
		{
		}

		// Token: 0x04000575 RID: 1397
		public List<RailSpaceWorkVoteDetail> vote_details = new List<RailSpaceWorkVoteDetail>();

		// Token: 0x04000576 RID: 1398
		public string description;

		// Token: 0x04000577 RID: 1399
		public string preview_url;

		// Token: 0x04000578 RID: 1400
		public SpaceWorkID id = new SpaceWorkID();

		// Token: 0x04000579 RID: 1401
		public string detail_url;

		// Token: 0x0400057A RID: 1402
		public List<RailID> uploader_ids = new List<RailID>();

		// Token: 0x0400057B RID: 1403
		public string name;
	}
}
