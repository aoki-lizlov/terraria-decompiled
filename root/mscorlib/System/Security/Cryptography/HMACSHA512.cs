using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000466 RID: 1126
	[ComVisible(true)]
	public class HMACSHA512 : HMAC
	{
		// Token: 0x06002EB5 RID: 11957 RVA: 0x000A80B7 File Offset: 0x000A62B7
		public HMACSHA512()
			: this(Utils.GenerateRandom(128))
		{
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x000A80CC File Offset: 0x000A62CC
		[SecuritySafeCritical]
		public HMACSHA512(byte[] key)
		{
			this.m_hashName = "SHA512";
			this.m_hash1 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA512Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA512CryptoServiceProvider"));
			this.m_hash2 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA512Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA512CryptoServiceProvider"));
			this.HashSizeValue = 512;
			base.BlockSizeValue = this.BlockSize;
			base.InitializeKey(key);
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06002EB7 RID: 11959 RVA: 0x000A81A5 File Offset: 0x000A63A5
		private int BlockSize
		{
			get
			{
				if (!this.m_useLegacyBlockSize)
				{
					return 128;
				}
				return 64;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06002EB8 RID: 11960 RVA: 0x000A81B7 File Offset: 0x000A63B7
		// (set) Token: 0x06002EB9 RID: 11961 RVA: 0x000A81BF File Offset: 0x000A63BF
		public bool ProduceLegacyHmacValues
		{
			get
			{
				return this.m_useLegacyBlockSize;
			}
			set
			{
				this.m_useLegacyBlockSize = value;
				base.BlockSizeValue = this.BlockSize;
				base.InitializeKey(this.KeyValue);
			}
		}

		// Token: 0x04002042 RID: 8258
		private bool m_useLegacyBlockSize = Utils._ProduceLegacyHmacValues();

		// Token: 0x02000467 RID: 1127
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002EBA RID: 11962 RVA: 0x000A81E0 File Offset: 0x000A63E0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002EBB RID: 11963 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06002EBC RID: 11964 RVA: 0x000A81EC File Offset: 0x000A63EC
			internal HashAlgorithm <.ctor>b__2_0()
			{
				return new SHA512Managed();
			}

			// Token: 0x06002EBD RID: 11965 RVA: 0x000A81F3 File Offset: 0x000A63F3
			internal HashAlgorithm <.ctor>b__2_1()
			{
				return HashAlgorithm.Create("System.Security.Cryptography.SHA512CryptoServiceProvider");
			}

			// Token: 0x06002EBE RID: 11966 RVA: 0x000A81EC File Offset: 0x000A63EC
			internal HashAlgorithm <.ctor>b__2_2()
			{
				return new SHA512Managed();
			}

			// Token: 0x06002EBF RID: 11967 RVA: 0x000A81F3 File Offset: 0x000A63F3
			internal HashAlgorithm <.ctor>b__2_3()
			{
				return HashAlgorithm.Create("System.Security.Cryptography.SHA512CryptoServiceProvider");
			}

			// Token: 0x04002043 RID: 8259
			public static readonly HMACSHA512.<>c <>9 = new HMACSHA512.<>c();

			// Token: 0x04002044 RID: 8260
			public static Func<HashAlgorithm> <>9__2_0;

			// Token: 0x04002045 RID: 8261
			public static Func<HashAlgorithm> <>9__2_1;

			// Token: 0x04002046 RID: 8262
			public static Func<HashAlgorithm> <>9__2_2;

			// Token: 0x04002047 RID: 8263
			public static Func<HashAlgorithm> <>9__2_3;
		}
	}
}
