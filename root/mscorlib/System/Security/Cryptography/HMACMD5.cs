using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200045F RID: 1119
	[ComVisible(true)]
	public class HMACMD5 : HMAC
	{
		// Token: 0x06002E9B RID: 11931 RVA: 0x000A7D70 File Offset: 0x000A5F70
		public HMACMD5()
			: this(Utils.GenerateRandom(64))
		{
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000A7D7F File Offset: 0x000A5F7F
		public HMACMD5(byte[] key)
		{
			this.m_hashName = "MD5";
			this.m_hash1 = new MD5CryptoServiceProvider();
			this.m_hash2 = new MD5CryptoServiceProvider();
			this.HashSizeValue = 128;
			base.InitializeKey(key);
		}
	}
}
