using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000488 RID: 1160
	[ComVisible(true)]
	public class SignatureDescription
	{
		// Token: 0x06003015 RID: 12309 RVA: 0x000025BE File Offset: 0x000007BE
		public SignatureDescription()
		{
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000B0C1C File Offset: 0x000AEE1C
		public SignatureDescription(SecurityElement el)
		{
			if (el == null)
			{
				throw new ArgumentNullException("el");
			}
			this._strKey = el.SearchForTextOfTag("Key");
			this._strDigest = el.SearchForTextOfTag("Digest");
			this._strFormatter = el.SearchForTextOfTag("Formatter");
			this._strDeformatter = el.SearchForTextOfTag("Deformatter");
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06003017 RID: 12311 RVA: 0x000B0C81 File Offset: 0x000AEE81
		// (set) Token: 0x06003018 RID: 12312 RVA: 0x000B0C89 File Offset: 0x000AEE89
		public string KeyAlgorithm
		{
			get
			{
				return this._strKey;
			}
			set
			{
				this._strKey = value;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06003019 RID: 12313 RVA: 0x000B0C92 File Offset: 0x000AEE92
		// (set) Token: 0x0600301A RID: 12314 RVA: 0x000B0C9A File Offset: 0x000AEE9A
		public string DigestAlgorithm
		{
			get
			{
				return this._strDigest;
			}
			set
			{
				this._strDigest = value;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600301B RID: 12315 RVA: 0x000B0CA3 File Offset: 0x000AEEA3
		// (set) Token: 0x0600301C RID: 12316 RVA: 0x000B0CAB File Offset: 0x000AEEAB
		public string FormatterAlgorithm
		{
			get
			{
				return this._strFormatter;
			}
			set
			{
				this._strFormatter = value;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x0600301D RID: 12317 RVA: 0x000B0CB4 File Offset: 0x000AEEB4
		// (set) Token: 0x0600301E RID: 12318 RVA: 0x000B0CBC File Offset: 0x000AEEBC
		public string DeformatterAlgorithm
		{
			get
			{
				return this._strDeformatter;
			}
			set
			{
				this._strDeformatter = value;
			}
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000B0CC5 File Offset: 0x000AEEC5
		public virtual AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = (AsymmetricSignatureDeformatter)CryptoConfig.CreateFromName(this._strDeformatter);
			asymmetricSignatureDeformatter.SetKey(key);
			return asymmetricSignatureDeformatter;
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000B0CDE File Offset: 0x000AEEDE
		public virtual AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = (AsymmetricSignatureFormatter)CryptoConfig.CreateFromName(this._strFormatter);
			asymmetricSignatureFormatter.SetKey(key);
			return asymmetricSignatureFormatter;
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000B0CF7 File Offset: 0x000AEEF7
		public virtual HashAlgorithm CreateDigest()
		{
			return (HashAlgorithm)CryptoConfig.CreateFromName(this._strDigest);
		}

		// Token: 0x040020B4 RID: 8372
		private string _strKey;

		// Token: 0x040020B5 RID: 8373
		private string _strDigest;

		// Token: 0x040020B6 RID: 8374
		private string _strFormatter;

		// Token: 0x040020B7 RID: 8375
		private string _strDeformatter;
	}
}
