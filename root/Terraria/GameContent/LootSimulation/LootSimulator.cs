using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using ReLogic.OS;
using Terraria.ID;

namespace Terraria.GameContent.LootSimulation
{
	// Token: 0x020002E8 RID: 744
	public class LootSimulator
	{
		// Token: 0x06002650 RID: 9808 RVA: 0x0055E7EE File Offset: 0x0055C9EE
		public LootSimulator()
		{
			this.FillDesiredTestConditions();
			this.FillItemExclusions();
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x0055E81C File Offset: 0x0055CA1C
		private void FillItemExclusions()
		{
			List<int> list = new List<int>();
			list.AddRange(from tuple in ItemID.Sets.IsAPickup.Select((bool state, int index) => new { index, state })
				where tuple.state
				select tuple.index);
			list.AddRange(from tuple in ItemID.Sets.CommonCoin.Select((bool state, int index) => new { index, state })
				where tuple.state
				select tuple.index);
			this._excludedItemIds = list.ToArray();
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x0055E92C File Offset: 0x0055CB2C
		private void FillDesiredTestConditions()
		{
			this._neededTestConditions.AddRange(new List<ISimulationConditionSetter>
			{
				SimulationConditionSetters.MidDay,
				SimulationConditionSetters.MidNight,
				SimulationConditionSetters.HardMode,
				SimulationConditionSetters.ExpertMode,
				SimulationConditionSetters.ExpertAndHardMode,
				SimulationConditionSetters.WindyExpertHardmodeEndgameBloodMoonNight,
				SimulationConditionSetters.WindyExpertHardmodeEndgameEclipseMorning,
				SimulationConditionSetters.SlimeStaffTest,
				SimulationConditionSetters.LuckyCoinTest
			});
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x0055E9AC File Offset: 0x0055CBAC
		public void Run()
		{
			int num = 10000;
			this.SetCleanSlateWorldConditions();
			string text = "";
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			for (int i = -65; i < (int)NPCID.Count; i++)
			{
				string text2;
				if (this.TryGettingLootFor(i, num, out text2))
				{
					text = text + text2 + "\n\n";
				}
			}
			stopwatch.Stop();
			text += string.Format("\nSimulation Took {0} seconds to complete.\n", (float)stopwatch.ElapsedMilliseconds / 1000f);
			Platform.Get<IClipboard>().Value = text;
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x0055EA3C File Offset: 0x0055CC3C
		private void SetCleanSlateWorldConditions()
		{
			Main.dayTime = true;
			Main.time = 27000.0;
			Main.hardMode = false;
			Main.GameMode = 0;
			NPC.downedMechBoss1 = false;
			NPC.downedMechBoss2 = false;
			NPC.downedMechBoss3 = false;
			NPC.downedMechBossAny = false;
			NPC.downedPlantBoss = false;
			Main._shouldUseWindyDayMusic = false;
			Main._shouldUseStormMusic = false;
			Main.eclipse = false;
			Main.bloodMoon = false;
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x0055EAA0 File Offset: 0x0055CCA0
		private bool TryGettingLootFor(int npcNetId, int timesMultiplier, out string outputText)
		{
			SimulatorInfo simulatorInfo = new SimulatorInfo();
			NPC npc = new NPC();
			npc.SetDefaults(npcNetId, default(NPCSpawnParams));
			simulatorInfo.npcVictim = npc;
			LootSimulationItemCounter lootSimulationItemCounter = new LootSimulationItemCounter();
			simulatorInfo.itemCounter = lootSimulationItemCounter;
			foreach (ISimulationConditionSetter simulationConditionSetter in this._neededTestConditions)
			{
				simulationConditionSetter.Setup(simulatorInfo);
				int num = simulationConditionSetter.GetTimesToRunMultiplier(simulatorInfo) * timesMultiplier;
				for (int i = 0; i < num; i++)
				{
					npc.NPCLoot();
				}
				lootSimulationItemCounter.IncreaseTimesAttempted(num, simulatorInfo.runningExpertMode);
				simulationConditionSetter.TearDown(simulatorInfo);
				this.SetCleanSlateWorldConditions();
			}
			lootSimulationItemCounter.Exclude(this._excludedItemIds.ToArray<int>());
			string text = lootSimulationItemCounter.PrintCollectedItems(false);
			string text2 = lootSimulationItemCounter.PrintCollectedItems(true);
			string name = NPCID.Search.GetName(npcNetId);
			string text3 = string.Format("FindEntryByNPCID(NPCID.{0})", name);
			if (text.Length > 0)
			{
				text3 = string.Format("{0}\n.AddDropsNormalMode({1})", text3, text);
			}
			if (text2.Length > 0)
			{
				text3 = string.Format("{0}\n.AddDropsExpertMode({1})", text3, text2);
			}
			text3 += ";";
			outputText = text3;
			return text.Length > 0 || text2.Length > 0;
		}

		// Token: 0x0400506A RID: 20586
		private List<ISimulationConditionSetter> _neededTestConditions = new List<ISimulationConditionSetter>();

		// Token: 0x0400506B RID: 20587
		private int[] _excludedItemIds = new int[0];

		// Token: 0x02000829 RID: 2089
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600431B RID: 17179 RVA: 0x006C0B70 File Offset: 0x006BED70
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600431C RID: 17180 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x0600431D RID: 17181 RVA: 0x006C0B7C File Offset: 0x006BED7C
			internal <>f__AnonymousType2<int, bool> <FillItemExclusions>b__3_0(bool state, int index)
			{
				return new { index, state };
			}

			// Token: 0x0600431E RID: 17182 RVA: 0x006C0B85 File Offset: 0x006BED85
			internal bool <FillItemExclusions>b__3_1(<>f__AnonymousType2<int, bool> tuple)
			{
				return tuple.state;
			}

			// Token: 0x0600431F RID: 17183 RVA: 0x006C0B8D File Offset: 0x006BED8D
			internal int <FillItemExclusions>b__3_2(<>f__AnonymousType2<int, bool> tuple)
			{
				return tuple.index;
			}

			// Token: 0x06004320 RID: 17184 RVA: 0x006C0B7C File Offset: 0x006BED7C
			internal <>f__AnonymousType2<int, bool> <FillItemExclusions>b__3_3(bool state, int index)
			{
				return new { index, state };
			}

			// Token: 0x06004321 RID: 17185 RVA: 0x006C0B85 File Offset: 0x006BED85
			internal bool <FillItemExclusions>b__3_4(<>f__AnonymousType2<int, bool> tuple)
			{
				return tuple.state;
			}

			// Token: 0x06004322 RID: 17186 RVA: 0x006C0B8D File Offset: 0x006BED8D
			internal int <FillItemExclusions>b__3_5(<>f__AnonymousType2<int, bool> tuple)
			{
				return tuple.index;
			}

			// Token: 0x04007264 RID: 29284
			public static readonly LootSimulator.<>c <>9 = new LootSimulator.<>c();

			// Token: 0x04007265 RID: 29285
			public static Func<bool, int, <>f__AnonymousType2<int, bool>> <>9__3_0;

			// Token: 0x04007266 RID: 29286
			public static Func<<>f__AnonymousType2<int, bool>, bool> <>9__3_1;

			// Token: 0x04007267 RID: 29287
			public static Func<<>f__AnonymousType2<int, bool>, int> <>9__3_2;

			// Token: 0x04007268 RID: 29288
			public static Func<bool, int, <>f__AnonymousType2<int, bool>> <>9__3_3;

			// Token: 0x04007269 RID: 29289
			public static Func<<>f__AnonymousType2<int, bool>, bool> <>9__3_4;

			// Token: 0x0400726A RID: 29290
			public static Func<<>f__AnonymousType2<int, bool>, int> <>9__3_5;
		}
	}
}
