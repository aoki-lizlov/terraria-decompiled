using System;
using System.Collections.Generic;

namespace ReLogic.Content
{
	// Token: 0x0200008D RID: 141
	public class RejectedAssetCollection
	{
		// Token: 0x0600032D RID: 813 RVA: 0x0000C030 File Offset: 0x0000A230
		public void Reject(string assetPath, IRejectionReason reason)
		{
			this._rejectedAssetsAndReasons.Add(assetPath, reason);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000C03F File Offset: 0x0000A23F
		public bool IsRejected(string assetPath)
		{
			return this._rejectedAssetsAndReasons.ContainsKey(assetPath);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000C04D File Offset: 0x0000A24D
		public void Clear()
		{
			this._rejectedAssetsAndReasons.Clear();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000C05C File Offset: 0x0000A25C
		public bool TryGetRejections(List<string> rejectionReasons)
		{
			foreach (KeyValuePair<string, IRejectionReason> keyValuePair in this._rejectedAssetsAndReasons)
			{
				rejectionReasons.Add(keyValuePair.Value.GetReason());
			}
			return this._rejectedAssetsAndReasons.Count > 0;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public RejectedAssetCollection()
		{
		}

		// Token: 0x040004FC RID: 1276
		private Dictionary<string, IRejectionReason> _rejectedAssetsAndReasons = new Dictionary<string, IRejectionReason>();
	}
}
