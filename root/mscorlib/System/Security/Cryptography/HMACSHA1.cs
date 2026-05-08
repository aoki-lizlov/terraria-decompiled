using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000461 RID: 1121
	[ComVisible(true)]
	public class HMACSHA1 : HMAC
	{
		// Token: 0x06002E9F RID: 11935 RVA: 0x000A7E04 File Offset: 0x000A6004
		public HMACSHA1()
			: this(Utils.GenerateRandom(64))
		{
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x000A7E13 File Offset: 0x000A6013
		public HMACSHA1(byte[] key)
			: this(key, false)
		{
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x000A7E20 File Offset: 0x000A6020
		public HMACSHA1(byte[] key, bool useManagedSha1)
		{
			this.m_hashName = "SHA1";
			if (useManagedSha1)
			{
				this.m_hash1 = new SHA1Managed();
				this.m_hash2 = new SHA1Managed();
			}
			else
			{
				this.m_hash1 = new SHA1CryptoServiceProvider();
				this.m_hash2 = new SHA1CryptoServiceProvider();
			}
			this.HashSizeValue = 160;
			base.InitializeKey(key);
		}
	}
}
