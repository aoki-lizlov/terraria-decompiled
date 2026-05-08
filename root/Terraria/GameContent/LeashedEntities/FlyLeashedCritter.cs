using System;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x02000458 RID: 1112
	public abstract class FlyLeashedCritter : FlyerLeashedCritter
	{
		// Token: 0x0600327A RID: 12922 RVA: 0x005F039A File Offset: 0x005EE59A
		protected override void SetDefaults(Item sample)
		{
			base.SetDefaults(sample);
			this.scale = (float)Main.rand.Next(75, 111) * 0.01f;
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x005F03BE File Offset: 0x005EE5BE
		protected FlyLeashedCritter()
		{
		}
	}
}
