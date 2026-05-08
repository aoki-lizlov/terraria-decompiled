using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000460 RID: 1120
	[ComVisible(true)]
	public class HMACRIPEMD160 : HMAC
	{
		// Token: 0x06002E9D RID: 11933 RVA: 0x000A7DBA File Offset: 0x000A5FBA
		public HMACRIPEMD160()
			: this(Utils.GenerateRandom(64))
		{
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x000A7DC9 File Offset: 0x000A5FC9
		public HMACRIPEMD160(byte[] key)
		{
			this.m_hashName = "RIPEMD160";
			this.m_hash1 = new RIPEMD160Managed();
			this.m_hash2 = new RIPEMD160Managed();
			this.HashSizeValue = 160;
			base.InitializeKey(key);
		}
	}
}
