using System;
using Terraria.ID;

namespace Terraria.DataStructures
{
	// Token: 0x02000556 RID: 1366
	public class NPCDebuffImmunityData
	{
		// Token: 0x06003797 RID: 14231 RVA: 0x0062F8AC File Offset: 0x0062DAAC
		public void ApplyToNPC(NPC npc)
		{
			if (this.ImmuneToWhips || this.ImmuneToAllBuffsThatAreNotWhips)
			{
				for (int i = 1; i < BuffID.Count; i++)
				{
					bool flag = BuffID.Sets.IsAnNPCWhipDebuff[i];
					bool flag2 = false;
					flag2 |= flag && this.ImmuneToWhips;
					flag2 |= !flag && this.ImmuneToAllBuffsThatAreNotWhips;
					npc.buffImmune[i] = flag2;
				}
			}
			if (this.SpecificallyImmuneTo != null)
			{
				for (int j = 0; j < this.SpecificallyImmuneTo.Length; j++)
				{
					int num = this.SpecificallyImmuneTo[j];
					npc.buffImmune[num] = true;
				}
			}
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x0000357B File Offset: 0x0000177B
		public NPCDebuffImmunityData()
		{
		}

		// Token: 0x04005BC6 RID: 23494
		public bool ImmuneToWhips;

		// Token: 0x04005BC7 RID: 23495
		public bool ImmuneToAllBuffsThatAreNotWhips;

		// Token: 0x04005BC8 RID: 23496
		public int[] SpecificallyImmuneTo;
	}
}
