using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	// Token: 0x0200046E RID: 1134
	[ComVisible(true)]
	public class PasswordDeriveBytes : DeriveBytes
	{
		// Token: 0x06002EE9 RID: 12009 RVA: 0x000A8745 File Offset: 0x000A6945
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt)
			: this(strPassword, rgbSalt, new CspParameters())
		{
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x000A8754 File Offset: 0x000A6954
		public PasswordDeriveBytes(byte[] password, byte[] salt)
			: this(password, salt, new CspParameters())
		{
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x000A8763 File Offset: 0x000A6963
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, string strHashName, int iterations)
			: this(strPassword, rgbSalt, strHashName, iterations, new CspParameters())
		{
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x000A8775 File Offset: 0x000A6975
		public PasswordDeriveBytes(byte[] password, byte[] salt, string hashName, int iterations)
			: this(password, salt, hashName, iterations, new CspParameters())
		{
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x000A8787 File Offset: 0x000A6987
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, CspParameters cspParams)
			: this(strPassword, rgbSalt, "SHA1", 100, cspParams)
		{
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x000A8799 File Offset: 0x000A6999
		public PasswordDeriveBytes(byte[] password, byte[] salt, CspParameters cspParams)
			: this(password, salt, "SHA1", 100, cspParams)
		{
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x000A87AB File Offset: 0x000A69AB
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, string strHashName, int iterations, CspParameters cspParams)
			: this(new UTF8Encoding(false).GetBytes(strPassword), rgbSalt, strHashName, iterations, cspParams)
		{
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x000A87C5 File Offset: 0x000A69C5
		[SecuritySafeCritical]
		public PasswordDeriveBytes(byte[] password, byte[] salt, string hashName, int iterations, CspParameters cspParams)
		{
			this.IterationCount = iterations;
			this.Salt = salt;
			this.HashName = hashName;
			this._password = password;
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06002EF1 RID: 12017 RVA: 0x000A87EA File Offset: 0x000A69EA
		// (set) Token: 0x06002EF2 RID: 12018 RVA: 0x000A87F4 File Offset: 0x000A69F4
		public string HashName
		{
			get
			{
				return this._hashName;
			}
			set
			{
				if (this._baseValue != null)
				{
					throw new CryptographicException(Environment.GetResourceString("Value of '{0}' cannot be changed after the bytes have been retrieved.", new object[] { "HashName" }));
				}
				this._hashName = value;
				this._hash = (HashAlgorithm)CryptoConfig.CreateFromName(this._hashName);
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x000A8844 File Offset: 0x000A6A44
		// (set) Token: 0x06002EF4 RID: 12020 RVA: 0x000A884C File Offset: 0x000A6A4C
		public int IterationCount
		{
			get
			{
				return this._iterations;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Positive number required."));
				}
				if (this._baseValue != null)
				{
					throw new CryptographicException(Environment.GetResourceString("Value of '{0}' cannot be changed after the bytes have been retrieved.", new object[] { "IterationCount" }));
				}
				this._iterations = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x000A889F File Offset: 0x000A6A9F
		// (set) Token: 0x06002EF6 RID: 12022 RVA: 0x000A88BC File Offset: 0x000A6ABC
		public byte[] Salt
		{
			get
			{
				if (this._salt == null)
				{
					return null;
				}
				return (byte[])this._salt.Clone();
			}
			set
			{
				if (this._baseValue != null)
				{
					throw new CryptographicException(Environment.GetResourceString("Value of '{0}' cannot be changed after the bytes have been retrieved.", new object[] { "Salt" }));
				}
				if (value == null)
				{
					this._salt = null;
					return;
				}
				this._salt = (byte[])value.Clone();
			}
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x000A890C File Offset: 0x000A6B0C
		[SecuritySafeCritical]
		[Obsolete("Rfc2898DeriveBytes replaces PasswordDeriveBytes for deriving key material from a password and is preferred in new applications.")]
		public override byte[] GetBytes(int cb)
		{
			if (cb < 1)
			{
				throw new IndexOutOfRangeException("cb");
			}
			int num = 0;
			byte[] array = new byte[cb];
			if (this._baseValue == null)
			{
				this.ComputeBaseValue();
			}
			else if (this._extra != null)
			{
				num = this._extra.Length - this._extraCount;
				if (num >= cb)
				{
					Buffer.InternalBlockCopy(this._extra, this._extraCount, array, 0, cb);
					if (num > cb)
					{
						this._extraCount += cb;
					}
					else
					{
						this._extra = null;
					}
					return array;
				}
				Buffer.InternalBlockCopy(this._extra, num, array, 0, num);
				this._extra = null;
			}
			byte[] array2 = this.ComputeBytes(cb - num);
			Buffer.InternalBlockCopy(array2, 0, array, num, cb - num);
			if (array2.Length + num > cb)
			{
				this._extra = array2;
				this._extraCount = cb - num;
			}
			return array;
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000A89D7 File Offset: 0x000A6BD7
		public override void Reset()
		{
			this._prefix = 0;
			this._extra = null;
			this._baseValue = null;
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000A89F0 File Offset: 0x000A6BF0
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (this._hash != null)
				{
					this._hash.Dispose();
				}
				if (this._baseValue != null)
				{
					Array.Clear(this._baseValue, 0, this._baseValue.Length);
				}
				if (this._extra != null)
				{
					Array.Clear(this._extra, 0, this._extra.Length);
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
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x000A8A8D File Offset: 0x000A6C8D
		[SecuritySafeCritical]
		public byte[] CryptDeriveKey(string algname, string alghashname, int keySize, byte[] rgbIV)
		{
			if (keySize < 0)
			{
				throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
			}
			throw new NotSupportedException("CspParameters are not supported by Mono");
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x000A8AB0 File Offset: 0x000A6CB0
		private byte[] ComputeBaseValue()
		{
			this._hash.Initialize();
			this._hash.TransformBlock(this._password, 0, this._password.Length, this._password, 0);
			if (this._salt != null)
			{
				this._hash.TransformBlock(this._salt, 0, this._salt.Length, this._salt, 0);
			}
			this._hash.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			this._baseValue = this._hash.Hash;
			this._hash.Initialize();
			for (int i = 1; i < this._iterations - 1; i++)
			{
				this._hash.ComputeHash(this._baseValue);
				this._baseValue = this._hash.Hash;
			}
			return this._baseValue;
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x000A8B80 File Offset: 0x000A6D80
		[SecurityCritical]
		private byte[] ComputeBytes(int cb)
		{
			int num = 0;
			this._hash.Initialize();
			int num2 = this._hash.HashSize / 8;
			byte[] array = new byte[(cb + num2 - 1) / num2 * num2];
			using (CryptoStream cryptoStream = new CryptoStream(Stream.Null, this._hash, CryptoStreamMode.Write))
			{
				this.HashPrefix(cryptoStream);
				cryptoStream.Write(this._baseValue, 0, this._baseValue.Length);
				cryptoStream.Close();
			}
			Buffer.InternalBlockCopy(this._hash.Hash, 0, array, num, num2);
			num += num2;
			while (cb > num)
			{
				this._hash.Initialize();
				using (CryptoStream cryptoStream2 = new CryptoStream(Stream.Null, this._hash, CryptoStreamMode.Write))
				{
					this.HashPrefix(cryptoStream2);
					cryptoStream2.Write(this._baseValue, 0, this._baseValue.Length);
					cryptoStream2.Close();
				}
				Buffer.InternalBlockCopy(this._hash.Hash, 0, array, num, num2);
				num += num2;
			}
			return array;
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x000A8CA0 File Offset: 0x000A6EA0
		private void HashPrefix(CryptoStream cs)
		{
			int num = 0;
			byte[] array = new byte[] { 48, 48, 48 };
			if (this._prefix > 999)
			{
				throw new CryptographicException(Environment.GetResourceString("Requested number of bytes exceeds the maximum."));
			}
			if (this._prefix >= 100)
			{
				byte[] array2 = array;
				int num2 = 0;
				array2[num2] += (byte)(this._prefix / 100);
				num++;
			}
			if (this._prefix >= 10)
			{
				byte[] array3 = array;
				int num3 = num;
				array3[num3] += (byte)(this._prefix % 100 / 10);
				num++;
			}
			if (this._prefix > 0)
			{
				byte[] array4 = array;
				int num4 = num;
				array4[num4] += (byte)(this._prefix % 10);
				num++;
				cs.Write(array, 0, num);
			}
			this._prefix++;
		}

		// Token: 0x04002053 RID: 8275
		private int _extraCount;

		// Token: 0x04002054 RID: 8276
		private int _prefix;

		// Token: 0x04002055 RID: 8277
		private int _iterations;

		// Token: 0x04002056 RID: 8278
		private byte[] _baseValue;

		// Token: 0x04002057 RID: 8279
		private byte[] _extra;

		// Token: 0x04002058 RID: 8280
		private byte[] _salt;

		// Token: 0x04002059 RID: 8281
		private string _hashName;

		// Token: 0x0400205A RID: 8282
		private byte[] _password;

		// Token: 0x0400205B RID: 8283
		private HashAlgorithm _hash;
	}
}
