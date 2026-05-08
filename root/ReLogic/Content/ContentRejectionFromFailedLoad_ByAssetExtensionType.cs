using System;

namespace ReLogic.Content
{
	// Token: 0x0200008E RID: 142
	public class ContentRejectionFromFailedLoad_ByAssetExtensionType : IRejectionReason
	{
		// Token: 0x06000332 RID: 818 RVA: 0x0000C0DB File Offset: 0x0000A2DB
		public string GetReason()
		{
			return "Only textures of type '.png' and '.xnb' may be loaded.";
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000448A File Offset: 0x0000268A
		public ContentRejectionFromFailedLoad_ByAssetExtensionType()
		{
		}
	}
}
