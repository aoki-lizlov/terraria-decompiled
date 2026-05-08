using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent
{
	// Token: 0x0200025E RID: 606
	public static class LucyAxeMessage
	{
		// Token: 0x0600236A RID: 9066 RVA: 0x0053E758 File Offset: 0x0053C958
		private static string GetCategoryName(LucyAxeMessage.MessageSource source)
		{
			switch (source)
			{
			default:
				return "LucyTheAxe_Idle";
			case LucyAxeMessage.MessageSource.Storage:
				return "LucyTheAxe_Storage";
			case LucyAxeMessage.MessageSource.ThrownAway:
				return "LucyTheAxe_ThrownAway";
			case LucyAxeMessage.MessageSource.PickedUp:
				return "LucyTheAxe_PickedUp";
			case LucyAxeMessage.MessageSource.ChoppedTree:
				return "LucyTheAxe_ChoppedTree";
			case LucyAxeMessage.MessageSource.ChoppedGemTree:
				return "LucyTheAxe_GemTree";
			case LucyAxeMessage.MessageSource.ChoppedCactus:
				return "LucyTheAxe_ChoppedCactus";
			}
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x0053E7B0 File Offset: 0x0053C9B0
		public static void Initialize()
		{
			ItemSlot.OnItemTransferred += LucyAxeMessage.ItemSlot_OnItemTransferred;
			Player.Hooks.OnEnterWorld += LucyAxeMessage.Hooks_OnEnterWorld;
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x0053E7D4 File Offset: 0x0053C9D4
		private static void Hooks_OnEnterWorld(Player player)
		{
			if (player == Main.LocalPlayer)
			{
				LucyAxeMessage.GiveIdleMessageCooldown();
			}
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x0053E7E4 File Offset: 0x0053C9E4
		public static void UpdateMessageCooldowns()
		{
			for (int i = 0; i < LucyAxeMessage._messageCooldownsByType.Length; i++)
			{
				if (LucyAxeMessage._messageCooldownsByType[i] > 0)
				{
					LucyAxeMessage._messageCooldownsByType[i]--;
				}
			}
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x0053E820 File Offset: 0x0053CA20
		public static void TryPlayingIdleMessage()
		{
			LucyAxeMessage.MessageSource messageSource = LucyAxeMessage.MessageSource.Idle;
			if (LucyAxeMessage._messageCooldownsByType[(int)messageSource] > 0)
			{
				return;
			}
			Player localPlayer = Main.LocalPlayer;
			LucyAxeMessage.Create(messageSource, localPlayer.Top, new Vector2(Main.rand.NextFloatDirection() * 7f, -2f + Main.rand.NextFloat() * -2f));
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x0053E878 File Offset: 0x0053CA78
		private static void ItemSlot_OnItemTransferred(ItemSlot.ItemTransferInfo info)
		{
			if (info.ItemType != 5095)
			{
				return;
			}
			bool flag = LucyAxeMessage.CountsAsStorage(info.FromContenxt);
			bool flag2 = LucyAxeMessage.CountsAsStorage(info.ToContext);
			if (flag == flag2)
			{
				return;
			}
			LucyAxeMessage.MessageSource messageSource = (flag ? LucyAxeMessage.MessageSource.PickedUp : LucyAxeMessage.MessageSource.Storage);
			if (LucyAxeMessage._messageCooldownsByType[(int)messageSource] > 0)
			{
				return;
			}
			LucyAxeMessage.PutMessageTypeOnCooldown(messageSource, 420);
			Player localPlayer = Main.LocalPlayer;
			LucyAxeMessage.Create(messageSource, localPlayer.Top, new Vector2((float)(localPlayer.direction * 7), -2f));
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x0053E8F3 File Offset: 0x0053CAF3
		private static void GiveIdleMessageCooldown()
		{
			LucyAxeMessage.PutMessageTypeOnCooldown(LucyAxeMessage.MessageSource.Idle, Main.rand.Next(7200, 14400));
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x0053E90F File Offset: 0x0053CB0F
		public static void PutMessageTypeOnCooldown(LucyAxeMessage.MessageSource source, int timeInFrames)
		{
			LucyAxeMessage._messageCooldownsByType[(int)source] = timeInFrames;
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x0053E919 File Offset: 0x0053CB19
		private static bool CountsAsStorage(int itemSlotContext)
		{
			return itemSlotContext == 3 || itemSlotContext == 6 || itemSlotContext == 15;
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x0053E92B File Offset: 0x0053CB2B
		public static void TryCreatingMessageWithCooldown(LucyAxeMessage.MessageSource messageSource, Vector2 position, Vector2 velocity, int cooldownTimeInTicks)
		{
			if (Main.netMode == 2)
			{
				return;
			}
			if (LucyAxeMessage._messageCooldownsByType[(int)messageSource] > 0)
			{
				return;
			}
			LucyAxeMessage.PutMessageTypeOnCooldown(messageSource, cooldownTimeInTicks);
			LucyAxeMessage.Create(messageSource, position, velocity);
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x0053E950 File Offset: 0x0053CB50
		public static void Create(LucyAxeMessage.MessageSource source, Vector2 position, Vector2 velocity)
		{
			if (Main.netMode == 2)
			{
				return;
			}
			LucyAxeMessage.GiveIdleMessageCooldown();
			LucyAxeMessage.SpawnPopupText(source, (int)LucyAxeMessage._variation, position, velocity);
			LucyAxeMessage.PlaySound(source, position);
			LucyAxeMessage.SpawnEmoteBubble();
			if (Main.netMode == 1)
			{
				NetMessage.SendData(141, -1, -1, null, (int)source, (float)LucyAxeMessage._variation, velocity.X, velocity.Y, (int)position.X, (int)position.Y, 0);
			}
			LucyAxeMessage._variation += 1;
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x0053E9C8 File Offset: 0x0053CBC8
		private static void SpawnEmoteBubble()
		{
			EmoteBubble.MakeLocalPlayerEmote(149);
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x0053E9D4 File Offset: 0x0053CBD4
		public static void CreateFromNet(LucyAxeMessage.MessageSource source, byte variation, Vector2 position, Vector2 velocity)
		{
			LucyAxeMessage.SpawnPopupText(source, (int)variation, position, velocity);
			LucyAxeMessage.PlaySound(source, position);
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x0053E9E6 File Offset: 0x0053CBE6
		private static void PlaySound(LucyAxeMessage.MessageSource source, Vector2 position)
		{
			SoundEngine.PlaySound(SoundID.LucyTheAxeTalk, position, 0f, 1f);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x0053EA00 File Offset: 0x0053CC00
		private static void SpawnPopupText(LucyAxeMessage.MessageSource source, int variationUnwrapped, Vector2 position, Vector2 velocity)
		{
			string textForVariation = LucyAxeMessage.GetTextForVariation(source, variationUnwrapped);
			PopupText.NewText(new AdvancedPopupRequest
			{
				Text = textForVariation,
				DurationInFrames = 420,
				Velocity = velocity,
				Color = new Color(184, 96, 98) * 1.15f
			}, position);
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x0053EA60 File Offset: 0x0053CC60
		private static string GetTextForVariation(LucyAxeMessage.MessageSource source, int variationUnwrapped)
		{
			string categoryName = LucyAxeMessage.GetCategoryName(source);
			return LanguageManager.Instance.IndexedFromCategory(categoryName, variationUnwrapped).Value;
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x0053EA85 File Offset: 0x0053CC85
		// Note: this type is marked as 'beforefieldinit'.
		static LucyAxeMessage()
		{
		}

		// Token: 0x04004D8A RID: 19850
		private static byte _variation;

		// Token: 0x04004D8B RID: 19851
		private static int[] _messageCooldownsByType = new int[7];

		// Token: 0x020007E7 RID: 2023
		public enum MessageSource
		{
			// Token: 0x04007134 RID: 28980
			Idle,
			// Token: 0x04007135 RID: 28981
			Storage,
			// Token: 0x04007136 RID: 28982
			ThrownAway,
			// Token: 0x04007137 RID: 28983
			PickedUp,
			// Token: 0x04007138 RID: 28984
			ChoppedTree,
			// Token: 0x04007139 RID: 28985
			ChoppedGemTree,
			// Token: 0x0400713A RID: 28986
			ChoppedCactus,
			// Token: 0x0400713B RID: 28987
			Count
		}
	}
}
