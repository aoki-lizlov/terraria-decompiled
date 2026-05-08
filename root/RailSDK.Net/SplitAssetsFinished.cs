using System;

namespace rail
{
	// Token: 0x02000057 RID: 87
	public class SplitAssetsFinished : EventBase
	{
		// Token: 0x06001601 RID: 5633 RVA: 0x0000F049 File Offset: 0x0000D249
		public SplitAssetsFinished()
		{
		}

		// Token: 0x0400003A RID: 58
		public ulong source_asset;

		// Token: 0x0400003B RID: 59
		public uint to_quantity;

		// Token: 0x0400003C RID: 60
		public ulong new_asset_id;
	}
}
