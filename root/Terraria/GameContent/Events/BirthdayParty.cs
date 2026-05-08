using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Achievements;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Events
{
	// Token: 0x020004FA RID: 1274
	public class BirthdayParty
	{
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06003574 RID: 13684 RVA: 0x00618A99 File Offset: 0x00616C99
		public static bool PartyIsUp
		{
			get
			{
				return BirthdayParty.GenuineParty || BirthdayParty.ManualParty;
			}
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x00618AA9 File Offset: 0x00616CA9
		public static void CheckMorning()
		{
			BirthdayParty.NaturalAttempt();
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x00618AB0 File Offset: 0x00616CB0
		public static void CheckNight()
		{
			bool flag = false;
			if (BirthdayParty.GenuineParty)
			{
				flag = true;
				BirthdayParty.GenuineParty = false;
				BirthdayParty.CelebratingNPCs.Clear();
			}
			if (BirthdayParty.ManualParty)
			{
				flag = true;
				BirthdayParty.ManualParty = false;
			}
			if (flag)
			{
				Color color = new Color(255, 0, 160);
				WorldGen.BroadcastText(NetworkText.FromKey(Lang.misc[99].Key, new object[0]), color);
			}
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x00618B1C File Offset: 0x00616D1C
		private static bool CanNPCParty(NPC n)
		{
			return n.active && n.townNPC && n.aiStyle != 0 && n.type != 37 && n.type != 453 && n.type != 441 && !NPCID.Sets.IsTownPet[n.type];
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x00618B78 File Offset: 0x00616D78
		private static void NaturalAttempt()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			if (!NPC.AnyNPCs(208))
			{
				return;
			}
			if (BirthdayParty.PartyDaysOnCooldown > 0)
			{
				BirthdayParty.PartyDaysOnCooldown--;
				return;
			}
			int num = 10;
			if (Main.tenthAnniversaryWorld)
			{
				num = 7;
			}
			if (Main.rand.Next(num) != 0)
			{
				return;
			}
			List<NPC> list = new List<NPC>();
			for (int l = 0; l < Main.maxNPCs; l++)
			{
				NPC npc = Main.npc[l];
				if (BirthdayParty.CanNPCParty(npc))
				{
					list.Add(npc);
				}
			}
			if (list.Count < 5)
			{
				return;
			}
			BirthdayParty.GenuineParty = true;
			BirthdayParty.PartyDaysOnCooldown = Main.rand.Next(5, 11);
			NPC.freeCake = true;
			BirthdayParty.CelebratingNPCs.Clear();
			List<int> list2 = new List<int>();
			int num2 = 1;
			if (Main.rand.Next(5) == 0 && list.Count > 12)
			{
				num2 = 3;
			}
			else if (Main.rand.Next(3) == 0)
			{
				num2 = 2;
			}
			list = list.OrderBy((NPC i) => Main.rand.Next()).ToList<NPC>();
			for (int j = 0; j < num2; j++)
			{
				list2.Add(j);
			}
			for (int k = 0; k < list2.Count; k++)
			{
				BirthdayParty.CelebratingNPCs.Add(list[list2[k]].whoAmI);
			}
			Color color = new Color(255, 0, 160);
			if (BirthdayParty.CelebratingNPCs.Count == 3)
			{
				WorldGen.BroadcastText(NetworkText.FromKey("Game.BirthdayParty_3", new object[]
				{
					Main.npc[BirthdayParty.CelebratingNPCs[0]].GetGivenOrTypeNetName(),
					Main.npc[BirthdayParty.CelebratingNPCs[1]].GetGivenOrTypeNetName(),
					Main.npc[BirthdayParty.CelebratingNPCs[2]].GetGivenOrTypeNetName()
				}), color);
			}
			else if (BirthdayParty.CelebratingNPCs.Count == 2)
			{
				WorldGen.BroadcastText(NetworkText.FromKey("Game.BirthdayParty_2", new object[]
				{
					Main.npc[BirthdayParty.CelebratingNPCs[0]].GetGivenOrTypeNetName(),
					Main.npc[BirthdayParty.CelebratingNPCs[1]].GetGivenOrTypeNetName()
				}), color);
			}
			else
			{
				WorldGen.BroadcastText(NetworkText.FromKey("Game.BirthdayParty_1", new object[] { Main.npc[BirthdayParty.CelebratingNPCs[0]].GetGivenOrTypeNetName() }), color);
			}
			NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			BirthdayParty.CheckForAchievement();
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x00618E0C File Offset: 0x0061700C
		public static void ToggleManualParty()
		{
			bool partyIsUp = BirthdayParty.PartyIsUp;
			if (Main.netMode != 1)
			{
				BirthdayParty.ManualParty = !BirthdayParty.ManualParty;
			}
			else
			{
				NetMessage.SendData(111, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			if (partyIsUp != BirthdayParty.PartyIsUp)
			{
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				BirthdayParty.CheckForAchievement();
			}
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x00618E82 File Offset: 0x00617082
		private static void CheckForAchievement()
		{
			if (BirthdayParty.PartyIsUp)
			{
				AchievementsHelper.NotifyProgressionEvent(25);
			}
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x00618E92 File Offset: 0x00617092
		public static void WorldClear()
		{
			BirthdayParty.ManualParty = false;
			BirthdayParty.GenuineParty = false;
			BirthdayParty.PartyDaysOnCooldown = 0;
			BirthdayParty.CelebratingNPCs.Clear();
			BirthdayParty._wasCelebrating = false;
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x00618EB8 File Offset: 0x006170B8
		public static void UpdateTime()
		{
			if (BirthdayParty._wasCelebrating != BirthdayParty.PartyIsUp)
			{
				if (Main.netMode != 2)
				{
					if (BirthdayParty.PartyIsUp)
					{
						SkyManager.Instance.Activate("Party", default(Vector2), new object[0]);
					}
					else
					{
						SkyManager.Instance.Deactivate("Party", new object[0]);
					}
				}
				if (Main.netMode != 1 && BirthdayParty.CelebratingNPCs.Count > 0)
				{
					for (int i = 0; i < BirthdayParty.CelebratingNPCs.Count; i++)
					{
						if (!BirthdayParty.CanNPCParty(Main.npc[BirthdayParty.CelebratingNPCs[i]]))
						{
							BirthdayParty.CelebratingNPCs.RemoveAt(i);
						}
					}
					if (BirthdayParty.CelebratingNPCs.Count == 0)
					{
						BirthdayParty.GenuineParty = false;
						if (!BirthdayParty.ManualParty)
						{
							Color color = new Color(255, 0, 160);
							WorldGen.BroadcastText(NetworkText.FromKey(Lang.misc[99].Key, new object[0]), color);
							NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
			}
			BirthdayParty._wasCelebrating = BirthdayParty.PartyIsUp;
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x0000357B File Offset: 0x0000177B
		public BirthdayParty()
		{
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x00618FDB File Offset: 0x006171DB
		// Note: this type is marked as 'beforefieldinit'.
		static BirthdayParty()
		{
		}

		// Token: 0x04005AC4 RID: 23236
		public static bool ManualParty;

		// Token: 0x04005AC5 RID: 23237
		public static bool GenuineParty;

		// Token: 0x04005AC6 RID: 23238
		public static int PartyDaysOnCooldown;

		// Token: 0x04005AC7 RID: 23239
		public static List<int> CelebratingNPCs = new List<int>();

		// Token: 0x04005AC8 RID: 23240
		private static bool _wasCelebrating;

		// Token: 0x0200098A RID: 2442
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004966 RID: 18790 RVA: 0x006D1DA7 File Offset: 0x006CFFA7
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004967 RID: 18791 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004968 RID: 18792 RVA: 0x006D1DB3 File Offset: 0x006CFFB3
			internal int <NaturalAttempt>b__10_0(NPC i)
			{
				return Main.rand.Next();
			}

			// Token: 0x04007643 RID: 30275
			public static readonly BirthdayParty.<>c <>9 = new BirthdayParty.<>c();

			// Token: 0x04007644 RID: 30276
			public static Func<NPC, int> <>9__10_0;
		}
	}
}
