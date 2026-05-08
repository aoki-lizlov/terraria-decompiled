using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000449 RID: 1097
	[ComVisible(true)]
	public abstract class AsymmetricKeyExchangeDeformatter
	{
		// Token: 0x06002E04 RID: 11780 RVA: 0x000025BE File Offset: 0x000007BE
		protected AsymmetricKeyExchangeDeformatter()
		{
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06002E05 RID: 11781
		// (set) Token: 0x06002E06 RID: 11782
		public abstract string Parameters { get; set; }

		// Token: 0x06002E07 RID: 11783
		public abstract void SetKey(AsymmetricAlgorithm key);

		// Token: 0x06002E08 RID: 11784
		public abstract byte[] DecryptKeyExchange(byte[] rgb);
	}
}
