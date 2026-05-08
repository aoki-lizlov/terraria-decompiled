using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200004A RID: 74
	public class DirectConsumeAssetsFinished : EventBase
	{
		// Token: 0x060015F7 RID: 5623 RVA: 0x0000F171 File Offset: 0x0000D371
		public DirectConsumeAssetsFinished()
		{
		}

		// Token: 0x04000010 RID: 16
		public List<RailAssetItem> assets = new List<RailAssetItem>();
	}
}
