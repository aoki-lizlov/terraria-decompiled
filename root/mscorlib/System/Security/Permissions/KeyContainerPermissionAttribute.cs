using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000419 RID: 1049
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002C22 RID: 11298 RVA: 0x0009FD2E File Offset: 0x0009DF2E
		public KeyContainerPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this._spec = -1;
			this._type = -1;
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06002C23 RID: 11299 RVA: 0x0009FD45 File Offset: 0x0009DF45
		// (set) Token: 0x06002C24 RID: 11300 RVA: 0x0009FD4D File Offset: 0x0009DF4D
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06002C25 RID: 11301 RVA: 0x0009FD56 File Offset: 0x0009DF56
		// (set) Token: 0x06002C26 RID: 11302 RVA: 0x0009FD5E File Offset: 0x0009DF5E
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

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06002C27 RID: 11303 RVA: 0x0009FD67 File Offset: 0x0009DF67
		// (set) Token: 0x06002C28 RID: 11304 RVA: 0x0009FD6F File Offset: 0x0009DF6F
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

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06002C29 RID: 11305 RVA: 0x0009FD78 File Offset: 0x0009DF78
		// (set) Token: 0x06002C2A RID: 11306 RVA: 0x0009FD80 File Offset: 0x0009DF80
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

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06002C2B RID: 11307 RVA: 0x0009FD89 File Offset: 0x0009DF89
		// (set) Token: 0x06002C2C RID: 11308 RVA: 0x0009FD91 File Offset: 0x0009DF91
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

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06002C2D RID: 11309 RVA: 0x0009FD9A File Offset: 0x0009DF9A
		// (set) Token: 0x06002C2E RID: 11310 RVA: 0x0009FDA2 File Offset: 0x0009DFA2
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

		// Token: 0x06002C2F RID: 11311 RVA: 0x0009FDAC File Offset: 0x0009DFAC
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new KeyContainerPermission(PermissionState.Unrestricted);
			}
			if (this.EmptyEntry())
			{
				return new KeyContainerPermission(this._flags);
			}
			KeyContainerPermissionAccessEntry[] array = new KeyContainerPermissionAccessEntry[]
			{
				new KeyContainerPermissionAccessEntry(this._store, this._providerName, this._type, this._containerName, this._spec, this._flags)
			};
			return new KeyContainerPermission(this._flags, array);
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x0009FE1B File Offset: 0x0009E01B
		private bool EmptyEntry()
		{
			return this._containerName == null && this._spec == 0 && this._store == null && this._providerName == null && this._type == 0;
		}

		// Token: 0x04001F20 RID: 7968
		private KeyContainerPermissionFlags _flags;

		// Token: 0x04001F21 RID: 7969
		private string _containerName;

		// Token: 0x04001F22 RID: 7970
		private int _spec;

		// Token: 0x04001F23 RID: 7971
		private string _store;

		// Token: 0x04001F24 RID: 7972
		private string _providerName;

		// Token: 0x04001F25 RID: 7973
		private int _type;
	}
}
