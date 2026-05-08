using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200047D RID: 1149
	[ComVisible(true)]
	public class RSAOAEPKeyExchangeFormatter : AsymmetricKeyExchangeFormatter
	{
		// Token: 0x06002FB0 RID: 12208 RVA: 0x000AE984 File Offset: 0x000ACB84
		public RSAOAEPKeyExchangeFormatter()
		{
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000AE98C File Offset: 0x000ACB8C
		public RSAOAEPKeyExchangeFormatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x000AE9AE File Offset: 0x000ACBAE
		// (set) Token: 0x06002FB3 RID: 12211 RVA: 0x000AE9CA File Offset: 0x000ACBCA
		public byte[] Parameter
		{
			get
			{
				if (this.ParameterValue != null)
				{
					return (byte[])this.ParameterValue.Clone();
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.ParameterValue = (byte[])value.Clone();
					return;
				}
				this.ParameterValue = null;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override string Parameters
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06002FB5 RID: 12213 RVA: 0x000AE9E8 File Offset: 0x000ACBE8
		// (set) Token: 0x06002FB6 RID: 12214 RVA: 0x000AE9F0 File Offset: 0x000ACBF0
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

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000AE9F9 File Offset: 0x000ACBF9
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
			this._rsaOverridesEncrypt = null;
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000AEA24 File Offset: 0x000ACC24
		[SecuritySafeCritical]
		public override byte[] CreateKeyExchange(byte[] rgbData)
		{
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("No asymmetric key object has been associated with this formatter object."));
			}
			if (this.OverridesEncrypt)
			{
				return this._rsaKey.Encrypt(rgbData, RSAEncryptionPadding.OaepSHA1);
			}
			return Utils.RsaOaepEncrypt(this._rsaKey, SHA1.Create(), new PKCS1MaskGenerationMethod(), RandomNumberGenerator.Create(), rgbData);
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000AEA7E File Offset: 0x000ACC7E
		public override byte[] CreateKeyExchange(byte[] rgbData, Type symAlgType)
		{
			return this.CreateKeyExchange(rgbData);
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06002FBA RID: 12218 RVA: 0x000AEA88 File Offset: 0x000ACC88
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

		// Token: 0x04002097 RID: 8343
		private byte[] ParameterValue;

		// Token: 0x04002098 RID: 8344
		private RSA _rsaKey;

		// Token: 0x04002099 RID: 8345
		private bool? _rsaOverridesEncrypt;

		// Token: 0x0400209A RID: 8346
		private RandomNumberGenerator RngValue;
	}
}
