using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000486 RID: 1158
	[ComVisible(true)]
	public abstract class SHA512 : HashAlgorithm
	{
		// Token: 0x06003001 RID: 12289 RVA: 0x000B049E File Offset: 0x000AE69E
		protected SHA512()
		{
			this.HashSizeValue = 512;
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x000B04B1 File Offset: 0x000AE6B1
		public new static SHA512 Create()
		{
			return SHA512.Create("System.Security.Cryptography.SHA512");
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x000B04BD File Offset: 0x000AE6BD
		public new static SHA512 Create(string hashName)
		{
			return (SHA512)CryptoConfig.CreateFromName(hashName);
		}
	}
}
