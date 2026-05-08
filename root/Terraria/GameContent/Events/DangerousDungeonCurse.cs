using System;
using Terraria.GameContent.Generation.Dungeon;

namespace Terraria.GameContent.Events
{
	// Token: 0x020004F6 RID: 1270
	public class DangerousDungeonCurse
	{
		// Token: 0x06003553 RID: 13651 RVA: 0x0061823C File Offset: 0x0061643C
		public static int GetProgressPlayerNeedsToMatch(Player player)
		{
			if (player.ZoneLihzhardTemple)
			{
				return DualDungeonUnbreakableWallTiers.Temple;
			}
			if (player.ZoneHallow)
			{
				return DualDungeonUnbreakableWallTiers.Hallow;
			}
			if (player.ZoneDungeon)
			{
				return DualDungeonUnbreakableWallTiers.Dungeon;
			}
			if (player.ZoneJungle)
			{
				return DualDungeonUnbreakableWallTiers.JungleBoss;
			}
			if (player.ZoneCrimson || player.ZoneCorrupt)
			{
				return DualDungeonUnbreakableWallTiers.EvilBoss;
			}
			return DualDungeonUnbreakableWallTiers.EarlyGame;
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x0061829C File Offset: 0x0061649C
		public static int GetProgressPlayerCanSafelyMatch()
		{
			if (NPC.downedMechBossAny || NPC.downedQueenSlime)
			{
				return DualDungeonUnbreakableWallTiers.Temple;
			}
			if (NPC.downedBoss3 || Main.hardMode)
			{
				return DualDungeonUnbreakableWallTiers.Hallow;
			}
			if (NPC.downedQueenBee)
			{
				return DualDungeonUnbreakableWallTiers.Dungeon;
			}
			if (NPC.downedBoss2)
			{
				return DualDungeonUnbreakableWallTiers.JungleBoss;
			}
			if (NPC.downedSlimeKing || NPC.downedBoss1)
			{
				return DualDungeonUnbreakableWallTiers.EvilBoss;
			}
			return DualDungeonUnbreakableWallTiers.EarlyGame;
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x0000357B File Offset: 0x0000177B
		public DangerousDungeonCurse()
		{
		}
	}
}
