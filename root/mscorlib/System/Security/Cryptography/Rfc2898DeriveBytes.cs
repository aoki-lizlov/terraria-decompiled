using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text;
using Internal.Cryptography;

namespace System.Security.Cryptography
{
	// Token: 0x02000436 RID: 1078
	public class Rfc2898DeriveBytes : DeriveBytes
	{
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06002D5F RID: 11615 RVA: 0x000A3C84 File Offset: 0x000A1E84
		public HashAlgorithmName HashAlgorithm
		{
			[CompilerGenerated]
			get
			{
				return this.<HashAlgorithm>k__BackingField;
			}
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x000A3C8C File Offset: 0x000A1E8C
		public Rfc2898DeriveBytes(byte[] password, byte[] salt, int iterations)
			: this(password, salt, iterations, HashAlgorithmName.SHA1)
		{
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x000A3C9C File Offset: 0x000A1E9C
		public Rfc2898DeriveBytes(byte[] password, byte[] salt, int iterations, HashAlgorithmName hashAlgorithm)
		{
			if (salt == null)
			{
				throw new ArgumentNullException("salt");
			}
			if (salt.Length < 8)
			{
				throw new ArgumentException("Salt is not at least eight bytes.", "salt");
			}
			if (iterations <= 0)
			{
				throw new ArgumentOutOfRangeException("iterations", "Positive number required.");
			}
			if (password == null)
			{
				throw new NullReferenceException();
			}
			this._salt = salt.CloneByteArray();
			this._iterations = (uint)iterations;
			this._password = password.CloneByteArray();
			this.HashAlgorithm = hashAlgorithm;
			this._hmac = this.OpenHmac();
			this._blockSize = this._hmac.HashSize >> 3;
			this.Initialize();
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x000A3D3C File Offset: 0x000A1F3C
		public Rfc2898DeriveBytes(string password, byte[] salt)
			: this(password, salt, 1000)
		{
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000A3D4B File Offset: 0x000A1F4B
		public Rfc2898DeriveBytes(string password, byte[] salt, int iterations)
			: this(password, salt, iterations, HashAlgorithmName.SHA1)
		{
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000A3D5B File Offset: 0x000A1F5B
		public Rfc2898DeriveBytes(string password, byte[] salt, int iterations, HashAlgorithmName hashAlgorithm)
			: this(Encoding.UTF8.GetBytes(password), salt, iterations, hashAlgorithm)
		{
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000A3D72 File Offset: 0x000A1F72
		public Rfc2898DeriveBytes(string password, int saltSize)
			: this(password, saltSize, 1000)
		{
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000A3D81 File Offset: 0x000A1F81
		public Rfc2898DeriveBytes(string password, int saltSize, int iterations)
			: this(password, saltSize, iterations, HashAlgorithmName.SHA1)
		{
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000A3D94 File Offset: 0x000A1F94
		public Rfc2898DeriveBytes(string password, int saltSize, int iterations, HashAlgorithmName hashAlgorithm)
		{
			if (saltSize < 0)
			{
				throw new ArgumentOutOfRangeException("saltSize", "Non-negative number required.");
			}
			if (saltSize < 8)
			{
				throw new ArgumentException("Salt is not at least eight bytes.", "saltSize");
			}
			if (iterations <= 0)
			{
				throw new ArgumentOutOfRangeException("iterations", "Positive number required.");
			}
			this._salt = Helpers.GenerateRandom(saltSize);
			this._iterations = (uint)iterations;
			this._password = Encoding.UTF8.GetBytes(password);
			this.HashAlgorithm = hashAlgorithm;
			this._hmac = this.OpenHmac();
			this._blockSize = this._hmac.HashSize >> 3;
			this.Initialize();
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06002D68 RID: 11624 RVA: 0x000A3E34 File Offset: 0x000A2034
		// (set) Token: 0x06002D69 RID: 11625 RVA: 0x000A3E3C File Offset: 0x000A203C
		public int IterationCount
		{
			get
			{
				return (int)this._iterations;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", "Positive number required.");
				}
				this._iterations = (uint)value;
				this.Initialize();
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x000A3E5F File Offset: 0x000A205F
		// (set) Token: 0x06002D6B RID: 11627 RVA: 0x000A3E6C File Offset: 0x000A206C
		public byte[] Salt
		{
			get
			{
				return this._salt.CloneByteArray();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length < 8)
				{
					throw new ArgumentException("Salt is not at least eight bytes.");
				}
				this._salt = value.CloneByteArray();
				this.Initialize();
			}
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000A3EA0 File Offset: 0x000A20A0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._hmac != null)
				{
					this._hmac.Dispose();
					this._hmac = null;
				}
				if (this._buffer != null)
				{
					Array.Clear(this._buffer, 0, this._buffer.Length);
				}
				if (this._password != null)
				{
					Array.Clear(this._password, 0, this._password.Length);
				}
				if (this._salt != null)
				{
					Array.Clear(this._salt, 0, this._salt.Length);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000A3F28 File Offset: 0x000A2128
		public override byte[] GetBytes(int cb)
		{
			if (cb <= 0)
			{
				throw new ArgumentOutOfRangeException("cb", "Positive number required.");
			}
			byte[] array = new byte[cb];
			int i = 0;
			int num = this._endIndex - this._startIndex;
			if (num > 0)
			{
				if (cb < num)
				{
					Buffer.BlockCopy(this._buffer, this._startIndex, array, 0, cb);
					this._startIndex += cb;
					return array;
				}
				Buffer.BlockCopy(this._buffer, this._startIndex, array, 0, num);
				this._startIndex = (this._endIndex = 0);
				i += num;
			}
			while (i < cb)
			{
				byte[] array2 = this.Func();
				int num2 = cb - i;
				if (num2 <= this._blockSize)
				{
					Buffer.BlockCopy(array2, 0, array, i, num2);
					i += num2;
					Buffer.BlockCopy(array2, num2, this._buffer, this._startIndex, this._blockSize - num2);
					this._endIndex += this._blockSize - num2;
					return array;
				}
				Buffer.BlockCopy(array2, 0, array, i, this._blockSize);
				i += this._blockSize;
			}
			return array;
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public byte[] CryptDeriveKey(string algname, string alghashname, int keySize, byte[] rgbIV)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x000A403A File Offset: 0x000A223A
		public override void Reset()
		{
			this.Initialize();
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x000A4044 File Offset: 0x000A2244
		private HMAC OpenHmac()
		{
			HashAlgorithmName hashAlgorithm = this.HashAlgorithm;
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new CryptographicException("The hash algorithm name cannot be null or empty.");
			}
			if (hashAlgorithm == HashAlgorithmName.SHA1)
			{
				return new HMACSHA1(this._password);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA256)
			{
				return new HMACSHA256(this._password);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA384)
			{
				return new HMACSHA384(this._password);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA512)
			{
				return new HMACSHA512(this._password);
			}
			throw new CryptographicException(SR.Format("'{0}' is not a known hash algorithm.", hashAlgorithm.Name));
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x000A40EC File Offset: 0x000A22EC
		private void Initialize()
		{
			if (this._buffer != null)
			{
				Array.Clear(this._buffer, 0, this._buffer.Length);
			}
			this._buffer = new byte[this._blockSize];
			this._block = 1U;
			this._startIndex = (this._endIndex = 0);
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x000A4140 File Offset: 0x000A2340
		private byte[] Func()
		{
			byte[] array = new byte[this._salt.Length + 4];
			Buffer.BlockCopy(this._salt, 0, array, 0, this._salt.Length);
			Helpers.WriteInt(this._block, array, this._salt.Length);
			byte[] array2 = ArrayPool<byte>.Shared.Rent(this._blockSize);
			byte[] array5;
			try
			{
				Span<byte> span = new Span<byte>(array2, 0, this._blockSize);
				int num;
				if (!this._hmac.TryComputeHash(array, span, out num) || num != this._blockSize)
				{
					throw new CryptographicException();
				}
				byte[] array3 = new byte[this._blockSize];
				span.CopyTo(array3);
				int num2 = 2;
				while ((long)num2 <= (long)((ulong)this._iterations))
				{
					if (!this._hmac.TryComputeHash(span, span, out num) || num != this._blockSize)
					{
						throw new CryptographicException();
					}
					for (int i = 0; i < this._blockSize; i++)
					{
						byte[] array4 = array3;
						int num3 = i;
						array4[num3] ^= array2[i];
					}
					num2++;
				}
				this._block += 1U;
				array5 = array3;
			}
			finally
			{
				Array.Clear(array2, 0, this._blockSize);
				ArrayPool<byte>.Shared.Return(array2, false);
			}
			return array5;
		}

		// Token: 0x04001F8E RID: 8078
		private const int MinimumSaltSize = 8;

		// Token: 0x04001F8F RID: 8079
		private readonly byte[] _password;

		// Token: 0x04001F90 RID: 8080
		private byte[] _salt;

		// Token: 0x04001F91 RID: 8081
		private uint _iterations;

		// Token: 0x04001F92 RID: 8082
		private HMAC _hmac;

		// Token: 0x04001F93 RID: 8083
		private int _blockSize;

		// Token: 0x04001F94 RID: 8084
		private byte[] _buffer;

		// Token: 0x04001F95 RID: 8085
		private uint _block;

		// Token: 0x04001F96 RID: 8086
		private int _startIndex;

		// Token: 0x04001F97 RID: 8087
		private int _endIndex;

		// Token: 0x04001F98 RID: 8088
		[CompilerGenerated]
		private readonly HashAlgorithmName <HashAlgorithm>k__BackingField;
	}
}
