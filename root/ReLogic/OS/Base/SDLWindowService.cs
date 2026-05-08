using System;
using Microsoft.Xna.Framework;
using SDL3;

namespace ReLogic.OS.Base
{
	// Token: 0x02000079 RID: 121
	public abstract class SDLWindowService : IWindowService
	{
		// Token: 0x06000298 RID: 664 RVA: 0x0000A980 File Offset: 0x00008B80
		public float GetScaling()
		{
			return 1f;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000046AD File Offset: 0x000028AD
		public void SetQuickEditEnabled(bool enabled)
		{
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000A987 File Offset: 0x00008B87
		public void SetUnicodeTitle(GameWindow window, string title)
		{
			window.Title = title;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000046AD File Offset: 0x000028AD
		public void StartFlashingIcon(GameWindow window)
		{
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000046AD File Offset: 0x000028AD
		public void StopFlashingIcon(GameWindow window)
		{
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000A990 File Offset: 0x00008B90
		public void Activate(GameWindow window)
		{
			SDL.SDL_RaiseWindow(window.Handle);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000A9A0 File Offset: 0x00008BA0
		public bool IsSizeable(GameWindow window)
		{
			SDL.SDL_WindowFlags sdl_WindowFlags = SDL.SDL_GetWindowFlags(window.Handle);
			return (sdl_WindowFlags & 32L) != null && (sdl_WindowFlags & 16L) == 0L;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000A9CB File Offset: 0x00008BCB
		public void SetPosition(GameWindow window, int x, int y)
		{
			SDL.SDL_SetWindowPosition(window.Handle, x, y);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000A9DC File Offset: 0x00008BDC
		public Rectangle GetBounds(GameWindow window)
		{
			int num;
			int num2;
			int num3;
			int num4;
			if (SDL.SDL_GetWindowPosition(window.Handle, ref num, ref num2) && SDL.SDL_GetWindowSize(window.Handle, ref num3, ref num4))
			{
				return new Rectangle(num, num2, num3, num4);
			}
			return default(Rectangle);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000448A File Offset: 0x0000268A
		protected SDLWindowService()
		{
		}
	}
}
