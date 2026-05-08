using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200005A RID: 90
	public class UpdateAssetsPropertyFinished : EventBase
	{
		// Token: 0x06001604 RID: 5636 RVA: 0x0000F1F9 File Offset: 0x0000D3F9
		public UpdateAssetsPropertyFinished()
		{
		}

		// Token: 0x04000041 RID: 65
		public List<RailAssetProperty> asset_property_list = new List<RailAssetProperty>();
	}
}
