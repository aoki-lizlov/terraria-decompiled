using System;

namespace Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes
{
	// Token: 0x020002F0 RID: 752
	public class LuckyCoinConditionSetter : ISimulationConditionSetter
	{
		// Token: 0x06002670 RID: 9840 RVA: 0x0055F222 File Offset: 0x0055D422
		public LuckyCoinConditionSetter(int timesToRunMultiplier)
		{
			this._timesToRun = timesToRunMultiplier;
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x0055F234 File Offset: 0x0055D434
		public int GetTimesToRunMultiplier(SimulatorInfo info)
		{
			int netID = info.npcVictim.netID;
			if (netID != 216 && netID != 491)
			{
				return 0;
			}
			return this._timesToRun;
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x00009E46 File Offset: 0x00008046
		public void Setup(SimulatorInfo info)
		{
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x00009E46 File Offset: 0x00008046
		public void TearDown(SimulatorInfo info)
		{
		}

		// Token: 0x0400508A RID: 20618
		private int _timesToRun;
	}
}
