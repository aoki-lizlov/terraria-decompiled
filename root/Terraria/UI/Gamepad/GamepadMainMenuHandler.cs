using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameInput;

namespace Terraria.UI.Gamepad
{
	// Token: 0x02000105 RID: 261
	public class GamepadMainMenuHandler
	{
		// Token: 0x06001A40 RID: 6720 RVA: 0x004F56CC File Offset: 0x004F38CC
		public static void Update()
		{
			if (!GamepadMainMenuHandler.CanRun)
			{
				UILinkPage uilinkPage = UILinkPointNavigator.Pages[1000];
				uilinkPage.CurrentPoint = uilinkPage.DefaultPoint;
				Vector2 vector = new Vector2((float)Math.Cos((double)(Main.GlobalTimeWrappedHourly * 6.2831855f)), (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f * 2f))) * new Vector2(30f, 15f) + Vector2.UnitY * 20f;
				UILinkPointNavigator.SetPosition(2000, new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f + vector);
				return;
			}
			if (!Main.gameMenu)
			{
				return;
			}
			if (Main.MenuUI.IsVisible)
			{
				return;
			}
			if (GamepadMainMenuHandler.LastDrew != Main.menuMode)
			{
				return;
			}
			int lastMainMenu = GamepadMainMenuHandler.LastMainMenu;
			GamepadMainMenuHandler.LastMainMenu = Main.menuMode;
			switch (Main.menuMode)
			{
			case 17:
			case 18:
			case 19:
			case 21:
			case 22:
			case 23:
			case 24:
			case 26:
				if (GamepadMainMenuHandler.MenuItemPositions.Count >= 4)
				{
					Vector2 vector2 = GamepadMainMenuHandler.MenuItemPositions[3];
					GamepadMainMenuHandler.MenuItemPositions.RemoveAt(3);
					if (Main.menuMode == 17)
					{
						GamepadMainMenuHandler.MenuItemPositions.Insert(0, vector2);
					}
				}
				break;
			case 28:
				if (GamepadMainMenuHandler.MenuItemPositions.Count >= 3)
				{
					GamepadMainMenuHandler.MenuItemPositions.RemoveAt(1);
				}
				break;
			}
			UILinkPage uilinkPage2 = UILinkPointNavigator.Pages[1000];
			if (lastMainMenu != Main.menuMode)
			{
				uilinkPage2.CurrentPoint = uilinkPage2.DefaultPoint;
			}
			for (int i = 0; i < GamepadMainMenuHandler.MenuItemPositions.Count; i++)
			{
				Vector2 vector3 = GamepadMainMenuHandler.MenuItemPositions[i] * Main.UIScale;
				if (i == 0 && lastMainMenu != GamepadMainMenuHandler.LastMainMenu && PlayerInput.UsingGamepad && Main.InvisibleCursorForGamepad)
				{
					Main.mouseX = (PlayerInput.MouseX = (int)vector3.X);
					Main.mouseY = (PlayerInput.MouseY = (int)vector3.Y);
					Main.menuFocus = -1;
				}
				UILinkPoint uilinkPoint = uilinkPage2.LinkMap[2000 + i];
				uilinkPoint.Position = vector3;
				if (i == 0)
				{
					uilinkPoint.Up = -1;
				}
				else
				{
					uilinkPoint.Up = 2000 + i - 1;
				}
				uilinkPoint.Left = -3;
				uilinkPoint.Right = -4;
				if (i == GamepadMainMenuHandler.MenuItemPositions.Count - 1)
				{
					uilinkPoint.Down = -2;
				}
				else
				{
					uilinkPoint.Down = 2000 + i + 1;
				}
				if (GamepadMainMenuHandler.MoveCursorOnNextRun)
				{
					GamepadMainMenuHandler.MoveCursorOnNextRun = false;
					UILinkPointNavigator.ChangePoint(uilinkPoint.ID);
				}
			}
			GamepadMainMenuHandler.MenuItemPositions.Clear();
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x0000357B File Offset: 0x0000177B
		public GamepadMainMenuHandler()
		{
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x004F5988 File Offset: 0x004F3B88
		// Note: this type is marked as 'beforefieldinit'.
		static GamepadMainMenuHandler()
		{
		}

		// Token: 0x040014C9 RID: 5321
		public static int LastMainMenu = -1;

		// Token: 0x040014CA RID: 5322
		public static List<Vector2> MenuItemPositions = new List<Vector2>(20);

		// Token: 0x040014CB RID: 5323
		public static int LastDrew = -1;

		// Token: 0x040014CC RID: 5324
		public static bool CanRun = false;

		// Token: 0x040014CD RID: 5325
		public static bool MoveCursorOnNextRun = false;
	}
}
