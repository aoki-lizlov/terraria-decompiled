using System;

namespace Terraria.GameContent.LootSimulation
{
	// Token: 0x020002EB RID: 747
	public interface ISimulationConditionSetter
	{
		// Token: 0x0600265F RID: 9823
		int GetTimesToRunMultiplier(SimulatorInfo info);

		// Token: 0x06002660 RID: 9824
		void Setup(SimulatorInfo info);

		// Token: 0x06002661 RID: 9825
		void TearDown(SimulatorInfo info);
	}
}
