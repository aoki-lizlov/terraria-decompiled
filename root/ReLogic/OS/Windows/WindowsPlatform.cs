using System;
using ReLogic.Localization.IME;

namespace ReLogic.OS.Windows
{
	// Token: 0x0200006D RID: 109
	internal class WindowsPlatform : Platform
	{
		// Token: 0x06000261 RID: 609 RVA: 0x0000A2F7 File Offset: 0x000084F7
		public WindowsPlatform()
			: base(PlatformType.Windows)
		{
			base.RegisterService<IClipboard>(new Clipboard());
			base.RegisterService<IPathService>(new PathService());
			base.RegisterService<IWindowService>(new WindowService());
			base.RegisterService<IImeService>(new UnsupportedPlatformIme());
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A32C File Offset: 0x0000852C
		public override void InitializeClientServices(IntPtr windowHandle)
		{
			if (this._wndProcHook == null)
			{
				this._wndProcHook = new WindowsMessageHook(windowHandle);
			}
			base.RegisterService<IImeService>(new WindowsIme(this._wndProcHook, windowHandle));
			base.RegisterService<IMouseNotifier>(new WindowsMouseNotifier(this._wndProcHook));
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A365 File Offset: 0x00008565
		protected override void Dispose(bool disposing)
		{
			if (!this._disposedValue)
			{
				if (disposing && this._wndProcHook != null)
				{
					this._wndProcHook.Dispose();
					this._wndProcHook = null;
				}
				this._disposedValue = true;
				base.Dispose(disposing);
			}
		}

		// Token: 0x04000328 RID: 808
		private WindowsMessageHook _wndProcHook;

		// Token: 0x04000329 RID: 809
		private bool _disposedValue;
	}
}
