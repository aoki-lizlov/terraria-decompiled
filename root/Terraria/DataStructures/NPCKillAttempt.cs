using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000558 RID: 1368
	public struct NPCKillAttempt
	{
		// Token: 0x060037A6 RID: 14246 RVA: 0x0062FB7B File Offset: 0x0062DD7B
		public NPCKillAttempt(NPC target)
		{
			this.npc = target;
			this.netId = target.netID;
			this.active = target.active;
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x0062FB9C File Offset: 0x0062DD9C
		public bool DidNPCDie()
		{
			return !this.npc.active;
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x0062FBAC File Offset: 0x0062DDAC
		public bool DidNPCDieOrTransform()
		{
			return this.DidNPCDie() || this.npc.netID != this.netId;
		}

		// Token: 0x04005BCC RID: 23500
		public readonly NPC npc;

		// Token: 0x04005BCD RID: 23501
		public readonly int netId;

		// Token: 0x04005BCE RID: 23502
		public readonly bool active;
	}
}
