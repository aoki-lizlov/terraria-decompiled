using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000151 RID: 337
	public class RailPublishFileToUserSpaceOption
	{
		// Token: 0x0600182C RID: 6188 RVA: 0x00010EE1 File Offset: 0x0000F0E1
		public RailPublishFileToUserSpaceOption()
		{
		}

		// Token: 0x040004E2 RID: 1250
		public RailKeyValue key_value = new RailKeyValue();

		// Token: 0x040004E3 RID: 1251
		public string description;

		// Token: 0x040004E4 RID: 1252
		public List<string> tags = new List<string>();

		// Token: 0x040004E5 RID: 1253
		public EnumRailSpaceWorkShareLevel level;

		// Token: 0x040004E6 RID: 1254
		public string version;

		// Token: 0x040004E7 RID: 1255
		public string preview_path_filename;

		// Token: 0x040004E8 RID: 1256
		public EnumRailSpaceWorkType type;

		// Token: 0x040004E9 RID: 1257
		public string space_work_name;
	}
}
