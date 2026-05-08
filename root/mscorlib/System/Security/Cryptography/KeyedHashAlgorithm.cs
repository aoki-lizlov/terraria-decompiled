using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000469 RID: 1129
	[ComVisible(true)]
	public abstract class KeyedHashAlgorithm : HashAlgorithm
	{
		// Token: 0x06002EC6 RID: 11974 RVA: 0x000A81FF File Offset: 0x000A63FF
		protected KeyedHashAlgorithm()
		{
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000A8207 File Offset: 0x000A6407
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.KeyValue != null)
				{
					Array.Clear(this.KeyValue, 0, this.KeyValue.Length);
				}
				this.KeyValue = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06002EC8 RID: 11976 RVA: 0x000A7AD9 File Offset: 0x000A5CD9
		// (set) Token: 0x06002EC9 RID: 11977 RVA: 0x000A8236 File Offset: 0x000A6436
		public virtual byte[] Key
		{
			get
			{
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (this.State != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Hash key cannot be changed after the first write to the stream."));
				}
				this.KeyValue = (byte[])value.Clone();
			}
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x000A8261 File Offset: 0x000A6461
		public new static KeyedHashAlgorithm Create()
		{
			return KeyedHashAlgorithm.Create("System.Security.Cryptography.KeyedHashAlgorithm");
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x000A826D File Offset: 0x000A646D
		public new static KeyedHashAlgorithm Create(string algName)
		{
			return (KeyedHashAlgorithm)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x04002048 RID: 8264
		protected byte[] KeyValue;
	}
}
