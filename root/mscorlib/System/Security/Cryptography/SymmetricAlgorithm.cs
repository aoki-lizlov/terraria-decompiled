using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200048F RID: 1167
	[ComVisible(true)]
	public abstract class SymmetricAlgorithm : IDisposable
	{
		// Token: 0x0600302A RID: 12330 RVA: 0x000B0DE6 File Offset: 0x000AEFE6
		protected SymmetricAlgorithm()
		{
			this.ModeValue = CipherMode.CBC;
			this.PaddingValue = PaddingMode.PKCS7;
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000B0DFC File Offset: 0x000AEFFC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000A5D2B File Offset: 0x000A3F2B
		public void Clear()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x000B0E0C File Offset: 0x000AF00C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.KeyValue != null)
				{
					Array.Clear(this.KeyValue, 0, this.KeyValue.Length);
					this.KeyValue = null;
				}
				if (this.IVValue != null)
				{
					Array.Clear(this.IVValue, 0, this.IVValue.Length);
					this.IVValue = null;
				}
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x0600302E RID: 12334 RVA: 0x000B0E62 File Offset: 0x000AF062
		// (set) Token: 0x0600302F RID: 12335 RVA: 0x000B0E6C File Offset: 0x000AF06C
		public virtual int BlockSize
		{
			get
			{
				return this.BlockSizeValue;
			}
			set
			{
				for (int i = 0; i < this.LegalBlockSizesValue.Length; i++)
				{
					if (this.LegalBlockSizesValue[i].SkipSize == 0)
					{
						if (this.LegalBlockSizesValue[i].MinSize == value)
						{
							this.BlockSizeValue = value;
							this.IVValue = null;
							return;
						}
					}
					else
					{
						for (int j = this.LegalBlockSizesValue[i].MinSize; j <= this.LegalBlockSizesValue[i].MaxSize; j += this.LegalBlockSizesValue[i].SkipSize)
						{
							if (j == value)
							{
								if (this.BlockSizeValue != value)
								{
									this.BlockSizeValue = value;
									this.IVValue = null;
								}
								return;
							}
						}
					}
				}
				throw new CryptographicException(Environment.GetResourceString("Specified block size is not valid for this algorithm."));
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06003030 RID: 12336 RVA: 0x000B0F18 File Offset: 0x000AF118
		// (set) Token: 0x06003031 RID: 12337 RVA: 0x000B0F20 File Offset: 0x000AF120
		public virtual int FeedbackSize
		{
			get
			{
				return this.FeedbackSizeValue;
			}
			set
			{
				if (value <= 0 || value > this.BlockSizeValue || value % 8 != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Specified feedback size is invalid."));
				}
				this.FeedbackSizeValue = value;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06003032 RID: 12338 RVA: 0x000B0F4B File Offset: 0x000AF14B
		// (set) Token: 0x06003033 RID: 12339 RVA: 0x000B0F6B File Offset: 0x000AF16B
		public virtual byte[] IV
		{
			get
			{
				if (this.IVValue == null)
				{
					this.GenerateIV();
				}
				return (byte[])this.IVValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length != this.BlockSizeValue / 8)
				{
					throw new CryptographicException(Environment.GetResourceString("Specified initialization vector (IV) does not match the block size for this algorithm."));
				}
				this.IVValue = (byte[])value.Clone();
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06003034 RID: 12340 RVA: 0x0000D79E File Offset: 0x0000B99E
		// (set) Token: 0x06003035 RID: 12341 RVA: 0x000B0FAC File Offset: 0x000AF1AC
		public virtual byte[] Key
		{
			get
			{
				if (this.KeyValue == null)
				{
					this.GenerateKey();
				}
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this.ValidKeySize(value.Length * 8))
				{
					throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
				}
				this.KeyValue = (byte[])value.Clone();
				this.KeySizeValue = value.Length * 8;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06003036 RID: 12342 RVA: 0x000B1000 File Offset: 0x000AF200
		public virtual KeySizes[] LegalBlockSizes
		{
			get
			{
				return (KeySizes[])this.LegalBlockSizesValue.Clone();
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06003037 RID: 12343 RVA: 0x000B1012 File Offset: 0x000AF212
		public virtual KeySizes[] LegalKeySizes
		{
			get
			{
				return (KeySizes[])this.LegalKeySizesValue.Clone();
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06003038 RID: 12344 RVA: 0x000A90BE File Offset: 0x000A72BE
		// (set) Token: 0x06003039 RID: 12345 RVA: 0x000B1024 File Offset: 0x000AF224
		public virtual int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (!this.ValidKeySize(value))
				{
					throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
				}
				this.KeySizeValue = value;
				this.KeyValue = null;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x0600303A RID: 12346 RVA: 0x000B104D File Offset: 0x000AF24D
		// (set) Token: 0x0600303B RID: 12347 RVA: 0x000B1055 File Offset: 0x000AF255
		public virtual CipherMode Mode
		{
			get
			{
				return this.ModeValue;
			}
			set
			{
				if (value < CipherMode.CBC || CipherMode.CFB < value)
				{
					throw new CryptographicException(Environment.GetResourceString("Specified cipher mode is not valid for this algorithm."));
				}
				this.ModeValue = value;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x0600303C RID: 12348 RVA: 0x000B1076 File Offset: 0x000AF276
		// (set) Token: 0x0600303D RID: 12349 RVA: 0x000B107E File Offset: 0x000AF27E
		public virtual PaddingMode Padding
		{
			get
			{
				return this.PaddingValue;
			}
			set
			{
				if (value < PaddingMode.None || PaddingMode.ISO10126 < value)
				{
					throw new CryptographicException(Environment.GetResourceString("Specified padding mode is not valid for this algorithm."));
				}
				this.PaddingValue = value;
			}
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000B10A0 File Offset: 0x000AF2A0
		public bool ValidKeySize(int bitLength)
		{
			KeySizes[] legalKeySizes = this.LegalKeySizes;
			if (legalKeySizes == null)
			{
				return false;
			}
			for (int i = 0; i < legalKeySizes.Length; i++)
			{
				if (legalKeySizes[i].SkipSize == 0)
				{
					if (legalKeySizes[i].MinSize == bitLength)
					{
						return true;
					}
				}
				else
				{
					for (int j = legalKeySizes[i].MinSize; j <= legalKeySizes[i].MaxSize; j += legalKeySizes[i].SkipSize)
					{
						if (j == bitLength)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x000B1106 File Offset: 0x000AF306
		public static SymmetricAlgorithm Create()
		{
			return SymmetricAlgorithm.Create("System.Security.Cryptography.SymmetricAlgorithm");
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x000B1112 File Offset: 0x000AF312
		public static SymmetricAlgorithm Create(string algName)
		{
			return (SymmetricAlgorithm)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x000B111F File Offset: 0x000AF31F
		public virtual ICryptoTransform CreateEncryptor()
		{
			return this.CreateEncryptor(this.Key, this.IV);
		}

		// Token: 0x06003042 RID: 12354
		public abstract ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV);

		// Token: 0x06003043 RID: 12355 RVA: 0x000B1133 File Offset: 0x000AF333
		public virtual ICryptoTransform CreateDecryptor()
		{
			return this.CreateDecryptor(this.Key, this.IV);
		}

		// Token: 0x06003044 RID: 12356
		public abstract ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV);

		// Token: 0x06003045 RID: 12357
		public abstract void GenerateKey();

		// Token: 0x06003046 RID: 12358
		public abstract void GenerateIV();

		// Token: 0x040020B9 RID: 8377
		protected int BlockSizeValue;

		// Token: 0x040020BA RID: 8378
		protected int FeedbackSizeValue;

		// Token: 0x040020BB RID: 8379
		protected byte[] IVValue;

		// Token: 0x040020BC RID: 8380
		protected byte[] KeyValue;

		// Token: 0x040020BD RID: 8381
		protected KeySizes[] LegalBlockSizesValue;

		// Token: 0x040020BE RID: 8382
		protected KeySizes[] LegalKeySizesValue;

		// Token: 0x040020BF RID: 8383
		protected int KeySizeValue;

		// Token: 0x040020C0 RID: 8384
		protected CipherMode ModeValue;

		// Token: 0x040020C1 RID: 8385
		protected PaddingMode PaddingValue;
	}
}
