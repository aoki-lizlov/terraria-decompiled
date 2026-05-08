using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200048E RID: 1166
	internal class DSASignatureDescription : SignatureDescription
	{
		// Token: 0x06003029 RID: 12329 RVA: 0x000B0DB2 File Offset: 0x000AEFB2
		public DSASignatureDescription()
		{
			base.KeyAlgorithm = "System.Security.Cryptography.DSACryptoServiceProvider";
			base.DigestAlgorithm = "System.Security.Cryptography.SHA1CryptoServiceProvider";
			base.FormatterAlgorithm = "System.Security.Cryptography.DSASignatureFormatter";
			base.DeformatterAlgorithm = "System.Security.Cryptography.DSASignatureDeformatter";
		}
	}
}
