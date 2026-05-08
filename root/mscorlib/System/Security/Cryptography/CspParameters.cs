using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Cryptography
{
	// Token: 0x02000456 RID: 1110
	[ComVisible(true)]
	public sealed class CspParameters
	{
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06002E43 RID: 11843 RVA: 0x000A6A9A File Offset: 0x000A4C9A
		// (set) Token: 0x06002E44 RID: 11844 RVA: 0x000A6AA4 File Offset: 0x000A4CA4
		public CspProviderFlags Flags
		{
			get
			{
				return (CspProviderFlags)this.m_flags;
			}
			set
			{
				int num = 255;
				if ((value & (CspProviderFlags)(~(CspProviderFlags)num)) != CspProviderFlags.NoFlags)
				{
					throw new ArgumentException(Environment.GetResourceString("Illegal enum value: {0}.", new object[] { (int)value }), "value");
				}
				this.m_flags = (int)value;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06002E45 RID: 11845 RVA: 0x000A6AEA File Offset: 0x000A4CEA
		// (set) Token: 0x06002E46 RID: 11846 RVA: 0x000A6AF2 File Offset: 0x000A4CF2
		public CryptoKeySecurity CryptoKeySecurity
		{
			get
			{
				return this.m_cryptoKeySecurity;
			}
			set
			{
				this.m_cryptoKeySecurity = value;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06002E47 RID: 11847 RVA: 0x000A6AFB File Offset: 0x000A4CFB
		// (set) Token: 0x06002E48 RID: 11848 RVA: 0x000A6B03 File Offset: 0x000A4D03
		public SecureString KeyPassword
		{
			get
			{
				return this.m_keyPassword;
			}
			set
			{
				this.m_keyPassword = value;
				this.m_parentWindowHandle = IntPtr.Zero;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x000A6B17 File Offset: 0x000A4D17
		// (set) Token: 0x06002E4A RID: 11850 RVA: 0x000A6B1F File Offset: 0x000A4D1F
		public IntPtr ParentWindowHandle
		{
			get
			{
				return this.m_parentWindowHandle;
			}
			set
			{
				this.m_parentWindowHandle = value;
				this.m_keyPassword = null;
			}
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x000A6B2F File Offset: 0x000A4D2F
		public CspParameters()
			: this(1, null, null)
		{
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x000A6B3A File Offset: 0x000A4D3A
		public CspParameters(int dwTypeIn)
			: this(dwTypeIn, null, null)
		{
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x000A6B45 File Offset: 0x000A4D45
		public CspParameters(int dwTypeIn, string strProviderNameIn)
			: this(dwTypeIn, strProviderNameIn, null)
		{
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x000A6B50 File Offset: 0x000A4D50
		public CspParameters(int dwTypeIn, string strProviderNameIn, string strContainerNameIn)
			: this(dwTypeIn, strProviderNameIn, strContainerNameIn, CspProviderFlags.NoFlags)
		{
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x000A6B5C File Offset: 0x000A4D5C
		public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, SecureString keyPassword)
			: this(providerType, providerName, keyContainerName)
		{
			this.m_cryptoKeySecurity = cryptoKeySecurity;
			this.m_keyPassword = keyPassword;
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x000A6B77 File Offset: 0x000A4D77
		public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, IntPtr parentWindowHandle)
			: this(providerType, providerName, keyContainerName)
		{
			this.m_cryptoKeySecurity = cryptoKeySecurity;
			this.m_parentWindowHandle = parentWindowHandle;
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x000A6B92 File Offset: 0x000A4D92
		internal CspParameters(int providerType, string providerName, string keyContainerName, CspProviderFlags flags)
		{
			this.ProviderType = providerType;
			this.ProviderName = providerName;
			this.KeyContainerName = keyContainerName;
			this.KeyNumber = -1;
			this.Flags = flags;
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x000A6BC0 File Offset: 0x000A4DC0
		internal CspParameters(CspParameters parameters)
		{
			this.ProviderType = parameters.ProviderType;
			this.ProviderName = parameters.ProviderName;
			this.KeyContainerName = parameters.KeyContainerName;
			this.KeyNumber = parameters.KeyNumber;
			this.Flags = parameters.Flags;
			this.m_cryptoKeySecurity = parameters.m_cryptoKeySecurity;
			this.m_keyPassword = parameters.m_keyPassword;
			this.m_parentWindowHandle = parameters.m_parentWindowHandle;
		}

		// Token: 0x0400201A RID: 8218
		public int ProviderType;

		// Token: 0x0400201B RID: 8219
		public string ProviderName;

		// Token: 0x0400201C RID: 8220
		public string KeyContainerName;

		// Token: 0x0400201D RID: 8221
		public int KeyNumber;

		// Token: 0x0400201E RID: 8222
		private int m_flags;

		// Token: 0x0400201F RID: 8223
		private CryptoKeySecurity m_cryptoKeySecurity;

		// Token: 0x04002020 RID: 8224
		private SecureString m_keyPassword;

		// Token: 0x04002021 RID: 8225
		private IntPtr m_parentWindowHandle;
	}
}
