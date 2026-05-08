using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000187 RID: 391
	public class RailSpaceWorkSearchFilter
	{
		// Token: 0x060018A5 RID: 6309 RVA: 0x000110FC File Offset: 0x0000F2FC
		public RailSpaceWorkSearchFilter()
		{
		}

		// Token: 0x04000581 RID: 1409
		public string search_text;

		// Token: 0x04000582 RID: 1410
		public List<RailKeyValue> required_metadata = new List<RailKeyValue>();

		// Token: 0x04000583 RID: 1411
		public bool match_all_required_metadata;

		// Token: 0x04000584 RID: 1412
		public List<string> required_tags = new List<string>();

		// Token: 0x04000585 RID: 1413
		public List<RailKeyValue> excluded_metadata = new List<RailKeyValue>();

		// Token: 0x04000586 RID: 1414
		public List<string> excluded_tags = new List<string>();
	}
}
