using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x02000241 RID: 577
	public struct PlayerPettingInfo
	{
		// Token: 0x060022AA RID: 8874 RVA: 0x00539942 File Offset: 0x00537B42
		public PlayerPettingInfo(NPC npc, Vector2 offsetFromPet, bool isPetSmall)
		{
			this.isPetting = false;
			this.npc = npc.whoAmI;
			this.proj = -1;
			this.type = npc.type;
			this.offsetFromPet = offsetFromPet;
			this.isPetSmall = isPetSmall;
			this.mount = false;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x0053997F File Offset: 0x00537B7F
		public PlayerPettingInfo(Projectile proj, Vector2 offsetFromPet, bool isPetSmall)
		{
			this.isPetting = false;
			this.npc = -1;
			this.proj = proj.whoAmI;
			this.type = proj.type;
			this.offsetFromPet = offsetFromPet;
			this.isPetSmall = isPetSmall;
			this.mount = false;
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x005399BC File Offset: 0x00537BBC
		public PlayerPettingInfo(int mountId, bool isPetSmall)
		{
			this.isPetting = false;
			this.npc = -1;
			this.proj = -1;
			this.type = mountId;
			this.offsetFromPet = Vector2.Zero;
			this.isPetSmall = isPetSmall;
			this.mount = true;
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x005399F4 File Offset: 0x00537BF4
		public bool TryGetTarget(out Entity target)
		{
			if (this.npc >= 0)
			{
				NPC npc = Main.npc[this.npc];
				target = npc;
				return npc.active && npc.type == this.type;
			}
			if (this.mount)
			{
				target = null;
				return true;
			}
			Projectile projectile = Main.projectile[this.proj];
			target = projectile;
			return projectile.active && projectile.type == this.type;
		}

		// Token: 0x04004D09 RID: 19721
		public bool isPetting;

		// Token: 0x04004D0A RID: 19722
		public int npc;

		// Token: 0x04004D0B RID: 19723
		public int proj;

		// Token: 0x04004D0C RID: 19724
		public int type;

		// Token: 0x04004D0D RID: 19725
		public bool mount;

		// Token: 0x04004D0E RID: 19726
		public Vector2 offsetFromPet;

		// Token: 0x04004D0F RID: 19727
		public bool isPetSmall;
	}
}
