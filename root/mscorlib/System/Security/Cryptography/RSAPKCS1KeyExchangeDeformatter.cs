using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200047E RID: 1150
	[ComVisible(true)]
	public class RSAPKCS1KeyExchangeDeformatter : AsymmetricKeyExchangeDeformatter
	{
		// Token: 0x06002FBB RID: 12219 RVA: 0x000AE878 File Offset: 0x000ACA78
		public RSAPKCS1KeyExchangeDeformatter()
		{
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000AEAE8 File Offset: 0x000ACCE8
		public RSAPKCS1KeyExchangeDeformatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06002FBD RID: 12221 RVA: 0x000AEB0A File Offset: 0x000ACD0A
		// (set) Token: 0x06002FBE RID: 12222 RVA: 0x000AEB12 File Offset: 0x000ACD12
		public RandomNumberGenerator RNG
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

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06002FBF RID: 12223 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		// (set) Token: 0x06002FC0 RID: 12224 RVA: 0x00004088 File Offset: 0x00002288
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

		// Token: 0x06002FC1 RID: 12225 RVA: 0x000AEB1C File Offset: 0x000ACD1C
		public override byte[] DecryptKeyExchange(byte[] rgbIn)
		{
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("No asymmetric key object has been associated with this formatter object."));
			}
			byte[] array;
			if (this.OverridesDecrypt)
			{
				array = this._rsaKey.Decrypt(rgbIn, RSAEncryptionPadding.Pkcs1);
			}
			else
			{
				byte[] array2 = this._rsaKey.DecryptValue(rgbIn);
				int num = 2;
				while (num < array2.Length && array2[num] != 0)
				{
					num++;
				}
				if (num >= array2.Length)
				{
					throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Error occurred while decoding PKCS1 padding."));
				}
				num++;
				array = new byte[array2.Length - num];
				Buffer.InternalBlockCopy(array2, num, array, 0, array.Length);
			}
			return array;
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000AEBB0 File Offset: 0x000ACDB0
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
			this._rsaOverridesDecrypt = null;
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x000AEBD8 File Offset: 0x000ACDD8
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

		// Token: 0x0400209B RID: 8347
		private RSA _rsaKey;

		// Token: 0x0400209C RID: 8348
		private bool? _rsaOverridesDecrypt;

		// Token: 0x0400209D RID: 8349
		private RandomNumberGenerator RngValue;
	}
}
