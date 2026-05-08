using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000056 RID: 86
	public class RequestAllAssetsFinished : EventBase
	{
		// Token: 0x06001600 RID: 5632 RVA: 0x0000F1E6 File Offset: 0x0000D3E6
		public RequestAllAssetsFinished()
		{
		}

		// Token: 0x04000039 RID: 57
		public List<RailAssetInfo> assetinfo_list = new List<RailAssetInfo>();
	}
}
