using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000406 RID: 1030
	[ComVisible(true)]
	[Serializable]
	public sealed class FileDialogPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06002B58 RID: 11096 RVA: 0x0009DEA6 File Offset: 0x0009C0A6
		public FileDialogPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._access = FileDialogPermissionAccess.OpenSave;
				return;
			}
			this._access = FileDialogPermissionAccess.None;
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x0009DEC7 File Offset: 0x0009C0C7
		public FileDialogPermission(FileDialogPermissionAccess access)
		{
			this.Access = access;
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06002B5A RID: 11098 RVA: 0x0009DED6 File Offset: 0x0009C0D6
		// (set) Token: 0x06002B5B RID: 11099 RVA: 0x0009DEDE File Offset: 0x0009C0DE
		public FileDialogPermissionAccess Access
		{
			get
			{
				return this._access;
			}
			set
			{
				if (!Enum.IsDefined(typeof(FileDialogPermissionAccess), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "FileDialogPermissionAccess");
				}
				this._access = value;
			}
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x0009DF1E File Offset: 0x0009C11E
		public override IPermission Copy()
		{
			return new FileDialogPermission(this._access);
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x0009DF2C File Offset: 0x0009C12C
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._access = FileDialogPermissionAccess.OpenSave;
				return;
			}
			string text = esd.Attribute("Access");
			if (text == null)
			{
				this._access = FileDialogPermissionAccess.None;
				return;
			}
			this._access = (FileDialogPermissionAccess)Enum.Parse(typeof(FileDialogPermissionAccess), text);
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x0009DF8C File Offset: 0x0009C18C
		public override IPermission Intersect(IPermission target)
		{
			FileDialogPermission fileDialogPermission = this.Cast(target);
			if (fileDialogPermission == null)
			{
				return null;
			}
			FileDialogPermissionAccess fileDialogPermissionAccess = this._access & fileDialogPermission._access;
			if (fileDialogPermissionAccess != FileDialogPermissionAccess.None)
			{
				return new FileDialogPermission(fileDialogPermissionAccess);
			}
			return null;
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x0009DFC0 File Offset: 0x0009C1C0
		public override bool IsSubsetOf(IPermission target)
		{
			FileDialogPermission fileDialogPermission = this.Cast(target);
			return fileDialogPermission != null && (this._access & fileDialogPermission._access) == this._access;
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x0009DFEF File Offset: 0x0009C1EF
		public bool IsUnrestricted()
		{
			return this._access == FileDialogPermissionAccess.OpenSave;
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x0009DFFC File Offset: 0x0009C1FC
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			switch (this._access)
			{
			case FileDialogPermissionAccess.Open:
				securityElement.AddAttribute("Access", "Open");
				break;
			case FileDialogPermissionAccess.Save:
				securityElement.AddAttribute("Access", "Save");
				break;
			case FileDialogPermissionAccess.OpenSave:
				securityElement.AddAttribute("Unrestricted", "true");
				break;
			}
			return securityElement;
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x0009E064 File Offset: 0x0009C264
		public override IPermission Union(IPermission target)
		{
			FileDialogPermission fileDialogPermission = this.Cast(target);
			if (fileDialogPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || fileDialogPermission.IsUnrestricted())
			{
				return new FileDialogPermission(PermissionState.Unrestricted);
			}
			return new FileDialogPermission(this._access | fileDialogPermission._access);
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x00003FB7 File Offset: 0x000021B7
		int IBuiltInPermission.GetTokenIndex()
		{
			return 1;
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x0009E0AC File Offset: 0x0009C2AC
		private FileDialogPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			FileDialogPermission fileDialogPermission = target as FileDialogPermission;
			if (fileDialogPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(FileDialogPermission));
			}
			return fileDialogPermission;
		}

		// Token: 0x04001EDA RID: 7898
		private const int version = 1;

		// Token: 0x04001EDB RID: 7899
		private FileDialogPermissionAccess _access;
	}
}
