using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000482 RID: 1154
	[ComVisible(true)]
	public abstract class SHA256 : HashAlgorithm
	{
		// Token: 0x06002FD9 RID: 12249 RVA: 0x000AF5A4 File Offset: 0x000AD7A4
		protected SHA256()
		{
			this.HashSizeValue = 256;
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x000AF5B7 File Offset: 0x000AD7B7
		public new static SHA256 Create()
		{
			return SHA256.Create("System.Security.Cryptography.SHA256");
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x000AF5C3 File Offset: 0x000AD7C3
		public new static SHA256 Create(string hashName)
		{
			return (SHA256)CryptoConfig.CreateFromName(hashName);
		}
	}
}
