using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.Initializers
{
	// Token: 0x02000089 RID: 137
	public class UILinksInitializer
	{
		// Token: 0x060015AD RID: 5549 RVA: 0x004CCE59 File Offset: 0x004CB059
		public static bool NothingMoreImportantThanNPCChat()
		{
			return !Main.hairWindow && Main.npcShop == 0 && Main.player[Main.myPlayer].chest == -1;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x004CCE80 File Offset: 0x004CB080
		public static float HandleSliderHorizontalInput(float currentValue, float min, float max, float deadZone = 0.2f, float sensitivity = 0.5f)
		{
			float num = PlayerInput.GamepadThumbstickLeft.X;
			if (num < -deadZone || num > deadZone)
			{
				num = MathHelper.Lerp(0f, sensitivity / 60f, (Math.Abs(num) - deadZone) / (1f - deadZone)) * (float)Math.Sign(num);
			}
			else
			{
				num = 0f;
			}
			return MathHelper.Clamp((currentValue - min) / (max - min) + num, 0f, 1f) * (max - min) + min;
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x004CCEF4 File Offset: 0x004CB0F4
		public static float HandleSliderVerticalInput(float currentValue, float min, float max, float deadZone = 0.2f, float sensitivity = 0.5f)
		{
			float num = -PlayerInput.GamepadThumbstickLeft.Y;
			if (num < -deadZone || num > deadZone)
			{
				num = MathHelper.Lerp(0f, sensitivity / 60f, (Math.Abs(num) - deadZone) / (1f - deadZone)) * (float)Math.Sign(num);
			}
			else
			{
				num = 0f;
			}
			return MathHelper.Clamp((currentValue - min) / (max - min) + num, 0f, 1f) * (max - min) + min;
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x004CCF67 File Offset: 0x004CB167
		public static bool CanExecuteInputCommand()
		{
			return PlayerInput.AllowExecutionOfGamepadInstructions;
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x004CCF6E File Offset: 0x004CB16E
		// (set) Token: 0x060015B2 RID: 5554 RVA: 0x004CCF75 File Offset: 0x004CB175
		public static int MainfocusRecipe
		{
			get
			{
				return Main.focusRecipe;
			}
			set
			{
				Main.focusRecipe = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x004CCF6E File Offset: 0x004CB16E
		// (set) Token: 0x060015B4 RID: 5556 RVA: 0x004CCF75 File Offset: 0x004CB175
		public static int MainFocusBanner
		{
			get
			{
				return Main.focusRecipe;
			}
			set
			{
				Main.focusRecipe = value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x004CCF7D File Offset: 0x004CB17D
		// (set) Token: 0x060015B6 RID: 5558 RVA: 0x004CCF84 File Offset: 0x004CB184
		public static int MainnumAvailableRecipes
		{
			get
			{
				return Main.numAvailableRecipes;
			}
			set
			{
				Main.numAvailableRecipes = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x004CCF7D File Offset: 0x004CB17D
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x004CCF84 File Offset: 0x004CB184
		public static int MainnumAvailableRecipes2
		{
			get
			{
				return Main.numAvailableRecipes;
			}
			set
			{
				Main.numAvailableRecipes = value;
			}
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x004CCF8C File Offset: 0x004CB18C
		public static void Load()
		{
			Func<string> func = () => PlayerInput.BuildCommand(Lang.misc[53].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] });
			UILinkPage uilinkPage = new UILinkPage();
			uilinkPage.UpdateEvent += delegate
			{
				PlayerInput.GamepadAllowScrolling = true;
			};
			for (int i = 0; i < 20; i++)
			{
				uilinkPage.LinkMap.Add(2000 + i, new UILinkPoint(2000 + i, true, -3, -4, -1, -2));
			}
			uilinkPage.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[53].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] }) + PlayerInput.BuildCommand(Lang.misc[82].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] });
			uilinkPage.UpdateEvent += delegate
			{
				bool flag = PlayerInput.Triggers.JustPressed.Inventory;
				if (Main.inputTextEscape)
				{
					Main.inputTextEscape = false;
					flag = true;
				}
				if (UILinksInitializer.CanExecuteInputCommand() && flag)
				{
					UILinksInitializer.FancyExit();
				}
				UILinkPointNavigator.Shortcuts.BackButtonInUse = flag;
				UILinksInitializer.HandleOptionsSpecials();
			};
			uilinkPage.IsValidEvent += () => Main.gameMenu && !Main.MenuUI.IsVisible;
			uilinkPage.CanEnterEvent += () => Main.gameMenu && !Main.MenuUI.IsVisible;
			UILinkPointNavigator.RegisterPage(uilinkPage, 1000, true);
			UILinkPage cp2 = new UILinkPage();
			cp2.LinkMap.Add(2500, new UILinkPoint(2500, true, -3, -4, -1, -2));
			cp2.LinkMap.Add(2501, new UILinkPoint(2501, true, -3, -4, -1, -2));
			cp2.LinkMap.Add(2502, new UILinkPoint(2502, true, -3, -4, -1, -2));
			cp2.LinkMap.Add(2503, new UILinkPoint(2503, true, -3, -4, -1, -2));
			cp2.LinkMap.Add(2504, new UILinkPoint(2504, true, -3, -4, -1, -2));
			cp2.LinkMap.Add(2505, new UILinkPoint(2505, true, -3, -4, -1, -2));
			cp2.LinkMap.Add(2506, new UILinkPoint(2506, true, -3, -4, -1, -2));
			cp2.LinkMap.Add(2507, new UILinkPoint(2507, true, -3, -4, -1, -2));
			cp2.LinkMap.Add(2508, new UILinkPoint(2508, true, -3, -4, -1, -2));
			cp2.LinkMap.Add(2509, new UILinkPoint(2509, true, -3, -4, -1, -2));
			cp2.LinkMap.Add(2510, new UILinkPoint(2510, true, -3, -4, -1, -2));
			cp2.LinkMap.Add(2511, new UILinkPoint(2511, true, -3, -4, -1, -2));
			cp2.UpdateEvent += delegate
			{
				if (UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsNew)
				{
					for (int num33 = 0; num33 < UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsCount; num33++)
					{
						if (num33 - 4 >= 0)
						{
							cp2.LinkMap[2500 + num33].Up = 2500 + num33 - 4;
						}
						else
						{
							cp2.LinkMap[2500 + num33].Up = -1;
						}
						if (num33 + 4 < UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsCount)
						{
							cp2.LinkMap[2500 + num33].Down = 2500 + num33 + 4;
						}
						else
						{
							cp2.LinkMap[2500 + num33].Down = -2;
						}
						cp2.LinkMap[2500 + num33].Left = ((num33 > 0) ? (2500 + num33 - 1) : (-3));
						cp2.LinkMap[2500 + num33].Right = ((num33 < UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsCount - 1) ? (2500 + num33 + 1) : (-4));
					}
					return;
				}
				cp2.LinkMap[2501].Right = (UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight ? 2502 : (-4));
				if (cp2.LinkMap[2501].Right == -4 && UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight2)
				{
					cp2.LinkMap[2501].Right = 2503;
				}
				cp2.LinkMap[2502].Right = (UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight2 ? 2503 : (-4));
				cp2.LinkMap[2503].Left = (UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight ? 2502 : 2501);
			};
			cp2.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[53].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] }) + PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] });
			cp2.IsValidEvent += () => (Main.player[Main.myPlayer].talkNPC != -1 || Main.player[Main.myPlayer].sign != -1) && UILinksInitializer.NothingMoreImportantThanNPCChat();
			cp2.CanEnterEvent += () => (Main.player[Main.myPlayer].talkNPC != -1 || Main.player[Main.myPlayer].sign != -1) && UILinksInitializer.NothingMoreImportantThanNPCChat();
			cp2.EnterEvent += delegate
			{
				Main.player[Main.myPlayer].releaseInventory = false;
			};
			cp2.LeaveEvent += delegate
			{
				Main.npcChatRelease = false;
				Main.player[Main.myPlayer].LockGamepadTileInteractions();
			};
			UILinkPointNavigator.RegisterPage(cp2, 1003, true);
			UILinkPage cp3 = new UILinkPage();
			cp3.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			Func<string> func2 = delegate
			{
				int currentPoint = UILinkPointNavigator.CurrentPoint;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].inventory, 0, currentPoint);
			};
			Func<string> func3 = () => ItemSlot.GetGamepadInstructions(ref Main.player[Main.myPlayer].trashItem, 6);
			for (int j = 0; j <= 49; j++)
			{
				UILinkPoint uilinkPoint = new UILinkPoint(j, true, j - 1, j + 1, j - 10, j + 10);
				uilinkPoint.OnSpecialInteracts += func2;
				int num = j;
				if (num < 10)
				{
					uilinkPoint.Up = -1;
				}
				if (num >= 40)
				{
					uilinkPoint.Down = -2;
				}
				if (num % 10 == 9)
				{
					uilinkPoint.Right = -4;
				}
				if (num % 10 == 0)
				{
					uilinkPoint.Left = -3;
				}
				cp3.LinkMap.Add(j, uilinkPoint);
			}
			cp3.LinkMap[9].Right = 0;
			cp3.LinkMap[19].Right = 50;
			cp3.LinkMap[29].Right = 51;
			cp3.LinkMap[39].Right = 52;
			cp3.LinkMap[49].Right = 53;
			cp3.LinkMap[0].Left = 9;
			cp3.LinkMap[10].Left = 54;
			cp3.LinkMap[20].Left = 55;
			cp3.LinkMap[30].Left = 56;
			cp3.LinkMap[40].Left = 57;
			cp3.LinkMap.Add(300, new UILinkPoint(300, true, 309, 310, 49, -2));
			cp3.LinkMap.Add(309, new UILinkPoint(309, true, 310, 300, 302, 54));
			cp3.LinkMap.Add(310, new UILinkPoint(310, true, 300, 309, 301, 50));
			cp3.LinkMap.Add(301, new UILinkPoint(301, true, 300, 302, 53, 310));
			cp3.LinkMap.Add(302, new UILinkPoint(302, true, 301, 300, 57, 309));
			cp3.LinkMap.Add(311, new UILinkPoint(311, true, -3, -4, 40, -2));
			cp3.LinkMap[301].OnSpecialInteracts += func;
			cp3.LinkMap[302].OnSpecialInteracts += func;
			cp3.LinkMap[309].OnSpecialInteracts += func;
			cp3.LinkMap[310].OnSpecialInteracts += func;
			cp3.LinkMap[300].OnSpecialInteracts += func3;
			cp3.UpdateEvent += delegate
			{
				bool inReforgeMenu = Main.InReforgeMenu;
				bool flag2 = Main.LocalPlayer.chest != -1;
				bool flag3 = Main.npcShop != 0;
				TileEntity tileEntity = Main.LocalPlayer.tileEntityAnchor.GetTileEntity();
				bool flag4 = tileEntity is TEHatRack;
				bool flag5 = tileEntity is TEDisplayDoll;
				if (NewCraftingUI.Visible)
				{
					flag2 = false;
				}
				for (int num34 = 40; num34 <= 49; num34++)
				{
					if (inReforgeMenu)
					{
						cp3.LinkMap[num34].Down = ((num34 < 45) ? 303 : 304);
					}
					else if (flag2)
					{
						cp3.LinkMap[num34].Down = 400 + num34 - 40;
					}
					else if (flag3)
					{
						cp3.LinkMap[num34].Down = 2700 + num34 - 40;
					}
					else if (num34 == 40 && Main.IsJourneyMode && !Main.CreativeMenu.Blocked)
					{
						cp3.LinkMap[num34].Down = 311;
					}
					else if (!NewCraftingUI.Visible)
					{
						cp3.LinkMap[num34].Down = -2;
					}
				}
				if (flag5)
				{
					for (int num35 = 41; num35 <= 48; num35++)
					{
						cp3.LinkMap[num35].Down = 5100 + (int)Math.Round((double)((num35 - 40) * 10) / 9.0) - 1;
					}
					cp3.LinkMap[40].Down = 5118;
				}
				if (flag4)
				{
					for (int num36 = 44; num36 <= 45; num36++)
					{
						cp3.LinkMap[num36].Down = 5000 + num36 - 44;
					}
				}
				if (NewCraftingUI.Visible && Main.LocalPlayer.chest != -1)
				{
					cp3.LinkMap[49].Down = 300;
					cp3.LinkMap[300].Up = 49;
					cp3.LinkMap[300].Right = 310;
					cp3.LinkMap[310].Up = 53;
					cp3.LinkMap[309].Up = 57;
				}
				else if (flag2)
				{
					cp3.LinkMap[300].Up = 439;
					cp3.LinkMap[300].Right = 310;
					cp3.LinkMap[300].Left = 309;
					cp3.LinkMap[310].Up = ((Main.LocalPlayer.chest < -1) ? 505 : 504);
					cp3.LinkMap[309].Up = ((Main.LocalPlayer.chest < -1) ? 505 : 504);
				}
				else if (flag3)
				{
					cp3.LinkMap[300].Up = 2739;
					cp3.LinkMap[300].Right = 310;
					cp3.LinkMap[300].Left = 309;
					cp3.LinkMap[310].Up = 53;
					cp3.LinkMap[309].Up = 57;
				}
				else
				{
					cp3.LinkMap[49].Down = 300;
					cp3.LinkMap[300].Up = 49;
					cp3.LinkMap[300].Right = 301;
					if (!NewCraftingUI.Visible)
					{
						cp3.LinkMap[300].Left = 302;
					}
					cp3.LinkMap[309].Up = 302;
					cp3.LinkMap[310].Up = 301;
				}
				if (!NewCraftingUI.Visible)
				{
					cp3.LinkMap[311].Right = -1;
					cp3.LinkMap[311].Down = -1;
					cp3.LinkMap[300].Down = -1;
				}
				cp3.LinkMap[0].Left = 9;
				cp3.LinkMap[10].Left = 54;
				cp3.LinkMap[20].Left = 55;
				cp3.LinkMap[30].Left = 56;
				cp3.LinkMap[40].Left = 57;
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 0)
				{
					cp3.LinkMap[0].Left = 6000;
				}
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 2)
				{
					cp3.LinkMap[10].Left = 6002;
				}
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 4)
				{
					cp3.LinkMap[20].Left = 6004;
				}
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 6)
				{
					cp3.LinkMap[30].Left = 6006;
				}
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 8)
				{
					cp3.LinkMap[40].Left = 6008;
				}
				cp3.PageOnLeft = 9;
				if (Main.InPipBanner)
				{
					cp3.PageOnLeft = 22;
				}
				if (Main.CreativeMenu.Enabled)
				{
					cp3.PageOnLeft = 1005;
				}
				if (NewCraftingUI.Visible)
				{
					cp3.PageOnLeft = 24;
				}
				if (Main.InReforgeMenu)
				{
					cp3.PageOnLeft = 5;
				}
				if (flag5)
				{
					cp3.PageOnLeft = 20;
				}
				if (flag4)
				{
					cp3.PageOnLeft = 21;
				}
			};
			cp3.IsValidEvent += () => Main.playerInventory;
			cp3.PageOnLeft = 9;
			cp3.PageOnRight = 2;
			UILinkPointNavigator.RegisterPage(cp3, 0, true);
			UILinkPage cp4 = new UILinkPage();
			cp4.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			Func<string> func4 = delegate
			{
				int currentPoint2 = UILinkPointNavigator.CurrentPoint;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].inventory, 1, currentPoint2);
			};
			for (int k = 50; k <= 53; k++)
			{
				UILinkPoint uilinkPoint2 = new UILinkPoint(k, true, -3, -4, k - 1, k + 1);
				uilinkPoint2.OnSpecialInteracts += func4;
				cp4.LinkMap.Add(k, uilinkPoint2);
			}
			cp4.LinkMap[50].Left = 19;
			cp4.LinkMap[51].Left = 29;
			cp4.LinkMap[52].Left = 39;
			cp4.LinkMap[53].Left = 49;
			cp4.LinkMap[50].Right = 54;
			cp4.LinkMap[51].Right = 55;
			cp4.LinkMap[52].Right = 56;
			cp4.LinkMap[53].Right = 57;
			cp4.LinkMap[50].Up = 310;
			cp4.UpdateEvent += delegate
			{
				if (Main.npcShop != 0)
				{
					cp4.LinkMap[53].Down = 310;
					return;
				}
				if (Main.player[Main.myPlayer].chest != -1)
				{
					cp4.LinkMap[53].Down = (NewCraftingUI.Visible ? 310 : 500);
					return;
				}
				cp4.LinkMap[53].Down = 301;
			};
			cp4.IsValidEvent += () => Main.playerInventory;
			cp4.PageOnLeft = 0;
			cp4.PageOnRight = 2;
			UILinkPointNavigator.RegisterPage(cp4, 1, true);
			UILinkPage cp5 = new UILinkPage();
			cp5.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			Func<string> func5 = delegate
			{
				int currentPoint3 = UILinkPointNavigator.CurrentPoint;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].inventory, 2, currentPoint3);
			};
			for (int l = 54; l <= 57; l++)
			{
				UILinkPoint uilinkPoint3 = new UILinkPoint(l, true, -3, -4, l - 1, l + 1);
				uilinkPoint3.OnSpecialInteracts += func5;
				cp5.LinkMap.Add(l, uilinkPoint3);
			}
			cp5.LinkMap[54].Left = 50;
			cp5.LinkMap[55].Left = 51;
			cp5.LinkMap[56].Left = 52;
			cp5.LinkMap[57].Left = 53;
			cp5.LinkMap[54].Right = 10;
			cp5.LinkMap[55].Right = 20;
			cp5.LinkMap[56].Right = 30;
			cp5.LinkMap[57].Right = 40;
			cp5.LinkMap[54].Up = 309;
			cp5.UpdateEvent += delegate
			{
				if (Main.npcShop != 0)
				{
					cp5.LinkMap[57].Down = 309;
					return;
				}
				if (Main.player[Main.myPlayer].chest != -1)
				{
					cp5.LinkMap[57].Down = (NewCraftingUI.Visible ? 310 : 500);
					return;
				}
				cp5.LinkMap[57].Down = 302;
			};
			cp5.PageOnLeft = 0;
			cp5.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp5, 2, true);
			UILinkPage cp6 = new UILinkPage();
			cp6.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			Func<string> func6 = delegate
			{
				int num37 = UILinkPointNavigator.CurrentPoint - 100;
				if (num37 % 10 == 8 && !Main.LocalPlayer.CanDemonHeartAccessoryBeShown())
				{
					num37++;
				}
				bool flag6 = num37 >= 10;
				int num38 = ((num37 % 10 < 3) ? (flag6 ? 9 : 8) : (flag6 ? 11 : 10));
				return ItemSlot.GetGamepadInstructions(Main.LocalPlayer.armor, num38, num37);
			};
			Func<string> func7 = delegate
			{
				int num39 = UILinkPointNavigator.CurrentPoint - 120;
				if (num39 % 10 == 8 && !Main.LocalPlayer.CanDemonHeartAccessoryBeShown())
				{
					num39++;
				}
				return ItemSlot.GetGamepadInstructions(Main.LocalPlayer.dye, 12, num39);
			};
			for (int m = 100; m <= 119; m++)
			{
				UILinkPoint uilinkPoint4 = new UILinkPoint(m, true, m + 10, m - 10, m - 1, m + 1);
				uilinkPoint4.OnSpecialInteracts += func6;
				int num2 = m - 100;
				if (num2 == 0)
				{
					uilinkPoint4.Up = 305;
				}
				if (num2 == 10)
				{
					uilinkPoint4.Up = 306;
				}
				if (num2 == 9 || num2 == 19)
				{
					uilinkPoint4.Down = -2;
				}
				if (num2 >= 10)
				{
					uilinkPoint4.Left = 120 + num2 % 10;
				}
				else if (num2 >= 3)
				{
					uilinkPoint4.Right = -4;
				}
				else
				{
					uilinkPoint4.Right = 312 + num2;
				}
				cp6.LinkMap.Add(m, uilinkPoint4);
			}
			for (int n = 120; n <= 129; n++)
			{
				UILinkPoint uilinkPoint4 = new UILinkPoint(n, true, -3, -10 + n, n - 1, n + 1);
				uilinkPoint4.OnSpecialInteracts += func7;
				int num3 = n - 120;
				if (num3 == 0)
				{
					uilinkPoint4.Up = 307;
				}
				if (num3 == 9)
				{
					uilinkPoint4.Down = 308;
					uilinkPoint4.Left = 1557;
				}
				if (num3 == 8)
				{
					uilinkPoint4.Left = 1570;
				}
				cp6.LinkMap.Add(n, uilinkPoint4);
			}
			for (int num4 = 312; num4 <= 314; num4++)
			{
				int num5 = num4 - 312;
				UILinkPoint uilinkPoint4 = new UILinkPoint(num4, true, 100 + num5, -4, num4 - 1, num4 + 1);
				if (num5 == 0)
				{
					uilinkPoint4.Up = -1;
				}
				if (num5 == 2)
				{
					uilinkPoint4.Down = -2;
				}
				uilinkPoint4.OnSpecialInteracts += func;
				cp6.LinkMap.Add(num4, uilinkPoint4);
			}
			cp6.IsValidEvent += () => Main.playerInventory && Main.EquipPage == 0;
			cp6.UpdateEvent += delegate
			{
				int num40 = 107;
				int amountOfExtraAccessorySlotsToShow = Main.player[Main.myPlayer].GetAmountOfExtraAccessorySlotsToShow();
				for (int num41 = 0; num41 < amountOfExtraAccessorySlotsToShow; num41++)
				{
					cp6.LinkMap[num40 + num41].Down = num40 + num41 + 1;
					cp6.LinkMap[num40 - 100 + 120 + num41].Down = num40 - 100 + 120 + num41 + 1;
					cp6.LinkMap[num40 + 10 + num41].Down = num40 + 10 + num41 + 1;
				}
				cp6.LinkMap[num40 + amountOfExtraAccessorySlotsToShow].Down = 308;
				cp6.LinkMap[num40 + 10 + amountOfExtraAccessorySlotsToShow].Down = 308;
				cp6.LinkMap[num40 - 100 + 120 + amountOfExtraAccessorySlotsToShow].Down = 308;
				for (int num42 = 120; num42 <= 129; num42++)
				{
					UILinkPoint uilinkPoint20 = cp6.LinkMap[num42];
					int num43 = num42 - 120;
					uilinkPoint20.Left = -3;
					if (num43 == 0)
					{
						uilinkPoint20.Left = (Main.ShouldPVPDraw ? 1550 : (-3));
					}
					if (num43 == 1)
					{
						uilinkPoint20.Left = (Main.ShouldTeamSelectDraw ? 1552 : (-3));
					}
					if (num43 == 2)
					{
						uilinkPoint20.Left = (Main.ShouldTeamSelectDraw ? 1556 : (-3));
					}
					if (num43 == 3)
					{
						uilinkPoint20.Left = ((UILinkPointNavigator.Shortcuts.INFOACCCOUNT >= 1) ? 1558 : (-3));
					}
					if (num43 == 4)
					{
						uilinkPoint20.Left = ((UILinkPointNavigator.Shortcuts.INFOACCCOUNT >= 5) ? 1562 : (-3));
					}
					if (num43 == 5)
					{
						uilinkPoint20.Left = ((UILinkPointNavigator.Shortcuts.INFOACCCOUNT >= 9) ? 1566 : (-3));
					}
				}
				cp6.LinkMap[num40 - 100 + 120 + amountOfExtraAccessorySlotsToShow].Left = 1557;
				cp6.LinkMap[num40 - 100 + 120 + amountOfExtraAccessorySlotsToShow - 1].Left = 1570;
			};
			cp6.PageOnLeft = 8;
			cp6.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp6, 3, true);
			UILinkPage cp7 = new UILinkPage();
			cp7.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			Func<string> func8 = delegate
			{
				int num44 = UILinkPointNavigator.CurrentPoint - 400;
				int num45 = 4;
				Item[] array = Main.player[Main.myPlayer].bank.item;
				switch (Main.player[Main.myPlayer].chest)
				{
				case -5:
					array = Main.player[Main.myPlayer].bank4.item;
					num45 = 32;
					break;
				case -4:
					array = Main.player[Main.myPlayer].bank3.item;
					break;
				case -3:
					array = Main.player[Main.myPlayer].bank2.item;
					break;
				case -2:
					break;
				case -1:
					return "";
				default:
					array = Main.chest[Main.player[Main.myPlayer].chest].item;
					num45 = 3;
					break;
				}
				return ItemSlot.GetGamepadInstructions(array, num45, num44);
			};
			for (int num6 = 400; num6 <= 439; num6++)
			{
				UILinkPoint uilinkPoint5 = new UILinkPoint(num6, true, num6 - 1, num6 + 1, num6 - 10, num6 + 10);
				uilinkPoint5.OnSpecialInteracts += func8;
				int num7 = num6 - 400;
				if (num7 < 10)
				{
					uilinkPoint5.Up = 40 + num7;
				}
				if (num7 >= 30)
				{
					uilinkPoint5.Down = -2;
				}
				if (num7 % 10 == 9)
				{
					uilinkPoint5.Right = -4;
				}
				if (num7 % 10 == 0)
				{
					uilinkPoint5.Left = -3;
				}
				cp7.LinkMap.Add(num6, uilinkPoint5);
			}
			cp7.LinkMap.Add(500, new UILinkPoint(500, true, 409, -4, 53, 501));
			cp7.LinkMap.Add(501, new UILinkPoint(501, true, 419, -4, 500, 502));
			cp7.LinkMap.Add(502, new UILinkPoint(502, true, 429, -4, 501, 503));
			cp7.LinkMap.Add(503, new UILinkPoint(503, true, 439, -4, 502, 505));
			cp7.LinkMap.Add(505, new UILinkPoint(505, true, 439, -4, 503, 504));
			cp7.LinkMap.Add(504, new UILinkPoint(504, true, 439, -4, 505, 310));
			cp7.LinkMap[500].OnSpecialInteracts += func;
			cp7.LinkMap[501].OnSpecialInteracts += func;
			cp7.LinkMap[502].OnSpecialInteracts += func;
			cp7.LinkMap[503].OnSpecialInteracts += func;
			cp7.LinkMap[504].OnSpecialInteracts += func;
			cp7.LinkMap[505].OnSpecialInteracts += func;
			cp7.LinkMap[409].Right = 500;
			cp7.LinkMap[419].Right = 501;
			cp7.LinkMap[429].Right = 502;
			cp7.LinkMap[439].Right = 503;
			cp7.LinkMap[439].Down = 300;
			cp7.PageOnLeft = 0;
			cp7.PageOnRight = 0;
			cp7.DefaultPoint = 400;
			cp7.UpdateEvent += delegate
			{
				if (Main.LocalPlayer.chest < -1)
				{
					cp7.LinkMap[505].Down = 310;
					return;
				}
				cp7.LinkMap[505].Down = 504;
			};
			UILinkPointNavigator.RegisterPage(cp7, 4, false);
			cp7.IsValidEvent += () => Main.playerInventory && Main.player[Main.myPlayer].chest != -1 && !NewCraftingUI.Visible;
			UILinkPage uilinkPage2 = new UILinkPage();
			uilinkPage2.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			Func<string> func9 = delegate
			{
				int num46 = UILinkPointNavigator.CurrentPoint - 5100;
				TEDisplayDoll tedisplayDoll = Main.LocalPlayer.tileEntityAnchor.GetTileEntity() as TEDisplayDoll;
				if (tedisplayDoll == null)
				{
					return "";
				}
				return tedisplayDoll.GetItemGamepadInstructions(num46);
			};
			int num8;
			UILinkPoint uilinkPoint6;
			for (num8 = 5100; num8 < 5118; num8++)
			{
				uilinkPoint6 = new UILinkPoint(num8, true, num8 - 1, num8 + 1, num8 - 9, num8 + 9);
				uilinkPoint6.OnSpecialInteracts += func9;
				int num9 = num8 - 5100;
				if (num9 < 9)
				{
					uilinkPoint6.Up = 40 + (int)Math.Round((double)(num9 + 1) * 0.9);
				}
				if (num9 >= 9)
				{
					uilinkPoint6.Down = -2;
				}
				if (num9 % 9 == 8)
				{
					uilinkPoint6.Right = -4;
				}
				if (num9 % 9 == 0)
				{
					uilinkPoint6.Left = -3;
				}
				uilinkPage2.LinkMap.Add(num8, uilinkPoint6);
			}
			uilinkPoint6 = new UILinkPoint(num8, true, -3, 5100, 40, -2);
			uilinkPoint6.OnSpecialInteracts += func9;
			uilinkPage2.LinkMap.Add(num8, uilinkPoint6);
			uilinkPage2.LinkMap[5100].Left = num8;
			uilinkPage2.PageOnLeft = 0;
			uilinkPage2.PageOnRight = 0;
			uilinkPage2.DefaultPoint = 5100;
			UILinkPointNavigator.RegisterPage(uilinkPage2, 20, false);
			uilinkPage2.IsValidEvent += () => Main.playerInventory && Main.LocalPlayer.tileEntityAnchor.GetTileEntity() is TEDisplayDoll;
			UILinkPage uilinkPage3 = new UILinkPage();
			uilinkPage3.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			Func<string> func10 = delegate
			{
				int num47 = UILinkPointNavigator.CurrentPoint - 5000;
				TEHatRack tehatRack = Main.LocalPlayer.tileEntityAnchor.GetTileEntity() as TEHatRack;
				if (tehatRack == null)
				{
					return "";
				}
				return tehatRack.GetItemGamepadInstructions(num47);
			};
			for (int num10 = 5000; num10 <= 5003; num10++)
			{
				UILinkPoint uilinkPoint7 = new UILinkPoint(num10, true, num10 - 1, num10 + 1, num10 - 2, num10 + 2);
				uilinkPoint7.OnSpecialInteracts += func10;
				int num11 = num10 - 5000;
				if (num11 < 2)
				{
					uilinkPoint7.Up = 44 + num11;
				}
				if (num11 >= 2)
				{
					uilinkPoint7.Down = -2;
				}
				if (num11 % 2 == 1)
				{
					uilinkPoint7.Right = -4;
				}
				if (num11 % 2 == 0)
				{
					uilinkPoint7.Left = -3;
				}
				uilinkPage3.LinkMap.Add(num10, uilinkPoint7);
			}
			uilinkPage3.PageOnLeft = 0;
			uilinkPage3.PageOnRight = 0;
			uilinkPage3.DefaultPoint = 5000;
			UILinkPointNavigator.RegisterPage(uilinkPage3, 21, false);
			uilinkPage3.IsValidEvent += () => Main.playerInventory && Main.LocalPlayer.tileEntityAnchor.GetTileEntity() is TEHatRack;
			UILinkPage uilinkPage4 = new UILinkPage();
			uilinkPage4.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			Func<string> func11 = delegate
			{
				if (Main.npcShop == 0)
				{
					return "";
				}
				int num48 = UILinkPointNavigator.CurrentPoint - 2700;
				return ItemSlot.GetGamepadInstructions(Main.instance.shop[Main.npcShop].item, 15, num48);
			};
			for (int num12 = 2700; num12 <= 2739; num12++)
			{
				UILinkPoint uilinkPoint8 = new UILinkPoint(num12, true, num12 - 1, num12 + 1, num12 - 10, num12 + 10);
				uilinkPoint8.OnSpecialInteracts += func11;
				int num13 = num12 - 2700;
				if (num13 < 10)
				{
					uilinkPoint8.Up = 40 + num13;
				}
				if (num13 >= 30)
				{
					uilinkPoint8.Down = -2;
				}
				if (num13 % 10 == 9)
				{
					uilinkPoint8.Right = -4;
				}
				if (num13 % 10 == 0)
				{
					uilinkPoint8.Left = -3;
				}
				uilinkPage4.LinkMap.Add(num12, uilinkPoint8);
			}
			uilinkPage4.LinkMap[2739].Down = 300;
			uilinkPage4.PageOnLeft = 0;
			uilinkPage4.PageOnRight = 0;
			UILinkPointNavigator.RegisterPage(uilinkPage4, 13, true);
			uilinkPage4.IsValidEvent += () => Main.playerInventory && Main.npcShop != 0;
			UILinkPage cp8 = new UILinkPage();
			cp8.LinkMap.Add(303, new UILinkPoint(303, true, 304, 304, 40, -2));
			cp8.LinkMap.Add(304, new UILinkPoint(304, true, 303, 303, 40, -2));
			cp8.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			Func<string> func12 = () => ItemSlot.GetGamepadInstructions(ref Main.reforgeItem, 5);
			cp8.LinkMap[303].OnSpecialInteracts += func12;
			cp8.LinkMap[304].OnSpecialInteracts += () => Lang.misc[53].Value;
			cp8.UpdateEvent += delegate
			{
				if (Main.reforgeItem.type > 0)
				{
					cp8.LinkMap[303].Left = (cp8.LinkMap[303].Right = 304);
					return;
				}
				if (UILinkPointNavigator.OverridePoint == -1 && cp8.CurrentPoint == 304)
				{
					UILinkPointNavigator.ChangePoint(303);
				}
				cp8.LinkMap[303].Left = -3;
				cp8.LinkMap[303].Right = -4;
			};
			cp8.IsValidEvent += () => Main.playerInventory && Main.InReforgeMenu;
			cp8.PageOnLeft = 0;
			cp8.PageOnRight = 0;
			cp8.EnterEvent += delegate
			{
				PlayerInput.LockGamepadButtons("MouseLeft");
			};
			UILinkPointNavigator.RegisterPage(cp8, 5, true);
			UILinkPage cp9 = new UILinkPage();
			cp9.OnSpecialInteracts += delegate
			{
				string text = PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
				if (PlayerInput.ControllerHousingCursorActive)
				{
					bool flag7 = UILinkPointNavigator.CurrentPoint == 600;
					bool flag8 = UILinkPointNavigator.Shortcuts.NPCS_HoveredBanner >= 0;
					if (flag8)
					{
						string fullName = Main.npc[UILinkPointNavigator.Shortcuts.NPCS_HoveredBanner].FullName;
						text += PlayerInput.BuildCommand(Language.GetTextValue("UI.HousingEvict", fullName), new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"] });
					}
					else if (flag7)
					{
						text += PlayerInput.BuildCommand(Lang.misc[70].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"] });
					}
					else if (UILinkPointNavigator.Shortcuts.NPCS_SelectedNPC >= 0)
					{
						string fullName2 = Main.npc[UILinkPointNavigator.Shortcuts.NPCS_SelectedNPC].FullName;
						text += PlayerInput.BuildCommand(Language.GetTextValue("UI.HousingAssign", fullName2), new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"] });
					}
					if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.Grapple)
					{
						Point point = PlayerInput.HousingWorldPosition.ToTileCoordinates();
						if (flag8)
						{
							WorldGen.kickOut(UILinkPointNavigator.Shortcuts.NPCS_HoveredBanner);
							SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
						}
						else if (flag7)
						{
							Main.instance.PerformHousingCheck(point.X, point.Y);
						}
						else if (UILinkPointNavigator.Shortcuts.NPCS_SelectedNPC >= 0)
						{
							Main.instance.TryMovingNPC(point.X, point.Y, UILinkPointNavigator.Shortcuts.NPCS_SelectedNPC);
						}
						PlayerInput.LockGamepadButtons("Grapple");
						PlayerInput.SettingsForUI.TryRevertingToMouseMode();
					}
					text += PlayerInput.BuildCommand(Language.GetTextValue("UI.HousingAim"), new List<string>[] { UILinksInitializer.RightStickGlyphBinding });
				}
				return text;
			};
			for (int num14 = 600; num14 <= 650; num14++)
			{
				UILinkPoint uilinkPoint9 = new UILinkPoint(num14, true, num14 + 10, num14 - 10, num14 - 1, num14 + 1);
				cp9.LinkMap.Add(num14, uilinkPoint9);
			}
			cp9.UpdateEvent += delegate
			{
				int num49 = UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn;
				if (num49 == 0)
				{
					num49 = 100;
				}
				for (int num50 = 0; num50 < 50; num50++)
				{
					cp9.LinkMap[600 + num50].Up = ((num50 % num49 == 0) ? (-1) : (600 + num50 - 1));
					if (cp9.LinkMap[600 + num50].Up == -1)
					{
						if (num50 >= num49 * 2)
						{
							cp9.LinkMap[600 + num50].Up = 307;
						}
						else if (num50 >= num49)
						{
							cp9.LinkMap[600 + num50].Up = 306;
						}
						else
						{
							cp9.LinkMap[600 + num50].Up = 305;
						}
					}
					cp9.LinkMap[600 + num50].Down = (((num50 + 1) % num49 == 0 || num50 == UILinkPointNavigator.Shortcuts.NPCS_IconsTotal - 1) ? 308 : (600 + num50 + 1));
					cp9.LinkMap[600 + num50].Left = ((num50 < UILinkPointNavigator.Shortcuts.NPCS_IconsTotal - num49) ? (600 + num50 + num49) : (-3));
					cp9.LinkMap[600 + num50].Right = ((num50 < num49) ? (-4) : (600 + num50 - num49));
				}
			};
			cp9.IsValidEvent += () => Main.playerInventory && Main.EquipPage == 1;
			cp9.PageOnLeft = 8;
			cp9.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp9, 6, true);
			UILinkPage cp10 = new UILinkPage();
			cp10.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			Func<string> func13 = delegate
			{
				int num51 = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 20, num51);
			};
			Func<string> func14 = delegate
			{
				int num52 = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 19, num52);
			};
			Func<string> func15 = delegate
			{
				int num53 = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 18, num53);
			};
			Func<string> func16 = delegate
			{
				int num54 = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 17, num54);
			};
			Func<string> func17 = delegate
			{
				int num55 = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 16, num55);
			};
			Func<string> func18 = delegate
			{
				int num56 = UILinkPointNavigator.CurrentPoint - 185;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscDyes, 33, num56);
			};
			for (int num15 = 180; num15 <= 184; num15++)
			{
				UILinkPoint uilinkPoint10 = new UILinkPoint(num15, true, 185 + num15 - 180, -4, num15 - 1, num15 + 1);
				int num16 = num15 - 180;
				if (num16 == 0)
				{
					uilinkPoint10.Up = 305;
				}
				if (num16 == 4)
				{
					uilinkPoint10.Down = 308;
				}
				cp10.LinkMap.Add(num15, uilinkPoint10);
				switch (num15)
				{
				case 180:
					uilinkPoint10.OnSpecialInteracts += func14;
					break;
				case 181:
					uilinkPoint10.OnSpecialInteracts += func13;
					break;
				case 182:
					uilinkPoint10.OnSpecialInteracts += func15;
					break;
				case 183:
					uilinkPoint10.OnSpecialInteracts += func16;
					break;
				case 184:
					uilinkPoint10.OnSpecialInteracts += func17;
					break;
				}
			}
			for (int num17 = 185; num17 <= 189; num17++)
			{
				UILinkPoint uilinkPoint10 = new UILinkPoint(num17, true, -3, -5 + num17, num17 - 1, num17 + 1);
				uilinkPoint10.OnSpecialInteracts += func18;
				int num18 = num17 - 185;
				if (num18 == 0)
				{
					uilinkPoint10.Up = 306;
				}
				if (num18 == 4)
				{
					uilinkPoint10.Down = 308;
				}
				cp10.LinkMap.Add(num17, uilinkPoint10);
			}
			cp10.UpdateEvent += delegate
			{
				cp10.LinkMap[184].Down = ((UILinkPointNavigator.Shortcuts.BUFFS_DRAWN > 0) ? 9000 : 308);
				cp10.LinkMap[189].Down = ((UILinkPointNavigator.Shortcuts.BUFFS_DRAWN > 0) ? 9000 : 308);
			};
			cp10.IsValidEvent += () => Main.playerInventory && Main.EquipPage == 2;
			cp10.PageOnLeft = 8;
			cp10.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp10, 7, true);
			UILinkPage cp11 = new UILinkPage();
			cp11.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			cp11.LinkMap.Add(305, new UILinkPoint(305, true, 306, -4, 308, -2));
			cp11.LinkMap.Add(306, new UILinkPoint(306, true, 307, 305, 308, -2));
			cp11.LinkMap.Add(307, new UILinkPoint(307, true, -3, 306, 308, -2));
			cp11.LinkMap.Add(308, new UILinkPoint(308, true, -3, -4, -1, 305));
			cp11.LinkMap[305].OnSpecialInteracts += func;
			cp11.LinkMap[306].OnSpecialInteracts += func;
			cp11.LinkMap[307].OnSpecialInteracts += func;
			cp11.LinkMap[308].OnSpecialInteracts += func;
			cp11.UpdateEvent += delegate
			{
				switch (Main.EquipPage)
				{
				case 0:
					cp11.LinkMap[305].Down = 100;
					cp11.LinkMap[306].Down = 110;
					cp11.LinkMap[307].Down = 120;
					cp11.LinkMap[308].Up = 108 + Main.player[Main.myPlayer].GetAmountOfExtraAccessorySlotsToShow() - 1;
					break;
				case 1:
				{
					cp11.LinkMap[305].Down = 600;
					cp11.LinkMap[306].Down = ((UILinkPointNavigator.Shortcuts.NPCS_IconsTotal > UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn) ? (600 + UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn) : 600);
					cp11.LinkMap[307].Down = ((UILinkPointNavigator.Shortcuts.NPCS_IconsTotal > UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn * 2) ? (600 + UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn * 2) : cp11.LinkMap[306].Down);
					int num57 = UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn;
					if (num57 == 0)
					{
						num57 = 100;
					}
					if (num57 == 100)
					{
						num57 = UILinkPointNavigator.Shortcuts.NPCS_IconsTotal;
					}
					cp11.LinkMap[308].Up = 600 + num57 - 1;
					break;
				}
				case 2:
					cp11.LinkMap[305].Down = 180;
					cp11.LinkMap[306].Down = 185;
					cp11.LinkMap[307].Down = -2;
					cp11.LinkMap[308].Up = ((UILinkPointNavigator.Shortcuts.BUFFS_DRAWN > 0) ? 9000 : 184);
					break;
				}
				cp11.PageOnRight = UILinksInitializer.GetCornerWrapPageIdFromRightToLeft();
			};
			cp11.IsValidEvent += () => Main.playerInventory;
			cp11.PageOnLeft = 0;
			cp11.PageOnRight = 0;
			UILinkPointNavigator.RegisterPage(cp11, 8, true);
			UILinkPage cp12 = new UILinkPage();
			cp12.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			cp12.OnSpecialInteractsLate += () => ItemSlot.GetGamepadInstructions(Main.InPipBanner ? 35 : 22);
			for (int num19 = 1500; num19 < 1550; num19++)
			{
				UILinkPoint uilinkPoint11 = new UILinkPoint(num19, true, num19, num19, -1, -2);
				cp12.LinkMap.Add(num19, uilinkPoint11);
			}
			cp12.LinkMap.Add(11001, new UILinkPoint(11001, true, 1501, 11002, -1, 11003));
			cp12.LinkMap.Add(11002, new UILinkPoint(11002, true, 11001, -4, -1, 11003));
			cp12.LinkMap.Add(11003, new UILinkPoint(11003, true, 1501, -4, 11001, 1502));
			cp12.LinkMap[1500].OnSpecialInteracts += () => ItemSlot.GetGamepadInstructions(ref Main.guideItem, 7);
			cp12.LinkMap[11001].OnSpecialInteracts += () => PlayerInput.BuildCommand(Language.GetTextValue("UI.ToggleClassicGrid"), new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"] });
			cp12.UpdateEvent += delegate
			{
				cp12.PageOnLeft = ((Player.Settings.CraftingGridControl == Player.Settings.CraftingGridMode.Classic) ? 10 : 8);
				int num58 = UILinkPointNavigator.Shortcuts.CRAFT_CurrentIngredientsCount;
				int num59 = num58;
				if (UILinksInitializer.MainnumAvailableRecipes > 0)
				{
					num59 += 2;
				}
				if (num58 < num59)
				{
					num58 = num59;
				}
				if (UILinkPointNavigator.OverridePoint == -1)
				{
					if (cp12.CurrentPoint == 11003)
					{
						if (Main.InGuideCraftMenu)
						{
							UILinkPointNavigator.ChangePoint(1501);
						}
					}
					else if (cp12.CurrentPoint != 11001)
					{
						if (cp12.CurrentPoint == 11002)
						{
							if (!Main.bannerUI.AnyAvailableBanners || Main.InGuideCraftMenu)
							{
								UILinkPointNavigator.ChangePoint(11001);
							}
						}
						else if (cp12.CurrentPoint == 1500)
						{
							if (!Main.InGuideCraftMenu)
							{
								UILinkPointNavigator.ChangePoint(1501);
							}
						}
						else if (cp12.CurrentPoint > 1500 + num58)
						{
							UILinkPointNavigator.ChangePoint(1500);
						}
					}
				}
				bool flag9 = Main.LocalPlayer.chest != -1;
				for (int num60 = 1; num60 < num58; num60++)
				{
					cp12.LinkMap[1500 + num60].Left = 1500 + num60 - 1;
					cp12.LinkMap[1500 + num60].Right = ((num60 == num58 - 2) ? (-4) : (1500 + num60 + 1));
					if (num60 >= 2)
					{
						cp12.LinkMap[1500 + num60].Up = (Main.InGuideCraftMenu ? 1500 : (flag9 ? 11003 : (-1)));
						cp12.LinkMap[1500 + num60].Down = (flag9 ? (-1) : ((num60 >= 3 && Main.bannerUI.AnyAvailableBanners) ? 11002 : 11001));
					}
				}
				cp12.LinkMap[1501].Left = -3;
				if (num58 > 0)
				{
					cp12.LinkMap[1500 + num58 - 1].Right = -4;
				}
				cp12.LinkMap[1500].Down = ((num58 >= 2) ? 1502 : (-2));
				cp12.LinkMap[1500].Left = ((num58 >= 1) ? 1501 : (-3));
				cp12.LinkMap[1500].Up = 11001;
				cp12.LinkMap[11001].Left = (Main.InPipCrafting ? 1501 : 12000);
				cp12.LinkMap[11001].Down = ((!Main.InPipCrafting) ? (-1) : (Main.InGuideCraftMenu ? 1500 : 11003));
				cp12.LinkMap[11001].Right = ((!Main.bannerUI.AnyAvailableBanners || Main.InGuideCraftMenu) ? (-1) : 11002);
				cp12.LinkMap[11001].Up = (flag9 ? (-1) : 1502);
				cp12.LinkMap[11002].Down = ((!Main.InPipCrafting) ? (-1) : 11003);
				cp12.LinkMap[11002].Up = (flag9 ? (-1) : ((num58 >= 5) ? 1503 : 1502));
				cp12.LinkMap[11003].Down = (flag9 ? 1502 : (-1));
			};
			cp12.LinkMap[1501].OnSpecialInteracts += () => ItemSlot.GetCraftSlotGamepadInstructions();
			cp12.ReachEndEvent += delegate(int current, int next)
			{
				if (current != 1500)
				{
					if (current == 1501)
					{
						if (next == -1)
						{
							if (UILinksInitializer.MainfocusRecipe > 0)
							{
								UILinksInitializer.MainfocusRecipe--;
								return;
							}
						}
						else if (next == -2 && UILinksInitializer.MainfocusRecipe < UILinksInitializer.MainnumAvailableRecipes - 1)
						{
							UILinksInitializer.MainfocusRecipe++;
							return;
						}
					}
					else if (next == -1)
					{
						if (UILinksInitializer.MainfocusRecipe > 0)
						{
							UILinkPointNavigator.ChangePoint(1501);
							UILinksInitializer.MainfocusRecipe--;
							return;
						}
					}
					else if (next == -2 && UILinksInitializer.MainfocusRecipe < UILinksInitializer.MainnumAvailableRecipes - 1)
					{
						UILinkPointNavigator.ChangePoint(1501);
						UILinksInitializer.MainfocusRecipe++;
					}
				}
			};
			cp12.EnterEvent += delegate
			{
				Main.PipsUseGrid = false;
				PlayerInput.LockGamepadButtons("MouseLeft");
			};
			cp12.CanEnterEvent += () => Main.playerInventory && (UILinksInitializer.MainnumAvailableRecipes > 0 || Main.InGuideCraftMenu);
			cp12.IsValidEvent += () => Main.playerInventory && (UILinksInitializer.MainnumAvailableRecipes > 0 || Main.InGuideCraftMenu);
			cp12.PageOnLeft = 8;
			cp12.PageOnRight = 0;
			UILinkPointNavigator.RegisterPage(cp12, 9, true);
			UILinkPage cp13 = new UILinkPage();
			cp13.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			cp13.OnSpecialInteractsLate += () => ItemSlot.GetGamepadInstructions(Main.InPipBanner ? 35 : 22);
			for (int num20 = 22000; num20 < 30000; num20++)
			{
				UILinkPoint uilinkPoint12 = new UILinkPoint(num20, true, num20, num20, num20, num20);
				int IHateLambda = num20;
				uilinkPoint12.OnSpecialInteracts += delegate
				{
					string text2 = PlayerInput.BuildCommand(Lang.misc[73].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] });
					if (UILinksInitializer.TryQuickCrafting(22000, IHateLambda))
					{
						text2 += PlayerInput.BuildCommand(Lang.misc[71].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"] });
					}
					return text2;
				};
				cp13.LinkMap.Add(num20, uilinkPoint12);
			}
			cp13.UpdateEvent += delegate
			{
				int num61 = UILinkPointNavigator.Shortcuts.CRAFT_IconsPerRow;
				int craft_IconsPerColumn = UILinkPointNavigator.Shortcuts.CRAFT_IconsPerColumn;
				if (num61 == 0)
				{
					num61 = 100;
				}
				int num62 = num61 * craft_IconsPerColumn;
				if (num62 > 8000)
				{
					num62 = 8000;
				}
				if (num62 > UILinksInitializer.MainnumAvailableRecipes)
				{
					num62 = UILinksInitializer.MainnumAvailableRecipes;
				}
				for (int num63 = 0; num63 < num62; num63++)
				{
					cp13.LinkMap[22000 + num63].Left = ((num63 % num61 == 0) ? (-3) : (22000 + num63 - 1));
					cp13.LinkMap[22000 + num63].Right = (((num63 + 1) % num61 == 0 || num63 == UILinksInitializer.MainnumAvailableRecipes - 1) ? (-4) : (22000 + num63 + 1));
					cp13.LinkMap[22000 + num63].Down = ((num63 < num62 - num61) ? (22000 + num63 + num61) : (-2));
					cp13.LinkMap[22000 + num63].Up = ((num63 < num61) ? (-1) : (22000 + num63 - num61));
				}
				cp13.PageOnLeft = UILinksInitializer.GetCornerWrapPageIdFromLeftToRight();
			};
			cp13.ReachEndEvent += delegate(int current, int next)
			{
				int craft_IconsPerRow = UILinkPointNavigator.Shortcuts.CRAFT_IconsPerRow;
				if (next == -1)
				{
					Main.recStart -= craft_IconsPerRow;
					if (Main.recStart < 0)
					{
						Main.recStart = 0;
						return;
					}
				}
				else if (next == -2)
				{
					Main.recStart += craft_IconsPerRow;
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					if (Main.recStart > UILinksInitializer.MainnumAvailableRecipes - craft_IconsPerRow)
					{
						Main.recStart = UILinksInitializer.MainnumAvailableRecipes - craft_IconsPerRow;
					}
				}
			};
			cp13.EnterEvent += delegate
			{
				Main.PipsUseGrid = true;
			};
			cp13.LeaveEvent += delegate
			{
				Main.PipsUseGrid = false;
			};
			cp13.CanEnterEvent += () => Main.playerInventory && UILinksInitializer.MainnumAvailableRecipes > 0;
			cp13.IsValidEvent += () => Main.playerInventory && Main.PipsUseGrid && UILinksInitializer.MainnumAvailableRecipes > 0;
			cp13.PageOnLeft = 0;
			cp13.PageOnRight = 9;
			UILinkPointNavigator.RegisterPage(cp13, 10, true);
			UILinkPage cp14 = new UILinkPage();
			cp14.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			for (int num21 = 2605; num21 < 2620; num21++)
			{
				UILinkPoint uilinkPoint13 = new UILinkPoint(num21, true, num21, num21, num21, num21);
				uilinkPoint13.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[73].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] });
				cp14.LinkMap.Add(num21, uilinkPoint13);
			}
			cp14.UpdateEvent += delegate
			{
				int num64 = 5;
				int num65 = 3;
				int num66 = num64 * num65;
				int count = Main.Hairstyles.AvailableHairstyles.Count;
				for (int num67 = 0; num67 < num66; num67++)
				{
					cp14.LinkMap[2605 + num67].Left = ((num67 % num64 == 0) ? (-3) : (2605 + num67 - 1));
					cp14.LinkMap[2605 + num67].Right = (((num67 + 1) % num64 == 0 || num67 == count - 1) ? (-4) : (2605 + num67 + 1));
					cp14.LinkMap[2605 + num67].Down = ((num67 < num66 - num64) ? (2605 + num67 + num64) : (-2));
					cp14.LinkMap[2605 + num67].Up = ((num67 < num64) ? (-1) : (2605 + num67 - num64));
				}
			};
			cp14.ReachEndEvent += delegate(int current, int next)
			{
				int num68 = 5;
				if (next == -1)
				{
					Main.hairStart -= num68;
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				if (next == -2)
				{
					Main.hairStart += num68;
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
			};
			cp14.CanEnterEvent += () => Main.hairWindow;
			cp14.IsValidEvent += () => Main.hairWindow;
			cp14.PageOnLeft = 12;
			cp14.PageOnRight = 12;
			UILinkPointNavigator.RegisterPage(cp14, 11, true);
			UILinkPage uilinkPage5 = new UILinkPage();
			uilinkPage5.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			uilinkPage5.LinkMap.Add(2600, new UILinkPoint(2600, true, -3, -4, -1, 2601));
			uilinkPage5.LinkMap.Add(2601, new UILinkPoint(2601, true, -3, -4, 2600, 2602));
			uilinkPage5.LinkMap.Add(2602, new UILinkPoint(2602, true, -3, -4, 2601, 2603));
			uilinkPage5.LinkMap.Add(2603, new UILinkPoint(2603, true, -3, 2604, 2602, -2));
			uilinkPage5.LinkMap.Add(2604, new UILinkPoint(2604, true, 2603, -4, 2602, -2));
			uilinkPage5.UpdateEvent += delegate
			{
				Vector3 vector = Main.rgbToHsl(Main.selColor);
				float interfaceDeadzoneX = PlayerInput.CurrentProfile.InterfaceDeadzoneX;
				float num69 = PlayerInput.GamepadThumbstickLeft.X;
				if (num69 < -interfaceDeadzoneX || num69 > interfaceDeadzoneX)
				{
					num69 = MathHelper.Lerp(0f, 0.008333334f, (Math.Abs(num69) - interfaceDeadzoneX) / (1f - interfaceDeadzoneX)) * (float)Math.Sign(num69);
				}
				else
				{
					num69 = 0f;
				}
				int currentPoint4 = UILinkPointNavigator.CurrentPoint;
				if (currentPoint4 == 2600)
				{
					Main.hBar = MathHelper.Clamp(Main.hBar + num69, 0f, 1f);
				}
				if (currentPoint4 == 2601)
				{
					Main.sBar = MathHelper.Clamp(Main.sBar + num69, 0f, 1f);
				}
				if (currentPoint4 == 2602)
				{
					Main.lBar = MathHelper.Clamp(Main.lBar + num69, 0.15f, 1f);
				}
				Vector3.Clamp(vector, Vector3.Zero, Vector3.One);
				if (num69 != 0f)
				{
					if (Main.hairWindow)
					{
						Main.player[Main.myPlayer].hairColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
					}
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
			};
			uilinkPage5.CanEnterEvent += () => Main.hairWindow;
			uilinkPage5.IsValidEvent += () => Main.hairWindow;
			uilinkPage5.PageOnLeft = 11;
			uilinkPage5.PageOnRight = 11;
			UILinkPointNavigator.RegisterPage(uilinkPage5, 12, true);
			UILinkPage cp15 = new UILinkPage();
			for (int num22 = 0; num22 < 30; num22++)
			{
				cp15.LinkMap.Add(2900 + num22, new UILinkPoint(2900 + num22, true, -3, -4, -1, -2));
				cp15.LinkMap[2900 + num22].OnSpecialInteracts += func;
			}
			cp15.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			cp15.TravelEvent += delegate
			{
				if (UILinkPointNavigator.CurrentPage == cp15.ID)
				{
					int num70 = cp15.CurrentPoint - 2900;
					if (num70 < 5)
					{
						IngameOptions.category = num70;
					}
				}
			};
			cp15.UpdateEvent += delegate
			{
				int num71 = UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_LEFT;
				if (num71 == 0)
				{
					num71 = 5;
				}
				if (UILinkPointNavigator.OverridePoint == -1 && cp15.CurrentPoint < 2930 && cp15.CurrentPoint > 2900 + num71 - 1)
				{
					UILinkPointNavigator.ChangePoint(2900);
				}
				for (int num72 = 2900; num72 < 2900 + num71; num72++)
				{
					cp15.LinkMap[num72].Up = num72 - 1;
					cp15.LinkMap[num72].Down = num72 + 1;
				}
				cp15.LinkMap[2900].Up = 2900 + num71 - 1;
				cp15.LinkMap[2900 + num71 - 1].Down = 2900;
				int num73 = cp15.CurrentPoint - 2900;
				if (num73 < 4 && UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.MouseLeft)
				{
					IngameOptions.category = num73;
					UILinkPointNavigator.ChangePage(1002);
				}
				int num74 = ((SocialAPI.Network != null && SocialAPI.Network.CanInvite()) ? 1 : 0);
				if (num73 == 4 + num74 && UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.MouseLeft)
				{
					UILinkPointNavigator.ChangePage(1004);
				}
			};
			cp15.EnterEvent += delegate
			{
				cp15.CurrentPoint = 2900 + IngameOptions.category;
			};
			cp15.PageOnLeft = (cp15.PageOnRight = 1002);
			cp15.IsValidEvent += () => Main.ingameOptionsWindow && !Main.InGameUI.IsVisible;
			cp15.CanEnterEvent += () => Main.ingameOptionsWindow && !Main.InGameUI.IsVisible;
			UILinkPointNavigator.RegisterPage(cp15, 1001, true);
			UILinkPage cp16 = new UILinkPage();
			for (int num23 = 0; num23 < 30; num23++)
			{
				cp16.LinkMap.Add(2930 + num23, new UILinkPoint(2930 + num23, true, -3, -4, -1, -2));
				cp16.LinkMap[2930 + num23].OnSpecialInteracts += func;
			}
			cp16.EnterEvent += delegate
			{
				Main.mouseLeftRelease = false;
			};
			cp16.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			cp16.UpdateEvent += delegate
			{
				int num75 = UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_RIGHT;
				if (num75 == 0)
				{
					num75 = 5;
				}
				if (UILinkPointNavigator.OverridePoint == -1 && cp16.CurrentPoint >= 2930 && cp16.CurrentPoint > 2930 + num75 - 1)
				{
					UILinkPointNavigator.ChangePoint(2930);
				}
				for (int num76 = 2930; num76 < 2930 + num75; num76++)
				{
					cp16.LinkMap[num76].Up = num76 - 1;
					cp16.LinkMap[num76].Down = num76 + 1;
				}
				cp16.LinkMap[2930].Up = -1;
				cp16.LinkMap[2930 + num75 - 1].Down = -2;
				UILinksInitializer.HandleOptionsSpecials();
			};
			cp16.PageOnLeft = (cp16.PageOnRight = 1001);
			cp16.IsValidEvent += () => Main.ingameOptionsWindow;
			cp16.CanEnterEvent += () => Main.ingameOptionsWindow;
			UILinkPointNavigator.RegisterPage(cp16, 1002, true);
			UILinkPage cp17 = new UILinkPage();
			cp17.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			for (int num24 = 1550; num24 < 1558; num24++)
			{
				UILinkPoint uilinkPoint14 = new UILinkPoint(num24, true, -3, -4, -1, -2);
				switch (num24 - 1550)
				{
				case 1:
				case 3:
				case 5:
					uilinkPoint14.Up = uilinkPoint14.ID - 2;
					uilinkPoint14.Down = uilinkPoint14.ID + 2;
					uilinkPoint14.Right = uilinkPoint14.ID + 1;
					break;
				case 2:
				case 4:
				case 6:
					uilinkPoint14.Up = uilinkPoint14.ID - 2;
					uilinkPoint14.Down = uilinkPoint14.ID + 2;
					uilinkPoint14.Left = uilinkPoint14.ID - 1;
					break;
				}
				cp17.LinkMap.Add(num24, uilinkPoint14);
			}
			cp17.LinkMap[1550].Down = 1551;
			cp17.LinkMap[1550].Right = 120;
			cp17.LinkMap[1550].Up = 307;
			cp17.LinkMap[1552].Right = 121;
			cp17.LinkMap[1554].Right = 121;
			cp17.LinkMap[1555].Down = 1570;
			cp17.LinkMap[1556].Down = 1570;
			cp17.LinkMap[1556].Right = 122;
			cp17.LinkMap[1557].Up = 1570;
			cp17.LinkMap[1557].Down = 308;
			cp17.LinkMap[1557].Right = 127;
			cp17.LinkMap.Add(1570, new UILinkPoint(1570, true, -3, -4, -1, -2));
			cp17.LinkMap[1570].Up = 1555;
			cp17.LinkMap[1570].Down = 1557;
			cp17.LinkMap[1570].Right = 126;
			for (int num25 = 0; num25 < 7; num25++)
			{
				cp17.LinkMap[1550 + num25].OnSpecialInteracts += func;
			}
			cp17.UpdateEvent += delegate
			{
				cp17.LinkMap[1551].Up = (Main.ShouldPVPDraw ? 1550 : (-1));
				cp17.LinkMap[1552].Up = (Main.ShouldPVPDraw ? 1550 : (-1));
				cp17.LinkMap[1570].Up = (Main.ShouldTeamSelectDraw ? 1555 : (-1));
				int infoacccount = UILinkPointNavigator.Shortcuts.INFOACCCOUNT;
				if (infoacccount > 0)
				{
					cp17.LinkMap[1570].Up = 1558 + (infoacccount - 1) / 2 * 2;
				}
				if (Main.ShouldTeamSelectDraw)
				{
					if (infoacccount >= 1)
					{
						cp17.LinkMap[1555].Down = 1558;
						cp17.LinkMap[1556].Down = 1558;
					}
					else
					{
						cp17.LinkMap[1555].Down = 1570;
						cp17.LinkMap[1556].Down = 1570;
					}
					if (infoacccount >= 2)
					{
						cp17.LinkMap[1556].Down = 1559;
						return;
					}
					cp17.LinkMap[1556].Down = 1570;
				}
			};
			cp17.IsValidEvent += () => Main.playerInventory;
			cp17.PageOnLeft = 8;
			cp17.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp17, 16, true);
			UILinkPage cp18 = new UILinkPage();
			cp18.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			for (int num26 = 1558; num26 < 1570; num26++)
			{
				UILinkPoint uilinkPoint15 = new UILinkPoint(num26, true, -3, -4, -1, -2);
				uilinkPoint15.OnSpecialInteracts += func;
				switch (num26 - 1558)
				{
				case 1:
				case 3:
				case 5:
					uilinkPoint15.Up = uilinkPoint15.ID - 2;
					uilinkPoint15.Down = uilinkPoint15.ID + 2;
					uilinkPoint15.Right = uilinkPoint15.ID + 1;
					break;
				case 2:
				case 4:
				case 6:
					uilinkPoint15.Up = uilinkPoint15.ID - 2;
					uilinkPoint15.Down = uilinkPoint15.ID + 2;
					uilinkPoint15.Left = uilinkPoint15.ID - 1;
					break;
				}
				cp18.LinkMap.Add(num26, uilinkPoint15);
			}
			cp18.UpdateEvent += delegate
			{
				int infoacccount2 = UILinkPointNavigator.Shortcuts.INFOACCCOUNT;
				if (UILinkPointNavigator.OverridePoint == -1 && cp18.CurrentPoint - 1558 >= infoacccount2)
				{
					UILinkPointNavigator.ChangePoint(1558 + infoacccount2 - 1);
				}
				for (int num77 = 0; num77 < infoacccount2; num77++)
				{
					bool flag10 = num77 % 2 == 0;
					int num78 = num77 + 1558;
					cp18.LinkMap[num78].Down = ((num77 < infoacccount2 - 2) ? (num78 + 2) : 1570);
					cp18.LinkMap[num78].Up = ((num77 > 1) ? (num78 - 2) : (Main.ShouldTeamSelectDraw ? (flag10 ? 1555 : 1556) : (-1)));
					cp18.LinkMap[num78].Right = ((flag10 && num77 + 1 < infoacccount2) ? (num78 + 1) : (123 + num77 / 4));
					cp18.LinkMap[num78].Left = (flag10 ? (-3) : (num78 - 1));
				}
			};
			cp18.IsValidEvent += () => Main.playerInventory && UILinkPointNavigator.Shortcuts.INFOACCCOUNT > 0;
			cp18.PageOnLeft = 8;
			cp18.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp18, 17, true);
			UILinkPage cp19 = new UILinkPage();
			cp19.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			int num27 = 6000;
			while (num27 < 6012)
			{
				UILinkPoint uilinkPoint16 = new UILinkPoint(num27, true, -3, -4, -1, -2);
				switch (num27)
				{
				case 6000:
					uilinkPoint16.Right = 0;
					break;
				case 6001:
				case 6002:
					uilinkPoint16.Right = 10;
					break;
				case 6003:
				case 6004:
					uilinkPoint16.Right = 20;
					break;
				case 6005:
				case 6006:
					uilinkPoint16.Right = 30;
					break;
				case 6007:
				case 6008:
				case 6009:
					goto IL_2E17;
				default:
					goto IL_2E17;
				}
				IL_2E20:
				cp19.LinkMap.Add(num27, uilinkPoint16);
				num27++;
				continue;
				IL_2E17:
				uilinkPoint16.Right = 40;
				goto IL_2E20;
			}
			cp19.UpdateEvent += delegate
			{
				int builderacccount = UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT;
				if (UILinkPointNavigator.OverridePoint == -1 && cp19.CurrentPoint - 6000 >= builderacccount)
				{
					UILinkPointNavigator.ChangePoint(6000 + builderacccount - 1);
				}
				for (int num79 = 0; num79 < builderacccount; num79++)
				{
					int num80 = num79 % 2;
					int num81 = num79 + 6000;
					cp19.LinkMap[num81].Down = ((num79 < builderacccount - 1) ? (num81 + 1) : (-2));
					cp19.LinkMap[num81].Up = ((num79 > 0) ? (num81 - 1) : (-1));
				}
			};
			cp19.IsValidEvent += () => Main.playerInventory && UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 0;
			cp19.PageOnLeft = 8;
			cp19.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp19, 18, true);
			UILinkPage uilinkPage6 = new UILinkPage();
			uilinkPage6.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			uilinkPage6.LinkMap.Add(2806, new UILinkPoint(2806, true, 2805, 2807, -1, 2808));
			uilinkPage6.LinkMap.Add(2807, new UILinkPoint(2807, true, 2806, 2810, -1, 2809));
			uilinkPage6.LinkMap.Add(2808, new UILinkPoint(2808, true, 2813, 2809, 2806, -2));
			uilinkPage6.LinkMap.Add(2809, new UILinkPoint(2809, true, 2808, 2811, 2807, -2));
			uilinkPage6.LinkMap.Add(2810, new UILinkPoint(2810, true, 2807, -4, -1, 2811));
			uilinkPage6.LinkMap.Add(2811, new UILinkPoint(2811, true, 2809, -4, 2810, -2));
			uilinkPage6.LinkMap.Add(2805, new UILinkPoint(2805, true, -3, 2806, -1, 2813));
			uilinkPage6.LinkMap.Add(2813, new UILinkPoint(2813, true, -3, 2808, 2805, -2));
			uilinkPage6.LinkMap[2806].OnSpecialInteracts += func;
			uilinkPage6.LinkMap[2807].OnSpecialInteracts += func;
			uilinkPage6.LinkMap[2808].OnSpecialInteracts += func;
			uilinkPage6.LinkMap[2809].OnSpecialInteracts += func;
			uilinkPage6.LinkMap[2805].OnSpecialInteracts += func;
			uilinkPage6.LinkMap[2813].OnSpecialInteracts += func;
			uilinkPage6.CanEnterEvent += () => Main.clothesWindow;
			uilinkPage6.IsValidEvent += () => Main.clothesWindow;
			uilinkPage6.EnterEvent += delegate
			{
				Main.player[Main.myPlayer].releaseInventory = false;
			};
			uilinkPage6.LeaveEvent += delegate
			{
				Main.player[Main.myPlayer].LockGamepadTileInteractions();
			};
			uilinkPage6.PageOnLeft = 15;
			uilinkPage6.PageOnRight = 15;
			UILinkPointNavigator.RegisterPage(uilinkPage6, 14, true);
			UILinkPage uilinkPage7 = new UILinkPage();
			uilinkPage7.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			uilinkPage7.LinkMap.Add(2800, new UILinkPoint(2800, true, -3, -4, -1, 2801));
			uilinkPage7.LinkMap.Add(2801, new UILinkPoint(2801, true, -3, -4, 2800, 2802));
			uilinkPage7.LinkMap.Add(2802, new UILinkPoint(2802, true, -3, -4, 2801, 2812));
			uilinkPage7.LinkMap.Add(2812, new UILinkPoint(2812, true, -3, -4, 2802, 2803));
			uilinkPage7.LinkMap.Add(2803, new UILinkPoint(2803, true, -3, 2804, 2812, -2));
			uilinkPage7.LinkMap.Add(2804, new UILinkPoint(2804, true, 2803, -4, 2812, -2));
			uilinkPage7.LinkMap[2800].OnSpecialInteracts += func;
			uilinkPage7.LinkMap[2801].OnSpecialInteracts += func;
			uilinkPage7.LinkMap[2802].OnSpecialInteracts += func;
			uilinkPage7.LinkMap[2812].OnSpecialInteracts += func;
			uilinkPage7.LinkMap[2803].OnSpecialInteracts += func;
			uilinkPage7.LinkMap[2804].OnSpecialInteracts += func;
			uilinkPage7.UpdateEvent += delegate
			{
				Vector3 vector2 = Main.rgbToHsl(Main.selColor);
				float interfaceDeadzoneX2 = PlayerInput.CurrentProfile.InterfaceDeadzoneX;
				float num82 = PlayerInput.GamepadThumbstickLeft.X;
				if (num82 < -interfaceDeadzoneX2 || num82 > interfaceDeadzoneX2)
				{
					num82 = MathHelper.Lerp(0f, 0.008333334f, (Math.Abs(num82) - interfaceDeadzoneX2) / (1f - interfaceDeadzoneX2)) * (float)Math.Sign(num82);
				}
				else
				{
					num82 = 0f;
				}
				int currentPoint5 = UILinkPointNavigator.CurrentPoint;
				if (currentPoint5 == 2800)
				{
					Main.hBar = MathHelper.Clamp(Main.hBar + num82, 0f, 1f);
				}
				if (currentPoint5 == 2801)
				{
					Main.sBar = MathHelper.Clamp(Main.sBar + num82, 0f, 1f);
				}
				if (currentPoint5 == 2802)
				{
					Main.lBar = MathHelper.Clamp(Main.lBar + num82, 0.15f, 1f);
				}
				if (currentPoint5 == 2812)
				{
					Main.player[Main.myPlayer].voicePitchOffset = MathHelper.Clamp(Main.player[Main.myPlayer].voicePitchOffset + num82, -1f, 1f);
				}
				Vector3.Clamp(vector2, Vector3.Zero, Vector3.One);
				if (num82 != 0f)
				{
					if (Main.clothesWindow)
					{
						Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue);
						switch (Main.selClothes)
						{
						case 0:
							Main.player[Main.myPlayer].shirtColor = Main.selColor;
							break;
						case 1:
							Main.player[Main.myPlayer].underShirtColor = Main.selColor;
							break;
						case 2:
							Main.player[Main.myPlayer].pantsColor = Main.selColor;
							break;
						case 3:
							Main.player[Main.myPlayer].shoeColor = Main.selColor;
							break;
						}
					}
					if (currentPoint5 != 2812)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					}
				}
				if (currentPoint5 == 2812)
				{
					bool flag11 = num82 != 0f;
					if (Main.WasDraggingPlayerAudio && !flag11)
					{
						Main.player[Main.myPlayer].PlayHurtSound();
					}
					Main.WasDraggingPlayerAudio = flag11;
				}
			};
			uilinkPage7.CanEnterEvent += () => Main.clothesWindow;
			uilinkPage7.IsValidEvent += () => Main.clothesWindow;
			uilinkPage7.EnterEvent += delegate
			{
				Main.player[Main.myPlayer].releaseInventory = false;
				Main.WasDraggingPlayerAudio = false;
			};
			uilinkPage7.LeaveEvent += delegate
			{
				Main.player[Main.myPlayer].LockGamepadTileInteractions();
			};
			uilinkPage7.PageOnLeft = 14;
			uilinkPage7.PageOnRight = 14;
			UILinkPointNavigator.RegisterPage(uilinkPage7, 15, true);
			UILinkPage cp20 = new UILinkPage();
			cp20.UpdateEvent += delegate
			{
				PlayerInput.GamepadAllowScrolling = true;
			};
			for (int num28 = 3000; num28 <= 4999; num28++)
			{
				cp20.LinkMap.Add(num28, new UILinkPoint(num28, true, -3, -4, -1, -2));
			}
			cp20.OnSpecialInteracts += delegate
			{
				if (Main.InGameUI.CurrentState is UIBestiaryTest)
				{
					return PlayerInput.BuildCommand(Lang.misc[82].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Language.GetText("UI.SwitchPage").Value, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
						PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
					}) + PlayerInput.BuildCommand(Lang.misc[53].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] }) + UILinksInitializer.FancyUISpecialInstructions();
				}
				return PlayerInput.BuildCommand(Lang.misc[53].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] }) + PlayerInput.BuildCommand(Lang.misc[82].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + UILinksInitializer.FancyUISpecialInstructions();
			};
			cp20.UpdateEvent += delegate
			{
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.Inventory)
				{
					UILinksInitializer.FancyExit();
				}
			};
			cp20.EnterEvent += delegate
			{
				cp20.CurrentPoint = 3002;
			};
			cp20.CanEnterEvent += () => Main.MenuUI.IsVisible || Main.InGameUI.IsVisible;
			cp20.IsValidEvent += () => Main.MenuUI.IsVisible || Main.InGameUI.IsVisible;
			cp20.OnPageMoveAttempt += UILinksInitializer.OnFancyUIPageMoveAttempt;
			UILinkPointNavigator.RegisterPage(cp20, 1004, true);
			UILinkPage cp21 = new UILinkPage();
			cp21.UpdateEvent += delegate
			{
				PlayerInput.GamepadAllowScrolling = true;
			};
			for (int num29 = 10000; num29 <= 11000; num29++)
			{
				cp21.LinkMap.Add(num29, new UILinkPoint(num29, true, -3, -4, -1, -2));
			}
			for (int num30 = 15000; num30 <= 15000; num30++)
			{
				cp21.LinkMap.Add(num30, new UILinkPoint(num30, true, -3, -4, -1, -2));
			}
			cp21.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}) + PlayerInput.BuildCommand(Lang.misc[53].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] }) + UILinksInitializer.FancyUISpecialInstructions();
			cp21.UpdateEvent += delegate
			{
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.Inventory)
				{
					UILinksInitializer.FancyExit();
				}
			};
			cp21.EnterEvent += delegate
			{
				cp21.CurrentPoint = 10000;
			};
			cp21.CanEnterEvent += UILinksInitializer.CanEnterCreativeMenu;
			cp21.IsValidEvent += UILinksInitializer.CanEnterCreativeMenu;
			cp21.OnPageMoveAttempt += UILinksInitializer.OnFancyUIPageMoveAttempt;
			cp21.PageOnLeft = 8;
			cp21.PageOnRight = 0;
			UILinkPointNavigator.RegisterPage(cp21, 1005, true);
			UILinkPage uilinkPage8 = new UILinkPage();
			for (int num31 = 20000; num31 < 21000; num31++)
			{
				uilinkPage8.LinkMap.Add(num31, new UILinkPoint(num31, true, -3, -4, -1, -2));
			}
			uilinkPage8.CanEnterEvent += () => NewCraftingUI.Visible;
			uilinkPage8.IsValidEvent += () => NewCraftingUI.Visible;
			uilinkPage8.OnPageMoveAttempt += UILinksInitializer.OnFancyUIPageMoveAttempt;
			uilinkPage8.PageOnLeft = 8;
			uilinkPage8.PageOnRight = 0;
			UILinkPointNavigator.RegisterPage(uilinkPage8, 24, true);
			UILinkPage cp22 = new UILinkPage();
			cp22.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			Func<string> func19 = () => PlayerInput.BuildCommand(Lang.misc[94].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] });
			for (int num32 = 9000; num32 <= 9050; num32++)
			{
				UILinkPoint uilinkPoint17 = new UILinkPoint(num32, true, num32 + 10, num32 - 10, num32 - 1, num32 + 1);
				cp22.LinkMap.Add(num32, uilinkPoint17);
				uilinkPoint17.OnSpecialInteracts += func19;
			}
			cp22.UpdateEvent += delegate
			{
				int num83 = UILinkPointNavigator.Shortcuts.BUFFS_PER_COLUMN;
				if (num83 == 0)
				{
					num83 = 100;
				}
				for (int num84 = 0; num84 < 50; num84++)
				{
					cp22.LinkMap[9000 + num84].Up = ((num84 % num83 == 0) ? (-1) : (9000 + num84 - 1));
					if (cp22.LinkMap[9000 + num84].Up == -1)
					{
						if (num84 >= num83)
						{
							cp22.LinkMap[9000 + num84].Up = 184;
						}
						else
						{
							cp22.LinkMap[9000 + num84].Up = 189;
						}
					}
					cp22.LinkMap[9000 + num84].Down = (((num84 + 1) % num83 == 0 || num84 == UILinkPointNavigator.Shortcuts.BUFFS_DRAWN - 1) ? 308 : (9000 + num84 + 1));
					cp22.LinkMap[9000 + num84].Left = ((num84 < UILinkPointNavigator.Shortcuts.BUFFS_DRAWN - num83) ? (9000 + num84 + num83) : (-3));
					cp22.LinkMap[9000 + num84].Right = ((num84 < num83) ? (-4) : (9000 + num84 - num83));
				}
			};
			cp22.IsValidEvent += () => Main.playerInventory && Main.EquipPage == 2 && UILinkPointNavigator.Shortcuts.BUFFS_DRAWN > 0;
			cp22.PageOnLeft = 8;
			cp22.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp22, 19, true);
			UILinkPage uilinkPage9 = new UILinkPage();
			uilinkPage9.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			uilinkPage9.OnSpecialInteractsLate += () => ItemSlot.GetGamepadInstructions(35);
			UILinkPoint uilinkPoint18 = new UILinkPoint(12000, true, -3, 11001, -1, -2);
			uilinkPage9.LinkMap.Add(12000, uilinkPoint18);
			uilinkPage9.LinkMap[12000].OnSpecialInteracts += delegate
			{
				string text3 = "";
				if (Main.mouseItem.stack <= 0 || (Main.mouseItem.type == Main.bannerUI.FocusedItemType && Main.mouseItem.stack < Main.mouseItem.maxStack))
				{
					text3 += PlayerInput.BuildCommand(Language.GetTextValue("UI.GamepadClaimBanner"), new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"],
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
					});
				}
				return text3;
			};
			uilinkPage9.ReachEndEvent += delegate(int current, int next)
			{
				bool flag12 = next == -1;
				int num85 = (next == -2).ToInt() - flag12.ToInt();
				Main.bannerUI.NavigatePipsList(num85);
			};
			uilinkPage9.EnterEvent += delegate
			{
				Main.PipsUseGrid = false;
				PlayerInput.LockGamepadButtons("MouseLeft");
			};
			uilinkPage9.CanEnterEvent += () => Main.playerInventory && Main.bannerUI.AnyAvailableBanners;
			uilinkPage9.IsValidEvent += () => Main.playerInventory && Main.bannerUI.AnyAvailableBanners;
			uilinkPage9.PageOnLeft = 23;
			uilinkPage9.PageOnRight = 0;
			UILinkPointNavigator.RegisterPage(uilinkPage9, 22, true);
			UILinkPage cp = new UILinkPage();
			cp.OnSpecialInteracts += () => PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			});
			cp.OnSpecialInteractsLate += () => ItemSlot.GetGamepadInstructions(35);
			UILinkPoint uilinkPoint19 = new UILinkPoint(11100, true, -3, -4, -1, -2);
			cp.LinkMap.Add(11100, uilinkPoint19);
			cp.UpdateEvent += delegate
			{
				int craft_IconsPerRow2 = UILinkPointNavigator.Shortcuts.CRAFT_IconsPerRow;
				int craft_IconsPerColumn2 = UILinkPointNavigator.Shortcuts.CRAFT_IconsPerColumn;
				cp.PageOnLeft = UILinksInitializer.GetCornerWrapPageIdFromLeftToRight();
			};
			cp.ReachEndEvent += delegate(int current, int next)
			{
				bool flag13 = next == -3;
				int num86 = (next == -4).ToInt() - flag13.ToInt();
				bool flag14 = next == -1;
				int num87 = (next == -2).ToInt() - flag14.ToInt();
				Main.bannerUI.NavigatePipsGrid(num86, num87);
			};
			cp.EnterEvent += delegate
			{
				Main.PipsUseGrid = true;
				Main.bannerUI.ResetGridSelection();
			};
			cp.LeaveEvent += delegate
			{
				Main.PipsUseGrid = false;
			};
			cp.CanEnterEvent += () => Main.playerInventory && Main.bannerUI.AnyAvailableBanners;
			cp.IsValidEvent += () => Main.playerInventory && Main.PipsUseGrid && Main.bannerUI.AnyAvailableBanners;
			cp.PageOnLeft = 0;
			cp.PageOnRight = 22;
			UILinkPointNavigator.RegisterPage(cp, 23, true);
			UILinkPage uilinkPage10 = UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage];
			uilinkPage10.CurrentPoint = uilinkPage10.DefaultPoint;
			uilinkPage10.Enter();
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x004D0B60 File Offset: 0x004CED60
		private static bool TryQuickCrafting(int startPoint, int pointOffset)
		{
			Player player = Main.player[Main.myPlayer];
			int num = Main.recStart + pointOffset;
			if (num >= UILinksInitializer.MainnumAvailableRecipes)
			{
				return false;
			}
			bool flag = false;
			int num2 = num - startPoint;
			Recipe recipe = Main.recipe[Main.availableRecipe[num2]];
			if (Main.mouseItem.type == 0 && recipe.createItem.maxStack > 1 && player.ItemSpace(recipe.createItem).CanTakeItemToPersonalInventory && !player.HasLockedInventory())
			{
				flag = true;
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.Grapple)
				{
					UILinksInitializer.SomeVarsForUILinkers.SequencedCraftingCurrent = recipe;
				}
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.Current.Grapple && Main.stackSplit <= 1)
				{
					ItemSlot.RefreshStackSplitCooldown();
					if (UILinksInitializer.SomeVarsForUILinkers.SequencedCraftingCurrent == recipe)
					{
						CraftingRequests.CraftItem(recipe, 1, true);
					}
				}
			}
			return flag;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x004D0C37 File Offset: 0x004CEE37
		private static bool CanEnterCreativeMenu()
		{
			return Main.LocalPlayer.chest == -1 && Main.LocalPlayer.talkNPC == -1 && Main.playerInventory && Main.CreativeMenu.Enabled;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x004D0C6A File Offset: 0x004CEE6A
		private static int GetCornerWrapPageIdFromLeftToRight()
		{
			return 8;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x004D0C70 File Offset: 0x004CEE70
		private static int GetCornerWrapPageIdFromRightToLeft()
		{
			if (Main.CreativeMenu.Enabled)
			{
				return 1005;
			}
			if (NewCraftingUI.Visible)
			{
				return 24;
			}
			if (Main.InPipBanner)
			{
				return 23;
			}
			TileEntity tileEntity = Main.LocalPlayer.tileEntityAnchor.GetTileEntity();
			if (tileEntity is TEDisplayDoll)
			{
				return 20;
			}
			if (tileEntity is TEHatRack)
			{
				return 21;
			}
			return 9;
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x004D0CD4 File Offset: 0x004CEED4
		private static void OnFancyUIPageMoveAttempt(int direction)
		{
			UICharacterCreation uicharacterCreation = Main.MenuUI.CurrentState as UICharacterCreation;
			if (uicharacterCreation != null)
			{
				uicharacterCreation.TryMovingCategory(direction);
			}
			UIBestiaryTest uibestiaryTest = UserInterface.ActiveInstance.CurrentState as UIBestiaryTest;
			if (uibestiaryTest != null)
			{
				uibestiaryTest.TryMovingPages(direction);
			}
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x004D0D18 File Offset: 0x004CEF18
		public static void FancyExit()
		{
			switch (UILinkPointNavigator.Shortcuts.BackButtonCommand)
			{
			case 1:
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				Main.menuMode = 0;
				return;
			case 2:
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				Main.menuMode = (Main.menuMultiplayer ? 12 : 1);
				return;
			case 3:
				Main.menuMode = 0;
				IngameFancyUI.Close(false);
				return;
			case 4:
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				Main.menuMode = 11;
				return;
			case 5:
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				Main.menuMode = 11;
				return;
			case 6:
				Main.LocalPlayer.releaseInventory = false;
				UIVirtualKeyboard.Cancel();
				return;
			case 7:
			{
				IHaveBackButtonCommand haveBackButtonCommand = Main.MenuUI.CurrentState as IHaveBackButtonCommand;
				if (haveBackButtonCommand != null)
				{
					haveBackButtonCommand.HandleBackButtonUsage();
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x004D0E04 File Offset: 0x004CF004
		public static string FancyUISpecialInstructions()
		{
			string text = "";
			int fancyui_SPECIAL_INSTRUCTIONS = UILinkPointNavigator.Shortcuts.FANCYUI_SPECIAL_INSTRUCTIONS;
			if (fancyui_SPECIAL_INSTRUCTIONS == 1)
			{
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.HotbarMinus)
				{
					UIVirtualKeyboard.CycleSymbols();
					PlayerInput.LockGamepadButtons("HotbarMinus");
					PlayerInput.SettingsForUI.TryRevertingToMouseMode();
				}
				text += PlayerInput.BuildCommand(Lang.menu[235].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"] });
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.MouseRight)
				{
					UIVirtualKeyboard.BackSpace();
					PlayerInput.LockGamepadButtons("MouseRight");
					PlayerInput.SettingsForUI.TryRevertingToMouseMode();
				}
				text += PlayerInput.BuildCommand(Lang.menu[236].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"] });
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.SmartCursor)
				{
					UIVirtualKeyboard.Write(" ");
					PlayerInput.LockGamepadButtons("SmartCursor");
					PlayerInput.SettingsForUI.TryRevertingToMouseMode();
				}
				text += PlayerInput.BuildCommand(Lang.menu[238].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["SmartCursor"] });
				if (UIVirtualKeyboard.CanSubmit)
				{
					if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.HotbarPlus)
					{
						UIVirtualKeyboard.Submit();
						PlayerInput.LockGamepadButtons("HotbarPlus");
						PlayerInput.SettingsForUI.TryRevertingToMouseMode();
					}
					text += PlayerInput.BuildCommand(Lang.menu[237].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"] });
				}
			}
			return text;
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x004D0FC8 File Offset: 0x004CF1C8
		public static void HandleOptionsSpecials()
		{
			switch (UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE)
			{
			case 1:
				Main.bgScroll = (int)UILinksInitializer.HandleSliderHorizontalInput((float)Main.bgScroll, 0f, 100f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 1f);
				Main.caveParallax = 1f - (float)Main.bgScroll / 500f;
				return;
			case 2:
				Main.musicVolume = UILinksInitializer.HandleSliderHorizontalInput(Main.musicVolume, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
				return;
			case 3:
				Main.soundVolume = UILinksInitializer.HandleSliderHorizontalInput(Main.soundVolume, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
				return;
			case 4:
				Main.ambientVolume = UILinksInitializer.HandleSliderHorizontalInput(Main.ambientVolume, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
				return;
			case 5:
			{
				float hBar = Main.hBar;
				float num = (Main.hBar = UILinksInitializer.HandleSliderHorizontalInput(hBar, 0f, 1f, 0.2f, 0.5f));
				if (hBar != num)
				{
					int num2 = Main.menuMode;
					switch (num2)
					{
					case 17:
						Main.player[Main.myPlayer].hairColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 18:
						Main.player[Main.myPlayer].eyeColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 19:
						Main.player[Main.myPlayer].skinColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 20:
						break;
					case 21:
						Main.player[Main.myPlayer].shirtColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 22:
						Main.player[Main.myPlayer].underShirtColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 23:
						Main.player[Main.myPlayer].pantsColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 24:
						Main.player[Main.myPlayer].shoeColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 25:
						Main.mouseColorSlider.Hue = num;
						break;
					default:
						if (num2 == 252)
						{
							Main.mouseBorderColorSlider.Hue = num;
						}
						break;
					}
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				break;
			}
			case 6:
			{
				float sBar = Main.sBar;
				float num3 = (Main.sBar = UILinksInitializer.HandleSliderHorizontalInput(sBar, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.5f));
				if (sBar != num3)
				{
					int num2 = Main.menuMode;
					switch (num2)
					{
					case 17:
						Main.player[Main.myPlayer].hairColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 18:
						Main.player[Main.myPlayer].eyeColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 19:
						Main.player[Main.myPlayer].skinColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 20:
						break;
					case 21:
						Main.player[Main.myPlayer].shirtColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 22:
						Main.player[Main.myPlayer].underShirtColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 23:
						Main.player[Main.myPlayer].pantsColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 24:
						Main.player[Main.myPlayer].shoeColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 25:
						Main.mouseColorSlider.Saturation = num3;
						break;
					default:
						if (num2 == 252)
						{
							Main.mouseBorderColorSlider.Saturation = num3;
						}
						break;
					}
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				break;
			}
			case 7:
			{
				float lBar = Main.lBar;
				float num4 = 0.15f;
				if (Main.menuMode == 252)
				{
					num4 = 0f;
				}
				Main.lBar = UILinksInitializer.HandleSliderHorizontalInput(lBar, num4, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.5f);
				float lBar2 = Main.lBar;
				if (lBar != lBar2)
				{
					int num2 = Main.menuMode;
					switch (num2)
					{
					case 17:
						Main.player[Main.myPlayer].hairColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 18:
						Main.player[Main.myPlayer].eyeColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 19:
						Main.player[Main.myPlayer].skinColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 20:
						break;
					case 21:
						Main.player[Main.myPlayer].shirtColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 22:
						Main.player[Main.myPlayer].underShirtColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 23:
						Main.player[Main.myPlayer].pantsColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 24:
						Main.player[Main.myPlayer].shoeColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 25:
						Main.mouseColorSlider.Luminance = lBar2;
						break;
					default:
						if (num2 == 252)
						{
							Main.mouseBorderColorSlider.Luminance = lBar2;
						}
						break;
					}
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				break;
			}
			case 8:
			{
				float aBar = Main.aBar;
				float num5 = (Main.aBar = UILinksInitializer.HandleSliderHorizontalInput(aBar, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.5f));
				if (aBar != num5)
				{
					int num2 = Main.menuMode;
					if (num2 == 252)
					{
						Main.mouseBorderColorSlider.Alpha = num5;
					}
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				break;
			}
			case 9:
			{
				bool left = PlayerInput.Triggers.Current.Left;
				bool right = PlayerInput.Triggers.Current.Right;
				if (PlayerInput.Triggers.JustPressed.Left || PlayerInput.Triggers.JustPressed.Right)
				{
					UILinksInitializer.SomeVarsForUILinkers.HairMoveCD = 0;
				}
				else if (UILinksInitializer.SomeVarsForUILinkers.HairMoveCD > 0)
				{
					UILinksInitializer.SomeVarsForUILinkers.HairMoveCD--;
				}
				if (UILinksInitializer.SomeVarsForUILinkers.HairMoveCD == 0 && (left || right))
				{
					if (left)
					{
						Main.PendingPlayer.hair--;
					}
					if (right)
					{
						Main.PendingPlayer.hair++;
					}
					UILinksInitializer.SomeVarsForUILinkers.HairMoveCD = 12;
				}
				int num6 = 51;
				if (Main.PendingPlayer.hair >= num6)
				{
					Main.PendingPlayer.hair = 0;
				}
				if (Main.PendingPlayer.hair < 0)
				{
					Main.PendingPlayer.hair = num6 - 1;
					return;
				}
				break;
			}
			case 10:
				Main.GameZoomTarget = UILinksInitializer.HandleSliderHorizontalInput(Main.GameZoomTarget, 1f, 2f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
				return;
			case 11:
				Main.UIScale = UILinksInitializer.HandleSliderHorizontalInput(Main.UIScaleWanted, 0.5f, 2f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
				Main.temporaryGUIScaleSlider = Main.UIScaleWanted;
				return;
			case 12:
				Main.MapScale = UILinksInitializer.HandleSliderHorizontalInput(Main.MapScale, 0.5f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.7f);
				break;
			default:
				return;
			}
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x0000357B File Offset: 0x0000177B
		public UILinksInitializer()
		{
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x004D18BB File Offset: 0x004CFABB
		// Note: this type is marked as 'beforefieldinit'.
		static UILinksInitializer()
		{
		}

		// Token: 0x04001115 RID: 4373
		private static List<string> RightStickGlyphBinding = new List<string> { "RightStickAxis" };

		// Token: 0x0200066E RID: 1646
		public class SomeVarsForUILinkers
		{
			// Token: 0x06003DC0 RID: 15808 RVA: 0x0000357B File Offset: 0x0000177B
			public SomeVarsForUILinkers()
			{
			}

			// Token: 0x040066B0 RID: 26288
			public static Recipe SequencedCraftingCurrent;

			// Token: 0x040066B1 RID: 26289
			public static int HairMoveCD;
		}

		// Token: 0x0200066F RID: 1647
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_0
		{
			// Token: 0x06003DC1 RID: 15809 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_0()
			{
			}

			// Token: 0x06003DC2 RID: 15810 RVA: 0x006932B4 File Offset: 0x006914B4
			internal void <Load>b__6()
			{
				if (UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsNew)
				{
					for (int i = 0; i < UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsCount; i++)
					{
						if (i - 4 >= 0)
						{
							this.cp.LinkMap[2500 + i].Up = 2500 + i - 4;
						}
						else
						{
							this.cp.LinkMap[2500 + i].Up = -1;
						}
						if (i + 4 < UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsCount)
						{
							this.cp.LinkMap[2500 + i].Down = 2500 + i + 4;
						}
						else
						{
							this.cp.LinkMap[2500 + i].Down = -2;
						}
						this.cp.LinkMap[2500 + i].Left = ((i > 0) ? (2500 + i - 1) : (-3));
						this.cp.LinkMap[2500 + i].Right = ((i < UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsCount - 1) ? (2500 + i + 1) : (-4));
					}
					return;
				}
				this.cp.LinkMap[2501].Right = (UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight ? 2502 : (-4));
				if (this.cp.LinkMap[2501].Right == -4 && UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight2)
				{
					this.cp.LinkMap[2501].Right = 2503;
				}
				this.cp.LinkMap[2502].Right = (UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight2 ? 2503 : (-4));
				this.cp.LinkMap[2503].Left = (UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight ? 2502 : 2501);
			}

			// Token: 0x040066B2 RID: 26290
			public UILinkPage cp;
		}

		// Token: 0x02000670 RID: 1648
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_1
		{
			// Token: 0x06003DC3 RID: 15811 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_1()
			{
			}

			// Token: 0x06003DC4 RID: 15812 RVA: 0x006934A0 File Offset: 0x006916A0
			internal void <Load>b__15()
			{
				bool inReforgeMenu = Main.InReforgeMenu;
				bool flag = Main.LocalPlayer.chest != -1;
				bool flag2 = Main.npcShop != 0;
				TileEntity tileEntity = Main.LocalPlayer.tileEntityAnchor.GetTileEntity();
				bool flag3 = tileEntity is TEHatRack;
				bool flag4 = tileEntity is TEDisplayDoll;
				if (NewCraftingUI.Visible)
				{
					flag = false;
				}
				for (int i = 40; i <= 49; i++)
				{
					if (inReforgeMenu)
					{
						this.cp.LinkMap[i].Down = ((i < 45) ? 303 : 304);
					}
					else if (flag)
					{
						this.cp.LinkMap[i].Down = 400 + i - 40;
					}
					else if (flag2)
					{
						this.cp.LinkMap[i].Down = 2700 + i - 40;
					}
					else if (i == 40 && Main.IsJourneyMode && !Main.CreativeMenu.Blocked)
					{
						this.cp.LinkMap[i].Down = 311;
					}
					else if (!NewCraftingUI.Visible)
					{
						this.cp.LinkMap[i].Down = -2;
					}
				}
				if (flag4)
				{
					for (int j = 41; j <= 48; j++)
					{
						this.cp.LinkMap[j].Down = 5100 + (int)Math.Round((double)((j - 40) * 10) / 9.0) - 1;
					}
					this.cp.LinkMap[40].Down = 5118;
				}
				if (flag3)
				{
					for (int k = 44; k <= 45; k++)
					{
						this.cp.LinkMap[k].Down = 5000 + k - 44;
					}
				}
				if (NewCraftingUI.Visible && Main.LocalPlayer.chest != -1)
				{
					this.cp.LinkMap[49].Down = 300;
					this.cp.LinkMap[300].Up = 49;
					this.cp.LinkMap[300].Right = 310;
					this.cp.LinkMap[310].Up = 53;
					this.cp.LinkMap[309].Up = 57;
				}
				else if (flag)
				{
					this.cp.LinkMap[300].Up = 439;
					this.cp.LinkMap[300].Right = 310;
					this.cp.LinkMap[300].Left = 309;
					this.cp.LinkMap[310].Up = ((Main.LocalPlayer.chest < -1) ? 505 : 504);
					this.cp.LinkMap[309].Up = ((Main.LocalPlayer.chest < -1) ? 505 : 504);
				}
				else if (flag2)
				{
					this.cp.LinkMap[300].Up = 2739;
					this.cp.LinkMap[300].Right = 310;
					this.cp.LinkMap[300].Left = 309;
					this.cp.LinkMap[310].Up = 53;
					this.cp.LinkMap[309].Up = 57;
				}
				else
				{
					this.cp.LinkMap[49].Down = 300;
					this.cp.LinkMap[300].Up = 49;
					this.cp.LinkMap[300].Right = 301;
					if (!NewCraftingUI.Visible)
					{
						this.cp.LinkMap[300].Left = 302;
					}
					this.cp.LinkMap[309].Up = 302;
					this.cp.LinkMap[310].Up = 301;
				}
				if (!NewCraftingUI.Visible)
				{
					this.cp.LinkMap[311].Right = -1;
					this.cp.LinkMap[311].Down = -1;
					this.cp.LinkMap[300].Down = -1;
				}
				this.cp.LinkMap[0].Left = 9;
				this.cp.LinkMap[10].Left = 54;
				this.cp.LinkMap[20].Left = 55;
				this.cp.LinkMap[30].Left = 56;
				this.cp.LinkMap[40].Left = 57;
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 0)
				{
					this.cp.LinkMap[0].Left = 6000;
				}
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 2)
				{
					this.cp.LinkMap[10].Left = 6002;
				}
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 4)
				{
					this.cp.LinkMap[20].Left = 6004;
				}
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 6)
				{
					this.cp.LinkMap[30].Left = 6006;
				}
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 8)
				{
					this.cp.LinkMap[40].Left = 6008;
				}
				this.cp.PageOnLeft = 9;
				if (Main.InPipBanner)
				{
					this.cp.PageOnLeft = 22;
				}
				if (Main.CreativeMenu.Enabled)
				{
					this.cp.PageOnLeft = 1005;
				}
				if (NewCraftingUI.Visible)
				{
					this.cp.PageOnLeft = 24;
				}
				if (Main.InReforgeMenu)
				{
					this.cp.PageOnLeft = 5;
				}
				if (flag4)
				{
					this.cp.PageOnLeft = 20;
				}
				if (flag3)
				{
					this.cp.PageOnLeft = 21;
				}
			}

			// Token: 0x040066B3 RID: 26291
			public UILinkPage cp;
		}

		// Token: 0x02000671 RID: 1649
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_2
		{
			// Token: 0x06003DC5 RID: 15813 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_2()
			{
			}

			// Token: 0x06003DC6 RID: 15814 RVA: 0x00693B6C File Offset: 0x00691D6C
			internal void <Load>b__19()
			{
				if (Main.npcShop != 0)
				{
					this.cp.LinkMap[53].Down = 310;
					return;
				}
				if (Main.player[Main.myPlayer].chest != -1)
				{
					this.cp.LinkMap[53].Down = (NewCraftingUI.Visible ? 310 : 500);
					return;
				}
				this.cp.LinkMap[53].Down = 301;
			}

			// Token: 0x040066B4 RID: 26292
			public UILinkPage cp;
		}

		// Token: 0x02000672 RID: 1650
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_3
		{
			// Token: 0x06003DC7 RID: 15815 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_3()
			{
			}

			// Token: 0x06003DC8 RID: 15816 RVA: 0x00693BF8 File Offset: 0x00691DF8
			internal void <Load>b__23()
			{
				if (Main.npcShop != 0)
				{
					this.cp.LinkMap[57].Down = 309;
					return;
				}
				if (Main.player[Main.myPlayer].chest != -1)
				{
					this.cp.LinkMap[57].Down = (NewCraftingUI.Visible ? 310 : 500);
					return;
				}
				this.cp.LinkMap[57].Down = 302;
			}

			// Token: 0x040066B5 RID: 26293
			public UILinkPage cp;
		}

		// Token: 0x02000673 RID: 1651
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_4
		{
			// Token: 0x06003DC9 RID: 15817 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_4()
			{
			}

			// Token: 0x06003DCA RID: 15818 RVA: 0x00693C84 File Offset: 0x00691E84
			internal void <Load>b__28()
			{
				int num = 107;
				int amountOfExtraAccessorySlotsToShow = Main.player[Main.myPlayer].GetAmountOfExtraAccessorySlotsToShow();
				for (int i = 0; i < amountOfExtraAccessorySlotsToShow; i++)
				{
					this.cp.LinkMap[num + i].Down = num + i + 1;
					this.cp.LinkMap[num - 100 + 120 + i].Down = num - 100 + 120 + i + 1;
					this.cp.LinkMap[num + 10 + i].Down = num + 10 + i + 1;
				}
				this.cp.LinkMap[num + amountOfExtraAccessorySlotsToShow].Down = 308;
				this.cp.LinkMap[num + 10 + amountOfExtraAccessorySlotsToShow].Down = 308;
				this.cp.LinkMap[num - 100 + 120 + amountOfExtraAccessorySlotsToShow].Down = 308;
				for (int j = 120; j <= 129; j++)
				{
					UILinkPoint uilinkPoint = this.cp.LinkMap[j];
					int num2 = j - 120;
					uilinkPoint.Left = -3;
					if (num2 == 0)
					{
						uilinkPoint.Left = (Main.ShouldPVPDraw ? 1550 : (-3));
					}
					if (num2 == 1)
					{
						uilinkPoint.Left = (Main.ShouldTeamSelectDraw ? 1552 : (-3));
					}
					if (num2 == 2)
					{
						uilinkPoint.Left = (Main.ShouldTeamSelectDraw ? 1556 : (-3));
					}
					if (num2 == 3)
					{
						uilinkPoint.Left = ((UILinkPointNavigator.Shortcuts.INFOACCCOUNT >= 1) ? 1558 : (-3));
					}
					if (num2 == 4)
					{
						uilinkPoint.Left = ((UILinkPointNavigator.Shortcuts.INFOACCCOUNT >= 5) ? 1562 : (-3));
					}
					if (num2 == 5)
					{
						uilinkPoint.Left = ((UILinkPointNavigator.Shortcuts.INFOACCCOUNT >= 9) ? 1566 : (-3));
					}
				}
				this.cp.LinkMap[num - 100 + 120 + amountOfExtraAccessorySlotsToShow].Left = 1557;
				this.cp.LinkMap[num - 100 + 120 + amountOfExtraAccessorySlotsToShow - 1].Left = 1570;
			}

			// Token: 0x040066B6 RID: 26294
			public UILinkPage cp;
		}

		// Token: 0x02000674 RID: 1652
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_5
		{
			// Token: 0x06003DCB RID: 15819 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_5()
			{
			}

			// Token: 0x06003DCC RID: 15820 RVA: 0x00693EA0 File Offset: 0x006920A0
			internal void <Load>b__31()
			{
				if (Main.LocalPlayer.chest < -1)
				{
					this.cp.LinkMap[505].Down = 310;
					return;
				}
				this.cp.LinkMap[505].Down = 504;
			}

			// Token: 0x040066B7 RID: 26295
			public UILinkPage cp;
		}

		// Token: 0x02000675 RID: 1653
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_6
		{
			// Token: 0x06003DCD RID: 15821 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_6()
			{
			}

			// Token: 0x06003DCE RID: 15822 RVA: 0x00693EFC File Offset: 0x006920FC
			internal void <Load>b__45()
			{
				if (Main.reforgeItem.type > 0)
				{
					this.cp.LinkMap[303].Left = (this.cp.LinkMap[303].Right = 304);
					return;
				}
				if (UILinkPointNavigator.OverridePoint == -1 && this.cp.CurrentPoint == 304)
				{
					UILinkPointNavigator.ChangePoint(303);
				}
				this.cp.LinkMap[303].Left = -3;
				this.cp.LinkMap[303].Right = -4;
			}

			// Token: 0x040066B8 RID: 26296
			public UILinkPage cp;
		}

		// Token: 0x02000676 RID: 1654
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_7
		{
			// Token: 0x06003DCF RID: 15823 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_7()
			{
			}

			// Token: 0x06003DD0 RID: 15824 RVA: 0x00693FB4 File Offset: 0x006921B4
			internal void <Load>b__49()
			{
				int num = UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn;
				if (num == 0)
				{
					num = 100;
				}
				for (int i = 0; i < 50; i++)
				{
					this.cp.LinkMap[600 + i].Up = ((i % num == 0) ? (-1) : (600 + i - 1));
					if (this.cp.LinkMap[600 + i].Up == -1)
					{
						if (i >= num * 2)
						{
							this.cp.LinkMap[600 + i].Up = 307;
						}
						else if (i >= num)
						{
							this.cp.LinkMap[600 + i].Up = 306;
						}
						else
						{
							this.cp.LinkMap[600 + i].Up = 305;
						}
					}
					this.cp.LinkMap[600 + i].Down = (((i + 1) % num == 0 || i == UILinkPointNavigator.Shortcuts.NPCS_IconsTotal - 1) ? 308 : (600 + i + 1));
					this.cp.LinkMap[600 + i].Left = ((i < UILinkPointNavigator.Shortcuts.NPCS_IconsTotal - num) ? (600 + i + num) : (-3));
					this.cp.LinkMap[600 + i].Right = ((i < num) ? (-4) : (600 + i - num));
				}
			}

			// Token: 0x040066B9 RID: 26297
			public UILinkPage cp;
		}

		// Token: 0x02000677 RID: 1655
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_8
		{
			// Token: 0x06003DD1 RID: 15825 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_8()
			{
			}

			// Token: 0x06003DD2 RID: 15826 RVA: 0x0069413C File Offset: 0x0069233C
			internal void <Load>b__58()
			{
				this.cp.LinkMap[184].Down = ((UILinkPointNavigator.Shortcuts.BUFFS_DRAWN > 0) ? 9000 : 308);
				this.cp.LinkMap[189].Down = ((UILinkPointNavigator.Shortcuts.BUFFS_DRAWN > 0) ? 9000 : 308);
			}

			// Token: 0x040066BA RID: 26298
			public UILinkPage cp;
		}

		// Token: 0x02000678 RID: 1656
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_9
		{
			// Token: 0x06003DD3 RID: 15827 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_9()
			{
			}

			// Token: 0x06003DD4 RID: 15828 RVA: 0x006941A8 File Offset: 0x006923A8
			internal void <Load>b__61()
			{
				switch (Main.EquipPage)
				{
				case 0:
					this.cp.LinkMap[305].Down = 100;
					this.cp.LinkMap[306].Down = 110;
					this.cp.LinkMap[307].Down = 120;
					this.cp.LinkMap[308].Up = 108 + Main.player[Main.myPlayer].GetAmountOfExtraAccessorySlotsToShow() - 1;
					break;
				case 1:
				{
					this.cp.LinkMap[305].Down = 600;
					this.cp.LinkMap[306].Down = ((UILinkPointNavigator.Shortcuts.NPCS_IconsTotal > UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn) ? (600 + UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn) : 600);
					this.cp.LinkMap[307].Down = ((UILinkPointNavigator.Shortcuts.NPCS_IconsTotal > UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn * 2) ? (600 + UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn * 2) : this.cp.LinkMap[306].Down);
					int num = UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn;
					if (num == 0)
					{
						num = 100;
					}
					if (num == 100)
					{
						num = UILinkPointNavigator.Shortcuts.NPCS_IconsTotal;
					}
					this.cp.LinkMap[308].Up = 600 + num - 1;
					break;
				}
				case 2:
					this.cp.LinkMap[305].Down = 180;
					this.cp.LinkMap[306].Down = 185;
					this.cp.LinkMap[307].Down = -2;
					this.cp.LinkMap[308].Up = ((UILinkPointNavigator.Shortcuts.BUFFS_DRAWN > 0) ? 9000 : 184);
					break;
				}
				this.cp.PageOnRight = UILinksInitializer.GetCornerWrapPageIdFromRightToLeft();
			}

			// Token: 0x040066BB RID: 26299
			public UILinkPage cp;
		}

		// Token: 0x02000679 RID: 1657
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_10
		{
			// Token: 0x06003DD5 RID: 15829 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_10()
			{
			}

			// Token: 0x06003DD6 RID: 15830 RVA: 0x006943E0 File Offset: 0x006925E0
			internal void <Load>b__67()
			{
				this.cp.PageOnLeft = ((Player.Settings.CraftingGridControl == Player.Settings.CraftingGridMode.Classic) ? 10 : 8);
				int num = UILinkPointNavigator.Shortcuts.CRAFT_CurrentIngredientsCount;
				int num2 = num;
				if (UILinksInitializer.MainnumAvailableRecipes > 0)
				{
					num2 += 2;
				}
				if (num < num2)
				{
					num = num2;
				}
				if (UILinkPointNavigator.OverridePoint == -1)
				{
					if (this.cp.CurrentPoint == 11003)
					{
						if (Main.InGuideCraftMenu)
						{
							UILinkPointNavigator.ChangePoint(1501);
						}
					}
					else if (this.cp.CurrentPoint != 11001)
					{
						if (this.cp.CurrentPoint == 11002)
						{
							if (!Main.bannerUI.AnyAvailableBanners || Main.InGuideCraftMenu)
							{
								UILinkPointNavigator.ChangePoint(11001);
							}
						}
						else if (this.cp.CurrentPoint == 1500)
						{
							if (!Main.InGuideCraftMenu)
							{
								UILinkPointNavigator.ChangePoint(1501);
							}
						}
						else if (this.cp.CurrentPoint > 1500 + num)
						{
							UILinkPointNavigator.ChangePoint(1500);
						}
					}
				}
				bool flag = Main.LocalPlayer.chest != -1;
				for (int i = 1; i < num; i++)
				{
					this.cp.LinkMap[1500 + i].Left = 1500 + i - 1;
					this.cp.LinkMap[1500 + i].Right = ((i == num - 2) ? (-4) : (1500 + i + 1));
					if (i >= 2)
					{
						this.cp.LinkMap[1500 + i].Up = (Main.InGuideCraftMenu ? 1500 : (flag ? 11003 : (-1)));
						this.cp.LinkMap[1500 + i].Down = (flag ? (-1) : ((i >= 3 && Main.bannerUI.AnyAvailableBanners) ? 11002 : 11001));
					}
				}
				this.cp.LinkMap[1501].Left = -3;
				if (num > 0)
				{
					this.cp.LinkMap[1500 + num - 1].Right = -4;
				}
				this.cp.LinkMap[1500].Down = ((num >= 2) ? 1502 : (-2));
				this.cp.LinkMap[1500].Left = ((num >= 1) ? 1501 : (-3));
				this.cp.LinkMap[1500].Up = 11001;
				this.cp.LinkMap[11001].Left = (Main.InPipCrafting ? 1501 : 12000);
				this.cp.LinkMap[11001].Down = ((!Main.InPipCrafting) ? (-1) : (Main.InGuideCraftMenu ? 1500 : 11003));
				this.cp.LinkMap[11001].Right = ((!Main.bannerUI.AnyAvailableBanners || Main.InGuideCraftMenu) ? (-1) : 11002);
				this.cp.LinkMap[11001].Up = (flag ? (-1) : 1502);
				this.cp.LinkMap[11002].Down = ((!Main.InPipCrafting) ? (-1) : 11003);
				this.cp.LinkMap[11002].Up = (flag ? (-1) : ((num >= 5) ? 1503 : 1502));
				this.cp.LinkMap[11003].Down = (flag ? 1502 : (-1));
			}

			// Token: 0x040066BC RID: 26300
			public UILinkPage cp;
		}

		// Token: 0x0200067A RID: 1658
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_11
		{
			// Token: 0x06003DD7 RID: 15831 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_11()
			{
			}

			// Token: 0x06003DD8 RID: 15832 RVA: 0x006947B4 File Offset: 0x006929B4
			internal void <Load>b__75()
			{
				int num = UILinkPointNavigator.Shortcuts.CRAFT_IconsPerRow;
				int craft_IconsPerColumn = UILinkPointNavigator.Shortcuts.CRAFT_IconsPerColumn;
				if (num == 0)
				{
					num = 100;
				}
				int num2 = num * craft_IconsPerColumn;
				if (num2 > 8000)
				{
					num2 = 8000;
				}
				if (num2 > UILinksInitializer.MainnumAvailableRecipes)
				{
					num2 = UILinksInitializer.MainnumAvailableRecipes;
				}
				for (int i = 0; i < num2; i++)
				{
					this.cp.LinkMap[22000 + i].Left = ((i % num == 0) ? (-3) : (22000 + i - 1));
					this.cp.LinkMap[22000 + i].Right = (((i + 1) % num == 0 || i == UILinksInitializer.MainnumAvailableRecipes - 1) ? (-4) : (22000 + i + 1));
					this.cp.LinkMap[22000 + i].Down = ((i < num2 - num) ? (22000 + i + num) : (-2));
					this.cp.LinkMap[22000 + i].Up = ((i < num) ? (-1) : (22000 + i - num));
				}
				this.cp.PageOnLeft = UILinksInitializer.GetCornerWrapPageIdFromLeftToRight();
			}

			// Token: 0x040066BD RID: 26301
			public UILinkPage cp;
		}

		// Token: 0x0200067B RID: 1659
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_12
		{
			// Token: 0x06003DD9 RID: 15833 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_12()
			{
			}

			// Token: 0x06003DDA RID: 15834 RVA: 0x006948D8 File Offset: 0x00692AD8
			internal string <Load>b__81()
			{
				string text = PlayerInput.BuildCommand(Lang.misc[73].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] });
				if (UILinksInitializer.TryQuickCrafting(22000, this.IHateLambda))
				{
					text += PlayerInput.BuildCommand(Lang.misc[71].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"] });
				}
				return text;
			}

			// Token: 0x040066BE RID: 26302
			public int IHateLambda;
		}

		// Token: 0x0200067C RID: 1660
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_13
		{
			// Token: 0x06003DDB RID: 15835 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_13()
			{
			}

			// Token: 0x06003DDC RID: 15836 RVA: 0x00694960 File Offset: 0x00692B60
			internal void <Load>b__83()
			{
				int num = 5;
				int num2 = 3;
				int num3 = num * num2;
				int count = Main.Hairstyles.AvailableHairstyles.Count;
				for (int i = 0; i < num3; i++)
				{
					this.cp.LinkMap[2605 + i].Left = ((i % num == 0) ? (-3) : (2605 + i - 1));
					this.cp.LinkMap[2605 + i].Right = (((i + 1) % num == 0 || i == count - 1) ? (-4) : (2605 + i + 1));
					this.cp.LinkMap[2605 + i].Down = ((i < num3 - num) ? (2605 + i + num) : (-2));
					this.cp.LinkMap[2605 + i].Up = ((i < num) ? (-1) : (2605 + i - num));
				}
			}

			// Token: 0x040066BF RID: 26303
			public UILinkPage cp;
		}

		// Token: 0x0200067D RID: 1661
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_14
		{
			// Token: 0x06003DDD RID: 15837 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_14()
			{
			}

			// Token: 0x06003DDE RID: 15838 RVA: 0x00694A68 File Offset: 0x00692C68
			internal void <Load>b__93()
			{
				if (UILinkPointNavigator.CurrentPage == this.cp.ID)
				{
					int num = this.cp.CurrentPoint - 2900;
					if (num < 5)
					{
						IngameOptions.category = num;
					}
				}
			}

			// Token: 0x06003DDF RID: 15839 RVA: 0x00694AA4 File Offset: 0x00692CA4
			internal void <Load>b__94()
			{
				int num = UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_LEFT;
				if (num == 0)
				{
					num = 5;
				}
				if (UILinkPointNavigator.OverridePoint == -1 && this.cp.CurrentPoint < 2930 && this.cp.CurrentPoint > 2900 + num - 1)
				{
					UILinkPointNavigator.ChangePoint(2900);
				}
				for (int i = 2900; i < 2900 + num; i++)
				{
					this.cp.LinkMap[i].Up = i - 1;
					this.cp.LinkMap[i].Down = i + 1;
				}
				this.cp.LinkMap[2900].Up = 2900 + num - 1;
				this.cp.LinkMap[2900 + num - 1].Down = 2900;
				int num2 = this.cp.CurrentPoint - 2900;
				if (num2 < 4 && UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.MouseLeft)
				{
					IngameOptions.category = num2;
					UILinkPointNavigator.ChangePage(1002);
				}
				int num3 = ((SocialAPI.Network != null && SocialAPI.Network.CanInvite()) ? 1 : 0);
				if (num2 == 4 + num3 && UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.MouseLeft)
				{
					UILinkPointNavigator.ChangePage(1004);
				}
			}

			// Token: 0x06003DE0 RID: 15840 RVA: 0x00694C05 File Offset: 0x00692E05
			internal void <Load>b__95()
			{
				this.cp.CurrentPoint = 2900 + IngameOptions.category;
			}

			// Token: 0x040066C0 RID: 26304
			public UILinkPage cp;
		}

		// Token: 0x0200067E RID: 1662
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_15
		{
			// Token: 0x06003DE1 RID: 15841 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_15()
			{
			}

			// Token: 0x06003DE2 RID: 15842 RVA: 0x00694C20 File Offset: 0x00692E20
			internal void <Load>b__100()
			{
				int num = UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_RIGHT;
				if (num == 0)
				{
					num = 5;
				}
				if (UILinkPointNavigator.OverridePoint == -1 && this.cp.CurrentPoint >= 2930 && this.cp.CurrentPoint > 2930 + num - 1)
				{
					UILinkPointNavigator.ChangePoint(2930);
				}
				for (int i = 2930; i < 2930 + num; i++)
				{
					this.cp.LinkMap[i].Up = i - 1;
					this.cp.LinkMap[i].Down = i + 1;
				}
				this.cp.LinkMap[2930].Up = -1;
				this.cp.LinkMap[2930 + num - 1].Down = -2;
				UILinksInitializer.HandleOptionsSpecials();
			}

			// Token: 0x040066C1 RID: 26305
			public UILinkPage cp;
		}

		// Token: 0x0200067F RID: 1663
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_16
		{
			// Token: 0x06003DE3 RID: 15843 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_16()
			{
			}

			// Token: 0x06003DE4 RID: 15844 RVA: 0x00694CFC File Offset: 0x00692EFC
			internal void <Load>b__104()
			{
				this.cp.LinkMap[1551].Up = (Main.ShouldPVPDraw ? 1550 : (-1));
				this.cp.LinkMap[1552].Up = (Main.ShouldPVPDraw ? 1550 : (-1));
				this.cp.LinkMap[1570].Up = (Main.ShouldTeamSelectDraw ? 1555 : (-1));
				int infoacccount = UILinkPointNavigator.Shortcuts.INFOACCCOUNT;
				if (infoacccount > 0)
				{
					this.cp.LinkMap[1570].Up = 1558 + (infoacccount - 1) / 2 * 2;
				}
				if (Main.ShouldTeamSelectDraw)
				{
					if (infoacccount >= 1)
					{
						this.cp.LinkMap[1555].Down = 1558;
						this.cp.LinkMap[1556].Down = 1558;
					}
					else
					{
						this.cp.LinkMap[1555].Down = 1570;
						this.cp.LinkMap[1556].Down = 1570;
					}
					if (infoacccount >= 2)
					{
						this.cp.LinkMap[1556].Down = 1559;
						return;
					}
					this.cp.LinkMap[1556].Down = 1570;
				}
			}

			// Token: 0x040066C2 RID: 26306
			public UILinkPage cp;
		}

		// Token: 0x02000680 RID: 1664
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_17
		{
			// Token: 0x06003DE5 RID: 15845 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_17()
			{
			}

			// Token: 0x06003DE6 RID: 15846 RVA: 0x00694E84 File Offset: 0x00693084
			internal void <Load>b__107()
			{
				int infoacccount = UILinkPointNavigator.Shortcuts.INFOACCCOUNT;
				if (UILinkPointNavigator.OverridePoint == -1 && this.cp.CurrentPoint - 1558 >= infoacccount)
				{
					UILinkPointNavigator.ChangePoint(1558 + infoacccount - 1);
				}
				for (int i = 0; i < infoacccount; i++)
				{
					bool flag = i % 2 == 0;
					int num = i + 1558;
					this.cp.LinkMap[num].Down = ((i < infoacccount - 2) ? (num + 2) : 1570);
					this.cp.LinkMap[num].Up = ((i > 1) ? (num - 2) : (Main.ShouldTeamSelectDraw ? (flag ? 1555 : 1556) : (-1)));
					this.cp.LinkMap[num].Right = ((flag && i + 1 < infoacccount) ? (num + 1) : (123 + i / 4));
					this.cp.LinkMap[num].Left = (flag ? (-3) : (num - 1));
				}
			}

			// Token: 0x040066C3 RID: 26307
			public UILinkPage cp;
		}

		// Token: 0x02000681 RID: 1665
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_18
		{
			// Token: 0x06003DE7 RID: 15847 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_18()
			{
			}

			// Token: 0x06003DE8 RID: 15848 RVA: 0x00694F8C File Offset: 0x0069318C
			internal void <Load>b__110()
			{
				int builderacccount = UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT;
				if (UILinkPointNavigator.OverridePoint == -1 && this.cp.CurrentPoint - 6000 >= builderacccount)
				{
					UILinkPointNavigator.ChangePoint(6000 + builderacccount - 1);
				}
				for (int i = 0; i < builderacccount; i++)
				{
					int num = i % 2;
					int num2 = i + 6000;
					this.cp.LinkMap[num2].Down = ((i < builderacccount - 1) ? (num2 + 1) : (-2));
					this.cp.LinkMap[num2].Up = ((i > 0) ? (num2 - 1) : (-1));
				}
			}

			// Token: 0x040066C4 RID: 26308
			public UILinkPage cp;
		}

		// Token: 0x02000682 RID: 1666
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_19
		{
			// Token: 0x06003DE9 RID: 15849 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_19()
			{
			}

			// Token: 0x06003DEA RID: 15850 RVA: 0x00695024 File Offset: 0x00693224
			internal void <Load>b__126()
			{
				this.cp.CurrentPoint = 3002;
			}

			// Token: 0x040066C5 RID: 26309
			public UILinkPage cp;
		}

		// Token: 0x02000683 RID: 1667
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_20
		{
			// Token: 0x06003DEB RID: 15851 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_20()
			{
			}

			// Token: 0x06003DEC RID: 15852 RVA: 0x00695036 File Offset: 0x00693236
			internal void <Load>b__132()
			{
				this.cp.CurrentPoint = 10000;
			}

			// Token: 0x040066C6 RID: 26310
			public UILinkPage cp;
		}

		// Token: 0x02000684 RID: 1668
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_21
		{
			// Token: 0x06003DED RID: 15853 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_21()
			{
			}

			// Token: 0x06003DEE RID: 15854 RVA: 0x00695048 File Offset: 0x00693248
			internal void <Load>b__137()
			{
				int num = UILinkPointNavigator.Shortcuts.BUFFS_PER_COLUMN;
				if (num == 0)
				{
					num = 100;
				}
				for (int i = 0; i < 50; i++)
				{
					this.cp.LinkMap[9000 + i].Up = ((i % num == 0) ? (-1) : (9000 + i - 1));
					if (this.cp.LinkMap[9000 + i].Up == -1)
					{
						if (i >= num)
						{
							this.cp.LinkMap[9000 + i].Up = 184;
						}
						else
						{
							this.cp.LinkMap[9000 + i].Up = 189;
						}
					}
					this.cp.LinkMap[9000 + i].Down = (((i + 1) % num == 0 || i == UILinkPointNavigator.Shortcuts.BUFFS_DRAWN - 1) ? 308 : (9000 + i + 1));
					this.cp.LinkMap[9000 + i].Left = ((i < UILinkPointNavigator.Shortcuts.BUFFS_DRAWN - num) ? (9000 + i + num) : (-3));
					this.cp.LinkMap[9000 + i].Right = ((i < num) ? (-4) : (9000 + i - num));
				}
			}

			// Token: 0x040066C7 RID: 26311
			public UILinkPage cp;
		}

		// Token: 0x02000685 RID: 1669
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_22
		{
			// Token: 0x06003DEF RID: 15855 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_22()
			{
			}

			// Token: 0x06003DF0 RID: 15856 RVA: 0x006951A5 File Offset: 0x006933A5
			internal void <Load>b__148()
			{
				int craft_IconsPerRow = UILinkPointNavigator.Shortcuts.CRAFT_IconsPerRow;
				int craft_IconsPerColumn = UILinkPointNavigator.Shortcuts.CRAFT_IconsPerColumn;
				this.cp.PageOnLeft = UILinksInitializer.GetCornerWrapPageIdFromLeftToRight();
			}

			// Token: 0x040066C8 RID: 26312
			public UILinkPage cp;
		}

		// Token: 0x02000686 RID: 1670
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003DF1 RID: 15857 RVA: 0x006951C3 File Offset: 0x006933C3
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003DF2 RID: 15858 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003DF3 RID: 15859 RVA: 0x006951CF File Offset: 0x006933CF
			internal string <Load>b__18_0()
			{
				return PlayerInput.BuildCommand(Lang.misc[53].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] });
			}

			// Token: 0x06003DF4 RID: 15860 RVA: 0x00695200 File Offset: 0x00693400
			internal void <Load>b__18_1()
			{
				PlayerInput.GamepadAllowScrolling = true;
			}

			// Token: 0x06003DF5 RID: 15861 RVA: 0x00695208 File Offset: 0x00693408
			internal string <Load>b__18_2()
			{
				return PlayerInput.BuildCommand(Lang.misc[53].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] }) + PlayerInput.BuildCommand(Lang.misc[82].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] });
			}

			// Token: 0x06003DF6 RID: 15862 RVA: 0x00695278 File Offset: 0x00693478
			internal void <Load>b__18_3()
			{
				bool flag = PlayerInput.Triggers.JustPressed.Inventory;
				if (Main.inputTextEscape)
				{
					Main.inputTextEscape = false;
					flag = true;
				}
				if (UILinksInitializer.CanExecuteInputCommand() && flag)
				{
					UILinksInitializer.FancyExit();
				}
				UILinkPointNavigator.Shortcuts.BackButtonInUse = flag;
				UILinksInitializer.HandleOptionsSpecials();
			}

			// Token: 0x06003DF7 RID: 15863 RVA: 0x006952BD File Offset: 0x006934BD
			internal bool <Load>b__18_4()
			{
				return Main.gameMenu && !Main.MenuUI.IsVisible;
			}

			// Token: 0x06003DF8 RID: 15864 RVA: 0x006952BD File Offset: 0x006934BD
			internal bool <Load>b__18_5()
			{
				return Main.gameMenu && !Main.MenuUI.IsVisible;
			}

			// Token: 0x06003DF9 RID: 15865 RVA: 0x006952D8 File Offset: 0x006934D8
			internal string <Load>b__18_7()
			{
				return PlayerInput.BuildCommand(Lang.misc[53].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] }) + PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] });
			}

			// Token: 0x06003DFA RID: 15866 RVA: 0x00695348 File Offset: 0x00693548
			internal bool <Load>b__18_8()
			{
				return (Main.player[Main.myPlayer].talkNPC != -1 || Main.player[Main.myPlayer].sign != -1) && UILinksInitializer.NothingMoreImportantThanNPCChat();
			}

			// Token: 0x06003DFB RID: 15867 RVA: 0x00695348 File Offset: 0x00693548
			internal bool <Load>b__18_9()
			{
				return (Main.player[Main.myPlayer].talkNPC != -1 || Main.player[Main.myPlayer].sign != -1) && UILinksInitializer.NothingMoreImportantThanNPCChat();
			}

			// Token: 0x06003DFC RID: 15868 RVA: 0x00695377 File Offset: 0x00693577
			internal void <Load>b__18_10()
			{
				Main.player[Main.myPlayer].releaseInventory = false;
			}

			// Token: 0x06003DFD RID: 15869 RVA: 0x0069538A File Offset: 0x0069358A
			internal void <Load>b__18_11()
			{
				Main.npcChatRelease = false;
				Main.player[Main.myPlayer].LockGamepadTileInteractions();
			}

			// Token: 0x06003DFE RID: 15870 RVA: 0x006953A4 File Offset: 0x006935A4
			internal string <Load>b__18_12()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003DFF RID: 15871 RVA: 0x0069542C File Offset: 0x0069362C
			internal string <Load>b__18_13()
			{
				int currentPoint = UILinkPointNavigator.CurrentPoint;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].inventory, 0, currentPoint);
			}

			// Token: 0x06003E00 RID: 15872 RVA: 0x00695456 File Offset: 0x00693656
			internal string <Load>b__18_14()
			{
				return ItemSlot.GetGamepadInstructions(ref Main.player[Main.myPlayer].trashItem, 6);
			}

			// Token: 0x06003E01 RID: 15873 RVA: 0x0069546E File Offset: 0x0069366E
			internal bool <Load>b__18_16()
			{
				return Main.playerInventory;
			}

			// Token: 0x06003E02 RID: 15874 RVA: 0x00695478 File Offset: 0x00693678
			internal string <Load>b__18_17()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E03 RID: 15875 RVA: 0x00695500 File Offset: 0x00693700
			internal string <Load>b__18_18()
			{
				int currentPoint = UILinkPointNavigator.CurrentPoint;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].inventory, 1, currentPoint);
			}

			// Token: 0x06003E04 RID: 15876 RVA: 0x0069546E File Offset: 0x0069366E
			internal bool <Load>b__18_20()
			{
				return Main.playerInventory;
			}

			// Token: 0x06003E05 RID: 15877 RVA: 0x0069552C File Offset: 0x0069372C
			internal string <Load>b__18_21()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E06 RID: 15878 RVA: 0x006955B4 File Offset: 0x006937B4
			internal string <Load>b__18_22()
			{
				int currentPoint = UILinkPointNavigator.CurrentPoint;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].inventory, 2, currentPoint);
			}

			// Token: 0x06003E07 RID: 15879 RVA: 0x006955E0 File Offset: 0x006937E0
			internal string <Load>b__18_24()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E08 RID: 15880 RVA: 0x00695668 File Offset: 0x00693868
			internal string <Load>b__18_25()
			{
				int num = UILinkPointNavigator.CurrentPoint - 100;
				if (num % 10 == 8 && !Main.LocalPlayer.CanDemonHeartAccessoryBeShown())
				{
					num++;
				}
				bool flag = num >= 10;
				int num2 = ((num % 10 < 3) ? (flag ? 9 : 8) : (flag ? 11 : 10));
				return ItemSlot.GetGamepadInstructions(Main.LocalPlayer.armor, num2, num);
			}

			// Token: 0x06003E09 RID: 15881 RVA: 0x006956CC File Offset: 0x006938CC
			internal string <Load>b__18_26()
			{
				int num = UILinkPointNavigator.CurrentPoint - 120;
				if (num % 10 == 8 && !Main.LocalPlayer.CanDemonHeartAccessoryBeShown())
				{
					num++;
				}
				return ItemSlot.GetGamepadInstructions(Main.LocalPlayer.dye, 12, num);
			}

			// Token: 0x06003E0A RID: 15882 RVA: 0x0069570D File Offset: 0x0069390D
			internal bool <Load>b__18_27()
			{
				return Main.playerInventory && Main.EquipPage == 0;
			}

			// Token: 0x06003E0B RID: 15883 RVA: 0x00695720 File Offset: 0x00693920
			internal string <Load>b__18_29()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E0C RID: 15884 RVA: 0x006957A8 File Offset: 0x006939A8
			internal string <Load>b__18_30()
			{
				int num = UILinkPointNavigator.CurrentPoint - 400;
				int num2 = 4;
				Item[] array = Main.player[Main.myPlayer].bank.item;
				switch (Main.player[Main.myPlayer].chest)
				{
				case -5:
					array = Main.player[Main.myPlayer].bank4.item;
					num2 = 32;
					break;
				case -4:
					array = Main.player[Main.myPlayer].bank3.item;
					break;
				case -3:
					array = Main.player[Main.myPlayer].bank2.item;
					break;
				case -2:
					break;
				case -1:
					return "";
				default:
					array = Main.chest[Main.player[Main.myPlayer].chest].item;
					num2 = 3;
					break;
				}
				return ItemSlot.GetGamepadInstructions(array, num2, num);
			}

			// Token: 0x06003E0D RID: 15885 RVA: 0x00695880 File Offset: 0x00693A80
			internal bool <Load>b__18_32()
			{
				return Main.playerInventory && Main.player[Main.myPlayer].chest != -1 && !NewCraftingUI.Visible;
			}

			// Token: 0x06003E0E RID: 15886 RVA: 0x006958A8 File Offset: 0x00693AA8
			internal string <Load>b__18_33()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E0F RID: 15887 RVA: 0x00695930 File Offset: 0x00693B30
			internal string <Load>b__18_34()
			{
				int num = UILinkPointNavigator.CurrentPoint - 5100;
				TEDisplayDoll tedisplayDoll = Main.LocalPlayer.tileEntityAnchor.GetTileEntity() as TEDisplayDoll;
				if (tedisplayDoll == null)
				{
					return "";
				}
				return tedisplayDoll.GetItemGamepadInstructions(num);
			}

			// Token: 0x06003E10 RID: 15888 RVA: 0x0069596E File Offset: 0x00693B6E
			internal bool <Load>b__18_35()
			{
				return Main.playerInventory && Main.LocalPlayer.tileEntityAnchor.GetTileEntity() is TEDisplayDoll;
			}

			// Token: 0x06003E11 RID: 15889 RVA: 0x00695990 File Offset: 0x00693B90
			internal string <Load>b__18_36()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E12 RID: 15890 RVA: 0x00695A18 File Offset: 0x00693C18
			internal string <Load>b__18_37()
			{
				int num = UILinkPointNavigator.CurrentPoint - 5000;
				TEHatRack tehatRack = Main.LocalPlayer.tileEntityAnchor.GetTileEntity() as TEHatRack;
				if (tehatRack == null)
				{
					return "";
				}
				return tehatRack.GetItemGamepadInstructions(num);
			}

			// Token: 0x06003E13 RID: 15891 RVA: 0x00695A56 File Offset: 0x00693C56
			internal bool <Load>b__18_38()
			{
				return Main.playerInventory && Main.LocalPlayer.tileEntityAnchor.GetTileEntity() is TEHatRack;
			}

			// Token: 0x06003E14 RID: 15892 RVA: 0x00695A78 File Offset: 0x00693C78
			internal string <Load>b__18_39()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E15 RID: 15893 RVA: 0x00695B00 File Offset: 0x00693D00
			internal string <Load>b__18_40()
			{
				if (Main.npcShop == 0)
				{
					return "";
				}
				int num = UILinkPointNavigator.CurrentPoint - 2700;
				return ItemSlot.GetGamepadInstructions(Main.instance.shop[Main.npcShop].item, 15, num);
			}

			// Token: 0x06003E16 RID: 15894 RVA: 0x00695B43 File Offset: 0x00693D43
			internal bool <Load>b__18_41()
			{
				return Main.playerInventory && Main.npcShop != 0;
			}

			// Token: 0x06003E17 RID: 15895 RVA: 0x00695B58 File Offset: 0x00693D58
			internal string <Load>b__18_42()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E18 RID: 15896 RVA: 0x00695BDF File Offset: 0x00693DDF
			internal string <Load>b__18_43()
			{
				return ItemSlot.GetGamepadInstructions(ref Main.reforgeItem, 5);
			}

			// Token: 0x06003E19 RID: 15897 RVA: 0x00695BEC File Offset: 0x00693DEC
			internal string <Load>b__18_44()
			{
				return Lang.misc[53].Value;
			}

			// Token: 0x06003E1A RID: 15898 RVA: 0x00695BFB File Offset: 0x00693DFB
			internal bool <Load>b__18_46()
			{
				return Main.playerInventory && Main.InReforgeMenu;
			}

			// Token: 0x06003E1B RID: 15899 RVA: 0x00695C0B File Offset: 0x00693E0B
			internal void <Load>b__18_47()
			{
				PlayerInput.LockGamepadButtons("MouseLeft");
			}

			// Token: 0x06003E1C RID: 15900 RVA: 0x00695C18 File Offset: 0x00693E18
			internal string <Load>b__18_48()
			{
				string text = PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
				if (PlayerInput.ControllerHousingCursorActive)
				{
					bool flag = UILinkPointNavigator.CurrentPoint == 600;
					bool flag2 = UILinkPointNavigator.Shortcuts.NPCS_HoveredBanner >= 0;
					if (flag2)
					{
						string fullName = Main.npc[UILinkPointNavigator.Shortcuts.NPCS_HoveredBanner].FullName;
						text += PlayerInput.BuildCommand(Language.GetTextValue("UI.HousingEvict", fullName), new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"] });
					}
					else if (flag)
					{
						text += PlayerInput.BuildCommand(Lang.misc[70].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"] });
					}
					else if (UILinkPointNavigator.Shortcuts.NPCS_SelectedNPC >= 0)
					{
						string fullName2 = Main.npc[UILinkPointNavigator.Shortcuts.NPCS_SelectedNPC].FullName;
						text += PlayerInput.BuildCommand(Language.GetTextValue("UI.HousingAssign", fullName2), new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"] });
					}
					if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.Grapple)
					{
						Point point = PlayerInput.HousingWorldPosition.ToTileCoordinates();
						if (flag2)
						{
							WorldGen.kickOut(UILinkPointNavigator.Shortcuts.NPCS_HoveredBanner);
							SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
						}
						else if (flag)
						{
							Main.instance.PerformHousingCheck(point.X, point.Y);
						}
						else if (UILinkPointNavigator.Shortcuts.NPCS_SelectedNPC >= 0)
						{
							Main.instance.TryMovingNPC(point.X, point.Y, UILinkPointNavigator.Shortcuts.NPCS_SelectedNPC);
						}
						PlayerInput.LockGamepadButtons("Grapple");
						PlayerInput.SettingsForUI.TryRevertingToMouseMode();
					}
					text += PlayerInput.BuildCommand(Language.GetTextValue("UI.HousingAim"), new List<string>[] { UILinksInitializer.RightStickGlyphBinding });
				}
				return text;
			}

			// Token: 0x06003E1D RID: 15901 RVA: 0x00695E5E File Offset: 0x0069405E
			internal bool <Load>b__18_50()
			{
				return Main.playerInventory && Main.EquipPage == 1;
			}

			// Token: 0x06003E1E RID: 15902 RVA: 0x00695E74 File Offset: 0x00694074
			internal string <Load>b__18_51()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E1F RID: 15903 RVA: 0x00695EFC File Offset: 0x006940FC
			internal string <Load>b__18_52()
			{
				int num = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 20, num);
			}

			// Token: 0x06003E20 RID: 15904 RVA: 0x00695F30 File Offset: 0x00694130
			internal string <Load>b__18_53()
			{
				int num = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 19, num);
			}

			// Token: 0x06003E21 RID: 15905 RVA: 0x00695F64 File Offset: 0x00694164
			internal string <Load>b__18_54()
			{
				int num = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 18, num);
			}

			// Token: 0x06003E22 RID: 15906 RVA: 0x00695F98 File Offset: 0x00694198
			internal string <Load>b__18_55()
			{
				int num = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 17, num);
			}

			// Token: 0x06003E23 RID: 15907 RVA: 0x00695FCC File Offset: 0x006941CC
			internal string <Load>b__18_56()
			{
				int num = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 16, num);
			}

			// Token: 0x06003E24 RID: 15908 RVA: 0x00696000 File Offset: 0x00694200
			internal string <Load>b__18_57()
			{
				int num = UILinkPointNavigator.CurrentPoint - 185;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscDyes, 33, num);
			}

			// Token: 0x06003E25 RID: 15909 RVA: 0x00696031 File Offset: 0x00694231
			internal bool <Load>b__18_59()
			{
				return Main.playerInventory && Main.EquipPage == 2;
			}

			// Token: 0x06003E26 RID: 15910 RVA: 0x00696044 File Offset: 0x00694244
			internal string <Load>b__18_60()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E27 RID: 15911 RVA: 0x0069546E File Offset: 0x0069366E
			internal bool <Load>b__18_62()
			{
				return Main.playerInventory;
			}

			// Token: 0x06003E28 RID: 15912 RVA: 0x006960CC File Offset: 0x006942CC
			internal string <Load>b__18_63()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E29 RID: 15913 RVA: 0x00696153 File Offset: 0x00694353
			internal string <Load>b__18_64()
			{
				return ItemSlot.GetGamepadInstructions(Main.InPipBanner ? 35 : 22);
			}

			// Token: 0x06003E2A RID: 15914 RVA: 0x00696167 File Offset: 0x00694367
			internal string <Load>b__18_65()
			{
				return ItemSlot.GetGamepadInstructions(ref Main.guideItem, 7);
			}

			// Token: 0x06003E2B RID: 15915 RVA: 0x00696174 File Offset: 0x00694374
			internal string <Load>b__18_66()
			{
				return PlayerInput.BuildCommand(Language.GetTextValue("UI.ToggleClassicGrid"), new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"] });
			}

			// Token: 0x06003E2C RID: 15916 RVA: 0x006961A2 File Offset: 0x006943A2
			internal string <Load>b__18_68()
			{
				return ItemSlot.GetCraftSlotGamepadInstructions();
			}

			// Token: 0x06003E2D RID: 15917 RVA: 0x006961AC File Offset: 0x006943AC
			internal void <Load>b__18_69(int current, int next)
			{
				if (current != 1500)
				{
					if (current == 1501)
					{
						if (next == -1)
						{
							if (UILinksInitializer.MainfocusRecipe > 0)
							{
								UILinksInitializer.MainfocusRecipe--;
								return;
							}
						}
						else if (next == -2 && UILinksInitializer.MainfocusRecipe < UILinksInitializer.MainnumAvailableRecipes - 1)
						{
							UILinksInitializer.MainfocusRecipe++;
							return;
						}
					}
					else if (next == -1)
					{
						if (UILinksInitializer.MainfocusRecipe > 0)
						{
							UILinkPointNavigator.ChangePoint(1501);
							UILinksInitializer.MainfocusRecipe--;
							return;
						}
					}
					else if (next == -2 && UILinksInitializer.MainfocusRecipe < UILinksInitializer.MainnumAvailableRecipes - 1)
					{
						UILinkPointNavigator.ChangePoint(1501);
						UILinksInitializer.MainfocusRecipe++;
					}
				}
			}

			// Token: 0x06003E2E RID: 15918 RVA: 0x00696251 File Offset: 0x00694451
			internal void <Load>b__18_70()
			{
				Main.PipsUseGrid = false;
				PlayerInput.LockGamepadButtons("MouseLeft");
			}

			// Token: 0x06003E2F RID: 15919 RVA: 0x00696263 File Offset: 0x00694463
			internal bool <Load>b__18_71()
			{
				return Main.playerInventory && (UILinksInitializer.MainnumAvailableRecipes > 0 || Main.InGuideCraftMenu);
			}

			// Token: 0x06003E30 RID: 15920 RVA: 0x00696263 File Offset: 0x00694463
			internal bool <Load>b__18_72()
			{
				return Main.playerInventory && (UILinksInitializer.MainnumAvailableRecipes > 0 || Main.InGuideCraftMenu);
			}

			// Token: 0x06003E31 RID: 15921 RVA: 0x00696280 File Offset: 0x00694480
			internal string <Load>b__18_73()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E32 RID: 15922 RVA: 0x00696153 File Offset: 0x00694353
			internal string <Load>b__18_74()
			{
				return ItemSlot.GetGamepadInstructions(Main.InPipBanner ? 35 : 22);
			}

			// Token: 0x06003E33 RID: 15923 RVA: 0x00696308 File Offset: 0x00694508
			internal void <Load>b__18_76(int current, int next)
			{
				int craft_IconsPerRow = UILinkPointNavigator.Shortcuts.CRAFT_IconsPerRow;
				if (next == -1)
				{
					Main.recStart -= craft_IconsPerRow;
					if (Main.recStart < 0)
					{
						Main.recStart = 0;
						return;
					}
				}
				else if (next == -2)
				{
					Main.recStart += craft_IconsPerRow;
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					if (Main.recStart > UILinksInitializer.MainnumAvailableRecipes - craft_IconsPerRow)
					{
						Main.recStart = UILinksInitializer.MainnumAvailableRecipes - craft_IconsPerRow;
					}
				}
			}

			// Token: 0x06003E34 RID: 15924 RVA: 0x0069637A File Offset: 0x0069457A
			internal void <Load>b__18_77()
			{
				Main.PipsUseGrid = true;
			}

			// Token: 0x06003E35 RID: 15925 RVA: 0x00696382 File Offset: 0x00694582
			internal void <Load>b__18_78()
			{
				Main.PipsUseGrid = false;
			}

			// Token: 0x06003E36 RID: 15926 RVA: 0x0069638A File Offset: 0x0069458A
			internal bool <Load>b__18_79()
			{
				return Main.playerInventory && UILinksInitializer.MainnumAvailableRecipes > 0;
			}

			// Token: 0x06003E37 RID: 15927 RVA: 0x0069639D File Offset: 0x0069459D
			internal bool <Load>b__18_80()
			{
				return Main.playerInventory && Main.PipsUseGrid && UILinksInitializer.MainnumAvailableRecipes > 0;
			}

			// Token: 0x06003E38 RID: 15928 RVA: 0x006963B8 File Offset: 0x006945B8
			internal string <Load>b__18_82()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E39 RID: 15929 RVA: 0x0069643F File Offset: 0x0069463F
			internal string <Load>b__18_87()
			{
				return PlayerInput.BuildCommand(Lang.misc[73].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] });
			}

			// Token: 0x06003E3A RID: 15930 RVA: 0x00696470 File Offset: 0x00694670
			internal void <Load>b__18_84(int current, int next)
			{
				int num = 5;
				if (next == -1)
				{
					Main.hairStart -= num;
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				if (next == -2)
				{
					Main.hairStart += num;
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
			}

			// Token: 0x06003E3B RID: 15931 RVA: 0x006964CB File Offset: 0x006946CB
			internal bool <Load>b__18_85()
			{
				return Main.hairWindow;
			}

			// Token: 0x06003E3C RID: 15932 RVA: 0x006964CB File Offset: 0x006946CB
			internal bool <Load>b__18_86()
			{
				return Main.hairWindow;
			}

			// Token: 0x06003E3D RID: 15933 RVA: 0x006964D4 File Offset: 0x006946D4
			internal string <Load>b__18_88()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E3E RID: 15934 RVA: 0x0069655C File Offset: 0x0069475C
			internal void <Load>b__18_89()
			{
				Vector3 vector = Main.rgbToHsl(Main.selColor);
				float interfaceDeadzoneX = PlayerInput.CurrentProfile.InterfaceDeadzoneX;
				float num = PlayerInput.GamepadThumbstickLeft.X;
				if (num < -interfaceDeadzoneX || num > interfaceDeadzoneX)
				{
					num = MathHelper.Lerp(0f, 0.008333334f, (Math.Abs(num) - interfaceDeadzoneX) / (1f - interfaceDeadzoneX)) * (float)Math.Sign(num);
				}
				else
				{
					num = 0f;
				}
				int currentPoint = UILinkPointNavigator.CurrentPoint;
				if (currentPoint == 2600)
				{
					Main.hBar = MathHelper.Clamp(Main.hBar + num, 0f, 1f);
				}
				if (currentPoint == 2601)
				{
					Main.sBar = MathHelper.Clamp(Main.sBar + num, 0f, 1f);
				}
				if (currentPoint == 2602)
				{
					Main.lBar = MathHelper.Clamp(Main.lBar + num, 0.15f, 1f);
				}
				Vector3.Clamp(vector, Vector3.Zero, Vector3.One);
				if (num != 0f)
				{
					if (Main.hairWindow)
					{
						Main.player[Main.myPlayer].hairColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
					}
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
			}

			// Token: 0x06003E3F RID: 15935 RVA: 0x006964CB File Offset: 0x006946CB
			internal bool <Load>b__18_90()
			{
				return Main.hairWindow;
			}

			// Token: 0x06003E40 RID: 15936 RVA: 0x006964CB File Offset: 0x006946CB
			internal bool <Load>b__18_91()
			{
				return Main.hairWindow;
			}

			// Token: 0x06003E41 RID: 15937 RVA: 0x00696694 File Offset: 0x00694894
			internal string <Load>b__18_92()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E42 RID: 15938 RVA: 0x0069671B File Offset: 0x0069491B
			internal bool <Load>b__18_96()
			{
				return Main.ingameOptionsWindow && !Main.InGameUI.IsVisible;
			}

			// Token: 0x06003E43 RID: 15939 RVA: 0x0069671B File Offset: 0x0069491B
			internal bool <Load>b__18_97()
			{
				return Main.ingameOptionsWindow && !Main.InGameUI.IsVisible;
			}

			// Token: 0x06003E44 RID: 15940 RVA: 0x00696733 File Offset: 0x00694933
			internal void <Load>b__18_98()
			{
				Main.mouseLeftRelease = false;
			}

			// Token: 0x06003E45 RID: 15941 RVA: 0x0069673C File Offset: 0x0069493C
			internal string <Load>b__18_99()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E46 RID: 15942 RVA: 0x006967C3 File Offset: 0x006949C3
			internal bool <Load>b__18_101()
			{
				return Main.ingameOptionsWindow;
			}

			// Token: 0x06003E47 RID: 15943 RVA: 0x006967C3 File Offset: 0x006949C3
			internal bool <Load>b__18_102()
			{
				return Main.ingameOptionsWindow;
			}

			// Token: 0x06003E48 RID: 15944 RVA: 0x006967CC File Offset: 0x006949CC
			internal string <Load>b__18_103()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E49 RID: 15945 RVA: 0x0069546E File Offset: 0x0069366E
			internal bool <Load>b__18_105()
			{
				return Main.playerInventory;
			}

			// Token: 0x06003E4A RID: 15946 RVA: 0x00696854 File Offset: 0x00694A54
			internal string <Load>b__18_106()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E4B RID: 15947 RVA: 0x006968DB File Offset: 0x00694ADB
			internal bool <Load>b__18_108()
			{
				return Main.playerInventory && UILinkPointNavigator.Shortcuts.INFOACCCOUNT > 0;
			}

			// Token: 0x06003E4C RID: 15948 RVA: 0x006968F0 File Offset: 0x00694AF0
			internal string <Load>b__18_109()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E4D RID: 15949 RVA: 0x00696977 File Offset: 0x00694B77
			internal bool <Load>b__18_111()
			{
				return Main.playerInventory && UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 0;
			}

			// Token: 0x06003E4E RID: 15950 RVA: 0x0069698C File Offset: 0x00694B8C
			internal string <Load>b__18_112()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E4F RID: 15951 RVA: 0x00696A13 File Offset: 0x00694C13
			internal bool <Load>b__18_113()
			{
				return Main.clothesWindow;
			}

			// Token: 0x06003E50 RID: 15952 RVA: 0x00696A13 File Offset: 0x00694C13
			internal bool <Load>b__18_114()
			{
				return Main.clothesWindow;
			}

			// Token: 0x06003E51 RID: 15953 RVA: 0x00695377 File Offset: 0x00693577
			internal void <Load>b__18_115()
			{
				Main.player[Main.myPlayer].releaseInventory = false;
			}

			// Token: 0x06003E52 RID: 15954 RVA: 0x00696A1A File Offset: 0x00694C1A
			internal void <Load>b__18_116()
			{
				Main.player[Main.myPlayer].LockGamepadTileInteractions();
			}

			// Token: 0x06003E53 RID: 15955 RVA: 0x00696A2C File Offset: 0x00694C2C
			internal string <Load>b__18_117()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E54 RID: 15956 RVA: 0x00696AB4 File Offset: 0x00694CB4
			internal void <Load>b__18_118()
			{
				Vector3 vector = Main.rgbToHsl(Main.selColor);
				float interfaceDeadzoneX = PlayerInput.CurrentProfile.InterfaceDeadzoneX;
				float num = PlayerInput.GamepadThumbstickLeft.X;
				if (num < -interfaceDeadzoneX || num > interfaceDeadzoneX)
				{
					num = MathHelper.Lerp(0f, 0.008333334f, (Math.Abs(num) - interfaceDeadzoneX) / (1f - interfaceDeadzoneX)) * (float)Math.Sign(num);
				}
				else
				{
					num = 0f;
				}
				int currentPoint = UILinkPointNavigator.CurrentPoint;
				if (currentPoint == 2800)
				{
					Main.hBar = MathHelper.Clamp(Main.hBar + num, 0f, 1f);
				}
				if (currentPoint == 2801)
				{
					Main.sBar = MathHelper.Clamp(Main.sBar + num, 0f, 1f);
				}
				if (currentPoint == 2802)
				{
					Main.lBar = MathHelper.Clamp(Main.lBar + num, 0.15f, 1f);
				}
				if (currentPoint == 2812)
				{
					Main.player[Main.myPlayer].voicePitchOffset = MathHelper.Clamp(Main.player[Main.myPlayer].voicePitchOffset + num, -1f, 1f);
				}
				Vector3.Clamp(vector, Vector3.Zero, Vector3.One);
				if (num != 0f)
				{
					if (Main.clothesWindow)
					{
						Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue);
						switch (Main.selClothes)
						{
						case 0:
							Main.player[Main.myPlayer].shirtColor = Main.selColor;
							break;
						case 1:
							Main.player[Main.myPlayer].underShirtColor = Main.selColor;
							break;
						case 2:
							Main.player[Main.myPlayer].pantsColor = Main.selColor;
							break;
						case 3:
							Main.player[Main.myPlayer].shoeColor = Main.selColor;
							break;
						}
					}
					if (currentPoint != 2812)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					}
				}
				if (currentPoint == 2812)
				{
					bool flag = num != 0f;
					if (Main.WasDraggingPlayerAudio && !flag)
					{
						Main.player[Main.myPlayer].PlayHurtSound();
					}
					Main.WasDraggingPlayerAudio = flag;
				}
			}

			// Token: 0x06003E55 RID: 15957 RVA: 0x00696A13 File Offset: 0x00694C13
			internal bool <Load>b__18_119()
			{
				return Main.clothesWindow;
			}

			// Token: 0x06003E56 RID: 15958 RVA: 0x00696A13 File Offset: 0x00694C13
			internal bool <Load>b__18_120()
			{
				return Main.clothesWindow;
			}

			// Token: 0x06003E57 RID: 15959 RVA: 0x00696CD1 File Offset: 0x00694ED1
			internal void <Load>b__18_121()
			{
				Main.player[Main.myPlayer].releaseInventory = false;
				Main.WasDraggingPlayerAudio = false;
			}

			// Token: 0x06003E58 RID: 15960 RVA: 0x00696A1A File Offset: 0x00694C1A
			internal void <Load>b__18_122()
			{
				Main.player[Main.myPlayer].LockGamepadTileInteractions();
			}

			// Token: 0x06003E59 RID: 15961 RVA: 0x00695200 File Offset: 0x00693400
			internal void <Load>b__18_123()
			{
				PlayerInput.GamepadAllowScrolling = true;
			}

			// Token: 0x06003E5A RID: 15962 RVA: 0x00696CEC File Offset: 0x00694EEC
			internal string <Load>b__18_124()
			{
				if (Main.InGameUI.CurrentState is UIBestiaryTest)
				{
					return PlayerInput.BuildCommand(Lang.misc[82].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Language.GetText("UI.SwitchPage").Value, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
						PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
					}) + PlayerInput.BuildCommand(Lang.misc[53].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] }) + UILinksInitializer.FancyUISpecialInstructions();
				}
				return PlayerInput.BuildCommand(Lang.misc[53].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] }) + PlayerInput.BuildCommand(Lang.misc[82].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + UILinksInitializer.FancyUISpecialInstructions();
			}

			// Token: 0x06003E5B RID: 15963 RVA: 0x00696E26 File Offset: 0x00695026
			internal void <Load>b__18_125()
			{
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.Inventory)
				{
					UILinksInitializer.FancyExit();
				}
			}

			// Token: 0x06003E5C RID: 15964 RVA: 0x00696E45 File Offset: 0x00695045
			internal bool <Load>b__18_127()
			{
				return Main.MenuUI.IsVisible || Main.InGameUI.IsVisible;
			}

			// Token: 0x06003E5D RID: 15965 RVA: 0x00696E45 File Offset: 0x00695045
			internal bool <Load>b__18_128()
			{
				return Main.MenuUI.IsVisible || Main.InGameUI.IsVisible;
			}

			// Token: 0x06003E5E RID: 15966 RVA: 0x00695200 File Offset: 0x00693400
			internal void <Load>b__18_129()
			{
				PlayerInput.GamepadAllowScrolling = true;
			}

			// Token: 0x06003E5F RID: 15967 RVA: 0x00696E60 File Offset: 0x00695060
			internal string <Load>b__18_130()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				}) + PlayerInput.BuildCommand(Lang.misc[53].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] }) + UILinksInitializer.FancyUISpecialInstructions();
			}

			// Token: 0x06003E60 RID: 15968 RVA: 0x00696E26 File Offset: 0x00695026
			internal void <Load>b__18_131()
			{
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.Inventory)
				{
					UILinksInitializer.FancyExit();
				}
			}

			// Token: 0x06003E61 RID: 15969 RVA: 0x004E4298 File Offset: 0x004E2498
			internal bool <Load>b__18_133()
			{
				return NewCraftingUI.Visible;
			}

			// Token: 0x06003E62 RID: 15970 RVA: 0x004E4298 File Offset: 0x004E2498
			internal bool <Load>b__18_134()
			{
				return NewCraftingUI.Visible;
			}

			// Token: 0x06003E63 RID: 15971 RVA: 0x00696F1C File Offset: 0x0069511C
			internal string <Load>b__18_135()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E64 RID: 15972 RVA: 0x00696FA3 File Offset: 0x006951A3
			internal string <Load>b__18_136()
			{
				return PlayerInput.BuildCommand(Lang.misc[94].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"] });
			}

			// Token: 0x06003E65 RID: 15973 RVA: 0x00696FD4 File Offset: 0x006951D4
			internal bool <Load>b__18_138()
			{
				return Main.playerInventory && Main.EquipPage == 2 && UILinkPointNavigator.Shortcuts.BUFFS_DRAWN > 0;
			}

			// Token: 0x06003E66 RID: 15974 RVA: 0x00696FF0 File Offset: 0x006951F0
			internal string <Load>b__18_139()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E67 RID: 15975 RVA: 0x00697077 File Offset: 0x00695277
			internal string <Load>b__18_140()
			{
				return ItemSlot.GetGamepadInstructions(35);
			}

			// Token: 0x06003E68 RID: 15976 RVA: 0x00697080 File Offset: 0x00695280
			internal string <Load>b__18_141()
			{
				string text = "";
				if (Main.mouseItem.stack <= 0 || (Main.mouseItem.type == Main.bannerUI.FocusedItemType && Main.mouseItem.stack < Main.mouseItem.maxStack))
				{
					text += PlayerInput.BuildCommand(Language.GetTextValue("UI.GamepadClaimBanner"), new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"],
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
					});
				}
				return text;
			}

			// Token: 0x06003E69 RID: 15977 RVA: 0x00697118 File Offset: 0x00695318
			internal void <Load>b__18_142(int current, int next)
			{
				bool flag = next == -1;
				int num = (next == -2).ToInt() - flag.ToInt();
				Main.bannerUI.NavigatePipsList(num);
			}

			// Token: 0x06003E6A RID: 15978 RVA: 0x00696251 File Offset: 0x00694451
			internal void <Load>b__18_143()
			{
				Main.PipsUseGrid = false;
				PlayerInput.LockGamepadButtons("MouseLeft");
			}

			// Token: 0x06003E6B RID: 15979 RVA: 0x00697147 File Offset: 0x00695347
			internal bool <Load>b__18_144()
			{
				return Main.playerInventory && Main.bannerUI.AnyAvailableBanners;
			}

			// Token: 0x06003E6C RID: 15980 RVA: 0x00697147 File Offset: 0x00695347
			internal bool <Load>b__18_145()
			{
				return Main.playerInventory && Main.bannerUI.AnyAvailableBanners;
			}

			// Token: 0x06003E6D RID: 15981 RVA: 0x0069715C File Offset: 0x0069535C
			internal string <Load>b__18_146()
			{
				return PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] }) + PlayerInput.BuildCommand(Lang.misc[64].Value, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
				});
			}

			// Token: 0x06003E6E RID: 15982 RVA: 0x00697077 File Offset: 0x00695277
			internal string <Load>b__18_147()
			{
				return ItemSlot.GetGamepadInstructions(35);
			}

			// Token: 0x06003E6F RID: 15983 RVA: 0x006971E4 File Offset: 0x006953E4
			internal void <Load>b__18_149(int current, int next)
			{
				bool flag = next == -3;
				int num = (next == -4).ToInt() - flag.ToInt();
				bool flag2 = next == -1;
				int num2 = (next == -2).ToInt() - flag2.ToInt();
				Main.bannerUI.NavigatePipsGrid(num, num2);
			}

			// Token: 0x06003E70 RID: 15984 RVA: 0x0069722C File Offset: 0x0069542C
			internal void <Load>b__18_150()
			{
				Main.PipsUseGrid = true;
				Main.bannerUI.ResetGridSelection();
			}

			// Token: 0x06003E71 RID: 15985 RVA: 0x00696382 File Offset: 0x00694582
			internal void <Load>b__18_151()
			{
				Main.PipsUseGrid = false;
			}

			// Token: 0x06003E72 RID: 15986 RVA: 0x00697147 File Offset: 0x00695347
			internal bool <Load>b__18_152()
			{
				return Main.playerInventory && Main.bannerUI.AnyAvailableBanners;
			}

			// Token: 0x06003E73 RID: 15987 RVA: 0x0069723E File Offset: 0x0069543E
			internal bool <Load>b__18_153()
			{
				return Main.playerInventory && Main.PipsUseGrid && Main.bannerUI.AnyAvailableBanners;
			}

			// Token: 0x040066C9 RID: 26313
			public static readonly UILinksInitializer.<>c <>9 = new UILinksInitializer.<>c();

			// Token: 0x040066CA RID: 26314
			public static Func<string> <>9__18_0;

			// Token: 0x040066CB RID: 26315
			public static Action <>9__18_1;

			// Token: 0x040066CC RID: 26316
			public static Func<string> <>9__18_2;

			// Token: 0x040066CD RID: 26317
			public static Action <>9__18_3;

			// Token: 0x040066CE RID: 26318
			public static Func<bool> <>9__18_4;

			// Token: 0x040066CF RID: 26319
			public static Func<bool> <>9__18_5;

			// Token: 0x040066D0 RID: 26320
			public static Func<string> <>9__18_7;

			// Token: 0x040066D1 RID: 26321
			public static Func<bool> <>9__18_8;

			// Token: 0x040066D2 RID: 26322
			public static Func<bool> <>9__18_9;

			// Token: 0x040066D3 RID: 26323
			public static Action <>9__18_10;

			// Token: 0x040066D4 RID: 26324
			public static Action <>9__18_11;

			// Token: 0x040066D5 RID: 26325
			public static Func<string> <>9__18_12;

			// Token: 0x040066D6 RID: 26326
			public static Func<string> <>9__18_13;

			// Token: 0x040066D7 RID: 26327
			public static Func<string> <>9__18_14;

			// Token: 0x040066D8 RID: 26328
			public static Func<bool> <>9__18_16;

			// Token: 0x040066D9 RID: 26329
			public static Func<string> <>9__18_17;

			// Token: 0x040066DA RID: 26330
			public static Func<string> <>9__18_18;

			// Token: 0x040066DB RID: 26331
			public static Func<bool> <>9__18_20;

			// Token: 0x040066DC RID: 26332
			public static Func<string> <>9__18_21;

			// Token: 0x040066DD RID: 26333
			public static Func<string> <>9__18_22;

			// Token: 0x040066DE RID: 26334
			public static Func<string> <>9__18_24;

			// Token: 0x040066DF RID: 26335
			public static Func<string> <>9__18_25;

			// Token: 0x040066E0 RID: 26336
			public static Func<string> <>9__18_26;

			// Token: 0x040066E1 RID: 26337
			public static Func<bool> <>9__18_27;

			// Token: 0x040066E2 RID: 26338
			public static Func<string> <>9__18_29;

			// Token: 0x040066E3 RID: 26339
			public static Func<string> <>9__18_30;

			// Token: 0x040066E4 RID: 26340
			public static Func<bool> <>9__18_32;

			// Token: 0x040066E5 RID: 26341
			public static Func<string> <>9__18_33;

			// Token: 0x040066E6 RID: 26342
			public static Func<string> <>9__18_34;

			// Token: 0x040066E7 RID: 26343
			public static Func<bool> <>9__18_35;

			// Token: 0x040066E8 RID: 26344
			public static Func<string> <>9__18_36;

			// Token: 0x040066E9 RID: 26345
			public static Func<string> <>9__18_37;

			// Token: 0x040066EA RID: 26346
			public static Func<bool> <>9__18_38;

			// Token: 0x040066EB RID: 26347
			public static Func<string> <>9__18_39;

			// Token: 0x040066EC RID: 26348
			public static Func<string> <>9__18_40;

			// Token: 0x040066ED RID: 26349
			public static Func<bool> <>9__18_41;

			// Token: 0x040066EE RID: 26350
			public static Func<string> <>9__18_42;

			// Token: 0x040066EF RID: 26351
			public static Func<string> <>9__18_43;

			// Token: 0x040066F0 RID: 26352
			public static Func<string> <>9__18_44;

			// Token: 0x040066F1 RID: 26353
			public static Func<bool> <>9__18_46;

			// Token: 0x040066F2 RID: 26354
			public static Action <>9__18_47;

			// Token: 0x040066F3 RID: 26355
			public static Func<string> <>9__18_48;

			// Token: 0x040066F4 RID: 26356
			public static Func<bool> <>9__18_50;

			// Token: 0x040066F5 RID: 26357
			public static Func<string> <>9__18_51;

			// Token: 0x040066F6 RID: 26358
			public static Func<string> <>9__18_52;

			// Token: 0x040066F7 RID: 26359
			public static Func<string> <>9__18_53;

			// Token: 0x040066F8 RID: 26360
			public static Func<string> <>9__18_54;

			// Token: 0x040066F9 RID: 26361
			public static Func<string> <>9__18_55;

			// Token: 0x040066FA RID: 26362
			public static Func<string> <>9__18_56;

			// Token: 0x040066FB RID: 26363
			public static Func<string> <>9__18_57;

			// Token: 0x040066FC RID: 26364
			public static Func<bool> <>9__18_59;

			// Token: 0x040066FD RID: 26365
			public static Func<string> <>9__18_60;

			// Token: 0x040066FE RID: 26366
			public static Func<bool> <>9__18_62;

			// Token: 0x040066FF RID: 26367
			public static Func<string> <>9__18_63;

			// Token: 0x04006700 RID: 26368
			public static Func<string> <>9__18_64;

			// Token: 0x04006701 RID: 26369
			public static Func<string> <>9__18_65;

			// Token: 0x04006702 RID: 26370
			public static Func<string> <>9__18_66;

			// Token: 0x04006703 RID: 26371
			public static Func<string> <>9__18_68;

			// Token: 0x04006704 RID: 26372
			public static Action<int, int> <>9__18_69;

			// Token: 0x04006705 RID: 26373
			public static Action <>9__18_70;

			// Token: 0x04006706 RID: 26374
			public static Func<bool> <>9__18_71;

			// Token: 0x04006707 RID: 26375
			public static Func<bool> <>9__18_72;

			// Token: 0x04006708 RID: 26376
			public static Func<string> <>9__18_73;

			// Token: 0x04006709 RID: 26377
			public static Func<string> <>9__18_74;

			// Token: 0x0400670A RID: 26378
			public static Action<int, int> <>9__18_76;

			// Token: 0x0400670B RID: 26379
			public static Action <>9__18_77;

			// Token: 0x0400670C RID: 26380
			public static Action <>9__18_78;

			// Token: 0x0400670D RID: 26381
			public static Func<bool> <>9__18_79;

			// Token: 0x0400670E RID: 26382
			public static Func<bool> <>9__18_80;

			// Token: 0x0400670F RID: 26383
			public static Func<string> <>9__18_82;

			// Token: 0x04006710 RID: 26384
			public static Func<string> <>9__18_87;

			// Token: 0x04006711 RID: 26385
			public static Action<int, int> <>9__18_84;

			// Token: 0x04006712 RID: 26386
			public static Func<bool> <>9__18_85;

			// Token: 0x04006713 RID: 26387
			public static Func<bool> <>9__18_86;

			// Token: 0x04006714 RID: 26388
			public static Func<string> <>9__18_88;

			// Token: 0x04006715 RID: 26389
			public static Action <>9__18_89;

			// Token: 0x04006716 RID: 26390
			public static Func<bool> <>9__18_90;

			// Token: 0x04006717 RID: 26391
			public static Func<bool> <>9__18_91;

			// Token: 0x04006718 RID: 26392
			public static Func<string> <>9__18_92;

			// Token: 0x04006719 RID: 26393
			public static Func<bool> <>9__18_96;

			// Token: 0x0400671A RID: 26394
			public static Func<bool> <>9__18_97;

			// Token: 0x0400671B RID: 26395
			public static Action <>9__18_98;

			// Token: 0x0400671C RID: 26396
			public static Func<string> <>9__18_99;

			// Token: 0x0400671D RID: 26397
			public static Func<bool> <>9__18_101;

			// Token: 0x0400671E RID: 26398
			public static Func<bool> <>9__18_102;

			// Token: 0x0400671F RID: 26399
			public static Func<string> <>9__18_103;

			// Token: 0x04006720 RID: 26400
			public static Func<bool> <>9__18_105;

			// Token: 0x04006721 RID: 26401
			public static Func<string> <>9__18_106;

			// Token: 0x04006722 RID: 26402
			public static Func<bool> <>9__18_108;

			// Token: 0x04006723 RID: 26403
			public static Func<string> <>9__18_109;

			// Token: 0x04006724 RID: 26404
			public static Func<bool> <>9__18_111;

			// Token: 0x04006725 RID: 26405
			public static Func<string> <>9__18_112;

			// Token: 0x04006726 RID: 26406
			public static Func<bool> <>9__18_113;

			// Token: 0x04006727 RID: 26407
			public static Func<bool> <>9__18_114;

			// Token: 0x04006728 RID: 26408
			public static Action <>9__18_115;

			// Token: 0x04006729 RID: 26409
			public static Action <>9__18_116;

			// Token: 0x0400672A RID: 26410
			public static Func<string> <>9__18_117;

			// Token: 0x0400672B RID: 26411
			public static Action <>9__18_118;

			// Token: 0x0400672C RID: 26412
			public static Func<bool> <>9__18_119;

			// Token: 0x0400672D RID: 26413
			public static Func<bool> <>9__18_120;

			// Token: 0x0400672E RID: 26414
			public static Action <>9__18_121;

			// Token: 0x0400672F RID: 26415
			public static Action <>9__18_122;

			// Token: 0x04006730 RID: 26416
			public static Action <>9__18_123;

			// Token: 0x04006731 RID: 26417
			public static Func<string> <>9__18_124;

			// Token: 0x04006732 RID: 26418
			public static Action <>9__18_125;

			// Token: 0x04006733 RID: 26419
			public static Func<bool> <>9__18_127;

			// Token: 0x04006734 RID: 26420
			public static Func<bool> <>9__18_128;

			// Token: 0x04006735 RID: 26421
			public static Action <>9__18_129;

			// Token: 0x04006736 RID: 26422
			public static Func<string> <>9__18_130;

			// Token: 0x04006737 RID: 26423
			public static Action <>9__18_131;

			// Token: 0x04006738 RID: 26424
			public static Func<bool> <>9__18_133;

			// Token: 0x04006739 RID: 26425
			public static Func<bool> <>9__18_134;

			// Token: 0x0400673A RID: 26426
			public static Func<string> <>9__18_135;

			// Token: 0x0400673B RID: 26427
			public static Func<string> <>9__18_136;

			// Token: 0x0400673C RID: 26428
			public static Func<bool> <>9__18_138;

			// Token: 0x0400673D RID: 26429
			public static Func<string> <>9__18_139;

			// Token: 0x0400673E RID: 26430
			public static Func<string> <>9__18_140;

			// Token: 0x0400673F RID: 26431
			public static Func<string> <>9__18_141;

			// Token: 0x04006740 RID: 26432
			public static Action<int, int> <>9__18_142;

			// Token: 0x04006741 RID: 26433
			public static Action <>9__18_143;

			// Token: 0x04006742 RID: 26434
			public static Func<bool> <>9__18_144;

			// Token: 0x04006743 RID: 26435
			public static Func<bool> <>9__18_145;

			// Token: 0x04006744 RID: 26436
			public static Func<string> <>9__18_146;

			// Token: 0x04006745 RID: 26437
			public static Func<string> <>9__18_147;

			// Token: 0x04006746 RID: 26438
			public static Action<int, int> <>9__18_149;

			// Token: 0x04006747 RID: 26439
			public static Action <>9__18_150;

			// Token: 0x04006748 RID: 26440
			public static Action <>9__18_151;

			// Token: 0x04006749 RID: 26441
			public static Func<bool> <>9__18_152;

			// Token: 0x0400674A RID: 26442
			public static Func<bool> <>9__18_153;
		}
	}
}
