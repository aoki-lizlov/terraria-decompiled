using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using Mono.Math;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000073 RID: 115
	internal class DSAManaged : DSA
	{
		// Token: 0x06000327 RID: 807 RVA: 0x0001207C File Offset: 0x0001027C
		public DSAManaged()
			: this(1024)
		{
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00012089 File Offset: 0x00010289
		public DSAManaged(int dwKeySize)
		{
			this.KeySizeValue = dwKeySize;
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(512, 1024, 64);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000120C0 File Offset: 0x000102C0
		~DSAManaged()
		{
			this.Dispose(false);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000120F0 File Offset: 0x000102F0
		private void Generate()
		{
			this.GenerateParams(base.KeySize);
			this.GenerateKeyPair();
			this.keypairGenerated = true;
			if (this.KeyGenerated != null)
			{
				this.KeyGenerated(this, null);
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00012120 File Offset: 0x00010320
		private void GenerateKeyPair()
		{
			this.x = BigInteger.GenerateRandom(160);
			while (this.x == 0U || this.x >= this.q)
			{
				this.x.Randomize();
			}
			this.y = this.g.ModPow(this.x, this.p);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00012188 File Offset: 0x00010388
		private void add(byte[] a, byte[] b, int value)
		{
			uint num = (uint)((int)(b[b.Length - 1] & byte.MaxValue) + value);
			a[b.Length - 1] = (byte)num;
			num >>= 8;
			for (int i = b.Length - 2; i >= 0; i--)
			{
				num += (uint)(b[i] & byte.MaxValue);
				a[i] = (byte)num;
				num >>= 8;
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000121D8 File Offset: 0x000103D8
		private void GenerateParams(int keyLength)
		{
			byte[] array = new byte[20];
			byte[] array2 = new byte[20];
			byte[] array3 = new byte[20];
			byte[] array4 = new byte[20];
			SHA1 sha = SHA1.Create();
			int num = (keyLength - 1) / 160;
			byte[] array5 = new byte[keyLength / 8];
			bool flag = false;
			while (!flag)
			{
				do
				{
					this.Random.GetBytes(array);
					array2 = sha.ComputeHash(array);
					Array.Copy(array, 0, array3, 0, array.Length);
					this.add(array3, array, 1);
					array3 = sha.ComputeHash(array3);
					for (int num2 = 0; num2 != array4.Length; num2++)
					{
						array4[num2] = array2[num2] ^ array3[num2];
					}
					byte[] array6 = array4;
					int num3 = 0;
					array6[num3] |= 128;
					byte[] array7 = array4;
					int num4 = 19;
					array7[num4] |= 1;
					this.q = new BigInteger(array4);
				}
				while (!this.q.IsProbablePrime());
				this.counter = 0;
				int num5 = 2;
				while (this.counter < 4096)
				{
					for (int i = 0; i < num; i++)
					{
						this.add(array2, array, num5 + i);
						array2 = sha.ComputeHash(array2);
						Array.Copy(array2, 0, array5, array5.Length - (i + 1) * array2.Length, array2.Length);
					}
					this.add(array2, array, num5 + num);
					array2 = sha.ComputeHash(array2);
					Array.Copy(array2, array2.Length - (array5.Length - num * array2.Length), array5, 0, array5.Length - num * array2.Length);
					byte[] array8 = array5;
					int num6 = 0;
					array8[num6] |= 128;
					BigInteger bigInteger = new BigInteger(array5);
					BigInteger bigInteger2 = bigInteger % (this.q * 2);
					this.p = bigInteger - (bigInteger2 - 1);
					if (this.p.TestBit((uint)(keyLength - 1)) && this.p.IsProbablePrime())
					{
						flag = true;
						break;
					}
					this.counter++;
					num5 += num + 1;
				}
			}
			BigInteger bigInteger3 = (this.p - 1) / this.q;
			for (;;)
			{
				BigInteger bigInteger4 = BigInteger.GenerateRandom(keyLength);
				if (!(bigInteger4 <= 1) && !(bigInteger4 >= this.p - 1))
				{
					this.g = bigInteger4.ModPow(bigInteger3, this.p);
					if (!(this.g <= 1))
					{
						break;
					}
				}
			}
			this.seed = new BigInteger(array);
			this.j = (this.p - 1) / this.q;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0001247E File Offset: 0x0001067E
		private RandomNumberGenerator Random
		{
			get
			{
				if (this.rng == null)
				{
					this.rng = RandomNumberGenerator.Create();
				}
				return this.rng;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00012499 File Offset: 0x00010699
		public override int KeySize
		{
			get
			{
				if (this.keypairGenerated)
				{
					return this.p.BitCount();
				}
				return base.KeySize;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000331 RID: 817 RVA: 0x000124B5 File Offset: 0x000106B5
		public bool PublicOnly
		{
			get
			{
				return this.keypairGenerated && this.x == null;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000332 RID: 818 RVA: 0x000124CD File Offset: 0x000106CD
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x000124D4 File Offset: 0x000106D4
		private byte[] NormalizeArray(byte[] array)
		{
			int num = array.Length % 4;
			if (num > 0)
			{
				byte[] array2 = new byte[array.Length + 4 - num];
				Array.Copy(array, 0, array2, 4 - num, array.Length);
				return array2;
			}
			return array;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0001250C File Offset: 0x0001070C
		public override DSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (!this.keypairGenerated)
			{
				this.Generate();
			}
			if (includePrivateParameters && this.x == null)
			{
				throw new CryptographicException("no private key to export");
			}
			DSAParameters dsaparameters = default(DSAParameters);
			dsaparameters.P = this.NormalizeArray(this.p.GetBytes());
			dsaparameters.Q = this.NormalizeArray(this.q.GetBytes());
			dsaparameters.G = this.NormalizeArray(this.g.GetBytes());
			dsaparameters.Y = this.NormalizeArray(this.y.GetBytes());
			if (!this.j_missing)
			{
				dsaparameters.J = this.NormalizeArray(this.j.GetBytes());
			}
			if (this.seed != 0U)
			{
				dsaparameters.Seed = this.NormalizeArray(this.seed.GetBytes());
				dsaparameters.Counter = this.counter;
			}
			if (includePrivateParameters)
			{
				byte[] bytes = this.x.GetBytes();
				if (bytes.Length == 20)
				{
					dsaparameters.X = this.NormalizeArray(bytes);
				}
			}
			return dsaparameters;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0001263C File Offset: 0x0001083C
		public override void ImportParameters(DSAParameters parameters)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (parameters.P == null || parameters.Q == null || parameters.G == null)
			{
				throw new CryptographicException(Locale.GetText("Missing mandatory DSA parameters (P, Q or G)."));
			}
			if (parameters.X == null && parameters.Y == null)
			{
				throw new CryptographicException(Locale.GetText("Missing both public (Y) and private (X) keys."));
			}
			this.p = new BigInteger(parameters.P);
			this.q = new BigInteger(parameters.Q);
			this.g = new BigInteger(parameters.G);
			if (parameters.X != null)
			{
				this.x = new BigInteger(parameters.X);
			}
			else
			{
				this.x = null;
			}
			if (parameters.Y != null)
			{
				this.y = new BigInteger(parameters.Y);
			}
			else
			{
				this.y = this.g.ModPow(this.x, this.p);
			}
			if (parameters.J != null)
			{
				this.j = new BigInteger(parameters.J);
			}
			else
			{
				this.j = (this.p - 1) / this.q;
				this.j_missing = true;
			}
			if (parameters.Seed != null)
			{
				this.seed = new BigInteger(parameters.Seed);
				this.counter = parameters.Counter;
			}
			else
			{
				this.seed = 0;
			}
			this.keypairGenerated = true;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000127B4 File Offset: 0x000109B4
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbHash.Length != 20)
			{
				throw new CryptographicException("invalid hash length");
			}
			if (!this.keypairGenerated)
			{
				this.Generate();
			}
			if (this.x == null)
			{
				throw new CryptographicException("no private key available for signature");
			}
			BigInteger bigInteger = new BigInteger(rgbHash);
			BigInteger bigInteger2 = BigInteger.GenerateRandom(160);
			while (bigInteger2 >= this.q)
			{
				bigInteger2.Randomize();
			}
			BigInteger bigInteger3 = this.g.ModPow(bigInteger2, this.p) % this.q;
			BigInteger bigInteger4 = bigInteger2.ModInverse(this.q) * (bigInteger + this.x * bigInteger3) % this.q;
			byte[] array = new byte[40];
			byte[] bytes = bigInteger3.GetBytes();
			byte[] bytes2 = bigInteger4.GetBytes();
			int num = 20 - bytes.Length;
			Array.Copy(bytes, 0, array, num, bytes.Length);
			num = 40 - bytes2.Length;
			Array.Copy(bytes2, 0, array, num, bytes2.Length);
			return array;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000128DC File Offset: 0x00010ADC
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			if (rgbHash.Length != 20)
			{
				throw new CryptographicException("invalid hash length");
			}
			if (rgbSignature.Length != 40)
			{
				throw new CryptographicException("invalid signature length");
			}
			if (!this.keypairGenerated)
			{
				return false;
			}
			bool flag;
			try
			{
				BigInteger bigInteger = new BigInteger(rgbHash);
				byte[] array = new byte[20];
				Array.Copy(rgbSignature, 0, array, 0, 20);
				BigInteger bigInteger2 = new BigInteger(array);
				Array.Copy(rgbSignature, 20, array, 0, 20);
				BigInteger bigInteger3 = new BigInteger(array);
				if (bigInteger2 < 0 || this.q <= bigInteger2)
				{
					flag = false;
				}
				else if (bigInteger3 < 0 || this.q <= bigInteger3)
				{
					flag = false;
				}
				else
				{
					BigInteger bigInteger4 = bigInteger3.ModInverse(this.q);
					BigInteger bigInteger5 = bigInteger * bigInteger4 % this.q;
					BigInteger bigInteger6 = bigInteger2 * bigInteger4 % this.q;
					bigInteger5 = this.g.ModPow(bigInteger5, this.p);
					bigInteger6 = this.y.ModPow(bigInteger6, this.p);
					flag = bigInteger5 * bigInteger6 % this.p % this.q == bigInteger2;
				}
			}
			catch
			{
				throw new CryptographicException("couldn't compute signature verification");
			}
			return flag;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00012A80 File Offset: 0x00010C80
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.x != null)
				{
					this.x.Clear();
					this.x = null;
				}
				if (disposing)
				{
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
					if (this.g != null)
					{
						this.g.Clear();
						this.g = null;
					}
					if (this.j != null)
					{
						this.j.Clear();
						this.j = null;
					}
					if (this.seed != null)
					{
						this.seed.Clear();
						this.seed = null;
					}
					if (this.y != null)
					{
						this.y.Clear();
						this.y = null;
					}
				}
			}
			this.m_disposed = true;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000339 RID: 825 RVA: 0x00012B88 File Offset: 0x00010D88
		// (remove) Token: 0x0600033A RID: 826 RVA: 0x00012BC0 File Offset: 0x00010DC0
		public event DSAManaged.KeyGeneratedEventHandler KeyGenerated
		{
			[CompilerGenerated]
			add
			{
				DSAManaged.KeyGeneratedEventHandler keyGeneratedEventHandler = this.KeyGenerated;
				DSAManaged.KeyGeneratedEventHandler keyGeneratedEventHandler2;
				do
				{
					keyGeneratedEventHandler2 = keyGeneratedEventHandler;
					DSAManaged.KeyGeneratedEventHandler keyGeneratedEventHandler3 = (DSAManaged.KeyGeneratedEventHandler)Delegate.Combine(keyGeneratedEventHandler2, value);
					keyGeneratedEventHandler = Interlocked.CompareExchange<DSAManaged.KeyGeneratedEventHandler>(ref this.KeyGenerated, keyGeneratedEventHandler3, keyGeneratedEventHandler2);
				}
				while (keyGeneratedEventHandler != keyGeneratedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				DSAManaged.KeyGeneratedEventHandler keyGeneratedEventHandler = this.KeyGenerated;
				DSAManaged.KeyGeneratedEventHandler keyGeneratedEventHandler2;
				do
				{
					keyGeneratedEventHandler2 = keyGeneratedEventHandler;
					DSAManaged.KeyGeneratedEventHandler keyGeneratedEventHandler3 = (DSAManaged.KeyGeneratedEventHandler)Delegate.Remove(keyGeneratedEventHandler2, value);
					keyGeneratedEventHandler = Interlocked.CompareExchange<DSAManaged.KeyGeneratedEventHandler>(ref this.KeyGenerated, keyGeneratedEventHandler3, keyGeneratedEventHandler2);
				}
				while (keyGeneratedEventHandler != keyGeneratedEventHandler2);
			}
		}

		// Token: 0x04000E1A RID: 3610
		private const int defaultKeySize = 1024;

		// Token: 0x04000E1B RID: 3611
		private bool keypairGenerated;

		// Token: 0x04000E1C RID: 3612
		private bool m_disposed;

		// Token: 0x04000E1D RID: 3613
		private BigInteger p;

		// Token: 0x04000E1E RID: 3614
		private BigInteger q;

		// Token: 0x04000E1F RID: 3615
		private BigInteger g;

		// Token: 0x04000E20 RID: 3616
		private BigInteger x;

		// Token: 0x04000E21 RID: 3617
		private BigInteger y;

		// Token: 0x04000E22 RID: 3618
		private BigInteger j;

		// Token: 0x04000E23 RID: 3619
		private BigInteger seed;

		// Token: 0x04000E24 RID: 3620
		private int counter;

		// Token: 0x04000E25 RID: 3621
		private bool j_missing;

		// Token: 0x04000E26 RID: 3622
		private RandomNumberGenerator rng;

		// Token: 0x04000E27 RID: 3623
		[CompilerGenerated]
		private DSAManaged.KeyGeneratedEventHandler KeyGenerated;

		// Token: 0x02000074 RID: 116
		// (Invoke) Token: 0x0600033C RID: 828
		public delegate void KeyGeneratedEventHandler(object sender, EventArgs e);
	}
}
