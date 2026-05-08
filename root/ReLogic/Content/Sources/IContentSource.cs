using System;
using System.Collections.Generic;
using System.IO;

namespace ReLogic.Content.Sources
{
	// Token: 0x020000A5 RID: 165
	public interface IContentSource
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060003C8 RID: 968
		// (set) Token: 0x060003C9 RID: 969
		IContentValidator ContentValidator { get; set; }

		// Token: 0x060003CA RID: 970
		bool HasAsset(string assetName);

		// Token: 0x060003CB RID: 971
		List<string> GetAllAssetsStartingWith(string assetNameStart);

		// Token: 0x060003CC RID: 972
		string GetExtension(string assetName);

		// Token: 0x060003CD RID: 973
		Stream OpenStream(string assetName);

		// Token: 0x060003CE RID: 974
		void RejectAsset(string assetName, IRejectionReason reason);

		// Token: 0x060003CF RID: 975
		void ClearRejections();

		// Token: 0x060003D0 RID: 976
		bool TryGetRejections(List<string> rejectionReasons);

		// Token: 0x060003D1 RID: 977
		void Refresh();

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060003D2 RID: 978
		string FileWatcherPath { get; }
	}
}
