using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200006F RID: 111
	internal abstract class RC4 : SymmetricAlgorithm
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x000107F9 File Offset: 0x0000E9F9
		public RC4()
		{
			this.KeySizeValue = 128;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = RC4.s_legalBlockSizes;
			this.LegalKeySizesValue = RC4.s_legalKeySizes;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00010836 File Offset: 0x0000EA36
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00004088 File Offset: 0x00002288
		public override byte[] IV
		{
			get
			{
				return new byte[0];
			}
			set
			{
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0001083E File Offset: 0x0000EA3E
		public new static RC4 Create()
		{
			return RC4.Create("RC4");
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0001084C File Offset: 0x0000EA4C
		public new static RC4 Create(string algName)
		{
			object obj = CryptoConfig.CreateFromName(algName);
			if (obj == null)
			{
				obj = new ARC4Managed();
			}
			return (RC4)obj;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0001086F File Offset: 0x0000EA6F
		// Note: this type is marked as 'beforefieldinit'.
		static RC4()
		{
		}

		// Token: 0x04000DFE RID: 3582
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x04000DFF RID: 3583
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 2048, 8)
		};
	}
}
