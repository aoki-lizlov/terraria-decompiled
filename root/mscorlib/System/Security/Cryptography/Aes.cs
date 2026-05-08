using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000447 RID: 1095
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	public abstract class Aes : SymmetricAlgorithm
	{
		// Token: 0x06002DE7 RID: 11751 RVA: 0x000A61C0 File Offset: 0x000A43C0
		protected Aes()
		{
			this.LegalBlockSizesValue = Aes.s_legalBlockSizes;
			this.LegalKeySizesValue = Aes.s_legalKeySizes;
			this.BlockSizeValue = 128;
			this.FeedbackSizeValue = 8;
			this.KeySizeValue = 256;
			this.ModeValue = CipherMode.CBC;
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x000A620D File Offset: 0x000A440D
		public new static Aes Create()
		{
			return Aes.Create("AES");
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x000A6219 File Offset: 0x000A4419
		public new static Aes Create(string algorithmName)
		{
			if (algorithmName == null)
			{
				throw new ArgumentNullException("algorithmName");
			}
			return CryptoConfig.CreateFromName(algorithmName) as Aes;
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x000A6234 File Offset: 0x000A4434
		// Note: this type is marked as 'beforefieldinit'.
		static Aes()
		{
		}

		// Token: 0x04001FF4 RID: 8180
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(128, 128, 0)
		};

		// Token: 0x04001FF5 RID: 8181
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(128, 256, 64)
		};
	}
}
