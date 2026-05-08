using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000480 RID: 1152
	[ComVisible(true)]
	public abstract class SHA1 : HashAlgorithm
	{
		// Token: 0x06002FCD RID: 12237 RVA: 0x000AB0D2 File Offset: 0x000A92D2
		protected SHA1()
		{
			this.HashSizeValue = 160;
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000AEDE0 File Offset: 0x000ACFE0
		public new static SHA1 Create()
		{
			return SHA1.Create("System.Security.Cryptography.SHA1");
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000AEDEC File Offset: 0x000ACFEC
		public new static SHA1 Create(string hashName)
		{
			return (SHA1)CryptoConfig.CreateFromName(hashName);
		}
	}
}
