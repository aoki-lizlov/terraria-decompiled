using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	// Token: 0x0200046F RID: 1135
	[ComVisible(true)]
	public class PKCS1MaskGenerationMethod : MaskGenerationMethod
	{
		// Token: 0x06002EFE RID: 12030 RVA: 0x000A8D63 File Offset: 0x000A6F63
		public PKCS1MaskGenerationMethod()
		{
			this.HashNameValue = "SHA1";
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06002EFF RID: 12031 RVA: 0x000A8D76 File Offset: 0x000A6F76
		// (set) Token: 0x06002F00 RID: 12032 RVA: 0x000A8D7E File Offset: 0x000A6F7E
		public string HashName
		{
			get
			{
				return this.HashNameValue;
			}
			set
			{
				this.HashNameValue = value;
				if (this.HashNameValue == null)
				{
					this.HashNameValue = "SHA1";
				}
			}
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000A8D9A File Offset: 0x000A6F9A
		public override byte[] GenerateMask(byte[] rgbSeed, int cbReturn)
		{
			return PKCS1.MGF1(HashAlgorithm.Create(this.HashNameValue), rgbSeed, cbReturn);
		}

		// Token: 0x0400205C RID: 8284
		private string HashNameValue;
	}
}
