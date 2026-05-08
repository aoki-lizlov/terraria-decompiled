using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Utilities;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020002FC RID: 764
	public static class CommonCode
	{
		// Token: 0x0600269B RID: 9883 RVA: 0x0055F4C0 File Offset: 0x0055D6C0
		public static void DropItemFromNPC(NPC npc, int itemId, int stack, bool scattered = false)
		{
			if (itemId <= 0 || itemId >= (int)ItemID.Count)
			{
				return;
			}
			int num = (int)npc.position.X + npc.width / 2;
			int num2 = (int)npc.position.Y + npc.height / 2;
			if (scattered)
			{
				num = (int)npc.position.X + Main.rand.Next(npc.width + 1);
				num2 = (int)npc.position.Y + Main.rand.Next(npc.height + 1);
			}
			int num3 = Item.NewItem(npc.GetItemSource_Loot(), num, num2, 0, 0, itemId, stack, false, -1, false);
			CommonCode.ModifyItemDropFromNPC(npc, num3);
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x0055F568 File Offset: 0x0055D768
		public static void DropItemLocalPerClientAndSetNPCMoneyTo0(NPC npc, int itemId, int stack, bool interactionRequired = true)
		{
			if (itemId <= 0 || itemId >= (int)ItemID.Count)
			{
				return;
			}
			if (Main.netMode == 2)
			{
				int num = Item.NewItem(npc.GetItemSource_Loot(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, itemId, stack, true, -1, false);
				Main.timeItemSlotCannotBeReusedFor[num] = 54000;
				for (int i = 0; i < 255; i++)
				{
					if (Main.player[i].active && (npc.playerInteraction[i] || !interactionRequired))
					{
						NetMessage.SendData(90, i, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				Main.item[num].TurnToAir(false);
			}
			else
			{
				CommonCode.DropItemFromNPC(npc, itemId, stack, false);
			}
			npc.value = 0f;
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x0055F63C File Offset: 0x0055D83C
		public static void DropItemForEachInteractingPlayerOnThePlayer(NPC npc, int itemId, UnifiedRandom rng, int chanceNumerator, int chanceDenominator, int stack = 1, bool interactionRequired = true)
		{
			if (itemId <= 0 || itemId >= (int)ItemID.Count)
			{
				return;
			}
			if (Main.netMode == 2)
			{
				for (int i = 0; i < 255; i++)
				{
					Player player = Main.player[i];
					if (player.active && (npc.playerInteraction[i] || !interactionRequired) && rng.Next(chanceDenominator) < chanceNumerator)
					{
						int num = Item.NewItem(npc.GetItemSource_Loot(), player.position, player.Size, itemId, stack, false, -1, false);
						CommonCode.ModifyItemDropFromNPC(npc, num);
					}
				}
			}
			else if (rng.Next(chanceDenominator) < chanceNumerator)
			{
				CommonCode.DropItemFromNPC(npc, itemId, stack, false);
			}
			npc.value = 0f;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x0055F6E0 File Offset: 0x0055D8E0
		public static void ModifyItemDropFromNPC(NPC npc, int itemIndex)
		{
			WorldItem worldItem = Main.item[itemIndex];
			int type = worldItem.type;
			if (type != 23)
			{
				if (type != 319)
				{
					return;
				}
				switch (npc.netID)
				{
				case 542:
					worldItem.color = new Color(189, 148, 96, 255);
					NetMessage.SendData(88, -1, -1, null, itemIndex, 1f, 0f, 0f, 0, 0, 0);
					return;
				case 543:
					worldItem.color = new Color(112, 85, 89, 255);
					NetMessage.SendData(88, -1, -1, null, itemIndex, 1f, 0f, 0f, 0, 0, 0);
					return;
				case 544:
					worldItem.color = new Color(145, 27, 40, 255);
					NetMessage.SendData(88, -1, -1, null, itemIndex, 1f, 0f, 0f, 0, 0, 0);
					return;
				case 545:
					worldItem.color = new Color(158, 113, 164, 255);
					NetMessage.SendData(88, -1, -1, null, itemIndex, 1f, 0f, 0f, 0, 0, 0);
					break;
				default:
					return;
				}
			}
			else
			{
				if (npc.type == 1 && npc.netID != -1 && npc.netID != -2 && npc.netID != -5 && npc.netID != -6)
				{
					worldItem.color = npc.color;
					NetMessage.SendData(88, -1, -1, null, itemIndex, 1f, 0f, 0f, 0, 0, 0);
				}
				if (Main.remixWorld && npc.type == 59)
				{
					worldItem.color = new Color(255, 127, 0);
					NetMessage.SendData(88, -1, -1, null, itemIndex, 1f, 0f, 0f, 0, 0, 0);
					return;
				}
			}
		}
	}
}
