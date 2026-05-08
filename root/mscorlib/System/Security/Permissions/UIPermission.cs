using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000430 RID: 1072
	[ComVisible(true)]
	[Serializable]
	public sealed class UIPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06002D25 RID: 11557 RVA: 0x000A3329 File Offset: 0x000A1529
		public UIPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._clipboard = UIPermissionClipboard.AllClipboard;
				this._window = UIPermissionWindow.AllWindows;
			}
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x000A3349 File Offset: 0x000A1549
		public UIPermission(UIPermissionClipboard clipboardFlag)
		{
			this.Clipboard = clipboardFlag;
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000A3358 File Offset: 0x000A1558
		public UIPermission(UIPermissionWindow windowFlag)
		{
			this.Window = windowFlag;
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x000A3367 File Offset: 0x000A1567
		public UIPermission(UIPermissionWindow windowFlag, UIPermissionClipboard clipboardFlag)
		{
			this.Clipboard = clipboardFlag;
			this.Window = windowFlag;
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06002D29 RID: 11561 RVA: 0x000A337D File Offset: 0x000A157D
		// (set) Token: 0x06002D2A RID: 11562 RVA: 0x000A3385 File Offset: 0x000A1585
		public UIPermissionClipboard Clipboard
		{
			get
			{
				return this._clipboard;
			}
			set
			{
				if (!Enum.IsDefined(typeof(UIPermissionClipboard), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "UIPermissionClipboard");
				}
				this._clipboard = value;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x000A33C5 File Offset: 0x000A15C5
		// (set) Token: 0x06002D2C RID: 11564 RVA: 0x000A33CD File Offset: 0x000A15CD
		public UIPermissionWindow Window
		{
			get
			{
				return this._window;
			}
			set
			{
				if (!Enum.IsDefined(typeof(UIPermissionWindow), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "UIPermissionWindow");
				}
				this._window = value;
			}
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x000A340D File Offset: 0x000A160D
		public override IPermission Copy()
		{
			return new UIPermission(this._window, this._clipboard);
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x000A3420 File Offset: 0x000A1620
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._window = UIPermissionWindow.AllWindows;
				this._clipboard = UIPermissionClipboard.AllClipboard;
				return;
			}
			string text = esd.Attribute("Window");
			if (text == null)
			{
				this._window = UIPermissionWindow.NoWindows;
			}
			else
			{
				this._window = (UIPermissionWindow)Enum.Parse(typeof(UIPermissionWindow), text);
			}
			string text2 = esd.Attribute("Clipboard");
			if (text2 == null)
			{
				this._clipboard = UIPermissionClipboard.NoClipboard;
				return;
			}
			this._clipboard = (UIPermissionClipboard)Enum.Parse(typeof(UIPermissionClipboard), text2);
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x000A34B8 File Offset: 0x000A16B8
		public override IPermission Intersect(IPermission target)
		{
			UIPermission uipermission = this.Cast(target);
			if (uipermission == null)
			{
				return null;
			}
			UIPermissionWindow uipermissionWindow = ((this._window < uipermission._window) ? this._window : uipermission._window);
			UIPermissionClipboard uipermissionClipboard = ((this._clipboard < uipermission._clipboard) ? this._clipboard : uipermission._clipboard);
			if (this.IsEmpty(uipermissionWindow, uipermissionClipboard))
			{
				return null;
			}
			return new UIPermission(uipermissionWindow, uipermissionClipboard);
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x000A3520 File Offset: 0x000A1720
		public override bool IsSubsetOf(IPermission target)
		{
			UIPermission uipermission = this.Cast(target);
			if (uipermission == null)
			{
				return this.IsEmpty(this._window, this._clipboard);
			}
			return uipermission.IsUnrestricted() || (this._window <= uipermission._window && this._clipboard <= uipermission._clipboard);
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x000A3576 File Offset: 0x000A1776
		public bool IsUnrestricted()
		{
			return this._window == UIPermissionWindow.AllWindows && this._clipboard == UIPermissionClipboard.AllClipboard;
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x000A358C File Offset: 0x000A178C
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._window == UIPermissionWindow.AllWindows && this._clipboard == UIPermissionClipboard.AllClipboard)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				if (this._window != UIPermissionWindow.NoWindows)
				{
					securityElement.AddAttribute("Window", this._window.ToString());
				}
				if (this._clipboard != UIPermissionClipboard.NoClipboard)
				{
					securityElement.AddAttribute("Clipboard", this._clipboard.ToString());
				}
			}
			return securityElement;
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x000A3610 File Offset: 0x000A1810
		public override IPermission Union(IPermission target)
		{
			UIPermission uipermission = this.Cast(target);
			if (uipermission == null)
			{
				return this.Copy();
			}
			UIPermissionWindow uipermissionWindow = ((this._window > uipermission._window) ? this._window : uipermission._window);
			UIPermissionClipboard uipermissionClipboard = ((this._clipboard > uipermission._clipboard) ? this._clipboard : uipermission._clipboard);
			if (this.IsEmpty(uipermissionWindow, uipermissionClipboard))
			{
				return null;
			}
			return new UIPermission(uipermissionWindow, uipermissionClipboard);
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x00029C12 File Offset: 0x00027E12
		int IBuiltInPermission.GetTokenIndex()
		{
			return 7;
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000A367C File Offset: 0x000A187C
		private bool IsEmpty(UIPermissionWindow w, UIPermissionClipboard c)
		{
			return w == UIPermissionWindow.NoWindows && c == UIPermissionClipboard.NoClipboard;
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000A3687 File Offset: 0x000A1887
		private UIPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			UIPermission uipermission = target as UIPermission;
			if (uipermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(UIPermission));
			}
			return uipermission;
		}

		// Token: 0x04001F83 RID: 8067
		private UIPermissionWindow _window;

		// Token: 0x04001F84 RID: 8068
		private UIPermissionClipboard _clipboard;

		// Token: 0x04001F85 RID: 8069
		private const int version = 1;
	}
}
