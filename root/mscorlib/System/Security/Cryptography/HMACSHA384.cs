using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000464 RID: 1124
	[ComVisible(true)]
	public class HMACSHA384 : HMAC
	{
		// Token: 0x06002EAA RID: 11946 RVA: 0x000A7F71 File Offset: 0x000A6171
		public HMACSHA384()
			: this(Utils.GenerateRandom(128))
		{
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000A7F84 File Offset: 0x000A6184
		[SecuritySafeCritical]
		public HMACSHA384(byte[] key)
		{
			this.m_hashName = "SHA384";
			this.m_hash1 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA384Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA384CryptoServiceProvider"));
			this.m_hash2 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA384Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA384CryptoServiceProvider"));
			this.HashSizeValue = 384;
			base.BlockSizeValue = this.BlockSize;
			base.InitializeKey(key);
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06002EAC RID: 11948 RVA: 0x000A805D File Offset: 0x000A625D
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

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06002EAD RID: 11949 RVA: 0x000A806F File Offset: 0x000A626F
		// (set) Token: 0x06002EAE RID: 11950 RVA: 0x000A8077 File Offset: 0x000A6277
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

		// Token: 0x0400203C RID: 8252
		private bool m_useLegacyBlockSize = Utils._ProduceLegacyHmacValues();

		// Token: 0x02000465 RID: 1125
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002EAF RID: 11951 RVA: 0x000A8098 File Offset: 0x000A6298
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002EB0 RID: 11952 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06002EB1 RID: 11953 RVA: 0x000A80A4 File Offset: 0x000A62A4
			internal HashAlgorithm <.ctor>b__2_0()
			{
				return new SHA384Managed();
			}

			// Token: 0x06002EB2 RID: 11954 RVA: 0x000A80AB File Offset: 0x000A62AB
			internal HashAlgorithm <.ctor>b__2_1()
			{
				return HashAlgorithm.Create("System.Security.Cryptography.SHA384CryptoServiceProvider");
			}

			// Token: 0x06002EB3 RID: 11955 RVA: 0x000A80A4 File Offset: 0x000A62A4
			internal HashAlgorithm <.ctor>b__2_2()
			{
				return new SHA384Managed();
			}

			// Token: 0x06002EB4 RID: 11956 RVA: 0x000A80AB File Offset: 0x000A62AB
			internal HashAlgorithm <.ctor>b__2_3()
			{
				return HashAlgorithm.Create("System.Security.Cryptography.SHA384CryptoServiceProvider");
			}

			// Token: 0x0400203D RID: 8253
			public static readonly HMACSHA384.<>c <>9 = new HMACSHA384.<>c();

			// Token: 0x0400203E RID: 8254
			public static Func<HashAlgorithm> <>9__2_0;

			// Token: 0x0400203F RID: 8255
			public static Func<HashAlgorithm> <>9__2_1;

			// Token: 0x04002040 RID: 8256
			public static Func<HashAlgorithm> <>9__2_2;

			// Token: 0x04002041 RID: 8257
			public static Func<HashAlgorithm> <>9__2_3;
		}
	}
}
