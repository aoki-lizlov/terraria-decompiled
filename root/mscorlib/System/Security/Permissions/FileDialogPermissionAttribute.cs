using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000407 RID: 1031
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class FileDialogPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002B65 RID: 11109 RVA: 0x0009DE0C File Offset: 0x0009C00C
		public FileDialogPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06002B66 RID: 11110 RVA: 0x0009E0CC File Offset: 0x0009C2CC
		// (set) Token: 0x06002B67 RID: 11111 RVA: 0x0009E0D4 File Offset: 0x0009C2D4
		public bool Open
		{
			get
			{
				return this.canOpen;
			}
			set
			{
				this.canOpen = value;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06002B68 RID: 11112 RVA: 0x0009E0DD File Offset: 0x0009C2DD
		// (set) Token: 0x06002B69 RID: 11113 RVA: 0x0009E0E5 File Offset: 0x0009C2E5
		public bool Save
		{
			get
			{
				return this.canSave;
			}
			set
			{
				this.canSave = value;
			}
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x0009E0F0 File Offset: 0x0009C2F0
		public override IPermission CreatePermission()
		{
			FileDialogPermission fileDialogPermission;
			if (base.Unrestricted)
			{
				fileDialogPermission = new FileDialogPermission(PermissionState.Unrestricted);
			}
			else
			{
				FileDialogPermissionAccess fileDialogPermissionAccess = FileDialogPermissionAccess.None;
				if (this.canOpen)
				{
					fileDialogPermissionAccess |= FileDialogPermissionAccess.Open;
				}
				if (this.canSave)
				{
					fileDialogPermissionAccess |= FileDialogPermissionAccess.Save;
				}
				fileDialogPermission = new FileDialogPermission(fileDialogPermissionAccess);
			}
			return fileDialogPermission;
		}

		// Token: 0x04001EDC RID: 7900
		private bool canOpen;

		// Token: 0x04001EDD RID: 7901
		private bool canSave;
	}
}
