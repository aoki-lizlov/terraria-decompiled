using System;

namespace rail
{
	// Token: 0x02000058 RID: 88
	public class SplitAssetsToFinished : EventBase
	{
		// Token: 0x06001602 RID: 5634 RVA: 0x0000F049 File Offset: 0x0000D249
		public SplitAssetsToFinished()
		{
		}

		// Token: 0x0400003D RID: 61
		public ulong source_asset;

		// Token: 0x0400003E RID: 62
		public uint to_quantity;

		// Token: 0x0400003F RID: 63
		public ulong split_to_asset_id;
	}
}
