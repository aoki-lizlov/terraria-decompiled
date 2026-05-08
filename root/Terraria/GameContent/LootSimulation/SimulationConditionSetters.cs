using System;
using System.Runtime.CompilerServices;
using Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes;

namespace Terraria.GameContent.LootSimulation
{
	// Token: 0x020002EC RID: 748
	public class SimulationConditionSetters
	{
		// Token: 0x06002662 RID: 9826 RVA: 0x0000357B File Offset: 0x0000177B
		public SimulationConditionSetters()
		{
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x0055EE10 File Offset: 0x0055D010
		// Note: this type is marked as 'beforefieldinit'.
		static SimulationConditionSetters()
		{
		}

		// Token: 0x04005077 RID: 20599
		public static FastConditionSetter HardMode = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			Main.hardMode = true;
		}, delegate(SimulatorInfo info)
		{
			Main.hardMode = false;
		});

		// Token: 0x04005078 RID: 20600
		public static FastConditionSetter ExpertMode = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			Main.GameMode = 1;
			info.runningExpertMode = true;
		}, delegate(SimulatorInfo info)
		{
			Main.GameMode = 0;
			info.runningExpertMode = false;
		});

		// Token: 0x04005079 RID: 20601
		public static FastConditionSetter Eclipse = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			Main.eclipse = true;
		}, delegate(SimulatorInfo info)
		{
			Main.eclipse = false;
		});

		// Token: 0x0400507A RID: 20602
		public static FastConditionSetter BloodMoon = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			Main.bloodMoon = true;
		}, delegate(SimulatorInfo info)
		{
			Main.bloodMoon = false;
		});

		// Token: 0x0400507B RID: 20603
		public static FastConditionSetter SlainMechBosses = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			NPC.downedMechBoss1 = (NPC.downedMechBoss2 = (NPC.downedMechBoss3 = (NPC.downedMechBossAny = true)));
		}, delegate(SimulatorInfo info)
		{
			NPC.downedMechBoss1 = (NPC.downedMechBoss2 = (NPC.downedMechBoss3 = (NPC.downedMechBossAny = false)));
		});

		// Token: 0x0400507C RID: 20604
		public static FastConditionSetter SlainPlantera = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			NPC.downedPlantBoss = true;
		}, delegate(SimulatorInfo info)
		{
			NPC.downedPlantBoss = false;
		});

		// Token: 0x0400507D RID: 20605
		public static StackedConditionSetter ExpertAndHardMode = new StackedConditionSetter(new ISimulationConditionSetter[]
		{
			SimulationConditionSetters.ExpertMode,
			SimulationConditionSetters.HardMode
		});

		// Token: 0x0400507E RID: 20606
		public static FastConditionSetter WindyWeather = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			Main._shouldUseWindyDayMusic = true;
		}, delegate(SimulatorInfo info)
		{
			Main._shouldUseWindyDayMusic = false;
		});

		// Token: 0x0400507F RID: 20607
		public static FastConditionSetter MidDay = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			Main.dayTime = true;
			Main.time = 27000.0;
		}, delegate(SimulatorInfo info)
		{
			info.ReturnToOriginalDaytime();
		});

		// Token: 0x04005080 RID: 20608
		public static FastConditionSetter MidNight = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			Main.dayTime = false;
			Main.time = 16200.0;
		}, delegate(SimulatorInfo info)
		{
			info.ReturnToOriginalDaytime();
		});

		// Token: 0x04005081 RID: 20609
		public static FastConditionSetter SlimeRain = new FastConditionSetter(delegate(SimulatorInfo info)
		{
			Main.slimeRain = true;
		}, delegate(SimulatorInfo info)
		{
			Main.slimeRain = false;
		});

		// Token: 0x04005082 RID: 20610
		public static StackedConditionSetter WindyExpertHardmodeEndgameEclipseMorning = new StackedConditionSetter(new ISimulationConditionSetter[]
		{
			SimulationConditionSetters.WindyWeather,
			SimulationConditionSetters.ExpertMode,
			SimulationConditionSetters.HardMode,
			SimulationConditionSetters.SlainMechBosses,
			SimulationConditionSetters.SlainPlantera,
			SimulationConditionSetters.Eclipse,
			SimulationConditionSetters.MidDay
		});

		// Token: 0x04005083 RID: 20611
		public static StackedConditionSetter WindyExpertHardmodeEndgameBloodMoonNight = new StackedConditionSetter(new ISimulationConditionSetter[]
		{
			SimulationConditionSetters.WindyWeather,
			SimulationConditionSetters.ExpertMode,
			SimulationConditionSetters.HardMode,
			SimulationConditionSetters.SlainMechBosses,
			SimulationConditionSetters.SlainPlantera,
			SimulationConditionSetters.BloodMoon,
			SimulationConditionSetters.MidNight
		});

		// Token: 0x04005084 RID: 20612
		public static SlimeStaffConditionSetter SlimeStaffTest = new SlimeStaffConditionSetter(100);

		// Token: 0x04005085 RID: 20613
		public static LuckyCoinConditionSetter LuckyCoinTest = new LuckyCoinConditionSetter(100);

		// Token: 0x0200082C RID: 2092
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600432A RID: 17194 RVA: 0x006C0BED File Offset: 0x006BEDED
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600432B RID: 17195 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x0600432C RID: 17196 RVA: 0x006C0BF9 File Offset: 0x006BEDF9
			internal void <.cctor>b__16_0(SimulatorInfo info)
			{
				Main.hardMode = true;
			}

			// Token: 0x0600432D RID: 17197 RVA: 0x006C0C01 File Offset: 0x006BEE01
			internal void <.cctor>b__16_1(SimulatorInfo info)
			{
				Main.hardMode = false;
			}

			// Token: 0x0600432E RID: 17198 RVA: 0x006C0C09 File Offset: 0x006BEE09
			internal void <.cctor>b__16_2(SimulatorInfo info)
			{
				Main.GameMode = 1;
				info.runningExpertMode = true;
			}

			// Token: 0x0600432F RID: 17199 RVA: 0x006C0C18 File Offset: 0x006BEE18
			internal void <.cctor>b__16_3(SimulatorInfo info)
			{
				Main.GameMode = 0;
				info.runningExpertMode = false;
			}

			// Token: 0x06004330 RID: 17200 RVA: 0x006C0C27 File Offset: 0x006BEE27
			internal void <.cctor>b__16_4(SimulatorInfo info)
			{
				Main.eclipse = true;
			}

			// Token: 0x06004331 RID: 17201 RVA: 0x006C0C2F File Offset: 0x006BEE2F
			internal void <.cctor>b__16_5(SimulatorInfo info)
			{
				Main.eclipse = false;
			}

			// Token: 0x06004332 RID: 17202 RVA: 0x006C0C37 File Offset: 0x006BEE37
			internal void <.cctor>b__16_6(SimulatorInfo info)
			{
				Main.bloodMoon = true;
			}

			// Token: 0x06004333 RID: 17203 RVA: 0x006C0C3F File Offset: 0x006BEE3F
			internal void <.cctor>b__16_7(SimulatorInfo info)
			{
				Main.bloodMoon = false;
			}

			// Token: 0x06004334 RID: 17204 RVA: 0x006C0C47 File Offset: 0x006BEE47
			internal void <.cctor>b__16_8(SimulatorInfo info)
			{
				NPC.downedMechBoss1 = (NPC.downedMechBoss2 = (NPC.downedMechBoss3 = (NPC.downedMechBossAny = true)));
			}

			// Token: 0x06004335 RID: 17205 RVA: 0x006C0C61 File Offset: 0x006BEE61
			internal void <.cctor>b__16_9(SimulatorInfo info)
			{
				NPC.downedMechBoss1 = (NPC.downedMechBoss2 = (NPC.downedMechBoss3 = (NPC.downedMechBossAny = false)));
			}

			// Token: 0x06004336 RID: 17206 RVA: 0x006C0C7B File Offset: 0x006BEE7B
			internal void <.cctor>b__16_10(SimulatorInfo info)
			{
				NPC.downedPlantBoss = true;
			}

			// Token: 0x06004337 RID: 17207 RVA: 0x006C0C83 File Offset: 0x006BEE83
			internal void <.cctor>b__16_11(SimulatorInfo info)
			{
				NPC.downedPlantBoss = false;
			}

			// Token: 0x06004338 RID: 17208 RVA: 0x006C0C8B File Offset: 0x006BEE8B
			internal void <.cctor>b__16_12(SimulatorInfo info)
			{
				Main._shouldUseWindyDayMusic = true;
			}

			// Token: 0x06004339 RID: 17209 RVA: 0x006C0C93 File Offset: 0x006BEE93
			internal void <.cctor>b__16_13(SimulatorInfo info)
			{
				Main._shouldUseWindyDayMusic = false;
			}

			// Token: 0x0600433A RID: 17210 RVA: 0x006C0C9B File Offset: 0x006BEE9B
			internal void <.cctor>b__16_14(SimulatorInfo info)
			{
				Main.dayTime = true;
				Main.time = 27000.0;
			}

			// Token: 0x0600433B RID: 17211 RVA: 0x006C0CB1 File Offset: 0x006BEEB1
			internal void <.cctor>b__16_15(SimulatorInfo info)
			{
				info.ReturnToOriginalDaytime();
			}

			// Token: 0x0600433C RID: 17212 RVA: 0x006C0CB9 File Offset: 0x006BEEB9
			internal void <.cctor>b__16_16(SimulatorInfo info)
			{
				Main.dayTime = false;
				Main.time = 16200.0;
			}

			// Token: 0x0600433D RID: 17213 RVA: 0x006C0CB1 File Offset: 0x006BEEB1
			internal void <.cctor>b__16_17(SimulatorInfo info)
			{
				info.ReturnToOriginalDaytime();
			}

			// Token: 0x0600433E RID: 17214 RVA: 0x006C0CCF File Offset: 0x006BEECF
			internal void <.cctor>b__16_18(SimulatorInfo info)
			{
				Main.slimeRain = true;
			}

			// Token: 0x0600433F RID: 17215 RVA: 0x006C0CD7 File Offset: 0x006BEED7
			internal void <.cctor>b__16_19(SimulatorInfo info)
			{
				Main.slimeRain = false;
			}

			// Token: 0x04007271 RID: 29297
			public static readonly SimulationConditionSetters.<>c <>9 = new SimulationConditionSetters.<>c();
		}
	}
}
