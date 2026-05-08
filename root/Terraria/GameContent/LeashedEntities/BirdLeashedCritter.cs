using System;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x02000460 RID: 1120
	public class BirdLeashedCritter : FlyerLeashedCritter
	{
		// Token: 0x0600329C RID: 12956 RVA: 0x005F0B2C File Offset: 0x005EED2C
		public BirdLeashedCritter()
		{
			this.anchorStyle = 2;
			this.minWaitTime = 120;
			this.maxWaitTime = 420;
			this.maxFlySpeed = 1.2f;
			this.acceleration = 0.1f;
			this.rotationScalar = 0.25f;
			this.brakeDuration = 10;
			this.hoverAmplitude = 3f;
			this.hoverPeriod = 0.005f;
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x005F0B98 File Offset: 0x005EED98
		// Note: this type is marked as 'beforefieldinit'.
		static BirdLeashedCritter()
		{
		}

		// Token: 0x04005837 RID: 22583
		public new static BirdLeashedCritter Prototype = new BirdLeashedCritter();
	}
}
