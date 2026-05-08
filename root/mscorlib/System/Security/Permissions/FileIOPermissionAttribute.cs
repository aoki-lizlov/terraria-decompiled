using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200040A RID: 1034
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class FileIOPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002B90 RID: 11152 RVA: 0x0009DE0C File Offset: 0x0009C00C
		public FileIOPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x0009DE15 File Offset: 0x0009C015
		// (set) Token: 0x06002B92 RID: 11154 RVA: 0x0009EC40 File Offset: 0x0009CE40
		[Obsolete("use newer properties")]
		public string All
		{
			get
			{
				throw new NotSupportedException("All");
			}
			set
			{
				this.append = value;
				this.path = value;
				this.read = value;
				this.write = value;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06002B93 RID: 11155 RVA: 0x0009EC5E File Offset: 0x0009CE5E
		// (set) Token: 0x06002B94 RID: 11156 RVA: 0x0009EC66 File Offset: 0x0009CE66
		public string Append
		{
			get
			{
				return this.append;
			}
			set
			{
				this.append = value;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06002B95 RID: 11157 RVA: 0x0009EC6F File Offset: 0x0009CE6F
		// (set) Token: 0x06002B96 RID: 11158 RVA: 0x0009EC77 File Offset: 0x0009CE77
		public string PathDiscovery
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = value;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06002B97 RID: 11159 RVA: 0x0009EC80 File Offset: 0x0009CE80
		// (set) Token: 0x06002B98 RID: 11160 RVA: 0x0009EC88 File Offset: 0x0009CE88
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

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06002B99 RID: 11161 RVA: 0x0009EC91 File Offset: 0x0009CE91
		// (set) Token: 0x06002B9A RID: 11162 RVA: 0x0009EC99 File Offset: 0x0009CE99
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

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x0009ECA2 File Offset: 0x0009CEA2
		// (set) Token: 0x06002B9C RID: 11164 RVA: 0x0009ECAA File Offset: 0x0009CEAA
		public FileIOPermissionAccess AllFiles
		{
			get
			{
				return this.allFiles;
			}
			set
			{
				this.allFiles = value;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06002B9D RID: 11165 RVA: 0x0009ECB3 File Offset: 0x0009CEB3
		// (set) Token: 0x06002B9E RID: 11166 RVA: 0x0009ECBB File Offset: 0x0009CEBB
		public FileIOPermissionAccess AllLocalFiles
		{
			get
			{
				return this.allLocalFiles;
			}
			set
			{
				this.allLocalFiles = value;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06002B9F RID: 11167 RVA: 0x0009ECC4 File Offset: 0x0009CEC4
		// (set) Token: 0x06002BA0 RID: 11168 RVA: 0x0009ECCC File Offset: 0x0009CECC
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

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06002BA1 RID: 11169 RVA: 0x0009ECD5 File Offset: 0x0009CED5
		// (set) Token: 0x06002BA2 RID: 11170 RVA: 0x0009ECDD File Offset: 0x0009CEDD
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

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x00047E00 File Offset: 0x00046000
		// (set) Token: 0x06002BA4 RID: 11172 RVA: 0x0009EC40 File Offset: 0x0009CE40
		public string ViewAndModify
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				this.append = value;
				this.path = value;
				this.read = value;
				this.write = value;
			}
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x0009ECE8 File Offset: 0x0009CEE8
		public override IPermission CreatePermission()
		{
			FileIOPermission fileIOPermission;
			if (base.Unrestricted)
			{
				fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
			}
			else
			{
				fileIOPermission = new FileIOPermission(PermissionState.None);
				if (this.append != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.Append, this.append);
				}
				if (this.path != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.PathDiscovery, this.path);
				}
				if (this.read != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.Read, this.read);
				}
				if (this.write != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.Write, this.write);
				}
			}
			return fileIOPermission;
		}

		// Token: 0x04001EEF RID: 7919
		private string append;

		// Token: 0x04001EF0 RID: 7920
		private string path;

		// Token: 0x04001EF1 RID: 7921
		private string read;

		// Token: 0x04001EF2 RID: 7922
		private string write;

		// Token: 0x04001EF3 RID: 7923
		private FileIOPermissionAccess allFiles;

		// Token: 0x04001EF4 RID: 7924
		private FileIOPermissionAccess allLocalFiles;

		// Token: 0x04001EF5 RID: 7925
		private string changeAccessControl;

		// Token: 0x04001EF6 RID: 7926
		private string viewAccessControl;
	}
}
