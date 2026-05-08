using System;

namespace ReLogic.Content
{
	// Token: 0x0200008B RID: 139
	public interface IContentValidator
	{
		// Token: 0x0600032B RID: 811
		bool AssetIsValid<T>(T content, string contentPath, out IRejectionReason rejectionReason) where T : class;
	}
}
