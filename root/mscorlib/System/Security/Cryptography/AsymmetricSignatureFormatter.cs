using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200044C RID: 1100
	[ComVisible(true)]
	public abstract class AsymmetricSignatureFormatter
	{
		// Token: 0x06002E13 RID: 11795 RVA: 0x000025BE File Offset: 0x000007BE
		protected AsymmetricSignatureFormatter()
		{
		}

		// Token: 0x06002E14 RID: 11796
		public abstract void SetKey(AsymmetricAlgorithm key);

		// Token: 0x06002E15 RID: 11797
		public abstract void SetHashAlgorithm(string strName);

		// Token: 0x06002E16 RID: 11798 RVA: 0x000A637A File Offset: 0x000A457A
		public virtual byte[] CreateSignature(HashAlgorithm hash)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			this.SetHashAlgorithm(hash.ToString());
			return this.CreateSignature(hash.Hash);
		}

		// Token: 0x06002E17 RID: 11799
		public abstract byte[] CreateSignature(byte[] rgbHash);
	}
}
