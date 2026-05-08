using System;

namespace Terraria
{
	// Token: 0x0200003B RID: 59
	public static class NewProjectileModifiers
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x0012B877 File Offset: 0x00129A77
		public static void RainHazard(Projectile projectile)
		{
			projectile.netImportant = true;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0012B880 File Offset: 0x00129A80
		public static void IchorDartUpdatePenetrate(Projectile projectile)
		{
			if (Main.myPlayer != projectile.owner)
			{
				return;
			}
			if (projectile.ai[1] >= 0f)
			{
				projectile.maxPenetrate = (projectile.penetrate = -1);
				return;
			}
			if (projectile.penetrate < 0)
			{
				projectile.maxPenetrate = (projectile.penetrate = 1);
			}
		}
	}
}
