using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000431 RID: 1073
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class UIPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002D37 RID: 11575 RVA: 0x0009DE0C File Offset: 0x0009C00C
		public UIPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06002D38 RID: 11576 RVA: 0x000A36A7 File Offset: 0x000A18A7
		// (set) Token: 0x06002D39 RID: 11577 RVA: 0x000A36AF File Offset: 0x000A18AF
		public UIPermissionClipboard Clipboard
		{
			get
			{
				return this.clipboard;
			}
			set
			{
				this.clipboard = value;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06002D3A RID: 11578 RVA: 0x000A36B8 File Offset: 0x000A18B8
		// (set) Token: 0x06002D3B RID: 11579 RVA: 0x000A36C0 File Offset: 0x000A18C0
		public UIPermissionWindow Window
		{
			get
			{
				return this.window;
			}
			set
			{
				this.window = value;
			}
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x000A36CC File Offset: 0x000A18CC
		public override IPermission CreatePermission()
		{
			UIPermission uipermission;
			if (base.Unrestricted)
			{
				uipermission = new UIPermission(PermissionState.Unrestricted);
			}
			else
			{
				uipermission = new UIPermission(this.window, this.clipboard);
			}
			return uipermission;
		}

		// Token: 0x04001F86 RID: 8070
		private UIPermissionClipboard clipboard;

		// Token: 0x04001F87 RID: 8071
		private UIPermissionWindow window;
	}
}
