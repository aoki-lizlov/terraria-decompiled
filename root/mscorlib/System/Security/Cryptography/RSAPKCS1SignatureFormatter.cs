using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	// Token: 0x0200049F RID: 1183
	[ComVisible(true)]
	public class RSAPKCS1SignatureFormatter : AsymmetricSignatureFormatter
	{
		// Token: 0x060030E7 RID: 12519 RVA: 0x000B59E0 File Offset: 0x000B3BE0
		public RSAPKCS1SignatureFormatter()
		{
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000B59E8 File Offset: 0x000B3BE8
		public RSAPKCS1SignatureFormatter(AsymmetricAlgorithm key)
		{
			this.SetKey(key);
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000B59F8 File Offset: 0x000B3BF8
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			if (this.rsa == null)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("No key pair available."));
			}
			if (this.hash == null)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("Missing hash algorithm."));
			}
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			return PKCS1.Sign_v15(this.rsa, this.hash, rgbHash);
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000B5A55 File Offset: 0x000B3C55
		public override void SetHashAlgorithm(string strName)
		{
			if (strName == null)
			{
				throw new ArgumentNullException("strName");
			}
			this.hash = strName;
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000B5A6C File Offset: 0x000B3C6C
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.rsa = (RSA)key;
		}

		// Token: 0x040021F4 RID: 8692
		private RSA rsa;

		// Token: 0x040021F5 RID: 8693
		private string hash;
	}
}
