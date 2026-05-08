using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002C5 RID: 709
	public static class CommonConditions
	{
		// Token: 0x060025D6 RID: 9686 RVA: 0x00559FB8 File Offset: 0x005581B8
		// Note: this type is marked as 'beforefieldinit'.
		static CommonConditions()
		{
		}

		// Token: 0x04005038 RID: 20536
		public static readonly ChromaCondition InMenu = new CommonConditions.SimpleCondition(() => Main.gameMenu && !Main.drunkWorld);

		// Token: 0x04005039 RID: 20537
		public static readonly ChromaCondition DrunkMenu = new CommonConditions.SimpleCondition(() => Main.gameMenu && Main.drunkWorld);

		// Token: 0x02000813 RID: 2067
		private class SimpleCondition : ChromaCondition
		{
			// Token: 0x060042F1 RID: 17137 RVA: 0x006C0009 File Offset: 0x006BE209
			public SimpleCondition(Func<bool> condition)
			{
				this._condition = condition;
			}

			// Token: 0x060042F2 RID: 17138 RVA: 0x006C0018 File Offset: 0x006BE218
			public override bool IsActive()
			{
				return this._condition();
			}

			// Token: 0x040071F5 RID: 29173
			private Func<bool> _condition;
		}

		// Token: 0x02000814 RID: 2068
		private class SceneCondition : CommonConditions.SimpleCondition
		{
			// Token: 0x060042F3 RID: 17139 RVA: 0x006C0028 File Offset: 0x006BE228
			public SceneCondition(Func<SceneMetrics, bool> condition)
				: base(() => condition(Main.SceneMetrics))
			{
			}

			// Token: 0x02000ACD RID: 2765
			[CompilerGenerated]
			private sealed class <>c__DisplayClass0_0
			{
				// Token: 0x06004C78 RID: 19576 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass0_0()
				{
				}

				// Token: 0x06004C79 RID: 19577 RVA: 0x006DAE00 File Offset: 0x006D9000
				internal bool <.ctor>b__0()
				{
					return this.condition(Main.SceneMetrics);
				}

				// Token: 0x0400787E RID: 30846
				public Func<SceneMetrics, bool> condition;
			}
		}

		// Token: 0x02000815 RID: 2069
		private class PlayerCondition : CommonConditions.SimpleCondition
		{
			// Token: 0x060042F4 RID: 17140 RVA: 0x006C0054 File Offset: 0x006BE254
			public PlayerCondition(Func<Player, bool> condition)
				: base(() => condition(Main.LocalPlayer))
			{
			}

			// Token: 0x02000ACE RID: 2766
			[CompilerGenerated]
			private sealed class <>c__DisplayClass0_0
			{
				// Token: 0x06004C7A RID: 19578 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass0_0()
				{
				}

				// Token: 0x06004C7B RID: 19579 RVA: 0x006DAE12 File Offset: 0x006D9012
				internal bool <.ctor>b__0()
				{
					return this.condition(Main.LocalPlayer);
				}

				// Token: 0x0400787F RID: 30847
				public Func<Player, bool> condition;
			}
		}

		// Token: 0x02000816 RID: 2070
		public static class SurfaceBiome
		{
			// Token: 0x060042F5 RID: 17141 RVA: 0x006C0080 File Offset: 0x006BE280
			// Note: this type is marked as 'beforefieldinit'.
			static SurfaceBiome()
			{
			}

			// Token: 0x040071F6 RID: 29174
			public static readonly ChromaCondition Ocean = new CommonConditions.SurfaceBiome.SurfaceCondition((SceneMetrics scene) => scene.ZoneBeach);

			// Token: 0x040071F7 RID: 29175
			public static readonly ChromaCondition Desert = new CommonConditions.SurfaceBiome.SurfaceCondition((SceneMetrics scene) => scene.ZoneDesert);

			// Token: 0x040071F8 RID: 29176
			public static readonly ChromaCondition Jungle = new CommonConditions.SurfaceBiome.SurfaceCondition((SceneMetrics scene) => scene.ZoneJungle);

			// Token: 0x040071F9 RID: 29177
			public static readonly ChromaCondition Snow = new CommonConditions.SurfaceBiome.SurfaceCondition((SceneMetrics scene) => scene.ZoneSnow);

			// Token: 0x040071FA RID: 29178
			public static readonly ChromaCondition Mushroom = new CommonConditions.SurfaceBiome.SurfaceCondition((SceneMetrics scene) => scene.ZoneGlowshroom);

			// Token: 0x040071FB RID: 29179
			public static readonly ChromaCondition Corruption = new CommonConditions.SurfaceBiome.SurfaceCondition((SceneMetrics scene) => scene.ZoneCorrupt);

			// Token: 0x040071FC RID: 29180
			public static readonly ChromaCondition Hallow = new CommonConditions.SurfaceBiome.SurfaceCondition((SceneMetrics scene) => scene.ZoneHallow);

			// Token: 0x040071FD RID: 29181
			public static readonly ChromaCondition Crimson = new CommonConditions.SurfaceBiome.SurfaceCondition((SceneMetrics scene) => scene.ZoneCrimson);

			// Token: 0x02000ACF RID: 2767
			private class SurfaceCondition : CommonConditions.SceneCondition
			{
				// Token: 0x06004C7C RID: 19580 RVA: 0x006DAE24 File Offset: 0x006D9024
				public SurfaceCondition(Func<SceneMetrics, bool> condition)
					: base((SceneMetrics scene) => scene.ZoneOverworldHeight && condition(scene))
				{
				}

				// Token: 0x02000B19 RID: 2841
				[CompilerGenerated]
				private sealed class <>c__DisplayClass0_0
				{
					// Token: 0x06004DC0 RID: 19904 RVA: 0x0000357B File Offset: 0x0000177B
					public <>c__DisplayClass0_0()
					{
					}

					// Token: 0x06004DC1 RID: 19905 RVA: 0x006DC921 File Offset: 0x006DAB21
					internal bool <.ctor>b__0(SceneMetrics scene)
					{
						return scene.ZoneOverworldHeight && this.condition(scene);
					}

					// Token: 0x04007921 RID: 31009
					public Func<SceneMetrics, bool> condition;
				}
			}

			// Token: 0x02000AD0 RID: 2768
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004C7D RID: 19581 RVA: 0x006DAE50 File Offset: 0x006D9050
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004C7E RID: 19582 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004C7F RID: 19583 RVA: 0x006DAE5C File Offset: 0x006D905C
				internal bool <.cctor>b__9_0(SceneMetrics scene)
				{
					return scene.ZoneBeach;
				}

				// Token: 0x06004C80 RID: 19584 RVA: 0x006DAE64 File Offset: 0x006D9064
				internal bool <.cctor>b__9_1(SceneMetrics scene)
				{
					return scene.ZoneDesert;
				}

				// Token: 0x06004C81 RID: 19585 RVA: 0x006DAE6C File Offset: 0x006D906C
				internal bool <.cctor>b__9_2(SceneMetrics scene)
				{
					return scene.ZoneJungle;
				}

				// Token: 0x06004C82 RID: 19586 RVA: 0x006DAE74 File Offset: 0x006D9074
				internal bool <.cctor>b__9_3(SceneMetrics scene)
				{
					return scene.ZoneSnow;
				}

				// Token: 0x06004C83 RID: 19587 RVA: 0x006DAE7C File Offset: 0x006D907C
				internal bool <.cctor>b__9_4(SceneMetrics scene)
				{
					return scene.ZoneGlowshroom;
				}

				// Token: 0x06004C84 RID: 19588 RVA: 0x006DAE84 File Offset: 0x006D9084
				internal bool <.cctor>b__9_5(SceneMetrics scene)
				{
					return scene.ZoneCorrupt;
				}

				// Token: 0x06004C85 RID: 19589 RVA: 0x006DAE8C File Offset: 0x006D908C
				internal bool <.cctor>b__9_6(SceneMetrics scene)
				{
					return scene.ZoneHallow;
				}

				// Token: 0x06004C86 RID: 19590 RVA: 0x006DAE94 File Offset: 0x006D9094
				internal bool <.cctor>b__9_7(SceneMetrics scene)
				{
					return scene.ZoneCrimson;
				}

				// Token: 0x04007880 RID: 30848
				public static readonly CommonConditions.SurfaceBiome.<>c <>9 = new CommonConditions.SurfaceBiome.<>c();
			}
		}

		// Token: 0x02000817 RID: 2071
		public static class MiscBiome
		{
			// Token: 0x060042F6 RID: 17142 RVA: 0x006C015D File Offset: 0x006BE35D
			// Note: this type is marked as 'beforefieldinit'.
			static MiscBiome()
			{
			}

			// Token: 0x040071FE RID: 29182
			public static readonly ChromaCondition Meteorite = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneMeteor);

			// Token: 0x02000AD1 RID: 2769
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004C87 RID: 19591 RVA: 0x006DAE9C File Offset: 0x006D909C
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004C88 RID: 19592 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004C89 RID: 19593 RVA: 0x006DAEA8 File Offset: 0x006D90A8
				internal bool <.cctor>b__1_0(SceneMetrics scene)
				{
					return scene.ZoneMeteor;
				}

				// Token: 0x04007881 RID: 30849
				public static readonly CommonConditions.MiscBiome.<>c <>9 = new CommonConditions.MiscBiome.<>c();
			}
		}

		// Token: 0x02000818 RID: 2072
		public static class UndergroundBiome
		{
			// Token: 0x060042F7 RID: 17143 RVA: 0x006C017C File Offset: 0x006BE37C
			// Note: this type is marked as 'beforefieldinit'.
			static UndergroundBiome()
			{
			}

			// Token: 0x040071FF RID: 29183
			public static readonly ChromaCondition Hive = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneHive);

			// Token: 0x04007200 RID: 29184
			public static readonly ChromaCondition Jungle = new CommonConditions.UndergroundBiome.UndergroundCondition((SceneMetrics scene) => scene.ZoneJungle);

			// Token: 0x04007201 RID: 29185
			public static readonly ChromaCondition Mushroom = new CommonConditions.UndergroundBiome.UndergroundCondition((SceneMetrics scene) => scene.ZoneGlowshroom);

			// Token: 0x04007202 RID: 29186
			public static readonly ChromaCondition Ice = new CommonConditions.UndergroundBiome.UndergroundCondition((SceneMetrics scene) => scene.ZoneSnow);

			// Token: 0x04007203 RID: 29187
			public static readonly ChromaCondition HallowIce = new CommonConditions.UndergroundBiome.UndergroundCondition((SceneMetrics scene) => scene.ZoneSnow && scene.ZoneHallow);

			// Token: 0x04007204 RID: 29188
			public static readonly ChromaCondition CrimsonIce = new CommonConditions.UndergroundBiome.UndergroundCondition((SceneMetrics scene) => scene.ZoneSnow && scene.ZoneCrimson);

			// Token: 0x04007205 RID: 29189
			public static readonly ChromaCondition CorruptIce = new CommonConditions.UndergroundBiome.UndergroundCondition((SceneMetrics scene) => scene.ZoneSnow && scene.ZoneCorrupt);

			// Token: 0x04007206 RID: 29190
			public static readonly ChromaCondition Hallow = new CommonConditions.UndergroundBiome.UndergroundCondition((SceneMetrics scene) => scene.ZoneHallow);

			// Token: 0x04007207 RID: 29191
			public static readonly ChromaCondition Crimson = new CommonConditions.UndergroundBiome.UndergroundCondition((SceneMetrics scene) => scene.ZoneCrimson);

			// Token: 0x04007208 RID: 29192
			public static readonly ChromaCondition Corrupt = new CommonConditions.UndergroundBiome.UndergroundCondition((SceneMetrics scene) => scene.ZoneCorrupt);

			// Token: 0x04007209 RID: 29193
			public static readonly ChromaCondition Desert = new CommonConditions.UndergroundBiome.UndergroundCondition((SceneMetrics scene) => scene.ZoneDesert);

			// Token: 0x0400720A RID: 29194
			public static readonly ChromaCondition HallowDesert = new CommonConditions.UndergroundBiome.UndergroundCondition((SceneMetrics scene) => scene.ZoneDesert && scene.ZoneHallow);

			// Token: 0x0400720B RID: 29195
			public static readonly ChromaCondition CrimsonDesert = new CommonConditions.UndergroundBiome.UndergroundCondition((SceneMetrics scene) => scene.ZoneDesert && scene.ZoneCrimson);

			// Token: 0x0400720C RID: 29196
			public static readonly ChromaCondition CorruptDesert = new CommonConditions.UndergroundBiome.UndergroundCondition((SceneMetrics scene) => scene.ZoneDesert && scene.ZoneCorrupt);

			// Token: 0x0400720D RID: 29197
			public static readonly ChromaCondition Temple = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneLihzhardTemple);

			// Token: 0x0400720E RID: 29198
			public static readonly ChromaCondition Dungeon = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneDungeon);

			// Token: 0x0400720F RID: 29199
			public static readonly ChromaCondition Marble = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneMarble);

			// Token: 0x04007210 RID: 29200
			public static readonly ChromaCondition Granite = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneGranite);

			// Token: 0x04007211 RID: 29201
			public static readonly ChromaCondition GemCave = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneGemCave);

			// Token: 0x04007212 RID: 29202
			public static readonly ChromaCondition Shimmer = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneShimmer);

			// Token: 0x02000AD2 RID: 2770
			private class UndergroundCondition : CommonConditions.SceneCondition
			{
				// Token: 0x06004C8A RID: 19594 RVA: 0x006DAEB0 File Offset: 0x006D90B0
				public UndergroundCondition(Func<SceneMetrics, bool> condition)
					: base((SceneMetrics scene) => !scene.ZoneOverworldHeight && condition(scene))
				{
				}

				// Token: 0x02000B1A RID: 2842
				[CompilerGenerated]
				private sealed class <>c__DisplayClass0_0
				{
					// Token: 0x06004DC2 RID: 19906 RVA: 0x0000357B File Offset: 0x0000177B
					public <>c__DisplayClass0_0()
					{
					}

					// Token: 0x06004DC3 RID: 19907 RVA: 0x006DC939 File Offset: 0x006DAB39
					internal bool <.ctor>b__0(SceneMetrics scene)
					{
						return !scene.ZoneOverworldHeight && this.condition(scene);
					}

					// Token: 0x04007922 RID: 31010
					public Func<SceneMetrics, bool> condition;
				}
			}

			// Token: 0x02000AD3 RID: 2771
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004C8B RID: 19595 RVA: 0x006DAEDC File Offset: 0x006D90DC
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004C8C RID: 19596 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004C8D RID: 19597 RVA: 0x006DAEE8 File Offset: 0x006D90E8
				internal bool <.cctor>b__21_0(SceneMetrics scene)
				{
					return scene.ZoneHive;
				}

				// Token: 0x06004C8E RID: 19598 RVA: 0x006DAE6C File Offset: 0x006D906C
				internal bool <.cctor>b__21_1(SceneMetrics scene)
				{
					return scene.ZoneJungle;
				}

				// Token: 0x06004C8F RID: 19599 RVA: 0x006DAE7C File Offset: 0x006D907C
				internal bool <.cctor>b__21_2(SceneMetrics scene)
				{
					return scene.ZoneGlowshroom;
				}

				// Token: 0x06004C90 RID: 19600 RVA: 0x006DAE74 File Offset: 0x006D9074
				internal bool <.cctor>b__21_3(SceneMetrics scene)
				{
					return scene.ZoneSnow;
				}

				// Token: 0x06004C91 RID: 19601 RVA: 0x006DAEF0 File Offset: 0x006D90F0
				internal bool <.cctor>b__21_4(SceneMetrics scene)
				{
					return scene.ZoneSnow && scene.ZoneHallow;
				}

				// Token: 0x06004C92 RID: 19602 RVA: 0x006DAF02 File Offset: 0x006D9102
				internal bool <.cctor>b__21_5(SceneMetrics scene)
				{
					return scene.ZoneSnow && scene.ZoneCrimson;
				}

				// Token: 0x06004C93 RID: 19603 RVA: 0x006DAF14 File Offset: 0x006D9114
				internal bool <.cctor>b__21_6(SceneMetrics scene)
				{
					return scene.ZoneSnow && scene.ZoneCorrupt;
				}

				// Token: 0x06004C94 RID: 19604 RVA: 0x006DAE8C File Offset: 0x006D908C
				internal bool <.cctor>b__21_7(SceneMetrics scene)
				{
					return scene.ZoneHallow;
				}

				// Token: 0x06004C95 RID: 19605 RVA: 0x006DAE94 File Offset: 0x006D9094
				internal bool <.cctor>b__21_8(SceneMetrics scene)
				{
					return scene.ZoneCrimson;
				}

				// Token: 0x06004C96 RID: 19606 RVA: 0x006DAE84 File Offset: 0x006D9084
				internal bool <.cctor>b__21_9(SceneMetrics scene)
				{
					return scene.ZoneCorrupt;
				}

				// Token: 0x06004C97 RID: 19607 RVA: 0x006DAE64 File Offset: 0x006D9064
				internal bool <.cctor>b__21_10(SceneMetrics scene)
				{
					return scene.ZoneDesert;
				}

				// Token: 0x06004C98 RID: 19608 RVA: 0x006DAF26 File Offset: 0x006D9126
				internal bool <.cctor>b__21_11(SceneMetrics scene)
				{
					return scene.ZoneDesert && scene.ZoneHallow;
				}

				// Token: 0x06004C99 RID: 19609 RVA: 0x006DAF38 File Offset: 0x006D9138
				internal bool <.cctor>b__21_12(SceneMetrics scene)
				{
					return scene.ZoneDesert && scene.ZoneCrimson;
				}

				// Token: 0x06004C9A RID: 19610 RVA: 0x006DAF4A File Offset: 0x006D914A
				internal bool <.cctor>b__21_13(SceneMetrics scene)
				{
					return scene.ZoneDesert && scene.ZoneCorrupt;
				}

				// Token: 0x06004C9B RID: 19611 RVA: 0x006DAF5C File Offset: 0x006D915C
				internal bool <.cctor>b__21_14(SceneMetrics scene)
				{
					return scene.ZoneLihzhardTemple;
				}

				// Token: 0x06004C9C RID: 19612 RVA: 0x006DAF64 File Offset: 0x006D9164
				internal bool <.cctor>b__21_15(SceneMetrics scene)
				{
					return scene.ZoneDungeon;
				}

				// Token: 0x06004C9D RID: 19613 RVA: 0x006DAF6C File Offset: 0x006D916C
				internal bool <.cctor>b__21_16(SceneMetrics scene)
				{
					return scene.ZoneMarble;
				}

				// Token: 0x06004C9E RID: 19614 RVA: 0x006DAF74 File Offset: 0x006D9174
				internal bool <.cctor>b__21_17(SceneMetrics scene)
				{
					return scene.ZoneGranite;
				}

				// Token: 0x06004C9F RID: 19615 RVA: 0x006DAF7C File Offset: 0x006D917C
				internal bool <.cctor>b__21_18(SceneMetrics scene)
				{
					return scene.ZoneGemCave;
				}

				// Token: 0x06004CA0 RID: 19616 RVA: 0x006DAF84 File Offset: 0x006D9184
				internal bool <.cctor>b__21_19(SceneMetrics scene)
				{
					return scene.ZoneShimmer;
				}

				// Token: 0x04007882 RID: 30850
				public static readonly CommonConditions.UndergroundBiome.<>c <>9 = new CommonConditions.UndergroundBiome.<>c();
			}
		}

		// Token: 0x02000819 RID: 2073
		public static class Boss
		{
			// Token: 0x060042F8 RID: 17144 RVA: 0x006C0394 File Offset: 0x006BE594
			// Note: this type is marked as 'beforefieldinit'.
			static Boss()
			{
			}

			// Token: 0x04007213 RID: 29203
			public static int HighestTierBossOrEvent;

			// Token: 0x04007214 RID: 29204
			public static readonly ChromaCondition EaterOfWorlds = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 13);

			// Token: 0x04007215 RID: 29205
			public static readonly ChromaCondition Destroyer = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 134);

			// Token: 0x04007216 RID: 29206
			public static readonly ChromaCondition KingSlime = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 50);

			// Token: 0x04007217 RID: 29207
			public static readonly ChromaCondition QueenSlime = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 657);

			// Token: 0x04007218 RID: 29208
			public static readonly ChromaCondition BrainOfCthulhu = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 266);

			// Token: 0x04007219 RID: 29209
			public static readonly ChromaCondition DukeFishron = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 370);

			// Token: 0x0400721A RID: 29210
			public static readonly ChromaCondition QueenBee = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 222);

			// Token: 0x0400721B RID: 29211
			public static readonly ChromaCondition Plantera = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 262);

			// Token: 0x0400721C RID: 29212
			public static readonly ChromaCondition Empress = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 636);

			// Token: 0x0400721D RID: 29213
			public static readonly ChromaCondition EyeOfCthulhu = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 4);

			// Token: 0x0400721E RID: 29214
			public static readonly ChromaCondition TheTwins = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 126);

			// Token: 0x0400721F RID: 29215
			public static readonly ChromaCondition MoonLord = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 398);

			// Token: 0x04007220 RID: 29216
			public static readonly ChromaCondition WallOfFlesh = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 113);

			// Token: 0x04007221 RID: 29217
			public static readonly ChromaCondition Golem = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 245);

			// Token: 0x04007222 RID: 29218
			public static readonly ChromaCondition Cultist = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 439);

			// Token: 0x04007223 RID: 29219
			public static readonly ChromaCondition Skeletron = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 35);

			// Token: 0x04007224 RID: 29220
			public static readonly ChromaCondition SkeletronPrime = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 127);

			// Token: 0x04007225 RID: 29221
			public static readonly ChromaCondition Deerclops = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == 668);

			// Token: 0x02000AD4 RID: 2772
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004CA1 RID: 19617 RVA: 0x006DAF8C File Offset: 0x006D918C
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004CA2 RID: 19618 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004CA3 RID: 19619 RVA: 0x006DAF98 File Offset: 0x006D9198
				internal bool <.cctor>b__19_0(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 13;
				}

				// Token: 0x06004CA4 RID: 19620 RVA: 0x006DAFA3 File Offset: 0x006D91A3
				internal bool <.cctor>b__19_1(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 134;
				}

				// Token: 0x06004CA5 RID: 19621 RVA: 0x006DAFB1 File Offset: 0x006D91B1
				internal bool <.cctor>b__19_2(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 50;
				}

				// Token: 0x06004CA6 RID: 19622 RVA: 0x006DAFBC File Offset: 0x006D91BC
				internal bool <.cctor>b__19_3(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 657;
				}

				// Token: 0x06004CA7 RID: 19623 RVA: 0x006DAFCA File Offset: 0x006D91CA
				internal bool <.cctor>b__19_4(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 266;
				}

				// Token: 0x06004CA8 RID: 19624 RVA: 0x006DAFD8 File Offset: 0x006D91D8
				internal bool <.cctor>b__19_5(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 370;
				}

				// Token: 0x06004CA9 RID: 19625 RVA: 0x006DAFE6 File Offset: 0x006D91E6
				internal bool <.cctor>b__19_6(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 222;
				}

				// Token: 0x06004CAA RID: 19626 RVA: 0x006DAFF4 File Offset: 0x006D91F4
				internal bool <.cctor>b__19_7(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 262;
				}

				// Token: 0x06004CAB RID: 19627 RVA: 0x006DB002 File Offset: 0x006D9202
				internal bool <.cctor>b__19_8(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 636;
				}

				// Token: 0x06004CAC RID: 19628 RVA: 0x006DB010 File Offset: 0x006D9210
				internal bool <.cctor>b__19_9(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 4;
				}

				// Token: 0x06004CAD RID: 19629 RVA: 0x006DB01A File Offset: 0x006D921A
				internal bool <.cctor>b__19_10(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 126;
				}

				// Token: 0x06004CAE RID: 19630 RVA: 0x006DB025 File Offset: 0x006D9225
				internal bool <.cctor>b__19_11(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 398;
				}

				// Token: 0x06004CAF RID: 19631 RVA: 0x006DB033 File Offset: 0x006D9233
				internal bool <.cctor>b__19_12(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 113;
				}

				// Token: 0x06004CB0 RID: 19632 RVA: 0x006DB03E File Offset: 0x006D923E
				internal bool <.cctor>b__19_13(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 245;
				}

				// Token: 0x06004CB1 RID: 19633 RVA: 0x006DB04C File Offset: 0x006D924C
				internal bool <.cctor>b__19_14(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 439;
				}

				// Token: 0x06004CB2 RID: 19634 RVA: 0x006DB05A File Offset: 0x006D925A
				internal bool <.cctor>b__19_15(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 35;
				}

				// Token: 0x06004CB3 RID: 19635 RVA: 0x006DB065 File Offset: 0x006D9265
				internal bool <.cctor>b__19_16(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 127;
				}

				// Token: 0x06004CB4 RID: 19636 RVA: 0x006DB070 File Offset: 0x006D9270
				internal bool <.cctor>b__19_17(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == 668;
				}

				// Token: 0x04007883 RID: 30851
				public static readonly CommonConditions.Boss.<>c <>9 = new CommonConditions.Boss.<>c();
			}
		}

		// Token: 0x0200081A RID: 2074
		public static class Weather
		{
			// Token: 0x060042F9 RID: 17145 RVA: 0x006C0578 File Offset: 0x006BE778
			// Note: this type is marked as 'beforefieldinit'.
			static Weather()
			{
			}

			// Token: 0x04007226 RID: 29222
			public static readonly ChromaCondition Rain = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneRain && !scene.ZoneSnow);

			// Token: 0x04007227 RID: 29223
			public static readonly ChromaCondition Sandstorm = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneSandstorm);

			// Token: 0x04007228 RID: 29224
			public static readonly ChromaCondition Blizzard = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneSnow && scene.ZoneRain);

			// Token: 0x04007229 RID: 29225
			public static readonly ChromaCondition SlimeRain = new CommonConditions.SceneCondition((SceneMetrics scene) => Main.slimeRain && scene.ZoneOverworldHeight);

			// Token: 0x02000AD5 RID: 2773
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004CB5 RID: 19637 RVA: 0x006DB07E File Offset: 0x006D927E
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004CB6 RID: 19638 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004CB7 RID: 19639 RVA: 0x006DB08A File Offset: 0x006D928A
				internal bool <.cctor>b__4_0(SceneMetrics scene)
				{
					return scene.ZoneRain && !scene.ZoneSnow;
				}

				// Token: 0x06004CB8 RID: 19640 RVA: 0x006DB09F File Offset: 0x006D929F
				internal bool <.cctor>b__4_1(SceneMetrics scene)
				{
					return scene.ZoneSandstorm;
				}

				// Token: 0x06004CB9 RID: 19641 RVA: 0x006DB0A7 File Offset: 0x006D92A7
				internal bool <.cctor>b__4_2(SceneMetrics scene)
				{
					return scene.ZoneSnow && scene.ZoneRain;
				}

				// Token: 0x06004CBA RID: 19642 RVA: 0x006DB0B9 File Offset: 0x006D92B9
				internal bool <.cctor>b__4_3(SceneMetrics scene)
				{
					return Main.slimeRain && scene.ZoneOverworldHeight;
				}

				// Token: 0x04007884 RID: 30852
				public static readonly CommonConditions.Weather.<>c <>9 = new CommonConditions.Weather.<>c();
			}
		}

		// Token: 0x0200081B RID: 2075
		public static class Depth
		{
			// Token: 0x060042FA RID: 17146 RVA: 0x006C05F0 File Offset: 0x006BE7F0
			private static bool IsInFrontOfDirtWall(Point tilePosition)
			{
				if (!WorldGen.InWorld(tilePosition.X, tilePosition.Y, 0))
				{
					return false;
				}
				if (Main.tile[tilePosition.X, tilePosition.Y] == null)
				{
					return false;
				}
				ushort wall = Main.tile[tilePosition.X, tilePosition.Y].wall;
				if (wall <= 61)
				{
					if (wall <= 16)
					{
						if (wall != 2 && wall != 16)
						{
							return false;
						}
					}
					else if (wall - 54 > 5 && wall != 61)
					{
						return false;
					}
				}
				else if (wall <= 185)
				{
					if (wall - 170 > 1 && wall != 185)
					{
						return false;
					}
				}
				else if (wall - 196 > 3 && wall - 212 > 3)
				{
					return false;
				}
				return true;
			}

			// Token: 0x060042FB RID: 17147 RVA: 0x006C06A0 File Offset: 0x006BE8A0
			// Note: this type is marked as 'beforefieldinit'.
			static Depth()
			{
			}

			// Token: 0x0400722A RID: 29226
			public static readonly ChromaCondition Sky = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneSkyHeight);

			// Token: 0x0400722B RID: 29227
			public static readonly ChromaCondition Surface = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneOverworldHeight && !CommonConditions.Depth.IsInFrontOfDirtWall(scene.TileCenter));

			// Token: 0x0400722C RID: 29228
			public static readonly ChromaCondition Vines = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneOverworldHeight && CommonConditions.Depth.IsInFrontOfDirtWall(scene.TileCenter));

			// Token: 0x0400722D RID: 29229
			public static readonly ChromaCondition Underground = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneDirtLayerHeight);

			// Token: 0x0400722E RID: 29230
			public static readonly ChromaCondition Caverns = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneRockLayerHeight && scene.TileCenter.Y <= Main.maxTilesY - 400);

			// Token: 0x0400722F RID: 29231
			public static readonly ChromaCondition Magma = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneRockLayerHeight && scene.TileCenter.Y > Main.maxTilesY - 400);

			// Token: 0x04007230 RID: 29232
			public static readonly ChromaCondition Underworld = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.ZoneUnderworldHeight);

			// Token: 0x02000AD6 RID: 2774
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004CBB RID: 19643 RVA: 0x006DB0CA File Offset: 0x006D92CA
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004CBC RID: 19644 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004CBD RID: 19645 RVA: 0x006DB0D6 File Offset: 0x006D92D6
				internal bool <.cctor>b__8_0(SceneMetrics scene)
				{
					return scene.ZoneSkyHeight;
				}

				// Token: 0x06004CBE RID: 19646 RVA: 0x006DB0DE File Offset: 0x006D92DE
				internal bool <.cctor>b__8_1(SceneMetrics scene)
				{
					return scene.ZoneOverworldHeight && !CommonConditions.Depth.IsInFrontOfDirtWall(scene.TileCenter);
				}

				// Token: 0x06004CBF RID: 19647 RVA: 0x006DB0F8 File Offset: 0x006D92F8
				internal bool <.cctor>b__8_2(SceneMetrics scene)
				{
					return scene.ZoneOverworldHeight && CommonConditions.Depth.IsInFrontOfDirtWall(scene.TileCenter);
				}

				// Token: 0x06004CC0 RID: 19648 RVA: 0x006DB10F File Offset: 0x006D930F
				internal bool <.cctor>b__8_3(SceneMetrics scene)
				{
					return scene.ZoneDirtLayerHeight;
				}

				// Token: 0x06004CC1 RID: 19649 RVA: 0x006DB117 File Offset: 0x006D9317
				internal bool <.cctor>b__8_4(SceneMetrics scene)
				{
					return scene.ZoneRockLayerHeight && scene.TileCenter.Y <= Main.maxTilesY - 400;
				}

				// Token: 0x06004CC2 RID: 19650 RVA: 0x006DB13E File Offset: 0x006D933E
				internal bool <.cctor>b__8_5(SceneMetrics scene)
				{
					return scene.ZoneRockLayerHeight && scene.TileCenter.Y > Main.maxTilesY - 400;
				}

				// Token: 0x06004CC3 RID: 19651 RVA: 0x006DB162 File Offset: 0x006D9362
				internal bool <.cctor>b__8_6(SceneMetrics scene)
				{
					return scene.ZoneUnderworldHeight;
				}

				// Token: 0x04007885 RID: 30853
				public static readonly CommonConditions.Depth.<>c <>9 = new CommonConditions.Depth.<>c();
			}
		}

		// Token: 0x0200081C RID: 2076
		public static class Events
		{
			// Token: 0x060042FC RID: 17148 RVA: 0x006C0764 File Offset: 0x006BE964
			// Note: this type is marked as 'beforefieldinit'.
			static Events()
			{
			}

			// Token: 0x04007231 RID: 29233
			public static readonly ChromaCondition BloodMoon = new CommonConditions.SceneCondition((SceneMetrics _) => Main.bloodMoon && !Main.snowMoon && !Main.pumpkinMoon);

			// Token: 0x04007232 RID: 29234
			public static readonly ChromaCondition FrostMoon = new CommonConditions.SceneCondition((SceneMetrics _) => Main.snowMoon);

			// Token: 0x04007233 RID: 29235
			public static readonly ChromaCondition PumpkinMoon = new CommonConditions.SceneCondition((SceneMetrics _) => Main.pumpkinMoon);

			// Token: 0x04007234 RID: 29236
			public static readonly ChromaCondition SolarEclipse = new CommonConditions.SceneCondition((SceneMetrics _) => Main.eclipse);

			// Token: 0x04007235 RID: 29237
			public static readonly ChromaCondition SolarPillar = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.CloseEnoughToSolarTower);

			// Token: 0x04007236 RID: 29238
			public static readonly ChromaCondition NebulaPillar = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.CloseEnoughToNebulaTower);

			// Token: 0x04007237 RID: 29239
			public static readonly ChromaCondition VortexPillar = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.CloseEnoughToVortexTower);

			// Token: 0x04007238 RID: 29240
			public static readonly ChromaCondition StardustPillar = new CommonConditions.SceneCondition((SceneMetrics scene) => scene.CloseEnoughToStardustTower);

			// Token: 0x04007239 RID: 29241
			public static readonly ChromaCondition PirateInvasion = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == -3);

			// Token: 0x0400723A RID: 29242
			public static readonly ChromaCondition DD2Event = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == -6);

			// Token: 0x0400723B RID: 29243
			public static readonly ChromaCondition FrostLegion = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == -2);

			// Token: 0x0400723C RID: 29244
			public static readonly ChromaCondition MartianMadness = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == -4);

			// Token: 0x0400723D RID: 29245
			public static readonly ChromaCondition GoblinArmy = new CommonConditions.SceneCondition((SceneMetrics _) => CommonConditions.Boss.HighestTierBossOrEvent == -1);

			// Token: 0x02000AD7 RID: 2775
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004CC4 RID: 19652 RVA: 0x006DB16A File Offset: 0x006D936A
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004CC5 RID: 19653 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004CC6 RID: 19654 RVA: 0x006DB176 File Offset: 0x006D9376
				internal bool <.cctor>b__13_0(SceneMetrics _)
				{
					return Main.bloodMoon && !Main.snowMoon && !Main.pumpkinMoon;
				}

				// Token: 0x06004CC7 RID: 19655 RVA: 0x006DB190 File Offset: 0x006D9390
				internal bool <.cctor>b__13_1(SceneMetrics _)
				{
					return Main.snowMoon;
				}

				// Token: 0x06004CC8 RID: 19656 RVA: 0x006C0FBF File Offset: 0x006BF1BF
				internal bool <.cctor>b__13_2(SceneMetrics _)
				{
					return Main.pumpkinMoon;
				}

				// Token: 0x06004CC9 RID: 19657 RVA: 0x006DB197 File Offset: 0x006D9397
				internal bool <.cctor>b__13_3(SceneMetrics _)
				{
					return Main.eclipse;
				}

				// Token: 0x06004CCA RID: 19658 RVA: 0x006DB19E File Offset: 0x006D939E
				internal bool <.cctor>b__13_4(SceneMetrics scene)
				{
					return scene.CloseEnoughToSolarTower;
				}

				// Token: 0x06004CCB RID: 19659 RVA: 0x006DB1A6 File Offset: 0x006D93A6
				internal bool <.cctor>b__13_5(SceneMetrics scene)
				{
					return scene.CloseEnoughToNebulaTower;
				}

				// Token: 0x06004CCC RID: 19660 RVA: 0x006DB1AE File Offset: 0x006D93AE
				internal bool <.cctor>b__13_6(SceneMetrics scene)
				{
					return scene.CloseEnoughToVortexTower;
				}

				// Token: 0x06004CCD RID: 19661 RVA: 0x006DB1B6 File Offset: 0x006D93B6
				internal bool <.cctor>b__13_7(SceneMetrics scene)
				{
					return scene.CloseEnoughToStardustTower;
				}

				// Token: 0x06004CCE RID: 19662 RVA: 0x006DB1BE File Offset: 0x006D93BE
				internal bool <.cctor>b__13_8(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == -3;
				}

				// Token: 0x06004CCF RID: 19663 RVA: 0x006DB1C9 File Offset: 0x006D93C9
				internal bool <.cctor>b__13_9(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == -6;
				}

				// Token: 0x06004CD0 RID: 19664 RVA: 0x006DB1D4 File Offset: 0x006D93D4
				internal bool <.cctor>b__13_10(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == -2;
				}

				// Token: 0x06004CD1 RID: 19665 RVA: 0x006DB1DF File Offset: 0x006D93DF
				internal bool <.cctor>b__13_11(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == -4;
				}

				// Token: 0x06004CD2 RID: 19666 RVA: 0x006DB1EA File Offset: 0x006D93EA
				internal bool <.cctor>b__13_12(SceneMetrics _)
				{
					return CommonConditions.Boss.HighestTierBossOrEvent == -1;
				}

				// Token: 0x04007886 RID: 30854
				public static readonly CommonConditions.Events.<>c <>9 = new CommonConditions.Events.<>c();
			}
		}

		// Token: 0x0200081D RID: 2077
		public static class Alert
		{
			// Token: 0x060042FD RID: 17149 RVA: 0x006C08C4 File Offset: 0x006BEAC4
			// Note: this type is marked as 'beforefieldinit'.
			static Alert()
			{
			}

			// Token: 0x0400723E RID: 29246
			public static readonly ChromaCondition MoonlordComing = new CommonConditions.SceneCondition((SceneMetrics _) => NPC.MoonLordCountdown > 0);

			// Token: 0x0400723F RID: 29247
			public static readonly ChromaCondition Keybinds = new CommonConditions.SimpleCondition(() => Main.InGameUI.CurrentState == Main.ManageControlsMenu || Main.MenuUI.CurrentState == Main.ManageControlsMenu);

			// Token: 0x04007240 RID: 29248
			public static readonly ChromaCondition Drowning = new CommonConditions.PlayerCondition((Player player) => player.breath != player.breathMax);

			// Token: 0x04007241 RID: 29249
			public static readonly ChromaCondition LavaIndicator = new CommonConditions.PlayerCondition((Player player) => player.lavaWet);

			// Token: 0x02000AD8 RID: 2776
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004CD3 RID: 19667 RVA: 0x006DB1F4 File Offset: 0x006D93F4
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004CD4 RID: 19668 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004CD5 RID: 19669 RVA: 0x006DB200 File Offset: 0x006D9400
				internal bool <.cctor>b__4_0(SceneMetrics _)
				{
					return NPC.MoonLordCountdown > 0;
				}

				// Token: 0x06004CD6 RID: 19670 RVA: 0x006DB20A File Offset: 0x006D940A
				internal bool <.cctor>b__4_1()
				{
					return Main.InGameUI.CurrentState == Main.ManageControlsMenu || Main.MenuUI.CurrentState == Main.ManageControlsMenu;
				}

				// Token: 0x06004CD7 RID: 19671 RVA: 0x006DB230 File Offset: 0x006D9430
				internal bool <.cctor>b__4_2(Player player)
				{
					return player.breath != player.breathMax;
				}

				// Token: 0x06004CD8 RID: 19672 RVA: 0x006DB243 File Offset: 0x006D9443
				internal bool <.cctor>b__4_3(Player player)
				{
					return player.lavaWet;
				}

				// Token: 0x04007887 RID: 30855
				public static readonly CommonConditions.Alert.<>c <>9 = new CommonConditions.Alert.<>c();
			}
		}

		// Token: 0x0200081E RID: 2078
		public static class CriticalAlert
		{
			// Token: 0x060042FE RID: 17150 RVA: 0x006C0939 File Offset: 0x006BEB39
			// Note: this type is marked as 'beforefieldinit'.
			static CriticalAlert()
			{
			}

			// Token: 0x04007242 RID: 29250
			public static readonly ChromaCondition LowLife = new CommonConditions.PlayerCondition((Player player) => Main.ChromaPainter.PotionAlert);

			// Token: 0x04007243 RID: 29251
			public static readonly ChromaCondition Death = new CommonConditions.PlayerCondition((Player player) => player.dead);

			// Token: 0x02000AD9 RID: 2777
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004CD9 RID: 19673 RVA: 0x006DB24B File Offset: 0x006D944B
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004CDA RID: 19674 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004CDB RID: 19675 RVA: 0x006DB257 File Offset: 0x006D9457
				internal bool <.cctor>b__2_0(Player player)
				{
					return Main.ChromaPainter.PotionAlert;
				}

				// Token: 0x06004CDC RID: 19676 RVA: 0x006DB263 File Offset: 0x006D9463
				internal bool <.cctor>b__2_1(Player player)
				{
					return player.dead;
				}

				// Token: 0x04007888 RID: 30856
				public static readonly CommonConditions.CriticalAlert.<>c <>9 = new CommonConditions.CriticalAlert.<>c();
			}
		}

		// Token: 0x0200081F RID: 2079
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060042FF RID: 17151 RVA: 0x006C096F File Offset: 0x006BEB6F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004300 RID: 17152 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004301 RID: 17153 RVA: 0x006C097B File Offset: 0x006BEB7B
			internal bool <.cctor>b__14_0()
			{
				return Main.gameMenu && !Main.drunkWorld;
			}

			// Token: 0x06004302 RID: 17154 RVA: 0x006C098E File Offset: 0x006BEB8E
			internal bool <.cctor>b__14_1()
			{
				return Main.gameMenu && Main.drunkWorld;
			}

			// Token: 0x04007244 RID: 29252
			public static readonly CommonConditions.<>c <>9 = new CommonConditions.<>c();
		}
	}
}
