using System;
using Steamworks;
using Terraria.GameInput;
using Terraria.Social.Base;
using Terraria.UI.Gamepad;

namespace Terraria.Social.Steam
{
	// Token: 0x0200013E RID: 318
	public class PlatformSocialModule : PlatformSocialModule
	{
		// Token: 0x06001C8B RID: 7307 RVA: 0x004FEDA8 File Offset: 0x004FCFA8
		public override void Initialize()
		{
			if (Main.dedServ)
			{
				return;
			}
			bool flag = (PlayerInput.UseSteamDeckIfPossible = SteamUtils.IsSteamRunningOnSteamDeck());
			if (flag)
			{
				PlayerInput.SettingsForUI.SetCursorMode(CursorMode.Gamepad);
				PlayerInput.CurrentInputMode = InputMode.XBoxGamepadUI;
				GamepadMainMenuHandler.MoveCursorOnNextRun = true;
				PlayerInput.PreventFirstMousePositionGrab = true;
			}
			if (flag)
			{
				Main.graphics.PreferredBackBufferWidth = (Main.screenWidth = 1280);
				Main.graphics.PreferredBackBufferHeight = (Main.screenHeight = 800);
				Main.startFullscreen = true;
				Main.toggleFullscreen = true;
				Main.screenBorderless = false;
				Main.screenMaximized = false;
				Main.InitialMapScale = (Main.MapScale = 0.73f);
				Main.UIScale = 1.07f;
			}
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Shutdown()
		{
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x004FEE41 File Offset: 0x004FD041
		public PlatformSocialModule()
		{
		}
	}
}
