using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200006A RID: 106
	internal abstract class MD2 : HashAlgorithm
	{
		// Token: 0x060002BC RID: 700 RVA: 0x0000F3A5 File Offset: 0x0000D5A5
		protected MD2()
		{
			this.HashSizeValue = 128;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
		public new static MD2 Create()
		{
			return MD2.Create("MD2");
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000F3C4 File Offset: 0x0000D5C4
		public new static MD2 Create(string hashName)
		{
			object obj = CryptoConfig.CreateFromName(hashName);
			if (obj == null)
			{
				obj = new MD2Managed();
			}
			return (MD2)obj;
		}
	}
}
