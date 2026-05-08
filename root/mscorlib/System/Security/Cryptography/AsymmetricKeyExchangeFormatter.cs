using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200044A RID: 1098
	[ComVisible(true)]
	public abstract class AsymmetricKeyExchangeFormatter
	{
		// Token: 0x06002E09 RID: 11785 RVA: 0x000025BE File Offset: 0x000007BE
		protected AsymmetricKeyExchangeFormatter()
		{
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06002E0A RID: 11786
		public abstract string Parameters { get; }

		// Token: 0x06002E0B RID: 11787
		public abstract void SetKey(AsymmetricAlgorithm key);

		// Token: 0x06002E0C RID: 11788
		public abstract byte[] CreateKeyExchange(byte[] data);

		// Token: 0x06002E0D RID: 11789
		public abstract byte[] CreateKeyExchange(byte[] data, Type symAlgType);
	}
}
