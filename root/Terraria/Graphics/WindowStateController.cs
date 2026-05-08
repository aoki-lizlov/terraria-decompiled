using System;
using System.Drawing;
using System.Windows.Forms;
using ReLogic.OS;

namespace Terraria.Graphics
{
	// Token: 0x020001D7 RID: 471
	public class WindowStateController
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x004F8A66 File Offset: 0x004F6C66
		public bool CanMoveWindowAcrossScreens
		{
			get
			{
				return Platform.IsWindows;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001FBD RID: 8125 RVA: 0x0051D797 File Offset: 0x0051B997
		public string ScreenDeviceName
		{
			get
			{
				if (!Platform.IsWindows)
				{
					return "";
				}
				return Main.instance.Window.ScreenDeviceName;
			}
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x0051D7B8 File Offset: 0x0051B9B8
		public void TryMovingToScreen(string screenDeviceName)
		{
			if (!this.CanMoveWindowAcrossScreens)
			{
				return;
			}
			Rectangle rectangle;
			if (!this.TryGetBounds(screenDeviceName, out rectangle))
			{
				return;
			}
			if (!this.IsVisibleOnAnyScreen(rectangle))
			{
				return;
			}
			Form form = Control.FromHandle(Main.instance.Window.Handle);
			if (!this.WouldViewFitInScreen(form.Bounds, rectangle))
			{
				return;
			}
			form.Location = new Point(rectangle.Width / 2 - form.Width / 2 + rectangle.X, rectangle.Height / 2 - form.Height / 2 + rectangle.Y);
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x0051D848 File Offset: 0x0051BA48
		private bool TryGetBounds(string screenDeviceName, out Rectangle bounds)
		{
			bounds = default(Rectangle);
			foreach (Screen screen in Screen.AllScreens)
			{
				if (screen.DeviceName == screenDeviceName)
				{
					bounds = screen.Bounds;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0051D891 File Offset: 0x0051BA91
		private bool WouldViewFitInScreen(Rectangle view, Rectangle screen)
		{
			return view.Width <= screen.Width && view.Height <= screen.Height;
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x0051D8B8 File Offset: 0x0051BAB8
		private bool IsVisibleOnAnyScreen(Rectangle rect)
		{
			Screen[] allScreens = Screen.AllScreens;
			for (int i = 0; i < allScreens.Length; i++)
			{
				if (allScreens[i].WorkingArea.IntersectsWith(rect))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0000357B File Offset: 0x0000177B
		public WindowStateController()
		{
		}
	}
}
