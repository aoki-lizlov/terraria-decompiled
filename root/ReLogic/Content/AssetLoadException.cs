using System;

namespace ReLogic.Content
{
	// Token: 0x0200009F RID: 159
	[Serializable]
	public class AssetLoadException : Exception
	{
		// Token: 0x060003AA RID: 938 RVA: 0x0000D1A3 File Offset: 0x0000B3A3
		private AssetLoadException(string text, Exception innerException)
			: base(text, innerException)
		{
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000D1AD File Offset: 0x0000B3AD
		public static AssetLoadException FromMissingAsset(string assetName, Exception innerException = null)
		{
			return new AssetLoadException(string.Format("Asset could not be found: \"{0}\"", assetName), innerException);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000D1C0 File Offset: 0x0000B3C0
		public static AssetLoadException FromAssetException(string assetName, Exception innerException = null)
		{
			return new AssetLoadException(string.Format("Problem loading asset: \"{0}\"", assetName), innerException);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000D1D3 File Offset: 0x0000B3D3
		public static AssetLoadException FromInvalidReader<TReaderType, TAssetType>()
		{
			return new AssetLoadException(string.Format("Asset Reader {0} is unable to read {1}", typeof(TReaderType).Name, typeof(TAssetType).Name), null);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000D203 File Offset: 0x0000B403
		public static AssetLoadException FromMissingReader(string extension)
		{
			return new AssetLoadException(string.Format("Unable to find asset reader for type {0}.", extension), null);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000D216 File Offset: 0x0000B416
		public static AssetLoadException RejectedByValidator(IRejectionReason reason)
		{
			return new AssetLoadException(reason.GetReason(), null);
		}
	}
}
