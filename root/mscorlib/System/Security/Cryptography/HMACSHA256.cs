using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000462 RID: 1122
	[ComVisible(true)]
	public class HMACSHA256 : HMAC
	{
		// Token: 0x06002EA2 RID: 11938 RVA: 0x000A7E81 File Offset: 0x000A6081
		public HMACSHA256()
			: this(Utils.GenerateRandom(64))
		{
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x000A7E90 File Offset: 0x000A6090
		public HMACSHA256(byte[] key)
		{
			this.m_hashName = "SHA256";
			this.m_hash1 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA256Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider"));
			this.m_hash2 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA256Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider"));
			this.HashSizeValue = 256;
			base.InitializeKey(key);
		}

		// Token: 0x02000463 RID: 1123
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002EA4 RID: 11940 RVA: 0x000A7F52 File Offset: 0x000A6152
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002EA5 RID: 11941 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06002EA6 RID: 11942 RVA: 0x000A7F5E File Offset: 0x000A615E
			internal HashAlgorithm <.ctor>b__1_0()
			{
				return new SHA256Managed();
			}

			// Token: 0x06002EA7 RID: 11943 RVA: 0x000A7F65 File Offset: 0x000A6165
			internal HashAlgorithm <.ctor>b__1_1()
			{
				return HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider");
			}

			// Token: 0x06002EA8 RID: 11944 RVA: 0x000A7F5E File Offset: 0x000A615E
			internal HashAlgorithm <.ctor>b__1_2()
			{
				return new SHA256Managed();
			}

			// Token: 0x06002EA9 RID: 11945 RVA: 0x000A7F65 File Offset: 0x000A6165
			internal HashAlgorithm <.ctor>b__1_3()
			{
				return HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider");
			}

			// Token: 0x04002037 RID: 8247
			public static readonly HMACSHA256.<>c <>9 = new HMACSHA256.<>c();

			// Token: 0x04002038 RID: 8248
			public static Func<HashAlgorithm> <>9__1_0;

			// Token: 0x04002039 RID: 8249
			public static Func<HashAlgorithm> <>9__1_1;

			// Token: 0x0400203A RID: 8250
			public static Func<HashAlgorithm> <>9__1_2;

			// Token: 0x0400203B RID: 8251
			public static Func<HashAlgorithm> <>9__1_3;
		}
	}
}
