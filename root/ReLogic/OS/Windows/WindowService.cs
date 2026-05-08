using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace ReLogic.OS.Windows
{
	// Token: 0x0200006A RID: 106
	internal class WindowService : IWindowService
	{
		// Token: 0x06000246 RID: 582 RVA: 0x00009DC0 File Offset: 0x00007FC0
		public float GetScaling()
		{
			float num;
			try
			{
				IntPtr hdc = Graphics.FromHwnd(IntPtr.Zero).GetHdc();
				int deviceCaps = NativeMethods.GetDeviceCaps(hdc, NativeMethods.DeviceCap.VertRes);
				num = (float)NativeMethods.GetDeviceCaps(hdc, NativeMethods.DeviceCap.DesktopVertRes) / (float)deviceCaps;
			}
			catch (Exception)
			{
				num = 1f;
			}
			return num;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009E10 File Offset: 0x00008010
		public void SetQuickEditEnabled(bool enabled)
		{
			IntPtr stdHandle = NativeMethods.GetStdHandle(NativeMethods.StdHandleType.Input);
			NativeMethods.ConsoleMode consoleMode;
			if (!NativeMethods.GetConsoleMode(stdHandle, out consoleMode))
			{
				return;
			}
			if (enabled)
			{
				consoleMode |= NativeMethods.ConsoleMode.QuickEditMode;
			}
			else
			{
				consoleMode &= ~NativeMethods.ConsoleMode.QuickEditMode;
			}
			NativeMethods.SetConsoleMode(stdHandle, consoleMode);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00009E48 File Offset: 0x00008048
		public void SetUnicodeTitle(GameWindow window, string title)
		{
			NativeMethods.WndProcCallback wndProcCallback = new NativeMethods.WndProcCallback(NativeMethods.DefWindowProc);
			int num = NativeMethods.SetWindowLong(window.Handle, -4, (int)Marshal.GetFunctionPointerForDelegate(wndProcCallback));
			window.Title = title;
			NativeMethods.SetWindowLong(window.Handle, -4, num);
			GC.KeepAlive(wndProcCallback);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009E98 File Offset: 0x00008098
		public void StartFlashingIcon(GameWindow window)
		{
			NativeMethods.FlashInfo flashInfo = NativeMethods.FlashInfo.CreateStart(window.Handle);
			NativeMethods.FlashWindowEx(ref flashInfo);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009EBC File Offset: 0x000080BC
		public void StopFlashingIcon(GameWindow window)
		{
			NativeMethods.FlashInfo flashInfo = NativeMethods.FlashInfo.CreateStop(window.Handle);
			NativeMethods.FlashWindowEx(ref flashInfo);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00009EDD File Offset: 0x000080DD
		public void Activate(GameWindow window)
		{
			((Form)Control.FromHandle(window.Handle)).Activate();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00009EF4 File Offset: 0x000080F4
		public bool IsSizeable(GameWindow window)
		{
			Form form = (Form)Control.FromHandle(window.Handle);
			return form.WindowState == null && form.FormBorderStyle == 4;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009F25 File Offset: 0x00008125
		public void SetPosition(GameWindow window, int x, int y)
		{
			((Form)Control.FromHandle(window.Handle)).Location = new Point(x, y);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009F44 File Offset: 0x00008144
		public Rectangle GetBounds(GameWindow window)
		{
			Form form = (Form)Control.FromHandle(window.Handle);
			return new Rectangle(form.Bounds.X, form.Bounds.Y, form.Bounds.Width, form.Bounds.Height);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000448A File Offset: 0x0000268A
		public WindowService()
		{
		}
	}
}
