using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Mono.Math;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000070 RID: 112
	internal class RSAManaged : RSA
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x000108A4 File Offset: 0x0000EAA4
		public RSAManaged()
			: this(1024)
		{
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000108B1 File Offset: 0x0000EAB1
		public RSAManaged(int keySize)
		{
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(384, 16384, 8);
			base.KeySize = keySize;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000108EC File Offset: 0x0000EAEC
		~RSAManaged()
		{
			this.Dispose(false);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0001091C File Offset: 0x0000EB1C
		private void GenerateKeyPair()
		{
			int num = this.KeySize + 1 >> 1;
			int num2 = this.KeySize - num;
			this.e = 65537U;
			do
			{
				this.p = BigInteger.GeneratePseudoPrime(num);
			}
			while (this.p % 65537U == 1U);
			for (;;)
			{
				this.q = BigInteger.GeneratePseudoPrime(num2);
				if (this.q % 65537U != 1U && this.p != this.q)
				{
					this.n = this.p * this.q;
					if (this.n.BitCount() == this.KeySize)
					{
						break;
					}
					if (this.p < this.q)
					{
						this.p = this.q;
					}
				}
			}
			BigInteger bigInteger = this.p - 1;
			BigInteger bigInteger2 = this.q - 1;
			BigInteger bigInteger3 = bigInteger * bigInteger2;
			this.d = this.e.ModInverse(bigInteger3);
			this.dp = this.d % bigInteger;
			this.dq = this.d % bigInteger2;
			this.qInv = this.q.ModInverse(this.p);
			this.keypairGenerated = true;
			this.isCRTpossible = true;
			if (this.KeyGenerated != null)
			{
				this.KeyGenerated(this, null);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00010A88 File Offset: 0x0000EC88
		public override int KeySize
		{
			get
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
				}
				if (this.keypairGenerated)
				{
					int num = this.n.BitCount();
					if ((num & 7) != 0)
					{
						num += 8 - (num & 7);
					}
					return num;
				}
				return base.KeySize;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00010AD6 File Offset: 0x0000ECD6
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "RSA-PKCS1-KeyEx";
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00010ADD File Offset: 0x0000ECDD
		public bool PublicOnly
		{
			get
			{
				return this.keypairGenerated && (this.d == null || this.n == null);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00010B05 File Offset: 0x0000ED05
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00010B0C File Offset: 0x0000ED0C
		public override byte[] DecryptValue(byte[] rgb)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("private key");
			}
			if (!this.keypairGenerated)
			{
				this.GenerateKeyPair();
			}
			BigInteger bigInteger = new BigInteger(rgb);
			BigInteger bigInteger2 = null;
			if (this.keyBlinding)
			{
				bigInteger2 = BigInteger.GenerateRandom(this.n.BitCount());
				bigInteger = bigInteger2.ModPow(this.e, this.n) * bigInteger % this.n;
			}
			BigInteger bigInteger6;
			if (this.isCRTpossible)
			{
				BigInteger bigInteger3 = bigInteger.ModPow(this.dp, this.p);
				BigInteger bigInteger4 = bigInteger.ModPow(this.dq, this.q);
				if (bigInteger4 > bigInteger3)
				{
					BigInteger bigInteger5 = this.p - (bigInteger4 - bigInteger3) * this.qInv % this.p;
					bigInteger6 = bigInteger4 + this.q * bigInteger5;
				}
				else
				{
					BigInteger bigInteger5 = (bigInteger3 - bigInteger4) * this.qInv % this.p;
					bigInteger6 = bigInteger4 + this.q * bigInteger5;
				}
			}
			else
			{
				if (this.PublicOnly)
				{
					throw new CryptographicException(Locale.GetText("Missing private key to decrypt value."));
				}
				bigInteger6 = bigInteger.ModPow(this.d, this.n);
			}
			if (this.keyBlinding)
			{
				bigInteger6 = bigInteger6 * bigInteger2.ModInverse(this.n) % this.n;
				bigInteger2.Clear();
			}
			byte[] paddedValue = this.GetPaddedValue(bigInteger6, this.KeySize >> 3);
			bigInteger.Clear();
			bigInteger6.Clear();
			return paddedValue;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00010CAC File Offset: 0x0000EEAC
		public override byte[] EncryptValue(byte[] rgb)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("public key");
			}
			if (!this.keypairGenerated)
			{
				this.GenerateKeyPair();
			}
			BigInteger bigInteger = new BigInteger(rgb);
			BigInteger bigInteger2 = bigInteger.ModPow(this.e, this.n);
			byte[] paddedValue = this.GetPaddedValue(bigInteger2, this.KeySize >> 3);
			bigInteger.Clear();
			bigInteger2.Clear();
			return paddedValue;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00010D10 File Offset: 0x0000EF10
		public override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (!this.keypairGenerated)
			{
				this.GenerateKeyPair();
			}
			RSAParameters rsaparameters = default(RSAParameters);
			rsaparameters.Exponent = this.e.GetBytes();
			rsaparameters.Modulus = this.n.GetBytes();
			if (includePrivateParameters)
			{
				if (this.d == null)
				{
					throw new CryptographicException("Missing private key");
				}
				rsaparameters.D = this.d.GetBytes();
				if (rsaparameters.D.Length != rsaparameters.Modulus.Length)
				{
					byte[] array = new byte[rsaparameters.Modulus.Length];
					Buffer.BlockCopy(rsaparameters.D, 0, array, array.Length - rsaparameters.D.Length, rsaparameters.D.Length);
					rsaparameters.D = array;
				}
				if (this.p != null && this.q != null && this.dp != null && this.dq != null && this.qInv != null)
				{
					int num = this.KeySize >> 4;
					rsaparameters.P = this.GetPaddedValue(this.p, num);
					rsaparameters.Q = this.GetPaddedValue(this.q, num);
					rsaparameters.DP = this.GetPaddedValue(this.dp, num);
					rsaparameters.DQ = this.GetPaddedValue(this.dq, num);
					rsaparameters.InverseQ = this.GetPaddedValue(this.qInv, num);
				}
			}
			return rsaparameters;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00010EA8 File Offset: 0x0000F0A8
		public override void ImportParameters(RSAParameters parameters)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (parameters.Exponent == null)
			{
				throw new CryptographicException(Locale.GetText("Missing Exponent"));
			}
			if (parameters.Modulus == null)
			{
				throw new CryptographicException(Locale.GetText("Missing Modulus"));
			}
			this.e = new BigInteger(parameters.Exponent);
			this.n = new BigInteger(parameters.Modulus);
			this.d = (this.dp = (this.dq = (this.qInv = (this.p = (this.q = null)))));
			if (parameters.D != null)
			{
				this.d = new BigInteger(parameters.D);
			}
			if (parameters.DP != null)
			{
				this.dp = new BigInteger(parameters.DP);
			}
			if (parameters.DQ != null)
			{
				this.dq = new BigInteger(parameters.DQ);
			}
			if (parameters.InverseQ != null)
			{
				this.qInv = new BigInteger(parameters.InverseQ);
			}
			if (parameters.P != null)
			{
				this.p = new BigInteger(parameters.P);
			}
			if (parameters.Q != null)
			{
				this.q = new BigInteger(parameters.Q);
			}
			this.keypairGenerated = true;
			bool flag = this.p != null && this.q != null && this.dp != null;
			this.isCRTpossible = flag && this.dq != null && this.qInv != null;
			if (!flag)
			{
				return;
			}
			bool flag2 = this.n == this.p * this.q;
			if (flag2)
			{
				BigInteger bigInteger = this.p - 1;
				BigInteger bigInteger2 = this.q - 1;
				BigInteger bigInteger3 = bigInteger * bigInteger2;
				BigInteger bigInteger4 = this.e.ModInverse(bigInteger3);
				flag2 = this.d == bigInteger4;
				if (!flag2 && this.isCRTpossible)
				{
					flag2 = this.dp == bigInteger4 % bigInteger && this.dq == bigInteger4 % bigInteger2 && this.qInv == this.q.ModInverse(this.p);
				}
			}
			if (!flag2)
			{
				throw new CryptographicException(Locale.GetText("Private/public key mismatch"));
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00011120 File Offset: 0x0000F320
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.d != null)
				{
					this.d.Clear();
					this.d = null;
				}
				if (this.p != null)
				{
					this.p.Clear();
					this.p = null;
				}
				if (this.q != null)
				{
					this.q.Clear();
					this.q = null;
				}
				if (this.dp != null)
				{
					this.dp.Clear();
					this.dp = null;
				}
				if (this.dq != null)
				{
					this.dq.Clear();
					this.dq = null;
				}
				if (this.qInv != null)
				{
					this.qInv.Clear();
					this.qInv = null;
				}
				if (disposing)
				{
					if (this.e != null)
					{
						this.e.Clear();
						this.e = null;
					}
					if (this.n != null)
					{
						this.n.Clear();
						this.n = null;
					}
				}
			}
			this.m_disposed = true;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000305 RID: 773 RVA: 0x00011244 File Offset: 0x0000F444
		// (remove) Token: 0x06000306 RID: 774 RVA: 0x0001127C File Offset: 0x0000F47C
		public event RSAManaged.KeyGeneratedEventHandler KeyGenerated
		{
			[CompilerGenerated]
			add
			{
				RSAManaged.KeyGeneratedEventHandler keyGeneratedEventHandler = this.KeyGenerated;
				RSAManaged.KeyGeneratedEventHandler keyGeneratedEventHandler2;
				do
				{
					keyGeneratedEventHandler2 = keyGeneratedEventHandler;
					RSAManaged.KeyGeneratedEventHandler keyGeneratedEventHandler3 = (RSAManaged.KeyGeneratedEventHandler)Delegate.Combine(keyGeneratedEventHandler2, value);
					keyGeneratedEventHandler = Interlocked.CompareExchange<RSAManaged.KeyGeneratedEventHandler>(ref this.KeyGenerated, keyGeneratedEventHandler3, keyGeneratedEventHandler2);
				}
				while (keyGeneratedEventHandler != keyGeneratedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				RSAManaged.KeyGeneratedEventHandler keyGeneratedEventHandler = this.KeyGenerated;
				RSAManaged.KeyGeneratedEventHandler keyGeneratedEventHandler2;
				do
				{
					keyGeneratedEventHandler2 = keyGeneratedEventHandler;
					RSAManaged.KeyGeneratedEventHandler keyGeneratedEventHandler3 = (RSAManaged.KeyGeneratedEventHandler)Delegate.Remove(keyGeneratedEventHandler2, value);
					keyGeneratedEventHandler = Interlocked.CompareExchange<RSAManaged.KeyGeneratedEventHandler>(ref this.KeyGenerated, keyGeneratedEventHandler3, keyGeneratedEventHandler2);
				}
				while (keyGeneratedEventHandler != keyGeneratedEventHandler2);
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000112B4 File Offset: 0x0000F4B4
		public override string ToXmlString(bool includePrivateParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			RSAParameters rsaparameters = this.ExportParameters(includePrivateParameters);
			try
			{
				stringBuilder.Append("<RSAKeyValue>");
				stringBuilder.Append("<Modulus>");
				stringBuilder.Append(Convert.ToBase64String(rsaparameters.Modulus));
				stringBuilder.Append("</Modulus>");
				stringBuilder.Append("<Exponent>");
				stringBuilder.Append(Convert.ToBase64String(rsaparameters.Exponent));
				stringBuilder.Append("</Exponent>");
				if (includePrivateParameters)
				{
					if (rsaparameters.P != null)
					{
						stringBuilder.Append("<P>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.P));
						stringBuilder.Append("</P>");
					}
					if (rsaparameters.Q != null)
					{
						stringBuilder.Append("<Q>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.Q));
						stringBuilder.Append("</Q>");
					}
					if (rsaparameters.DP != null)
					{
						stringBuilder.Append("<DP>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.DP));
						stringBuilder.Append("</DP>");
					}
					if (rsaparameters.DQ != null)
					{
						stringBuilder.Append("<DQ>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.DQ));
						stringBuilder.Append("</DQ>");
					}
					if (rsaparameters.InverseQ != null)
					{
						stringBuilder.Append("<InverseQ>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.InverseQ));
						stringBuilder.Append("</InverseQ>");
					}
					stringBuilder.Append("<D>");
					stringBuilder.Append(Convert.ToBase64String(rsaparameters.D));
					stringBuilder.Append("</D>");
				}
				stringBuilder.Append("</RSAKeyValue>");
			}
			catch
			{
				if (rsaparameters.P != null)
				{
					Array.Clear(rsaparameters.P, 0, rsaparameters.P.Length);
				}
				if (rsaparameters.Q != null)
				{
					Array.Clear(rsaparameters.Q, 0, rsaparameters.Q.Length);
				}
				if (rsaparameters.DP != null)
				{
					Array.Clear(rsaparameters.DP, 0, rsaparameters.DP.Length);
				}
				if (rsaparameters.DQ != null)
				{
					Array.Clear(rsaparameters.DQ, 0, rsaparameters.DQ.Length);
				}
				if (rsaparameters.InverseQ != null)
				{
					Array.Clear(rsaparameters.InverseQ, 0, rsaparameters.InverseQ.Length);
				}
				if (rsaparameters.D != null)
				{
					Array.Clear(rsaparameters.D, 0, rsaparameters.D.Length);
				}
				throw;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00011538 File Offset: 0x0000F738
		// (set) Token: 0x06000309 RID: 777 RVA: 0x00011540 File Offset: 0x0000F740
		public bool UseKeyBlinding
		{
			get
			{
				return this.keyBlinding;
			}
			set
			{
				this.keyBlinding = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00011549 File Offset: 0x0000F749
		public bool IsCrtPossible
		{
			get
			{
				return !this.keypairGenerated || this.isCRTpossible;
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0001155C File Offset: 0x0000F75C
		private byte[] GetPaddedValue(BigInteger value, int length)
		{
			byte[] bytes = value.GetBytes();
			if (bytes.Length >= length)
			{
				return bytes;
			}
			byte[] array = new byte[length];
			Buffer.BlockCopy(bytes, 0, array, length - bytes.Length, bytes.Length);
			Array.Clear(bytes, 0, bytes.Length);
			return array;
		}

		// Token: 0x04000E00 RID: 3584
		private const int defaultKeySize = 1024;

		// Token: 0x04000E01 RID: 3585
		private bool isCRTpossible;

		// Token: 0x04000E02 RID: 3586
		private bool keyBlinding = true;

		// Token: 0x04000E03 RID: 3587
		private bool keypairGenerated;

		// Token: 0x04000E04 RID: 3588
		private bool m_disposed;

		// Token: 0x04000E05 RID: 3589
		private BigInteger d;

		// Token: 0x04000E06 RID: 3590
		private BigInteger p;

		// Token: 0x04000E07 RID: 3591
		private BigInteger q;

		// Token: 0x04000E08 RID: 3592
		private BigInteger dp;

		// Token: 0x04000E09 RID: 3593
		private BigInteger dq;

		// Token: 0x04000E0A RID: 3594
		private BigInteger qInv;

		// Token: 0x04000E0B RID: 3595
		private BigInteger n;

		// Token: 0x04000E0C RID: 3596
		private BigInteger e;

		// Token: 0x04000E0D RID: 3597
		[CompilerGenerated]
		private RSAManaged.KeyGeneratedEventHandler KeyGenerated;

		// Token: 0x02000071 RID: 113
		// (Invoke) Token: 0x0600030D RID: 781
		public delegate void KeyGeneratedEventHandler(object sender, EventArgs e);
	}
}
