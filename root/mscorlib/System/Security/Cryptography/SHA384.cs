using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000484 RID: 1156
	[ComVisible(true)]
	public abstract class SHA384 : HashAlgorithm
	{
		// Token: 0x06002FED RID: 12269 RVA: 0x000AFD06 File Offset: 0x000ADF06
		protected SHA384()
		{
			this.HashSizeValue = 384;
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000AFD19 File Offset: 0x000ADF19
		public new static SHA384 Create()
		{
			return SHA384.Create("System.Security.Cryptography.SHA384");
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000AFD25 File Offset: 0x000ADF25
		public new static SHA384 Create(string hashName)
		{
			return (SHA384)CryptoConfig.CreateFromName(hashName);
		}
	}
}
