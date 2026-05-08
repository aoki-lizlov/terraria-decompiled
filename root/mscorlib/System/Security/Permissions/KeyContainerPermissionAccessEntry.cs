using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace System.Security.Permissions
{
	// Token: 0x02000416 RID: 1046
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntry
	{
		// Token: 0x06002BFE RID: 11262 RVA: 0x0009F990 File Offset: 0x0009DB90
		public KeyContainerPermissionAccessEntry(CspParameters parameters, KeyContainerPermissionFlags flags)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this.ProviderName = parameters.ProviderName;
			this.ProviderType = parameters.ProviderType;
			this.KeyContainerName = parameters.KeyContainerName;
			this.KeySpec = parameters.KeyNumber;
			this.Flags = flags;
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x0009F9E8 File Offset: 0x0009DBE8
		public KeyContainerPermissionAccessEntry(string keyContainerName, KeyContainerPermissionFlags flags)
		{
			this.KeyContainerName = keyContainerName;
			this.Flags = flags;
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x0009F9FE File Offset: 0x0009DBFE
		public KeyContainerPermissionAccessEntry(string keyStore, string providerName, int providerType, string keyContainerName, int keySpec, KeyContainerPermissionFlags flags)
		{
			this.KeyStore = keyStore;
			this.ProviderName = providerName;
			this.ProviderType = providerType;
			this.KeyContainerName = keyContainerName;
			this.KeySpec = keySpec;
			this.Flags = flags;
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06002C01 RID: 11265 RVA: 0x0009FA33 File Offset: 0x0009DC33
		// (set) Token: 0x06002C02 RID: 11266 RVA: 0x0009FA3B File Offset: 0x0009DC3B
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				if ((value & KeyContainerPermissionFlags.AllFlags) != KeyContainerPermissionFlags.NoFlags)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "KeyContainerPermissionFlags");
				}
				this._flags = value;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x0009FA6D File Offset: 0x0009DC6D
		// (set) Token: 0x06002C04 RID: 11268 RVA: 0x0009FA75 File Offset: 0x0009DC75
		public string KeyContainerName
		{
			get
			{
				return this._containerName;
			}
			set
			{
				this._containerName = value;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x0009FA7E File Offset: 0x0009DC7E
		// (set) Token: 0x06002C06 RID: 11270 RVA: 0x0009FA86 File Offset: 0x0009DC86
		public int KeySpec
		{
			get
			{
				return this._spec;
			}
			set
			{
				this._spec = value;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x0009FA8F File Offset: 0x0009DC8F
		// (set) Token: 0x06002C08 RID: 11272 RVA: 0x0009FA97 File Offset: 0x0009DC97
		public string KeyStore
		{
			get
			{
				return this._store;
			}
			set
			{
				this._store = value;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x0009FAA0 File Offset: 0x0009DCA0
		// (set) Token: 0x06002C0A RID: 11274 RVA: 0x0009FAA8 File Offset: 0x0009DCA8
		public string ProviderName
		{
			get
			{
				return this._providerName;
			}
			set
			{
				this._providerName = value;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06002C0B RID: 11275 RVA: 0x0009FAB1 File Offset: 0x0009DCB1
		// (set) Token: 0x06002C0C RID: 11276 RVA: 0x0009FAB9 File Offset: 0x0009DCB9
		public int ProviderType
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x0009FAC4 File Offset: 0x0009DCC4
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = o as KeyContainerPermissionAccessEntry;
			return keyContainerPermissionAccessEntry != null && this._flags == keyContainerPermissionAccessEntry._flags && !(this._containerName != keyContainerPermissionAccessEntry._containerName) && !(this._store != keyContainerPermissionAccessEntry._store) && !(this._providerName != keyContainerPermissionAccessEntry._providerName) && this._type == keyContainerPermissionAccessEntry._type;
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x0009FB44 File Offset: 0x0009DD44
		public override int GetHashCode()
		{
			int num = this._type ^ this._spec ^ (int)this._flags;
			if (this._containerName != null)
			{
				num ^= this._containerName.GetHashCode();
			}
			if (this._store != null)
			{
				num ^= this._store.GetHashCode();
			}
			if (this._providerName != null)
			{
				num ^= this._providerName.GetHashCode();
			}
			return num;
		}

		// Token: 0x04001F18 RID: 7960
		private KeyContainerPermissionFlags _flags;

		// Token: 0x04001F19 RID: 7961
		private string _containerName;

		// Token: 0x04001F1A RID: 7962
		private int _spec;

		// Token: 0x04001F1B RID: 7963
		private string _store;

		// Token: 0x04001F1C RID: 7964
		private string _providerName;

		// Token: 0x04001F1D RID: 7965
		private int _type;
	}
}
