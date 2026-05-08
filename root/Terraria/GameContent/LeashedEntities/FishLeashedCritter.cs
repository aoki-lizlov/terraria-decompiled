using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x02000462 RID: 1122
	public class FishLeashedCritter : FlyerLeashedCritter
	{
		// Token: 0x060032A1 RID: 12961 RVA: 0x005F0BEC File Offset: 0x005EEDEC
		public FishLeashedCritter()
		{
			this.anchorStyle = 3;
			this.minWaitTime = 120;
			this.maxFlySpeed = 0.5f;
			this.acceleration = 0.015f;
			this.hoverAmplitude = 10f;
			this.hoverPeriod = 0.003f;
			this.isAquatic = true;
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x005F0C41 File Offset: 0x005EEE41
		protected override void CopyToDummy()
		{
			base.CopyToDummy();
			LeashedCritter._dummy.wet = true;
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x005F0C54 File Offset: 0x005EEE54
		public override Vector2 GetDrawOffset()
		{
			return base.GetBobbingOffset();
		}

		// Token: 0x060032A4 RID: 12964 RVA: 0x005F0C5C File Offset: 0x005EEE5C
		// Note: this type is marked as 'beforefieldinit'.
		static FishLeashedCritter()
		{
		}

		// Token: 0x04005839 RID: 22585
		public new static FishLeashedCritter Prototype = new FishLeashedCritter();
	}
}
