using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.GameContent.NetModules;
using Terraria.Net;

namespace Terraria.GameContent.Ambience
{
	// Token: 0x02000362 RID: 866
	public class AmbienceServer
	{
		// Token: 0x060028D5 RID: 10453 RVA: 0x005751D6 File Offset: 0x005733D6
		private static bool IsSunnyDay()
		{
			return !Main.IsItRaining && Main.dayTime && !Main.eclipse;
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x005751F0 File Offset: 0x005733F0
		private static bool IsSunset()
		{
			return Main.dayTime && Main.time > 40500.0;
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x0057520B File Offset: 0x0057340B
		private static bool IsCalmNight()
		{
			return !Main.IsItRaining && !Main.dayTime && !Main.bloodMoon && !Main.pumpkinMoon && !Main.snowMoon;
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x00575234 File Offset: 0x00573434
		public AmbienceServer()
		{
			this.ResetSpawnTime();
			this._spawnConditions[SkyEntityType.BirdsV] = new Func<bool>(AmbienceServer.IsSunnyDay);
			this._spawnConditions[SkyEntityType.Wyvern] = () => AmbienceServer.IsSunnyDay() && Main.hardMode;
			this._spawnConditions[SkyEntityType.Airship] = () => AmbienceServer.IsSunnyDay() && Main.IsItAHappyWindyDay;
			this._spawnConditions[SkyEntityType.AirBalloon] = () => AmbienceServer.IsSunnyDay() && !Main.IsItAHappyWindyDay;
			this._spawnConditions[SkyEntityType.Eyeball] = () => !Main.dayTime;
			this._spawnConditions[SkyEntityType.Butterflies] = () => AmbienceServer.IsSunnyDay() && !Main.IsItAHappyWindyDay && !NPC.TooWindyForButterflies && NPC.butterflyChance < 6;
			this._spawnConditions[SkyEntityType.LostKite] = () => Main.dayTime && !Main.eclipse && Main.IsItAHappyWindyDay;
			this._spawnConditions[SkyEntityType.Vulture] = () => AmbienceServer.IsSunnyDay();
			this._spawnConditions[SkyEntityType.Bats] = () => (AmbienceServer.IsSunset() && AmbienceServer.IsSunnyDay()) || AmbienceServer.IsCalmNight();
			this._spawnConditions[SkyEntityType.PixiePosse] = () => AmbienceServer.IsSunnyDay() || AmbienceServer.IsCalmNight();
			this._spawnConditions[SkyEntityType.Seagulls] = () => AmbienceServer.IsSunnyDay();
			this._spawnConditions[SkyEntityType.SlimeBalloons] = () => AmbienceServer.IsSunnyDay() && Main.IsItAHappyWindyDay;
			this._spawnConditions[SkyEntityType.Gastropods] = () => AmbienceServer.IsCalmNight();
			this._spawnConditions[SkyEntityType.Pegasus] = () => AmbienceServer.IsSunnyDay();
			this._spawnConditions[SkyEntityType.EaterOfSouls] = () => AmbienceServer.IsSunnyDay() || AmbienceServer.IsCalmNight();
			this._spawnConditions[SkyEntityType.Crimera] = () => AmbienceServer.IsSunnyDay() || AmbienceServer.IsCalmNight();
			this._spawnConditions[SkyEntityType.Hellbats] = () => true;
			this._secondarySpawnConditionsPerPlayer[SkyEntityType.Vulture] = (Player player) => player.ZoneDesert;
			this._secondarySpawnConditionsPerPlayer[SkyEntityType.PixiePosse] = (Player player) => player.ZoneHallow;
			this._secondarySpawnConditionsPerPlayer[SkyEntityType.Seagulls] = (Player player) => player.ZoneBeach;
			this._secondarySpawnConditionsPerPlayer[SkyEntityType.Gastropods] = (Player player) => player.ZoneHallow;
			this._secondarySpawnConditionsPerPlayer[SkyEntityType.Pegasus] = (Player player) => player.ZoneHallow;
			this._secondarySpawnConditionsPerPlayer[SkyEntityType.EaterOfSouls] = (Player player) => player.ZoneCorrupt;
			this._secondarySpawnConditionsPerPlayer[SkyEntityType.Crimera] = (Player player) => player.ZoneCrimson;
			this._secondarySpawnConditionsPerPlayer[SkyEntityType.Bats] = (Player player) => player.ZoneJungle;
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x0057569F File Offset: 0x0057389F
		private bool IsPlayerAtRightHeightForType(SkyEntityType type, Player plr)
		{
			if (type == SkyEntityType.Hellbats)
			{
				return AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbienceHell(plr);
			}
			return AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbienceSky(plr);
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x005756B4 File Offset: 0x005738B4
		public void Update()
		{
			this.SpawnForcedEntities();
			if (this._updatesUntilNextAttempt > 0)
			{
				this._updatesUntilNextAttempt -= Main.dayRate;
				return;
			}
			this.ResetSpawnTime();
			IEnumerable<SkyEntityType> enumerable = from pair in this._spawnConditions
				where pair.Value()
				select pair.Key;
			if (enumerable.Count((SkyEntityType type) => true) == 0)
			{
				return;
			}
			Player player;
			AmbienceServer.FindPlayerThatCanSeeBackgroundAmbience(out player);
			if (player == null)
			{
				return;
			}
			IEnumerable<SkyEntityType> enumerable2 = enumerable.Where((SkyEntityType type) => this.IsPlayerAtRightHeightForType(type, player) && this._secondarySpawnConditionsPerPlayer.ContainsKey(type) && this._secondarySpawnConditionsPerPlayer[type](player));
			int num = enumerable2.Count((SkyEntityType type) => true);
			if (num == 0 || Main.rand.Next(5) < 3)
			{
				enumerable2 = enumerable.Where((SkyEntityType type) => this.IsPlayerAtRightHeightForType(type, player) && (!this._secondarySpawnConditionsPerPlayer.ContainsKey(type) || this._secondarySpawnConditionsPerPlayer[type](player)));
				num = enumerable2.Count((SkyEntityType type) => true);
			}
			if (num == 0)
			{
				return;
			}
			SkyEntityType skyEntityType = enumerable2.ElementAt(Main.rand.Next(num));
			this.SpawnForPlayer(player, skyEntityType);
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x00575829 File Offset: 0x00573A29
		public void ResetSpawnTime()
		{
			this._updatesUntilNextAttempt = Main.rand.Next(600, 7200);
			if (Main.tenthAnniversaryWorld)
			{
				this._updatesUntilNextAttempt /= 2;
			}
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x0057585A File Offset: 0x00573A5A
		public void ForceEntitySpawn(AmbienceServer.AmbienceSpawnInfo info)
		{
			this._forcedSpawns.Add(info);
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x00575868 File Offset: 0x00573A68
		private void SpawnForcedEntities()
		{
			if (this._forcedSpawns.Count == 0)
			{
				return;
			}
			for (int i = this._forcedSpawns.Count - 1; i >= 0; i--)
			{
				AmbienceServer.AmbienceSpawnInfo ambienceSpawnInfo = this._forcedSpawns[i];
				Player player;
				if (ambienceSpawnInfo.targetPlayer == -1)
				{
					AmbienceServer.FindPlayerThatCanSeeBackgroundAmbience(out player);
				}
				else
				{
					player = Main.player[ambienceSpawnInfo.targetPlayer];
				}
				if (player != null && this.IsPlayerAtRightHeightForType(ambienceSpawnInfo.skyEntityType, player))
				{
					this.SpawnForPlayer(player, ambienceSpawnInfo.skyEntityType);
				}
				this._forcedSpawns.RemoveAt(i);
			}
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x005758F4 File Offset: 0x00573AF4
		private static void FindPlayerThatCanSeeBackgroundAmbience(out Player player)
		{
			player = null;
			int num = Main.player.Count((Player plr) => plr.active && AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbience(plr));
			if (num == 0)
			{
				return;
			}
			player = Main.player.Where((Player plr) => plr.active && AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbience(plr)).ElementAt(Main.rand.Next(num));
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x0057596D File Offset: 0x00573B6D
		private static bool IsPlayerInAPlaceWhereTheyCanSeeAmbience(Player plr)
		{
			return AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbienceSky(plr) || AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbienceHell(plr);
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x0057597F File Offset: 0x00573B7F
		private static bool IsPlayerInAPlaceWhereTheyCanSeeAmbienceSky(Player plr)
		{
			return (double)plr.position.Y <= Main.worldSurface * 16.0 + 1600.0;
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x005759AB File Offset: 0x00573BAB
		private static bool IsPlayerInAPlaceWhereTheyCanSeeAmbienceHell(Player plr)
		{
			return plr.position.Y >= (float)((Main.UnderworldLayer - 100) * 16);
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x005759C9 File Offset: 0x00573BC9
		private void SpawnForPlayer(Player player, SkyEntityType type)
		{
			NetManager.Instance.BroadcastOrLoopback(NetAmbienceModule.SerializeSkyEntitySpawn(player, type));
		}

		// Token: 0x04005173 RID: 20851
		private const int MINIMUM_SECONDS_BETWEEN_SPAWNS = 10;

		// Token: 0x04005174 RID: 20852
		private const int MAXIMUM_SECONDS_BETWEEN_SPAWNS = 120;

		// Token: 0x04005175 RID: 20853
		private readonly Dictionary<SkyEntityType, Func<bool>> _spawnConditions = new Dictionary<SkyEntityType, Func<bool>>();

		// Token: 0x04005176 RID: 20854
		private readonly Dictionary<SkyEntityType, Func<Player, bool>> _secondarySpawnConditionsPerPlayer = new Dictionary<SkyEntityType, Func<Player, bool>>();

		// Token: 0x04005177 RID: 20855
		private int _updatesUntilNextAttempt;

		// Token: 0x04005178 RID: 20856
		private List<AmbienceServer.AmbienceSpawnInfo> _forcedSpawns = new List<AmbienceServer.AmbienceSpawnInfo>();

		// Token: 0x020008C5 RID: 2245
		public struct AmbienceSpawnInfo
		{
			// Token: 0x0400733A RID: 29498
			public SkyEntityType skyEntityType;

			// Token: 0x0400733B RID: 29499
			public int targetPlayer;
		}

		// Token: 0x020008C6 RID: 2246
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600462F RID: 17967 RVA: 0x006C66CB File Offset: 0x006C48CB
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004630 RID: 17968 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004631 RID: 17969 RVA: 0x006C66D7 File Offset: 0x006C48D7
			internal bool <.ctor>b__10_0()
			{
				return AmbienceServer.IsSunnyDay() && Main.hardMode;
			}

			// Token: 0x06004632 RID: 17970 RVA: 0x006C66E7 File Offset: 0x006C48E7
			internal bool <.ctor>b__10_1()
			{
				return AmbienceServer.IsSunnyDay() && Main.IsItAHappyWindyDay;
			}

			// Token: 0x06004633 RID: 17971 RVA: 0x006C66F7 File Offset: 0x006C48F7
			internal bool <.ctor>b__10_2()
			{
				return AmbienceServer.IsSunnyDay() && !Main.IsItAHappyWindyDay;
			}

			// Token: 0x06004634 RID: 17972 RVA: 0x006C670A File Offset: 0x006C490A
			internal bool <.ctor>b__10_3()
			{
				return !Main.dayTime;
			}

			// Token: 0x06004635 RID: 17973 RVA: 0x006C6714 File Offset: 0x006C4914
			internal bool <.ctor>b__10_4()
			{
				return AmbienceServer.IsSunnyDay() && !Main.IsItAHappyWindyDay && !NPC.TooWindyForButterflies && NPC.butterflyChance < 6;
			}

			// Token: 0x06004636 RID: 17974 RVA: 0x006C6735 File Offset: 0x006C4935
			internal bool <.ctor>b__10_5()
			{
				return Main.dayTime && !Main.eclipse && Main.IsItAHappyWindyDay;
			}

			// Token: 0x06004637 RID: 17975 RVA: 0x006C674C File Offset: 0x006C494C
			internal bool <.ctor>b__10_6()
			{
				return AmbienceServer.IsSunnyDay();
			}

			// Token: 0x06004638 RID: 17976 RVA: 0x006C6753 File Offset: 0x006C4953
			internal bool <.ctor>b__10_7()
			{
				return (AmbienceServer.IsSunset() && AmbienceServer.IsSunnyDay()) || AmbienceServer.IsCalmNight();
			}

			// Token: 0x06004639 RID: 17977 RVA: 0x006C676A File Offset: 0x006C496A
			internal bool <.ctor>b__10_8()
			{
				return AmbienceServer.IsSunnyDay() || AmbienceServer.IsCalmNight();
			}

			// Token: 0x0600463A RID: 17978 RVA: 0x006C674C File Offset: 0x006C494C
			internal bool <.ctor>b__10_9()
			{
				return AmbienceServer.IsSunnyDay();
			}

			// Token: 0x0600463B RID: 17979 RVA: 0x006C66E7 File Offset: 0x006C48E7
			internal bool <.ctor>b__10_10()
			{
				return AmbienceServer.IsSunnyDay() && Main.IsItAHappyWindyDay;
			}

			// Token: 0x0600463C RID: 17980 RVA: 0x006C677A File Offset: 0x006C497A
			internal bool <.ctor>b__10_11()
			{
				return AmbienceServer.IsCalmNight();
			}

			// Token: 0x0600463D RID: 17981 RVA: 0x006C674C File Offset: 0x006C494C
			internal bool <.ctor>b__10_12()
			{
				return AmbienceServer.IsSunnyDay();
			}

			// Token: 0x0600463E RID: 17982 RVA: 0x006C676A File Offset: 0x006C496A
			internal bool <.ctor>b__10_13()
			{
				return AmbienceServer.IsSunnyDay() || AmbienceServer.IsCalmNight();
			}

			// Token: 0x0600463F RID: 17983 RVA: 0x006C676A File Offset: 0x006C496A
			internal bool <.ctor>b__10_14()
			{
				return AmbienceServer.IsSunnyDay() || AmbienceServer.IsCalmNight();
			}

			// Token: 0x06004640 RID: 17984 RVA: 0x000379E9 File Offset: 0x00035BE9
			internal bool <.ctor>b__10_15()
			{
				return true;
			}

			// Token: 0x06004641 RID: 17985 RVA: 0x005BA5D6 File Offset: 0x005B87D6
			internal bool <.ctor>b__10_16(Player player)
			{
				return player.ZoneDesert;
			}

			// Token: 0x06004642 RID: 17986 RVA: 0x005BA627 File Offset: 0x005B8827
			internal bool <.ctor>b__10_17(Player player)
			{
				return player.ZoneHallow;
			}

			// Token: 0x06004643 RID: 17987 RVA: 0x005BA585 File Offset: 0x005B8785
			internal bool <.ctor>b__10_18(Player player)
			{
				return player.ZoneBeach;
			}

			// Token: 0x06004644 RID: 17988 RVA: 0x005BA627 File Offset: 0x005B8827
			internal bool <.ctor>b__10_19(Player player)
			{
				return player.ZoneHallow;
			}

			// Token: 0x06004645 RID: 17989 RVA: 0x005BA627 File Offset: 0x005B8827
			internal bool <.ctor>b__10_20(Player player)
			{
				return player.ZoneHallow;
			}

			// Token: 0x06004646 RID: 17990 RVA: 0x005BA678 File Offset: 0x005B8878
			internal bool <.ctor>b__10_21(Player player)
			{
				return player.ZoneCorrupt;
			}

			// Token: 0x06004647 RID: 17991 RVA: 0x005BA693 File Offset: 0x005B8893
			internal bool <.ctor>b__10_22(Player player)
			{
				return player.ZoneCrimson;
			}

			// Token: 0x06004648 RID: 17992 RVA: 0x005BA5F1 File Offset: 0x005B87F1
			internal bool <.ctor>b__10_23(Player player)
			{
				return player.ZoneJungle;
			}

			// Token: 0x06004649 RID: 17993 RVA: 0x006C6781 File Offset: 0x006C4981
			internal bool <Update>b__12_0(KeyValuePair<SkyEntityType, Func<bool>> pair)
			{
				return pair.Value();
			}

			// Token: 0x0600464A RID: 17994 RVA: 0x006C678F File Offset: 0x006C498F
			internal SkyEntityType <Update>b__12_1(KeyValuePair<SkyEntityType, Func<bool>> pair)
			{
				return pair.Key;
			}

			// Token: 0x0600464B RID: 17995 RVA: 0x000379E9 File Offset: 0x00035BE9
			internal bool <Update>b__12_2(SkyEntityType type)
			{
				return true;
			}

			// Token: 0x0600464C RID: 17996 RVA: 0x000379E9 File Offset: 0x00035BE9
			internal bool <Update>b__12_4(SkyEntityType type)
			{
				return true;
			}

			// Token: 0x0600464D RID: 17997 RVA: 0x000379E9 File Offset: 0x00035BE9
			internal bool <Update>b__12_6(SkyEntityType type)
			{
				return true;
			}

			// Token: 0x0600464E RID: 17998 RVA: 0x006C6798 File Offset: 0x006C4998
			internal bool <FindPlayerThatCanSeeBackgroundAmbience>b__16_0(Player plr)
			{
				return plr.active && AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbience(plr);
			}

			// Token: 0x0600464F RID: 17999 RVA: 0x006C6798 File Offset: 0x006C4998
			internal bool <FindPlayerThatCanSeeBackgroundAmbience>b__16_1(Player plr)
			{
				return plr.active && AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbience(plr);
			}

			// Token: 0x0400733C RID: 29500
			public static readonly AmbienceServer.<>c <>9 = new AmbienceServer.<>c();

			// Token: 0x0400733D RID: 29501
			public static Func<bool> <>9__10_0;

			// Token: 0x0400733E RID: 29502
			public static Func<bool> <>9__10_1;

			// Token: 0x0400733F RID: 29503
			public static Func<bool> <>9__10_2;

			// Token: 0x04007340 RID: 29504
			public static Func<bool> <>9__10_3;

			// Token: 0x04007341 RID: 29505
			public static Func<bool> <>9__10_4;

			// Token: 0x04007342 RID: 29506
			public static Func<bool> <>9__10_5;

			// Token: 0x04007343 RID: 29507
			public static Func<bool> <>9__10_6;

			// Token: 0x04007344 RID: 29508
			public static Func<bool> <>9__10_7;

			// Token: 0x04007345 RID: 29509
			public static Func<bool> <>9__10_8;

			// Token: 0x04007346 RID: 29510
			public static Func<bool> <>9__10_9;

			// Token: 0x04007347 RID: 29511
			public static Func<bool> <>9__10_10;

			// Token: 0x04007348 RID: 29512
			public static Func<bool> <>9__10_11;

			// Token: 0x04007349 RID: 29513
			public static Func<bool> <>9__10_12;

			// Token: 0x0400734A RID: 29514
			public static Func<bool> <>9__10_13;

			// Token: 0x0400734B RID: 29515
			public static Func<bool> <>9__10_14;

			// Token: 0x0400734C RID: 29516
			public static Func<bool> <>9__10_15;

			// Token: 0x0400734D RID: 29517
			public static Func<Player, bool> <>9__10_16;

			// Token: 0x0400734E RID: 29518
			public static Func<Player, bool> <>9__10_17;

			// Token: 0x0400734F RID: 29519
			public static Func<Player, bool> <>9__10_18;

			// Token: 0x04007350 RID: 29520
			public static Func<Player, bool> <>9__10_19;

			// Token: 0x04007351 RID: 29521
			public static Func<Player, bool> <>9__10_20;

			// Token: 0x04007352 RID: 29522
			public static Func<Player, bool> <>9__10_21;

			// Token: 0x04007353 RID: 29523
			public static Func<Player, bool> <>9__10_22;

			// Token: 0x04007354 RID: 29524
			public static Func<Player, bool> <>9__10_23;

			// Token: 0x04007355 RID: 29525
			public static Func<KeyValuePair<SkyEntityType, Func<bool>>, bool> <>9__12_0;

			// Token: 0x04007356 RID: 29526
			public static Func<KeyValuePair<SkyEntityType, Func<bool>>, SkyEntityType> <>9__12_1;

			// Token: 0x04007357 RID: 29527
			public static Func<SkyEntityType, bool> <>9__12_2;

			// Token: 0x04007358 RID: 29528
			public static Func<SkyEntityType, bool> <>9__12_4;

			// Token: 0x04007359 RID: 29529
			public static Func<SkyEntityType, bool> <>9__12_6;

			// Token: 0x0400735A RID: 29530
			public static Func<Player, bool> <>9__16_0;

			// Token: 0x0400735B RID: 29531
			public static Func<Player, bool> <>9__16_1;
		}

		// Token: 0x020008C7 RID: 2247
		[CompilerGenerated]
		private sealed class <>c__DisplayClass12_0
		{
			// Token: 0x06004650 RID: 18000 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass12_0()
			{
			}

			// Token: 0x06004651 RID: 18001 RVA: 0x006C67AC File Offset: 0x006C49AC
			internal bool <Update>b__3(SkyEntityType type)
			{
				return this.<>4__this.IsPlayerAtRightHeightForType(type, this.player) && this.<>4__this._secondarySpawnConditionsPerPlayer.ContainsKey(type) && this.<>4__this._secondarySpawnConditionsPerPlayer[type](this.player);
			}

			// Token: 0x06004652 RID: 18002 RVA: 0x006C6800 File Offset: 0x006C4A00
			internal bool <Update>b__5(SkyEntityType type)
			{
				return this.<>4__this.IsPlayerAtRightHeightForType(type, this.player) && (!this.<>4__this._secondarySpawnConditionsPerPlayer.ContainsKey(type) || this.<>4__this._secondarySpawnConditionsPerPlayer[type](this.player));
			}

			// Token: 0x0400735C RID: 29532
			public AmbienceServer <>4__this;

			// Token: 0x0400735D RID: 29533
			public Player player;
		}
	}
}
