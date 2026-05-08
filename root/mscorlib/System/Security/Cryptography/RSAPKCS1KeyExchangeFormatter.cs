using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200047F RID: 1151
	[ComVisible(true)]
	public class RSAPKCS1KeyExchangeFormatter : AsymmetricKeyExchangeFormatter
	{
		// Token: 0x06002FC4 RID: 12228 RVA: 0x000AE984 File Offset: 0x000ACB84
		public RSAPKCS1KeyExchangeFormatter()
		{
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x000AEC38 File Offset: 0x000ACE38
		public RSAPKCS1KeyExchangeFormatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06002FC6 RID: 12230 RVA: 0x000AEC5A File Offset: 0x000ACE5A
		public override string Parameters
		{
			get
			{
				return "<enc:KeyEncryptionMethod enc:Algorithm=\"http://www.microsoft.com/xml/security/algorithm/PKCS1-v1.5-KeyEx\" xmlns:enc=\"http://www.microsoft.com/xml/security/encryption/v1.0\" />";
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06002FC7 RID: 12231 RVA: 0x000AEC61 File Offset: 0x000ACE61
		// (set) Token: 0x06002FC8 RID: 12232 RVA: 0x000AEC69 File Offset: 0x000ACE69
		public RandomNumberGenerator Rng
		{
			get
			{
				return this.RngValue;
			}
			set
			{
				this.RngValue = value;
			}
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000AEC72 File Offset: 0x000ACE72
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
			this._rsaOverridesEncrypt = null;
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000AEC9C File Offset: 0x000ACE9C
		public override byte[] CreateKeyExchange(byte[] rgbData)
		{
			if (rgbData == null)
			{
				throw new ArgumentNullException("rgbData");
			}
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("No asymmetric key object has been associated with this formatter object."));
			}
			byte[] array;
			if (this.OverridesEncrypt)
			{
				array = this._rsaKey.Encrypt(rgbData, RSAEncryptionPadding.Pkcs1);
			}
			else
			{
				int num = this._rsaKey.KeySize / 8;
				if (rgbData.Length + 11 > num)
				{
					throw new CryptographicException(Environment.GetResourceString("The data to be encrypted exceeds the maximum for this modulus of {0} bytes.", new object[] { num - 11 }));
				}
				byte[] array2 = new byte[num];
				if (this.RngValue == null)
				{
					this.RngValue = RandomNumberGenerator.Create();
				}
				this.Rng.GetNonZeroBytes(array2);
				array2[0] = 0;
				array2[1] = 2;
				array2[num - rgbData.Length - 1] = 0;
				Buffer.InternalBlockCopy(rgbData, 0, array2, num - rgbData.Length, rgbData.Length);
				array = this._rsaKey.EncryptValue(array2);
			}
			return array;
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x000AEA7E File Offset: 0x000ACC7E
		public override byte[] CreateKeyExchange(byte[] rgbData, Type symAlgType)
		{
			return this.CreateKeyExchange(rgbData);
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06002FCC RID: 12236 RVA: 0x000AED80 File Offset: 0x000ACF80
		private bool OverridesEncrypt
		{
			get
			{
				if (this._rsaOverridesEncrypt == null)
				{
					this._rsaOverridesEncrypt = new bool?(Utils.DoesRsaKeyOverride(this._rsaKey, "Encrypt", new Type[]
					{
						typeof(byte[]),
						typeof(RSAEncryptionPadding)
					}));
				}
				return this._rsaOverridesEncrypt.Value;
			}
		}

		// Token: 0x0400209E RID: 8350
		private RandomNumberGenerator RngValue;

		// Token: 0x0400209F RID: 8351
		private RSA _rsaKey;

		// Token: 0x040020A0 RID: 8352
		private bool? _rsaOverridesEncrypt;
	}
}
