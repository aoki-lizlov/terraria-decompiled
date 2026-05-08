using System;

namespace rail
{
	// Token: 0x02000048 RID: 72
	public class CompleteConsumeAssetsFinished : EventBase
	{
		// Token: 0x060015F5 RID: 5621 RVA: 0x0000F15E File Offset: 0x0000D35E
		public CompleteConsumeAssetsFinished()
		{
		}

		// Token: 0x0400000F RID: 15
		public RailAssetItem asset_item = new RailAssetItem();
	}
}
