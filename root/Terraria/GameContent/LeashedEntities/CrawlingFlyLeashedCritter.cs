using System;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x0200045F RID: 1119
	public class CrawlingFlyLeashedCritter : FlyerLeashedCritter
	{
		// Token: 0x06003299 RID: 12953 RVA: 0x005F0AEB File Offset: 0x005EECEB
		public CrawlingFlyLeashedCritter()
		{
			this.hasGroundBias = true;
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x005F0AFA File Offset: 0x005EECFA
		protected override void SetDefaults(Item sample)
		{
			base.SetDefaults(sample);
			this.scale = Main.rand.NextFloat() * 0.2f + 0.7f;
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x005F0B1F File Offset: 0x005EED1F
		// Note: this type is marked as 'beforefieldinit'.
		static CrawlingFlyLeashedCritter()
		{
		}

		// Token: 0x04005836 RID: 22582
		public new static CrawlingFlyLeashedCritter Prototype = new CrawlingFlyLeashedCritter();
	}
}
