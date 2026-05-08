using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200046D RID: 1133
	[ComVisible(true)]
	public abstract class MD5 : HashAlgorithm
	{
		// Token: 0x06002EE6 RID: 12006 RVA: 0x0000F3A5 File Offset: 0x0000D5A5
		protected MD5()
		{
			this.HashSizeValue = 128;
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x000A872C File Offset: 0x000A692C
		public new static MD5 Create()
		{
			return MD5.Create("System.Security.Cryptography.MD5");
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000A8738 File Offset: 0x000A6938
		public new static MD5 Create(string algName)
		{
			return (MD5)CryptoConfig.CreateFromName(algName);
		}
	}
}
