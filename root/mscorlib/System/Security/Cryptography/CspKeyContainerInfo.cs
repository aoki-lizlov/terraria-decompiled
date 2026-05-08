using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Cryptography
{
	// Token: 0x02000498 RID: 1176
	[ComVisible(true)]
	public sealed class CspKeyContainerInfo
	{
		// Token: 0x0600308F RID: 12431 RVA: 0x000B2D3C File Offset: 0x000B0F3C
		public CspKeyContainerInfo(CspParameters parameters)
		{
			this._params = parameters;
			this._random = true;
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06003090 RID: 12432 RVA: 0x00003FB7 File Offset: 0x000021B7
		public bool Accessible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06003091 RID: 12433 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public CryptoKeySecurity CryptoKeySecurity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x00003FB7 File Offset: 0x000021B7
		public bool Exportable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x0000408A File Offset: 0x0000228A
		public bool HardwareDevice
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06003094 RID: 12436 RVA: 0x000B2D52 File Offset: 0x000B0F52
		public string KeyContainerName
		{
			get
			{
				return this._params.KeyContainerName;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x000B2D5F File Offset: 0x000B0F5F
		public KeyNumber KeyNumber
		{
			get
			{
				return (KeyNumber)this._params.KeyNumber;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x0000408A File Offset: 0x0000228A
		public bool MachineKeyStore
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x0000408A File Offset: 0x0000228A
		public bool Protected
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x000B2D6C File Offset: 0x000B0F6C
		public string ProviderName
		{
			get
			{
				return this._params.ProviderName;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x000B2D79 File Offset: 0x000B0F79
		public int ProviderType
		{
			get
			{
				return this._params.ProviderType;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x000B2D86 File Offset: 0x000B0F86
		public bool RandomlyGenerated
		{
			get
			{
				return this._random;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x0600309B RID: 12443 RVA: 0x0000408A File Offset: 0x0000228A
		public bool Removable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x000B2D8E File Offset: 0x000B0F8E
		public string UniqueKeyContainerName
		{
			get
			{
				return this._params.ProviderName + "\\" + this._params.KeyContainerName;
			}
		}

		// Token: 0x040021CB RID: 8651
		private CspParameters _params;

		// Token: 0x040021CC RID: 8652
		internal bool _random;
	}
}
