using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200047C RID: 1148
	[ComVisible(true)]
	public class RSAOAEPKeyExchangeDeformatter : AsymmetricKeyExchangeDeformatter
	{
		// Token: 0x06002FA9 RID: 12201 RVA: 0x000AE878 File Offset: 0x000ACA78
		public RSAOAEPKeyExchangeDeformatter()
		{
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x000AE880 File Offset: 0x000ACA80
		public RSAOAEPKeyExchangeDeformatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06002FAB RID: 12203 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		// (set) Token: 0x06002FAC RID: 12204 RVA: 0x00004088 File Offset: 0x00002288
		public override string Parameters
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x000AE8A4 File Offset: 0x000ACAA4
		[SecuritySafeCritical]
		public override byte[] DecryptKeyExchange(byte[] rgbData)
		{
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("No asymmetric key object has been associated with this formatter object."));
			}
			if (this.OverridesDecrypt)
			{
				return this._rsaKey.Decrypt(rgbData, RSAEncryptionPadding.OaepSHA1);
			}
			return Utils.RsaOaepDecrypt(this._rsaKey, SHA1.Create(), new PKCS1MaskGenerationMethod(), rgbData);
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x000AE8F9 File Offset: 0x000ACAF9
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
			this._rsaOverridesDecrypt = null;
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06002FAF RID: 12207 RVA: 0x000AE924 File Offset: 0x000ACB24
		private bool OverridesDecrypt
		{
			get
			{
				if (this._rsaOverridesDecrypt == null)
				{
					this._rsaOverridesDecrypt = new bool?(Utils.DoesRsaKeyOverride(this._rsaKey, "Decrypt", new Type[]
					{
						typeof(byte[]),
						typeof(RSAEncryptionPadding)
					}));
				}
				return this._rsaOverridesDecrypt.Value;
			}
		}

		// Token: 0x04002095 RID: 8341
		private RSA _rsaKey;

		// Token: 0x04002096 RID: 8342
		private bool? _rsaOverridesDecrypt;
	}
}
