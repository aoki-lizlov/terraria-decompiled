using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200006C RID: 108
	internal abstract class MD4 : HashAlgorithm
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000F3A5 File Offset: 0x0000D5A5
		protected MD4()
		{
			this.HashSizeValue = 128;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000F692 File Offset: 0x0000D892
		public new static MD4 Create()
		{
			return MD4.Create("MD4");
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000F6A0 File Offset: 0x0000D8A0
		public new static MD4 Create(string hashName)
		{
			object obj = CryptoConfig.CreateFromName(hashName);
			if (obj == null)
			{
				obj = new MD4Managed();
			}
			return (MD4)obj;
		}
	}
}
