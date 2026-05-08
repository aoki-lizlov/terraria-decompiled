using System;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x0200045E RID: 1118
	public class DragonflyLeashedCritter : FlyerLeashedCritter
	{
		// Token: 0x06003297 RID: 12951 RVA: 0x005F0AB1 File Offset: 0x005EECB1
		public DragonflyLeashedCritter()
		{
			this.minWaitTime = 10;
			this.maxFlySpeed = 2.5f;
			this.acceleration = 0.4f;
			this.brakeDuration = 10;
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x005F0ADF File Offset: 0x005EECDF
		// Note: this type is marked as 'beforefieldinit'.
		static DragonflyLeashedCritter()
		{
		}

		// Token: 0x04005835 RID: 22581
		public new static DragonflyLeashedCritter Prototype = new DragonflyLeashedCritter();
	}
}
