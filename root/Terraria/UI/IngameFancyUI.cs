using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Achievements;
using Terraria.Audio;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI.Gamepad;

namespace Terraria.UI
{
	// Token: 0x020000F9 RID: 249
	public class IngameFancyUI
	{
		// Token: 0x06001957 RID: 6487 RVA: 0x004E8146 File Offset: 0x004E6346
		public static void CoverNextFrame()
		{
			IngameFancyUI.CoverForOneUIFrame = true;
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x004E814E File Offset: 0x004E634E
		public static bool CanCover()
		{
			if (IngameFancyUI.CoverForOneUIFrame)
			{
				IngameFancyUI.CoverForOneUIFrame = false;
				return true;
			}
			return Main.inFancyUI;
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x004E8169 File Offset: 0x004E6369
		public static void OpenAchievements()
		{
			if (Main.gameMenu)
			{
				Main.MenuUI.SetState(Main.AchievementsMenu);
				return;
			}
			IngameFancyUI.OpenUIState(Main.AchievementsMenu);
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x004E818C File Offset: 0x004E638C
		public static void OpenAchievementsAndGoto(Achievement achievement)
		{
			IngameFancyUI.OpenAchievements();
			Main.AchievementsMenu.GotoAchievement(achievement);
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x004E819E File Offset: 0x004E639E
		private static void ClearChat()
		{
			Main.ClosePlayerChat();
			Main.chatText = "";
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x004E81AF File Offset: 0x004E63AF
		public static void OpenKeybinds()
		{
			IngameFancyUI.OpenUIState(Main.ManageControlsMenu);
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x004E81BB File Offset: 0x004E63BB
		public static void OpenUIState(UIState uiState)
		{
			IngameFancyUI.OpenUIState(uiState, true);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x004E81C4 File Offset: 0x004E63C4
		public static void OpenUIState(UIState uiState, bool closeIngameWindows = true)
		{
			IngameFancyUI.CoverNextFrame();
			IngameFancyUI.ClearChat();
			if (!Main.inFancyUI && closeIngameWindows)
			{
				IngameUIWindows.CloseAll(true);
			}
			Main.inFancyUI = true;
			Main.InGameUI.SetState(uiState);
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x004E81F3 File Offset: 0x004E63F3
		public static bool CanShowVirtualKeyboard(int context)
		{
			return UIVirtualKeyboard.CanDisplay(context);
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x004E81FC File Offset: 0x004E63FC
		public static void OpenVirtualKeyboard(int keyboardContext)
		{
			IngameFancyUI.CoverNextFrame();
			IngameFancyUI.ClearChat();
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			string text = "";
			if (keyboardContext != 1)
			{
				if (keyboardContext == 2)
				{
					text = Language.GetTextValue("UI.EnterNewName");
					Player player = Main.player[Main.myPlayer];
					Main.npcChatText = Main.chest[player.chest].name;
					Tile tile = Main.tile[player.chestX, player.chestY];
					if (tile.type == 21)
					{
						Main.defaultChestName = Lang.chestType[(int)(tile.frameX / 36)].Value;
					}
					else if (tile.type == 467 && tile.frameX / 36 == 4)
					{
						Main.defaultChestName = Lang.GetItemNameValue(3988);
					}
					else if (tile.type == 467)
					{
						Main.defaultChestName = Lang.chestType2[(int)(tile.frameX / 36)].Value;
					}
					else if (tile.type == 88)
					{
						Main.defaultChestName = Lang.dresserType[(int)(tile.frameX / 54)].Value;
					}
					if (Main.npcChatText == "")
					{
						Main.npcChatText = Main.defaultChestName;
					}
					Main.editChest = true;
				}
			}
			else
			{
				Main.editSign = true;
				text = Language.GetTextValue("UI.EnterMessage");
			}
			Main.clrInput();
			if (!IngameFancyUI.CanShowVirtualKeyboard(keyboardContext))
			{
				return;
			}
			Main.inFancyUI = true;
			if (keyboardContext != 1)
			{
				if (keyboardContext == 2)
				{
					Main.InGameUI.SetState(new UIVirtualKeyboard(text, Main.npcChatText, delegate(string s)
					{
						ChestUI.RenameChestSubmit(Main.player[Main.myPlayer]);
						IngameFancyUI.Close(true);
					}, delegate
					{
						ChestUI.RenameChestCancel();
						IngameFancyUI.Close(true);
					}, keyboardContext, false, 20));
				}
			}
			else
			{
				Main.InGameUI.SetState(new UIVirtualKeyboard(text, Main.npcChatText, delegate(string s)
				{
					Main.SubmitSignText();
					IngameFancyUI.Close(true);
				}, delegate
				{
					Main.InputTextSignCancel();
					IngameFancyUI.Close(true);
				}, keyboardContext, false, 1200));
			}
			UILinkPointNavigator.GoToDefaultPage(1);
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x004E842C File Offset: 0x004E662C
		public static void Close(bool quiet = false)
		{
			Main.inFancyUI = false;
			if (!quiet)
			{
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			}
			bool flag = false;
			if (!Main.gameMenu)
			{
				if (Main.InGameUI.CurrentState is UIVirtualKeyboard)
				{
					flag = UIVirtualKeyboard.KeyboardContext == 2;
				}
				else if (!(Main.InGameUI.CurrentState is UIEmotesMenu))
				{
					flag = true;
				}
			}
			if (flag)
			{
				Main.playerInventory = true;
			}
			Main.LocalPlayer.releaseInventory = false;
			Main.InGameUI.SetState(null);
			UILinkPointNavigator.Shortcuts.FANCYUI_SPECIAL_INSTRUCTIONS = 0;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x004E84B4 File Offset: 0x004E66B4
		public static bool Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			bool flag = false;
			if (Main.InGameUI.CurrentState is UIVirtualKeyboard && UIVirtualKeyboard.KeyboardContext > 0)
			{
				if (!Main.inFancyUI)
				{
					Main.InGameUI.SetState(null);
				}
				if (Main.screenWidth >= 1705 || !PlayerInput.UsingGamepad)
				{
					flag = true;
				}
			}
			if (!Main.gameMenu)
			{
				Main.mouseText = false;
				if (Main.InGameUI != null && Main.InGameUI.IsElementUnderMouse())
				{
					Main.player[Main.myPlayer].mouseInterface = true;
				}
				Main.instance.GUIBarsDraw();
				if (Main.InGameUI.CurrentState is UIVirtualKeyboard && UIVirtualKeyboard.KeyboardContext > 0)
				{
					Main.instance.GUIChatDraw();
				}
				if (!Main.inFancyUI)
				{
					Main.InGameUI.SetState(null);
				}
				Main.instance.DrawMouseOver();
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.SamplerStateForCursor, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
				Main.DrawCursor(Main.DrawThickCursor(false), false);
			}
			return flag;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x004E85BC File Offset: 0x004E67BC
		public static void MouseOver()
		{
			if (!Main.inFancyUI)
			{
				return;
			}
			if (Main.InGameUI.IsElementUnderMouse())
			{
				Main.mouseText = true;
			}
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0000357B File Offset: 0x0000177B
		public IngameFancyUI()
		{
		}

		// Token: 0x0400134B RID: 4939
		private static bool CoverForOneUIFrame;

		// Token: 0x0200070A RID: 1802
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600400D RID: 16397 RVA: 0x0069D5FF File Offset: 0x0069B7FF
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600400E RID: 16398 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x0600400F RID: 16399 RVA: 0x0069D60B File Offset: 0x0069B80B
			internal void <OpenVirtualKeyboard>b__10_0(string s)
			{
				Main.SubmitSignText();
				IngameFancyUI.Close(true);
			}

			// Token: 0x06004010 RID: 16400 RVA: 0x0069D618 File Offset: 0x0069B818
			internal void <OpenVirtualKeyboard>b__10_1()
			{
				Main.InputTextSignCancel();
				IngameFancyUI.Close(true);
			}

			// Token: 0x06004011 RID: 16401 RVA: 0x0069D625 File Offset: 0x0069B825
			internal void <OpenVirtualKeyboard>b__10_2(string s)
			{
				ChestUI.RenameChestSubmit(Main.player[Main.myPlayer]);
				IngameFancyUI.Close(true);
			}

			// Token: 0x06004012 RID: 16402 RVA: 0x0069D63D File Offset: 0x0069B83D
			internal void <OpenVirtualKeyboard>b__10_3()
			{
				ChestUI.RenameChestCancel();
				IngameFancyUI.Close(true);
			}

			// Token: 0x040068B3 RID: 26803
			public static readonly IngameFancyUI.<>c <>9 = new IngameFancyUI.<>c();

			// Token: 0x040068B4 RID: 26804
			public static UIVirtualKeyboard.KeyboardSubmitEvent <>9__10_0;

			// Token: 0x040068B5 RID: 26805
			public static Action <>9__10_1;

			// Token: 0x040068B6 RID: 26806
			public static UIVirtualKeyboard.KeyboardSubmitEvent <>9__10_2;

			// Token: 0x040068B7 RID: 26807
			public static Action <>9__10_3;
		}
	}
}
