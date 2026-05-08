using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000473 RID: 1139
	[ComVisible(true)]
	public abstract class Rijndael : SymmetricAlgorithm
	{
		// Token: 0x06002F22 RID: 12066 RVA: 0x000A923B File Offset: 0x000A743B
		protected Rijndael()
		{
			this.KeySizeValue = 256;
			this.BlockSizeValue = 128;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = Rijndael.s_legalBlockSizes;
			this.LegalKeySizesValue = Rijndael.s_legalKeySizes;
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x000A927B File Offset: 0x000A747B
		public new static Rijndael Create()
		{
			return Rijndael.Create("System.Security.Cryptography.Rijndael");
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x000A9287 File Offset: 0x000A7487
		public new static Rijndael Create(string algName)
		{
			return (Rijndael)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x000A9294 File Offset: 0x000A7494
		// Note: this type is marked as 'beforefieldinit'.
		static Rijndael()
		{
		}

		// Token: 0x04002062 RID: 8290
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(128, 256, 64)
		};

		// Token: 0x04002063 RID: 8291
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(128, 256, 64)
		};
	}
}
