using System;

namespace Terraria.Audio
{
	// Token: 0x020005CB RID: 1483
	public class ProjectileAudioTracker
	{
		// Token: 0x06003A3A RID: 14906 RVA: 0x00655285 File Offset: 0x00653485
		public ProjectileAudioTracker(Projectile proj)
		{
			this._expectedIndex = proj.whoAmI;
			this._expectedType = proj.type;
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x006552A8 File Offset: 0x006534A8
		public bool IsActiveAndInGame()
		{
			if (Main.gameMenu)
			{
				return false;
			}
			Projectile projectile = Main.projectile[this._expectedIndex];
			return projectile.active && projectile.type == this._expectedType;
		}

		// Token: 0x04005DD7 RID: 24023
		private int _expectedType;

		// Token: 0x04005DD8 RID: 24024
		private int _expectedIndex;
	}
}
