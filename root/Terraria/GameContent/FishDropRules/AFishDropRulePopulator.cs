using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.FishDropRules
{
	// Token: 0x0200047B RID: 1147
	public abstract class AFishDropRulePopulator
	{
		// Token: 0x0600332B RID: 13099 RVA: 0x005F3D3C File Offset: 0x005F1F3C
		public AFishDropRulePopulator(FishDropRuleList list)
		{
			this._list = list;
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x005F4368 File Offset: 0x005F2568
		protected void Add(FishRarityCondition tier, int chanceNominator, int chanceDenominator, int[] itemTypes, params AFishingCondition[] conditions)
		{
			FishDropRule fishDropRule = new FishDropRule
			{
				PossibleItems = itemTypes,
				ChanceNumerator = chanceNominator,
				ChanceDenominator = chanceDenominator,
				Rarity = tier,
				Conditions = conditions
			};
			this._list.Add(fishDropRule);
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x005F43AC File Offset: 0x005F25AC
		protected void Add(FishRarityCondition tier, int chanceNominator, int chanceDenominator, int itemType, params AFishingCondition[] conditions)
		{
			this.Add(tier, chanceNominator, chanceDenominator, this.Group(new int[] { itemType }), conditions);
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x005F43D5 File Offset: 0x005F25D5
		protected void Add(FishRarityCondition tier, int chanceDenominator, int[] itemTypes, params AFishingCondition[] conditions)
		{
			this.Add(tier, 1, chanceDenominator, itemTypes, conditions);
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x005F43E4 File Offset: 0x005F25E4
		protected void Add(FishRarityCondition tier, int chanceDenominator, int itemType, params AFishingCondition[] conditions)
		{
			this.Add(tier, 1, chanceDenominator, this.Group(new int[] { itemType }), conditions);
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x005F440C File Offset: 0x005F260C
		protected void AddQuestFish(FishRarityCondition tier, int chanceDenominator, int itemType, params AFishingCondition[] conditions)
		{
			FishingConditions.QuestFishCondition questFishCondition = new FishingConditions.QuestFishCondition
			{
				CheckedType = itemType
			};
			this.Add(tier, 1, chanceDenominator, this.Group(new int[] { itemType }), this.Join(conditions, new AFishingCondition[] { questFishCondition }));
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x005F4454 File Offset: 0x005F2654
		protected void AddQuestFishForRemix(FishRarityCondition tier, int chanceDenominator, int itemType, params AFishingCondition[] conditions)
		{
			FishingConditions.QuestFishConditionRemix questFishConditionRemix = new FishingConditions.QuestFishConditionRemix
			{
				CheckedType = itemType
			};
			this.Add(tier, 1, chanceDenominator, this.Group(new int[] { itemType }), this.Join(conditions, new AFishingCondition[] { questFishConditionRemix }));
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x005F449C File Offset: 0x005F269C
		protected void AddWithHardmode(FishRarityCondition tier, int chanceDenominator, int itemTypeEarly, int itemTypeHard, params AFishingCondition[] conditions)
		{
			FishDropRule fishDropRule = new FishDropRule
			{
				PossibleItems = new int[] { itemTypeEarly },
				ChanceNumerator = 1,
				ChanceDenominator = chanceDenominator,
				Rarity = tier,
				Conditions = this.Join(conditions, new AFishingCondition[] { this.EarlyMode })
			};
			this._list.Add(fishDropRule);
			FishDropRule fishDropRule2 = new FishDropRule
			{
				PossibleItems = new int[] { itemTypeHard },
				ChanceNumerator = 1,
				ChanceDenominator = chanceDenominator,
				Rarity = tier,
				Conditions = this.Join(conditions, new AFishingCondition[] { this.HardMode })
			};
			this._list.Add(fishDropRule2);
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x005F4556 File Offset: 0x005F2756
		protected void AddStopper(AFishingCondition condition)
		{
			this.Add(AFishDropRulePopulator.Rarity.Any, 1, new int[0], new AFishingCondition[] { condition });
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x001FC6F1 File Offset: 0x001FA8F1
		public int[] Group(params int[] itemTypes)
		{
			return itemTypes;
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x005F4574 File Offset: 0x005F2774
		protected AFishingCondition[] Join(AFishingCondition[] original, params AFishingCondition[] additions)
		{
			return original.Concat(additions).ToArray<AFishingCondition>();
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x001FC6F1 File Offset: 0x001FA8F1
		protected AFishingCondition[] Join(params AFishingCondition[] additions)
		{
			return additions;
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x005F4582 File Offset: 0x005F2782
		private static bool IsHardmode(bool state)
		{
			return Main.hardMode == state;
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x005F458C File Offset: 0x005F278C
		private static bool IsOriginalOcean(FishingContext context)
		{
			return context.Fisher.heightLevel <= 1 && (context.Fisher.X < 380 || context.Fisher.X > Main.maxTilesX - 380) && context.Fisher.waterTilesCount > 1000;
		}

		// Token: 0x04005898 RID: 22680
		private FishDropRuleList _list;

		// Token: 0x04005899 RID: 22681
		protected AFishingCondition HardMode = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => AFishDropRulePopulator.IsHardmode(true));

		// Token: 0x0400589A RID: 22682
		protected AFishingCondition EarlyMode = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => AFishDropRulePopulator.IsHardmode(false));

		// Token: 0x0400589B RID: 22683
		protected AFishingCondition InLava = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Fisher.inLava);

		// Token: 0x0400589C RID: 22684
		protected AFishingCondition InHoney = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Fisher.inHoney);

		// Token: 0x0400589D RID: 22685
		protected AFishingCondition Junk = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Fisher.junk);

		// Token: 0x0400589E RID: 22686
		protected AFishingCondition Crate = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Fisher.crate);

		// Token: 0x0400589F RID: 22687
		protected AFishingCondition AnyEnemies = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Fisher.rolledEnemySpawn > 0);

		// Token: 0x040058A0 RID: 22688
		protected AFishingCondition CanFishInLava = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Fisher.CanFishInLava);

		// Token: 0x040058A1 RID: 22689
		protected AFishingCondition Dungeon = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Player.ZoneDungeon && NPC.downedBoss3);

		// Token: 0x040058A2 RID: 22690
		protected AFishingCondition Beach = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Player.ZoneBeach);

		// Token: 0x040058A3 RID: 22691
		protected AFishingCondition Hallow = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Player.ZoneHallow);

		// Token: 0x040058A4 RID: 22692
		protected AFishingCondition GlowingMushrooms = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Player.ZoneGlowshroom);

		// Token: 0x040058A5 RID: 22693
		protected AFishingCondition TrueDesert = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Player.ZoneDesert);

		// Token: 0x040058A6 RID: 22694
		protected AFishingCondition TrueSnow = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Player.ZoneSnow);

		// Token: 0x040058A7 RID: 22695
		protected AFishingCondition Remix = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => Main.remixWorld);

		// Token: 0x040058A8 RID: 22696
		protected AFishingCondition Height1 = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Fisher.heightLevel == 1);

		// Token: 0x040058A9 RID: 22697
		protected AFishingCondition Height1And2 = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Fisher.heightLevel == 1 || context.Fisher.heightLevel == 2);

		// Token: 0x040058AA RID: 22698
		protected AFishingCondition HeightAbove1 = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Fisher.heightLevel > 1);

		// Token: 0x040058AB RID: 22699
		protected AFishingCondition HeightAboveAnd1 = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Fisher.heightLevel >= 1);

		// Token: 0x040058AC RID: 22700
		protected AFishingCondition HeightUnder2 = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Fisher.heightLevel < 2);

		// Token: 0x040058AD RID: 22701
		protected AFishingCondition HeightAbove2 = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Fisher.heightLevel > 2);

		// Token: 0x040058AE RID: 22702
		protected AFishingCondition Height0 = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Fisher.heightLevel == 0);

		// Token: 0x040058AF RID: 22703
		protected AFishingCondition Height2 = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Fisher.heightLevel == 2);

		// Token: 0x040058B0 RID: 22704
		protected AFishingCondition Height3 = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Fisher.heightLevel == 3);

		// Token: 0x040058B1 RID: 22705
		protected AFishingCondition UnderRockLayer = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => (double)context.Fisher.Y >= Main.rockLayer);

		// Token: 0x040058B2 RID: 22706
		protected AFishingCondition Corruption = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.RolledCorruption);

		// Token: 0x040058B3 RID: 22707
		protected AFishingCondition Crimson = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.RolledCrimson);

		// Token: 0x040058B4 RID: 22708
		protected AFishingCondition Jungle = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.RolledJungle);

		// Token: 0x040058B5 RID: 22709
		protected AFishingCondition Snow = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.RolledSnow);

		// Token: 0x040058B6 RID: 22710
		protected AFishingCondition Desert = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.RolledDesert);

		// Token: 0x040058B7 RID: 22711
		protected AFishingCondition RolledHallowDesert = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.RolledInfectedDesert && context.Player.ZoneHallow);

		// Token: 0x040058B8 RID: 22712
		protected AFishingCondition OriginalOcean = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => AFishDropRulePopulator.IsOriginalOcean(context));

		// Token: 0x040058B9 RID: 22713
		protected AFishingCondition RemixOcean = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.RolledRemixOcean);

		// Token: 0x040058BA RID: 22714
		protected AFishingCondition Ocean = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.RolledRemixOcean || AFishDropRulePopulator.IsOriginalOcean(context));

		// Token: 0x040058BB RID: 22715
		protected AFishingCondition Water1000 = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => context.Fisher.waterTilesCount > 1000);

		// Token: 0x040058BC RID: 22716
		protected AFishingCondition BloodMoon = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => Main.bloodMoon);

		// Token: 0x040058BD RID: 22717
		protected AFishingCondition DidNotUseCombatBook = new AFishDropRulePopulator.DelegateFishingCondition((FishingContext context) => !NPC.combatBookWasUsed);

		// Token: 0x02000974 RID: 2420
		private class DelegateFishingCondition : AFishingCondition
		{
			// Token: 0x0600490B RID: 18699 RVA: 0x006D093E File Offset: 0x006CEB3E
			public DelegateFishingCondition(AFishDropRulePopulator.DelegateFishingCondition.MatchCondition innerCondition)
			{
				this._condition = innerCondition;
			}

			// Token: 0x0600490C RID: 18700 RVA: 0x006D094D File Offset: 0x006CEB4D
			public override bool Matches(FishingContext context)
			{
				return this._condition(context);
			}

			// Token: 0x040075E8 RID: 30184
			private AFishDropRulePopulator.DelegateFishingCondition.MatchCondition _condition;

			// Token: 0x02000AEA RID: 2794
			// (Invoke) Token: 0x06004D04 RID: 19716
			public delegate bool MatchCondition(FishingContext context);
		}

		// Token: 0x02000975 RID: 2421
		private class DelegateFishingRarityCondition : FishRarityCondition
		{
			// Token: 0x0600490D RID: 18701 RVA: 0x006D095B File Offset: 0x006CEB5B
			public DelegateFishingRarityCondition(AFishDropRulePopulator.DelegateFishingRarityCondition.MatchCondition innerCondition)
			{
				this._condition = innerCondition;
			}

			// Token: 0x0600490E RID: 18702 RVA: 0x006D096A File Offset: 0x006CEB6A
			public override bool Matches(FishingContext context)
			{
				return this._condition(context);
			}

			// Token: 0x040075E9 RID: 30185
			private AFishDropRulePopulator.DelegateFishingRarityCondition.MatchCondition _condition;

			// Token: 0x02000AEB RID: 2795
			// (Invoke) Token: 0x06004D08 RID: 19720
			public delegate bool MatchCondition(FishingContext context);
		}

		// Token: 0x02000976 RID: 2422
		protected class Rarity
		{
			// Token: 0x0600490F RID: 18703 RVA: 0x0000357B File Offset: 0x0000177B
			public Rarity()
			{
			}

			// Token: 0x06004910 RID: 18704 RVA: 0x006D0978 File Offset: 0x006CEB78
			// Note: this type is marked as 'beforefieldinit'.
			static Rarity()
			{
			}

			// Token: 0x040075EA RID: 30186
			public static FishRarityCondition Any = new AFishDropRulePopulator.DelegateFishingRarityCondition((FishingContext context) => true)
			{
				HackedIsAny = true,
				FrequencyOfAppearanceForVisuals = 1f
			};

			// Token: 0x040075EB RID: 30187
			public static FishRarityCondition Legendary = new AFishDropRulePopulator.DelegateFishingRarityCondition((FishingContext context) => context.Fisher.legendary)
			{
				FrequencyOfAppearanceForVisuals = 0.1f
			};

			// Token: 0x040075EC RID: 30188
			public static FishRarityCondition VeryRare = new AFishDropRulePopulator.DelegateFishingRarityCondition((FishingContext context) => context.Fisher.veryrare)
			{
				FrequencyOfAppearanceForVisuals = 0.25f
			};

			// Token: 0x040075ED RID: 30189
			public static FishRarityCondition Rare = new AFishDropRulePopulator.DelegateFishingRarityCondition((FishingContext context) => context.Fisher.rare)
			{
				FrequencyOfAppearanceForVisuals = 0.4f
			};

			// Token: 0x040075EE RID: 30190
			public static FishRarityCondition Uncommon = new AFishDropRulePopulator.DelegateFishingRarityCondition((FishingContext context) => context.Fisher.uncommon)
			{
				FrequencyOfAppearanceForVisuals = 0.8f
			};

			// Token: 0x040075EF RID: 30191
			public static FishRarityCondition Common = new AFishDropRulePopulator.DelegateFishingRarityCondition((FishingContext context) => context.Fisher.common)
			{
				FrequencyOfAppearanceForVisuals = 1f
			};

			// Token: 0x040075F0 RID: 30192
			public static FishRarityCondition BombRarityOfNotLegendaryAndNotVeryRareAndUncommon = new AFishDropRulePopulator.DelegateFishingRarityCondition((FishingContext context) => !context.Fisher.legendary && !context.Fisher.veryrare && context.Fisher.uncommon)
			{
				FrequencyOfAppearanceForVisuals = 0.6f
			};

			// Token: 0x040075F1 RID: 30193
			public static FishRarityCondition UncommonOrCommon = new AFishDropRulePopulator.DelegateFishingRarityCondition((FishingContext context) => context.Fisher.uncommon || context.Fisher.common)
			{
				FrequencyOfAppearanceForVisuals = 1f
			};

			// Token: 0x02000AEC RID: 2796
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004D0B RID: 19723 RVA: 0x006DB7F8 File Offset: 0x006D99F8
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004D0C RID: 19724 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004D0D RID: 19725 RVA: 0x000379E9 File Offset: 0x00035BE9
				internal bool <.cctor>b__9_0(FishingContext context)
				{
					return true;
				}

				// Token: 0x06004D0E RID: 19726 RVA: 0x006DB804 File Offset: 0x006D9A04
				internal bool <.cctor>b__9_1(FishingContext context)
				{
					return context.Fisher.legendary;
				}

				// Token: 0x06004D0F RID: 19727 RVA: 0x006DB811 File Offset: 0x006D9A11
				internal bool <.cctor>b__9_2(FishingContext context)
				{
					return context.Fisher.veryrare;
				}

				// Token: 0x06004D10 RID: 19728 RVA: 0x006DB81E File Offset: 0x006D9A1E
				internal bool <.cctor>b__9_3(FishingContext context)
				{
					return context.Fisher.rare;
				}

				// Token: 0x06004D11 RID: 19729 RVA: 0x006DB82B File Offset: 0x006D9A2B
				internal bool <.cctor>b__9_4(FishingContext context)
				{
					return context.Fisher.uncommon;
				}

				// Token: 0x06004D12 RID: 19730 RVA: 0x006DB838 File Offset: 0x006D9A38
				internal bool <.cctor>b__9_5(FishingContext context)
				{
					return context.Fisher.common;
				}

				// Token: 0x06004D13 RID: 19731 RVA: 0x006DB845 File Offset: 0x006D9A45
				internal bool <.cctor>b__9_6(FishingContext context)
				{
					return !context.Fisher.legendary && !context.Fisher.veryrare && context.Fisher.uncommon;
				}

				// Token: 0x06004D14 RID: 19732 RVA: 0x006DB86E File Offset: 0x006D9A6E
				internal bool <.cctor>b__9_7(FishingContext context)
				{
					return context.Fisher.uncommon || context.Fisher.common;
				}

				// Token: 0x040078BA RID: 30906
				public static readonly AFishDropRulePopulator.Rarity.<>c <>9 = new AFishDropRulePopulator.Rarity.<>c();
			}
		}

		// Token: 0x02000977 RID: 2423
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004911 RID: 18705 RVA: 0x006D0AB4 File Offset: 0x006CECB4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004912 RID: 18706 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004913 RID: 18707 RVA: 0x006D0AC0 File Offset: 0x006CECC0
			internal bool <.ctor>b__1_0(FishingContext context)
			{
				return AFishDropRulePopulator.IsHardmode(true);
			}

			// Token: 0x06004914 RID: 18708 RVA: 0x006D0AC8 File Offset: 0x006CECC8
			internal bool <.ctor>b__1_1(FishingContext context)
			{
				return AFishDropRulePopulator.IsHardmode(false);
			}

			// Token: 0x06004915 RID: 18709 RVA: 0x006D0AD0 File Offset: 0x006CECD0
			internal bool <.ctor>b__1_2(FishingContext context)
			{
				return context.Fisher.inLava;
			}

			// Token: 0x06004916 RID: 18710 RVA: 0x006D0ADD File Offset: 0x006CECDD
			internal bool <.ctor>b__1_3(FishingContext context)
			{
				return context.Fisher.inHoney;
			}

			// Token: 0x06004917 RID: 18711 RVA: 0x006D0AEA File Offset: 0x006CECEA
			internal bool <.ctor>b__1_4(FishingContext context)
			{
				return context.Fisher.junk;
			}

			// Token: 0x06004918 RID: 18712 RVA: 0x006D0AF7 File Offset: 0x006CECF7
			internal bool <.ctor>b__1_5(FishingContext context)
			{
				return context.Fisher.crate;
			}

			// Token: 0x06004919 RID: 18713 RVA: 0x006D0B04 File Offset: 0x006CED04
			internal bool <.ctor>b__1_6(FishingContext context)
			{
				return context.Fisher.rolledEnemySpawn > 0;
			}

			// Token: 0x0600491A RID: 18714 RVA: 0x006D0B14 File Offset: 0x006CED14
			internal bool <.ctor>b__1_7(FishingContext context)
			{
				return context.Fisher.CanFishInLava;
			}

			// Token: 0x0600491B RID: 18715 RVA: 0x006D0B21 File Offset: 0x006CED21
			internal bool <.ctor>b__1_8(FishingContext context)
			{
				return context.Player.ZoneDungeon && NPC.downedBoss3;
			}

			// Token: 0x0600491C RID: 18716 RVA: 0x006D0B37 File Offset: 0x006CED37
			internal bool <.ctor>b__1_9(FishingContext context)
			{
				return context.Player.ZoneBeach;
			}

			// Token: 0x0600491D RID: 18717 RVA: 0x006D0B44 File Offset: 0x006CED44
			internal bool <.ctor>b__1_10(FishingContext context)
			{
				return context.Player.ZoneHallow;
			}

			// Token: 0x0600491E RID: 18718 RVA: 0x006D0B51 File Offset: 0x006CED51
			internal bool <.ctor>b__1_11(FishingContext context)
			{
				return context.Player.ZoneGlowshroom;
			}

			// Token: 0x0600491F RID: 18719 RVA: 0x006D0B5E File Offset: 0x006CED5E
			internal bool <.ctor>b__1_12(FishingContext context)
			{
				return context.Player.ZoneDesert;
			}

			// Token: 0x06004920 RID: 18720 RVA: 0x006D0B6B File Offset: 0x006CED6B
			internal bool <.ctor>b__1_13(FishingContext context)
			{
				return context.Player.ZoneSnow;
			}

			// Token: 0x06004921 RID: 18721 RVA: 0x006C1B23 File Offset: 0x006BFD23
			internal bool <.ctor>b__1_14(FishingContext context)
			{
				return Main.remixWorld;
			}

			// Token: 0x06004922 RID: 18722 RVA: 0x006D0B78 File Offset: 0x006CED78
			internal bool <.ctor>b__1_15(FishingContext context)
			{
				return context.Fisher.heightLevel == 1;
			}

			// Token: 0x06004923 RID: 18723 RVA: 0x006D0B88 File Offset: 0x006CED88
			internal bool <.ctor>b__1_16(FishingContext context)
			{
				return context.Fisher.heightLevel == 1 || context.Fisher.heightLevel == 2;
			}

			// Token: 0x06004924 RID: 18724 RVA: 0x006D0BA8 File Offset: 0x006CEDA8
			internal bool <.ctor>b__1_17(FishingContext context)
			{
				return context.Fisher.heightLevel > 1;
			}

			// Token: 0x06004925 RID: 18725 RVA: 0x006D0BB8 File Offset: 0x006CEDB8
			internal bool <.ctor>b__1_18(FishingContext context)
			{
				return context.Fisher.heightLevel >= 1;
			}

			// Token: 0x06004926 RID: 18726 RVA: 0x006D0BCB File Offset: 0x006CEDCB
			internal bool <.ctor>b__1_19(FishingContext context)
			{
				return context.Fisher.heightLevel < 2;
			}

			// Token: 0x06004927 RID: 18727 RVA: 0x006D0BDB File Offset: 0x006CEDDB
			internal bool <.ctor>b__1_20(FishingContext context)
			{
				return context.Fisher.heightLevel > 2;
			}

			// Token: 0x06004928 RID: 18728 RVA: 0x006D0BEB File Offset: 0x006CEDEB
			internal bool <.ctor>b__1_21(FishingContext context)
			{
				return context.Fisher.heightLevel == 0;
			}

			// Token: 0x06004929 RID: 18729 RVA: 0x006D0BFB File Offset: 0x006CEDFB
			internal bool <.ctor>b__1_22(FishingContext context)
			{
				return context.Fisher.heightLevel == 2;
			}

			// Token: 0x0600492A RID: 18730 RVA: 0x006D0C0B File Offset: 0x006CEE0B
			internal bool <.ctor>b__1_23(FishingContext context)
			{
				return context.Fisher.heightLevel == 3;
			}

			// Token: 0x0600492B RID: 18731 RVA: 0x006D0C1B File Offset: 0x006CEE1B
			internal bool <.ctor>b__1_24(FishingContext context)
			{
				return (double)context.Fisher.Y >= Main.rockLayer;
			}

			// Token: 0x0600492C RID: 18732 RVA: 0x006D0C33 File Offset: 0x006CEE33
			internal bool <.ctor>b__1_25(FishingContext context)
			{
				return context.RolledCorruption;
			}

			// Token: 0x0600492D RID: 18733 RVA: 0x006D0C3B File Offset: 0x006CEE3B
			internal bool <.ctor>b__1_26(FishingContext context)
			{
				return context.RolledCrimson;
			}

			// Token: 0x0600492E RID: 18734 RVA: 0x006D0C43 File Offset: 0x006CEE43
			internal bool <.ctor>b__1_27(FishingContext context)
			{
				return context.RolledJungle;
			}

			// Token: 0x0600492F RID: 18735 RVA: 0x006D0C4B File Offset: 0x006CEE4B
			internal bool <.ctor>b__1_28(FishingContext context)
			{
				return context.RolledSnow;
			}

			// Token: 0x06004930 RID: 18736 RVA: 0x006D0C53 File Offset: 0x006CEE53
			internal bool <.ctor>b__1_29(FishingContext context)
			{
				return context.RolledDesert;
			}

			// Token: 0x06004931 RID: 18737 RVA: 0x006D0C5B File Offset: 0x006CEE5B
			internal bool <.ctor>b__1_30(FishingContext context)
			{
				return context.RolledInfectedDesert && context.Player.ZoneHallow;
			}

			// Token: 0x06004932 RID: 18738 RVA: 0x006D0C72 File Offset: 0x006CEE72
			internal bool <.ctor>b__1_31(FishingContext context)
			{
				return AFishDropRulePopulator.IsOriginalOcean(context);
			}

			// Token: 0x06004933 RID: 18739 RVA: 0x006D0C7A File Offset: 0x006CEE7A
			internal bool <.ctor>b__1_32(FishingContext context)
			{
				return context.RolledRemixOcean;
			}

			// Token: 0x06004934 RID: 18740 RVA: 0x006D0C82 File Offset: 0x006CEE82
			internal bool <.ctor>b__1_33(FishingContext context)
			{
				return context.RolledRemixOcean || AFishDropRulePopulator.IsOriginalOcean(context);
			}

			// Token: 0x06004935 RID: 18741 RVA: 0x006D0C94 File Offset: 0x006CEE94
			internal bool <.ctor>b__1_34(FishingContext context)
			{
				return context.Fisher.waterTilesCount > 1000;
			}

			// Token: 0x06004936 RID: 18742 RVA: 0x006D0CA8 File Offset: 0x006CEEA8
			internal bool <.ctor>b__1_35(FishingContext context)
			{
				return Main.bloodMoon;
			}

			// Token: 0x06004937 RID: 18743 RVA: 0x006D0CAF File Offset: 0x006CEEAF
			internal bool <.ctor>b__1_36(FishingContext context)
			{
				return !NPC.combatBookWasUsed;
			}

			// Token: 0x040075F2 RID: 30194
			public static readonly AFishDropRulePopulator.<>c <>9 = new AFishDropRulePopulator.<>c();

			// Token: 0x040075F3 RID: 30195
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_0;

			// Token: 0x040075F4 RID: 30196
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_1;

			// Token: 0x040075F5 RID: 30197
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_2;

			// Token: 0x040075F6 RID: 30198
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_3;

			// Token: 0x040075F7 RID: 30199
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_4;

			// Token: 0x040075F8 RID: 30200
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_5;

			// Token: 0x040075F9 RID: 30201
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_6;

			// Token: 0x040075FA RID: 30202
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_7;

			// Token: 0x040075FB RID: 30203
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_8;

			// Token: 0x040075FC RID: 30204
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_9;

			// Token: 0x040075FD RID: 30205
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_10;

			// Token: 0x040075FE RID: 30206
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_11;

			// Token: 0x040075FF RID: 30207
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_12;

			// Token: 0x04007600 RID: 30208
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_13;

			// Token: 0x04007601 RID: 30209
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_14;

			// Token: 0x04007602 RID: 30210
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_15;

			// Token: 0x04007603 RID: 30211
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_16;

			// Token: 0x04007604 RID: 30212
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_17;

			// Token: 0x04007605 RID: 30213
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_18;

			// Token: 0x04007606 RID: 30214
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_19;

			// Token: 0x04007607 RID: 30215
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_20;

			// Token: 0x04007608 RID: 30216
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_21;

			// Token: 0x04007609 RID: 30217
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_22;

			// Token: 0x0400760A RID: 30218
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_23;

			// Token: 0x0400760B RID: 30219
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_24;

			// Token: 0x0400760C RID: 30220
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_25;

			// Token: 0x0400760D RID: 30221
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_26;

			// Token: 0x0400760E RID: 30222
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_27;

			// Token: 0x0400760F RID: 30223
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_28;

			// Token: 0x04007610 RID: 30224
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_29;

			// Token: 0x04007611 RID: 30225
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_30;

			// Token: 0x04007612 RID: 30226
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_31;

			// Token: 0x04007613 RID: 30227
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_32;

			// Token: 0x04007614 RID: 30228
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_33;

			// Token: 0x04007615 RID: 30229
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_34;

			// Token: 0x04007616 RID: 30230
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_35;

			// Token: 0x04007617 RID: 30231
			public static AFishDropRulePopulator.DelegateFishingCondition.MatchCondition <>9__1_36;
		}
	}
}
