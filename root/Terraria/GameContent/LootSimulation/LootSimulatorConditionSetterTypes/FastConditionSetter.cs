using System;

namespace Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes
{
	// Token: 0x020002ED RID: 749
	public class FastConditionSetter : ISimulationConditionSetter
	{
		// Token: 0x06002664 RID: 9828 RVA: 0x0055F089 File Offset: 0x0055D289
		public FastConditionSetter(Action<SimulatorInfo> setup, Action<SimulatorInfo> tearDown)
		{
			this._setup = setup;
			this._tearDown = tearDown;
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x0055F09F File Offset: 0x0055D29F
		public void Setup(SimulatorInfo info)
		{
			if (this._setup != null)
			{
				this._setup(info);
			}
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x0055F0B5 File Offset: 0x0055D2B5
		public void TearDown(SimulatorInfo info)
		{
			if (this._tearDown != null)
			{
				this._tearDown(info);
			}
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x000379E9 File Offset: 0x00035BE9
		public int GetTimesToRunMultiplier(SimulatorInfo info)
		{
			return 1;
		}

		// Token: 0x04005086 RID: 20614
		private Action<SimulatorInfo> _setup;

		// Token: 0x04005087 RID: 20615
		private Action<SimulatorInfo> _tearDown;
	}
}
