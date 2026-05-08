using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000477 RID: 1143
	[ComVisible(true)]
	public abstract class RIPEMD160 : HashAlgorithm
	{
		// Token: 0x06002F43 RID: 12099 RVA: 0x000AB0D2 File Offset: 0x000A92D2
		protected RIPEMD160()
		{
			this.HashSizeValue = 160;
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x000AB0E5 File Offset: 0x000A92E5
		public new static RIPEMD160 Create()
		{
			return RIPEMD160.Create("System.Security.Cryptography.RIPEMD160");
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x000AB0F1 File Offset: 0x000A92F1
		public new static RIPEMD160 Create(string hashName)
		{
			return (RIPEMD160)CryptoConfig.CreateFromName(hashName);
		}
	}
}
