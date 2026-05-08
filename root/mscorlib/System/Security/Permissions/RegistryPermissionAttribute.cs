using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000424 RID: 1060
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class RegistryPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002CA1 RID: 11425 RVA: 0x0009DE0C File Offset: 0x0009C00C
		public RegistryPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06002CA2 RID: 11426 RVA: 0x0009DE15 File Offset: 0x0009C015
		// (set) Token: 0x06002CA3 RID: 11427 RVA: 0x000A1B4C File Offset: 0x0009FD4C
		[Obsolete("use newer properties")]
		public string All
		{
			get
			{
				throw new NotSupportedException("All");
			}
			set
			{
				this.create = value;
				this.read = value;
				this.write = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06002CA4 RID: 11428 RVA: 0x000A1B63 File Offset: 0x0009FD63
		// (set) Token: 0x06002CA5 RID: 11429 RVA: 0x000A1B6B File Offset: 0x0009FD6B
		public string Create
		{
			get
			{
				return this.create;
			}
			set
			{
				this.create = value;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06002CA6 RID: 11430 RVA: 0x000A1B74 File Offset: 0x0009FD74
		// (set) Token: 0x06002CA7 RID: 11431 RVA: 0x000A1B7C File Offset: 0x0009FD7C
		public string Read
		{
			get
			{
				return this.read;
			}
			set
			{
				this.read = value;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06002CA8 RID: 11432 RVA: 0x000A1B85 File Offset: 0x0009FD85
		// (set) Token: 0x06002CA9 RID: 11433 RVA: 0x000A1B8D File Offset: 0x0009FD8D
		public string Write
		{
			get
			{
				return this.write;
			}
			set
			{
				this.write = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06002CAA RID: 11434 RVA: 0x000A1B96 File Offset: 0x0009FD96
		// (set) Token: 0x06002CAB RID: 11435 RVA: 0x000A1B9E File Offset: 0x0009FD9E
		public string ChangeAccessControl
		{
			get
			{
				return this.changeAccessControl;
			}
			set
			{
				this.changeAccessControl = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x000A1BA7 File Offset: 0x0009FDA7
		// (set) Token: 0x06002CAD RID: 11437 RVA: 0x000A1BAF File Offset: 0x0009FDAF
		public string ViewAccessControl
		{
			get
			{
				return this.viewAccessControl;
			}
			set
			{
				this.viewAccessControl = value;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x00047E00 File Offset: 0x00046000
		// (set) Token: 0x06002CAF RID: 11439 RVA: 0x000A1B4C File Offset: 0x0009FD4C
		public string ViewAndModify
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				this.create = value;
				this.read = value;
				this.write = value;
			}
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x000A1BB8 File Offset: 0x0009FDB8
		public override IPermission CreatePermission()
		{
			RegistryPermission registryPermission;
			if (base.Unrestricted)
			{
				registryPermission = new RegistryPermission(PermissionState.Unrestricted);
			}
			else
			{
				registryPermission = new RegistryPermission(PermissionState.None);
				if (this.create != null)
				{
					registryPermission.AddPathList(RegistryPermissionAccess.Create, this.create);
				}
				if (this.read != null)
				{
					registryPermission.AddPathList(RegistryPermissionAccess.Read, this.read);
				}
				if (this.write != null)
				{
					registryPermission.AddPathList(RegistryPermissionAccess.Write, this.write);
				}
			}
			return registryPermission;
		}

		// Token: 0x04001F4F RID: 8015
		private string create;

		// Token: 0x04001F50 RID: 8016
		private string read;

		// Token: 0x04001F51 RID: 8017
		private string write;

		// Token: 0x04001F52 RID: 8018
		private string changeAccessControl;

		// Token: 0x04001F53 RID: 8019
		private string viewAccessControl;
	}
}
