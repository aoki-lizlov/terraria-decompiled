using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200046C RID: 1132
	[ComVisible(true)]
	public abstract class MaskGenerationMethod
	{
		// Token: 0x06002EE4 RID: 12004
		[ComVisible(true)]
		public abstract byte[] GenerateMask(byte[] rgbSeed, int cbReturn);

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000025BE File Offset: 0x000007BE
		protected MaskGenerationMethod()
		{
		}
	}
}
