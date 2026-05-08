using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	// Token: 0x0200049E RID: 1182
	[ComVisible(true)]
	public class RSAPKCS1SignatureDeformatter : AsymmetricSignatureDeformatter
	{
		// Token: 0x060030E2 RID: 12514 RVA: 0x000B5929 File Offset: 0x000B3B29
		public RSAPKCS1SignatureDeformatter()
		{
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000B5931 File Offset: 0x000B3B31
		public RSAPKCS1SignatureDeformatter(AsymmetricAlgorithm key)
		{
			this.SetKey(key);
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000B5940 File Offset: 0x000B3B40
		public override void SetHashAlgorithm(string strName)
		{
			if (strName == null)
			{
				throw new ArgumentNullException("strName");
			}
			this.hashName = strName;
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x000B5957 File Offset: 0x000B3B57
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.rsa = (RSA)key;
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000B5974 File Offset: 0x000B3B74
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			if (this.rsa == null)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("No public key available."));
			}
			if (this.hashName == null)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("Missing hash algorithm."));
			}
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			return PKCS1.Verify_v15(this.rsa, this.hashName, rgbHash, rgbSignature);
		}

		// Token: 0x040021F2 RID: 8690
		private RSA rsa;

		// Token: 0x040021F3 RID: 8691
		private string hashName;
	}
}
