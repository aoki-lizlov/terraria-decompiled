using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000472 RID: 1138
	[ComVisible(true)]
	public sealed class RC2CryptoServiceProvider : RC2
	{
		// Token: 0x06002F18 RID: 12056 RVA: 0x000A9138 File Offset: 0x000A7338
		[SecuritySafeCritical]
		public RC2CryptoServiceProvider()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms)
			{
				throw new InvalidOperationException(Environment.GetResourceString("This implementation is not part of the Windows Platform FIPS validated cryptographic algorithms."));
			}
			if (!Utils.HasAlgorithm(26114, 0))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptographic service provider (CSP) could not be found for this algorithm."));
			}
			this.LegalKeySizesValue = RC2CryptoServiceProvider.s_legalKeySizes;
			this.FeedbackSizeValue = 8;
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06002F19 RID: 12057 RVA: 0x000A90BE File Offset: 0x000A72BE
		// (set) Token: 0x06002F1A RID: 12058 RVA: 0x000A9191 File Offset: 0x000A7391
		public override int EffectiveKeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (value != this.KeySizeValue)
				{
					throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("EffectiveKeySize must be the same as KeySize in this implementation."));
				}
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06002F1B RID: 12059 RVA: 0x000A91AC File Offset: 0x000A73AC
		// (set) Token: 0x06002F1C RID: 12060 RVA: 0x000A91B4 File Offset: 0x000A73B4
		[ComVisible(false)]
		public bool UseSalt
		{
			get
			{
				return this.m_use40bitSalt;
			}
			set
			{
				this.m_use40bitSalt = value;
			}
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x000A91BD File Offset: 0x000A73BD
		[SecuritySafeCritical]
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			if (this.m_use40bitSalt)
			{
				throw new NotImplementedException("UseSalt=true is not implemented on Mono yet");
			}
			return new RC2Transform(this, true, rgbKey, rgbIV);
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000A91DB File Offset: 0x000A73DB
		[SecuritySafeCritical]
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			if (this.m_use40bitSalt)
			{
				throw new NotImplementedException("UseSalt=true is not implemented on Mono yet");
			}
			return new RC2Transform(this, false, rgbKey, rgbIV);
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000A91F9 File Offset: 0x000A73F9
		public override void GenerateKey()
		{
			this.KeyValue = new byte[this.KeySizeValue / 8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x000A7029 File Offset: 0x000A5229
		public override void GenerateIV()
		{
			this.IVValue = new byte[8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.IVValue);
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x000A921E File Offset: 0x000A741E
		// Note: this type is marked as 'beforefieldinit'.
		static RC2CryptoServiceProvider()
		{
		}

		// Token: 0x04002060 RID: 8288
		private bool m_use40bitSalt;

		// Token: 0x04002061 RID: 8289
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 128, 8)
		};
	}
}
