using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200044B RID: 1099
	[ComVisible(true)]
	public abstract class AsymmetricSignatureDeformatter
	{
		// Token: 0x06002E0E RID: 11790 RVA: 0x000025BE File Offset: 0x000007BE
		protected AsymmetricSignatureDeformatter()
		{
		}

		// Token: 0x06002E0F RID: 11791
		public abstract void SetKey(AsymmetricAlgorithm key);

		// Token: 0x06002E10 RID: 11792
		public abstract void SetHashAlgorithm(string strName);

		// Token: 0x06002E11 RID: 11793 RVA: 0x000A6351 File Offset: 0x000A4551
		public virtual bool VerifySignature(HashAlgorithm hash, byte[] rgbSignature)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			this.SetHashAlgorithm(hash.ToString());
			return this.VerifySignature(hash.Hash, rgbSignature);
		}

		// Token: 0x06002E12 RID: 11794
		public abstract bool VerifySignature(byte[] rgbHash, byte[] rgbSignature);
	}
}
