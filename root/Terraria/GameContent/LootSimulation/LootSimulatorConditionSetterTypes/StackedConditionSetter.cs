using System;

namespace Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes
{
	// Token: 0x020002EE RID: 750
	public class StackedConditionSetter : ISimulationConditionSetter
	{
		// Token: 0x06002668 RID: 9832 RVA: 0x0055F0CB File Offset: 0x0055D2CB
		public StackedConditionSetter(params ISimulationConditionSetter[] setters)
		{
			this._setters = setters;
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x0055F0DC File Offset: 0x0055D2DC
		public void Setup(SimulatorInfo info)
		{
			for (int i = 0; i < this._setters.Length; i++)
			{
				this._setters[i].Setup(info);
			}
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x0055F10C File Offset: 0x0055D30C
		public void TearDown(SimulatorInfo info)
		{
			for (int i = 0; i < this._setters.Length; i++)
			{
				this._setters[i].TearDown(info);
			}
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x000379E9 File Offset: 0x00035BE9
		public int GetTimesToRunMultiplier(SimulatorInfo info)
		{
			return 1;
		}

		// Token: 0x04005088 RID: 20616
		private ISimulationConditionSetter[] _setters;
	}
}
