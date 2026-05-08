using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200045E RID: 1118
	[ComVisible(true)]
	public abstract class HMAC : KeyedHashAlgorithm
	{
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06002E8B RID: 11915 RVA: 0x000A79CB File Offset: 0x000A5BCB
		// (set) Token: 0x06002E8C RID: 11916 RVA: 0x000A79D3 File Offset: 0x000A5BD3
		protected int BlockSizeValue
		{
			get
			{
				return this.blockSizeValue;
			}
			set
			{
				this.blockSizeValue = value;
			}
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x000A79DC File Offset: 0x000A5BDC
		private void UpdateIOPadBuffers()
		{
			if (this.m_inner == null)
			{
				this.m_inner = new byte[this.BlockSizeValue];
			}
			if (this.m_outer == null)
			{
				this.m_outer = new byte[this.BlockSizeValue];
			}
			for (int i = 0; i < this.BlockSizeValue; i++)
			{
				this.m_inner[i] = 54;
				this.m_outer[i] = 92;
			}
			for (int i = 0; i < this.KeyValue.Length; i++)
			{
				byte[] inner = this.m_inner;
				int num = i;
				inner[num] ^= this.KeyValue[i];
				byte[] outer = this.m_outer;
				int num2 = i;
				outer[num2] ^= this.KeyValue[i];
			}
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x000A7A88 File Offset: 0x000A5C88
		internal void InitializeKey(byte[] key)
		{
			this.m_inner = null;
			this.m_outer = null;
			if (key.Length > this.BlockSizeValue)
			{
				this.KeyValue = this.m_hash1.ComputeHash(key);
			}
			else
			{
				this.KeyValue = (byte[])key.Clone();
			}
			this.UpdateIOPadBuffers();
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06002E8F RID: 11919 RVA: 0x000A7AD9 File Offset: 0x000A5CD9
		// (set) Token: 0x06002E90 RID: 11920 RVA: 0x000A7AEB File Offset: 0x000A5CEB
		public override byte[] Key
		{
			get
			{
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (this.m_hashing)
				{
					throw new CryptographicException(Environment.GetResourceString("Hash key cannot be changed after the first write to the stream."));
				}
				this.InitializeKey(value);
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06002E91 RID: 11921 RVA: 0x000A7B0C File Offset: 0x000A5D0C
		// (set) Token: 0x06002E92 RID: 11922 RVA: 0x000A7B14 File Offset: 0x000A5D14
		public string HashName
		{
			get
			{
				return this.m_hashName;
			}
			set
			{
				if (this.m_hashing)
				{
					throw new CryptographicException(Environment.GetResourceString("Hash name cannot be changed after the first write to the stream."));
				}
				this.m_hashName = value;
				this.m_hash1 = HashAlgorithm.Create(this.m_hashName);
				this.m_hash2 = HashAlgorithm.Create(this.m_hashName);
			}
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x000A7B62 File Offset: 0x000A5D62
		public new static HMAC Create()
		{
			return HMAC.Create("System.Security.Cryptography.HMAC");
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x000A7B6E File Offset: 0x000A5D6E
		public new static HMAC Create(string algorithmName)
		{
			return (HMAC)CryptoConfig.CreateFromName(algorithmName);
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x000A7B7B File Offset: 0x000A5D7B
		public override void Initialize()
		{
			this.m_hash1.Initialize();
			this.m_hash2.Initialize();
			this.m_hashing = false;
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000A7B9C File Offset: 0x000A5D9C
		protected override void HashCore(byte[] rgb, int ib, int cb)
		{
			if (!this.m_hashing)
			{
				this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
				this.m_hashing = true;
			}
			this.m_hash1.TransformBlock(rgb, ib, cb, rgb, ib);
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x000A7BEC File Offset: 0x000A5DEC
		protected override byte[] HashFinal()
		{
			if (!this.m_hashing)
			{
				this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
				this.m_hashing = true;
			}
			this.m_hash1.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			byte[] hashValue = this.m_hash1.HashValue;
			this.m_hash2.TransformBlock(this.m_outer, 0, this.m_outer.Length, this.m_outer, 0);
			this.m_hash2.TransformBlock(hashValue, 0, hashValue.Length, hashValue, 0);
			this.m_hashing = false;
			this.m_hash2.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			return this.m_hash2.HashValue;
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000A7CA4 File Offset: 0x000A5EA4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_hash1 != null)
				{
					((IDisposable)this.m_hash1).Dispose();
				}
				if (this.m_hash2 != null)
				{
					((IDisposable)this.m_hash2).Dispose();
				}
				if (this.m_inner != null)
				{
					Array.Clear(this.m_inner, 0, this.m_inner.Length);
				}
				if (this.m_outer != null)
				{
					Array.Clear(this.m_outer, 0, this.m_outer.Length);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000A7D1C File Offset: 0x000A5F1C
		internal static HashAlgorithm GetHashAlgorithmWithFipsFallback(Func<HashAlgorithm> createStandardHashAlgorithmCallback, Func<HashAlgorithm> createFipsHashAlgorithmCallback)
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms)
			{
				try
				{
					return createFipsHashAlgorithmCallback();
				}
				catch (PlatformNotSupportedException ex)
				{
					throw new InvalidOperationException(ex.Message, ex);
				}
			}
			return createStandardHashAlgorithmCallback();
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000A7D60 File Offset: 0x000A5F60
		protected HMAC()
		{
		}

		// Token: 0x04002030 RID: 8240
		private int blockSizeValue = 64;

		// Token: 0x04002031 RID: 8241
		internal string m_hashName;

		// Token: 0x04002032 RID: 8242
		internal HashAlgorithm m_hash1;

		// Token: 0x04002033 RID: 8243
		internal HashAlgorithm m_hash2;

		// Token: 0x04002034 RID: 8244
		private byte[] m_inner;

		// Token: 0x04002035 RID: 8245
		private byte[] m_outer;

		// Token: 0x04002036 RID: 8246
		private bool m_hashing;
	}
}
