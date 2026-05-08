using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.Achievements;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Events
{
	// Token: 0x020004FB RID: 1275
	public class DD2Event
	{
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600357F RID: 13695 RVA: 0x00618FE7 File Offset: 0x006171E7
		public static bool ReadyToFindBartender
		{
			get
			{
				return NPC.downedBoss2;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06003580 RID: 13696 RVA: 0x00618FEE File Offset: 0x006171EE
		public static bool DownedInvasionAnyDifficulty
		{
			get
			{
				return DD2Event.DownedInvasionT1 || DD2Event.DownedInvasionT2 || DD2Event.DownedInvasionT3;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06003581 RID: 13697 RVA: 0x00619005 File Offset: 0x00617205
		// (set) Token: 0x06003582 RID: 13698 RVA: 0x0061900C File Offset: 0x0061720C
		public static int TimeLeftBetweenWaves
		{
			get
			{
				return DD2Event._timeLeftUntilSpawningBegins;
			}
			set
			{
				DD2Event._timeLeftUntilSpawningBegins = value;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06003583 RID: 13699 RVA: 0x00619014 File Offset: 0x00617214
		public static bool EnemySpawningIsOnHold
		{
			get
			{
				return DD2Event._timeLeftUntilSpawningBegins != 0;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06003584 RID: 13700 RVA: 0x0061901E File Offset: 0x0061721E
		public static bool EnemiesShouldChasePlayers
		{
			get
			{
				return DD2Event.Ongoing || true;
			}
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x0061902A File Offset: 0x0061722A
		public static void Save(BinaryWriter writer)
		{
			writer.Write(DD2Event.DownedInvasionT1);
			writer.Write(DD2Event.DownedInvasionT2);
			writer.Write(DD2Event.DownedInvasionT3);
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x00619050 File Offset: 0x00617250
		public static void Load(BinaryReader reader, int gameVersionNumber)
		{
			if (gameVersionNumber < 178)
			{
				NPC.savedBartender = false;
				DD2Event.ResetProgressEntirely();
				return;
			}
			NPC.savedBartender = reader.ReadBoolean();
			DD2Event.DownedInvasionT1 = reader.ReadBoolean();
			DD2Event.DownedInvasionT2 = reader.ReadBoolean();
			DD2Event.DownedInvasionT3 = reader.ReadBoolean();
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x0061909D File Offset: 0x0061729D
		public static void ResetProgressEntirely()
		{
			DD2Event.DownedInvasionT1 = (DD2Event.DownedInvasionT2 = (DD2Event.DownedInvasionT3 = false));
			DD2Event.Ongoing = false;
			DD2Event.ArenaHitbox = default(Rectangle);
			DD2Event._arenaHitboxingCooldown = 0;
			DD2Event._timeLeftUntilSpawningBegins = 0;
			DD2Event._damageTracker = null;
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x006190D4 File Offset: 0x006172D4
		public static void ReportEventProgress()
		{
			int num;
			int num2;
			int num3;
			DD2Event.GetInvasionStatus(out num, out num2, out num3, false);
			Main.ReportInvasionProgress(num3, num2, 3, num);
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x006190F8 File Offset: 0x006172F8
		public static void SyncInvasionProgress(int toWho)
		{
			int num;
			int num2;
			int num3;
			DD2Event.GetInvasionStatus(out num, out num2, out num3, false);
			NetMessage.SendData(78, toWho, -1, null, num3, (float)num2, 3f, (float)num, 0, 0, 0);
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x00619128 File Offset: 0x00617328
		public static void UpdateTime()
		{
			if (!DD2Event.Ongoing && !Main.dedServ)
			{
				Filters.Scene.Deactivate("CrystalDestructionVortex", new object[0]);
				Filters.Scene.Deactivate("CrystalDestructionColor", new object[0]);
				Filters.Scene.Deactivate("CrystalWin", new object[0]);
				return;
			}
			if (Main.netMode != 1 && !NPC.AnyNPCs(548))
			{
				DD2Event.StopInvasion(false);
			}
			if (Main.netMode == 1)
			{
				if (DD2Event._timeLeftUntilSpawningBegins > 0)
				{
					DD2Event._timeLeftUntilSpawningBegins--;
				}
				if (DD2Event._timeLeftUntilSpawningBegins < 0)
				{
					DD2Event._timeLeftUntilSpawningBegins = 0;
				}
				return;
			}
			if (DD2Event._timeLeftUntilSpawningBegins > 0)
			{
				DD2Event._timeLeftUntilSpawningBegins--;
				if (DD2Event._timeLeftUntilSpawningBegins == 0)
				{
					int num;
					int num2;
					int num3;
					DD2Event.GetInvasionStatus(out num, out num2, out num3, false);
					if (!DD2Event.LostThisRun)
					{
						WorldGen.BroadcastText(Lang.GetInvasionWaveText(num, DD2Event.GetEnemiesForWave(num)), DD2Event.INFO_NEW_WAVE_COLOR);
						if (num == 7 && DD2Event.OngoingDifficulty == 3)
						{
							DD2Event.SummonBetsy();
						}
					}
					else
					{
						DD2Event.LoseInvasionMessage();
					}
					if (Main.netMode != 1)
					{
						Main.ReportInvasionProgress(num3, num2, 3, num);
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendData(78, -1, -1, null, Main.invasionProgress, (float)Main.invasionProgressMax, 3f, (float)num, 0, 0, 0);
					}
				}
			}
			if (DD2Event._timeLeftUntilSpawningBegins < 0)
			{
				DD2Event._timeLeftUntilSpawningBegins = 0;
			}
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x00619270 File Offset: 0x00617470
		public static void StartInvasion(int difficultyOverride = -1)
		{
			if (Main.netMode != 1)
			{
				DD2Event._crystalsDropping_toDrop = 0;
				DD2Event._crystalsDropping_alreadyDropped = 0;
				DD2Event._crystalsDropping_lastWave = 0;
				DD2Event._timeLeftUntilSpawningBegins = 0;
				DD2Event.Ongoing = true;
				DD2Event.FindProperDifficulty();
				if (difficultyOverride != -1)
				{
					DD2Event.OngoingDifficulty = difficultyOverride;
				}
				DD2Event._deadGoblinSpots.Clear();
				DD2Event._downedDarkMageT1 = false;
				DD2Event._downedOgreT2 = false;
				DD2Event._spawnedBetsyT3 = false;
				DD2Event.LostThisRun = false;
				DD2Event.WonThisRun = false;
				NPC.totalInvasionPoints = 0f;
				NPC.waveKills = 0f;
				NPC.waveNumber = 1;
				DD2Event.ClearAllTowersInGame();
				NPCDamageTracker.Start(DD2Event._damageTracker = new DD2Event.DamageTracker());
				WorldGen.BroadcastText(NetworkText.FromKey("DungeonDefenders2.InvasionStart", new object[0]), DD2Event.INFO_START_INVASION_COLOR);
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				if (Main.netMode != 1)
				{
					Main.ReportInvasionProgress(0, 1, 3, 1);
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(78, -1, -1, null, 0, 1f, 3f, 1f, 0, 0, 0);
				}
				DD2Event.SetEnemySpawningOnHold(300);
				DD2Event.WipeEntities();
			}
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x00619388 File Offset: 0x00617588
		public static void StopInvasion(bool win = false)
		{
			if (DD2Event.Ongoing)
			{
				if (win)
				{
					DD2Event.WinInvasionInternal();
				}
				DD2Event.Ongoing = false;
				DD2Event._deadGoblinSpots.Clear();
				if (Main.netMode != 1)
				{
					NPC.totalInvasionPoints = 0f;
					NPC.waveKills = 0f;
					NPC.waveNumber = 0;
					DD2Event.WipeEntities();
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x006193F8 File Offset: 0x006175F8
		private static void WinInvasionInternal()
		{
			if (DD2Event.OngoingDifficulty >= 1)
			{
				DD2Event.DownedInvasionT1 = true;
			}
			if (DD2Event.OngoingDifficulty >= 2)
			{
				DD2Event.DownedInvasionT2 = true;
			}
			if (DD2Event.OngoingDifficulty >= 3)
			{
				DD2Event.DownedInvasionT3 = true;
			}
			if (DD2Event.OngoingDifficulty == 1)
			{
				DD2Event.DropMedals(3);
			}
			if (DD2Event.OngoingDifficulty == 2)
			{
				DD2Event.DropMedals(15);
			}
			if (DD2Event.OngoingDifficulty == 3)
			{
				AchievementsHelper.NotifyProgressionEvent(23);
				DD2Event.DropMedals(60);
			}
			WorldGen.BroadcastText(NetworkText.FromKey("DungeonDefenders2.InvasionWin", new object[0]), DD2Event.INFO_START_INVASION_COLOR);
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x0061947C File Offset: 0x0061767C
		public static void LoseInvasionMessage()
		{
			WorldGen.BroadcastText(NetworkText.FromKey("DungeonDefenders2.InvasionLose", new object[0]), DD2Event.INFO_FAILURE_INVASION_COLOR);
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600358F RID: 13711 RVA: 0x00619498 File Offset: 0x00617698
		public static bool ReadyForTier2
		{
			get
			{
				return Main.hardMode && NPC.downedMechBossAny;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06003590 RID: 13712 RVA: 0x006194A8 File Offset: 0x006176A8
		public static bool ReadyForTier3
		{
			get
			{
				return Main.hardMode && NPC.downedGolemBoss;
			}
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x006194B8 File Offset: 0x006176B8
		private static void FindProperDifficulty()
		{
			DD2Event.OngoingDifficulty = 1;
			if (DD2Event.ReadyForTier2)
			{
				DD2Event.OngoingDifficulty = 2;
			}
			if (DD2Event.ReadyForTier3)
			{
				DD2Event.OngoingDifficulty = 3;
			}
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x006194DC File Offset: 0x006176DC
		public static void CheckProgress(int slainMonsterID)
		{
			if (Main.netMode == 1)
			{
				return;
			}
			if (!DD2Event.Ongoing)
			{
				return;
			}
			if (DD2Event.LostThisRun || DD2Event.WonThisRun)
			{
				return;
			}
			if (DD2Event.EnemySpawningIsOnHold)
			{
				return;
			}
			int num;
			int num2;
			int num3;
			DD2Event.GetInvasionStatus(out num, out num2, out num3, false);
			float num4 = (float)DD2Event.GetMonsterPointsWorth(slainMonsterID);
			float waveKills = NPC.waveKills;
			NPC.waveKills += num4;
			NPC.totalInvasionPoints += num4;
			num3 += (int)num4;
			bool flag = false;
			int num5 = num;
			if (NPC.waveKills >= (float)num2 && num2 != 0)
			{
				NPC.waveKills = 0f;
				NPC.waveNumber++;
				flag = true;
				DD2Event.GetInvasionStatus(out num, out num2, out num3, true);
				if (DD2Event.WonThisRun)
				{
					if ((float)num3 != waveKills && num4 != 0f)
					{
						if (Main.netMode != 1)
						{
							Main.ReportInvasionProgress(num3, num2, 3, num);
						}
						if (Main.netMode == 2)
						{
							NetMessage.SendData(78, -1, -1, null, Main.invasionProgress, (float)Main.invasionProgressMax, 3f, (float)num, 0, 0, 0);
						}
					}
					return;
				}
				int num6 = num;
				string text = "DungeonDefenders2.WaveComplete";
				if (num6 == 2)
				{
					text = "DungeonDefenders2.WaveCompleteFirst";
				}
				WorldGen.BroadcastText(NetworkText.FromKey(text, new object[0]), DD2Event.INFO_NEW_WAVE_COLOR);
				DD2Event.SetEnemySpawningOnHold(1800);
				if (DD2Event.OngoingDifficulty == 1)
				{
					if (num6 == 5)
					{
						DD2Event.DropMedals(1);
					}
					if (num6 == 4)
					{
						DD2Event.DropMedals(1);
					}
				}
				if (DD2Event.OngoingDifficulty == 2)
				{
					if (num6 == 7)
					{
						DD2Event.DropMedals(6);
					}
					if (num6 == 6)
					{
						DD2Event.DropMedals(3);
					}
					if (num6 == 5)
					{
						DD2Event.DropMedals(1);
					}
				}
				if (DD2Event.OngoingDifficulty == 3)
				{
					if (num6 == 7)
					{
						DD2Event.DropMedals(25);
					}
					if (num6 == 6)
					{
						DD2Event.DropMedals(11);
					}
					if (num6 == 5)
					{
						DD2Event.DropMedals(3);
					}
					if (num6 == 4)
					{
						DD2Event.DropMedals(1);
					}
				}
			}
			if ((float)num3 != waveKills)
			{
				if (flag)
				{
					int num7 = 1;
					int num8 = 1;
					if (Main.netMode != 1)
					{
						Main.ReportInvasionProgress(num7, num8, 3, num5);
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendData(78, -1, -1, null, num7, (float)num8, 3f, (float)num5, 0, 0, 0);
						return;
					}
				}
				else
				{
					if (Main.netMode != 1)
					{
						Main.ReportInvasionProgress(num3, num2, 3, num);
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendData(78, -1, -1, null, Main.invasionProgress, (float)Main.invasionProgressMax, 3f, (float)num, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x00619708 File Offset: 0x00617908
		public static void StartVictoryScene()
		{
			DD2Event.WonThisRun = true;
			if (DD2Event._damageTracker != null)
			{
				DD2Event._damageTracker.Stop(true);
			}
			int num = NPC.FindFirstNPC(548);
			if (num == -1)
			{
				return;
			}
			Main.npc[num].ai[1] = 2f;
			Main.npc[num].ai[0] = 2f;
			Main.npc[num].netUpdate = true;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (Main.npc[i] != null && Main.npc[i].active && Main.npc[i].type == 549)
				{
					Main.npc[i].ai[0] = 0f;
					Main.npc[i].ai[1] = 1f;
					Main.npc[i].netUpdate = true;
				}
			}
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x006197DD File Offset: 0x006179DD
		public static void ReportLoss()
		{
			if (DD2Event._damageTracker != null)
			{
				DD2Event._damageTracker.Stop(false);
			}
			DD2Event.LostThisRun = true;
			DD2Event.SetEnemySpawningOnHold(30);
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x00619800 File Offset: 0x00617A00
		private static void GetInvasionStatus(out int currentWave, out int requiredKillCount, out int currentKillCount, bool currentlyInCheckProgress = false)
		{
			currentWave = NPC.waveNumber;
			requiredKillCount = 10;
			currentKillCount = (int)NPC.waveKills;
			int ongoingDifficulty = DD2Event.OngoingDifficulty;
			if (ongoingDifficulty == 2)
			{
				requiredKillCount = DD2Event.Difficulty_2_GetRequiredWaveKills(ref currentWave, ref currentKillCount, currentlyInCheckProgress);
				return;
			}
			if (ongoingDifficulty == 3)
			{
				requiredKillCount = DD2Event.Difficulty_3_GetRequiredWaveKills(ref currentWave, ref currentKillCount, currentlyInCheckProgress);
				return;
			}
			requiredKillCount = DD2Event.Difficulty_1_GetRequiredWaveKills(ref currentWave, ref currentKillCount, currentlyInCheckProgress);
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x00619850 File Offset: 0x00617A50
		private static short[] GetEnemiesForWave(int wave)
		{
			int ongoingDifficulty = DD2Event.OngoingDifficulty;
			if (ongoingDifficulty == 2)
			{
				return DD2Event.Difficulty_2_GetEnemiesForWave(wave);
			}
			if (ongoingDifficulty == 3)
			{
				return DD2Event.Difficulty_3_GetEnemiesForWave(wave);
			}
			return DD2Event.Difficulty_1_GetEnemiesForWave(wave);
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x00619880 File Offset: 0x00617A80
		private static int GetMonsterPointsWorth(int slainMonsterID)
		{
			int ongoingDifficulty = DD2Event.OngoingDifficulty;
			if (ongoingDifficulty == 2)
			{
				return DD2Event.Difficulty_2_GetMonsterPointsWorth(slainMonsterID);
			}
			if (ongoingDifficulty == 3)
			{
				return DD2Event.Difficulty_3_GetMonsterPointsWorth(slainMonsterID);
			}
			return DD2Event.Difficulty_1_GetMonsterPointsWorth(slainMonsterID);
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x006198B0 File Offset: 0x00617AB0
		public static void SpawnMonsterFromGate(Vector2 gateBottom, bool leftGate)
		{
			int ongoingDifficulty = DD2Event.OngoingDifficulty;
			if (ongoingDifficulty == 2)
			{
				DD2Event.Difficulty_2_SpawnMonsterFromGate(gateBottom, leftGate);
				return;
			}
			if (ongoingDifficulty == 3)
			{
				DD2Event.Difficulty_3_SpawnMonsterFromGate(gateBottom, leftGate);
				return;
			}
			DD2Event.Difficulty_1_SpawnMonsterFromGate(gateBottom, leftGate);
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x006198E4 File Offset: 0x00617AE4
		public static void SummonCrystal(int x, int y, int whoAsks)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendData(113, -1, -1, null, x, (float)y, 0f, 0f, 0, 0, 0);
				return;
			}
			DD2Event.SummonCrystalDirect(x, y, whoAsks);
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x0061991C File Offset: 0x00617B1C
		public static void SummonCrystalDirect(int x, int y, int whoAsks)
		{
			if (NPC.AnyNPCs(548))
			{
				return;
			}
			Tile tileSafely = Framing.GetTileSafely(x, y);
			if (!tileSafely.active() || tileSafely.type != 466)
			{
				return;
			}
			Point point = new Point(x * 16, y * 16);
			point.X -= (int)(tileSafely.frameX / 18 * 16);
			point.Y -= (int)(tileSafely.frameY / 18 * 16);
			point.X += 40;
			point.Y += 64;
			DD2Event.StartInvasion(-1);
			NPC.NewNPC(Main.player[whoAsks].GetNPCSource_TileInteraction(x, y), point.X, point.Y, 548, 0, 0f, 0f, 0f, 0f, 255);
			DD2Event.DropStarterCrystals();
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x006199F8 File Offset: 0x00617BF8
		public static bool WouldFailSpawningHere(int x, int y)
		{
			Point point;
			Point point2;
			StrayMethods.CheckArenaScore(new Point(x, y).ToWorldCoordinates(8f, 8f), out point, out point2, 5, 10);
			int num = point2.X - x;
			int num2 = x - point.X;
			return num < 60 || num2 < 60;
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x00619A44 File Offset: 0x00617C44
		public static void FailureMessage(int client)
		{
			LocalizedText text = Language.GetText("DungeonDefenders2.BartenderWarning");
			Color color = new Color(255, 255, 0);
			if (Main.netMode == 2)
			{
				ChatHelper.SendChatMessageToClient(NetworkText.FromKey(text.Key, new object[0]), color, client);
				return;
			}
			Main.NewText(text.Value, color.R, color.G, color.B);
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x00619AB0 File Offset: 0x00617CB0
		public static void WipeEntities()
		{
			DD2Event.ClearAllTowersInGame();
			DD2Event.ClearAllDD2HostilesInGame();
			DD2Event.ClearAllDD2EnergyCrystalsInChests();
			if (Main.netMode == 2)
			{
				NetMessage.SendData(114, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x00619AF4 File Offset: 0x00617CF4
		public static void ClearAllTowersInGame()
		{
			for (int i = 0; i < 1000; i++)
			{
				if (Main.projectile[i].active && ProjectileID.Sets.IsADD2Turret[Main.projectile[i].type])
				{
					Main.projectile[i].Kill();
				}
			}
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x00619B40 File Offset: 0x00617D40
		public static void ClearAllDD2HostilesInGame()
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (Main.npc[i].active && NPCID.Sets.BelongsToInvasionOldOnesArmy[Main.npc[i].type])
				{
					Main.npc[i].active = false;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x00619BB4 File Offset: 0x00617DB4
		public static void ClearAllDD2EnergyCrystalsInGame()
		{
			for (int i = 0; i < 400; i++)
			{
				WorldItem worldItem = Main.item[i];
				if (worldItem.active && worldItem.type == 3822)
				{
					worldItem.TurnToAir(false);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(21, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x00619C1C File Offset: 0x00617E1C
		public static void ClearAllDD2EnergyCrystalsInChests()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			List<int> currentlyOpenChests = Chest.GetCurrentlyOpenChests();
			for (int i = 0; i < 8000; i++)
			{
				Chest chest = Main.chest[i];
				if (chest != null && currentlyOpenChests.Contains(i))
				{
					for (int j = 0; j < chest.maxItems; j++)
					{
						if (chest.item[j].type == 3822 && chest.item[j].stack > 0)
						{
							chest.item[j].TurnToAir(false);
							if (Main.netMode != 0)
							{
								NetMessage.SendData(32, -1, -1, null, i, (float)j, 0f, 0f, 0, 0, 0);
							}
						}
					}
				}
			}
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x00619CC3 File Offset: 0x00617EC3
		public static void AnnounceGoblinDeath(NPC n)
		{
			DD2Event._deadGoblinSpots.Add(n.Bottom);
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x00619CD8 File Offset: 0x00617ED8
		public static bool CanRaiseGoblinsHere(Vector2 spot)
		{
			int num = 0;
			using (List<Vector2>.Enumerator enumerator = DD2Event._deadGoblinSpots.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (Vector2.DistanceSquared(enumerator.Current, spot) <= 640000f)
					{
						num++;
						if (num >= 3)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x00619D40 File Offset: 0x00617F40
		public static void RaiseGoblins(NPC caller, Vector2 spot)
		{
			List<Vector2> list = new List<Vector2>();
			foreach (Vector2 vector in DD2Event._deadGoblinSpots)
			{
				if (Vector2.DistanceSquared(vector, spot) <= 722500f)
				{
					list.Add(vector);
				}
			}
			foreach (Vector2 vector2 in list)
			{
				DD2Event._deadGoblinSpots.Remove(vector2);
			}
			int num = 0;
			foreach (Vector2 vector3 in list)
			{
				Point point = vector3.ToTileCoordinates();
				point.X += Main.rand.Next(-15, 16);
				Point point2;
				if (WorldUtils.Find(point, Searches.Chain(new Searches.Down(50), new GenCondition[]
				{
					new Conditions.IsSolid()
				}), out point2))
				{
					if (DD2Event.OngoingDifficulty == 3)
					{
						NPC.NewNPC(caller.GetSpawnSourceForNPCFromNPCAI(), point2.X * 16 + 8, point2.Y * 16, 567, 0, 0f, 0f, 0f, 0f, 255);
					}
					else
					{
						NPC.NewNPC(caller.GetSpawnSourceForNPCFromNPCAI(), point2.X * 16 + 8, point2.Y * 16, 566, 0, 0f, 0f, 0f, 0f, 255);
					}
					if (++num >= 8)
					{
						break;
					}
				}
			}
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x00619F08 File Offset: 0x00618108
		public static void FindArenaHitbox()
		{
			if (DD2Event._arenaHitboxingCooldown > 0)
			{
				DD2Event._arenaHitboxingCooldown--;
				return;
			}
			DD2Event._arenaHitboxingCooldown = 60;
			Vector2 vector = new Vector2(float.MaxValue, float.MaxValue);
			Vector2 vector2 = new Vector2(0f, 0f);
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && (npc.type == 549 || npc.type == 548))
				{
					Vector2 vector3 = npc.TopLeft;
					if (vector.X > vector3.X)
					{
						vector.X = vector3.X;
					}
					if (vector.Y > vector3.Y)
					{
						vector.Y = vector3.Y;
					}
					vector3 = npc.BottomRight;
					if (vector2.X < vector3.X)
					{
						vector2.X = vector3.X;
					}
					if (vector2.Y < vector3.Y)
					{
						vector2.Y = vector3.Y;
					}
				}
			}
			Vector2 vector4 = new Vector2(16f, 16f) * 50f;
			vector -= vector4;
			vector2 += vector4;
			Vector2 vector5 = vector2 - vector;
			DD2Event.ArenaHitbox.X = (int)vector.X;
			DD2Event.ArenaHitbox.Y = (int)vector.Y;
			DD2Event.ArenaHitbox.Width = (int)vector5.X;
			DD2Event.ArenaHitbox.Height = (int)vector5.Y;
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x0061A09E File Offset: 0x0061829E
		public static bool ShouldBlockBuilding(Vector2 worldPosition)
		{
			return DD2Event.ArenaHitbox.Contains(worldPosition.ToPoint());
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x0061A0B0 File Offset: 0x006182B0
		public static void DropMedals(int numberOfMedals)
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == 548)
				{
					Main.npc[i].DropItemInstanced(Main.npc[i].position, Main.npc[i].Size, 3817, numberOfMedals, false);
				}
			}
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x0061A11C File Offset: 0x0061831C
		public static bool ShouldDropCrystals()
		{
			int num;
			int num2;
			int num3;
			DD2Event.GetInvasionStatus(out num, out num2, out num3, false);
			if (DD2Event._crystalsDropping_lastWave < num)
			{
				DD2Event._crystalsDropping_lastWave++;
				if (DD2Event._crystalsDropping_alreadyDropped > 0)
				{
					DD2Event._crystalsDropping_alreadyDropped -= DD2Event._crystalsDropping_toDrop;
				}
				if (DD2Event.OngoingDifficulty == 1)
				{
					switch (num)
					{
					case 1:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 2:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 3:
						DD2Event._crystalsDropping_toDrop = 30;
						break;
					case 4:
						DD2Event._crystalsDropping_toDrop = 30;
						break;
					case 5:
						DD2Event._crystalsDropping_toDrop = 40;
						break;
					}
				}
				else if (DD2Event.OngoingDifficulty == 2)
				{
					switch (num)
					{
					case 1:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 2:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 3:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 4:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 5:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 6:
						DD2Event._crystalsDropping_toDrop = 30;
						break;
					case 7:
						DD2Event._crystalsDropping_toDrop = 30;
						break;
					}
				}
				else if (DD2Event.OngoingDifficulty == 3)
				{
					switch (num)
					{
					case 1:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 2:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 3:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 4:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 5:
						DD2Event._crystalsDropping_toDrop = 30;
						break;
					case 6:
						DD2Event._crystalsDropping_toDrop = 30;
						break;
					case 7:
						DD2Event._crystalsDropping_toDrop = 30;
						break;
					}
				}
			}
			if (Main.netMode != 0 && Main.expertMode)
			{
				DD2Event._crystalsDropping_toDrop = (int)((float)DD2Event._crystalsDropping_toDrop * NPC.GetBalance());
			}
			float num4 = (float)num3 / (float)num2;
			if ((float)DD2Event._crystalsDropping_alreadyDropped < (float)DD2Event._crystalsDropping_toDrop * num4)
			{
				DD2Event._crystalsDropping_alreadyDropped++;
				return true;
			}
			return false;
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x0061A2F8 File Offset: 0x006184F8
		private static void SummonBetsy()
		{
			if (DD2Event._spawnedBetsyT3)
			{
				return;
			}
			if (NPC.AnyNPCs(551))
			{
				return;
			}
			Vector2 center = new Vector2(1f, 1f);
			int num = NPC.FindFirstNPC(548);
			if (num != -1)
			{
				center = Main.npc[num].Center;
			}
			NPC.SpawnOnPlayer((int)Player.FindClosest(center, 1, 1), 551, 0f, 0f, 0f, 0f);
			DD2Event._spawnedBetsyT3 = true;
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x0061A374 File Offset: 0x00618574
		private static void DropStarterCrystals()
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == 548)
				{
					for (int j = 0; j < 5; j++)
					{
						Item.NewItem(new EntitySource_WorldEvent(), Main.npc[i].position, Main.npc[i].width, Main.npc[i].height, 3822, 2, false, 0, false);
					}
					return;
				}
			}
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x0061A3F8 File Offset: 0x006185F8
		private static void SetEnemySpawningOnHold(int forHowLong)
		{
			DD2Event._timeLeftUntilSpawningBegins = forHowLong;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(116, -1, -1, null, DD2Event._timeLeftUntilSpawningBegins, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x0061A434 File Offset: 0x00618634
		private static short[] Difficulty_1_GetEnemiesForWave(int wave)
		{
			DD2Event.LaneSpawnRate = 60;
			switch (wave)
			{
			case 1:
				DD2Event.LaneSpawnRate = 90;
				return new short[] { 552 };
			case 2:
				return new short[] { 552, 555 };
			case 3:
				DD2Event.LaneSpawnRate = 55;
				return new short[] { 552, 555, 561 };
			case 4:
				DD2Event.LaneSpawnRate = 50;
				return new short[] { 552, 555, 561, 558 };
			case 5:
				DD2Event.LaneSpawnRate = 40;
				return new short[] { 552, 555, 561, 558, 564 };
			default:
				return new short[] { 552 };
			}
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x0061A4EC File Offset: 0x006186EC
		private static int Difficulty_1_GetRequiredWaveKills(ref int waveNumber, ref int currentKillCount, bool currentlyInCheckProgress)
		{
			switch (waveNumber)
			{
			case -1:
				return 0;
			case 1:
				return 60;
			case 2:
				return 80;
			case 3:
				return 100;
			case 4:
				DD2Event._deadGoblinSpots.Clear();
				return 120;
			case 5:
				if (!DD2Event._downedDarkMageT1 && currentKillCount > 139)
				{
					currentKillCount = 139;
				}
				return 140;
			case 6:
				waveNumber = 5;
				currentKillCount = 1;
				if (currentlyInCheckProgress)
				{
					DD2Event.StartVictoryScene();
				}
				return 1;
			}
			return 10;
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x0061A570 File Offset: 0x00618770
		private static void Difficulty_1_SpawnMonsterFromGate(Vector2 gateBottom, bool leftGate)
		{
			int num = (int)gateBottom.X;
			int num2 = (int)gateBottom.Y;
			int num3 = 50;
			int num4 = 6;
			if (NPC.waveNumber > 4)
			{
				num4 = 12;
			}
			else if (NPC.waveNumber > 3)
			{
				num4 = 8;
			}
			int num5 = 6;
			if (NPC.waveNumber > 4)
			{
				num5 = 8;
			}
			for (int i = 1; i < Main.CurrentFrameFlags.ActivePlayersCount; i++)
			{
				num3 = (int)((double)num3 * 1.3);
				num4 = (int)((double)num4 * 1.3);
				num5 = (int)((double)num5 * 1.3);
			}
			int num6 = Main.maxNPCs;
			switch (NPC.waveNumber)
			{
			case 1:
				if (NPC.CountNPCS(552) + NPC.CountNPCS(555) < num3)
				{
					num6 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 552, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 2:
				if (NPC.CountNPCS(552) + NPC.CountNPCS(555) < num3)
				{
					if (Main.rand.Next(7) == 0)
					{
						num6 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 555, 0, 0f, 0f, 0f, 0f, 255);
					}
					else
					{
						num6 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 552, 0, 0f, 0f, 0f, 0f, 255);
					}
				}
				break;
			case 3:
				if (Main.rand.Next(6) == 0 && NPC.CountNPCS(561) < num4)
				{
					num6 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 561, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(552) + NPC.CountNPCS(555) < num3)
				{
					if (Main.rand.Next(5) == 0)
					{
						num6 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 555, 0, 0f, 0f, 0f, 0f, 255);
					}
					else
					{
						num6 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 552, 0, 0f, 0f, 0f, 0f, 255);
					}
				}
				break;
			case 4:
				if (Main.rand.Next(12) == 0 && NPC.CountNPCS(558) < num5)
				{
					num6 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 558, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(5) == 0 && NPC.CountNPCS(561) < num4)
				{
					num6 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 561, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(552) + NPC.CountNPCS(555) < num3)
				{
					if (Main.rand.Next(5) == 0)
					{
						num6 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 555, 0, 0f, 0f, 0f, 0f, 255);
					}
					else
					{
						num6 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 552, 0, 0f, 0f, 0f, 0f, 255);
					}
				}
				break;
			case 5:
			{
				int num7;
				int num8;
				int num9;
				DD2Event.GetInvasionStatus(out num7, out num8, out num9, false);
				if ((!leftGate || Main.rand.Next(2) == 0) && (float)num9 > (float)num8 * 0.5f && !NPC.AnyNPCs(564))
				{
					num6 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 564, 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(10) == 0 && NPC.CountNPCS(558) < num5)
				{
					num6 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 558, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(4) == 0 && NPC.CountNPCS(561) < num4)
				{
					num6 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 561, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(552) + NPC.CountNPCS(555) < num3)
				{
					if (Main.rand.Next(4) == 0)
					{
						num6 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 555, 0, 0f, 0f, 0f, 0f, 255);
					}
					else
					{
						num6 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 552, 0, 0f, 0f, 0f, 0f, 255);
					}
				}
				break;
			}
			default:
				num6 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 552, 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			if (Main.netMode == 2 && num6 < Main.maxNPCs)
			{
				NetMessage.SendData(23, -1, -1, null, num6, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x0061AAE0 File Offset: 0x00618CE0
		private static int Difficulty_1_GetMonsterPointsWorth(int slainMonsterID)
		{
			if (NPC.waveNumber == 5 && NPC.waveKills >= 139f)
			{
				if (slainMonsterID == 564 || slainMonsterID == 565)
				{
					DD2Event._downedDarkMageT1 = true;
					return 1;
				}
				return 0;
			}
			else
			{
				if (slainMonsterID - 551 > 14 && slainMonsterID - 568 > 10)
				{
					return 0;
				}
				if (NPC.waveNumber == 5 && NPC.waveKills == 138f)
				{
					return 1;
				}
				if (!Main.expertMode)
				{
					return 1;
				}
				return 2;
			}
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x0061AB54 File Offset: 0x00618D54
		private static short[] Difficulty_2_GetEnemiesForWave(int wave)
		{
			DD2Event.LaneSpawnRate = 60;
			switch (wave)
			{
			case 1:
				DD2Event.LaneSpawnRate = 90;
				return new short[] { 553, 562 };
			case 2:
				DD2Event.LaneSpawnRate = 70;
				return new short[] { 553, 562, 572 };
			case 3:
				return new short[] { 553, 556, 562, 559, 572 };
			case 4:
				DD2Event.LaneSpawnRate = 55;
				return new short[] { 553, 559, 570, 572, 562 };
			case 5:
				DD2Event.LaneSpawnRate = 50;
				return new short[] { 553, 556, 559, 572, 574, 570 };
			case 6:
				DD2Event.LaneSpawnRate = 45;
				return new short[] { 553, 556, 562, 559, 568, 570, 572, 574 };
			case 7:
				DD2Event.LaneSpawnRate = 42;
				return new short[] { 553, 556, 572, 559, 568, 574, 570, 576 };
			default:
				return new short[] { 553 };
			}
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x0061AC4C File Offset: 0x00618E4C
		private static int Difficulty_2_GetRequiredWaveKills(ref int waveNumber, ref int currentKillCount, bool currentlyInCheckProgress)
		{
			switch (waveNumber)
			{
			case -1:
				return 0;
			case 1:
				return 60;
			case 2:
				return 80;
			case 3:
				return 100;
			case 4:
				return 120;
			case 5:
				return 140;
			case 6:
				return 180;
			case 7:
				if (!DD2Event._downedOgreT2 && currentKillCount > 219)
				{
					currentKillCount = 219;
				}
				return 220;
			case 8:
				waveNumber = 7;
				currentKillCount = 1;
				if (currentlyInCheckProgress)
				{
					DD2Event.StartVictoryScene();
				}
				return 1;
			}
			return 10;
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x0061ACD8 File Offset: 0x00618ED8
		private static int Difficulty_2_GetMonsterPointsWorth(int slainMonsterID)
		{
			if (NPC.waveNumber == 7 && NPC.waveKills >= 219f)
			{
				if (slainMonsterID == 576 || slainMonsterID == 577)
				{
					DD2Event._downedOgreT2 = true;
					return 1;
				}
				return 0;
			}
			else
			{
				if (slainMonsterID - 551 > 14 && slainMonsterID - 568 > 10)
				{
					return 0;
				}
				if (NPC.waveNumber == 7 && NPC.waveKills == 218f)
				{
					return 1;
				}
				if (!Main.expertMode)
				{
					return 1;
				}
				return 2;
			}
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x0061AD4C File Offset: 0x00618F4C
		private static void Difficulty_2_SpawnMonsterFromGate(Vector2 gateBottom, bool leftGate)
		{
			int num = (int)gateBottom.X;
			int num2 = (int)gateBottom.Y;
			int num3 = 50;
			int num4 = 5;
			if (NPC.waveNumber > 1)
			{
				num4 = 8;
			}
			if (NPC.waveNumber > 3)
			{
				num4 = 10;
			}
			if (NPC.waveNumber > 5)
			{
				num4 = 12;
			}
			int num5 = 5;
			if (NPC.waveNumber > 4)
			{
				num5 = 7;
			}
			int num6 = 2;
			int num7 = 8;
			if (NPC.waveNumber > 3)
			{
				num7 = 12;
			}
			int num8 = 3;
			if (NPC.waveNumber > 5)
			{
				num8 = 5;
			}
			for (int i = 1; i < Main.CurrentFrameFlags.ActivePlayersCount; i++)
			{
				num3 = (int)((double)num3 * 1.3);
				num4 = (int)((double)num4 * 1.3);
				num7 = (int)((double)num3 * 1.3);
				num8 = (int)((double)num3 * 1.35);
			}
			int num9 = Main.maxNPCs;
			int num10 = Main.maxNPCs;
			switch (NPC.waveNumber)
			{
			case 1:
				if (Main.rand.Next(20) == 0 && NPC.CountNPCS(562) < num4)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 562, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(553) < num3)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 553, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 2:
				if (Main.rand.Next(3) == 0 && NPC.CountNPCS(572) < num7)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 572, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(8) == 0 && NPC.CountNPCS(562) < num4)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 562, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(553) < num3)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 553, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 3:
				if (Main.rand.Next(7) == 0 && NPC.CountNPCS(572) < num7)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 572, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(10) == 0 && NPC.CountNPCS(559) < num5)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 559, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(8) == 0 && NPC.CountNPCS(562) < num4)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 562, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(553) + NPC.CountNPCS(556) < num3)
				{
					if (Main.rand.Next(4) == 0)
					{
						num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 556, 0, 0f, 0f, 0f, 0f, 255);
					}
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 553, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 4:
				if (Main.rand.Next(10) == 0 && NPC.CountNPCS(570) < num8)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 570, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(12) == 0 && NPC.CountNPCS(559) < num5)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 559, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(6) == 0 && NPC.CountNPCS(562) < num4)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 562, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(3) == 0 && NPC.CountNPCS(572) < num7)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 572, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(553) < num3)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 553, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 5:
				if (Main.rand.Next(7) == 0 && NPC.CountNPCS(570) < num8)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 570, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(10) == 0 && NPC.CountNPCS(559) < num5)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 559, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(4) == 0 && NPC.CountNPCS(572) + NPC.CountNPCS(574) < num7)
				{
					if (Main.rand.Next(2) == 0)
					{
						num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 572, 0, 0f, 0f, 0f, 0f, 255);
					}
					else
					{
						num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 574, 0, 0f, 0f, 0f, 0f, 255);
					}
				}
				else if (NPC.CountNPCS(553) + NPC.CountNPCS(556) < num3)
				{
					if (Main.rand.Next(3) == 0)
					{
						num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 556, 0, 0f, 0f, 0f, 0f, 255);
					}
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 553, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 6:
				if (Main.rand.Next(7) == 0 && NPC.CountNPCS(570) < num8)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 570, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(17) == 0 && NPC.CountNPCS(568) < num6)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 568, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(5) == 0 && NPC.CountNPCS(572) + NPC.CountNPCS(574) < num7)
				{
					if (Main.rand.Next(2) != 0)
					{
						num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 572, 0, 0f, 0f, 0f, 0f, 255);
					}
					else
					{
						num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 574, 0, 0f, 0f, 0f, 0f, 255);
					}
				}
				else if (Main.rand.Next(9) == 0 && NPC.CountNPCS(559) < num5)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 559, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(3) == 0 && NPC.CountNPCS(562) < num4)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 562, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(553) + NPC.CountNPCS(556) < num3)
				{
					if (Main.rand.Next(3) != 0)
					{
						num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 556, 0, 0f, 0f, 0f, 0f, 255);
					}
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 553, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 7:
			{
				int num11;
				int num12;
				int num13;
				DD2Event.GetInvasionStatus(out num11, out num12, out num13, false);
				if ((!leftGate || Main.rand.Next(2) == 0) && (float)num13 > (float)num12 * 0.1f && !NPC.AnyNPCs(576))
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 576, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(7) == 0 && NPC.CountNPCS(570) < num8)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 570, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(17) == 0 && NPC.CountNPCS(568) < num6)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 568, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(7) == 0 && NPC.CountNPCS(572) + NPC.CountNPCS(574) < num7)
				{
					if (Main.rand.Next(3) != 0)
					{
						num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 572, 0, 0f, 0f, 0f, 0f, 255);
					}
					else
					{
						num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 574, 0, 0f, 0f, 0f, 0f, 255);
					}
				}
				else if (Main.rand.Next(11) == 0 && NPC.CountNPCS(559) < num5)
				{
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 559, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(553) + NPC.CountNPCS(556) < num3)
				{
					if (Main.rand.Next(2) == 0)
					{
						num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 556, 0, 0f, 0f, 0f, 0f, 255);
					}
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 553, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			}
			default:
				num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 553, 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			if (Main.netMode == 2 && num9 < Main.maxNPCs)
			{
				NetMessage.SendData(23, -1, -1, null, num9, 0f, 0f, 0f, 0, 0, 0);
			}
			if (Main.netMode == 2 && num10 < Main.maxNPCs)
			{
				NetMessage.SendData(23, -1, -1, null, num10, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x0061B9A4 File Offset: 0x00619BA4
		private static short[] Difficulty_3_GetEnemiesForWave(int wave)
		{
			DD2Event.LaneSpawnRate = 60;
			switch (wave)
			{
			case 1:
				DD2Event.LaneSpawnRate = 85;
				return new short[] { 554, 557, 563 };
			case 2:
				DD2Event.LaneSpawnRate = 75;
				return new short[] { 554, 557, 563, 573, 578 };
			case 3:
				DD2Event.LaneSpawnRate = 60;
				return new short[] { 554, 563, 560, 573, 571 };
			case 4:
				DD2Event.LaneSpawnRate = 60;
				return new short[] { 554, 560, 571, 573, 563, 575, 565 };
			case 5:
				DD2Event.LaneSpawnRate = 55;
				return new short[] { 554, 557, 573, 575, 571, 569, 577 };
			case 6:
				DD2Event.LaneSpawnRate = 60;
				return new short[] { 554, 557, 563, 578, 569, 571, 577, 565 };
			case 7:
				DD2Event.LaneSpawnRate = 90;
				return new short[] { 554, 557, 563, 569, 571, 551 };
			default:
				return new short[] { 554 };
			}
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x0061BAA0 File Offset: 0x00619CA0
		private static int Difficulty_3_GetRequiredWaveKills(ref int waveNumber, ref int currentKillCount, bool currentlyInCheckProgress)
		{
			switch (waveNumber)
			{
			case -1:
				return 0;
			case 1:
				return 60;
			case 2:
				return 80;
			case 3:
				return 100;
			case 4:
				return 120;
			case 5:
				return 140;
			case 6:
				return 180;
			case 7:
			{
				int num = NPC.FindFirstNPC(551);
				if (num == -1)
				{
					return 1;
				}
				currentKillCount = 100 - (int)((float)Main.npc[num].life / (float)Main.npc[num].lifeMax * 100f);
				return 100;
			}
			case 8:
				waveNumber = 7;
				currentKillCount = 1;
				if (currentlyInCheckProgress)
				{
					DD2Event.StartVictoryScene();
				}
				return 1;
			}
			return 10;
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x0061BB49 File Offset: 0x00619D49
		private static int Difficulty_3_GetMonsterPointsWorth(int slainMonsterID)
		{
			if (NPC.waveNumber == 7)
			{
				if (slainMonsterID == 551)
				{
					return 1;
				}
				return 0;
			}
			else
			{
				if (slainMonsterID - 551 > 14 && slainMonsterID - 568 > 10)
				{
					return 0;
				}
				if (!Main.expertMode)
				{
					return 1;
				}
				return 2;
			}
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x0061BB84 File Offset: 0x00619D84
		private static void Difficulty_3_SpawnMonsterFromGate(Vector2 gateBottom, bool leftGate)
		{
			int num = (int)gateBottom.X;
			int num2 = (int)gateBottom.Y;
			int num3 = 60;
			int num4 = 7;
			if (NPC.waveNumber > 1)
			{
				num4 = 9;
			}
			if (NPC.waveNumber > 3)
			{
				num4 = 12;
			}
			if (NPC.waveNumber > 5)
			{
				num4 = 15;
			}
			int num5 = 7;
			if (NPC.waveNumber > 4)
			{
				num5 = 10;
			}
			int num6 = 2;
			if (NPC.waveNumber > 5)
			{
				num6 = 3;
			}
			int num7 = 12;
			if (NPC.waveNumber > 3)
			{
				num7 = 18;
			}
			int num8 = 4;
			if (NPC.waveNumber > 5)
			{
				num8 = 6;
			}
			int num9 = 4;
			for (int i = 1; i < Main.CurrentFrameFlags.ActivePlayersCount; i++)
			{
				num3 = (int)((double)num3 * 1.3);
				num4 = (int)((double)num4 * 1.3);
				num7 = (int)((double)num3 * 1.3);
				num8 = (int)((double)num3 * 1.35);
				num9 = (int)((double)num9 * 1.3);
			}
			int num10 = Main.maxNPCs;
			int num11 = Main.maxNPCs;
			switch (NPC.waveNumber)
			{
			case 1:
				if (Main.rand.Next(18) == 0 && NPC.CountNPCS(563) < num4)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 563, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(554) < num3)
				{
					if (Main.rand.Next(7) == 0)
					{
						num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 557, 0, 0f, 0f, 0f, 0f, 255);
					}
					num11 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 554, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 2:
				if (Main.rand.Next(3) == 0 && NPC.CountNPCS(578) < num9)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 578, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(7) == 0 && NPC.CountNPCS(563) < num4)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 563, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(3) == 0 && NPC.CountNPCS(573) < num7)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 573, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(554) < num3)
				{
					if (Main.rand.Next(4) == 0)
					{
						num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 557, 0, 0f, 0f, 0f, 0f, 255);
					}
					num11 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 554, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 3:
				if (Main.rand.Next(13) == 0 && NPC.CountNPCS(571) < num8)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 571, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(7) == 0 && NPC.CountNPCS(573) < num7)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 573, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(10) == 0 && NPC.CountNPCS(560) < num5)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 560, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(8) == 0 && NPC.CountNPCS(563) < num4)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 563, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(554) + NPC.CountNPCS(557) < num3)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 554, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 4:
				if (Main.rand.Next(24) == 0 && !NPC.AnyNPCs(565))
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 565, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(12) == 0 && NPC.CountNPCS(571) < num8)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 571, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(15) == 0 && NPC.CountNPCS(560) < num5)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 560, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(7) == 0 && NPC.CountNPCS(563) < num4)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 563, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(5) == 0 && NPC.CountNPCS(573) + NPC.CountNPCS(575) < num7)
				{
					if (Main.rand.Next(3) != 0)
					{
						num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 573, 0, 0f, 0f, 0f, 0f, 255);
					}
					else
					{
						num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 575, 0, 0f, 0f, 0f, 0f, 255);
					}
				}
				else if (NPC.CountNPCS(554) < num3)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 554, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 5:
				if (Main.rand.Next(20) == 0 && !NPC.AnyNPCs(577))
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 577, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(17) == 0 && NPC.CountNPCS(569) < num6)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 569, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(8) == 0 && NPC.CountNPCS(571) < num8)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 571, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(7) == 0 && NPC.CountNPCS(573) + NPC.CountNPCS(575) < num7)
				{
					if (Main.rand.Next(4) != 0)
					{
						num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 573, 0, 0f, 0f, 0f, 0f, 255);
					}
					else
					{
						num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 575, 0, 0f, 0f, 0f, 0f, 255);
					}
				}
				else if (NPC.CountNPCS(554) + NPC.CountNPCS(557) < num3)
				{
					if (Main.rand.Next(3) == 0)
					{
						num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 557, 0, 0f, 0f, 0f, 0f, 255);
					}
					num11 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 554, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 6:
				if (Main.rand.Next(20) == 0 && !NPC.AnyNPCs(577))
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 577, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(20) == 0 && !NPC.AnyNPCs(565))
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 565, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(12) == 0 && NPC.CountNPCS(571) < num8)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 571, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(25) == 0 && NPC.CountNPCS(569) < num6)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 569, 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(7) == 0 && NPC.CountNPCS(578) < num9)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 578, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(7) == 0 && NPC.CountNPCS(573) + NPC.CountNPCS(575) < num7)
				{
					if (Main.rand.Next(3) != 0)
					{
						num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 573, 0, 0f, 0f, 0f, 0f, 255);
					}
					else
					{
						num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 575, 0, 0f, 0f, 0f, 0f, 255);
					}
				}
				else if (Main.rand.Next(5) == 0 && NPC.CountNPCS(563) < num4)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 563, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(554) + NPC.CountNPCS(557) < num3)
				{
					if (Main.rand.Next(3) == 0)
					{
						num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 557, 0, 0f, 0f, 0f, 0f, 255);
					}
					num11 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 554, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 7:
				if (Main.rand.Next(20) == 0 && NPC.CountNPCS(571) < num8)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 571, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(17) == 0 && NPC.CountNPCS(569) < num6)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 569, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(10) == 0 && NPC.CountNPCS(563) < num4)
				{
					num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 563, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(554) + NPC.CountNPCS(557) < num3)
				{
					if (Main.rand.Next(5) == 0)
					{
						num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 557, 0, 0f, 0f, 0f, 0f, 255);
					}
					num11 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 554, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			default:
				num10 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), num, num2, 554, 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			if (Main.netMode == 2 && num10 < Main.maxNPCs)
			{
				NetMessage.SendData(23, -1, -1, null, num10, 0f, 0f, 0f, 0, 0, 0);
			}
			if (Main.netMode == 2 && num11 < Main.maxNPCs)
			{
				NetMessage.SendData(23, -1, -1, null, num11, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x0061C948 File Offset: 0x0061AB48
		public static bool IsStandActive(int x, int y)
		{
			Vector2 vector = new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8));
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc != null && npc.active && npc.type == 548)
				{
					return npc.Bottom.Distance(vector) < 36f;
				}
			}
			return false;
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x0061C9B0 File Offset: 0x0061ABB0
		public static void RequestToSkipWaitTime(int x, int y)
		{
			if (DD2Event.TimeLeftBetweenWaves <= 60)
			{
				return;
			}
			if (!DD2Event.IsStandActive(x, y))
			{
				return;
			}
			SoundEngine.PlaySound(SoundID.NPCDeath7, x * 16 + 8, y * 16 + 8, 0f, 1f);
			if (Main.netMode == 0)
			{
				DD2Event.AttemptToSkipWaitTime();
				return;
			}
			if (Main.netMode != 2)
			{
				NetMessage.SendData(143, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x0061CA27 File Offset: 0x0061AC27
		public static void AttemptToSkipWaitTime()
		{
			if (Main.netMode == 1 || DD2Event.TimeLeftBetweenWaves <= 60)
			{
				return;
			}
			DD2Event.SetEnemySpawningOnHold(60);
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x0061CA42 File Offset: 0x0061AC42
		private static IEntitySource GetSpawnSource_OldOnesArmy()
		{
			return new EntitySource_OldOnesArmy();
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x0000357B File Offset: 0x0000177B
		public DD2Event()
		{
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x0061CA4C File Offset: 0x0061AC4C
		// Note: this type is marked as 'beforefieldinit'.
		static DD2Event()
		{
		}

		// Token: 0x04005AC9 RID: 23241
		private static readonly Color INFO_NEW_WAVE_COLOR = new Color(175, 55, 255);

		// Token: 0x04005ACA RID: 23242
		private static readonly Color INFO_START_INVASION_COLOR = ChatColors.World;

		// Token: 0x04005ACB RID: 23243
		private static readonly Color INFO_FAILURE_INVASION_COLOR = new Color(255, 0, 0);

		// Token: 0x04005ACC RID: 23244
		private const int INVASION_ID = 3;

		// Token: 0x04005ACD RID: 23245
		public static bool DownedInvasionT1;

		// Token: 0x04005ACE RID: 23246
		public static bool DownedInvasionT2;

		// Token: 0x04005ACF RID: 23247
		public static bool DownedInvasionT3;

		// Token: 0x04005AD0 RID: 23248
		public static bool LostThisRun;

		// Token: 0x04005AD1 RID: 23249
		public static bool WonThisRun;

		// Token: 0x04005AD2 RID: 23250
		public static int LaneSpawnRate = 60;

		// Token: 0x04005AD3 RID: 23251
		private static bool _downedDarkMageT1;

		// Token: 0x04005AD4 RID: 23252
		private static bool _downedOgreT2;

		// Token: 0x04005AD5 RID: 23253
		private static bool _spawnedBetsyT3;

		// Token: 0x04005AD6 RID: 23254
		public static bool Ongoing;

		// Token: 0x04005AD7 RID: 23255
		private static DD2Event.DamageTracker _damageTracker;

		// Token: 0x04005AD8 RID: 23256
		public static Rectangle ArenaHitbox;

		// Token: 0x04005AD9 RID: 23257
		private static int _arenaHitboxingCooldown;

		// Token: 0x04005ADA RID: 23258
		public static int OngoingDifficulty;

		// Token: 0x04005ADB RID: 23259
		private static List<Vector2> _deadGoblinSpots = new List<Vector2>();

		// Token: 0x04005ADC RID: 23260
		private static int _crystalsDropping_lastWave;

		// Token: 0x04005ADD RID: 23261
		private static int _crystalsDropping_toDrop;

		// Token: 0x04005ADE RID: 23262
		private static int _crystalsDropping_alreadyDropped;

		// Token: 0x04005ADF RID: 23263
		private static int _timeLeftUntilSpawningBegins;

		// Token: 0x0200098B RID: 2443
		public class DamageTracker : NPCDamageTracker
		{
			// Token: 0x1700058D RID: 1421
			// (get) Token: 0x06004969 RID: 18793 RVA: 0x006D1DBF File Offset: 0x006CFFBF
			public override LocalizedText Name
			{
				get
				{
					return Language.GetText("Bestiary_Invasions.OldOnesArmy");
				}
			}

			// Token: 0x1700058E RID: 1422
			// (get) Token: 0x0600496A RID: 18794 RVA: 0x006D1DCB File Offset: 0x006CFFCB
			public override LocalizedText KillTimeMessage
			{
				get
				{
					return Language.GetText(this._won ? "BossDamageCommand.KillTimeDefeated" : "BossDamageCommand.KillTimeLost");
				}
			}

			// Token: 0x0600496B RID: 18795 RVA: 0x006D1DE6 File Offset: 0x006CFFE6
			protected override bool IncludeDamageFor(NPC npc)
			{
				return NPCID.Sets.BelongsToInvasionOldOnesArmy[npc.type] && npc.type != 548;
			}

			// Token: 0x0600496C RID: 18796 RVA: 0x006D1E08 File Offset: 0x006D0008
			public void Stop(bool won)
			{
				this._won = won;
				base.Stop();
			}

			// Token: 0x0600496D RID: 18797 RVA: 0x006D1E17 File Offset: 0x006D0017
			public DamageTracker()
			{
			}

			// Token: 0x04007645 RID: 30277
			private bool _won;
		}
	}
}
