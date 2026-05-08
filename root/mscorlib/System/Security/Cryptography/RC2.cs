using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000471 RID: 1137
	[ComVisible(true)]
	public abstract class RC2 : SymmetricAlgorithm
	{
		// Token: 0x06002F10 RID: 12048 RVA: 0x000A9002 File Offset: 0x000A7202
		protected RC2()
		{
			this.KeySizeValue = 128;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = RC2.s_legalBlockSizes;
			this.LegalKeySizesValue = RC2.s_legalKeySizes;
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06002F11 RID: 12049 RVA: 0x000A903F File Offset: 0x000A723F
		// (set) Token: 0x06002F12 RID: 12050 RVA: 0x000A9058 File Offset: 0x000A7258
		public virtual int EffectiveKeySize
		{
			get
			{
				if (this.EffectiveKeySizeValue == 0)
				{
					return this.KeySizeValue;
				}
				return this.EffectiveKeySizeValue;
			}
			set
			{
				if (value > this.KeySizeValue)
				{
					throw new CryptographicException(Environment.GetResourceString("EffectiveKeySize value must be at least as large as the KeySize value."));
				}
				if (value == 0)
				{
					this.EffectiveKeySizeValue = value;
					return;
				}
				if (value < 40)
				{
					throw new CryptographicException(Environment.GetResourceString("EffectiveKeySize value must be at least 40 bits."));
				}
				if (base.ValidKeySize(value))
				{
					this.EffectiveKeySizeValue = value;
					return;
				}
				throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06002F13 RID: 12051 RVA: 0x000A90BE File Offset: 0x000A72BE
		// (set) Token: 0x06002F14 RID: 12052 RVA: 0x000A90C6 File Offset: 0x000A72C6
		public override int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (value < this.EffectiveKeySizeValue)
				{
					throw new CryptographicException(Environment.GetResourceString("EffectiveKeySize value must be at least as large as the KeySize value."));
				}
				base.KeySize = value;
			}
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x000A90E8 File Offset: 0x000A72E8
		public new static RC2 Create()
		{
			return RC2.Create("System.Security.Cryptography.RC2");
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000A90F4 File Offset: 0x000A72F4
		public new static RC2 Create(string AlgName)
		{
			return (RC2)CryptoConfig.CreateFromName(AlgName);
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000A9101 File Offset: 0x000A7301
		// Note: this type is marked as 'beforefieldinit'.
		static RC2()
		{
		}

		// Token: 0x0400205D RID: 8285
		protected int EffectiveKeySizeValue;

		// Token: 0x0400205E RID: 8286
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x0400205F RID: 8287
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 1024, 8)
		};
	}
}
