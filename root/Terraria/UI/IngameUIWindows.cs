using System;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.Graphics.Capture;

namespace Terraria.UI
{
	// Token: 0x020000DF RID: 223
	public static class IngameUIWindows
	{
		// Token: 0x0600189C RID: 6300 RVA: 0x004E328C File Offset: 0x004E148C
		public static void CloseAll(bool quiet = false)
		{
			if (Main.mapFullscreen)
			{
				Main.mapFullscreen = false;
				if (!quiet)
				{
					SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				}
			}
			if (PlayerInput.InBuildingMode)
			{
				PlayerInput.ExitBuildingMode(quiet);
			}
			if (Main.ingameOptionsWindow)
			{
				IngameOptions.Close(quiet);
			}
			if (Main.inFancyUI)
			{
				IngameFancyUI.Close(quiet);
			}
			CaptureManager.Instance.Active = false;
			if (Main.LocalPlayer.talkNPC >= 0)
			{
				Main.LocalPlayer.SetTalkNPC(-1);
				Main.npcChatCornerItem = 0;
				Main.npcChatText = "";
				if (!quiet)
				{
					SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				}
			}
			Main.LocalPlayer.CloseSign(quiet);
			Main.CancelHairWindow(quiet);
			Main.CancelClothesWindow(quiet);
			if (Main.LocalPlayer.tileEntityAnchor.InUse)
			{
				Main.LocalPlayer.tileEntityAnchor.Clear();
			}
			if (Main.LocalPlayer.chest != -1)
			{
				Main.LocalPlayer.chest = -1;
				if (!quiet)
				{
					SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				}
			}
			if (Main.playerInventory)
			{
				Main.playerInventory = false;
				if (!quiet)
				{
					SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				}
			}
			Main.SetNPCShopIndex(0);
			Main.CreativeMenu.CloseMenu();
			NewCraftingUI.Close(true, false);
			CraftingUI.ClearHacks();
		}
	}
}
