using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000313 RID: 787
	public class Conditions
	{
		// Token: 0x06002714 RID: 10004 RVA: 0x00560F34 File Offset: 0x0055F134
		public static bool SoulOfWhateverConditionCanDrop(DropAttemptInfo info)
		{
			if (info.npc.boss)
			{
				return false;
			}
			if (NPCID.Sets.DontDropDungeonKeysOrSouls[info.npc.type])
			{
				return false;
			}
			int type = info.npc.type;
			if (type <= 15)
			{
				if (type != 1 && type - 13 > 2)
				{
					goto IL_0051;
				}
			}
			else if (type != 121 && type != 535)
			{
				goto IL_0051;
			}
			return false;
			IL_0051:
			if (Main.remixWorld)
			{
				if (!Main.hardMode || info.npc.lifeMax <= 1 || info.npc.friendly || info.npc.value < 1f)
				{
					return false;
				}
			}
			else if (!Main.hardMode || info.npc.lifeMax <= 1 || info.npc.friendly || (double)info.npc.position.Y <= Main.rockLayer * 16.0 || info.npc.value < 1f)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x0000357B File Offset: 0x0000177B
		public Conditions()
		{
		}

		// Token: 0x02000831 RID: 2097
		public class NeverTrue : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004350 RID: 17232 RVA: 0x001DAC3B File Offset: 0x001D8E3B
			public bool CanDrop(DropAttemptInfo info)
			{
				return false;
			}

			// Token: 0x06004351 RID: 17233 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06004352 RID: 17234 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004353 RID: 17235 RVA: 0x0000357B File Offset: 0x0000177B
			public NeverTrue()
			{
			}
		}

		// Token: 0x02000832 RID: 2098
		public class IsUsingSpecificAIValues : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004354 RID: 17236 RVA: 0x006C0DE9 File Offset: 0x006BEFE9
			public IsUsingSpecificAIValues(int aislot, float valueToMatch)
			{
				this.aiSlotToCheck = aislot;
				this.valueToMatch = valueToMatch;
			}

			// Token: 0x06004355 RID: 17237 RVA: 0x006C0DFF File Offset: 0x006BEFFF
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.ai[this.aiSlotToCheck] == this.valueToMatch;
			}

			// Token: 0x06004356 RID: 17238 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06004357 RID: 17239 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x04007280 RID: 29312
			public int aiSlotToCheck;

			// Token: 0x04007281 RID: 29313
			public float valueToMatch;
		}

		// Token: 0x02000833 RID: 2099
		public class FrostMoonDropGatingChance : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004358 RID: 17240 RVA: 0x006C0E1C File Offset: 0x006BF01C
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!Main.snowMoon)
				{
					return false;
				}
				int num = NPC.waveNumber;
				if (Main.expertMode)
				{
					num += 5;
				}
				int num2 = (int)((double)(28 - num) / 2.5);
				if (Main.expertMode)
				{
					num2 -= 2;
				}
				if (num2 < 1)
				{
					num2 = 1;
				}
				return info.player.RollLuck(num2) == 0;
			}

			// Token: 0x06004359 RID: 17241 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600435A RID: 17242 RVA: 0x006C0E74 File Offset: 0x006BF074
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.WaveBasedDrop");
			}

			// Token: 0x0600435B RID: 17243 RVA: 0x0000357B File Offset: 0x0000177B
			public FrostMoonDropGatingChance()
			{
			}
		}

		// Token: 0x02000834 RID: 2100
		public class PumpkinMoonDropGatingChance : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600435C RID: 17244 RVA: 0x006C0E80 File Offset: 0x006BF080
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!Main.pumpkinMoon)
				{
					return false;
				}
				int num = NPC.waveNumber;
				if (Main.expertMode)
				{
					num += 5;
				}
				int num2 = (int)((double)(24 - num) / 2.5);
				if (Main.expertMode)
				{
					num2--;
				}
				if (num2 < 1)
				{
					num2 = 1;
				}
				return info.player.RollLuck(num2) == 0;
			}

			// Token: 0x0600435D RID: 17245 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600435E RID: 17246 RVA: 0x006C0E74 File Offset: 0x006BF074
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.WaveBasedDrop");
			}

			// Token: 0x0600435F RID: 17247 RVA: 0x0000357B File Offset: 0x0000177B
			public PumpkinMoonDropGatingChance()
			{
			}
		}

		// Token: 0x02000835 RID: 2101
		public class FrostMoonDropGateForTrophies : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004360 RID: 17248 RVA: 0x006C0ED8 File Offset: 0x006BF0D8
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!Main.snowMoon)
				{
					return false;
				}
				int waveNumber = NPC.waveNumber;
				if (NPC.waveNumber < 15)
				{
					return false;
				}
				int num = 4;
				if (waveNumber == 16)
				{
					num = 4;
				}
				if (waveNumber == 17)
				{
					num = 3;
				}
				if (waveNumber == 18)
				{
					num = 3;
				}
				if (waveNumber == 19)
				{
					num = 2;
				}
				if (waveNumber >= 20)
				{
					num = 2;
				}
				if (Main.expertMode && Main.rand.Next(3) == 0)
				{
					num--;
				}
				return info.rng.Next(num) == 0;
			}

			// Token: 0x06004361 RID: 17249 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06004362 RID: 17250 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004363 RID: 17251 RVA: 0x0000357B File Offset: 0x0000177B
			public FrostMoonDropGateForTrophies()
			{
			}
		}

		// Token: 0x02000836 RID: 2102
		public class PumpkinMoonDropGateForTrophies : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004364 RID: 17252 RVA: 0x006C0F4C File Offset: 0x006BF14C
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!Main.pumpkinMoon)
				{
					return false;
				}
				int waveNumber = NPC.waveNumber;
				if (NPC.waveNumber < 15)
				{
					return false;
				}
				int num = 4;
				if (waveNumber == 16)
				{
					num = 4;
				}
				if (waveNumber == 17)
				{
					num = 3;
				}
				if (waveNumber == 18)
				{
					num = 3;
				}
				if (waveNumber == 19)
				{
					num = 2;
				}
				if (waveNumber >= 20)
				{
					num = 2;
				}
				if (Main.expertMode && Main.rand.Next(3) == 0)
				{
					num--;
				}
				return info.rng.Next(num) == 0;
			}

			// Token: 0x06004365 RID: 17253 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06004366 RID: 17254 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004367 RID: 17255 RVA: 0x0000357B File Offset: 0x0000177B
			public PumpkinMoonDropGateForTrophies()
			{
			}
		}

		// Token: 0x02000837 RID: 2103
		public class IsPumpkinMoon : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004368 RID: 17256 RVA: 0x006C0FBF File Offset: 0x006BF1BF
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.pumpkinMoon;
			}

			// Token: 0x06004369 RID: 17257 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600436A RID: 17258 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x0600436B RID: 17259 RVA: 0x0000357B File Offset: 0x0000177B
			public IsPumpkinMoon()
			{
			}
		}

		// Token: 0x02000838 RID: 2104
		public class FromCertainWaveAndAbove : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600436C RID: 17260 RVA: 0x006C0FC6 File Offset: 0x006BF1C6
			public FromCertainWaveAndAbove(int neededWave)
			{
				this.neededWave = neededWave;
			}

			// Token: 0x0600436D RID: 17261 RVA: 0x006C0FD5 File Offset: 0x006BF1D5
			public bool CanDrop(DropAttemptInfo info)
			{
				return NPC.waveNumber >= this.neededWave;
			}

			// Token: 0x0600436E RID: 17262 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600436F RID: 17263 RVA: 0x006C0FE7 File Offset: 0x006BF1E7
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.PastWaveBasedDrop", this.neededWave);
			}

			// Token: 0x04007282 RID: 29314
			public int neededWave;
		}

		// Token: 0x02000839 RID: 2105
		public class IsBloodMoonAndNotFromStatue : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004370 RID: 17264 RVA: 0x006C0FFE File Offset: 0x006BF1FE
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.dayTime && Main.bloodMoon && !info.npc.SpawnedFromStatue && !info.IsInSimulation;
			}

			// Token: 0x06004371 RID: 17265 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06004372 RID: 17266 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004373 RID: 17267 RVA: 0x0000357B File Offset: 0x0000177B
			public IsBloodMoonAndNotFromStatue()
			{
			}
		}

		// Token: 0x0200083A RID: 2106
		public class DownedAllMechBosses : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004374 RID: 17268 RVA: 0x006C1026 File Offset: 0x006BF226
			public bool CanDrop(DropAttemptInfo info)
			{
				return NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;
			}

			// Token: 0x06004375 RID: 17269 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06004376 RID: 17270 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004377 RID: 17271 RVA: 0x0000357B File Offset: 0x0000177B
			public DownedAllMechBosses()
			{
			}
		}

		// Token: 0x0200083B RID: 2107
		public class DownedPlantera : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004378 RID: 17272 RVA: 0x006C103D File Offset: 0x006BF23D
			public bool CanDrop(DropAttemptInfo info)
			{
				return NPC.downedPlantBoss;
			}

			// Token: 0x06004379 RID: 17273 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600437A RID: 17274 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x0600437B RID: 17275 RVA: 0x0000357B File Offset: 0x0000177B
			public DownedPlantera()
			{
			}
		}

		// Token: 0x0200083C RID: 2108
		public class IsHardmode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600437C RID: 17276 RVA: 0x004DE19F File Offset: 0x004DC39F
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.hardMode;
			}

			// Token: 0x0600437D RID: 17277 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600437E RID: 17278 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x0600437F RID: 17279 RVA: 0x0000357B File Offset: 0x0000177B
			public IsHardmode()
			{
			}
		}

		// Token: 0x0200083D RID: 2109
		public class FirstTimeKillingPlantera : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004380 RID: 17280 RVA: 0x006C1044 File Offset: 0x006BF244
			public bool CanDrop(DropAttemptInfo info)
			{
				return !NPC.downedPlantBoss;
			}

			// Token: 0x06004381 RID: 17281 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06004382 RID: 17282 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004383 RID: 17283 RVA: 0x0000357B File Offset: 0x0000177B
			public FirstTimeKillingPlantera()
			{
			}
		}

		// Token: 0x0200083E RID: 2110
		public class MechanicalBossesDummyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004384 RID: 17284 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanDrop(DropAttemptInfo info)
			{
				return true;
			}

			// Token: 0x06004385 RID: 17285 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06004386 RID: 17286 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004387 RID: 17287 RVA: 0x0000357B File Offset: 0x0000177B
			public MechanicalBossesDummyCondition()
			{
			}
		}

		// Token: 0x0200083F RID: 2111
		public class PirateMap : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004388 RID: 17288 RVA: 0x006C1050 File Offset: 0x006BF250
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && Main.hardMode && (double)(info.npc.position.Y / 16f) < Main.worldSurface + 10.0 && (info.npc.Center.X / 16f < 380f || info.npc.Center.X / 16f > (float)(Main.maxTilesX - 380)) && !info.IsInSimulation;
			}

			// Token: 0x06004389 RID: 17289 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600438A RID: 17290 RVA: 0x006C10EA File Offset: 0x006BF2EA
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.PirateMap");
			}

			// Token: 0x0600438B RID: 17291 RVA: 0x0000357B File Offset: 0x0000177B
			public PirateMap()
			{
			}
		}

		// Token: 0x02000840 RID: 2112
		public class IsChristmas : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600438C RID: 17292 RVA: 0x006C10F6 File Offset: 0x006BF2F6
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.xMas;
			}

			// Token: 0x0600438D RID: 17293 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600438E RID: 17294 RVA: 0x006C10FD File Offset: 0x006BF2FD
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsChristmas");
			}

			// Token: 0x0600438F RID: 17295 RVA: 0x0000357B File Offset: 0x0000177B
			public IsChristmas()
			{
			}
		}

		// Token: 0x02000841 RID: 2113
		public class NotExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004390 RID: 17296 RVA: 0x006C1109 File Offset: 0x006BF309
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.expertMode;
			}

			// Token: 0x06004391 RID: 17297 RVA: 0x006C1109 File Offset: 0x006BF309
			public bool CanShowItemDropInUI()
			{
				return !Main.expertMode;
			}

			// Token: 0x06004392 RID: 17298 RVA: 0x006C1113 File Offset: 0x006BF313
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.NotExpert");
			}

			// Token: 0x06004393 RID: 17299 RVA: 0x0000357B File Offset: 0x0000177B
			public NotExpert()
			{
			}
		}

		// Token: 0x02000842 RID: 2114
		public class DropExtraGel : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004394 RID: 17300 RVA: 0x006C111F File Offset: 0x006BF31F
			public bool CanDrop(DropAttemptInfo info)
			{
				return SpecialSeedFeatures.ShouldDropExtraGel;
			}

			// Token: 0x06004395 RID: 17301 RVA: 0x006C111F File Offset: 0x006BF31F
			public bool CanShowItemDropInUI()
			{
				return SpecialSeedFeatures.ShouldDropExtraGel;
			}

			// Token: 0x06004396 RID: 17302 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004397 RID: 17303 RVA: 0x0000357B File Offset: 0x0000177B
			public DropExtraGel()
			{
			}
		}

		// Token: 0x02000843 RID: 2115
		public class NotDropExtraGel : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004398 RID: 17304 RVA: 0x006C1126 File Offset: 0x006BF326
			public bool CanDrop(DropAttemptInfo info)
			{
				return !SpecialSeedFeatures.ShouldDropExtraGel;
			}

			// Token: 0x06004399 RID: 17305 RVA: 0x006C1126 File Offset: 0x006BF326
			public bool CanShowItemDropInUI()
			{
				return !SpecialSeedFeatures.ShouldDropExtraGel;
			}

			// Token: 0x0600439A RID: 17306 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x0600439B RID: 17307 RVA: 0x0000357B File Offset: 0x0000177B
			public NotDropExtraGel()
			{
			}
		}

		// Token: 0x02000844 RID: 2116
		public class NotMasterMode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600439C RID: 17308 RVA: 0x006C1130 File Offset: 0x006BF330
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.masterMode;
			}

			// Token: 0x0600439D RID: 17309 RVA: 0x006C1130 File Offset: 0x006BF330
			public bool CanShowItemDropInUI()
			{
				return !Main.masterMode;
			}

			// Token: 0x0600439E RID: 17310 RVA: 0x006C113A File Offset: 0x006BF33A
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.NotMasterMode");
			}

			// Token: 0x0600439F RID: 17311 RVA: 0x0000357B File Offset: 0x0000177B
			public NotMasterMode()
			{
			}
		}

		// Token: 0x02000845 RID: 2117
		public class MissingTwin : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043A0 RID: 17312 RVA: 0x006C1148 File Offset: 0x006BF348
			public bool CanDrop(DropAttemptInfo info)
			{
				int num = 125;
				if (info.npc.type == 125)
				{
					num = 126;
				}
				return !NPC.AnyNPCs(num);
			}

			// Token: 0x060043A1 RID: 17313 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060043A2 RID: 17314 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x060043A3 RID: 17315 RVA: 0x0000357B File Offset: 0x0000177B
			public MissingTwin()
			{
			}
		}

		// Token: 0x02000846 RID: 2118
		public class EmpressOfLightIsGenuinelyEnraged : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043A4 RID: 17316 RVA: 0x006C1173 File Offset: 0x006BF373
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.AI_120_HallowBoss_IsGenuinelyEnraged();
			}

			// Token: 0x060043A5 RID: 17317 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060043A6 RID: 17318 RVA: 0x006C1180 File Offset: 0x006BF380
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.EmpressOfLightOnlyTookDamageWhileEnraged");
			}

			// Token: 0x060043A7 RID: 17319 RVA: 0x0000357B File Offset: 0x0000177B
			public EmpressOfLightIsGenuinelyEnraged()
			{
			}
		}

		// Token: 0x02000847 RID: 2119
		public class RedHatSkeletron : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043A8 RID: 17320 RVA: 0x006C118C File Offset: 0x006BF38C
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.RedHatSkeletronAdjustmentsEnabled();
			}

			// Token: 0x060043A9 RID: 17321 RVA: 0x006C1199 File Offset: 0x006BF399
			public bool CanShowItemDropInUI()
			{
				return Main.Difficulty >= GameDifficultyLevel.Legendary;
			}

			// Token: 0x060043AA RID: 17322 RVA: 0x006C11AA File Offset: 0x006BF3AA
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.RedHatSkeletron");
			}

			// Token: 0x060043AB RID: 17323 RVA: 0x0000357B File Offset: 0x0000177B
			public RedHatSkeletron()
			{
			}
		}

		// Token: 0x02000848 RID: 2120
		public class PlayerNeedsHealing : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043AC RID: 17324 RVA: 0x006C11B6 File Offset: 0x006BF3B6
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.player.statLife < info.player.statLifeMax2;
			}

			// Token: 0x060043AD RID: 17325 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060043AE RID: 17326 RVA: 0x006C11D0 File Offset: 0x006BF3D0
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.PlayerNeedsHealing");
			}

			// Token: 0x060043AF RID: 17327 RVA: 0x0000357B File Offset: 0x0000177B
			public PlayerNeedsHealing()
			{
			}
		}

		// Token: 0x02000849 RID: 2121
		public class MechdusaKill : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043B0 RID: 17328 RVA: 0x006C11DC File Offset: 0x006BF3DC
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!SpecialSeedFeatures.Mechdusa)
				{
					return false;
				}
				for (int i = 0; i < Conditions.MechdusaKill._targetList.Length; i++)
				{
					if (Conditions.MechdusaKill._targetList[i] != info.npc.type && NPC.AnyNPCs(Conditions.MechdusaKill._targetList[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060043B1 RID: 17329 RVA: 0x006C1229 File Offset: 0x006BF429
			public bool CanShowItemDropInUI()
			{
				return SpecialSeedFeatures.Mechdusa;
			}

			// Token: 0x060043B2 RID: 17330 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x060043B3 RID: 17331 RVA: 0x0000357B File Offset: 0x0000177B
			public MechdusaKill()
			{
			}

			// Token: 0x060043B4 RID: 17332 RVA: 0x006C1230 File Offset: 0x006BF430
			// Note: this type is marked as 'beforefieldinit'.
			static MechdusaKill()
			{
			}

			// Token: 0x04007283 RID: 29315
			private static int[] _targetList = new int[] { 127, 126, 125, 134 };
		}

		// Token: 0x0200084A RID: 2122
		public class LegacyHack_IsBossAndExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043B5 RID: 17333 RVA: 0x006C1248 File Offset: 0x006BF448
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.boss && Main.expertMode;
			}

			// Token: 0x060043B6 RID: 17334 RVA: 0x006C125E File Offset: 0x006BF45E
			public bool CanShowItemDropInUI()
			{
				return Main.expertMode;
			}

			// Token: 0x060043B7 RID: 17335 RVA: 0x006C1265 File Offset: 0x006BF465
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.LegacyHack_IsBossAndExpert");
			}

			// Token: 0x060043B8 RID: 17336 RVA: 0x0000357B File Offset: 0x0000177B
			public LegacyHack_IsBossAndExpert()
			{
			}
		}

		// Token: 0x0200084B RID: 2123
		public class LegacyHack_IsBossAndNotExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043B9 RID: 17337 RVA: 0x006C1271 File Offset: 0x006BF471
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.boss && !Main.expertMode;
			}

			// Token: 0x060043BA RID: 17338 RVA: 0x006C1109 File Offset: 0x006BF309
			public bool CanShowItemDropInUI()
			{
				return !Main.expertMode;
			}

			// Token: 0x060043BB RID: 17339 RVA: 0x006C128A File Offset: 0x006BF48A
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.LegacyHack_IsBossAndNotExpert");
			}

			// Token: 0x060043BC RID: 17340 RVA: 0x0000357B File Offset: 0x0000177B
			public LegacyHack_IsBossAndNotExpert()
			{
			}
		}

		// Token: 0x0200084C RID: 2124
		public class LegacyHack_IsABoss : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043BD RID: 17341 RVA: 0x006C1296 File Offset: 0x006BF496
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.boss;
			}

			// Token: 0x060043BE RID: 17342 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060043BF RID: 17343 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x060043C0 RID: 17344 RVA: 0x0000357B File Offset: 0x0000177B
			public LegacyHack_IsABoss()
			{
			}
		}

		// Token: 0x0200084D RID: 2125
		public class IsExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043C1 RID: 17345 RVA: 0x006C125E File Offset: 0x006BF45E
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.expertMode;
			}

			// Token: 0x060043C2 RID: 17346 RVA: 0x006C125E File Offset: 0x006BF45E
			public bool CanShowItemDropInUI()
			{
				return Main.expertMode;
			}

			// Token: 0x060043C3 RID: 17347 RVA: 0x006C12A3 File Offset: 0x006BF4A3
			public string GetConditionDescription()
			{
				if (Main.masterMode)
				{
					return Language.GetTextValue("Bestiary_ItemDropConditions.IsMasterMode");
				}
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsExpert");
			}

			// Token: 0x060043C4 RID: 17348 RVA: 0x0000357B File Offset: 0x0000177B
			public IsExpert()
			{
			}
		}

		// Token: 0x0200084E RID: 2126
		public class IsMasterMode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043C5 RID: 17349 RVA: 0x006C12C1 File Offset: 0x006BF4C1
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.masterMode;
			}

			// Token: 0x060043C6 RID: 17350 RVA: 0x006C12C1 File Offset: 0x006BF4C1
			public bool CanShowItemDropInUI()
			{
				return Main.masterMode;
			}

			// Token: 0x060043C7 RID: 17351 RVA: 0x006C12C8 File Offset: 0x006BF4C8
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsMasterMode");
			}

			// Token: 0x060043C8 RID: 17352 RVA: 0x0000357B File Offset: 0x0000177B
			public IsMasterMode()
			{
			}
		}

		// Token: 0x0200084F RID: 2127
		public class IsCrimson : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043C9 RID: 17353 RVA: 0x006C12D4 File Offset: 0x006BF4D4
			public bool CanDrop(DropAttemptInfo info)
			{
				return WorldGen.crimson;
			}

			// Token: 0x060043CA RID: 17354 RVA: 0x006C12D4 File Offset: 0x006BF4D4
			public bool CanShowItemDropInUI()
			{
				return WorldGen.crimson;
			}

			// Token: 0x060043CB RID: 17355 RVA: 0x006C12DB File Offset: 0x006BF4DB
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsCrimson");
			}

			// Token: 0x060043CC RID: 17356 RVA: 0x0000357B File Offset: 0x0000177B
			public IsCrimson()
			{
			}
		}

		// Token: 0x02000850 RID: 2128
		public class IsCorruption : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043CD RID: 17357 RVA: 0x006C12E7 File Offset: 0x006BF4E7
			public bool CanDrop(DropAttemptInfo info)
			{
				return !WorldGen.crimson;
			}

			// Token: 0x060043CE RID: 17358 RVA: 0x006C12E7 File Offset: 0x006BF4E7
			public bool CanShowItemDropInUI()
			{
				return !WorldGen.crimson;
			}

			// Token: 0x060043CF RID: 17359 RVA: 0x006C12F1 File Offset: 0x006BF4F1
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsCorruption");
			}

			// Token: 0x060043D0 RID: 17360 RVA: 0x0000357B File Offset: 0x0000177B
			public IsCorruption()
			{
			}
		}

		// Token: 0x02000851 RID: 2129
		public class IsCrimsonAndNotExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043D1 RID: 17361 RVA: 0x006C12FD File Offset: 0x006BF4FD
			public bool CanDrop(DropAttemptInfo info)
			{
				return WorldGen.crimson && !Main.expertMode;
			}

			// Token: 0x060043D2 RID: 17362 RVA: 0x006C12FD File Offset: 0x006BF4FD
			public bool CanShowItemDropInUI()
			{
				return WorldGen.crimson && !Main.expertMode;
			}

			// Token: 0x060043D3 RID: 17363 RVA: 0x006C1310 File Offset: 0x006BF510
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsCrimsonAndNotExpert");
			}

			// Token: 0x060043D4 RID: 17364 RVA: 0x0000357B File Offset: 0x0000177B
			public IsCrimsonAndNotExpert()
			{
			}
		}

		// Token: 0x02000852 RID: 2130
		public class IsCorruptionAndNotExpert : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043D5 RID: 17365 RVA: 0x006C131C File Offset: 0x006BF51C
			public bool CanDrop(DropAttemptInfo info)
			{
				return !WorldGen.crimson && !Main.expertMode;
			}

			// Token: 0x060043D6 RID: 17366 RVA: 0x006C131C File Offset: 0x006BF51C
			public bool CanShowItemDropInUI()
			{
				return !WorldGen.crimson && !Main.expertMode;
			}

			// Token: 0x060043D7 RID: 17367 RVA: 0x006C132F File Offset: 0x006BF52F
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsCorruptionAndNotExpert");
			}

			// Token: 0x060043D8 RID: 17368 RVA: 0x0000357B File Offset: 0x0000177B
			public IsCorruptionAndNotExpert()
			{
			}
		}

		// Token: 0x02000853 RID: 2131
		public class HalloweenWeapons : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043D9 RID: 17369 RVA: 0x006C133C File Offset: 0x006BF53C
			public bool CanDrop(DropAttemptInfo info)
			{
				float num = 500f * GameDifficultyData.EnemyMoneyDropMultiplier.Sample(Main.Difficulty);
				float num2 = 40f * GameDifficultyData.EnemyDamageMultiplier.Sample(Main.Difficulty);
				float num3 = 20f;
				return Main.halloween && info.npc.value > 0f && info.npc.value < num && (float)info.npc.damage < num2 && (float)info.npc.defense < num3 && !info.IsInSimulation;
			}

			// Token: 0x060043DA RID: 17370 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060043DB RID: 17371 RVA: 0x006C13D1 File Offset: 0x006BF5D1
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.HalloweenWeapons");
			}

			// Token: 0x060043DC RID: 17372 RVA: 0x0000357B File Offset: 0x0000177B
			public HalloweenWeapons()
			{
			}
		}

		// Token: 0x02000854 RID: 2132
		public class SoulOfNight : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043DD RID: 17373 RVA: 0x006C13DD File Offset: 0x006BF5DD
			public bool CanDrop(DropAttemptInfo info)
			{
				return Conditions.SoulOfWhateverConditionCanDrop(info) && (info.player.ZoneCorrupt || info.player.ZoneCrimson);
			}

			// Token: 0x060043DE RID: 17374 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060043DF RID: 17375 RVA: 0x006C1403 File Offset: 0x006BF603
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.SoulOfNight");
			}

			// Token: 0x060043E0 RID: 17376 RVA: 0x0000357B File Offset: 0x0000177B
			public SoulOfNight()
			{
			}
		}

		// Token: 0x02000855 RID: 2133
		public class SoulOfLight : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043E1 RID: 17377 RVA: 0x006C140F File Offset: 0x006BF60F
			public bool CanDrop(DropAttemptInfo info)
			{
				return Conditions.SoulOfWhateverConditionCanDrop(info) && info.player.ZoneHallow;
			}

			// Token: 0x060043E2 RID: 17378 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060043E3 RID: 17379 RVA: 0x006C1426 File Offset: 0x006BF626
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.SoulOfLight");
			}

			// Token: 0x060043E4 RID: 17380 RVA: 0x0000357B File Offset: 0x0000177B
			public SoulOfLight()
			{
			}
		}

		// Token: 0x02000856 RID: 2134
		public class NotFromStatue : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043E5 RID: 17381 RVA: 0x006C1432 File Offset: 0x006BF632
			public bool CanDrop(DropAttemptInfo info)
			{
				return !info.npc.SpawnedFromStatue;
			}

			// Token: 0x060043E6 RID: 17382 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060043E7 RID: 17383 RVA: 0x006C1442 File Offset: 0x006BF642
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.NotFromStatue");
			}

			// Token: 0x060043E8 RID: 17384 RVA: 0x0000357B File Offset: 0x0000177B
			public NotFromStatue()
			{
			}
		}

		// Token: 0x02000857 RID: 2135
		public class HalloweenGoodieBagDrop : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043E9 RID: 17385 RVA: 0x006C1450 File Offset: 0x006BF650
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.halloween && info.npc.lifeMax > 1 && info.npc.damage > 0 && !info.npc.friendly && info.npc.type != 121 && info.npc.type != 23 && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x060043EA RID: 17386 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060043EB RID: 17387 RVA: 0x006C14C8 File Offset: 0x006BF6C8
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.HalloweenGoodieBagDrop");
			}

			// Token: 0x060043EC RID: 17388 RVA: 0x0000357B File Offset: 0x0000177B
			public HalloweenGoodieBagDrop()
			{
			}
		}

		// Token: 0x02000858 RID: 2136
		public class XmasPresentDrop : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043ED RID: 17389 RVA: 0x006C14D4 File Offset: 0x006BF6D4
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.xMas && info.npc.lifeMax > 1 && info.npc.damage > 0 && !info.npc.friendly && info.npc.type != 121 && info.npc.type != 23 && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x060043EE RID: 17390 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060043EF RID: 17391 RVA: 0x006C154C File Offset: 0x006BF74C
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.XmasPresentDrop");
			}

			// Token: 0x060043F0 RID: 17392 RVA: 0x0000357B File Offset: 0x0000177B
			public XmasPresentDrop()
			{
			}
		}

		// Token: 0x02000859 RID: 2137
		public class LivingFlames : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043F1 RID: 17393 RVA: 0x006C1558 File Offset: 0x006BF758
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.lifeMax > 5 && info.npc.value > 0f && !info.npc.friendly && Main.hardMode && info.npc.position.Y / 16f > (float)Main.UnderworldLayer && !info.IsInSimulation;
			}

			// Token: 0x060043F2 RID: 17394 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060043F3 RID: 17395 RVA: 0x006C15C2 File Offset: 0x006BF7C2
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.LivingFlames");
			}

			// Token: 0x060043F4 RID: 17396 RVA: 0x0000357B File Offset: 0x0000177B
			public LivingFlames()
			{
			}
		}

		// Token: 0x0200085A RID: 2138
		public class NamedNPC : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043F5 RID: 17397 RVA: 0x006C15CE File Offset: 0x006BF7CE
			public NamedNPC(string neededName)
			{
				this.neededName = neededName;
			}

			// Token: 0x060043F6 RID: 17398 RVA: 0x006C15DD File Offset: 0x006BF7DD
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.HasGivenName && info.npc.GivenName == Language.GetTextValue(this.neededName);
			}

			// Token: 0x060043F7 RID: 17399 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060043F8 RID: 17400 RVA: 0x006C1609 File Offset: 0x006BF809
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.NamedNPC");
			}

			// Token: 0x04007284 RID: 29316
			public string neededName;
		}

		// Token: 0x0200085B RID: 2139
		public class HallowKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043F9 RID: 17401 RVA: 0x006C1618 File Offset: 0x006BF818
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && !NPCID.Sets.DontDropDungeonKeysOrSouls[info.npc.type] && Main.hardMode && !info.IsInSimulation && info.player.ZoneHallow;
			}

			// Token: 0x060043FA RID: 17402 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060043FB RID: 17403 RVA: 0x006C1666 File Offset: 0x006BF866
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.HallowKeyCondition");
			}

			// Token: 0x060043FC RID: 17404 RVA: 0x0000357B File Offset: 0x0000177B
			public HallowKeyCondition()
			{
			}
		}

		// Token: 0x0200085C RID: 2140
		public class JungleKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x060043FD RID: 17405 RVA: 0x006C1674 File Offset: 0x006BF874
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && !NPCID.Sets.DontDropDungeonKeysOrSouls[info.npc.type] && Main.hardMode && !info.IsInSimulation && info.player.ZoneJungle;
			}

			// Token: 0x060043FE RID: 17406 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x060043FF RID: 17407 RVA: 0x006C16C2 File Offset: 0x006BF8C2
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.JungleKeyCondition");
			}

			// Token: 0x06004400 RID: 17408 RVA: 0x0000357B File Offset: 0x0000177B
			public JungleKeyCondition()
			{
			}
		}

		// Token: 0x0200085D RID: 2141
		public class CorruptKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004401 RID: 17409 RVA: 0x006C16D0 File Offset: 0x006BF8D0
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && !NPCID.Sets.DontDropDungeonKeysOrSouls[info.npc.type] && Main.hardMode && !info.IsInSimulation && info.player.ZoneCorrupt;
			}

			// Token: 0x06004402 RID: 17410 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06004403 RID: 17411 RVA: 0x006C171E File Offset: 0x006BF91E
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.CorruptKeyCondition");
			}

			// Token: 0x06004404 RID: 17412 RVA: 0x0000357B File Offset: 0x0000177B
			public CorruptKeyCondition()
			{
			}
		}

		// Token: 0x0200085E RID: 2142
		public class CrimsonKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004405 RID: 17413 RVA: 0x006C172C File Offset: 0x006BF92C
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && !NPCID.Sets.DontDropDungeonKeysOrSouls[info.npc.type] && Main.hardMode && !info.IsInSimulation && info.player.ZoneCrimson;
			}

			// Token: 0x06004406 RID: 17414 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06004407 RID: 17415 RVA: 0x006C177A File Offset: 0x006BF97A
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.CrimsonKeyCondition");
			}

			// Token: 0x06004408 RID: 17416 RVA: 0x0000357B File Offset: 0x0000177B
			public CrimsonKeyCondition()
			{
			}
		}

		// Token: 0x0200085F RID: 2143
		public class FrozenKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004409 RID: 17417 RVA: 0x006C1788 File Offset: 0x006BF988
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && !NPCID.Sets.DontDropDungeonKeysOrSouls[info.npc.type] && Main.hardMode && !info.IsInSimulation && info.player.ZoneSnow;
			}

			// Token: 0x0600440A RID: 17418 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600440B RID: 17419 RVA: 0x006C17D6 File Offset: 0x006BF9D6
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.FrozenKeyCondition");
			}

			// Token: 0x0600440C RID: 17420 RVA: 0x0000357B File Offset: 0x0000177B
			public FrozenKeyCondition()
			{
			}
		}

		// Token: 0x02000860 RID: 2144
		public class DesertKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600440D RID: 17421 RVA: 0x006C17E4 File Offset: 0x006BF9E4
			public bool CanDrop(DropAttemptInfo info)
			{
				return info.npc.value > 0f && !NPCID.Sets.DontDropDungeonKeysOrSouls[info.npc.type] && Main.hardMode && !info.IsInSimulation && info.player.ZoneDesert && !info.player.ZoneBeach;
			}

			// Token: 0x0600440E RID: 17422 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600440F RID: 17423 RVA: 0x006C1842 File Offset: 0x006BFA42
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.DesertKeyCondition");
			}

			// Token: 0x06004410 RID: 17424 RVA: 0x0000357B File Offset: 0x0000177B
			public DesertKeyCondition()
			{
			}
		}

		// Token: 0x02000861 RID: 2145
		public class BeatAnyMechBoss : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004411 RID: 17425 RVA: 0x006C184E File Offset: 0x006BFA4E
			public bool CanDrop(DropAttemptInfo info)
			{
				return NPC.downedMechBossAny;
			}

			// Token: 0x06004412 RID: 17426 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06004413 RID: 17427 RVA: 0x006C1855 File Offset: 0x006BFA55
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.BeatAnyMechBoss");
			}

			// Token: 0x06004414 RID: 17428 RVA: 0x0000357B File Offset: 0x0000177B
			public BeatAnyMechBoss()
			{
			}
		}

		// Token: 0x02000862 RID: 2146
		public class YoyoCascade : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004415 RID: 17429 RVA: 0x006C1864 File Offset: 0x006BFA64
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.hardMode && info.npc.HasPlayerTarget && info.npc.lifeMax > 5 && !info.npc.friendly && info.npc.value > 0f && info.npc.position.Y / 16f > (float)(Main.maxTilesY - 350) && NPC.downedBoss3 && !info.IsInSimulation;
			}

			// Token: 0x06004416 RID: 17430 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06004417 RID: 17431 RVA: 0x006C18E8 File Offset: 0x006BFAE8
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.YoyoCascade");
			}

			// Token: 0x06004418 RID: 17432 RVA: 0x0000357B File Offset: 0x0000177B
			public YoyoCascade()
			{
			}
		}

		// Token: 0x02000863 RID: 2147
		public class YoyosAmarok : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004419 RID: 17433 RVA: 0x006C18F4 File Offset: 0x006BFAF4
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.hardMode && info.npc.HasPlayerTarget && info.player.ZoneSnow && info.npc.lifeMax > 5 && !info.npc.friendly && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x0600441A RID: 17434 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600441B RID: 17435 RVA: 0x006C195A File Offset: 0x006BFB5A
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.YoyosAmarok");
			}

			// Token: 0x0600441C RID: 17436 RVA: 0x0000357B File Offset: 0x0000177B
			public YoyosAmarok()
			{
			}
		}

		// Token: 0x02000864 RID: 2148
		public class YoyosYelets : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600441D RID: 17437 RVA: 0x006C1968 File Offset: 0x006BFB68
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.hardMode && info.player.ZoneJungle && NPC.downedMechBossAny && info.npc.lifeMax > 5 && info.npc.HasPlayerTarget && !info.npc.friendly && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x0600441E RID: 17438 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600441F RID: 17439 RVA: 0x006C19D5 File Offset: 0x006BFBD5
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.YoyosYelets");
			}

			// Token: 0x06004420 RID: 17440 RVA: 0x0000357B File Offset: 0x0000177B
			public YoyosYelets()
			{
			}
		}

		// Token: 0x02000865 RID: 2149
		public class YoyosKraken : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004421 RID: 17441 RVA: 0x006C19E4 File Offset: 0x006BFBE4
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.hardMode && info.player.ZoneDungeon && NPC.downedPlantBoss && info.npc.lifeMax > 5 && info.npc.HasPlayerTarget && !info.npc.friendly && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x06004422 RID: 17442 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06004423 RID: 17443 RVA: 0x006C1A51 File Offset: 0x006BFC51
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.YoyosKraken");
			}

			// Token: 0x06004424 RID: 17444 RVA: 0x0000357B File Offset: 0x0000177B
			public YoyosKraken()
			{
			}
		}

		// Token: 0x02000866 RID: 2150
		public class YoyosHelFire : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004425 RID: 17445 RVA: 0x006C1A60 File Offset: 0x006BFC60
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.hardMode && !info.player.ZoneDungeon && (double)(info.npc.position.Y / 16f) > (Main.rockLayer + (double)(Main.maxTilesY * 2)) / 3.0 && info.npc.lifeMax > 5 && info.npc.HasPlayerTarget && !info.npc.friendly && info.npc.value > 0f && !info.IsInSimulation;
			}

			// Token: 0x06004426 RID: 17446 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x06004427 RID: 17447 RVA: 0x006C1AFA File Offset: 0x006BFCFA
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.YoyosHelFire");
			}

			// Token: 0x06004428 RID: 17448 RVA: 0x0000357B File Offset: 0x0000177B
			public YoyosHelFire()
			{
			}
		}

		// Token: 0x02000867 RID: 2151
		public class WindyEnoughForKiteDrops : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004429 RID: 17449 RVA: 0x006C1B06 File Offset: 0x006BFD06
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.WindyEnoughForKiteDrops;
			}

			// Token: 0x0600442A RID: 17450 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CanShowItemDropInUI()
			{
				return true;
			}

			// Token: 0x0600442B RID: 17451 RVA: 0x006C1B0D File Offset: 0x006BFD0D
			public string GetConditionDescription()
			{
				return Language.GetTextValue("Bestiary_ItemDropConditions.IsItAHappyWindyDay");
			}

			// Token: 0x0600442C RID: 17452 RVA: 0x0000357B File Offset: 0x0000177B
			public WindyEnoughForKiteDrops()
			{
			}
		}

		// Token: 0x02000868 RID: 2152
		public class Easymode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600442D RID: 17453 RVA: 0x006C1B19 File Offset: 0x006BFD19
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.hardMode;
			}

			// Token: 0x0600442E RID: 17454 RVA: 0x006C1B19 File Offset: 0x006BFD19
			public bool CanShowItemDropInUI()
			{
				return !Main.hardMode;
			}

			// Token: 0x0600442F RID: 17455 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004430 RID: 17456 RVA: 0x0000357B File Offset: 0x0000177B
			public Easymode()
			{
			}
		}

		// Token: 0x02000869 RID: 2153
		public class RemixSeed : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004431 RID: 17457 RVA: 0x006C1B23 File Offset: 0x006BFD23
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.remixWorld;
			}

			// Token: 0x06004432 RID: 17458 RVA: 0x006C1B23 File Offset: 0x006BFD23
			public bool CanShowItemDropInUI()
			{
				return Main.remixWorld;
			}

			// Token: 0x06004433 RID: 17459 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004434 RID: 17460 RVA: 0x0000357B File Offset: 0x0000177B
			public RemixSeed()
			{
			}
		}

		// Token: 0x0200086A RID: 2154
		public class NotRemixSeed : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004435 RID: 17461 RVA: 0x006C1B2A File Offset: 0x006BFD2A
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.remixWorld;
			}

			// Token: 0x06004436 RID: 17462 RVA: 0x006C1B2A File Offset: 0x006BFD2A
			public bool CanShowItemDropInUI()
			{
				return !Main.remixWorld;
			}

			// Token: 0x06004437 RID: 17463 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004438 RID: 17464 RVA: 0x0000357B File Offset: 0x0000177B
			public NotRemixSeed()
			{
			}
		}

		// Token: 0x0200086B RID: 2155
		public class RemixSeedEasymode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004439 RID: 17465 RVA: 0x006C1B34 File Offset: 0x006BFD34
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.remixWorld && !Main.hardMode;
			}

			// Token: 0x0600443A RID: 17466 RVA: 0x006C1B34 File Offset: 0x006BFD34
			public bool CanShowItemDropInUI()
			{
				return Main.remixWorld && !Main.hardMode;
			}

			// Token: 0x0600443B RID: 17467 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x0600443C RID: 17468 RVA: 0x0000357B File Offset: 0x0000177B
			public RemixSeedEasymode()
			{
			}
		}

		// Token: 0x0200086C RID: 2156
		public class RemixSeedHardmode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600443D RID: 17469 RVA: 0x006C1B47 File Offset: 0x006BFD47
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.remixWorld && Main.hardMode;
			}

			// Token: 0x0600443E RID: 17470 RVA: 0x006C1B47 File Offset: 0x006BFD47
			public bool CanShowItemDropInUI()
			{
				return Main.remixWorld && Main.hardMode;
			}

			// Token: 0x0600443F RID: 17471 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004440 RID: 17472 RVA: 0x0000357B File Offset: 0x0000177B
			public RemixSeedHardmode()
			{
			}
		}

		// Token: 0x0200086D RID: 2157
		public class NotRemixSeedEasymode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004441 RID: 17473 RVA: 0x006C1B57 File Offset: 0x006BFD57
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.remixWorld && !Main.hardMode;
			}

			// Token: 0x06004442 RID: 17474 RVA: 0x006C1B57 File Offset: 0x006BFD57
			public bool CanShowItemDropInUI()
			{
				return !Main.remixWorld && !Main.hardMode;
			}

			// Token: 0x06004443 RID: 17475 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004444 RID: 17476 RVA: 0x0000357B File Offset: 0x0000177B
			public NotRemixSeedEasymode()
			{
			}
		}

		// Token: 0x0200086E RID: 2158
		public class NotRemixSeedHardmode : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004445 RID: 17477 RVA: 0x006C1B6A File Offset: 0x006BFD6A
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.remixWorld && Main.hardMode;
			}

			// Token: 0x06004446 RID: 17478 RVA: 0x006C1B6A File Offset: 0x006BFD6A
			public bool CanShowItemDropInUI()
			{
				return !Main.remixWorld && Main.hardMode;
			}

			// Token: 0x06004447 RID: 17479 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004448 RID: 17480 RVA: 0x0000357B File Offset: 0x0000177B
			public NotRemixSeedHardmode()
			{
			}
		}

		// Token: 0x0200086F RID: 2159
		public class EyeOfCthulhuDefeatedAndNoAltarsInWorld : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004449 RID: 17481 RVA: 0x006C1B7A File Offset: 0x006BFD7A
			public bool CanDrop(DropAttemptInfo info)
			{
				return NPC.downedBoss1 && WorldGen.Skyblock.noAltars;
			}

			// Token: 0x0600444A RID: 17482 RVA: 0x001DAC3B File Offset: 0x001D8E3B
			public bool CanShowItemDropInUI()
			{
				return false;
			}

			// Token: 0x0600444B RID: 17483 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x0600444C RID: 17484 RVA: 0x0000357B File Offset: 0x0000177B
			public EyeOfCthulhuDefeatedAndNoAltarsInWorld()
			{
			}
		}

		// Token: 0x02000870 RID: 2160
		public class TenthAnniversaryIsUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600444D RID: 17485 RVA: 0x006C1B8A File Offset: 0x006BFD8A
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.tenthAnniversaryWorld;
			}

			// Token: 0x0600444E RID: 17486 RVA: 0x006C1B8A File Offset: 0x006BFD8A
			public bool CanShowItemDropInUI()
			{
				return Main.tenthAnniversaryWorld;
			}

			// Token: 0x0600444F RID: 17487 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004450 RID: 17488 RVA: 0x0000357B File Offset: 0x0000177B
			public TenthAnniversaryIsUp()
			{
			}
		}

		// Token: 0x02000871 RID: 2161
		public class TenthAnniversaryIsNotUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004451 RID: 17489 RVA: 0x006C1B91 File Offset: 0x006BFD91
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.tenthAnniversaryWorld;
			}

			// Token: 0x06004452 RID: 17490 RVA: 0x006C1B91 File Offset: 0x006BFD91
			public bool CanShowItemDropInUI()
			{
				return !Main.tenthAnniversaryWorld;
			}

			// Token: 0x06004453 RID: 17491 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004454 RID: 17492 RVA: 0x0000357B File Offset: 0x0000177B
			public TenthAnniversaryIsNotUp()
			{
			}
		}

		// Token: 0x02000872 RID: 2162
		public class DontStarveIsUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004455 RID: 17493 RVA: 0x006C1B9B File Offset: 0x006BFD9B
			public bool CanDrop(DropAttemptInfo info)
			{
				return Main.dontStarveWorld;
			}

			// Token: 0x06004456 RID: 17494 RVA: 0x006C1B9B File Offset: 0x006BFD9B
			public bool CanShowItemDropInUI()
			{
				return Main.dontStarveWorld;
			}

			// Token: 0x06004457 RID: 17495 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004458 RID: 17496 RVA: 0x0000357B File Offset: 0x0000177B
			public DontStarveIsUp()
			{
			}
		}

		// Token: 0x02000873 RID: 2163
		public class DontStarveIsNotUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004459 RID: 17497 RVA: 0x006C1BA2 File Offset: 0x006BFDA2
			public bool CanDrop(DropAttemptInfo info)
			{
				return !Main.dontStarveWorld;
			}

			// Token: 0x0600445A RID: 17498 RVA: 0x006C1BA2 File Offset: 0x006BFDA2
			public bool CanShowItemDropInUI()
			{
				return !Main.dontStarveWorld;
			}

			// Token: 0x0600445B RID: 17499 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x0600445C RID: 17500 RVA: 0x0000357B File Offset: 0x0000177B
			public DontStarveIsNotUp()
			{
			}
		}

		// Token: 0x02000874 RID: 2164
		public class SkyblockIsUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x0600445D RID: 17501 RVA: 0x006C1BAC File Offset: 0x006BFDAC
			public bool CanDrop(DropAttemptInfo info)
			{
				return WorldGen.Skyblock.lowTiles;
			}

			// Token: 0x0600445E RID: 17502 RVA: 0x006C1BAC File Offset: 0x006BFDAC
			public bool CanShowItemDropInUI()
			{
				return WorldGen.Skyblock.lowTiles;
			}

			// Token: 0x0600445F RID: 17503 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004460 RID: 17504 RVA: 0x0000357B File Offset: 0x0000177B
			public SkyblockIsUp()
			{
			}
		}

		// Token: 0x02000875 RID: 2165
		public class SkyblockIsNotUp : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004461 RID: 17505 RVA: 0x006C1BB3 File Offset: 0x006BFDB3
			public bool CanDrop(DropAttemptInfo info)
			{
				return !WorldGen.Skyblock.lowTiles;
			}

			// Token: 0x06004462 RID: 17506 RVA: 0x006C1BB3 File Offset: 0x006BFDB3
			public bool CanShowItemDropInUI()
			{
				return !WorldGen.Skyblock.lowTiles;
			}

			// Token: 0x06004463 RID: 17507 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004464 RID: 17508 RVA: 0x0000357B File Offset: 0x0000177B
			public SkyblockIsNotUp()
			{
			}
		}

		// Token: 0x02000876 RID: 2166
		public class SkyblockIsUpNoSickle : IItemDropRuleCondition, IProvideItemConditionDescription
		{
			// Token: 0x06004465 RID: 17509 RVA: 0x006C1BBD File Offset: 0x006BFDBD
			public bool CanDrop(DropAttemptInfo info)
			{
				return WorldGen.Skyblock.lowTiles && !info.player.HasItemInInventoryOrOpenVoidBag(1786);
			}

			// Token: 0x06004466 RID: 17510 RVA: 0x001DAC3B File Offset: 0x001D8E3B
			public bool CanShowItemDropInUI()
			{
				return false;
			}

			// Token: 0x06004467 RID: 17511 RVA: 0x00076333 File Offset: 0x00074533
			public string GetConditionDescription()
			{
				return null;
			}

			// Token: 0x06004468 RID: 17512 RVA: 0x0000357B File Offset: 0x0000177B
			public SkyblockIsUpNoSickle()
			{
			}
		}
	}
}
