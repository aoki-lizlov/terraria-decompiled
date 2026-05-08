using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000019 RID: 25
	internal class FNAWindow : GameWindow
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0000A93E File Offset: 0x00008B3E
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x0000A950 File Offset: 0x00008B50
		[DefaultValue(false)]
		public override bool AllowUserResizing
		{
			get
			{
				return FNAPlatform.GetWindowResizable(this.window);
			}
			set
			{
				FNAPlatform.SetWindowResizable(this.window, value);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0000A963 File Offset: 0x00008B63
		public override Rectangle ClientBounds
		{
			get
			{
				return FNAPlatform.GetWindowBounds(this.window);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0000A975 File Offset: 0x00008B75
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x0000A97D File Offset: 0x00008B7D
		public override DisplayOrientation CurrentOrientation
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentOrientation>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<CurrentOrientation>k__BackingField = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0000A986 File Offset: 0x00008B86
		public override IntPtr Handle
		{
			get
			{
				return this.window;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0000A98E File Offset: 0x00008B8E
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x0000A9A0 File Offset: 0x00008BA0
		public override bool IsBorderlessEXT
		{
			get
			{
				return FNAPlatform.GetWindowBorderless(this.window);
			}
			set
			{
				FNAPlatform.SetWindowBorderless(this.window, value);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0000A9B3 File Offset: 0x00008BB3
		public override string ScreenDeviceName
		{
			get
			{
				return this.deviceName;
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0000A9BB File Offset: 0x00008BBB
		internal FNAWindow(IntPtr nativeWindow, string display)
		{
			this.window = nativeWindow;
			this.deviceName = display;
			this.wantsFullscreen = false;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0000A9D8 File Offset: 0x00008BD8
		public override void BeginScreenDeviceChange(bool willBeFullScreen)
		{
			this.wantsFullscreen = willBeFullScreen;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		public override void EndScreenDeviceChange(string screenDeviceName, int clientWidth, int clientHeight)
		{
			string text = this.deviceName;
			FNAPlatform.ApplyWindowChanges(this.window, clientWidth, clientHeight, this.wantsFullscreen, screenDeviceName, ref this.deviceName);
			if (this.deviceName != text)
			{
				base.OnScreenDeviceNameChanged();
			}
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0000AA2B File Offset: 0x00008C2B
		internal void INTERNAL_ClientSizeChanged()
		{
			base.OnClientSizeChanged();
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0000AA33 File Offset: 0x00008C33
		internal void INTERNAL_ScreenDeviceNameChanged()
		{
			base.OnScreenDeviceNameChanged();
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0000AA3B File Offset: 0x00008C3B
		internal void INTERNAL_OnOrientationChanged()
		{
			base.OnOrientationChanged();
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0000AA43 File Offset: 0x00008C43
		protected internal override void SetSupportedOrientations(DisplayOrientation orientations)
		{
			FNALoggerEXT.LogWarn("Setting SupportedOrientations has no effect!");
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0000AA54 File Offset: 0x00008C54
		protected override void SetTitle(string title)
		{
			FNAPlatform.SetWindowTitle(this.window, title);
		}

		// Token: 0x04000505 RID: 1285
		[CompilerGenerated]
		private DisplayOrientation <CurrentOrientation>k__BackingField;

		// Token: 0x04000506 RID: 1286
		private IntPtr window;

		// Token: 0x04000507 RID: 1287
		private string deviceName;

		// Token: 0x04000508 RID: 1288
		private bool wantsFullscreen;
	}
}
