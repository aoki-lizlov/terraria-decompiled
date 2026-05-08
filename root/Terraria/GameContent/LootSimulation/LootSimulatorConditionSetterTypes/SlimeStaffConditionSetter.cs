using System;

namespace Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes
{
	// Token: 0x020002EF RID: 751
	public class SlimeStaffConditionSetter : ISimulationConditionSetter
	{
		// Token: 0x0600266C RID: 9836 RVA: 0x0055F13A File Offset: 0x0055D33A
		public SlimeStaffConditionSetter(int timesToRunMultiplier)
		{
			this._timesToRun = timesToRunMultiplier;
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x0055F14C File Offset: 0x0055D34C
		public int GetTimesToRunMultiplier(SimulatorInfo info)
		{
			int netID = info.npcVictim.netID;
			if (netID <= 147)
			{
				if (netID <= 1)
				{
					if (netID - -33 <= 1 || netID - -10 <= 7 || netID == 1)
					{
						goto IL_00C3;
					}
				}
				else if (netID <= 138)
				{
					if (netID == 16 || netID == 138)
					{
						goto IL_00C3;
					}
				}
				else if (netID == 141 || netID == 147)
				{
					goto IL_00C3;
				}
			}
			else if (netID <= 302)
			{
				if (netID <= 187)
				{
					if (netID == 184 || netID == 187)
					{
						goto IL_00C3;
					}
				}
				else if (netID == 204 || netID == 302)
				{
					goto IL_00C3;
				}
			}
			else if (netID <= 433)
			{
				if (netID - 333 <= 3 || netID == 433)
				{
					goto IL_00C3;
				}
			}
			else if (netID == 535 || netID == 537)
			{
				goto IL_00C3;
			}
			return 0;
			IL_00C3:
			return this._timesToRun;
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x00009E46 File Offset: 0x00008046
		public void Setup(SimulatorInfo info)
		{
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x00009E46 File Offset: 0x00008046
		public void TearDown(SimulatorInfo info)
		{
		}

		// Token: 0x04005089 RID: 20617
		private int _timesToRun;
	}
}
