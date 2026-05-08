using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x02000283 RID: 643
	public class AchievementsHelper
	{
		// Token: 0x14000045 RID: 69
		// (add) Token: 0x060024C0 RID: 9408 RVA: 0x00551CB8 File Offset: 0x0054FEB8
		// (remove) Token: 0x060024C1 RID: 9409 RVA: 0x00551CEC File Offset: 0x0054FEEC
		public static event AchievementsHelper.ItemPickupEvent OnItemPickup
		{
			[CompilerGenerated]
			add
			{
				AchievementsHelper.ItemPickupEvent itemPickupEvent = AchievementsHelper.OnItemPickup;
				AchievementsHelper.ItemPickupEvent itemPickupEvent2;
				do
				{
					itemPickupEvent2 = itemPickupEvent;
					AchievementsHelper.ItemPickupEvent itemPickupEvent3 = (AchievementsHelper.ItemPickupEvent)Delegate.Combine(itemPickupEvent2, value);
					itemPickupEvent = Interlocked.CompareExchange<AchievementsHelper.ItemPickupEvent>(ref AchievementsHelper.OnItemPickup, itemPickupEvent3, itemPickupEvent2);
				}
				while (itemPickupEvent != itemPickupEvent2);
			}
			[CompilerGenerated]
			remove
			{
				AchievementsHelper.ItemPickupEvent itemPickupEvent = AchievementsHelper.OnItemPickup;
				AchievementsHelper.ItemPickupEvent itemPickupEvent2;
				do
				{
					itemPickupEvent2 = itemPickupEvent;
					AchievementsHelper.ItemPickupEvent itemPickupEvent3 = (AchievementsHelper.ItemPickupEvent)Delegate.Remove(itemPickupEvent2, value);
					itemPickupEvent = Interlocked.CompareExchange<AchievementsHelper.ItemPickupEvent>(ref AchievementsHelper.OnItemPickup, itemPickupEvent3, itemPickupEvent2);
				}
				while (itemPickupEvent != itemPickupEvent2);
			}
		}

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x060024C2 RID: 9410 RVA: 0x00551D20 File Offset: 0x0054FF20
		// (remove) Token: 0x060024C3 RID: 9411 RVA: 0x00551D54 File Offset: 0x0054FF54
		public static event AchievementsHelper.ItemCraftEvent OnItemCraft
		{
			[CompilerGenerated]
			add
			{
				AchievementsHelper.ItemCraftEvent itemCraftEvent = AchievementsHelper.OnItemCraft;
				AchievementsHelper.ItemCraftEvent itemCraftEvent2;
				do
				{
					itemCraftEvent2 = itemCraftEvent;
					AchievementsHelper.ItemCraftEvent itemCraftEvent3 = (AchievementsHelper.ItemCraftEvent)Delegate.Combine(itemCraftEvent2, value);
					itemCraftEvent = Interlocked.CompareExchange<AchievementsHelper.ItemCraftEvent>(ref AchievementsHelper.OnItemCraft, itemCraftEvent3, itemCraftEvent2);
				}
				while (itemCraftEvent != itemCraftEvent2);
			}
			[CompilerGenerated]
			remove
			{
				AchievementsHelper.ItemCraftEvent itemCraftEvent = AchievementsHelper.OnItemCraft;
				AchievementsHelper.ItemCraftEvent itemCraftEvent2;
				do
				{
					itemCraftEvent2 = itemCraftEvent;
					AchievementsHelper.ItemCraftEvent itemCraftEvent3 = (AchievementsHelper.ItemCraftEvent)Delegate.Remove(itemCraftEvent2, value);
					itemCraftEvent = Interlocked.CompareExchange<AchievementsHelper.ItemCraftEvent>(ref AchievementsHelper.OnItemCraft, itemCraftEvent3, itemCraftEvent2);
				}
				while (itemCraftEvent != itemCraftEvent2);
			}
		}

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x060024C4 RID: 9412 RVA: 0x00551D88 File Offset: 0x0054FF88
		// (remove) Token: 0x060024C5 RID: 9413 RVA: 0x00551DBC File Offset: 0x0054FFBC
		public static event AchievementsHelper.TileDestroyedEvent OnTileDestroyed
		{
			[CompilerGenerated]
			add
			{
				AchievementsHelper.TileDestroyedEvent tileDestroyedEvent = AchievementsHelper.OnTileDestroyed;
				AchievementsHelper.TileDestroyedEvent tileDestroyedEvent2;
				do
				{
					tileDestroyedEvent2 = tileDestroyedEvent;
					AchievementsHelper.TileDestroyedEvent tileDestroyedEvent3 = (AchievementsHelper.TileDestroyedEvent)Delegate.Combine(tileDestroyedEvent2, value);
					tileDestroyedEvent = Interlocked.CompareExchange<AchievementsHelper.TileDestroyedEvent>(ref AchievementsHelper.OnTileDestroyed, tileDestroyedEvent3, tileDestroyedEvent2);
				}
				while (tileDestroyedEvent != tileDestroyedEvent2);
			}
			[CompilerGenerated]
			remove
			{
				AchievementsHelper.TileDestroyedEvent tileDestroyedEvent = AchievementsHelper.OnTileDestroyed;
				AchievementsHelper.TileDestroyedEvent tileDestroyedEvent2;
				do
				{
					tileDestroyedEvent2 = tileDestroyedEvent;
					AchievementsHelper.TileDestroyedEvent tileDestroyedEvent3 = (AchievementsHelper.TileDestroyedEvent)Delegate.Remove(tileDestroyedEvent2, value);
					tileDestroyedEvent = Interlocked.CompareExchange<AchievementsHelper.TileDestroyedEvent>(ref AchievementsHelper.OnTileDestroyed, tileDestroyedEvent3, tileDestroyedEvent2);
				}
				while (tileDestroyedEvent != tileDestroyedEvent2);
			}
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x060024C6 RID: 9414 RVA: 0x00551DF0 File Offset: 0x0054FFF0
		// (remove) Token: 0x060024C7 RID: 9415 RVA: 0x00551E24 File Offset: 0x00550024
		public static event AchievementsHelper.NPCKilledEvent OnNPCKilled
		{
			[CompilerGenerated]
			add
			{
				AchievementsHelper.NPCKilledEvent npckilledEvent = AchievementsHelper.OnNPCKilled;
				AchievementsHelper.NPCKilledEvent npckilledEvent2;
				do
				{
					npckilledEvent2 = npckilledEvent;
					AchievementsHelper.NPCKilledEvent npckilledEvent3 = (AchievementsHelper.NPCKilledEvent)Delegate.Combine(npckilledEvent2, value);
					npckilledEvent = Interlocked.CompareExchange<AchievementsHelper.NPCKilledEvent>(ref AchievementsHelper.OnNPCKilled, npckilledEvent3, npckilledEvent2);
				}
				while (npckilledEvent != npckilledEvent2);
			}
			[CompilerGenerated]
			remove
			{
				AchievementsHelper.NPCKilledEvent npckilledEvent = AchievementsHelper.OnNPCKilled;
				AchievementsHelper.NPCKilledEvent npckilledEvent2;
				do
				{
					npckilledEvent2 = npckilledEvent;
					AchievementsHelper.NPCKilledEvent npckilledEvent3 = (AchievementsHelper.NPCKilledEvent)Delegate.Remove(npckilledEvent2, value);
					npckilledEvent = Interlocked.CompareExchange<AchievementsHelper.NPCKilledEvent>(ref AchievementsHelper.OnNPCKilled, npckilledEvent3, npckilledEvent2);
				}
				while (npckilledEvent != npckilledEvent2);
			}
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x060024C8 RID: 9416 RVA: 0x00551E58 File Offset: 0x00550058
		// (remove) Token: 0x060024C9 RID: 9417 RVA: 0x00551E8C File Offset: 0x0055008C
		public static event AchievementsHelper.ProgressionEventEvent OnProgressionEvent
		{
			[CompilerGenerated]
			add
			{
				AchievementsHelper.ProgressionEventEvent progressionEventEvent = AchievementsHelper.OnProgressionEvent;
				AchievementsHelper.ProgressionEventEvent progressionEventEvent2;
				do
				{
					progressionEventEvent2 = progressionEventEvent;
					AchievementsHelper.ProgressionEventEvent progressionEventEvent3 = (AchievementsHelper.ProgressionEventEvent)Delegate.Combine(progressionEventEvent2, value);
					progressionEventEvent = Interlocked.CompareExchange<AchievementsHelper.ProgressionEventEvent>(ref AchievementsHelper.OnProgressionEvent, progressionEventEvent3, progressionEventEvent2);
				}
				while (progressionEventEvent != progressionEventEvent2);
			}
			[CompilerGenerated]
			remove
			{
				AchievementsHelper.ProgressionEventEvent progressionEventEvent = AchievementsHelper.OnProgressionEvent;
				AchievementsHelper.ProgressionEventEvent progressionEventEvent2;
				do
				{
					progressionEventEvent2 = progressionEventEvent;
					AchievementsHelper.ProgressionEventEvent progressionEventEvent3 = (AchievementsHelper.ProgressionEventEvent)Delegate.Remove(progressionEventEvent2, value);
					progressionEventEvent = Interlocked.CompareExchange<AchievementsHelper.ProgressionEventEvent>(ref AchievementsHelper.OnProgressionEvent, progressionEventEvent3, progressionEventEvent2);
				}
				while (progressionEventEvent != progressionEventEvent2);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x00551EBF File Offset: 0x005500BF
		// (set) Token: 0x060024CB RID: 9419 RVA: 0x00551EC6 File Offset: 0x005500C6
		public static bool CurrentlyMining
		{
			get
			{
				return AchievementsHelper._isMining;
			}
			set
			{
				AchievementsHelper._isMining = value;
			}
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x00551ECE File Offset: 0x005500CE
		public static void NotifyTileDestroyed(Player player, ushort tile)
		{
			if (Main.gameMenu || !AchievementsHelper._isMining)
			{
				return;
			}
			if (AchievementsHelper.OnTileDestroyed != null)
			{
				AchievementsHelper.OnTileDestroyed(player, tile);
			}
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x00551EF2 File Offset: 0x005500F2
		public static void NotifyItemPickup(Player player, Item item)
		{
			if (AchievementsHelper.OnItemPickup != null)
			{
				AchievementsHelper.OnItemPickup(player, (short)item.type, item.stack);
			}
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x00551F13 File Offset: 0x00550113
		public static void NotifyItemPickup(Player player, Item item, int customStack)
		{
			if (AchievementsHelper.OnItemPickup != null)
			{
				AchievementsHelper.OnItemPickup(player, (short)item.type, customStack);
			}
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x00551F2F File Offset: 0x0055012F
		public static void NotifyItemCraft(Recipe recipe)
		{
			if (AchievementsHelper.OnItemCraft != null)
			{
				AchievementsHelper.OnItemCraft((short)recipe.createItem.type, recipe.createItem.stack);
			}
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x00551F5C File Offset: 0x0055015C
		public static void TryGrantingBestiary100PercentAchievement()
		{
			if (Main.GetBestiaryProgressReport().CompletionPercent >= 1f)
			{
				AchievementsHelper.NotifyProgressionEvent(29);
			}
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x00551F84 File Offset: 0x00550184
		public static void Initialize()
		{
			Player.Hooks.OnEnterWorld += AchievementsHelper.OnPlayerEnteredWorld;
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x00551F98 File Offset: 0x00550198
		internal static void OnPlayerEnteredWorld(Player player)
		{
			if (AchievementsHelper.OnItemPickup != null)
			{
				for (int i = 0; i < 58; i++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.inventory[i].type, player.inventory[i].stack);
				}
				for (int j = 0; j < player.armor.Length; j++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.armor[j].type, player.armor[j].stack);
				}
				for (int k = 0; k < player.dye.Length; k++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.dye[k].type, player.dye[k].stack);
				}
				for (int l = 0; l < player.miscEquips.Length; l++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.miscEquips[l].type, player.miscEquips[l].stack);
				}
				for (int m = 0; m < player.miscDyes.Length; m++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.miscDyes[m].type, player.miscDyes[m].stack);
				}
				for (int n = 0; n < player.bank.item.Length; n++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.bank.item[n].type, player.bank.item[n].stack);
				}
				for (int num = 0; num < player.bank2.item.Length; num++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.bank2.item[num].type, player.bank2.item[num].stack);
				}
				for (int num2 = 0; num2 < player.bank3.item.Length; num2++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.bank3.item[num2].type, player.bank3.item[num2].stack);
				}
				for (int num3 = 0; num3 < player.bank4.item.Length; num3++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.bank4.item[num3].type, player.bank4.item[num3].stack);
				}
				for (int num4 = 0; num4 < player.Loadouts.Length; num4++)
				{
					Item[] array = player.Loadouts[num4].Armor;
					for (int num5 = 0; num5 < array.Length; num5++)
					{
						AchievementsHelper.OnItemPickup(player, (short)array[num5].type, array[num5].stack);
					}
					array = player.Loadouts[num4].Dye;
					for (int num6 = 0; num6 < array.Length; num6++)
					{
						AchievementsHelper.OnItemPickup(player, (short)array[num6].type, array[num6].stack);
					}
				}
			}
			if (player.statManaMax > 20)
			{
				Main.Achievements.GetCondition("STAR_POWER", "Use").Complete();
			}
			if (player.statLifeMax == 500 && player.statManaMax == 200)
			{
				Main.Achievements.GetCondition("TOPPED_OFF", "Use").Complete();
			}
			if (player.miscEquips[4].type > 0)
			{
				Main.Achievements.GetCondition("HOLD_ON_TIGHT", "Equip").Complete();
			}
			if (player.miscEquips[3].type > 0)
			{
				Main.Achievements.GetCondition("THE_CAVALRY", "Equip").Complete();
			}
			for (int num7 = 0; num7 < player.armor.Length; num7++)
			{
				if (player.armor[num7].wingSlot > 0)
				{
					Main.Achievements.GetCondition("HEAD_IN_THE_CLOUDS", "Equip").Complete();
					break;
				}
			}
			if (player.armor[0].stack > 0 && player.armor[1].stack > 0 && player.armor[2].stack > 0)
			{
				Main.Achievements.GetCondition("MATCHING_ATTIRE", "Equip").Complete();
			}
			if (player.armor[10].stack > 0 && player.armor[11].stack > 0 && player.armor[12].stack > 0)
			{
				Main.Achievements.GetCondition("FASHION_STATEMENT", "Equip").Complete();
			}
			bool flag = true;
			for (int num8 = 0; num8 < 10; num8++)
			{
				if (player.IsItemSlotUnlockedAndUsable(num8) && (player.dye[num8].type < 1 || player.dye[num8].stack < 1))
				{
					flag = false;
				}
			}
			if (flag)
			{
				Main.Achievements.GetCondition("DYE_HARD", "Equip").Complete();
			}
			if (player.unlockedBiomeTorches)
			{
				Main.Achievements.GetCondition("GAIN_TORCH_GODS_FAVOR", "Use").Complete();
			}
			WorldGen.CheckAchievement_RealEstateAndTownSlimes();
			AchievementsHelper.TryGrantingBestiary100PercentAchievement();
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x005524B8 File Offset: 0x005506B8
		public static void NotifyNPCKilled(NPC npc)
		{
			if (Main.netMode == 0)
			{
				if (npc.playerInteraction[Main.myPlayer])
				{
					AchievementsHelper.NotifyNPCKilledDirect(Main.player[Main.myPlayer], npc.netID);
					return;
				}
			}
			else
			{
				for (int i = 0; i < 255; i++)
				{
					if (npc.playerInteraction[i])
					{
						NetMessage.SendData(97, i, -1, null, npc.netID, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x0055252D File Offset: 0x0055072D
		public static void NotifyNPCKilledDirect(Player player, int npcNetID)
		{
			if (AchievementsHelper.OnNPCKilled != null)
			{
				AchievementsHelper.OnNPCKilled(player, (short)npcNetID);
			}
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x00552544 File Offset: 0x00550744
		public static void NotifyProgressionEvent(int eventID)
		{
			if (Main.netMode == 2)
			{
				NetMessage.SendData(98, -1, -1, null, eventID, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			if (AchievementsHelper.OnProgressionEvent != null)
			{
				AchievementsHelper.OnProgressionEvent(eventID);
			}
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x0055258C File Offset: 0x0055078C
		public static void HandleOnEquip(Player player, Item item, int context)
		{
			if (context == 16)
			{
				Main.Achievements.GetCondition("HOLD_ON_TIGHT", "Equip").Complete();
			}
			if (context == 17)
			{
				Main.Achievements.GetCondition("THE_CAVALRY", "Equip").Complete();
			}
			if ((context == 10 || context == 11) && item.wingSlot > 0)
			{
				Main.Achievements.GetCondition("HEAD_IN_THE_CLOUDS", "Equip").Complete();
			}
			if (context == 8 && player.armor[0].stack > 0 && player.armor[1].stack > 0 && player.armor[2].stack > 0)
			{
				Main.Achievements.GetCondition("MATCHING_ATTIRE", "Equip").Complete();
			}
			if (context == 9 && player.armor[10].stack > 0 && player.armor[11].stack > 0 && player.armor[12].stack > 0)
			{
				Main.Achievements.GetCondition("FASHION_STATEMENT", "Equip").Complete();
			}
			if (context == 12 || context == 33)
			{
				for (int i = 0; i < 10; i++)
				{
					if (player.IsItemSlotUnlockedAndUsable(i) && (player.dye[i].type < 1 || player.dye[i].stack < 1))
					{
						return;
					}
				}
				for (int j = 0; j < player.miscDyes.Length; j++)
				{
					if (player.miscDyes[j].type < 1 || player.miscDyes[j].stack < 1)
					{
						return;
					}
				}
				Main.Achievements.GetCondition("DYE_HARD", "Equip").Complete();
			}
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x00552730 File Offset: 0x00550930
		public static void HandleSpecialEvent(Player player, int eventID)
		{
			if (player.whoAmI != Main.myPlayer)
			{
				return;
			}
			switch (eventID)
			{
			case 1:
				Main.Achievements.GetCondition("STAR_POWER", "Use").Complete();
				if (player.statLifeMax == 500 && player.statManaMax == 200)
				{
					Main.Achievements.GetCondition("TOPPED_OFF", "Use").Complete();
					return;
				}
				break;
			case 2:
				Main.Achievements.GetCondition("GET_A_LIFE", "Use").Complete();
				if (player.statLifeMax == 500 && player.statManaMax == 200)
				{
					Main.Achievements.GetCondition("TOPPED_OFF", "Use").Complete();
					return;
				}
				break;
			case 3:
				Main.Achievements.GetCondition("NOT_THE_BEES", "Use").Complete();
				return;
			case 4:
				Main.Achievements.GetCondition("WATCH_YOUR_STEP", "Hit").Complete();
				return;
			case 5:
				Main.Achievements.GetCondition("RAINBOWS_AND_UNICORNS", "Use").Complete();
				return;
			case 6:
				Main.Achievements.GetCondition("YOU_AND_WHAT_ARMY", "Spawn").Complete();
				return;
			case 7:
				Main.Achievements.GetCondition("THROWING_LINES", "Use").Complete();
				return;
			case 8:
				Main.Achievements.GetCondition("LUCKY_BREAK", "Hit").Complete();
				return;
			case 9:
				Main.Achievements.GetCondition("VEHICULAR_MANSLAUGHTER", "Hit").Complete();
				return;
			case 10:
				Main.Achievements.GetCondition("ROCK_BOTTOM", "Reach").Complete();
				return;
			case 11:
				Main.Achievements.GetCondition("INTO_ORBIT", "Reach").Complete();
				return;
			case 12:
				Main.Achievements.GetCondition("WHERES_MY_HONEY", "Reach").Complete();
				return;
			case 13:
				Main.Achievements.GetCondition("JEEPERS_CREEPERS", "Reach").Complete();
				return;
			case 14:
				Main.Achievements.GetCondition("ITS_GETTING_HOT_IN_HERE", "Reach").Complete();
				return;
			case 15:
				Main.Achievements.GetCondition("FUNKYTOWN", "Reach").Complete();
				return;
			case 16:
				Main.Achievements.GetCondition("I_AM_LOOT", "Peek").Complete();
				return;
			case 17:
				Main.Achievements.GetCondition("FLY_A_KITE_ON_A_WINDY_DAY", "Use").Complete();
				return;
			case 18:
				Main.Achievements.GetCondition("FOUND_GRAVEYARD", "Reach").Complete();
				return;
			case 19:
				Main.Achievements.GetCondition("GO_LAVA_FISHING", "Do").Complete();
				return;
			case 20:
				Main.Achievements.GetCondition("TALK_TO_NPC_AT_MAX_HAPPINESS", "Do").Complete();
				return;
			case 21:
				Main.Achievements.GetCondition("PET_THE_PET", "Do").Complete();
				return;
			case 22:
				Main.Achievements.GetCondition("FIND_A_FAIRY", "Do").Complete();
				return;
			case 23:
				Main.Achievements.GetCondition("DIE_TO_DEAD_MANS_CHEST", "Do").Complete();
				return;
			case 24:
				Main.Achievements.GetCondition("GAIN_TORCH_GODS_FAVOR", "Use").Complete();
				return;
			case 25:
				Main.Achievements.GetCondition("DRINK_BOTTLED_WATER_WHILE_DROWNING", "Use").Complete();
				return;
			case 26:
				Main.Achievements.GetCondition("PLAY_ON_A_SPECIAL_SEED", "Do").Complete();
				return;
			case 27:
				Main.Achievements.GetCondition("PURIFY_ENTIRE_WORLD", "Do").Complete();
				break;
			default:
				return;
			}
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x00552AEF File Offset: 0x00550CEF
		public static void DoClassicTitleScreenAchievement()
		{
			Main.Achievements.GetCondition("GOING_OLDSCHOOL", "Do").Complete();
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x00552B0C File Offset: 0x00550D0C
		public static void CheckResearchAchievement(bool forced = false)
		{
			int lastEditId = Main.LocalPlayerCreativeTracker.ItemSacrifices.LastEditId;
			if (forced || AchievementsHelper._lastResearchVersion != lastEditId)
			{
				AchievementsHelper._lastResearchVersion = lastEditId;
				int num;
				int num2;
				Main.LocalPlayerCreativeTracker.ItemSacrifices.CountFullyResearchedItems(out num, out num2);
				if (num >= num2 / 2)
				{
					AchievementsHelper.NotifyProgressionEvent(45);
				}
			}
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x00552B5C File Offset: 0x00550D5C
		public static void PlantedAcorn()
		{
			CustomIntCondition customIntCondition = (CustomIntCondition)Main.Achievements.GetCondition("CONSERVATIONIST", "Do");
			int value = customIntCondition.Value;
			customIntCondition.Value = value + 1;
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x00552B91 File Offset: 0x00550D91
		public static void HandleNurseService(int coinsSpent)
		{
			((CustomFloatCondition)Main.Achievements.GetCondition("FREQUENT_FLYER", "Pay")).Value += (float)coinsSpent;
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x00552BBC File Offset: 0x00550DBC
		public static void HandleAnglerService()
		{
			Main.Achievements.GetCondition("SERVANT_IN_TRAINING", "Finish").Complete();
			CustomIntCondition customIntCondition = (CustomIntCondition)Main.Achievements.GetCondition("GOOD_LITTLE_SLAVE", "Finish");
			int num = customIntCondition.Value;
			customIntCondition.Value = num + 1;
			CustomIntCondition customIntCondition2 = (CustomIntCondition)Main.Achievements.GetCondition("TROUT_MONKEY", "Finish");
			num = customIntCondition2.Value;
			customIntCondition2.Value = num + 1;
			CustomIntCondition customIntCondition3 = (CustomIntCondition)Main.Achievements.GetCondition("FAST_AND_FISHIOUS", "Finish");
			num = customIntCondition3.Value;
			customIntCondition3.Value = num + 1;
			CustomIntCondition customIntCondition4 = (CustomIntCondition)Main.Achievements.GetCondition("SUPREME_HELPER_MINION", "Finish");
			num = customIntCondition4.Value;
			customIntCondition4.Value = num + 1;
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x00552C82 File Offset: 0x00550E82
		public static void HandleRunning(float pixelsMoved)
		{
			((CustomFloatCondition)Main.Achievements.GetCondition("MARATHON_MEDALIST", "Move")).Value += pixelsMoved;
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x00552CAC File Offset: 0x00550EAC
		public static void HandleMining()
		{
			CustomIntCondition customIntCondition = (CustomIntCondition)Main.Achievements.GetCondition("BULLDOZER", "Pick");
			int value = customIntCondition.Value;
			customIntCondition.Value = value + 1;
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x00552CE4 File Offset: 0x00550EE4
		public static void MechaMayhem_Clear()
		{
			bool flag;
			bool flag2;
			bool flag3;
			AchievementsHelper.ScanForMechs(out flag, out flag2, out flag3);
			if (!flag && !flag2 && !flag3)
			{
				AchievementsHelper.mayhem1down = false;
				AchievementsHelper.mayhem2down = false;
				AchievementsHelper.mayhem3down = false;
			}
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x00552D18 File Offset: 0x00550F18
		public static void MechaMayhem_Start()
		{
			bool flag;
			bool flag2;
			bool flag3;
			AchievementsHelper.ScanForMechs(out flag, out flag2, out flag3);
			AchievementsHelper.mayhemOK = flag && flag2 && flag3;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x00552D3C File Offset: 0x00550F3C
		public static void MechaMayhem_Kill(int justKilled)
		{
			if (!AchievementsHelper.mayhemOK)
			{
				return;
			}
			if (justKilled == 125 || justKilled == 126)
			{
				AchievementsHelper.mayhem1down = true;
			}
			else if (!NPC.AnyNPCs(125) && !NPC.AnyNPCs(126) && !AchievementsHelper.mayhem1down)
			{
				AchievementsHelper.mayhemOK = false;
				return;
			}
			if (justKilled == 134)
			{
				AchievementsHelper.mayhem2down = true;
			}
			else if (!NPC.AnyNPCs(134) && !AchievementsHelper.mayhem2down)
			{
				AchievementsHelper.mayhemOK = false;
				return;
			}
			if (justKilled == 127)
			{
				AchievementsHelper.mayhem3down = true;
			}
			else if (!NPC.AnyNPCs(127) && !AchievementsHelper.mayhem3down)
			{
				AchievementsHelper.mayhemOK = false;
				return;
			}
			if (AchievementsHelper.mayhem1down && AchievementsHelper.mayhem2down && AchievementsHelper.mayhem3down)
			{
				AchievementsHelper.NotifyProgressionEvent(21);
			}
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x00552DF0 File Offset: 0x00550FF0
		private static void ScanForMechs(out bool foundSkeletronPrime, out bool foundDestroyer, out bool foundTwins)
		{
			foundSkeletronPrime = false;
			foundDestroyer = false;
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active)
				{
					int type = npc.type;
					foundSkeletronPrime |= type == 127;
					foundDestroyer |= type == 134;
					flag |= type == 126;
					flag2 |= type == 125;
				}
			}
			foundTwins = flag && flag2;
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x0000357B File Offset: 0x0000177B
		public AchievementsHelper()
		{
		}

		// Token: 0x04004F64 RID: 20324
		[CompilerGenerated]
		private static AchievementsHelper.ItemPickupEvent OnItemPickup;

		// Token: 0x04004F65 RID: 20325
		[CompilerGenerated]
		private static AchievementsHelper.ItemCraftEvent OnItemCraft;

		// Token: 0x04004F66 RID: 20326
		[CompilerGenerated]
		private static AchievementsHelper.TileDestroyedEvent OnTileDestroyed;

		// Token: 0x04004F67 RID: 20327
		[CompilerGenerated]
		private static AchievementsHelper.NPCKilledEvent OnNPCKilled;

		// Token: 0x04004F68 RID: 20328
		[CompilerGenerated]
		private static AchievementsHelper.ProgressionEventEvent OnProgressionEvent;

		// Token: 0x04004F69 RID: 20329
		private static bool _isMining;

		// Token: 0x04004F6A RID: 20330
		private static int _lastResearchVersion;

		// Token: 0x04004F6B RID: 20331
		private static bool mayhemOK;

		// Token: 0x04004F6C RID: 20332
		private static bool mayhem1down;

		// Token: 0x04004F6D RID: 20333
		private static bool mayhem2down;

		// Token: 0x04004F6E RID: 20334
		private static bool mayhem3down;

		// Token: 0x0200080A RID: 2058
		// (Invoke) Token: 0x060042D6 RID: 17110
		public delegate void ItemPickupEvent(Player player, short itemId, int count);

		// Token: 0x0200080B RID: 2059
		// (Invoke) Token: 0x060042DA RID: 17114
		public delegate void ItemCraftEvent(short itemId, int count);

		// Token: 0x0200080C RID: 2060
		// (Invoke) Token: 0x060042DE RID: 17118
		public delegate void TileDestroyedEvent(Player player, ushort tileId);

		// Token: 0x0200080D RID: 2061
		// (Invoke) Token: 0x060042E2 RID: 17122
		public delegate void NPCKilledEvent(Player player, short npcId);

		// Token: 0x0200080E RID: 2062
		// (Invoke) Token: 0x060042E6 RID: 17126
		public delegate void ProgressionEventEvent(int eventID);
	}
}
